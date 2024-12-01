using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnsureThat;
using Hospital.Db.Repositories.Entities;
using Hospital.Domain.Requests.User;
using Hospital.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace Hospital.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public UserController(
            UserRepository userRepository,
            RoleRepository roleRepository,
            IOptions<JwtOptions> jwtOptions)
        {
            EnsureArg.IsNotNull(userRepository, nameof(userRepository));
            EnsureArg.IsNotNull(roleRepository, nameof(roleRepository));
            EnsureArg.IsNotNull(jwtOptions, nameof(jwtOptions));

            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtOptions = jwtOptions;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get/{id}")]
        [SwaggerOperation(OperationId = "Get", Summary = "Получение пользователя по Id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user =
                await _userRepository.GetAsync(id);
            if (user is null)
            {
                return NotFound($"Пользователь с Id:{id} не существует");
            }

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get")]
        [SwaggerOperation(OperationId = "GetAll", Summary = "Получение всех пользователей")]
        public async Task<IActionResult> GetAll()
        {
            var user =
                await _userRepository.GetAllAsync();

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("remove/{id}")]
        [SwaggerOperation(OperationId = "Remove", Summary = "Удаление пользователя")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user is null)
            {
                return NotFound($"Пользователь с Id:{id} не существует");
            }

            await _userRepository.RemoveAsync(user);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addUserRole")]
        [SwaggerOperation(OperationId = "AddUserRole", Summary = "Добавление роли пользователю")]
        public async Task<IActionResult> AddUserRole([FromBody] AddUserRole request)
        {
            var user = await _userRepository.GetAsync(request.UserId);
            if (user is null)
            {
                return NotFound($"Пользователь с Id:{request.UserId} не существует");
            }

            var role = await _roleRepository.GetAsync(request.RoleId);
            if (role is null)
            {
                return NotFound($"Роль с Id:{request.RoleId} не существует");
            }

            await _userRepository.AddUserRole(
                user,
                role);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("removeUserRole")]
        [SwaggerOperation(OperationId = "RemoveUserRole", Summary = "Удаление роли пользователя")]
        public async Task<IActionResult> RemoveUserRole([FromBody] RemoveUserRole request)
        {
            await _userRepository.RemoveUserRole(request.Id);
            return Ok();
        }

        [HttpPost("register")]
        [SwaggerOperation(OperationId = "Register", Summary = "Регистрация пользователя")]
        public async Task<IActionResult> Register([FromBody] RegisterUser request)
        {
            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);
            await _userRepository.AddAsync(request.FullName, request.Login, passwordHash);

            return Ok();
        }

        [HttpPost("login")]
        [SwaggerOperation(OperationId = "Login", Summary = "Вход пользователя")]
        public async Task<IActionResult> Login([FromBody] LoginUser request)
        {
            var user = await _userRepository.GetAsync(request.Login);
            if (user is null)
            {
                return NotFound("Пользователь с указанными учетными данными не существует");
            }

            var verifiedPassword = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash);
            if (!verifiedPassword)
            {
                return NotFound("Неверно указан логин или пароль");
            }

            var userRolesIds = new List<Guid>();
            user.Roles.ToList().ForEach(x => userRolesIds.Add(x.RoleId));

            var userRolesClaims = new List<Claim>();
            foreach (var role in userRolesIds)
            {
                var existingRole = await _roleRepository.GetAsync(role);
                if (existingRole is null)
                {
                    continue;
                }

                userRolesClaims.Add(new Claim("role", existingRole.Name));
            }

            var claims = new List<Claim> { new("userId", user.Id.ToString()) };
            claims.AddRange(userRolesClaims);
            
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.ClientSecret)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddHours(_jwtOptions.Value.ExpiresHours));

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
