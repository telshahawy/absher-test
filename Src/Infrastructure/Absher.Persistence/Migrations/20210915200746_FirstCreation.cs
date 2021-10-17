using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Absher.Persistence.Migrations
{
    public partial class FirstCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Audit");

            migrationBuilder.CreateTable(
                name: "AuditChangedData",
                schema: "Audit",
                columns: table => new
                {
                    AuditDataChangesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SchemaName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentifierSaveChangesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditChangedData", x => x.AuditDataChangesId);
                });

            migrationBuilder.CreateTable(
                name: "AuditUserAction",
                schema: "Audit",
                columns: table => new
                {
                    AuditUserActionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JsonData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditUserAction", x => x.AuditUserActionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditDataChangeType",
                schema: "Audit",
                table: "AuditChangedData",
                column: "ChangeType");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                schema: "Audit",
                table: "AuditChangedData",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreationDate",
                schema: "Audit",
                table: "AuditChangedData",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryKey",
                schema: "Audit",
                table: "AuditChangedData",
                column: "PrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaName",
                schema: "Audit",
                table: "AuditChangedData",
                column: "SchemaName");

            migrationBuilder.CreateIndex(
                name: "IX_TableName",
                schema: "Audit",
                table: "AuditChangedData",
                column: "TableName");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                schema: "Audit",
                table: "AuditUserAction",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreationDate",
                schema: "Audit",
                table: "AuditUserAction",
                column: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditChangedData",
                schema: "Audit");

            migrationBuilder.DropTable(
                name: "AuditUserAction",
                schema: "Audit");
        }
    }
}
