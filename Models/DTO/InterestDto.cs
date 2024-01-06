namespace Mini_project_API.Models.DTO
{
    //DTO to only require certain variables when creating new interest in database
    //since the database itself creates the id
    public class InterestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
