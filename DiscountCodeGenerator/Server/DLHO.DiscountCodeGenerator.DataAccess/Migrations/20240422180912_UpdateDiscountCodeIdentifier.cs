using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DLHO.DiscountCodeGenerator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiscountCodeIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCodes",
                table: "DiscountCodes");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DiscountCodes",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DiscountCodes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCodes",
                table: "DiscountCodes",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCodes",
                table: "DiscountCodes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DiscountCodes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DiscountCodes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCodes",
                table: "DiscountCodes",
                column: "Id");
        }
    }
}
