using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN232_SU25_GroupProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovefieldparentIdtableStudentMedication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "StudentMedications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "StudentMedications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
