using JopSy.Interface;
using JopSy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using JopSy.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace JopSy.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public JobController(IJobRepository jobRepository , IHttpContextAccessor httpContextAccessor)
        {
            _jobRepository = jobRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _jobRepository.GetAll();
            return View(jobs);
        }

        public IActionResult Create()
        {
            var curUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createJobViewModel = new CreateJobViewModel
            {
                UserId = curUserId
            };

            return View(createJobViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobViewModel jobVM)
        {
            if (!ModelState.IsValid)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                var job = new Job
                {
                    Title = jobVM.Title,
                    Description = jobVM.Description,
                    ContractType = jobVM.ContractType,
                    WorkMode = jobVM.WorkMode,
                    PostedDate = jobVM.PostedDate,
                    UserId = currentUserId,
                    AddressId = jobVM.AddressId,
             
                    Address = new Address
                    {
                        City = jobVM.Address.City,
                        Area = jobVM.Address.Area,
                        Street = jobVM.Address.Street,
                    }
                };
                _jobRepository.Add(job);
                return RedirectToAction("Index" , "Home");
            }
            else
            {
                ModelState.AddModelError("", " faileddd");


            }
            return View(jobVM);

        }











    }
}