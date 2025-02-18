using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Loader;
using System.Security.Claims;
using Talabat.Api.Dtos;
using Talabat.Api.Error;
using Talabat.Api.Extention;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        

        public AccountsController(UserManager<AppUser> userManager ,SignInManager<AppUser> signInManager,ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(CheckEmailExist (model.Email).Result.Value)
            {
                return BadRequest(new ApiResponce(400, "Email Is Already In Use"));
            }
            var user = new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponce(400));
            var returndUser = new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token =await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(returndUser);
        }

        // Login a user
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponce(401));

          var result= await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if(result is null )return Unauthorized(new ApiResponce(401));
            var returned = new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(returned);

        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user =await _userManager.FindByEmailAsync(email);
            var returnedObject = new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token=await _tokenService.CreateTokenAsync(user,_userManager)
            };
            return Ok(returnedObject);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserByAddress()
        {
            //var Email = User.FindFirstValue(ClaimTypes.Email);
            //var user =await _userManager.FindByEmailAsync(Email);
            var user =await _userManager.FindUserWithAddressAsync(User);

            var mappedObject = _mapper.Map<AddressDto>(user.Address);
            return Ok(user.Address);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<AddressDto>> UpdatedAddress(AddressDto UpdatedAddress)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var mappedAddress = _mapper.Map<Address>(UpdatedAddress);
            mappedAddress.Id = user.Address.Id;
            user.Address = mappedAddress;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponce(400));
            return Ok(UpdatedAddress);
        }
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var userEmail = _userManager.FindByEmailAsync(email);
            if (userEmail is null) return false;
            else return true;

        }


    }
}