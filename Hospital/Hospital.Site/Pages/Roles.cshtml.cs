using EnsureThat;
using Hospital.Client;
using Hospital.Domain.Models;
using Hospital.Domain.Requests.Role;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital.Site.Pages
{
    public class RolesModel : PageModel
    {
        private IHospitalCient _hospitalClient;

        public List<RoleModel>? Roles { get; set; }

        public RolesModel(
            IHospitalCient hospitalClient)
        {
            EnsureArg.IsNotNull(hospitalClient, nameof(hospitalClient));

            _hospitalClient = hospitalClient;
        }

        public async Task OnGetAsync()
        {
            Roles = await _hospitalClient.Roles();
        }

        public async Task OnPostAddRoleAsync(string roleName)
        {
            var request = new AddRole(roleName);

            await _hospitalClient.RoleAdd(request);

            await OnGetAsync();
        }

        public async Task OnPostEditTownAsync(Guid editRoleId, string editRoleName)
        {
            var request = new UpdateRole(editRoleId, editRoleName);

            await _hospitalClient.RoleUpdate(request);

            await OnGetAsync();
        }

        public async Task OnPostRemoveRoleAsync(Guid roleId)
        {
            await _hospitalClient.RoleRemove(roleId);

            await OnGetAsync();
        }
    }
}
