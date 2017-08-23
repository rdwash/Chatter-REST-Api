namespace ChatterApi.DTO
{
    public class User
    {
        public User()
        {
            Attributes = new UserAttributes();
            Links = new UserLinks();
        }
        public int Id { get; set; }
        public string Type { get; set; }
        public UserAttributes Attributes { get; set; }
        public UserLinks Links { get; set; }
    }

    public class UserAttributes
    {
        public UserAttributes()
        {

        }
        public string Username { get; set; }
    }

    public class UserLinks
    {
        public UserLinks()
        {

        }
        public string Self { get; set; }
    }
}
