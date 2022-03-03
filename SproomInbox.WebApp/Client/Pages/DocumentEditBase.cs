using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using SproomInbox.Shared;
using System.Net.Http.Json;

namespace SproomInbox.WebApp.Client.Pages
{
    public class DocumentEditBase : ComponentBase
    {
        // This object is injected by Depenency Injection container
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public DocumentModel? document = new DocumentModel();
        public IList<UserModel>? users;
        public EditContext? EditContext;

        public string Message="";
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            users = await Http.GetFromJsonAsync<IList<UserModel>>("User");
            Saved = false;
        }

        protected async Task  HandleValidSubmit() 
        {

            HttpResponseMessage responseMessage = await Http.PostAsJsonAsync("Document", document);
            if (responseMessage.IsSuccessStatusCode)
            {
                var createdDocument = await responseMessage.Content.ReadFromJsonAsync<DocumentModel>();

                if(createdDocument != null)
                {
                    // show the success message
                    Message = $"The document {createdDocument.Id} has been created.";
                    Saved = true;
                }
                else
                {
                    // TODO - what if the document was not created?
                }
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }
    }


}
