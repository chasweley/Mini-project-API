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
        public static IResult GetAllPersonLinks(PersonInterestContext context, int personId)
        {
            var result = context.PersonsInterestsLinks
                .Include(pi => pi.PersonsInterestsIdFkNavigation)
                .Include(l => l.LinkIdFkNavigation)
                .Where(p => p.PersonsInterestsIdFkNavigation.PersonIdFk == personId)
                .Select(l => new LinkViewModel
                {
                    Link = l.LinkIdFkNavigation.LinkToPage
                }).ToArray();

            if (result == null)
            {
                return Results.NotFound(new { Message = "No such person with interests" });
            }

            return Results.Json(result);
        }

        public static void AddLinkToDatabase(PersonInterestContext context, LinkDto linkToPage)
        { 
            context.Links.Add(new Link()
            {
                LinkToPage = linkToPage.LinkToPage
            });
            context.SaveChanges();
        }

        public static int GetLinkId(PersonInterestContext context, LinkDto linkToPage)
        {
            var linkId = context.Links
                .Where(l => l.LinkToPage == linkToPage.LinkToPage)
                .Select(li => li.LinkId)
                .SingleOrDefault();
            return linkId;
        }

        public static IResult AddPersonInterestLink(PersonInterestContext context, int personId, int interestId, LinkDto linkToPage)
        {
            //if (string.IsNullOrEmpty(linkToPage.LinkToPage))
            //{
            //    return
            //}
            if (!context.Links.Any(l => l.LinkToPage.Equals(linkToPage.LinkToPage)))
            {
                AddLinkToDatabase(context, linkToPage);
            }

            int personInterestId = InterestHandler.FindPersonInterestId(context, personId, interestId);
            int linkId = GetLinkId(context, linkToPage);

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
            else
            {
                return Results.Conflict("That person already has that link for that interest.");
            }
        }
    }
}
