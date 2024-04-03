using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicSoftAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Blood_Type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Medications = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    FName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    National_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    National_Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Patient_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Operation__Patie__59063A47",
                        column: x => x.Patient_Id,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Day = table.Column<DateOnly>(type: "date", nullable: true),
                    FromTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    ToTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Reservation_No = table.Column<int>(type: "int", nullable: true),
                    Patient_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Reservati__Patie__5535A963",
                        column: x => x.Patient_Id,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK__Doctor__User_Id__4AB81AF0",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receptionist",
                columns: table => new
                {
                    RecepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_Shift_Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    End_Shift_Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    Start_Working_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    User_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptionist", x => x.RecepId);
                    table.ForeignKey(
                        name: "FK__Reception__User___47DBAE45",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Availability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<DateOnly>(type: "date", nullable: true),
                    Day = table.Column<DateOnly>(type: "date", unicode: false, maxLength: 20, nullable: true),
                    From_Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    To_Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    Doctor_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Availabil__Docto__5070F446",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Electricity = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Rent = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Tools = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Salaries = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Others = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Month = table.Column<DateOnly>(type: "date", nullable: true),
                    Doctor_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Expenses__Doctor__4D94879B",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availability_Doctor_Id",
                table: "Availability",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Doctor_Id",
                table: "Expenses",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_Patient_Id",
                table: "Operation",
                column: "Patient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receptionist_User_Id",
                table: "Receptionist",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Patient_Id",
                table: "Reservation",
                column: "Patient_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Availability");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Receptionist");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
