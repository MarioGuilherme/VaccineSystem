using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccineSystem.Data.Migrations {
    public partial class AddTablePersonsAddressesVaccines : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new {
                    id_person = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_persons", x => x.id_person);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new {
                    id_address = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    id_person = table.Column<int>(type: "int", nullable: false),
                    street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    complement = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    neighborhood = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    city = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    state = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Personid_person = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_addresses", x => x.id_address);
                    table.ForeignKey(
                        name: "FK_addresses_persons_Personid_person",
                        column: x => x.Personid_person,
                        principalTable: "persons",
                        principalColumn: "id_person"
                    );
                });

            migrationBuilder.CreateTable(
                name: "vaccines",
                columns: table => new {
                    id_vaccine = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    id_person = table.Column<int>(type: "int", nullable: false),
                    vaccine_name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    vaccine_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    vaccine_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Personid_person = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_vaccines", x => x.id_vaccine);
                    table.ForeignKey(
                        name: "FK_vaccines_persons_Personid_person",
                        column: x => x.Personid_person,
                        principalTable: "persons",
                        principalColumn: "id_person"
                    );
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_Personid_person",
                table: "addresses",
                column: "Personid_person"
            );

            migrationBuilder.CreateIndex(
                name: "IX_vaccines_Personid_person",
                table: "vaccines",
                column: "Personid_person"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(name: "addresses");

            migrationBuilder.DropTable(name: "vaccines");

            migrationBuilder.DropTable(name: "persons");
        }
    }
}