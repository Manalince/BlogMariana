using MarianaDB.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianaDB.Infra
{
    class MeuCriadorDeBanco : DropCreateDatabaseIfModelChanges<ConexaoBanco>
    {
        protected override void Seed(ConexaoBanco context)
        {
            context.Usuarios.Add(new Usuario { Login = "ADM", Nome = "Administrador", Senha = "admin" });
            base.Seed(context);
        }
    }
}
