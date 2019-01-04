
namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 客户端使用范围
    /// </summary>
    public class ClientScope
    {
        public int Id { get; set; }
        public string Scope { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}