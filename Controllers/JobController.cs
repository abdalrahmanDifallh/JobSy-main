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

    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public JobController(IJobRepository jobRepository, IHttpContextAccessor httpContextAccessor)
        {
            _jobRepository = jobRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        
        public async Task<IActionResult> AllJobs()
        {
            var jobs = await _jobRepository.GetAll();
            var jobViewModels = jobs.Select(job => new CreateJobViewModel
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                ContractType = job.ContractType,
                WorkMode = job.WorkMode,
                PostedDate = job.PostedDate,
                UserId = job.UserId,
                AddressId = job.AddressId,
                Address = job.Address != null ? new Address
                {
                    City = job.Address.City,
                    Area = job.Address.Area,
                    Street = job.Address.Street
                } : null
            }).ToList();

            return View("AllJobs", jobViewModels); // استخدام Index.cshtml لعرض جميع الوظائف
        }

        [Authorize] // التأكد من أن المستخدم مسجل دخوله
        public async Task<IActionResult> Index()
        {
            // الحصول على UserId للمستخدم المسجل دخوله
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            // جلب الوظائف التي تتطابق مع UserId
            var jobs = await _jobRepository.GetByUserIdAsync(userId);
            var jobViewModels = jobs.Select(job => new CreateJobViewModel
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                ContractType = job.ContractType,
                WorkMode = job.WorkMode,
                PostedDate = job.PostedDate,
                UserId = job.UserId,
                AddressId = job.AddressId,
                Address = job.Address != null ? new Address
                {
                    City = job.Address.City,
                    Area = job.Address.Area,
                    Street = job.Address.Street
                } : null
            }).ToList();

            // إذا لم يكن هناك وظائف، عرض رسالة في الـ View
            if (!jobViewModels.Any())
            {
                ViewData["Message"] = "No jobs found for the current user.";
            }

            return View(jobViewModels); // استخدام Index.cshtml لعرض القائمة
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
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", " faileddd");


            }
            return View(jobVM);

        }


        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _jobRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error rrr");
            return View(clubDetails);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
            {
                return View("Error");
            }
            
            _jobRepository.Delete(job);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (job == null)
            {
                return View("Error");

            }
            var jobVM = new EditJobViewModel
            {
                Title = job.Title,
                Description = job.Description,
                ContractType = job.ContractType,
                WorkMode = job.WorkMode,
                PostedDate = job.PostedDate,
                UserId = currentUserId,
                AddressId = job.AddressId,

                Address = new Address
                {
                    City = job.Address.City,
                    Area = job.Address.Area,
                    Street = job.Address.Street,
                }




            };
            return View(jobVM);




        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditJobViewModel JobVM)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit Job");
                return View("Edit", JobVM);
            }
            var userClub = await _jobRepository.GetByIdAsyncNoTracking(id);
            if (userClub != null)
            {
                
             


                var job = new Job
                {
                    Id = id,
                    Title = JobVM.Title,
                    Description = JobVM.Description,
                    ContractType = JobVM.ContractType,
                    WorkMode = JobVM.WorkMode,
                    PostedDate = JobVM.PostedDate,
                    UserId = currentUserId,
                    AddressId = JobVM.AddressId,

                    Address = new Address
                    {
                        City = JobVM.Address.City,
                        Area = JobVM.Address.Area,
                        Street = JobVM.Address.Street,
                    }
                };

                _jobRepository.UpDate(job);

                return RedirectToAction("Index");

            }
            else
            {
                return View(JobVM);
            }
        }










    }
}