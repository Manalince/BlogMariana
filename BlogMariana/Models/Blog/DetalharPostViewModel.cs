using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarianaDB.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogMariana.Models.Blog
{
    public class DetalharPostViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Autor { get; set; }
        public string Resumo { get; set; }
        public bool Visivel { get; set; }
        public int QtdComentarios { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime HoraPublicacao { get; set; }
        public List<TagClass> Tags { get; set; }

        //Cadastrar comentario
        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = "O campo Nome deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string ComentarioNome { get; set; }
        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O campo E-mail deve possuir no máximo {1} caracteres!")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string ComentarioEmail { get; set; }
        [DisplayName("Descrição")]        
        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string ComentarioDescricao { get; set; }
        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage = "O campo Página Web deve possuir no máximo {1} caracteres!")]
        public string ComentarioPaginaWeb { get; set; }
    }
}