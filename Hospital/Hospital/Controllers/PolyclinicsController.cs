using AutoMapper;
using EnsureThat;
using Hospital.Db.Repositories.Entities;
using Hospital.Domain.Entities;
using Hospital.Domain.Models;
using Hospital.Domain.Requests.Polyclinic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hospital.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PolyclinicsController : ControllerBase
    {
        private readonly PolyclinicRepository _polyclinicsRepository;
        private readonly IMapper _mapper;

        public PolyclinicsController(
            PolyclinicRepository polyclinicsRepository,
            IMapper mapper)
        {
            EnsureArg.IsNotNull(polyclinicsRepository, nameof(polyclinicsRepository));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            _polyclinicsRepository = polyclinicsRepository;
            _mapper = mapper;
        }

        [HttpGet("get/{id}")]
        [SwaggerOperation(OperationId = "Get", Summary = "Получение поликлиники по Id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var polyclinic =
                await _polyclinicsRepository.GetAsync(id);
            if (polyclinic is null)
            {
                return NotFound($"Поликлиника с Id:{id} не существует");
            }

            var polyclinicModel = _mapper.Map<PolyclinicModel>( polyclinic);

            return Ok(polyclinicModel);
        }

        [HttpGet("get")]
        [SwaggerOperation(OperationId = "GetAll", Summary = "Получение всех поликлиник")]
        public async Task<IActionResult> GetAll()
        {
            var polyclinics =
                await _polyclinicsRepository.GetAllAsync();

            var polyclinicsModels = _mapper.Map<List<PolyclinicModel>>(polyclinics);

            return Ok(polyclinicsModels);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "Add", Summary = "Добавление поликлиники")]
        public async Task<IActionResult> Add([FromBody] AddPolyclinic request)
        {
            var polyclinic = new Polyclinic(
                request.Name,
                request.TownId, 
                request.Address, 
                request.ContactNumber,
                request.Photo);

            await _polyclinicsRepository.AddAsync(polyclinic);

            return Ok();
        }

        [HttpDelete("remove/{id}")]
        [SwaggerOperation(OperationId = "Remove", Summary = "Удаление поликлиники")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var polyclinic = await _polyclinicsRepository.GetAsync(id);
            if (polyclinic is null)
            {
                return NotFound($"Поликлиника с Id:{id} не существует");
            }

            await _polyclinicsRepository.RemoveAsync(polyclinic);
            return Ok();
        }

        [HttpPut("update")]
        [SwaggerOperation(OperationId = "Update", Summary = "Обновление поликлиники")]
        public async Task<IActionResult> Update([FromBody] UpdatePolyclinic request)
        {
            var polyclinic = await _polyclinicsRepository.GetAsync(request.Id);
            if (polyclinic is null)
            {
                return NotFound($"Поликлиника с Id:{request.Id} не существует");
            }

            await _polyclinicsRepository.UpdateAsync(polyclinic, 
                request.Name,
                request.TownId,
                request.Address,
                request.ContactNumber,
                request.Photo);

            var polyclinicModel = _mapper.Map<PolyclinicModel>(polyclinic);

            return Ok(polyclinicModel);
        }
    }
}
