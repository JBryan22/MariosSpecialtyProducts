using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MariosSpeciality.Migrations
{
    public partial class AddDatabaseGeneratedOptionAttributeOnDatePostedPropertyOfTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedDate",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePosted",
                table: "Products",
                nullable: false,
                oldClrType: typeof(DateTime))
                .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedDate",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(DateTime))
                .OldAnnotation("MySql:ValueGeneratedOnAddOrUpdate", true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePosted",
                table: "Products",
                nullable: false,
                oldClrType: typeof(DateTime))
                .OldAnnotation("MySql:ValueGeneratedOnAddOrUpdate", true);
        }
    }
}
