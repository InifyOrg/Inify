﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletsMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WalletTypes",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1L, "EVM" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WalletTypes",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
