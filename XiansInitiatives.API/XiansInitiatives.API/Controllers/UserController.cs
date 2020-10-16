using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Constants;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IAzureBusService _azureBusService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, IAzureBlobStorageService azureBlobStorageService, IAzureBusService azureBusService, IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userService = userService;
            _azureBusService = azureBusService;
            _azureBlobStorageService = azureBlobStorageService;
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var userToReturn = await _userService.GetUserAsync(id);
            if (userToReturn != null)
            {
                return Ok(userToReturn);
            }
            return BadRequest("User not found");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UsersForAdminDto> usersToReturn = new List<UsersForAdminDto>();
            var firstRole = 0;

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userToReturn = _mapper.Map<UsersForAdminDto>(user);
                userToReturn.Role = roles[firstRole];

                if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
                {
                    userToReturn.Locked = true;
                }
                else
                {
                    userToReturn.Locked = false;
                }

                usersToReturn.Add(userToReturn);
            }
            return Ok(usersToReturn);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UsersForAdminDto userForEdit)
        {
            var user = await _unitOfWork.User.GetUser(userForEdit.Id);
            if (user != null)
            {
                _mapper.Map(userForEdit, user);
                var firstRole = 0;
                try
                {
                    var previousRole = await _userManager.GetRolesAsync(user);
                    if (userForEdit.Role != null && previousRole[firstRole] != userForEdit.Role)
                    {
                        var addNewRole = await _userManager.AddToRoleAsync(user, userForEdit.Role);
                        if (!addNewRole.Succeeded)
                        {
                            return BadRequest("Adding new Role  unsuccessful");
                        }
                        var deletePreviousRole = await _userManager.RemoveFromRoleAsync(user, previousRole[firstRole]);
                        if (!deletePreviousRole.Succeeded)
                        {
                            return BadRequest("Removing previous Role unsuccessful");
                        }
                        await _azureBusService.SendEmailAsync(user.Id, "roleChange");
                    }
                    if (userForEdit.Locked)
                    {
                        user.LockoutEnd = DateTime.Now.AddYears(100);
                    }

                    if (!userForEdit.Locked && user.LockoutEnd > DateTime.Now && user.LockoutEnd != null)
                    {
                        user.LockoutEnd = DateTime.Now;
                    }
                    await _unitOfWork.Save();
                    return Ok();
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"error message: {exception}");
                    return BadRequest("Save unsuccesful");
                }
            }
            return BadRequest("Save unsuccesful");
        }

        [Authorize(Roles = UserRoles.AdminRole)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await _unitOfWork.User.GetUser(Id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest("Deletion unsuccessful");
        }

        [HttpPost("upload/{userId}")]
        public async Task<IActionResult> Upload(string userId, [FromForm] UserForEditDto userForEditDto)
        {
            try
            {
                var imageUrl = _azureBlobStorageService.UploadFileToBlob(userForEditDto.File);
                var result = await _userService.SetProfileImageUrl(userId, imageUrl);
                if (result == false)
                {
                    return BadRequest("Failed save the image");
                }
                Console.WriteLine($"path message: {imageUrl}");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}