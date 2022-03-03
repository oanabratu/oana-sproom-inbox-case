namespace SproomInbox.API.Services
{
    /// <summary>
    /// Error Handling Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T>
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}
