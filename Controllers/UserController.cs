using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        [ApiVersion(2)]
        [HttpGet]
        public ActionResult<IEnumerable<WasteBinPaginationViewModel>> GetUsers([FromQuery] int referencia = 0,
            [FromQuery] int tamanho = 10)
        {
            var wasteBins = _userService.GetAllReference(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<UserViewModel>>(wasteBins);
            if (viewModelList.IsNullOrEmpty())
            {
                return NotFound(new { message = "No users found for the given parameters." });
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