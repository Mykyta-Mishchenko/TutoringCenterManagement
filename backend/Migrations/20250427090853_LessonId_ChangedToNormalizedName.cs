using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class LessonId_ChangedToNormalizedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_StudentLessons_LessonId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessons_TeacherLessons_LessonId",
                table: "StudentLessons");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "StudentLessons",
                newName: "TeacherLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLessons_LessonId",
                table: "StudentLessons",
                newName: "IX_StudentLessons_TeacherLessonId");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Reports",
                newName: "StudentLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_LessonId",
                table: "Reports",
                newName: "IX_Reports_StudentLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_StudentLessons_StudentLessonId",
                table: "Reports",
                column: "StudentLessonId",
                principalTable: "StudentLessons",
                principalColumn: "StudentLessonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessons_TeacherLessons_TeacherLessonId",
                table: "StudentLessons",
                column: "TeacherLessonId",
                principalTable: "TeacherLessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_StudentLessons_StudentLessonId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessons_TeacherLessons_TeacherLessonId",
                table: "StudentLessons");

            migrationBuilder.RenameColumn(
                name: "TeacherLessonId",
                table: "StudentLessons",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLessons_TeacherLessonId",
                table: "StudentLessons",
                newName: "IX_StudentLessons_LessonId");

            migrationBuilder.RenameColumn(
                name: "StudentLessonId",
                table: "Reports",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_StudentLessonId",
                table: "Reports",
                newName: "IX_Reports_LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_StudentLessons_LessonId",
                table: "Reports",
                column: "LessonId",
                principalTable: "StudentLessons",
                principalColumn: "StudentLessonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessons_TeacherLessons_LessonId",
                table: "StudentLessons",
                column: "LessonId",
                principalTable: "TeacherLessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
