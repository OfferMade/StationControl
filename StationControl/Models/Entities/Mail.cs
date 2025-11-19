namespace StationControl.Models.Entities
{
    public class Mail
    {
        public int MailId { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }



        public int DeputyId { get; set; }
        Deputy Deputy { get; set; }
    }
}
