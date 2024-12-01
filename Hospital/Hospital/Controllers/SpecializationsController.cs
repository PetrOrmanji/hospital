using AutoMapper;
using EnsureThat;
using Hospital.Db.Repositories.Entities;
using Hospital.Domain.Models;
using Hospital.Domain.Requests.Specialization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hospital.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly SpecializationRepository _specializationRepository;
        private readonly IMapper _mapper;

        public SpecializationsController(
            SpecializationRepository specializationRepository,
            IMapper mapper)
        {
            EnsureArg.IsNotNull(specializationRepository, nameof(specializationRepository));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }

        [HttpGet("get/{id}")]
        [SwaggerOperation(OperationId = "Get", Summary = "Получение специализации по Id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var specialization =
                await _specializationRepository.GetAsync(id);
            if (specialization is null)
            {
                return NotFound($"Специализация с Id:{id} не существует");
            }

            var specializationModel = _mapper.Map<SpecializationModel>(specialization);

            return Ok(specializationModel);
        }

        [HttpGet("get")]
        [SwaggerOperation(OperationId = "GetAll", Summary = "Получение всех специализаций")]
        public async Task<IActionResult> GetAll()
        {
            var specializations =
                await _specializationRepository.GetAllAsync();

            var specializationsModels = _mapper.Map<List<SpecializationModel>>(specializations);

            return Ok(specializationsModels);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "Add", Summary = "Добавление специализации")]
        public async Task<IActionResult> Add([FromBody] AddSpecialization request)
        {
            await _specializationRepository.AddAsync(request.Name);
            return Ok();
        }

        [HttpDelete("remove/{id}")]
        [SwaggerOperation(OperationId = "Remove", Summary = "Удаление специализации")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var specialization = await _specializationRepository.GetAsync(id);
            if (specialization is null)
            {
                return NotFound($"Специализация с Id:{id} не существует");
            }

            await _specializationRepository.RemoveAsync(specialization);
            return Ok();
        }

        [HttpPut("update")]
        [SwaggerOperation(OperationId = "Update", Summary = "Обновление специализации")]
        public async Task<IActionResult> Update([FromBody] UpdateSpecialization request)
        {
            var specialization = await _specializationRepository.GetAsync(request.Id);
            if (specialization is null)
            {
                return NotFound($"Специализация с Id:{request.Id} не существует");
            }

            await _specializationRepository.UpdateAsync(specialization, request.Name);

            var specializationModel = _mapper.Map<SpecializationModel>(specialization);

            return Ok(specializationModel);
        }
    }
}
