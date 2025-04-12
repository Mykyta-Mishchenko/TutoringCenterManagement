using backend.Data.DataModels;
using backend.DTO.UsersDTO;
using backend.DTO.UsersInfoDTO;
using backend.Interfaces.Repositories;
using backend.Models;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Net;

namespace JwtBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _identityDbContext;
        public UserRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task AddUserAsync(User user)
        {
            await _identityDbContext.Users.AddAsync(user);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(int Id)
        {
            return await _identityDbContext.Users.FindAsync(Id);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<string> GetUserProfileAsync(int userId)
        {
            var profile = await _identityDbContext.UsersProfile.FirstOrDefaultAsync(u => u.UserId == userId);
            return profile?.ProfileImgUrl ?? "";
        }

        public async Task SetUserProfileAsync(int userId, string profileImgUrl)
        {
            try
            {
                var dbUserProfile = await _identityDbContext.UsersProfile.FirstOrDefaultAsync(p => p.UserId == userId);
                var newUserProfile = new UserProfile
                {
                    UserId = userId,
                    ProfileImgUrl = profileImgUrl
                };

                if (dbUserProfile == null)
                {
                    await _identityDbContext.UsersProfile.AddAsync(newUserProfile);
                }
                else
                {
                    dbUserProfile.ProfileImgUrl = profileImgUrl;
                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task RemoveUserAsync(User user)
        {
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbUser != null)
            {
                _identityDbContext.Users.Remove(dbUser);
                await _identityDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbUser != null)
            {
                user.UserId = dbUser.UserId;
                _identityDbContext.Users.Update(user);
                await _identityDbContext.SaveChangesAsync();
            }
        }
        public async Task<UsersListDTO> GetUsersByFilterAsync(UsersFilterDTO filter)
        {
            var query = _identityDbContext.Users.AsQueryable();

            var role = filter.Role;

            query = query.Where(u => u.UserRoles.Any(r => r.Role.Name == role.ToString()));

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(u => (u.FirstName + " " + u.LastName).Contains(filter.Name));
            }

            if (filter.SubjectId != 0 || filter.SchoolYear != 0)
            {
                if(role == UserRole.teacher)
                {
                    query = query.Where(u => u.TeacherLessons.Any(l =>
                    (filter.SubjectId == 0 || l.LessonType.SubjectId == filter.SubjectId) &&
                    (filter.SchoolYear == 0 || l.LessonType.SchoolYear == filter.SchoolYear)));
                }
                else if(role == UserRole.student)
                {
                    query = query.Where(u => u.StudentLessons.Any(l =>
                    (filter.SubjectId == 0 || l.TeacherLesson.LessonType.SubjectId == filter.SubjectId) &&
                    (filter.SchoolYear == 0 || l.TeacherLesson.LessonType.SchoolYear == filter.SchoolYear)));
                }
            }

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / filter.PerPage);


            query = query.Skip((filter.Page - 1) * filter.PerPage).Take(filter.PerPage);

            List<UserInfoDTO> usersInfo = new List<UserInfoDTO>();

            if(role == UserRole.teacher)
            {
                usersInfo = await query.Select(u => new UserInfoDTO()
                {
                    UserId = u.UserId,
                    FullName = u.FirstName + " " + u.LastName,
                    Subjects = u.TeacherLessons
                    .GroupBy(l => l.LessonType.Subject.SubjectName)
                    .Select(g =>
                        new SubjectInfoDTO()
                        {
                            Name = g.Key,
                            MinSchoolYear = g.Min(l => l.LessonType.SchoolYear),
                            MaxSchoolYear = g.Max(l => l.LessonType.SchoolYear)
                        }).ToList()
                }).ToListAsync();
            }
            else if(role == UserRole.student)
            {
                usersInfo = await query.Select(u => new UserInfoDTO()
                {
                    UserId = u.UserId,
                    FullName = u.FirstName + " " + u.LastName,
                    Subjects = u.StudentLessons
                    .GroupBy(l => l.TeacherLesson.LessonType.Subject.SubjectName)
                    .Select(g =>
                        new SubjectInfoDTO()
                        {
                            Name = g.Key,
                            MinSchoolYear = g.Min(l => l.TeacherLesson.LessonType.SchoolYear),
                            MaxSchoolYear = g.Max(l => l.TeacherLesson.LessonType.SchoolYear)
                        }).ToList()
                }).ToListAsync();
            }
            
            return new UsersListDTO() { TotalPageNumber = totalPages, UsersList = usersInfo };
        }

        public async Task<UserInfoDTO> GetUserInfoAsync(int userId)
        {
            return await _identityDbContext.Users
                .Where(u => u.UserId == userId)
                .Select(u => new UserInfoDTO()
                {
                    UserId = userId,
                    FullName = u.FirstName + " " + u.LastName,
                    Subjects = u.TeacherLessons
                        .GroupBy(l => l.LessonType.Subject.SubjectName)
                        .Select(g => new SubjectInfoDTO()
                        {
                            Name = g.Key,
                            MinSchoolYear = g.Min(l => l.LessonType.SchoolYear),
                            MaxSchoolYear = g.Max(l => l.LessonType.SchoolYear)
                        }).ToList()
                }).FirstAsync();
        }
    }
}
