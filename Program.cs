using Microsoft.EntityFrameworkCore;
using Mini_project_API.Data;

//using Mini_project_API.Data;
using Mini_project_API.Handlers;

namespace Mini_project_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("PersonInterestContext");
            builder.Services.AddDbContext<PersonInterestContext>(opt => opt.UseSqlServer(connectionString));
            var app = builder.Build();

            app.MapGet("/", () => "Welcome to persons and interests server!");

            app.MapGet("/persons", PersonHandler.GetAllPersons);
            app.MapGet("/persons/{personId}/links", LinkHandler.GetAllPersonLinks);
            app.MapGet("/persons/{personId}/interests", InterestHandler.GetPersonAllInterests);

            app.MapPost("/persons/{personId}/{interestId}/links", LinkHandler.AddPersonInterestLink);
            app.MapPost("/persons/{personId}/interests", InterestHandler.AddNewPersonInterest);


            app.Run();
        }
    }
}
