using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MyDemoProject003.Application.Common.Extensions;
using MyDemoProject003.Application.Common.Helpers;
using MyDemoProject003.Application.Customers.Commands.CreateCustomer;
using MyDemoProject003.Application.Customers.Queries.AuthenticateCustomer;
using MyDemoProject003.Application.Customers.Queries.GetCustomer;
using MyDemoProject003.Application.Customers.Queries.GetCustomersList;

using MyDemoProject003.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace MyDemoProject003.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    /*[EnableCors("CorsPolicy")] */ //allow other sites to make cross-origin requests to your app

    public class UsersController : ControllerBase
    {
        #region Data Members
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation($"Executing Controller:{this.GetType().Name}");
            
        }
        #endregion

        #region HTTP Verb Methods  

        #region Read    
        [HttpGet("AuthenticateUser/{username}/{password}", Name = "AuthenticateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateCustomer(string username, string password)
        {
            try
            {
                if (username.IsStringEmpty() || password.IsStringEmpty())
                    return BadRequest(StatusCode(StatusCodes.Status400BadRequest,
                        "Please provide username and password"));


                return Ok(await _mediator.Send(new AuthenticateCustomerQuery(username, password)));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("Get", Name = "GetCustomers")]

        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _mediator.Send(new GetCustomerListQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


       

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (!id.IsGuidValid())
                {
                    return NotFound("Invalid Id format");
                }

                var customerId = new Guid(id); 
                var result = await _mediator.Send(new GetCustomerByIdQuery(customerId));

                if (result == null)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
     
        #endregion

        #region Write

        [HttpPost("AddUser/", Name = "AddUser")]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CustomerDocument customer)
        {
            _logger.LogInformation("Creating New User:");

            var result = await _mediator.Send(new CreateCustomerCommand(customer));

            if (result != null)
            {
                _logger.LogInformation("New record saved JSON:{0}", result.JsonResponded);
            }

            return Ok(result);
        }

        #endregion

        #endregion
    }
}