using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoucherApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVoucherModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReedemable",
                table: "Vouchers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReedemable",
                table: "Vouchers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
