using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LC.Oauth2.Entities
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table("tb_SysUser", Schema = "dbo")]
    public class SysUser
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 是否锁定，锁定后不能进行登陆
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 新密码
        /// </summary>
        [NotMapped]
        public string NewPassword { get; set; }
    }
}
