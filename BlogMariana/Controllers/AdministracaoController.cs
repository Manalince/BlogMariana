using BlogMariana.Models.Administracao;
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
    public class AdministracaoController : Controller
    {

        // GET: Administracao        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CadastrarPost()
        {
            var viewModel = new CadastrarPostViewModel();
            viewModel.DataPublicacao = DateTime.Now;
            viewModel.HoraPublicacao = DateTime.Now;
            viewModel.Visivel = true;            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexaoBanco = new ConexaoBanco();
                var post = new Post();
                post.dataPublicacao = new DateTime(viewModel.DataPublicacao.Year,
                    viewModel.DataPublicacao.Month, viewModel.DataPublicacao.Day,
                    viewModel.HoraPublicacao.Hour, viewModel.HoraPublicacao.Minute, 0);
                post.Autor = viewModel.Autor;
                post.Descricao = viewModel.Descricao;
                post.Resumo = viewModel.Resumo;
                post.Titulo = viewModel.Titulo;
                post.visivel = viewModel.Visivel;
                post.TagPosts = new List<TagPost>();
                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexaoBanco.TagClasses
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();
                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexaoBanco.TagClasses.Add(tagClass);
                        }
                        var tagPost = new TagPost();
                        tagPost.idTagClass = item;
                        post.TagPosts.Add(tagPost);
                    }
                }
                try
                {
                    conexaoBanco.Posts.Add(post);
                    conexaoBanco.SaveChanges();
                    return RedirectToAction("Index", "Blog");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            return View(viewModel);
        }
        #region Editar
        public ActionResult EditarPost(int id)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts
                        where p.id == id
                        select p).FirstOrDefault();

            var viewModel = new CadastrarPostViewModel();            
            viewModel.DataPublicacao = post.dataPublicacao;
            viewModel.HoraPublicacao = post.dataPublicacao;
            viewModel.Autor = post.Autor;
            viewModel.Descricao = post.Descricao;
            viewModel.Resumo = post.Resumo;
            viewModel.Titulo = post.Titulo;
            viewModel.Visivel = post.visivel;
            viewModel.Id = post.id;
            viewModel.Tags = (from p in post.TagPosts
                              select p.idTagClass).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexaoBanco = new ConexaoBanco();
                var post = conexaoBanco.Posts.FirstOrDefault(x => x.id == viewModel.Id);                            
                if (post == null)
                {
                    throw new Exception(string.Format("Post código {0} não existe.", viewModel.Id));
                }
                post.dataPublicacao = new DateTime(viewModel.DataPublicacao.Year,
                    viewModel.DataPublicacao.Month, viewModel.DataPublicacao.Day,
                    viewModel.HoraPublicacao.Hour, viewModel.HoraPublicacao.Minute, 0);
                post.Autor = viewModel.Autor;
                post.Descricao = viewModel.Descricao;
                post.Resumo = viewModel.Resumo;
                post.Titulo = viewModel.Titulo;
                post.visivel = viewModel.Visivel;
                var postsTagsAtuais = post.TagPosts.ToList();
                foreach (var item in postsTagsAtuais)
                {
                    conexaoBanco.TagPosts.Remove(item);
                }
                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexaoBanco.TagClasses
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();
                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexaoBanco.TagClasses.Add(tagClass);
                        }
                        var tagPost = new TagPost();
                        tagPost.idTagClass = item;
                        post.TagPosts.Add(tagPost);
                    }
                }
                try
                {
                    conexaoBanco.SaveChanges();
                    return RedirectToAction("Index", "Blog");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }                
            }
            return View(viewModel);
        }
        #endregion

        #region EcluirPost
        public ActionResult ExcluirPost(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var post = (from p in conexaoBanco.Posts
                        where p.id == id
                        select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não existe.", id));
            }
            conexaoBanco.Posts.Remove(post);
            conexaoBanco.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }
        #endregion

        #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                              where p.id == id
                              select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Posts
                        where p.id == comentario.idPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.dataPublicacao.Year,
                mes = post.dataPublicacao.Month,
                dia = post.dataPublicacao.Day,
                titulo = post.Titulo,
                id = post.id
            }) + "#comentarios");
        }
        #endregion
    }
}