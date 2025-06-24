using JopSy.Interface;
using JopSy.Models;
using JopSy.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace JopSy.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<JobController> _logger;

        public JobController(IJobRepository jobRepository, IAddressRepository addressRepository, ILogger<JobController> logger)
        {
            _jobRepository = jobRepository;
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _jobRepository.GetAll();
            return View(jobs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return NotFound();
            return View(job);
        }

        public async Task<IActionResult> Create()
        {
            var addresses = await _addressRepository.GetAll();
            if (addresses == null || !addresses.Any())
            {
                _logger.LogWarning("No addresses found in the database for Create action.");
                TempData["Error"] = "No addresses available. Please add addresses first.";
                return View(new CreatJobViewModel { AddressOptions = new List<SelectListItem>() });
            }

            var model = new CreatJobViewModel
            {
                AddressOptions = addresses
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = $"{a.City} - {a.Area}"
                    }).ToList(),
                PostedDate = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatJobViewModel model)
        {
            var formData = Request.Form.ToDictionary(f => f.Key, f => f.Value.ToString());
            _logger.LogInformation("Create action called with form data: {@FormData}", formData);
            _logger.LogInformation("Model received: {@Model}", model);

            // Remove AddressOptions from ModelState
            ModelState.Remove("AddressOptions");

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(m => m.Value.Errors.Any())
                    .Select(m => new { Field = m.Key, Errors = m.Value.Errors.Select(e => e.ErrorMessage) });
                _logger.LogWarning("ModelState invalid. Errors: {@Errors}", errors);
                var addresses = await _addressRepository.GetAll();
                model.AddressOptions = addresses?.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.City} - {a.Area}"
                }).ToList() ?? new List<SelectListItem>();
                return View(model);
            }

            var addressExists = await _addressRepository.GetAll();
            if (!addressExists.Any(a => a.Id == model.AddressId))
            {
                _logger.LogWarning("Invalid AddressId: {AddressId}", model.AddressId);
                ModelState.AddModelError("AddressId", "Selected address is invalid.");
                var addresses = await _addressRepository.GetAll();
                model.AddressOptions = addresses?.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.City} - {a.Area}"
                }).ToList() ?? new List<SelectListItem>();
                return View(model);
            }

            var job = new Job
            {
                Title = model.Title,
                CompanyName = model.CompanyName,
                Salary = model.Salary,
                WorkType = model.WorkType,
                Description = model.Description,
                AddressId = model.AddressId,
                PostedDate = model.PostedDate
            };

            _logger.LogInformation("Attempting to add job: {@Job}", job);

            try
            {
                var success = _jobRepository.Add(job);
                if (!success)
                {
                    _logger.LogError("JobRepository.Add returned false, indicating SaveChanges failed.");
                    TempData["Error"] = "Failed to save job to database.";
                    model.AddressOptions = (await _addressRepository.GetAll())
                        ?.Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = $"{a.City} - {a.Area}"
                        }).ToList() ?? new List<SelectListItem>();
                    return View(model);
                }
                _logger.LogInformation("Job successfully added to database.");
                TempData["Success"] = "Job posted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating job: {Message}", ex.Message);
                TempData["Error"] = $"An error occurred while posting the job: {ex.Message}";
                model.AddressOptions = (await _addressRepository.GetAll())
                    ?.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = $"{a.City} - {a.Area}"
                    }).ToList() ?? new List<SelectListItem>();
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return NotFound();

            var addresses = await _addressRepository.GetAll();
            if (addresses == null || !addresses.Any())
            {
                _logger.LogWarning("No addresses found in the database for Edit action.");
                TempData["Error"] = "No addresses available. Please add addresses first.";
            }

            var model = new CreatJobViewModel
            {
                Id = job.Id,
                Title = job.Title,
                CompanyName = job.CompanyName,
                Salary = job.Salary,
                WorkType = job.WorkType,
                Description = job.Description,
                AddressId = job.AddressId,
                PostedDate = job.PostedDate,
                AddressOptions = addresses
                    ?.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = $"{a.City} - {a.Area}"
                    }).ToList() ?? new List<SelectListItem>()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreatJobViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            ModelState.Remove("AddressOptions");

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(m => m.Value.Errors.Any())
                    .Select(m => new { Field = m.Key, Errors = m.Value.Errors.Select(e => e.ErrorMessage) });
                _logger.LogWarning("ModelState invalid. Errors: {@Errors}", errors);
                model.AddressOptions = (await _addressRepository.GetAll())
                    ?.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = $"{a.City} - {a.Area}"
                    }).ToList() ?? new List<SelectListItem>();
                return View(model);
            }

            var job = new Job
            {
                Id = model.Id,
                Title = model.Title,
                CompanyName = model.CompanyName,
                Salary = model.Salary,
                WorkType = model.WorkType,
                Description = model.Description,
                AddressId = model.AddressId,
                PostedDate = model.PostedDate
            };

            try
            {
                var success = _jobRepository.UpDate(job);
                if (!success)
                {
                    _logger.LogError("JobRepository.UpDate returned false, indicating SaveChanges failed.");
                    TempData["Error"] = "Failed to update job in database.";
                    model.AddressOptions = (await _addressRepository.GetAll())
                        ?.Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = $"{a.City} - {a.Area}"
                        }).ToList() ?? new List<SelectListItem>();
                    return View(model);
                }
                _logger.LogInformation("Job successfully updated in database.");
                TempData["Success"] = "Job updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating job: {Message}", ex.Message);
                TempData["Error"] = $"An error occurred while updating the job: {ex.Message}";
                model.AddressOptions = (await _addressRepository.GetAll())
                    ?.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = $"{a.City} - {a.Area}"
                    }).ToList() ?? new List<SelectListItem>();
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return NotFound();

            return View(job);
        }

        
        
    }
}