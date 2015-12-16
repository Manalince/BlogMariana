using MarianaDB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianaDB.Classes
{
    public class Usuario:ClasseBase
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
