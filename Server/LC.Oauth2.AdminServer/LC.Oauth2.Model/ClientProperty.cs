
namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 客户端属性
    /// </summary>
    public class ClientProperty : Property
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}