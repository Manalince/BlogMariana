using MarianaDB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianaDB.Classes
{
    public class TagPost : ClasseBase
    {
        public string idTagClass { get; set; }
        public int idPost { get; set; }

        public virtual TagClass TagClass { get; set; }
        public virtual Post Post { get; set; }
    }
}