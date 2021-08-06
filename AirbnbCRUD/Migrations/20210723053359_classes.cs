using Microsoft.EntityFrameworkCore.Migrations;

namespace AirbnbCRUD.Migrations
{
    public partial class classes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonEmailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonBirthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HousePrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.HouseId);
                    table.ForeignKey(
                        name: "FK_Houses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonFeedbacks",
                columns: table => new
                {
                    PersonFeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonFeedbackBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonFeedbackStars = table.Column<int>(type: "int", nullable: false),
                    PersonFeedbackDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonHostId = table.Column<int>(type: "int", nullable: false),
                    PersonCustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonFeedbacks", x => x.PersonFeedbackId);
                    table.ForeignKey(
                        name: "FK_PersonFeedbacks_People_PersonCustomerId",
                        column: x => x.PersonCustomerId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonFeedbacks_People_PersonHostId",
                        column: x => x.PersonHostId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    StartBookingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndBookingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatingBookingDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bookings_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HouseFeedbacks",
                columns: table => new
                {
                    HouseFeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseFeedbackBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseFeedbackStars = table.Column<int>(type: "int", nullable: false),
                    HouseFeedbackDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseFeedbacks", x => x.HouseFeedbackId);
                    table.ForeignKey(
                        name: "FK_HouseFeedbacks_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HouseFeedbacks_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HousePhotos",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    HousePhotos = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HousePhotos", x => new { x.HouseId, x.HousePhotos });
                    table.ForeignKey(
                        name: "FK_HousePhotos_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HouseId",
                table: "Bookings",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PersonId",
                table: "Bookings",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseFeedbacks_HouseId",
                table: "HouseFeedbacks",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseFeedbacks_PersonId",
                table: "HouseFeedbacks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_PersonId",
                table: "Houses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonFeedbacks_PersonCustomerId",
                table: "PersonFeedbacks",
                column: "PersonCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonFeedbacks_PersonHostId",
                table: "PersonFeedbacks",
                column: "PersonHostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "HouseFeedbacks");

            migrationBuilder.DropTable(
                name: "HousePhotos");

            migrationBuilder.DropTable(
                name: "PersonFeedbacks");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
