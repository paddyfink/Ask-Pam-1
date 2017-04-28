namespace AskPam.Models.Contacts
{
    public class ContactList 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
        public string MobilePhoneNumber { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
