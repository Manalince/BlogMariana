using MarianaDB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianaDB.Classes
{
    public class Download : ClasseBase
    {
        public string ip { get; set; }
        public DateTime dataHora { get; set; }
        public int idArquivo { get; set; }

        public virtual Arquivo Arquivo { get; set; }
    }
}