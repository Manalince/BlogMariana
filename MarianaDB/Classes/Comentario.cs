using MarianaDB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianaDB.Classes
{
    public class Comentario : ClasseBase
    {
        public string Descricao { get; set; }        
        public string email { get; set; }
        public string PaginaWEB { get; set; }
        public string Nome { get; set; }
        public bool admPost { get; set; }
        public int idPost { get; set; }

        public virtual Post Post { get; set; }
    }
}