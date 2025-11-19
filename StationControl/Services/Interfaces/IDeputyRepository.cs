using StationControl.Models.Entities;
using StationControl.Models.ViewModels;

namespace StationControl.Services.Interfaces
{
    public interface IDeputyRepository
    {
        Task<Deputy> GetDeputyWithCertificates(string id); 
        List<Deputy> GetAllDeputiesWithCertificates();
        Task<int> SendMail(MailViewModel mailViewModel);

        

        
        
    }
}
