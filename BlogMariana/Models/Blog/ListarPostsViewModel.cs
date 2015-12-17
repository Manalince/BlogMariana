using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarianaDB.Classes;

namespace BlogMariana.Models.Blog
{
    public class ListarPostsViewModel
    {
        public List<DetalharPostViewModel> Posts { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public string Tag { get; set; }
        public List<string> Tags { get; set; }
        public string Pesquisa { get; set; }
        public int QtdComentarios { get; set; }
    }
}