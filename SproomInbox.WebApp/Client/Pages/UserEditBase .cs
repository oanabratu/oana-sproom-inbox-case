using Microsoft.AspNetCore.Components;
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

        public string Message="";
        protected bool UserCreatedSuccessfully;
        protected bool ShowSaveResultMessage;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            UserCreatedSuccessfully = false;
            ShowSaveResultMessage = false; 
        }

        protected async Task  HandleValidSubmit() 
        {

            HttpResponseMessage responseMessage = await Http.PostAsJsonAsync("User", user);
            if (responseMessage.IsSuccessStatusCode)
            {
                var createUserResult = await responseMessage.Content.ReadFromJsonAsync<CreateUserResultModel>();
                if (createUserResult != null)
                {
                    if(createUserResult.Success)
                    {
                        // show the success message
                        Message = $"The user {user?.Username} has been created.";
                        UserCreatedSuccessfully = true;
                    }
                    else
                    {
                        Message = createUserResult.ErrorMessage ?? string.Empty;
                        UserCreatedSuccessfully = false;
                    }
                }
                else
                {
                    Message = $"Unable to create {user?.Username}.";
                    UserCreatedSuccessfully = false;
                }
            }
            else
            {
                // show the unsuccess message
                Message = $"Unable to create {user?.Username}.";
                UserCreatedSuccessfully = false;
            }

            ShowSaveResultMessage = true;
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }
    }


}
