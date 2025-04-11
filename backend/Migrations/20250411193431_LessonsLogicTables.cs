using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class LessonsLogicTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    DateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    DayTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.DateId);
                    table.CheckConstraint("CK_Schedule_DayOfWeek", "[DayOfWeek] >= 1 AND [DayOfWeek] <= 7");
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "LessonTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SchoolYear = table.Column<int>(type: "int", nullable: false),
                    MaxStudentsCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTypes", x => x.TypeId);
                    table.CheckConstraint("CK_Lesson_MaxStudentsCount", "[MaxStudentsCount] >= 1 AND [MaxStudentsCount] <= 5");
                    table.CheckConstraint("CK_Lesson_Price", "[Price] >= 1 AND [Price] <= 5000");
                    table.CheckConstraint("CK_Lesson_SchoolYear", "[SchoolYear] >= 1 AND [SchoolYear] <= 12");
                    table.ForeignKey(
                        name: "FK_LessonTypes_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLessons", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_TeacherLessons_LessonTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "LessonTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLessons_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "DateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLessons_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessons", x => new { x.LessonId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentLessons_TeacherLessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "TeacherLessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLessons_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypes_SubjectId",
                table: "LessonTypes",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessons_StudentId",
                table: "StudentLessons",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessons_ScheduleId",
                table: "TeacherLessons",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessons_TeacherId",
                table: "TeacherLessons",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessons_TypeId",
                table: "TeacherLessons",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentLessons");

            migrationBuilder.DropTable(
                name: "TeacherLessons");

            migrationBuilder.DropTable(
                name: "LessonTypes");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
