

namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 用户身份信息-抽象类
    /// </summary>
    public abstract class UserClaim
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}