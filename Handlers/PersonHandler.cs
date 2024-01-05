using Microsoft.EntityFrameworkCore;
using Mini_project_API.Data;
using Mini_project_API.Models;
using Mini_project_API.Models.ViewModels;
using System;

namespace Mini_project_API.Handlers
{
    public static class PersonHandler
    {
        public static IResult GetAllPersons(PersonInterestContext context)
        {
            PersonListViewModel[] result = context.Persons
                .Select(p => new PersonListViewModel()
                {
                    Id = p.PersonId,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                })
                .ToArray();
            return Results.Json(result);
        }
    }
}
