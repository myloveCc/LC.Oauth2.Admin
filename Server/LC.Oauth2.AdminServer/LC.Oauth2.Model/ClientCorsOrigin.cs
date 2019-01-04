
namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 客户端跨域信息
    /// </summary>
    public class ClientCorsOrigin
    {
        public int Id { get; set; }
        /// <summary>
        /// 跨域域名
        /// </summary>
        public string Origin { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}