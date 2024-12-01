using AutoMapper;
using EnsureThat;
using Hospital.Db.Repositories.Entities;
using Hospital.Domain.Models;
using Hospital.Domain.Requests.Town;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hospital.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TownsController : ControllerBase
    {
        private readonly TownRepository _townRepository;
        private readonly IMapper _mapper;

        public TownsController(
            TownRepository townsRepository,
            IMapper mapper)
        {
            EnsureArg.IsNotNull(townsRepository, nameof(townsRepository));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            _townRepository = townsRepository;
            _mapper = mapper;
        }

        [HttpGet("get/{id}")]
        [SwaggerOperation(OperationId = "Get", Summary = "Получение города по Id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var town = 
                await _townRepository.GetAsync(id);
            if (town is null)
            {
                return NotFound($"Город с Id:{id} не существует");
            }

            var townModel = _mapper.Map<TownModel>(town);

            return Ok(townModel);
        }

        [HttpGet("get")]
        [SwaggerOperation(OperationId = "GetAll", Summary = "Получение всех городов")]
        public async Task<IActionResult> GetAll()
        {
            var towns =
                await _townRepository.GetAllAsync();

            var townsModels = _mapper.Map<List<TownModel>>(towns);

            return Ok(townsModels);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "Add", Summary = "Добавление города")]
        public async Task<IActionResult> Add([FromBody] AddTown request)
        {
            await _townRepository.AddAsync(request.Name);
            return Ok();
        }

        [HttpDelete("remove/{id}")]
        [SwaggerOperation(OperationId = "Remove", Summary = "Удаление города")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var town = await _townRepository.GetAsync(id);
            if (town is null)
            {
                return NotFound($"Город с Id:{id} не существует");
            }

            await _townRepository.RemoveAsync(town);
            return Ok();
        }

        [HttpPut("update")]
        [SwaggerOperation(OperationId = "Update", Summary = "Обновление города")]
        public async Task<IActionResult> Update([FromBody] UpdateTown request)
        {
            var town = await _townRepository.GetAsync(request.Id);
            if (town is null)
            {
                return NotFound($"Город с Id:{request.Id} не существует");
            }

            await _townRepository.UpdateAsync(town, request.Name);

            var townModel = _mapper.Map<TownModel>(town);
            
            return Ok(townModel);
        }
    }
}
