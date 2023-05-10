using System.ComponentModel.DataAnnotations;

namespace UsersVK.ViewModels.User
{
    public class DeleteViewModel
    {
        [Required(ErrorMessage = "Введите Login")]
        [MinLength(5, ErrorMessage = "Минимальная длина должна составлять 5 символов")]
        [MaxLength(20, ErrorMessage = "Максимальная длина должна составлять 20 символов")]
        public string Login { get; set; }
    }
}
