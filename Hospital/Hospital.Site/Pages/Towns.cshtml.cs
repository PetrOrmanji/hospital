using EnsureThat;
using Hospital.Client;
using Hospital.Domain.Models;
using Hospital.Domain.Requests.Town;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital.Site.Pages
{
    public class TownsModel : PageModel
    {
        private IHospitalCient _hospitalClient;

        public List<TownModel>? Towns { get; set; }

        public TownsModel(
            IHospitalCient hospitalClient)
        {
            EnsureArg.IsNotNull(hospitalClient, nameof(hospitalClient));

            _hospitalClient = hospitalClient;
        }

        public async Task OnGetAsync()
        {
            Towns = await _hospitalClient.Towns();
        }

        public async Task OnPostAddTownAsync(string townName)
        {
            var request = new AddTown(townName);

            await _hospitalClient.TownAdd(request);

            await OnGetAsync();
        }

        public async Task OnPostEditTownAsync(Guid editTownId, string editTownName)
        {
            var request = new UpdateTown(editTownId, editTownName);

            await _hospitalClient.TownUpdate(request);

            await OnGetAsync();
        }

        public async Task OnPostRemoveTownAsync(Guid townId)
        {
            await _hospitalClient.TownRemove(townId);

            await OnGetAsync();
        }
    }
}
