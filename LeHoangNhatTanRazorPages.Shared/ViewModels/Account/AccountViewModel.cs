using System.ComponentModel.DataAnnotations;

namespace LeHoangNhatTanRazorPages.Shared.ViewModels.Account
{
    public class AccountViewModel
    {
        public short AccountId { get; set; }

        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên tài khoản không được vượt quá 100 ký tự")]
        [Display(Name = "Tên tài khoản")]
        public string? AccountName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [StringLength(70, ErrorMessage = "Email không được vượt quá 70 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string? AccountEmail { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        [Display(Name = "Vai trò")]
        public int? AccountRole { get; set; }

        // Account role as text
        public string RoleName => AccountRole switch
        {
            0 => "Admin",
            1 => "Nhân viên",
            2 => "Giảng viên",
            _ => "Không xác định"
        };

        // Sử dụng cho create/update
        [StringLength(70, ErrorMessage = "Mật khẩu không được vượt quá 70 ký tự")]
        [Display(Name = "Mật khẩu")]
        public string? AccountPassword { get; set; }

    }
}
