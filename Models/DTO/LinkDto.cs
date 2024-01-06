namespace Mini_project_API.Models.DTO
{
    //DTO to only require certain variables when creating new link in database
    //since the database itself creates the id
    public class LinkDto
    {
        public string LinkToPage { get; set; }
    }
}
