using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WasteBinsAPI.Models;
using WasteBinsAPI.Services;
using WasteBinsAPI.ViewModel;

namespace WasteBinsAPI.Controllers
{
    [ApiVersion(1)]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var viewModelList = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateViewModel userUpdateViewModel)
        {
            if (id != userUpdateViewModel.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            var userFound = await _userService.GetUserByIdAsync(id);
            if (userFound == null)
            {
                return NotFound("User not found.");
            }

            var userModel = _mapper.Map<UserModel>(userUpdateViewModel);
            await _userService.UpdateUserAsync(userModel);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser([FromBody] UserCreateViewModel viewModel)
        {
            var userModel = _mapper.Map<UserModel>(viewModel);
            await _userService.AddUserAsync(userModel);
            
            var userViewModel = _mapper.Map<UserViewModel>(userModel);
            return CreatedAtAction(nameof(GetUser), new { id = userViewModel.UserId }, userViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userFound = await _userService.GetUserByIdAsync(id);
            if (userFound == null)
            {
                return NotFound("User not found.");
            }

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}