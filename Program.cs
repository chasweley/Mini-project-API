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
            
            //Create the connection to the database for the API
            string connectionString = builder.Configuration.GetConnectionString("PersonInterestContext");
            builder.Services.AddDbContext<PersonInterestContext>(opt => opt.UseSqlServer(connectionString));
            var app = builder.Build();

            app.MapGet("/", () => "Welcome to the persons and interests server!");

            //Get all persons in system
            app.MapGet("/persons", PersonHandler.GetAllPersons);

            //Get all links connected to a specific person
            app.MapGet("/persons/{personId}/links", LinkHandler.GetAllPersonLinks);

            //Create new link connected to specfic person and specific interest with a POST request
            app.MapPost("/persons/{personId}/{interestId}/links", LinkHandler.AddPersonInterestLink);

            //Get all links connected to a specific person
            app.MapGet("/persons/{personId}/interests", InterestHandler.GetPersonAllInterests);

            //Create new interest connected to a specfic person with a POST request
            app.MapPost("/persons/{personId}/interests", InterestHandler.AddNewPersonInterest);

            app.Run();
        }
    }
}
