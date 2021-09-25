using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagment.Migrations
{
    public partial class SeedingDataCoursesTeachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CoursesTeachers",
                columns: new[] { "Id", "CourseId", "TeacherId" },
                values: new object[] { 1 , 1 , 1 }
                );

            migrationBuilder.InsertData(
                table: "CoursesTeachers",
                columns: new[] { "Id", "CourseId", "TeacherId" },
                values: new object[] { 2, 2, 2 }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [CoursesTeachers]");
        }
    }
}
