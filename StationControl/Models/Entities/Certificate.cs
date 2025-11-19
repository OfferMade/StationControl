namespace StationControl.Models.Entities
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string Name { get; set; }



        public int DeputyId { get; set; }
        public Deputy Deputy { get; set; }
    }
}
