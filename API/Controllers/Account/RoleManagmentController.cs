using Common.DataContext;
using DTO.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleManagmentController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleManagmentController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        [HttpPost("CreateRole")]
        public async Task<object> CreateRoles([FromBody] AspNetRoles_DTO model)
        {
            string errors = null;
            var userRoleExist = await _roleManager.RoleExistsAsync(model.Name);
            if (userRoleExist)
            {
                return Ok(new { message = "Role Already Exist", messageDescription = "", messageType = "warning" });

            }

            var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = model.Name.Trim(), NormalizedName = model.Name.Trim().ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role Created Successfully", messageDescription = "", messageType = "success" });
            }
            else
            {
                return BadRequest(new { message = "Error While Trying To Add a Role", messageDescription = "", messageType = "error" });
            }

        }

        [HttpPut("EditRole")]
        public async Task<IActionResult> UpdateRoles(AspNetRoles_DTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleExist = await _roleManager.FindByIdAsync(model.Id);
            if (roleExist == null)
            {
                return BadRequest(new { message = "No Such Role Found", messageDescription = "", messageType = "error" });
            }

            var roleNameExist = await _roleManager.FindByNameAsync(model.Name);
            if (roleNameExist != null && roleNameExist.Id != model.Id)
            {
                return Ok(new { message = "Role Already Exists", messageDescription = "", messageType = "warning" });
            }

            if (roleExist.Name != model.Name)
            {
                roleExist.Name = model.Name;

                var result = await _roleManager.UpdateAsync(roleExist);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Role Updated Successfully", messageDescription = "", messageType = "success" });
                }
                else
                {
                    return BadRequest(new { message = "Error while trying to update the role", messageDescription = "", messageType = "error" });
                }
            }

            return Ok(new { message = "No Changes Detected", messageDescription = "", messageType = "info" });
        }


        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles
                .Select(role => new
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    Users = _context.UserRoles.Count(ur => ur.RoleId == role.Id)
                })
                .ToListAsync();

            return Ok(roles);
        }

        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRoles(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleExist = await _roleManager.FindByIdAsync(id);
            if (roleExist == null)
            {
                return BadRequest(new { message = "No Such Role Found", messageDescription = "", messageType = "error" });
            }

            var result = await _roleManager.DeleteAsync(roleExist);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role Deleted Successfully", messageDescription = "", messageType = "success" });
            }
            else
            {
                return BadRequest(new { message = "Error while trying to delete the role", messageDescription = "", messageType = "error" });
            }
        }
    }
}
