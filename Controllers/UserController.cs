using System.Security.Claims;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WasteBinsAPI.Models;
using WasteBinsAPI.Services;
using WasteBinsAPI.ViewModel;

namespace WasteBinsAPI.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Authorize]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var viewModelList = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return Ok(viewModelList);
        }

        [ApiVersion(2)]
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<IEnumerable<WasteBinPaginationViewModel>> GetUsers([FromQuery] int referencia = 0,
            [FromQuery] int tamanho = 10)
        {
            var wasteBins = _userService.GetAllReference(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<UserViewModel>>(wasteBins);
            if (viewModelList.IsNullOrEmpty())
            {
                return NotFound();
            }

            var viewModel = new UserPaginationViewModel
            {
                Users = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().UserId
            };
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<UserViewModel>(user);
            return Ok(viewModel);
        }

        [HttpGet("my-account")]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<ActionResult<UserViewModel>> GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByIdAsync(Int32.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<UserViewModel>(user);
            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateViewModel userUpdateViewModel)
        {
            if (id != userUpdateViewModel.UserId)
            {
                return BadRequest(new { message = "User ID mismatch." });
            }

            var userFound = await _userService.GetUserByIdAsync(id);
            if (userFound == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var userModel = _mapper.Map<UserModel>(userUpdateViewModel);
            await _userService.UpdateUserAsync(userModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userFound = await _userService.GetUserByIdAsync(id);
            if (userFound == null)
            {
                return NotFound(new { message = "User not found." });
            }

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpDelete("my-account/delete")]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<ActionResult<UserViewModel>> DeleteUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            int ParsedUserId = Int32.Parse(userId);
            var userFound = await _userService.GetUserByIdAsync(ParsedUserId);
            if (userFound == null)
            {
                return NotFound(new { message = "User not found." });
            }

            await _userService.DeleteUserAsync(ParsedUserId);
            return NoContent();
        }
    }
}