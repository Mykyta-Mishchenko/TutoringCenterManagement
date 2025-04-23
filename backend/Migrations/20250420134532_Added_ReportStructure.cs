using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Added_ReportStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessons_LessonTypes_TypeId",
                table: "TeacherLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessons_Schedules_ScheduleId",
                table: "TeacherLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentLessons",
                table: "StudentLessons");

            migrationBuilder.AddColumn<int>(
                name: "StudentLessonId",
                table: "StudentLessons",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentLessons",
                table: "StudentLessons",
                column: "StudentLessonId");

            migrationBuilder.CreateTable(
                name: "MarksTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarksTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_StudentLessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "StudentLessons",
                        principalColumn: "StudentLessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    MarkTypeId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => new { x.ReportId, x.MarkTypeId });
                    table.CheckConstraint("CK_Marks_Score", "[Score] >= 1 AND [Score] <= 10");
                    table.ForeignKey(
                        name: "FK_Marks_MarksTypes_MarkTypeId",
                        column: x => x.MarkTypeId,
                        principalTable: "MarksTypes",
                        principalColumn: "TypeId");
                    table.ForeignKey(
                        name: "FK_Marks_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessons_LessonId",
                table: "StudentLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_MarkTypeId",
                table: "Marks",
                column: "MarkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LessonId",
                table: "Reports",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessons_LessonTypes_TypeId",
                table: "TeacherLessons",
                column: "TypeId",
                principalTable: "LessonTypes",
                principalColumn: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessons_Schedules_ScheduleId",
                table: "TeacherLessons",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "DateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessons_LessonTypes_TypeId",
                table: "TeacherLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessons_Schedules_ScheduleId",
                table: "TeacherLessons");

            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "MarksTypes");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentLessons",
                table: "StudentLessons");

            migrationBuilder.DropIndex(
                name: "IX_StudentLessons_LessonId",
                table: "StudentLessons");

            migrationBuilder.DropColumn(
                name: "StudentLessonId",
                table: "StudentLessons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentLessons",
                table: "StudentLessons",
                columns: new[] { "LessonId", "StudentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessons_LessonTypes_TypeId",
                table: "TeacherLessons",
                column: "TypeId",
                principalTable: "LessonTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessons_Schedules_ScheduleId",
                table: "TeacherLessons",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "DateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
