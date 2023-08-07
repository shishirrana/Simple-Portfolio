using com.portfolio.website.Controllers.VM;
using com.portfolio.website.Data;
using com.portfolio.website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace com.portfolio.website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly comportfoliowebsiteContext _context;

        public HomeController(comportfoliowebsiteContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomePageVM vm = new HomePageVM();

            try
            {
                vm.PersonalInformation = new PersonalInformation();
                vm.PersonalInformation = _context.PersonalInformation.FirstOrDefault();
                vm.MyExpertises = _context.MyExpertise.ToList();
                vm.Educations = _context.Education.ToList();
                vm.Skills = _context.Skill.ToList();
                vm.Languages = _context.Language.ToList();

            }

            catch (Exception ex) 
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
            
            return View(vm);
        }

        [Authorize(Roles = "user")]
        public IActionResult Privacy()
        {
            return View();
        }

        //AceesDenied Page

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}