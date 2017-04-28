using System;

namespace AskPam.Models.Contacts
{
    public class Contact 
    {
        public int Id { get; set; }

        public string Gender { get; set; }

        public int? GenderValue { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string EmailAddress { get; set; }
        public string EmailAddress2 { get; set; }
        public string EmailAddress3 { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }

        public int? MaritalStatusValue { get; set; }
        public string PrimaryLanguage { get; set; }
        public string SecondaryLanguage { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Bio { get; set; }

        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string BusinessPhoneNumber { get; set; }

        public int? GroupId { get; set; }
        public string GroupName { get; set; }

        public int? ConversationId { get; set; }
    }
}
