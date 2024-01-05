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
        public static IResult GetPersonAllInterests(PersonInterestContext context, int personId)
        {
            var result = context.PersonsInterests
                .Include(i => i.InterestIdFkNavigation)
                .Where(p => p.PersonIdFk == personId)
                .Select(p => new InterestViewModel()
                {
                    Title = p.InterestIdFkNavigation.Title
                })
                .ToArray();

            if (result == null)
            {
                return Results.NotFound(new { Message = "No such person" });
            }

            return Results.Json(result);
        }

        public static int FindPersonInterestId(PersonInterestContext context, int personId, int interestId)
        {
            var personInterestId = context.PersonsInterests
                .Where(p => p.PersonIdFk == personId && p.InterestIdFk == interestId)
                .Select(pi => pi.PersonInterestId)
                .SingleOrDefault();

            return personInterestId;
        }

        public static void AddInterestToDb(PersonInterestContext context, InterestDto interest) 
        {
            context.Interests.Add(new Interest()
            {
                Title = interest.Title,
                Description = interest.Description
            });
            context.SaveChanges();
        }

        public static int GetInterestId(PersonInterestContext context, InterestDto interest)
        {
            var interestId = context.Interests
                .Where(i => i.Title == interest.Title)
                .Select(ii => ii.InterestId)
                .SingleOrDefault();
            return interestId;
        }

        public static IResult AddNewPersonInterest(PersonInterestContext context, int personId, InterestDto interest)
        {
            //if (string.IsNullOrEmpty(interest.Title))
            //{
            //    return
            //}
            if (!context.Interests.Any(i => i.Title.Equals(interest.Title)))
            {
                AddInterestToDb(context, interest);
            }

            int interestId = GetInterestId(context, interest);
            
            if(!context.PersonsInterests.Any(p => p.PersonIdFk.Equals(personId) && p.InterestIdFk.Equals(interestId)))
            {
                context.PersonsInterests.Add(new PersonsInterest()
                {
                    PersonIdFk = personId,
                    InterestIdFk = interestId
                });
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            else
            {
                return Results.Conflict("That person already has that interest.");
            }            
        }
    }
}
