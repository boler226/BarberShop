using BarberShop.Database.Entities.Identity;
using BarberShop.Exceptions;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Account;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController(
        UserManager<User> userManager,
        IJwtTokenService jwtTokenService,
        IValidator<RegisterVm> registerValidator,
        IAccountsControllerService service
        ) : ControllerBase {

        [HttpGet]
        public async Task<IActionResult> Me(string token) {
            var identityUser = await jwtTokenService.GetUserByTokenAsync(token);

            if (identityUser is null ) 
                return NotFound();

            return Ok(identityUser);
        }
 
        [HttpGet]
        [Authorize]
        public  IActionResult AuthorizedTest() {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            string tokenString = authHeader.Replace("Beraer ", "");

            var token = new JwtSecurityToken(tokenString);
            var response = $"Authenticated!{Environment.NewLine}";

            response += $"{Environment.NewLine}Exp Time: {token.ValidTo.ToLongTimeString()}, Time: {DateTime.UtcNow.ToLongTimeString()}";

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> LoginIn([FromForm] LoginVm vm) {
            User? user = await userManager.FindByEmailAsync(vm.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, vm.Password))
                return Unauthorized("Wrong authentication data");

            user.RefreshToken = jwtTokenService.CreateRefreshToken();
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(12);


            await userManager.UpdateAsync(user);

            return Ok(new JwtTokenResponse {
                IsLogedIn = true,
                Token = await jwtTokenService.CreateTokenAsync(user),
                RefreshToken = user.RefreshToken ?? throw new Exception("Refresh token null exeption")
            });
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] RegisterVm vm) {
            var validatrionResult = await registerValidator.ValidateAsync(vm);

            if (!validatrionResult.IsValid)
                return BadRequest(validatrionResult.Errors);

            try {
                var user = await service.SignUpAsync(vm);

                return Ok(new JwtTokenResponse {
                    Token = await jwtTokenService.CreateTokenAsync(user),
                    RefreshToken = user.RefreshToken ?? throw new Exception("Refresh token null exeption")
                });
            }
            catch (IdentityException e) {
                return StatusCode(500, e.IdentityResult.Errors);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenVm vm) {
            var loginResult = await jwtTokenService.RefreshToken(vm);
            if (!loginResult.IsLogedIn)
                return Unauthorized("Refresh token wasn't changed");

            return Ok(vm);
        }

    }
}
