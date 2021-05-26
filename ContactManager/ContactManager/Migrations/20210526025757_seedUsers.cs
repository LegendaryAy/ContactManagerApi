using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManager.Migrations
{
    public partial class seedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8729a0ba-809d-4744-b289-4890a7b48065", "61f384af-0f57-45f4-8762-8087ef28c5e2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b1531e19-eeb1-4e65-bfe4-037ca8f10a3b", "c734df57-2c80-4679-ab97-30c66e3a174f", "Regular User", "REGULAR USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4623c0c7-d2b2-4e4a-a65f-299329fe4c51", 0, "c3324c40-4363-4c9b-822d-03c21769ed0e", "fadeniayobami@gmail.com", false, "Ayobami", "Fadeni", false, null, "FADENIAYOBAMI@GMAIL.COM", "FADENIAYOBAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEBNuASUdXozD6Xu6xuCvgYrXSFLv/xG9lBw8+/uxIYHJAqJ6A7JhMW8RsQQHbLQKMA==", null, false, "6f6ea153-4b06-4fb0-8be4-6307f535b28f", false, "fadeniayobami@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a18935e1-95d7-4165-953d-69602f6c68dd", 0, "0a4adc03-e071-475d-a7ad-f2610fcd47cc", "fabian12@gmail.com", false, "Fabian", "Emmanuel", false, null, "FABIAN12@GMAIL.COM", "FABIAN12@GMAIL.COM", "AQAAAAEAACcQAAAAEMCr2fgwheiISnOvMuBpdqKv2WgKerXmb0Qm/jZh+DJKLFEEwRI5WUC51uzBLBeYIw==", null, false, "b584cf67-4bd9-4892-8b06-aeacc20f9d00", false, "fabian12@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8729a0ba-809d-4744-b289-4890a7b48065", "4623c0c7-d2b2-4e4a-a65f-299329fe4c51" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b1531e19-eeb1-4e65-bfe4-037ca8f10a3b", "a18935e1-95d7-4165-953d-69602f6c68dd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8729a0ba-809d-4744-b289-4890a7b48065", "4623c0c7-d2b2-4e4a-a65f-299329fe4c51" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b1531e19-eeb1-4e65-bfe4-037ca8f10a3b", "a18935e1-95d7-4165-953d-69602f6c68dd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8729a0ba-809d-4744-b289-4890a7b48065");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1531e19-eeb1-4e65-bfe4-037ca8f10a3b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4623c0c7-d2b2-4e4a-a65f-299329fe4c51");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18935e1-95d7-4165-953d-69602f6c68dd");
        }
    }
}
