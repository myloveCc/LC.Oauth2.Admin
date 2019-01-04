
namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 客户端授权类型
    /// </summary>
    public class ClientGrantType
    {
        public int Id { get; set; }
        public string GrantType { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}