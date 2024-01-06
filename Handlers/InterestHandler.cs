using Microsoft.EntityFrameworkCore;
using Mini_project_API.Data;
using Mini_project_API.Models;
using Mini_project_API.Models.DTO;
using Mini_project_API.Models.ViewModels;
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

        //Method to find id for specific person's specific interest
        public static int FindPersonInterestId(PersonInterestContext context, int personId, int interestId)
        {
            int personInterestId = context.PersonsInterests
                .Where(p => p.PersonIdFk == personId && p.InterestIdFk == interestId)
                .Select(pi => pi.PersonInterestId)
                .SingleOrDefault();

            return personInterestId;
        }

        //Method to add new interest to table Interests in database with help
        //of a dto to not have to send id to database, which it creates itself
        public static void AddInterestToDb(PersonInterestContext context, InterestDto interest) 
        {
            context.Interests.Add(new Interest()
            {
                Title = interest.Title,
                Description = interest.Description
            });
            context.SaveChanges();
        }

        //Method to get the interestId from database
        public static int GetInterestId(PersonInterestContext context, InterestDto interest)
        {
            int interestId = context.Interests
                .Where(i => i.Title == interest.Title)
                .Select(ii => ii.InterestId)
                .SingleOrDefault();
            return interestId;
        }

        //Method to add/connect interest to specific person to database
        public static IResult AddNewPersonInterest(PersonInterestContext context, int personId, InterestDto interest)
        {
            //If the link does not already exist in database in table Interests, first add it to database
            if (!context.Interests.Any(i => i.Title.Equals(interest.Title)))
            {
                AddInterestToDb(context, interest);
            }

            int interestId = GetInterestId(context, interest);

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
