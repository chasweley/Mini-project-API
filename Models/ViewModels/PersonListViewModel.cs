namespace Mini_project_API.Models.ViewModels
{
    //View model to only show selected info in database on all the persons in list
    public class PersonListViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
