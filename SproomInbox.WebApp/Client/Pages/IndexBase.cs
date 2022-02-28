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

        public IndexFilter filter = new IndexFilter();


        protected override async Task OnInitializedAsync()
        {
            documents = await Http.GetFromJsonAsync<IList<DocumentModel>>("Document");
            users = await Http.GetFromJsonAsync<IList<UserModel>>("User");
        }

        protected void ChangeDocState(StateModel? value)
        {
            filter.DocumentState = value;

            ReloadDocuments().Wait();
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
    }

    public class IndexFilter
    {
        public string? Username { get; set; }
        public DocumentTypeModel? DocumentType { get; set; }
        public StateModel? DocumentState { get; set; }
    }
}
