using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StationControl.Data;
using StationControl.Models.Entities;
using StationControl.Models.ViewModels;
using StationControl.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StationControl.Controllers
{

    public class MailController : Controller
    {
        private readonly IDeputyRepository _deputyRepository;
        private readonly IRepository<Mail> _repository;
        private readonly StationControlDbContext _context;

        public MailController(IDeputyRepository deputyRepository, IRepository<Mail> repository, StationControlDbContext context)
        {
            _deputyRepository = deputyRepository;
            _repository = repository;
            _context = context;
        }

        [Authorize(Roles ="User,Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var checkDeputy = _context.Deputies.FirstOrDefault(x => x.UserId == currentUserId);
            if (checkDeputy == null)
            {
                Response.WriteAsync("<script>alert('Deputy bilginiz bulunamadı. Yöneticiyle iletişime geçiniz.')</script>");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> SendMail(MailViewModel mailViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(mailViewModel);
            }
            await _deputyRepository.SendMail(mailViewModel);
            return RedirectToAction("Index", "Mail");
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllMails()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var checkDeputy = _context.Deputies.FirstOrDefault(x => x.UserId == currentUserId);
            if (checkDeputy == null)
            {
                
                Response.WriteAsync("<script>alert('Deputy bilginiz bulunamadi. Yoneticiyle iletisime geciniz.')</script>");
                return RedirectToAction("Index","Home");
            }
            var deputyMailFromID = _context.Deputies.Include(x => x.Mails).FirstOrDefault(x => x.UserId == currentUserId);
            if(deputyMailFromID != null)
            {
                return View(deputyMailFromID);
            }
            else
            {
                ViewBag.Error = "No mails found for the deputy.";
                return View();
            }
        }

    }
}
