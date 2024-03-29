﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;
using alibaba.Services.Models;
using System.Linq;
using System;

namespace alibaba.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _uservice;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserService uservice)
        {
            _uservice = uservice;
            _logger = logger;
        }

        [Route("getUser/{id}")]
        [HttpGet]
        public async Task<User> GetUserById([FromRoute] int id)
        {
            var result = await _uservice.GetUserById(id);

            return result;
        }

        [Route("isEmailVerified/{id}")]
        [HttpGet]
        public async Task<bool> IsEmailVerified([FromRoute] int id)
        {
            var result = await _uservice.GetUserById(id);
            return result.EmailVerified;
        }

        [Route("isPhoneVerified/{id}")]
        [HttpGet]
        public async Task<bool> IsPhoneVerified([FromRoute] int id)
        {
            var result = await _uservice.GetUserById(id);
            return result.EmailVerified;
        }

        [Route("getUserByFirebaseId/{id}")]
        [HttpGet]
        public async Task<User> GetUserByFirebaseId([FromRoute] string id)
        {
            var result = await _uservice.GetUserByFirebaseId(id);
            return result;
        }

        [Route("getUserAddresses/{id}")]
        [HttpGet]
        public async Task<IEnumerable<UserAddress>> GetUseAddresses([FromRoute] int id)
        {
            var result = await _uservice.GetUserAddresses(id);
            return result;
        }

        [Route("deleteUserAddresses/{addressId}")]
        [HttpDelete]
        public async Task<string> DeleteUserAddresses([FromRoute] int addressId)

        {
            var result = await _uservice.DeleteUserAddresses(addressId);
            return result;
        }


        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterUser([FromBody] User user)
        {
            var users = await _uservice.FilterUsers(new UserCriteria()
            {
                Email = user.Email,
            });
            if (users.Any<User>()) {
             
                return BadRequest("Email is Already Used");

            };
            var phoneusers = await _uservice.FilterUsers(new UserCriteria()
            {
                PhoneNumber = user.PhoneNumber
            });
            if (phoneusers.Any<User>()) {
                 return BadRequest("Phone Number is Already Used"); 

            };
            var result = await _uservice.PostUser(user);
            return Ok(result.ToString());
        }
       
        [HttpPost("addAddress")]
        public async Task<ActionResult<string>> AddAddress([FromBody] UserAddress user)
        {
            
            var result = await _uservice.PostUserAddress(user);
            return Ok(result.ToString());
        }
       
        [HttpPut("updateUser")]
        public async Task<string> updateuser([FromBody] User user)
        {
            var result = await _uservice.GetUserById(user.Id);
            if (result != null)
            {
                await _uservice.UpdateUser(user);
                return "success";
            }

            return "user does not exist";

        }
        [HttpGet("getTokensByType/{type}")]
        public async Task<IEnumerable<string>> GetTokens([FromRoute] int type)
        {
            var result = await _uservice.GetTokensByType(type);
        

            return result;

        }
        [HttpPut("VerifyPhoneNumber/{id}")]
        public async Task<string> VerifyPhoneNumber([FromRoute] int id)
        {
            var result = await _uservice.GetUserById(id);
            if (result != null)
            {
                result.PhoneVerified = true;
                await _uservice.UpdateUser(result);
                return "success";
            }

            return "user does not exist";

        }
        [HttpPut("VerifyEmail/{id}")]
        public async Task<string> VerifyEmail([FromRoute] int id)
        {
            var result = await _uservice.GetUserById(id);
            if (result != null)
            {
                result.EmailVerified = true;
                await _uservice.UpdateUser(result);
                return "success";
            }

            return "user does not exist";

        }
        [HttpGet("FindEmail/{email}")]
        //public async Task<string> VerifyEmail([FromRoute] string email)
          public async Task<IActionResult> VerifyEmail([FromRoute] string email)
        {
            var result = await _uservice.FilterUsers(new UserCriteria()
            {
                Email = email
            });
            if (result.Any())
            {
                return Ok(new { Message = "success"});

               //  return "success", result;
            }
            return Ok(new { Message = "field"});

            // return "user does not exist";
        }

        [HttpPut("setuserstatus")]
        public async Task<string> updateuserstatus([FromBody] UserStatus status)
        {
            var result = await _uservice.GetUserById(status.Id);
            if (result != null)
            {
                await _uservice.SetUserStatus(status);
                return "success";
            }

            return "user does not exist";
        }
        [HttpPut("setuserActiveStatus")]

        public async Task<string> updateuserActivestatus([FromBody] UserActiveStatus status)
        {

            await _uservice.SetUserActiveStatus(status);
            return "success";


        }


        [HttpPost("FilterUser")]
        public async Task<IEnumerable<User>> FilterUser([FromBody] UserCriteria criteria)
        {

            var res = await _uservice.FilterUsers(criteria);
            return res;

        }
        [HttpDelete("deleteuser/{userId}")]
        public async Task<string> DeleteUser([FromRoute] int userId)
        {

            var res = await _uservice.DeleteUser(userId);
            return res;

        }

    }
}