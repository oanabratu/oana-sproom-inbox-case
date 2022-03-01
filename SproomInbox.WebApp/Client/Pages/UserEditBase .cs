using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using SproomInbox.Shared;
using System.Net.Http.Json;

namespace SproomInbox.WebApp.Client.Pages
{
    public class UserEditBase : ComponentBase
    {
        // This object is injected by Depenency Injection container
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public UserModel? user = new UserModel();
        public EditContext? EditContext;

        public string Message="";
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            Saved = false;
        }

        protected async Task  HandleValidSubmit() 
        {

            HttpResponseMessage responseMessage = await Http.PostAsJsonAsync("User", user);
            if (responseMessage.IsSuccessStatusCode)
            {
                // show the success message
                Message = $"The user {user.Username} has been created.";
                Saved = true;
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }
    }


}
