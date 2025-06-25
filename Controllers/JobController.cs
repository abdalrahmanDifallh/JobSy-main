using JopSy.Interface;
using JopSy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using JopSy.ViewModel;

namespace JopSy.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;



        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;


        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _jobRepository.GetAll();
            return View(jobs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobViewModel jobVM)
        {
            if (ModelState.IsValid)
            {
                
                var job = new Job
                {
                    Title = jobVM.Title,
                    Description = jobVM.Description,
                    ContractType = jobVM.ContractType,
                    WorkMode = jobVM.WorkMode,
                    PostedDate = jobVM.PostedDate,
                    UserId = jobVM.UserId,
                    AddressId = jobVM.AddressId,
             
                    Address = new Address
                    {
                        City = jobVM.Address.City,
                        Area = jobVM.Address.Area,
                        Street = jobVM.Address.Street,
                    }
                };
                _jobRepository.Add(job);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", " failed");


            }
            return View(jobVM);

        }











    }
}