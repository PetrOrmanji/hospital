using EnsureThat;
using Hospital.Db.Repositories.Entities;
using Hospital.Domain.Requests.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly SpecializationRepository _specializationRepository;
        private readonly PolyclinicRepository _polyclinicRepository;

        public DoctorsController(
            DoctorRepository doctorRepository,
            SpecializationRepository specializationRepository,
            PolyclinicRepository polyclinicRepository)
        {
            EnsureArg.IsNotNull(doctorRepository, nameof(doctorRepository));
            EnsureArg.IsNotNull(specializationRepository, nameof(specializationRepository));
            EnsureArg.IsNotNull(polyclinicRepository, nameof(polyclinicRepository));

            _doctorRepository = doctorRepository;
            _specializationRepository = specializationRepository;
            _polyclinicRepository = polyclinicRepository;
        }

        [HttpGet("get/{id}")]
        [SwaggerOperation(OperationId = "Get", Summary = "Получение доктора по Id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var doctor =
                await _doctorRepository.GetAsync(id);
            if (doctor is null)
            {
                return NotFound($"Доктор с Id:{id} не существует");
            }

            return Ok(doctor);
        }

        [HttpGet("get")]
        [SwaggerOperation(OperationId = "GetAll", Summary = "Получение всех докторов")]
        public async Task<IActionResult> GetAll()
        {
            var doctors =
                await _doctorRepository.GetAllAsync();

            return Ok(doctors);
        }

        [HttpPost("add")]
        [SwaggerOperation(OperationId = "Add", Summary = "Добавление доктора")]
        public async Task<IActionResult> Add([FromBody] AddDoctor request)
        {
            await _doctorRepository.AddAsync(
                request.FullName,
                request.ContactNumber,
                request.Photo,
                request.ShortDescription,
                request.FullDescription);

            return Ok();
        }

        [HttpDelete("remove/{id}")]
        [SwaggerOperation(OperationId = "Remove", Summary = "Удаление доктора")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            if (doctor is null)
            {
                return NotFound($"Доктор с Id:{id} не существует");
            }

            await _doctorRepository.RemoveAsync(doctor);
            return Ok();
        }

        [HttpPut("update")]
        [SwaggerOperation(OperationId = "Update", Summary = "Обновление доктора")]
        public async Task<IActionResult> Update([FromBody] UpdateDoctor request)
        {
            var doctor = await _doctorRepository.GetAsync(request.Id);
            if (doctor is null)
            {
                return NotFound($"Доктор с Id:{request.Id} не существует");
            }

            await _doctorRepository.UpdateAsync(
                doctor, 
                doctor.FullName,
                doctor.ContactNumber,
                doctor.Photo,
                doctor.ShortDescription,
                doctor.FullDescription);

            return Ok(doctor);
        }

        [HttpPost("addDoctorSpecialization")]
        [SwaggerOperation(OperationId = "AddDoctorSpecialization", Summary = "Добавление специализации доктора")]
        public async Task<IActionResult> AddDoctorSpecialization([FromBody] AddDoctorSpecialization request)
        {
            var doctor = await _doctorRepository.GetAsync(request.DoctorId);
            if (doctor is null)
            {
                return NotFound($"Доктор с Id:{request.DoctorId} не существует");
            }

            var specialization = await _specializationRepository.GetAsync(request.SpecializationId);
            if (specialization is null)
            {
                return NotFound($"Специализация с Id:{request.DoctorId} не существует");
            }

            await _doctorRepository.AddDoctorSpecialization(
                doctor,
                specialization,
                request.Experience);

            return Ok();
        }

        [HttpDelete("removeDoctorSpecialization")]
        [SwaggerOperation(OperationId = "RemoveDoctorSpecialization", Summary = "Удаление специализации доктора")]
        public async Task<IActionResult> RemoveDoctorSpecialization([FromBody] RemoveDoctorSpecialization request)
        {
            await _doctorRepository.RemoveDoctorSpecialization(request.Id);
            return Ok();
        }

        [HttpPost("addDoctorPolyclinic")]
        [SwaggerOperation(OperationId = "AddDoctorPolyclinic", Summary = "Добавление поликлиники доктора")]
        public async Task<IActionResult> AddDoctorPolyclinic([FromBody] AddDoctorPolyclinic request)
        {
            var doctor = await _doctorRepository.GetAsync(request.DoctorId);
            if (doctor is null)
            {
                return NotFound($"Доктор с Id:{request.DoctorId} не существует");
            }

            var polyclinic = await _polyclinicRepository.GetAsync(request.PolyclinicId);
            if (polyclinic is null)
            {
                return NotFound($"Поликлиника с Id:{request.DoctorId} не существует");
            }

            await _doctorRepository.AddDoctorPolyclinic(
                doctor,
                polyclinic,
                request.Cost);

            return Ok();
        }

        [HttpDelete("removeDoctorPolyclinic")]
        [SwaggerOperation(OperationId = "RemoveDoctorPolyclinic", Summary = "Удаление поликлиники доктора")]
        public async Task<IActionResult> RemoveDoctorPolyclinic([FromBody] RemoveDoctorPolyclinic request)
        {
            await _doctorRepository.RemoveDoctorPolyclinic(request.Id);
            return Ok();
        }
    }
}
