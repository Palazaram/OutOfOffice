using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using OutOfOffice.Application.Services;
using OutOfOffice.Core.Models;

namespace OutOfOffice.Controllers
{
    [Authorize(Roles = "Administrator, HRManager, ProjectManager, Employee")]
    public class LeaveRequestController : Controller
    {
        private readonly LeaveRequestService _leaveRequestService;
        private readonly EmployeeService _employeeService;
        private readonly ApprovalRequestService _approvalRequestService;

        // Constructor to initialize services for leave request, employee, and approval request
        public LeaveRequestController(LeaveRequestService leaveRequestService, EmployeeService employeeService, ApprovalRequestService approvalRequestService)
        {
            _leaveRequestService = leaveRequestService;
            _employeeService = employeeService;
            _approvalRequestService = approvalRequestService;
        }

        // View of Leave Requests table
        [Route("Lists/LeaveRequests")]
        public async Task<IActionResult> LeaveRequests()
        {
            var leaveRequests = await _leaveRequestService.Get();

            return View(leaveRequests);
        }

        // View for creating a new leave request
        public async Task<IActionResult> LeaveRequestCreate()
        {
            ViewBag.Employees = await CreateWorkersSelectList();
            return View();
        }

        // Action to create a new leave request
        [HttpPost]
        public async Task<IActionResult> Create(LeaveRequestEntity leaveRequest)
        {
            try
            {
                await _leaveRequestService.Add(leaveRequest);
                return RedirectToAction("LeaveRequests");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return error status if exception occurs
            }
        }

        // View for editing an existing leave request
        public async Task<IActionResult> LeaveRequestEdit(Guid id)
        {
            try
            {
                ViewBag.Employees = await CreateWorkersSelectList();

                if (id != null)
                {
                    LeaveRequestEntity? leaveRequest = await _leaveRequestService.GetById(id);
                    if (leaveRequest != null)
                    {
                        return View(leaveRequest);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action to edit an existing leave request
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, LeaveRequestEntity leaveRequest)
        {
            try
            {
                if (id != leaveRequest.Id)
                {
                    return NotFound();
                }

                await _leaveRequestService.Update(leaveRequest);
                return RedirectToAction("LeaveRequests");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // View for displaying leave request information
        public async Task<IActionResult> LeaveRequestInfo(Guid id)
        {
            try
            {
                if (id != null)
                {
                    LeaveRequestEntity? leaveRequest = await _leaveRequestService.GetById(id);
                    if (leaveRequest != null)
                    {
                        return PartialView(leaveRequest);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action to submit a leave request
        [HttpPost]
        public async Task<IActionResult> SubmitLeaveRequest(Guid id)
        {
            try
            {
                if (id != null)
                {
                    await _leaveRequestService.SubmitLeaveRequest(id);

                    return RedirectToAction("LeaveRequests");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action to cancel a leave request
        [HttpPost]
        public async Task<IActionResult> CancelLeaveRequest(Guid id)
        {
            try
            {
                if (id != null)
                {
                    await _leaveRequestService.CancelLeaveRequest(id);

                    return RedirectToAction("LeaveRequests");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action for AJAX REQUEST to check if an employee has enough days off for the requested leave period
        [HttpGet]
        public async Task<bool> CheckDaysOff(Guid id, DateTime startDate, DateTime endDate)
        {
            var employee = await _employeeService.GetById(id);

            var daysRequested = (endDate - startDate).Days + 1;

            if (employee.OutOfOfficeBalance < daysRequested) 
            {
                return false;
            }

            return true;
        }

        // Helper method to create a select list of employees
        private async Task<List<SelectListItem>> CreateWorkersSelectList()
        {
            IEnumerable<EmployeeEntity> employees = await _employeeService.Get();

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
    }
}
