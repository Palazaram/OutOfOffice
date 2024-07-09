using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using OutOfOffice.Application.Services;
using OutOfOffice.Core.Models;
using System.Linq;

namespace OutOfOffice.Controllers
{
    [Authorize(Roles = "Administrator, HRManager, ProjectManager, Employee")]
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly EmployeeService _employeeService;

        // Constructor to initialize project and employee services
        public ProjectController(ProjectService projectService, EmployeeService employeeService)
        {
            _projectService = projectService;
            _employeeService = employeeService;
        }

        // View of Projects table
        [Route("Lists/Projects")]
        public async Task<IActionResult> Projects()
        {
            var projects = await _projectService.Get();

            return View(projects);
        }

        // Create View for a project
        public async Task<IActionResult> ProjectCreate()
        {
            ViewBag.ProjectManagers = await CreateProjectManagersSelectList();

            return View();
        }

        // Action to create a new project
        [HttpPost]
        public async Task<IActionResult> Create(ProjectEntity project)
        {
            try
            {
                await _projectService.Add(project);
                return RedirectToAction("Projects");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Edit View for a project
        public async Task<IActionResult> ProjectEdit(Guid id)
        {
            try
            {
                ViewBag.ProjectManagers = await CreateProjectManagersSelectList();

                if (id != null)
                {
                    ProjectEntity? project = await _projectService.GetById(id);
                    if (project != null)
                    {
                        return View(project);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return error status if exception occurs
            }
        }

        // Action to edit an existing project
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProjectEntity project)
        {
            try
            {
                if (id != project.Id)
                {
                    return NotFound();
                }

                await _projectService.Update(project);
                return RedirectToAction("Projects");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action to deactivate a project
        [HttpPost]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                if (id != null)
                {
                    ProjectEntity project = await _projectService.GetById(id);

                    project.Status = "Inactive";
                    await _projectService.Update(project);

                    return RedirectToAction("Projects");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PartialView project information
        public async Task<IActionResult> ProjectInfo(Guid id)
        {
            try
            {
                if (id != null)
                {
                    ProjectEntity? project = await _projectService.GetById(id);
                    if (project != null)
                    {
                        return PartialView(project);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Helper method to create a select list for project managers
        private async Task<List<SelectListItem>> CreateProjectManagersSelectList()
        {
            var projectManagersList = await _employeeService.Get();
            projectManagersList = projectManagersList.Where(x => x.Position == "Project Manager");

            var selectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- Select Project Manager --", Selected = true, Disabled = true }
            };

            selectList.AddRange(projectManagersList.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.FullName
            }));

            return selectList;
        }
    }
}
