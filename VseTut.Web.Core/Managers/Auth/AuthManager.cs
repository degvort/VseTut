using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.Auth;
using VseTut.Core.Auth.Dto;
using VseTut.Core.Users.Dto;
using VseTut.Core.Users.Model;
using VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories;

namespace VseTut.Web.Core.Managers.Auth
{
    public class AuthManager : IAuthManager
    {
        private IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IJwtManager _jwtManager;
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ClaimsIdentityFactory _claimsIdentityFactory;

        public AuthManager(
            IUnitOfWork uow,
            IMapper mapper,
            IJwtManager jwtManager,
            IPasswordHasher passwordHasher,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ClaimsIdentityFactory claimsIdentityFactory)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtManager = jwtManager;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _signInManager = signInManager;
            _claimsIdentityFactory = claimsIdentityFactory;
        }

        public async Task<AuthenticateResultModel> LogInAsync(string emailAddress, string password)
        {
            if (string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Looks like you've missed something, please check your credentials and try again");

            var user = _uow.Users.GetUserByEmailAddress(emailAddress);

            if (user == null) throw new Exception("Looks like something went wrong with the email address you entered, please try again!");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

            if (!result.Succeeded) throw new ApplicationException("Looks like something went wrong with your login, please try again");

            var identity = _claimsIdentityFactory.Create(user);
            var accessToken = _jwtManager.CreateAccessToken(_jwtManager.CreateJwtClaims(identity, user));
            var refreshToken = _jwtManager.CreateRefreshToken(_jwtManager.CreateJwtClaims(identity, user));

            return new AuthenticateResultModel
            {
                User = _mapper.Map<UserDto>(user),
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpireInSeconds = (int)AppConst.AccessTokenExpiration.TotalSeconds
            };
        }

        public async Task<AuthenticateResultModel> RegisterAsync(string emailAddress, string password, string userName)
        {
            if (string.IsNullOrWhiteSpace(emailAddress) || !emailAddress.Contains('@')) throw new Exception("Invalid email address, please enter valid email address.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));

            CheckRegistrationInfo(emailAddress, password, userName);

            var securityStamp = _passwordHasher.CreateSecurityStamp();
            var hashedPassword = _passwordHasher.HashPassword(password, securityStamp);
            var normalizedUserName = Normalize(userName);
            var normalizedEmail = Normalize(emailAddress);

            var user = new User
            {
                UserName = userName,
                NormalizedUserName = normalizedUserName,
                PasswordHash = hashedPassword,
                SecurityStamp = securityStamp,
                Email = emailAddress,
                NormalizedEmail = normalizedEmail
            };

            var registrationResult = await _userManager.CreateAsync(user, password);

            if (!registrationResult.Succeeded)
            {
                if (userName.Contains(" ") || userName.Contains('!')) 
                    throw new Exception("UserName can not contain white spaces and '!' caracter!");
                else
                    throw new Exception("Invalid Password. Password must contain at least one upper case character, one number and one special character, and must be at least six characters in length.");
            }
            var result = await LogInAsync(emailAddress, password);
            return result;
        }

        private string Normalize(string stringToNormalize)
        {
            return stringToNormalize.ToLower();
        }

        private void CheckRegistrationInfo(string emailAddress, string password, string userHandle)
        {
            if (_uow.Users.CheckUserEmailAddress(emailAddress)) throw new Exception("Looks like that email address is already registered!");
            if (_uow.Users.CheckUserName(userHandle)) throw new Exception("Looks like that UserName is already taken, please use another!");
        }
    }
}
