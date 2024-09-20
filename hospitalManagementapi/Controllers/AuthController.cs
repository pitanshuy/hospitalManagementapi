using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hospitalManagementapi.Data;
using System.Security.Cryptography;
using System.Text;
using hospitalManagementapi.Services;
using hospitalManagementapi.Models;
using Microsoft.EntityFrameworkCore;

namespace hospitalManagementapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TokenServices _tokenservice;



        public AuthController(DataContext context,TokenServices tokenServices)
        {
            _context = context;
            _tokenservice = tokenServices;

        }
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(User userDto )
        {
            if (await UserExists(userDto.username))
                return BadRequest("UserName is Taken");




            using var hmac = new HMACSHA512();
            var user = new User
            {
                username = userDto.username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password))


             }








        }


        // Method to check if a user with the given username already exists
        private async Task<bool> UserExists(string username)
        {
            // Use Entity Framework to check if any user in the database has the given username
            return await _context.Users.AnyAsync(user => user.username == username);
        }



    }
}
