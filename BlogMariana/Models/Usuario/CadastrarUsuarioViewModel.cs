using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMariana.Models.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "A quantidade de caracteres no campo nome deve ser entre {2} e {1}.")]
        public string Nome { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "O campo login é obrigatório")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "A quantidade de caracteres no campo login deve ser entre {2} e {1}.")]
        public string Login { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Senha deve possuir no máximo {1}.")]
        public string Senha { get; set; }

        [DisplayName("Confirmar senha")]
        [Required(ErrorMessage = "O campo confirmar senha é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Senha deve possuir no máximo {1}.")]
        [Compare("Senha", ErrorMessage = "As senhas digitadas não conferem.")]
        public string ConfirmarSenha { get; set; }
    }
}