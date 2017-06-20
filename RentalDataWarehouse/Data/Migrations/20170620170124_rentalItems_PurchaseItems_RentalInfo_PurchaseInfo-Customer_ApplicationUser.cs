using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalDataWarehouse.Data.Migrations
{
    public partial class rentalItems_PurchaseItems_RentalInfo_PurchaseInfoCustomer_ApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "RentalItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RentalItems");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PurchaseItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemInformationId",
                table: "RentalItems",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemInformationId",
                table: "PurchaseItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PurchaseInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TotalCopies = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalItems_ItemInformationId",
                table: "RentalItems",
                column: "ItemInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_ItemInformationId",
                table: "PurchaseItems",
                column: "ItemInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_PurchaseInfo_ItemInformationId",
                table: "PurchaseItems",
                column: "ItemInformationId",
                principalTable: "PurchaseInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalItems_RentalInfo_ItemInformationId",
                table: "RentalItems",
                column: "ItemInformationId",
                principalTable: "RentalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_PurchaseInfo_ItemInformationId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalItems_RentalInfo_ItemInformationId",
                table: "RentalItems");

            migrationBuilder.DropTable(
                name: "PurchaseInfo");

            migrationBuilder.DropTable(
                name: "RentalInfo");

            migrationBuilder.DropIndex(
                name: "IX_RentalItems_ItemInformationId",
                table: "RentalItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItems_ItemInformationId",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "ItemInformationId",
                table: "RentalItems");

            migrationBuilder.DropColumn(
                name: "ItemInformationId",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "RentalItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RentalItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "PurchaseItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PurchaseItems",
                nullable: true);
        }
    }
}
