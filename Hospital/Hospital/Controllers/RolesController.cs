using AutoMapper;
using EnsureThat;
using Hospital.Db.Repositories.Entities;
using Hospital.Domain.Models;
using Hospital.Domain.Requests.Role;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hospital.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RolesController(
            RoleRepository roleRepository,
            IMapper mapper)
        {
            EnsureArg.IsNotNull(roleRepository, nameof(roleRepository));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet("get/{id}")]
        [SwaggerOperation(OperationId = "Get", Summary = "Получение роли по Id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var role =
                await _roleRepository.GetAsync(id);
            if (role is null)
            {
                return NotFound($"Роль с Id:{id} не существует");
            }

            var roleModel = _mapper.Map<RoleModel>(role);

            return Ok(roleModel);
        }

        [HttpGet("get")]
        [SwaggerOperation(OperationId = "GetAll", Summary = "Получение всех ролей")]
        public async Task<IActionResult> GetAll()
        {
            var roles =
                await _roleRepository.GetAllAsync();

            var rolesModels = _mapper.Map<List<RoleModel>>(roles);

            return Ok(rolesModels);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "Add", Summary = "Добавление роли")]
        public async Task<IActionResult> Add([FromBody] AddRole request)
        {
            await _roleRepository.AddAsync(request.Name);
            return Ok();
        }

        [HttpDelete("remove/{id}")]
        [SwaggerOperation(OperationId = "Remove", Summary = "Удаление роли")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var role = await _roleRepository.GetAsync(id);
            if (role is null)
            {
                return NotFound($"Роль с Id:{id} не существует");
            }

            await _roleRepository.RemoveAsync(role);
            return Ok();
        }

        [HttpPut("update")]
        [SwaggerOperation(OperationId = "Update", Summary = "Обновление роли")]
        public async Task<IActionResult> Update([FromBody] UpdateRole request)
        {
            var role = await _roleRepository.GetAsync(request.Id);
            if (role is null)
            {
                return NotFound($"Роль с Id:{request.Id} не существует");
            }

            await _roleRepository.UpdateAsync(role, request.Name);

            var roleModel = _mapper.Map<RoleModel>(role);

            return Ok(roleModel);
        }
    }
}
