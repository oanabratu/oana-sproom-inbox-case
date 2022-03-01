using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using SproomInbox.Shared;
using System.Net.Http.Json;

namespace SproomInbox.WebApp.Client.Pages
{
    public class AllUsersBase : ComponentBase
    {
        // This object is injected by Depenency Injection container
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IList<UserModel>? users = new List<UserModel>();

        protected override async Task OnInitializedAsync()
        {
            //operationMessage.Clear();
            users = await Http.GetFromJsonAsync<IList<UserModel>>("User");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }
    }


}
