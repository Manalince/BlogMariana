using MarianaDB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianaDB.Classes
{
    public class Visita : ClasseBase
    {
        public string ip { get; set; }
        public DateTime dataHora { get; set; }
        public int idPost { get; set; }

        public virtual Post Post { get; set; }
    }
}