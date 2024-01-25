using Mini_project_API.Data;
using Mini_project_API.Models;
using Mini_project_API.Models.DTO;

namespace Mini_project_API.Helpers
{
    public class DbHelpers
    {
        //Method to get the interestId from database
        public static int GetInterestId(PersonInterestContext context, InterestDto interest)
        {
            int interestId = context.Interests
                .Where(i => i.Title == interest.Title)
                .Select(ii => ii.InterestId)
                .SingleOrDefault();
            return interestId;
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

        //Method to find id for specific person's specific interest
        public static int FindPersonInterestId(PersonInterestContext context, int personId, int interestId)
        {
            int personInterestId = context.PersonsInterests
                .Where(p => p.PersonIdFk == personId && p.InterestIdFk == interestId)
                .Select(pi => pi.PersonInterestId)
                .SingleOrDefault();

            return personInterestId;
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
    }
}
