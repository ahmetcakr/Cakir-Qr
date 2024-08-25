using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QrMenu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class qrcodesupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MenuQrCodes_MenuId",
                table: "MenuQrCodes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 72, 255, 165, 3, 253, 167, 45, 255, 87, 43, 24, 82, 156, 235, 135, 153, 175, 10, 139, 106, 52, 224, 188, 16, 49, 212, 155, 157, 237, 176, 254, 202, 191, 125, 21, 96, 2, 137, 46, 128, 98, 165, 55, 247, 106, 81, 14, 210, 156, 97, 60, 11, 6, 254, 179, 241, 31, 28, 146, 24, 190, 194, 211, 47 }, new byte[] { 93, 137, 169, 178, 185, 109, 153, 241, 155, 4, 148, 117, 251, 170, 84, 148, 110, 170, 179, 199, 168, 139, 0, 146, 168, 238, 199, 203, 238, 208, 49, 153, 47, 241, 97, 72, 138, 201, 61, 190, 68, 253, 206, 114, 167, 224, 198, 209, 117, 92, 144, 11, 106, 72, 201, 15, 2, 191, 214, 95, 218, 66, 142, 72, 214, 73, 145, 233, 188, 185, 156, 21, 114, 204, 78, 111, 216, 71, 204, 239, 115, 116, 145, 22, 186, 184, 216, 254, 244, 193, 218, 118, 84, 47, 146, 48, 5, 112, 46, 76, 30, 86, 76, 223, 119, 142, 147, 246, 175, 13, 76, 124, 101, 60, 148, 55, 121, 206, 120, 188, 197, 194, 79, 208, 252, 234, 156, 103 } });

            migrationBuilder.CreateIndex(
                name: "IX_MenuQrCodes_MenuId",
                table: "MenuQrCodes",
                column: "MenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MenuQrCodes_MenuId",
                table: "MenuQrCodes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 56, 207, 182, 132, 120, 179, 57, 171, 111, 104, 97, 84, 49, 8, 171, 175, 39, 173, 12, 46, 83, 47, 25, 59, 49, 165, 11, 113, 245, 101, 160, 60, 122, 46, 218, 79, 54, 206, 115, 202, 121, 100, 151, 165, 112, 124, 153, 113, 56, 139, 197, 191, 10, 10, 210, 175, 172, 253, 62, 244, 181, 30, 60, 2 }, new byte[] { 165, 228, 140, 197, 62, 8, 140, 192, 56, 147, 159, 107, 195, 28, 11, 171, 78, 143, 135, 206, 133, 35, 40, 77, 82, 237, 161, 71, 120, 247, 35, 110, 75, 60, 113, 1, 114, 54, 157, 45, 27, 55, 214, 110, 225, 153, 160, 123, 50, 202, 126, 233, 18, 117, 65, 195, 21, 156, 253, 35, 208, 47, 187, 137, 160, 51, 84, 247, 205, 143, 236, 75, 147, 88, 217, 65, 93, 152, 230, 54, 74, 75, 102, 237, 64, 177, 53, 67, 97, 132, 20, 23, 66, 10, 147, 124, 251, 194, 181, 175, 29, 86, 239, 163, 37, 132, 226, 103, 184, 112, 44, 29, 73, 200, 165, 161, 178, 219, 186, 26, 200, 248, 114, 37, 113, 241, 118, 247 } });

            migrationBuilder.CreateIndex(
                name: "IX_MenuQrCodes_MenuId",
                table: "MenuQrCodes",
                column: "MenuId",
                unique: true);
        }
    }
}
