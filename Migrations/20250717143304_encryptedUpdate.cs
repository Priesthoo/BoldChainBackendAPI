using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoldChainBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class encryptedUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PerformedBy",
                table: "auditLogs",
                newName: "TrustStatus");

            migrationBuilder.RenameColumn(
                name: "PerformedAt",
                table: "auditLogs",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "auditLogs",
                newName: "SenderWallet");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "auditLogs",
                newName: "SenderEmail");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "auditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MessageHash",
                table: "auditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipientEmail",
                table: "auditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EncryptedPrivateKey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "auditLogs");

            migrationBuilder.DropColumn(
                name: "MessageHash",
                table: "auditLogs");

            migrationBuilder.DropColumn(
                name: "RecipientEmail",
                table: "auditLogs");

            migrationBuilder.DropColumn(
                name: "EncryptedPrivateKey",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TrustStatus",
                table: "auditLogs",
                newName: "PerformedBy");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "auditLogs",
                newName: "PerformedAt");

            migrationBuilder.RenameColumn(
                name: "SenderWallet",
                table: "auditLogs",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "SenderEmail",
                table: "auditLogs",
                newName: "Details");
        }
    }
}
