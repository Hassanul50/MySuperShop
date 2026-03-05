using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySuperShopData.Migrations
{
    /// <inheritdoc />
    public partial class UserIdUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Changing the IDENTITY property cannot be done with ALTER COLUMN on SQL Server.
            // Create a temporary column, copy data, drop the old column, then rename the temp column.

            migrationBuilder.AddColumn<string>(
                name: "UserId_temp",
                table: "users",
                type: "nvarchar(450)",
                nullable: true);

            // Copy existing int UserId values into the new string column
            migrationBuilder.Sql("UPDATE users SET UserId_temp = CAST(UserId AS nvarchar(450))");

            // Drop primary key so we can drop the old column
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            // Drop the old UserId column (int, identity)
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "users");

            // Rename temp column to UserId
            migrationBuilder.RenameColumn(
                name: "UserId_temp",
                table: "users",
                newName: "UserId");

            // Make the new UserId non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            // Re-create primary key on the new UserId column
            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the change: create a temporary int column, copy values (if numeric), then replace the string column.

            migrationBuilder.AddColumn<int>(
                name: "UserId_temp",
                table: "users",
                type: "int",
                nullable: true);

            // Try to convert string values back to int where possible. Non-numeric values will become NULL.
            migrationBuilder.Sql("UPDATE users SET UserId_temp = TRY_CAST(UserId AS int)");

            // Drop primary key so we can drop the string column
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            // Drop the string UserId column
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "users");

            // Rename temp int column to UserId
            migrationBuilder.RenameColumn(
                name: "UserId_temp",
                table: "users",
                newName: "UserId");

            // Make the int column non-nullable and set identity annotation. Note: if any rows have NULL in UserId
            // after TRY_CAST, you'll need to handle them before making the column non-nullable in a real scenario.
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("SqlServer:Identity", "1, 1");

            // Re-create primary key on the int UserId column
            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "UserId");
        }
    }
}
