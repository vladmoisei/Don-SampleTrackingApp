using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DonSampleTrackingApp.Migrations
{
    public partial class PrimaryKeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProbaModels",
                columns: table => new
                {
                    ProbaModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPrelevare = table.Column<string>(maxLength: 100, nullable: true),
                    SiglaFurnizor = table.Column<string>(nullable: true),
                    Sarja = table.Column<string>(nullable: true),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(nullable: true),
                    NumarCuptor = table.Column<int>(nullable: false),
                    TipTratamentTermic = table.Column<int>(nullable: false),
                    TipCapBara = table.Column<int>(nullable: false),
                    Tipproba = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    ObservatiiOperator = table.Column<string>(nullable: true),
                    DataPreluare = table.Column<string>(nullable: true),
                    DataRaspunsCalitate = table.Column<string>(nullable: true),
                    NumeUtilizatorCalitate = table.Column<string>(nullable: true),
                    RezultatProba = table.Column<int>(nullable: false),
                    KV1 = table.Column<int>(nullable: false),
                    KV2 = table.Column<int>(nullable: false),
                    KV3 = table.Column<int>(nullable: false),
                    Temperatura = table.Column<int>(nullable: false),
                    DuritateHB = table.Column<int>(nullable: false),
                    ObservatiiCalitate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProbaModels", x => x.ProbaModelId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: true),
                    Nume = table.Column<string>(maxLength: 100, nullable: true),
                    Prenume = table.Column<string>(maxLength: 100, nullable: true),
                    Rol = table.Column<int>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProbaModels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
