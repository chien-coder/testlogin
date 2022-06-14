using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserAPI_NetCore6.DataConfig;
using UserAPI_NetCore6.Dtos;
using UserAPI_NetCore6.Models;
using UserAPI_NetCore6.Services;

namespace UserAPI_NetCore6.Controllers
{
    [ApiController]
    //[Authorize]
    public class UsersController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;

        public UsersController(IUserManager userManager, ILogger<UsersController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: All Users
        [HttpGet("user/get-all-users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            //return _userServices.GetAllUserAsync() != null ? 
            //            Ok(await _userServices.GetAllUserAsync()) :
            //            Problem("Entity set 'MyDbContext.Users'  is null.");
            try
            {
                return Ok(await _userManager.GetAllAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET: All Staff
        [HttpGet("user/get-all-staff")]
        public async Task<IActionResult> GetAllStaff()
        {
            try
            {
                return Ok(await _userManager.GetAllStaffAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Users/getuserbyid/5
        [HttpGet("user/getuserbyid/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: Users/CreateNewUser
        [HttpPost("user/add-new-user")]
        public async Task<IActionResult> CreateNewUserAsync(CreateUserDtos createUserDtos)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // test username contrained
                var userDb = await _userManager.GetFirstOrDefaultAsync(u => u.username == createUserDtos.username);
                if (userDb != null) return BadRequest("This username has already existed");

                User user = new User
                {
                    Id = Guid.NewGuid(),
                    username = createUserDtos.username,
                    password = BCrypt.Net.BCrypt.HashPassword(createUserDtos.password, 13),
                    roles = Roles.staff
                };

                //await _userServices.AddNewUserAsync(user);
                var isSaved = await _userManager.AddAsync(user);
                if (!isSaved) return BadRequest("Creating new user is failed");
                //await _userManager.AddAsync(user); 

                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return StatusCode(500);
            }
        }


        // PUT: Users/Edit/5
        [HttpPut("user/update-password-by-id/{id}")]
        public async Task<IActionResult> EditPasswordAsync(string id, UpdateUserDtos updateUserDtos)
        {
            if (!ModelState.IsValid) BadRequest();

            try
            {
                var user = await _userManager.GetUserByIdAsync(id);
                if (user is null)
                {
                    return NotFound();
                }

                user.password = BCrypt.Net.BCrypt.HashPassword(updateUserDtos.password, 13);
                //await _userServices.UpdateUserByIdAsync(user);
                await _userManager.UpdateUserByIdAsync(user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return StatusCode(500);
            }
        }


        // GET: Users/Delete/5
        [HttpDelete("user/delete-user-by-id/{id}")]
        public async Task<IActionResult> DeleteUserByIdAsync(string id)
        {
            try
            {
                var userDb = await _userManager.GetUserByIdAsync(id);
                if (userDb is null) return BadRequest();

                //await _userServices.DeleteUserByIdAsync(userDb);
                await _userManager.DeleteUserByIdAsync(userDb);
                return Ok("Delete successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return StatusCode(500);
            }
        }

        // POST: User/Login
        [HttpPost("user/login-admin")]
        public async Task<IActionResult> LoginAdminAsync(LoginAminDtos loginAminDtos)
        {
            if(!ModelState.IsValid) return BadRequest();
            
            try
            {
                // test username
                var userDB = await _userManager.GetUserByUsernameOfAdminAsync(loginAminDtos.username);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>(); 
                if(userDB is null) return BadRequest("Incorrect username or password.");

                // test password
                bool isValid = BCrypt.Net.BCrypt.Verify(loginAminDtos.password, userDB.password);
                if (isValid)
                {
                    //var claims = new[]
                    //{
                    //    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    //    new Claim("Id", userDB.Id.ToString()),
                    //    new Claim("UserName", userDB.username),
                    //    new Claim("Password", userDB.password)
                    //};

                    //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    //var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    //var token = new JwtSecurityToken(
                    //        jwt.Issuaer,
                    //        jwt.Audience,
                    //        claims,
                    //        expires: DateTime.Now.AddMinutes(10),
                    //        signingCredentials: signin
                    //    );


                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    return Ok(userDB);
                }

                return BadRequest("Incorrect username or password.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return StatusCode(500);
            }
        }

    }
}
