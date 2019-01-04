
namespace LC.Oauth2.Entities
{
    /// <summary>
    /// Api秘钥
    /// </summary>
    public class ApiSecret : Secret
    {
        public int ApiResourceId { get; set; }
        public ApiResource ApiResource { get; set; }
    }
}