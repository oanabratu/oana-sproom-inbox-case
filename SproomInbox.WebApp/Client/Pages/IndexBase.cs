using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using SproomInbox.Shared;
using System.Net.Http.Json;

namespace SproomInbox.WebApp.Client.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        public IList<DocumentModel>? documents;
        public IList<UserModel>? users;

        public string? operationMessage;

        public EditContext? EditContext;
        public IndexFilter filter = new IndexFilter();


        protected override async Task OnInitializedAsync()
        {
            documents = await Http.GetFromJsonAsync<IList<DocumentModel>>("Document");
            users = await Http.GetFromJsonAsync<IList<UserModel>>("User");

            EditContext = new EditContext(filter);
            EditContext.OnFieldChanged += async (sender,args) => await EditContext_OnFieldChanged(sender, args);
        }

        private async Task EditContext_OnFieldChanged(object sender, FieldChangedEventArgs args)
        {
            await ReloadDocuments();
            StateHasChanged();  
        }

        protected async Task ReloadDocuments()
        {
            const string url = "Document";

            var param = new Dictionary<string, string>();
            
            if (filter != null)
            {
                if (filter.Username != null)
                    param.Add("username", filter.Username);


                if(filter.DocumentState != null)
                {
                    int documentState = (int)filter.DocumentState;
                    param.Add("state", documentState.ToString());
                }

                if(filter.DocumentType != null)
                {
                    int documentType = (int)filter.DocumentType;
                    param.Add("type", documentType.ToString());
                }
            }

            var requestUri = QueryHelpers.AddQueryString(url, param);

            documents = await Http.GetFromJsonAsync<IList<DocumentModel>>(requestUri);
        }



        protected async Task ApproveDocuments()
        {
            const string url = "Document/approveAll";

            var requestBody = new ApproveAllDocumentsParams
            {
                DocumentIds = new List<Guid>()
            };


            foreach (var document in documents)
            {
                if(document.Selected == true)
                {
                    requestBody.DocumentIds.Add(document.Id);
                }

            }
            requestBody.Username = filter.Username;

            HttpResponseMessage responseMessage = Http.PutAsJsonAsync(url,  requestBody).Result;
            var operationResults = await responseMessage.Content.ReadFromJsonAsync<IList<OperationResultModel>>();
            operationMessage = operationResults.ToString();
        }


        protected async Task RejectDocuments()
        {
            const string url = "Document/rejectAll";

            var requestBody = new RejectAllDocumentsParams();

            foreach (var document in documents)
            {
                if (document.Selected == true)
                {
                    requestBody.DocumentIds.Add(document.Id);
                }
            }
            requestBody.Username = filter.Username;

            HttpResponseMessage responseMessage = await Http.PutAsJsonAsync(url, requestBody);
            var operationResults = await responseMessage.Content.ReadFromJsonAsync<List<OperationResultModel>>();
            operationMessage = operationResults.ToString();
        }

    }

    public class IndexFilter
    {
        public string? Username { get; set; }
        public DocumentTypeModel? DocumentType { get; set; }
        public StateModel? DocumentState { get; set; }
    }
}
