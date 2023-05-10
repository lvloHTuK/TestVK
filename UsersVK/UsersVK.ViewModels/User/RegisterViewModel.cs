using System.ComponentModel.DataAnnotations;

namespace UsersVK.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите Login")]
        [MinLength(5, ErrorMessage = "Минимальная длина должна составлять 5 символов")]
        [MaxLength(20, ErrorMessage = "Максимальная длина должна составлять 20 символов")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать как минимум 6 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "К какой группе принадлежит пользователь")]
        public string GroupCode { get; set; }
        public string GroupDescription { get; set; }
        public string StateDescription { get; set; }
    }
}
