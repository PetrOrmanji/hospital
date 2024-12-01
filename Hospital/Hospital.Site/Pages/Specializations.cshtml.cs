using EnsureThat;
using Hospital.Client;
using Hospital.Domain.Models;
using Hospital.Domain.Requests.Specialization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital.Site.Pages
{
    public class SpecializationsModel : PageModel
    {
        private IHospitalCient _hospitalClient;

        public List<SpecializationModel>? Specializations { get; set; }

        public SpecializationsModel(
            IHospitalCient hospitalClient)
        {
            EnsureArg.IsNotNull(hospitalClient, nameof(hospitalClient));

            _hospitalClient = hospitalClient;
        }

        public async Task OnGetAsync()
        {
            Specializations = await _hospitalClient.Specializations();
        }

        public async Task OnPostAddSpecializationAsync(string specializationName)
        {
            var request = new AddSpecialization(specializationName);

            await _hospitalClient.SpecializationAdd(request);

            await OnGetAsync();
        }

        public async Task OnPostEditSpecializationAsync(Guid editSpecializationId, string editSpecializationName)
        {
            var request = new UpdateSpecialization(editSpecializationId, editSpecializationName);

            await _hospitalClient.SpecializationUpdate(request);

            await OnGetAsync();
        }

        public async Task OnPostRemoveSpecializationAsync(Guid specializationId)
        {
            await _hospitalClient.SpecializationRemove(specializationId);

            await OnGetAsync();
        }
    }
}
