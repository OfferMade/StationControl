using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StationControl.Data;
using StationControl.Models.Entities;
using StationControl.Models.ViewModels;
using StationControl.Services.Interfaces;
using System.Threading.Tasks;

namespace StationControl.Services.Repositories
{
    public class DeputyRepository : IDeputyRepository
    {
        private readonly StationControlDbContext _context;
        private readonly StationControlContext _roleContext;

        public DeputyRepository(StationControlDbContext context, StationControlContext roleContext)
        {
            _context = context;
            _roleContext = roleContext;
        }

        public List<Deputy> GetAllDeputiesWithCertificates()
        {
            return _context.Deputies.Include(x => x.Certificates).ToList();
        }



        public async Task<Deputy> GetDeputyWithCertificates(string id)
        {
            return await _context.Deputies.Include(x=>x.Certificates).FirstOrDefaultAsync(d=>d.UserId == id);
        }

       

        public async Task<int> SendMail(MailViewModel mailViewModel)
        {
            var entity = new Mail
            {
                Email = mailViewModel.Email,
                Title = mailViewModel.Title,
                Message = mailViewModel.Message,
                DeputyId = _context.Deputies.Where(x => x.EmailAddress == mailViewModel.Email).Select(x => x.DeputyId).FirstOrDefault() //ilk önce tüm tabloyu al, sonra filtreler where ile, sonra select ile istediğin property'i seç.

            };
            await _context.Mails.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.MailId;
        }

        
    }
}
