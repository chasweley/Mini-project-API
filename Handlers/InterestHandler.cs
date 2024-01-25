using Microsoft.EntityFrameworkCore;
using Mini_project_API.Data;
using Mini_project_API.Models;
using Mini_project_API.Models.DTO;
using Mini_project_API.Models.ViewModels;
using Mini_project_API.Helpers;
using System.Net;

namespace Mini_project_API.Handlers
{
    public static class InterestHandler
    {
        //Method to get all of a specific person's interests from database by creating view model array
        //To only display relevant info on each interest
        public static IResult GetPersonAllInterests(PersonInterestContext context, int personId)
        {
            InterestViewModel[] result = context.PersonsInterests
                .Include(i => i.InterestIdFkNavigation)
                .Where(p => p.PersonIdFk == personId)
                .Select(p => new InterestViewModel()
                {
                    Title = p.InterestIdFkNavigation.Title,
                    Description = p.InterestIdFkNavigation.Description
                })
                .ToArray();

            //If-statement to return not found error message if the person
            //has no interests connected to them
            if (result == null)
            {
                return Results.NotFound(new { Message = "No such person with interests" });
            }

            return Results.Json(result);
        }

        //Method to add/connect interest to specific person to database
        public static IResult AddNewPersonInterest(PersonInterestContext context, int personId, InterestDto interest)
        {
            //If the request body is missing title and/or description return error message
            if (string.IsNullOrEmpty(interest.Title) || string.IsNullOrEmpty(interest.Description))
            {
                return Results.BadRequest(new { Message = "Interests needs to have both title and description" });
            }

            //If the link does not already exist in database in table Interests, first add it to database
            if (!context.Interests.Any(i => i.Title.Equals(interest.Title)))
            {
                DbHelpers.AddInterestToDb(context, interest);
            }

            int interestId = DbHelpers.GetInterestId(context, interest);

            //If-statment to check if the person already has that interest connected to them
            //before saving the connection to database
            if (!context.PersonsInterests.Any(p => p.PersonIdFk.Equals(personId) && p.InterestIdFk.Equals(interestId)))
            {
                context.PersonsInterests.Add(new PersonsInterest()
                {
                    PersonIdFk = personId,
                    InterestIdFk = interestId
                });
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            //If person already have that interest saved to them in database, conflict message will be shown
            else
            {
                return Results.Conflict("That person already has that interest.");
            }            
        }
    }
}
