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



     

       

        

       

        
        
    }
}