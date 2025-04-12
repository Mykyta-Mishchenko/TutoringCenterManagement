using backend.DTO.LessonsDTO;
using Microsoft.Identity.Client;

namespace backend.Interfaces.Services
{
    public interface ILessonsService
    {
        public Task<IList<SubjectDTO>> GetAllSubjectsAsync();
        public Task<IList<LessonDTO>> GetUserLessons(int userId);
    }
}
