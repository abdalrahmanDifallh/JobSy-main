using JopSy.Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JopSy.ViewModel
{
    public class CreatJobViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string CompanyName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Work type is required")]
        public WorkType WorkType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Posted date is required")]
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; }

        [BindNever]
        public IEnumerable<SelectListItem> AddressOptions { get; set; }
    }
}