using Microsoft.EntityFrameworkCore.Migrations;

namespace FELFELInventory.WebApi.Migrations
{
    public partial class Int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RemainingUnits",
                table: "Batches",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "OriginalUnitAmount",
                table: "Batches",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "OldAmount",
                table: "BatchChanges",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "NewAmount",
                table: "BatchChanges",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RemainingUnits",
                table: "Batches",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "OriginalUnitAmount",
                table: "Batches",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "OldAmount",
                table: "BatchChanges",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "NewAmount",
                table: "BatchChanges",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
