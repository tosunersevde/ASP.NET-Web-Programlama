using System.ComponentModel.DataAnnotations; 

namespace Moqups.Models
{
    public class LoginModel
    {

        private string? _returnurl; //giris yapilmadiysa login'e yonlendirme icin

        [Required(ErrorMessage = "UserName is required.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        public string ReturnUrl //Full prop tanimi
        {
            get
            {
                if (_returnurl is null) //ifade bossa anasayfaya gitsin.
                    return "/";
                else
                    return _returnurl;
            }
            set
            {
                _returnurl = value;
            }
        }
    }
}
