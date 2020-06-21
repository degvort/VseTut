using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VseTut.Core.Auth;
using VseTut.Core.Auth.Dto;
using VseTut.Core.Users.Model;
using VseTut.Web.Core;
using VseTut.Web.Host.Models;

namespace VseTut.Web.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IJwtManager _jwtManager;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthManager authManager,
            IJwtManager jwtManager,
            UserManager<User> userManager)
        {
            _authManager = authManager;
            _jwtManager = jwtManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterInput input)
        {
            try
            {
                var user = await _authManager.RegisterAsync(input.EmailAddress, input.Password, input.UserName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            try
            {
                var result = await _authManager.LogInAsync(model.EmailAddress, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.RefreshToken)) throw new ArgumentNullException(nameof(model.RefreshToken));
                if (!_jwtManager.IsRefreshTokenValid(model.RefreshToken, out var principal)) throw new ValidationException("Refresh token is not valid!");

                var userId = _userManager.GetUserId(User);
                var accessToken = _jwtManager.CreateAccessToken(principal.Claims);
                //var refreshToken = _jwtManager.CreateRefreshToken(principal.Claims);

                return Ok(await Task.FromResult(new RefreshTokenResult(accessToken, (int)AppConst.AccessTokenExpiration.TotalSeconds,
                    model.RefreshToken, userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
