namespace AthenaApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CompanyId { get; set; }
        public string SecurityStamp { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
