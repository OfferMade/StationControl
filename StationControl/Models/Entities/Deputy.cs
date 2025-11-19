namespace StationControl.Models.Entities
{
    public class Deputy
    {
        public int DeputyId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BadgeNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Rank { get; set; }



        public string UserId { get; set; }
        public int MailId { get; set; }


        public ICollection<Certificate> Certificates { get; set; }
        public ICollection<Mail> Mails { get; set; }
    }
}
