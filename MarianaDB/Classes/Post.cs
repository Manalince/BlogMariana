using MarianaDB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianaDB.Classes
{
    public class Post : ClasseBase
    {
        public string Autor { get; set; }
        public DateTime dataPublicacao { get; set; }       
        public string Descricao { get; set; }
        public string Resumo { get; set; }
        public string Titulo { get; set; }
        public bool visivel { get; set; }

        //exemplo vários comentarios
        public virtual IList<Comentario> Comentarios { get; set; }        
        public virtual IList<Arquivo> Arquivos { get; set; }
        public virtual IList<Imagem> Imagens { get; set; }
        public virtual IList<Visita> Visitas { get; set; }
        public virtual IList<TagPost> TagPosts { get; set; }
    }
}