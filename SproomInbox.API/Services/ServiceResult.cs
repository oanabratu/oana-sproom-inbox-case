namespace SproomInbox.API.Services
{
    public class ServiceResult<T>
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}
