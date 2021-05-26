using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManager.Migrations
{
    public partial class intid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Contacts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { 1, "fadeniayobami@gmail.com", "Ayobami", "Fadeni", "+2348106322363", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { 2, "afolabiahmed@gmail.com", "Afolabi", "Ahmed", "+2348135372863", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { 3, "ibrahimtope@gmail.com", "Ibrahim", "Tope", "+2348165289045", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { 4, "eugeneuche@gmail.com", "Eugene", "Uche", "+2348109873426", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { 5, "victorumeh@gmail.com", "Victor", "Umeh", "+2348035871098", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { 6, "olumidejoda@gmail.com", "Olumide", "Joda", "+2348062341790", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Contacts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { "1", "fadeniayobami@gmail.com", "Ayobami", "Fadeni", "+2348106322363", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { "2", "afolabiahmed@gmail.com", "Afolabi", "Ahmed", "+2348135372863", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { "3", "ibrahimtope@gmail.com", "Ibrahim", "Tope", "+2348165289045", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { "4", "eugeneuche@gmail.com", "Eugene", "Uche", "+2348109873426", null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "PhotoUrl" },
                values: new object[] { "5", "victorumeh@gmail.com", "Victor", "Umeh", "+2348035871098", null });
        }
    }
}
