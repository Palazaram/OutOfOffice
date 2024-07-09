using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOffice.Application.Services;
using OutOfOffice.Core.Models;
using OutOfOffice.ViewModels;

namespace OutOfOffice.Controllers
{
    [Authorize(Roles = "Administrator, HRManager, ProjectManager")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly ProjectService _projectService;
        private readonly IWebHostEnvironment _hostEnvironment;

        // Constructor to initialize services and host environment
        public EmployeeController(EmployeeService employeeService, ProjectService projectService, IWebHostEnvironment hostEnvironment)
        {
            _employeeService = employeeService;
            _projectService = projectService;
            _hostEnvironment = hostEnvironment;
        }

        // View of Employees table
        [Route("Lists/Employees")]
        public async Task<IActionResult> Employees()
        {
            var employees = await _employeeService.Get();

            return View(employees);
        }

        // Create View
        public async Task<IActionResult> EmployeeCreate()
        {
            ViewBag.PeoplePartners = await CreateWorkersSelectList(null);

            return View();
        }

        // Action to create a new employee
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeEntity employee)
        {
            try
            {
                if (employee.Photo != null)
                {
                    CreateImage(employee); // Create image if photo is provided
                }

                await _employeeService.Add(employee);
                return RedirectToAction("Employees");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return error status if exception occurs
            }
        }

        // Edit View
        public async Task<IActionResult> EmployeeEdit(Guid id)
        {
            try
            {
                ViewBag.PeoplePartners = await CreateWorkersSelectList(id);

                if (id != null)
                {
                    EmployeeEntity? employee = await _employeeService.GetById(id);
                    if (employee != null)
                    {
                        return View(employee);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action to edit an existing employee
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EmployeeEntity employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return NotFound();
                }

                if (employee.Photo != null)
                {
                    CreateImage(employee); // Update image if photo is provided
                }

                await _employeeService.Update(employee);
                return RedirectToAction("Employees");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            } 
        }

        // Action to deactivate an employee
        [HttpPost]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                if (id != null)
                {
                    EmployeeEntity employee = await _employeeService.GetById(id);

                    employee.Status = "Inactive"; // Set employee status to inactive
                    await _employeeService.Update(employee);

                    return RedirectToAction("Employees");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            } 
        }

        // View employee information
        public async Task<IActionResult> EmployeeInfo(Guid id)
        {
            try
            {
                if (id != null)
                {
                    EmployeeEntity employee = await _employeeService.GetById(id);
                    if (employee != null)
                    {
                        return PartialView(employee);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PartialView of projects for an employee
        public async Task<IActionResult> ProjectsList(Guid id)
        {
            try
            {
                var employee = await _employeeService.GetById(id);
                var projectList = await _projectService.Get();

                var employeeViewModel = new EmployeeViewModel 
                {
                    Employee = employee,
                    Projects = projectList
                };

                return PartialView(employeeViewModel);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action to add projects to an employee
        [HttpPost]
        public async Task<IActionResult> AddProjectsToEmployee(EmployeeViewModel model)
        {
            try
            {
                var employee = await _employeeService.GetById(model.Employee.Id);
                if (employee == null)
                {
                    return NotFound();
                }

                await _employeeService.AssignProjectsToEmployee(employee.Id, model.SelectedProjects.ToList());

                return RedirectToAction("Employees");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action for AJAX REQUEST to remove current photo of an employee
        public async Task RemoveCurrentPhoto(Guid id) 
        {
            var employee = await _employeeService.GetById(id);
            DeleteImage(employee);
            await _employeeService.DeletePhoto(employee);
        }

        // Helper method to create a select list for workers
        private async Task<List<SelectListItem>> CreateWorkersSelectList(Guid? id) 
        {
            IEnumerable<EmployeeEntity> employees = await _employeeService.Get();

            if (id != null)
            {
                employees = employees.Where(x => x.Position == "HR Manager" && x.Id != id);
            }
            else { employees = employees.Where(x => x.Position == "HR Manager"); }

            

            var employeeList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- Select Partner --", Selected = true, Disabled = true }
            };

            employeeList.AddRange(employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.FullName
            }));

            return employeeList;
        }

        // Helper method to create an image for an employee
        private async void CreateImage(EmployeeEntity employee)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            DeleteImage(employee); // Delete existing image if any

            string fileName = Path.GetFileNameWithoutExtension(employee.Photo.FileName);
            string extension = Path.GetExtension(employee.Photo.FileName);
            employee.PhotoPath = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/employeesPhotoes/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await employee.Photo.CopyToAsync(fileStream);
            }
        }

        // Helper method to delete an image of an employee
        private void DeleteImage(EmployeeEntity employee) 
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (!string.IsNullOrEmpty(employee.PhotoPath))
            {
                string oldPath = Path.Combine(wwwRootPath + "/employeesPhotoes/", employee.PhotoPath);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }
        }
    }
}
