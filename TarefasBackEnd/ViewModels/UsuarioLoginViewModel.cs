using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.ViewModel
{
    public class UsuarioLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
