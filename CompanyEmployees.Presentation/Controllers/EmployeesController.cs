

using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;


[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IServiceManager _service;

    public EmployeesController(IServiceManager service) => this._service = service;

    [HttpGet]
    public IActionResult GetEmployeesForCompany(Guid companyId)
    {
        var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges: false);
        return Ok(employees);
    }
    [HttpGet("{id:guid}", Name = "GetEmployeeFromCompany")]
    public IActionResult GetEmployeeFromCompany(Guid companyId, Guid id)
    {
        var employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges: false);
        return Ok(employee);
    }
    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
    {
        if (employee == null)
        {
            return BadRequest("EmployeeForCreation object is null");
        }
        var employeeToReturn = _service.EmployeeService
            .CreateEmployeeForCompany(companyId, employee, trackchanges: false);

        return CreatedAtRoute("GetEmployeeFromCompany",
            new { companyId, id = employeeToReturn.Id },
            employeeToReturn);
    }
}
