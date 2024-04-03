
using ClinicSoftAPI.IRepositories;
using ClinicSoftAPI.Models;
using ClinicSoftAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoftAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BookingClinicsContext>(Use =>
                    Use.UseSqlServer(builder.Configuration.GetConnectionString("BaseConnection")));
            builder.Services.AddAutoMapper(typeof(Program));
            #region Register Services
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IAvailabilityRepository,  AvailabilityRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
            builder.Services.AddScoped<IOperationRepository, OperationRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            #endregion
            builder.Services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("Default", PolicyBuilder =>
                {
                    PolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });

            });

            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("Default");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}