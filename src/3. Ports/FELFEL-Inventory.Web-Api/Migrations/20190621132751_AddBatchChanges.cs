using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FELFELInventory.WebApi.Migrations
{
    public partial class AddBatchChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchChange_Batches_BatchId",
                table: "BatchChange");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BatchChange",
                table: "BatchChange");

            migrationBuilder.RenameTable(
                name: "BatchChange",
                newName: "BatchChanges");

            migrationBuilder.RenameIndex(
                name: "IX_BatchChange_BatchId",
                table: "BatchChanges",
                newName: "IX_BatchChanges_BatchId");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "BatchChanges",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NewAmount",
                table: "BatchChanges",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OldAmount",
                table: "BatchChanges",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOfChange",
                table: "BatchChanges",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BatchChanges",
                table: "BatchChanges",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchChanges_Batches_BatchId",
                table: "BatchChanges",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchChanges_Batches_BatchId",
                table: "BatchChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BatchChanges",
                table: "BatchChanges");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "BatchChanges");

            migrationBuilder.DropColumn(
                name: "NewAmount",
                table: "BatchChanges");

            migrationBuilder.DropColumn(
                name: "OldAmount",
                table: "BatchChanges");

            migrationBuilder.DropColumn(
                name: "TimeOfChange",
                table: "BatchChanges");

            migrationBuilder.RenameTable(
                name: "BatchChanges",
                newName: "BatchChange");

            migrationBuilder.RenameIndex(
                name: "IX_BatchChanges_BatchId",
                table: "BatchChange",
                newName: "IX_BatchChange_BatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BatchChange",
                table: "BatchChange",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchChange_Batches_BatchId",
                table: "BatchChange",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
