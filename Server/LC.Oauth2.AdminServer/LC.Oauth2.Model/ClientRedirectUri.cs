
namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 客户端回跳URL
    /// </summary>
    public class ClientRedirectUri
    {
        public int Id { get; set; }
        public string RedirectUri { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}