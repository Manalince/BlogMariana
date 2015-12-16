using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMariana.Models.Administracao
{
    public class CadastrarPostViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "O campo título é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage ="A quantidade de caracteres no campo título deve ser entre {2} e {1}.")]
        public string Titulo { get; set; }
        [DisplayName("Resumo")]
        [Required(ErrorMessage = "O campo resumo é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo resumo deve ser entre {2} e {1}.")]
        public string Resumo { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string Descricao { get; set; }
        [DisplayName("Autor")]
        [Required(ErrorMessage = "O campo autor é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo autor deve ser entre {2} e {1}.")]
        public string Autor { get; set; }
        [DisplayName("Data de Publicação")]
        [Required(ErrorMessage = "O campo data de publicação é obrigatório")]
        public DateTime DataPublicacao { get; set; }
        [DisplayName("Hora de Publicação")]
        [Required(ErrorMessage = "O campo hora de publicação é obrigatório")]
        public DateTime HoraPublicacao { get; set; }
        [DisplayName("Visível")]
        [Required(ErrorMessage = "O campo visível é obrigatório")]
        public bool Visivel { get; set; }

        public List<string> Tags { get; set; }
    }
}