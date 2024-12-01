using EnsureThat;
using Hospital.Client;
using Hospital.Domain.Models;
using Hospital.Domain.Requests.Polyclinic;
using Hospital.Site.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital.Site.Pages
{
    public class PolyclinicsModel : PageModel
    {
        private readonly IHospitalCient _hospitalCient;

        public List<PolyclinicModel>? Polyclinics { get; set; }
        public List<TownModel>? Towns { get; set; }

        [BindProperty]
        public IFormFile? PhotoFormFile { get; set; }

        public PolyclinicsModel(
            IHospitalCient hospitalClient)
        {
            EnsureArg.IsNotNull(hospitalClient, nameof(hospitalClient));

            _hospitalCient = hospitalClient;
        }

        public async Task OnGetAsync()
        {
            Polyclinics = await _hospitalCient.Polyclinics();
            Towns = await _hospitalCient.Towns();
        }

        public async Task OnPostAddPolyclinicAsync(
            string addPolyclinicName,
            Guid addPolyclinicCity,
            string addPolyclinicAddress,
            string addPolyclinicContactNumber)
        {
            var photoFileBytes = 
                PhotoFormFile is null
                ? null
                : await PhotoFormFile.GetBytes();

            PhotoFormFile = null;

            var request = new AddPolyclinic(
                addPolyclinicName,
                addPolyclinicCity,
                addPolyclinicAddress,
                addPolyclinicContactNumber,
                photoFileBytes);

            await _hospitalCient.PolyclinicAdd(request);

            await OnGetAsync();
        }

        public async Task OnPostRemovePolyclinicAsync(
            Guid removePolyclinicId)
        {
            await _hospitalCient.PolyclinicRemove(removePolyclinicId);

            await OnGetAsync();
        }

        public async Task OnPostEditPolyclinicAsync(
            Guid editPolyclinicId,
            string editPolyclinicName,
            Guid editPolyclinicCityId,
            string editPolyclinicAddress,
            string editPolyclinicContactNumber)
        {
            var photoFileBytes =
               PhotoFormFile is null
               ? default
               : await PhotoFormFile.GetBytes();

            PhotoFormFile = null;

            var request = new UpdatePolyclinic(
                editPolyclinicId,
                editPolyclinicName,
                editPolyclinicCityId,
                editPolyclinicAddress,
                editPolyclinicContactNumber,
                photoFileBytes);

            await _hospitalCient.PolyclinicUpdate(request);
            await OnGetAsync();
        }
    }
}
