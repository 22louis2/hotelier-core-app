using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotelier_core_app.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTenantRoleCreationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Role",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_TenantId",
                table: "Role",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Tenant_TenantId",
                table: "Role",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Tenant_TenantId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_TenantId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Role");
        }
    }
}
