using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResponseApiClient.Messaging.Users.Contracts;

namespace RequestApiClient.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRequestClient<IGetUserDetails> _getUserDetailsClient;

        public UsersController(IRequestClient<IGetUserDetails> getUserDetailsClient)
        {
            _getUserDetailsClient = getUserDetailsClient;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var response = await _getUserDetailsClient.GetResponse<IUserDetails>(new { Id = userId });

            return Ok(response.Message);

        }
    }
}