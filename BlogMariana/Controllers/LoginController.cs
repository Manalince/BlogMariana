using BlogMariana.Models.Login;
using MarianaDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogMariana.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        #region Entrar
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index (LoginViewModel viewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var conxaoBanco = new ConexaoBanco();
            var usuario = (from p in conxaoBanco.Usuarios
                           where p.Login.ToUpper() == viewModel.Login.ToUpper()
                           && p.Senha == viewModel.Senha
                           select p).FirstOrDefault();
            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário e/ou senha estão incorretos.");
                return View(viewModel);              
            }
            FormsAuthentication.SetAuthCookie(usuario.Login, viewModel.Lembrar);
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Blog");
        }
        #endregion
        #region Sair
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        #endregion
    }
}