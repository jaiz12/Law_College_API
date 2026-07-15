using Common.DataContext;
using DTO.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagementController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserManagementController(RoleManager<IdentityRole> roleManager,  UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid data",
                    messageDescription = "Please provide valid user details.",
                    messageType = "error"
                });
            }

            try
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    return BadRequest(new
                    {
                        message = "User already exists",
                        messageDescription = "An account with this email already exists.",
                        messageType = "warning"
                    });
                }

                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,

                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,

                    UserStatus = true,

                    SecurityStamp = Guid.NewGuid().ToString(),

                    CreatedBy = model.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = model.CreatedBy,
                    UpdatedOn = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(new
                    {
                        message = "User creation failed",
                        messageDescription = string.Join(", ", result.Errors.Select(x => x.Description)),
                        messageType = "error"
                    });
                }

                // Find Role by Id
                var role = await _roleManager.FindByIdAsync(model.RoleId);

                if (role == null)
                {
                    return BadRequest(new
                    {
                        message = "Role not found",
                        messageDescription = "Selected role does not exist.",
                        messageType = "error"
                    });
                }

                // Assign User to Role
                var roleResult = await _userManager.AddToRoleAsync(user, role.Name);

                if (!roleResult.Succeeded)
                {
                    return BadRequest(new
                    {
                        message = "Role assignment failed",
                        messageDescription = string.Join(", ", roleResult.Errors.Select(x => x.Description)),
                        messageType = "error"
                    });
                }

                return Ok(new
                {
                    message = "Success",
                    messageDescription = "User created successfully.",
                    messageType = "success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server Error",
                    messageDescription = ex.Message,
                    messageType = "error"
                });
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid data",
                    messageDescription = "Please provide valid user details.",
                    messageType = "error"
                });
            }

            try
            {
                // Find User
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    return NotFound(new
                    {
                        message = "User not found",
                        messageType = "error"
                    });
                }

                // Check duplicate email
                var emailExists = await _userManager.FindByEmailAsync(model.Email);

                if (emailExists != null && emailExists.Id != user.Id)
                {
                    return BadRequest(new
                    {
                        message = "Email already exists",
                        messageDescription = "Another user is already using this email.",
                        messageType = "warning"
                    });
                }

                // Update User Details
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                user.NormalizedUserName = model.UserName.ToUpperInvariant();
                user.NormalizedEmail = model.Email.ToUpperInvariant();

                user.UpdatedBy = model.UpdatedBy;
                user.UpdatedOn = DateTime.UtcNow;

                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                {
                    return BadRequest(new
                    {
                        message = "User update failed",
                        messageDescription = string.Join(", ", updateResult.Errors.Select(x => x.Description)),
                        messageType = "error"
                    });
                }

                // ============================
                // Update Role
                // ============================

                var selectedRole = await _roleManager.FindByIdAsync(model.RoleId);

                if (selectedRole == null)
                {
                    return BadRequest(new
                    {
                        message = "Role not found",
                        messageDescription = "Selected role does not exist.",
                        messageType = "error"
                    });
                }

                var currentRoles = await _userManager.GetRolesAsync(user);

                if (!currentRoles.Contains(selectedRole.Name!))
                {
                    if (currentRoles.Any())
                    {
                        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

                        if (!removeResult.Succeeded)
                        {
                            return BadRequest(new
                            {
                                message = "Unable to remove existing role",
                                messageDescription = string.Join(", ", removeResult.Errors.Select(x => x.Description)),
                                messageType = "error"
                            });
                        }
                    }

                    var addResult = await _userManager.AddToRoleAsync(user, selectedRole.Name!);

                    if (!addResult.Succeeded)
                    {
                        return BadRequest(new
                        {
                            message = "Unable to assign new role",
                            messageDescription = string.Join(", ", addResult.Errors.Select(x => x.Description)),
                            messageType = "error"
                        });
                    }
                }

                // ============================
                // Update Password (Optional)
                // ============================

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResult = await _userManager.ResetPasswordAsync(
                        user,
                        token,
                        model.Password);

                    if (!passwordResult.Succeeded)
                    {
                        return BadRequest(new
                        {
                            message = "Password update failed",
                            messageDescription = string.Join(", ", passwordResult.Errors.Select(x => x.Description)),
                            messageType = "error"
                        });
                    }
                }

                return Ok(new
                {
                    message = "Success",
                    messageDescription = "User updated successfully.",
                    messageType = "success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server Error",
                    messageDescription = ex.Message,
                    messageType = "error"
                });
            }
        }


        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound(new
                    {
                        message = "User not found",
                        messageType = "error"
                    });
                }

                // Remove all assigned roles (optional)
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Any())
                {
                    var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, roles);

                    if (!removeRoleResult.Succeeded)
                    {
                        return BadRequest(new
                        {
                            message = "Unable to remove user roles.",
                            messageDescription = string.Join(", ", removeRoleResult.Errors.Select(x => x.Description)),
                            messageType = "error"
                        });
                    }
                }

                // Delete user
                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(new
                    {
                        message = "Delete failed",
                        messageDescription = string.Join(", ", result.Errors.Select(x => x.Description)),
                        messageType = "error"
                    });
                }

                return Ok(new
                {
                    message = "Success",
                    messageDescription = "User deleted successfully.",
                    messageType = "success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server Error",
                    messageDescription = ex.Message,
                    messageType = "error"
                });
            }
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userManager.Users
                    .OrderByDescending(x => x.CreatedOn)
                    .ToListAsync();

                var result = new List<object>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var roleName = roles.FirstOrDefault();

                    string? roleId = null;

                    if (!string.IsNullOrEmpty(roleName))
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);

                        roleId = role?.Id;
                    }

                    result.Add(new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.PhoneNumber,
                        user.UserStatus,
                        user.CreatedOn,
                        user.UpdatedOn,

                        RoleId = roleId,
                        RoleName = roleName
                    });
                }

                return Ok(new
                {
                    message = "Success",
                    messageType = "success",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server Error",
                    messageDescription = ex.Message,
                    messageType = "error"
                });
            }
        }
    }
}
