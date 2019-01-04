
namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 客户端秘钥
    /// </summary>
    public class ClientSecret : Secret
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}