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

        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] LoginVm vm) {
            User? user = await userManager.FindByEmailAsync(vm.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, vm.Password))
                return Unauthorized("Wrong authentication data");

            return Ok(new JwtTokenResponse {
                Token = await jwtTokenService.CreateTokenAsync(user)
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
                    Token = await jwtTokenService.CreateTokenAsync(user)
                });
            }
            catch (IdentityException e) {
                return StatusCode(500, e.IdentityResult.Errors);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenVm vm) {
            var loginResult = await jwtTokenService.RefreshToken(vm);
            if (!loginResult.IsLogedIn)
                return Unauthorized("Refresh token problem");

            return Ok(vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AuthorizedTest() {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            string tokenString = authHeader.Replace("Beraer ", "");

            var token = new JwtSecurityToken(tokenString);
            var response = $"Authenticated!{Environment.NewLine}";

            response += $"{Environment.NewLine}Exp Time: {token.ValidTo.ToLongTimeString()}, Time: {DateTime.UtcNow.ToLongTimeString()}";

            return Ok(response);
        }
    }
}
