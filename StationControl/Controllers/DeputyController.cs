using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StationControl.Data;
using StationControl.Models.Entities;
using StationControl.Models.ViewModels;
using StationControl.Services.Interfaces;
using System.Security.Claims;

namespace StationControl.Controllers
{
    public class DeputyController : Controller
    {
        private readonly IDeputyRepository _context;
        private readonly StationControlDbContext _dbContext;
        private readonly IRepository<Deputy> _repository;

        public DeputyController(IDeputyRepository context, StationControlDbContext dbContext, IRepository<Deputy> reporsitory)
        {
            _context = context;
            _dbContext = dbContext;
            _repository = reporsitory;
        }

        [Authorize]
        [HttpGet("Index")]
        [Route("Deputy/Index")]
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(currentUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var deputyInfo = await _dbContext.Deputies.Include(y => y.Certificates).FirstOrDefaultAsync(x => x.UserId == currentUserId);
            if (deputyInfo != null)
            {
                return View(deputyInfo);
                
            }
            else
            {
                
                return View();
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("DeputyList")]
        [Route("Deputy/DeputyList")]
        public async Task<IActionResult> DeputyList()
        {
            var deputies = _context.GetAllDeputiesWithCertificates();
            return View(deputies);
        }

        public async Task<IActionResult> Deputies()
        {
            var deputies = _context.GetAllDeputiesWithCertificates();
            return View(deputies);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Deputy/DeputyDetails/{id}")]
        public async Task<IActionResult> DeputyDetails(string id)
        {

            var deputy = await _dbContext.Deputies.FirstOrDefaultAsync(x => x.UserId == id);
            DeputyViewModel viewModel = new DeputyViewModel
            {
                BadgeNumber = deputy.BadgeNumber,
                DateOfBirth = deputy.DateOfBirth,
                FullName = deputy.FullName,
                EmailAddress = deputy.EmailAddress,
                UserId = deputy.UserId,
                Rank = deputy.Rank
            };
            if (viewModel == null)
            {
                return RedirectToAction("DeputyList");
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Deputy/SaveDeputyDetails")]
        public async Task<IActionResult> SaveDeputyDetails(DeputyViewModel viewModel)
        {
            var oldDeputy = await _dbContext.Deputies.FirstOrDefaultAsync(x => x.UserId == viewModel.UserId);
            oldDeputy.BadgeNumber = viewModel.BadgeNumber;
            oldDeputy.DateOfBirth = viewModel.DateOfBirth;
            oldDeputy.FullName = viewModel.FullName;
            oldDeputy.EmailAddress = viewModel.EmailAddress;
            oldDeputy.Rank = viewModel.Rank;
            oldDeputy.UserId = viewModel.UserId;
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(oldDeputy);
                return RedirectToAction("DeputyList");

            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Deputy/AddDeputy")]
        public async Task<IActionResult> AddDeputy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDeputyPost(DeputyViewModel viewModel)
        {
            
            Deputy deputy = new Deputy
            {
                BadgeNumber = viewModel.BadgeNumber,
                DateOfBirth = viewModel.DateOfBirth,
                FullName = viewModel.FullName,
                EmailAddress = viewModel.EmailAddress,
                UserId = viewModel.UserId,
                Rank = viewModel.Rank,

            };
            if (deputy != null)
            {
                await _repository.AddAsync(deputy);
                return RedirectToAction("DeputyList");
            }
            return View("AddDeputy");
        }
    }
}
