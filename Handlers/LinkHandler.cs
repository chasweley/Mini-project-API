using Microsoft.EntityFrameworkCore;
using Mini_project_API.Data;
using Mini_project_API.Models;
using Mini_project_API.Models.DTO;
using Mini_project_API.Models.ViewModels;
using System.Net;
using System.Reflection;

namespace Mini_project_API.Handlers
{
    public static class LinkHandler
    {
        //Method to get all of a specific person's links from database by creating view model array
        //To only display relevant info on each link
        public static IResult GetAllPersonLinks(PersonInterestContext context, int personId)
        {
            LinkViewModel[] result = context.PersonsInterestsLinks
                .Include(pi => pi.PersonsInterestsIdFkNavigation)
                .Include(l => l.LinkIdFkNavigation)
                .Where(p => p.PersonsInterestsIdFkNavigation.PersonIdFk == personId)
                .Select(l => new LinkViewModel
                {
                    LinkToPage = l.LinkIdFkNavigation.LinkToPage
                }).ToArray();

            //If-statement to return not found error message if the person
            //has no links connected to them and their interests
            if (result == null)
            {
                return Results.NotFound(new { Message = "No such person with links to interests" });
            }

            return Results.Json(result);
        }

        //Method to add new link to table Links in database with help
        //of a dto to not have to send id to database, which it creates itself
        public static void AddLinkToDatabase(PersonInterestContext context, LinkDto linkToPage)
        { 
            context.Links.Add(new Link()
            {
                LinkToPage = linkToPage.LinkToPage
            });
            context.SaveChanges();
        }

        //Method to get the linkId from database
        public static int GetLinkId(PersonInterestContext context, LinkDto linkToPage)
        {
            var linkId = context.Links
                .Where(l => l.LinkToPage == linkToPage.LinkToPage)
                .Select(li => li.LinkId)
                .SingleOrDefault();
            return linkId;
        }

        //Method to add/connect link to specific person and specific interest to database
        public static IResult AddPersonInterestLink(PersonInterestContext context, int personId, int interestId, LinkDto linkToPage)
        {
            //If the request body is missing linkToPage return error message
            if (string.IsNullOrEmpty(linkToPage.LinkToPage))
            {
                return Results.BadRequest(new { Message = "Links needs to have a linkToPage" });
            }

            //If the link does not already exist in database in table Links, first add it to database
            if (!context.Links.Any(l => l.LinkToPage.Equals(linkToPage.LinkToPage)))
            {
                AddLinkToDatabase(context, linkToPage);
            }

            int personInterestId = InterestHandler.FindPersonInterestId(context, personId, interestId);
            int linkId = GetLinkId(context, linkToPage);

            //If-statment to check if the person already has that link connected to them and the interest
            //before saving the connection to database
            if (!context.PersonsInterestsLinks.Any(p => p.PersonsInterestsIdFk.Equals(personInterestId) && p.LinkIdFk.Equals(linkId)))
            {
                context.PersonsInterestsLinks.Add(new PersonsInterestsLink()
                {
                    PersonsInterestsIdFk = personInterestId,
                    LinkIdFk = linkId
                });
                context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            //If person already have that link saved to them in database, conflict message will be shown
            else
            {
                return Results.Conflict("That person already has that link for that interest.");
            }
        }
    }
}
