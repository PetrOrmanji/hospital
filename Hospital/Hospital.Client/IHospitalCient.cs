using Hospital.Domain.Models;
using Hospital.Domain.Requests.Polyclinic;
using Hospital.Domain.Requests.Role;
using Hospital.Domain.Requests.Specialization;
using Hospital.Domain.Requests.Town;
using Refit;

namespace Hospital.Client;

public interface IHospitalCient
{
    #region Towns

    [Get("/towns/get/{id}")]
    Task<TownModel> TownById(Guid id);

    [Get("/towns/get")]
    Task<List<TownModel>> Towns();

    [Post("/towns/add")]
    Task TownAdd([Body] AddTown request);

    [Delete("/towns/remove/{id}")]
    Task TownRemove(Guid id);

    [Put("/towns/update")]
    Task TownUpdate([Body] UpdateTown request);

    #endregion

    #region Polyclinics

    [Get("/polyclinics/get/{id}")]
    Task<PolyclinicModel> PolyclinicById(Guid id);

    [Get("/polyclinics/get")]
    Task<List<PolyclinicModel>> Polyclinics();

    [Post("/polyclinics/add")]
    Task PolyclinicAdd([Body] AddPolyclinic request);

    [Delete("/polyclinics/remove/{id}")]
    Task PolyclinicRemove(Guid id);

    [Put("/polyclinics/update")]
    Task PolyclinicUpdate([Body] UpdatePolyclinic request);

    #endregion

    #region Specializations

    [Get("/specializations/get/{id}")]
    Task<SpecializationModel> SpecializationById(Guid id);

    [Get("/specializations/get")]
    Task<List<SpecializationModel>> Specializations();

    [Post("/specializations/add")]
    Task SpecializationAdd([Body] AddSpecialization request);

    [Delete("/specializations/remove/{id}")]
    Task SpecializationRemove(Guid id);

    [Put("/specializations/update")]
    Task SpecializationUpdate([Body] UpdateSpecialization request);

    #endregion

    #region Role

    [Get("/roles/get/{id}")]
    Task<RoleModel> RoleById(Guid id);

    [Get("/roles/get")]
    Task<List<RoleModel>> Roles();

    [Post("/roles/add")]
    Task RoleAdd([Body] AddRole request);

    [Delete("/roles/remove/{id}")]
    Task RoleRemove(Guid id);

    [Put("/roles/update")]
    Task RoleUpdate([Body] UpdateRole request);

    #endregion
}
