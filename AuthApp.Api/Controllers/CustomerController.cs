using AuthApp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers;

[ApiController]
public class CustomerController:ControllerBase
{

    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetCustomers")]
    public async Task<IActionResult> GetCustomers()
    {
        var customers=await _mediator.Send(new GetAllCustomerNameQuery());
        return Ok(customers);
    }
    
    [HttpGet("GetCustomersNameById")]
    public async Task<IActionResult> GetCustomerNameById(int id)
    {
        var customerName=await _mediator.Send(new GetCustomerNameByIdQuery(id));
        return Ok(customerName);
    }
}