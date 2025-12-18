using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.DTOs.Employee;
using NetCoreApi.Services;
using System.Security.Claims;

[ApiController]
[Route("api/employee")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // HR only
    [Authorize(Roles = "HR")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_employeeService.GetAll());
    }

    // HR only
    [Authorize(Roles = "HR")]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_employeeService.GetById(id));
    }

    // Employee (self)
    [Authorize(Roles = "Employee")]
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(_employeeService.GetByUserId(userId));
    }

    // HR only
    [Authorize(Roles = "HR")]
    [HttpPost]
    public IActionResult Create(CreateEmployeeRequest request)
    {
        _employeeService.Create(request);
        return Ok("Employee created");
    }

    // HR only
    [Authorize(Roles = "HR")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateEmployeeRequest request)
    {
        _employeeService.Update(id, request);
        return Ok("Employee updated");
    }

    // HR only
    [Authorize(Roles = "HR")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _employeeService.Delete(id);
        return Ok("Employee deleted");
    }
}
