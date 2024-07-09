using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOffice.Application.Services;
using OutOfOffice.Core.Models;
using OutOfOffice.ViewModels;

namespace OutOfOffice.Controllers
{
    [Authorize(Roles = "Administrator, HRManager, ProjectManager")]
    public class ApprovalRequestController : Controller
    {
        private readonly LeaveRequestService _leaveRequestService;
        private readonly EmployeeService _employeeService;
        private readonly ApprovalRequestService _approvalRequestService;

        // Constructor to initialize leave request, employee, and approval request services
        public ApprovalRequestController(LeaveRequestService leaveRequestService, EmployeeService employeeService, ApprovalRequestService approvalRequestService)
        {
            _leaveRequestService = leaveRequestService;
            _employeeService = employeeService;
            _approvalRequestService = approvalRequestService;
        }

        // View of Approval Requests table
        [Route("Lists/ApproveRequests")]
        public async Task<IActionResult> ApprovalRequests()
        {
            var approveRequests = await _approvalRequestService.Get();

            return View(approveRequests);
        }

        // View for displaying approval request information
        public async Task<IActionResult> ApprovalRequestInfo(Guid id)
        {
            try
            {
                if (id != null)
                {
                    ApprovalRequestEntity? approvalRequest = await _approvalRequestService.GetById(id);
                    if (approvalRequest != null)
                    {
                        return PartialView(approvalRequest);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PartialView for approving a request
        public async Task<IActionResult> ApproveRequestPartial(Guid id)
        {
            try
            {
                if (id != null)
                {
                    ApprovalRequestEntity? approvalRequest = await _approvalRequestService.GetById(id);
                    
                    ViewBag.RelatedEmployees = await CreateRelatedEmployeesList(approvalRequest);

                    return PartialView(approvalRequest);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action to approve a request
        [HttpPost]
        public async Task<IActionResult> ApproveRequest(ApprovalRequestEntity approvalRequest)
        {
            try
            {
                approvalRequest.LeaveRequest = await _leaveRequestService.GetById(approvalRequest.LeaveRequestId);

                await _approvalRequestService.ApproveRequest(approvalRequest.Id, approvalRequest.LeaveRequestId, (Guid)approvalRequest.ApproverId, approvalRequest.LeaveRequest.EmployeeId);

                return RedirectToAction("ApprovalRequests");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PartialView for rejecting a request
        public async Task<IActionResult> RejectRequestPartial(Guid id)
        {
            try
            {
                if (id != null)
                {
                    ApprovalRequestEntity? approvalRequest = await _approvalRequestService.GetById(id);
                    ViewBag.RelatedEmployees = await CreateRelatedEmployeesList(approvalRequest);
                    return PartialView(approvalRequest);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Action to reject a request
        [HttpPost]
        public async Task<IActionResult> RejectRequest(ApprovalRequestEntity approvalRequest)
        {
            try
            {
                approvalRequest.LeaveRequest = await _leaveRequestService.GetById(approvalRequest.LeaveRequestId);

                await _approvalRequestService.RejectRequest(approvalRequest.Id, approvalRequest.LeaveRequestId, (Guid)approvalRequest.ApproverId, approvalRequest.Comment);

                return RedirectToAction("ApprovalRequests");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Helper method to create a select list of related employees for approval
        private async Task<List<SelectListItem?>> CreateRelatedEmployeesList(ApprovalRequestEntity? approvalRequest)
        {
            LeaveRequestEntity? leaveRequest = approvalRequest.LeaveRequest;


            EmployeeEntity employeeLR = leaveRequest.Employee;


            EmployeeEntity? employeeHR = employeeLR.PeoplePartnerId.HasValue
                ? await _employeeService.GetById((Guid)employeeLR.PeoplePartnerId)
                : null;

            var relatedEmployees = employeeLR.AssignedProjects
                .Where(p => p.ProjectManagerId != employeeLR.Id)
                .Select(p => p.ProjectManager)
                .Distinct()
                .ToList();

            if (employeeHR != null)
            {
                relatedEmployees.Add(employeeHR);
            }

            var employeeList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "", Text = "-- Select Approver --", Selected = true, Disabled = true }
                    };

            if (relatedEmployees != null)
            {
                employeeList.AddRange(relatedEmployees.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }));
            }

            return employeeList;
        }
    }
}
