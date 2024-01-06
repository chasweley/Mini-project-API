using Microsoft.EntityFrameworkCore;
using Mini_project_API.Data;
using Mini_project_API.Models;
using Mini_project_API.Models.ViewModels;
using System;

namespace Mini_project_API.Handlers
{
    public static class PersonHandler
    {
        //Method to get all personsn from database by creating list view model array
        //To only display relevant info on each person
        public static IResult GetAllPersons(PersonInterestContext context)
        {
            PersonListViewModel[] result = context.Persons
                .Select(p => new PersonListViewModel()
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNo
                })
                .ToArray();

            //If-statement to return not found error message if there are no persons in database
            if (result == null)
            {
                return Results.NotFound(new { Message = "No persons in database" });
            }

            return Results.Json(result);
        }
    }
}
