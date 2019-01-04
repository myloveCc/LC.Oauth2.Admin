
namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 客户端退出URL
    /// </summary>
    public class ClientPostLogoutRedirectUri
    {
        public int Id { get; set; }
        public string PostLogoutRedirectUri { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}