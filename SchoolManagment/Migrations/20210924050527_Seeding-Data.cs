using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SchoolManagment.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name", "Field" },
                values: new object[] { 1, "Khallaf", "Arabic" }
                );
            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name", "Field" },
                values: new object[] { 2, "Sahar", "Socials" }
                );


            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Fees", "IsPaid", "Name"},
                values: new object[] { 1, 100, true, "Ahmed" }
                );
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Fees", "IsPaid", "Name" },
                values: new object[] { 2, 200, false, "Omar" }
                );


            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Field", "Duration", "Fees" , "TeacherId" },
                values: new object[] { 1, "Arabic", DateTime.Now.AddDays(10), 100 , 1 }
                );
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Field", "Duration", "Fees", "TeacherId" },
                values: new object[] { 2, "Socials", DateTime.Now.AddDays(10), 100, 2 }
                );


            migrationBuilder.InsertData(
                table: "StudentsCourses",
                columns: new[] { "Id", "StudentID", "CourseID" },
                values: new object[] { 1, 1,2 }
                );
            migrationBuilder.InsertData(
                table: "StudentsCourses",
                columns: new[] { "Id", "StudentID", "CourseID" },
                values: new object[] { 2, 2, 1 }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [StudentsCourses]");
            migrationBuilder.Sql("DELETE FROM [Courses]");
            migrationBuilder.Sql("DELETE FROM [Students]");
            migrationBuilder.Sql("DELETE FROM [Teachers]");
        }
    }
}
