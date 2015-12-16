using BlogMariana.Models.Usuario;
using MarianaDB;
using MarianaDB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMariana.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            var conexaoBanco = new ConexaoBanco();
            var usuarios = (from p in conexaoBanco.Usuarios
                            orderby p.Nome
                            select p).ToList();

            return View(usuarios);
        }

        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var usuario = new Usuario();
                usuario.Nome = viewModel.Nome;
                usuario.Login = viewModel.Login.ToUpper();
                usuario.Senha = viewModel.Senha;
                
                try
                {
                    var jaExiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Já existe usuário cadastrado com o login {0}.", usuario.Login));
                    }

                    conexao.Usuarios.Add(usuario);
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            
            return View(viewModel);
        }

        public ActionResult EditarUsuario(int id)
        {
            var conexao = new ConexaoBanco();
            var usuario = (from p in conexao.Usuarios
                           where p.id == id
                           select p).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Não achou usuário com código {0}.", id));
            }
            var viewModel = new CadastrarUsuarioViewModel();
            viewModel.Id = usuario.id;
            viewModel.Nome = usuario.Nome;
            viewModel.Login = usuario.Login;
            viewModel.Senha = usuario.Senha;
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var usuario = (from p in conexao.Usuarios
                               where p.id == viewModel.Id
                               select p).FirstOrDefault();
                if (usuario == null)
                {
                    throw new Exception(string.Format("Não achou usuário com código {0}.", viewModel.Id));
                }
                usuario.Nome = viewModel.Nome;
                usuario.Login = viewModel.Login.ToUpper();
                usuario.Senha = viewModel.Senha;

                try
                {
                    var jaExiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                       && p.id != usuario.id
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Já existe usuário cadastrado com o login {0}.", usuario.Login));
                    }

                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            return View(viewModel);
        }
        #region ExcluirUsuario
        public ActionResult ExcluirUsuario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var usuario = (from p in conexaoBanco.Usuarios
                        where p.id == id
                        select p).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuario com código {0} não existe.", id));
            }
            conexaoBanco.Usuarios.Remove(usuario);
            conexaoBanco.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion     
    }
}