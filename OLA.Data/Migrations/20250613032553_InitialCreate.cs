using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OLA.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesses", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Addresses",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        City = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Purok = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Active = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Addresses", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Addresses_AspNetUsers_AppUserId",
            //            column: x => x.AppUserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserLogins",
            //    columns: table => new
            //    {
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserTokens",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LoanApplication",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        AmountRequested = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        TermsInDays = table.Column<int>(type: "int", nullable: false),
            //        InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ApplicationStatus = table.Column<int>(type: "int", nullable: false),
            //        ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DisbursementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Active = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LoanApplication", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LoanApplication_AspNetUsers_AppUserId",
            //            column: x => x.AppUserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Loans",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        LoanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        DisbursementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        PrincipalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        TermsInDays = table.Column<int>(type: "int", nullable: false),
            //        LoanStatus = table.Column<int>(type: "int", nullable: false),
            //        DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Active = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Loans", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Loans_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Access = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            //migrationBuilder.CreateTable(
            //    name: "Families",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RelationType = table.Column<int>(type: "int", nullable: false),
            //        AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        DateOfBrith = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsEmergencyContact = table.Column<bool>(type: "bit", nullable: false),
            //        AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Active = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Families", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Families_Addresses_AddressId",
            //            column: x => x.AddressId,
            //            principalTable: "Addresses",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Families_AspNetUsers_AppUserId",
            //            column: x => x.AppUserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Payments",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        LoanApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        LoanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        PaymentMethod = table.Column<int>(type: "int", nullable: false),
            //        AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Active = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Payments", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Payments_AspNetUsers_AppUserId",
            //            column: x => x.AppUserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Payments_LoanApplication_LoanApplicationId",
            //            column: x => x.LoanApplicationId,
            //            principalTable: "LoanApplication",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Payments_Loans_LoanId",
            //            column: x => x.LoanId,
            //            principalTable: "Loans",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.InsertData(
                table: "Accesses",
                columns: new[] { "Id", "Active", "Modified", "ModifiedBy", "Module", "Name", "Path", "Roles" },
                values: new object[,]
                {
                    { new Guid("1f982648-2cf0-4ef7-9c96-7f17fd884ecc"), true, new DateTime(2025, 6, 13, 11, 25, 52, 832, DateTimeKind.Local).AddTicks(5600), new Guid("516eff2a-1cfa-4e67-a1a0-4d21b2a2545f"), "Loan", "Loan Application", "LoanApplication", "Admin,User" },
                    { new Guid("393d717c-eeae-485f-9eee-d3b259761ed6"), true, new DateTime(2025, 6, 13, 11, 25, 52, 832, DateTimeKind.Local).AddTicks(5500), new Guid("e3cd97bb-2cdb-4be9-869b-1d70e48fa27d"), "Security", "User", "User", "Admin" },
                    { new Guid("51f313ef-2090-47fb-9d5b-486f5ba7ad5c"), true, new DateTime(2025, 6, 13, 11, 25, 52, 832, DateTimeKind.Local).AddTicks(5570), new Guid("156c06e4-fe3e-4d91-a278-b79061d00e97"), "Security", "Permission", "Permission", "Admin" },
                    { new Guid("5889ed06-3f22-4198-bb0d-c80e3fbbc394"), true, new DateTime(2025, 6, 13, 11, 25, 52, 832, DateTimeKind.Local).AddTicks(5560), new Guid("00fd30eb-8291-4f2c-ae8a-4d07694b24f3"), "Security", "Access", "Access", "Admin" },
                    { new Guid("78e8c0d6-1ba4-4c88-985a-decceb56c7ff"), true, new DateTime(2025, 6, 13, 11, 25, 52, 832, DateTimeKind.Local).AddTicks(5590), new Guid("374c5124-d398-4533-9a55-589cacb7d86f"), "Customer", "Customer Enrollment", "Enrollment", "Admin,User" },
                    { new Guid("8db61816-a397-490c-b714-cbbf16432b01"), true, new DateTime(2025, 6, 13, 11, 25, 52, 832, DateTimeKind.Local).AddTicks(5550), new Guid("f635761b-3f92-4cf8-b7e5-8962b77646c4"), "Security", "Role", "Role", "Admin" },
                    { new Guid("ea60edc9-b785-41f4-9182-e75342ea57d6"), true, new DateTime(2025, 6, 13, 11, 25, 52, 832, DateTimeKind.Local).AddTicks(5610), new Guid("48a0281a-7a31-4779-8e0f-c5072dbedb58"), "Loan", "Loan", "Loan", "Admin,User" }
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Addresses_AppUserId",
            //    table: "Addresses",
            //    column: "AppUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetRoleClaims_RoleId",
            //    table: "AspNetRoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "RoleNameIndex",
            //    table: "AspNetRoles",
            //    column: "NormalizedName",
            //    unique: true,
            //    filter: "[NormalizedName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId",
            //    table: "AspNetUserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Families_AddressId",
            //    table: "Families",
            //    column: "AddressId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Families_AppUserId",
            //    table: "Families",
            //    column: "AppUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LoanApplication_AppUserId",
            //    table: "LoanApplication",
            //    column: "AppUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Loans_UserId",
            //    table: "Loans",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Payments_AppUserId",
            //    table: "Payments",
            //    column: "AppUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Payments_LoanApplicationId",
            //    table: "Payments",
            //    column: "LoanApplicationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Payments_LoanId",
            //    table: "Payments",
            //    column: "LoanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Permissions_AppUserId",
            //    table: "Permissions",
            //    column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "LoanApplication");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
