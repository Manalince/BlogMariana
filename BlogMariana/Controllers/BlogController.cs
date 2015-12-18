using BlogMariana.Models.Blog;
using MarianaDB;
using MarianaDB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMariana.Controllers
{
    public class BlogController : Controller
    {
        public ActionResult Index(int? pagina, string tag, string pesquisa)
        {
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registrosPorPagina = 10;

            var conexaoBanco = new ConexaoBanco();
            var posts = (from p in conexaoBanco.Posts
                                where p.visivel == true                                
                                select p);
            if (!string.IsNullOrEmpty(tag))
            {
                posts = (from p in posts
                         where p.TagPosts.Any(x => x.idTagClass.ToUpper() == tag.ToUpper())
                         select p);
            }
            if (!string.IsNullOrEmpty(pesquisa))
            {
                posts = (from p in posts
                         where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                         || p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                         || p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                         select p);
            }
            var qtdRegistros= posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdRegistrosPular = (indiceDaPagina * registrosPorPagina);
            var qtdPaginas = Math.Ceiling((decimal)qtdRegistros / registrosPorPagina);

            var viewModel = new ListarPostsViewModel();
            viewModel.Posts = (from p in posts
                               orderby p.dataPublicacao descending
                               select new DetalharPostViewModel
                               {
                                   DataPublicacao = p.dataPublicacao,
                                   Autor = p.Autor,
                                   Descricao = p.Descricao,
                                   Id = p.id,
                                   Resumo = p.Resumo,
                                   Titulo = p.Titulo,
                                   Visivel = p.visivel,
                                   QtdComentarios = p.Comentarios.Count
                               }).Skip(qtdRegistrosPular).Take(registrosPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtdPaginas;
            viewModel.Tag = tag;
            viewModel.Tags = (from p in conexaoBanco.TagClasses
                              where conexaoBanco.TagPosts.Any(x => x.idTagClass == p.Tag)
                              orderby p.Tag
                              select p.Tag).ToList();
            viewModel.Pesquisa = pesquisa;
            return View(viewModel);
        }
        #region Paginacao
        public ActionResult _Paginacao()
        {
            return PartialView();
        }
        #endregion
        public ActionResult Post(int id, int? pagina)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts
                        where p.id == id
                        select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não encontrado", id));
            }
            var viewModel = new DetalharPostViewModel();
            preencherViewModel(post, pagina, viewModel);

            return View(viewModel);
        }

        private static void preencherViewModel(Post post, int? pagina, DetalharPostViewModel viewModel)
        {
            viewModel.Id = post.id;
            viewModel.Titulo = post.Titulo;
            viewModel.Autor = post.Autor;
            viewModel.DataPublicacao = post.dataPublicacao;
            viewModel.HoraPublicacao = post.dataPublicacao;
            viewModel.Descricao = post.Descricao;
            viewModel.Visivel = post.visivel;
            viewModel.QtdComentarios = post.Comentarios.Count;
            viewModel.Tags = post.TagPosts.Select(x => x.TagClass).ToList();
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registrosPorPagina = 5;
            var qtdRegistros = post.Comentarios.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdRegistrosPular = (indiceDaPagina * registrosPorPagina);
            var qtdPaginas = Math.Ceiling((decimal)qtdRegistros / registrosPorPagina);
            viewModel.Comentarios = (from p in post.Comentarios
                                     orderby p.DataHora descending
                                     select p).Skip(qtdRegistrosPular).Take(registrosPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPagina = (int)qtdPaginas;
        }
        #region Comentario
        [HttpPost]
        public ActionResult Post(DetalharPostViewModel viewModel)
        {
            var conexaoBanco = new ConexaoBanco();
            var post = (from p in conexaoBanco.Posts
                        where p.id == viewModel.Id
                        select p).FirstOrDefault();
            if (ModelState.IsValid)
            {                
            if (post == null)
                {
                    throw new Exception(string.Format("Post código {0} não encontrado.", viewModel.Id));
                }
                var comentario = new Comentario();
                comentario.admPost = HttpContext.User.Identity.IsAuthenticated;
                comentario.Descricao = viewModel.ComentarioDescricao;
                comentario.email = viewModel.ComentarioEmail;
                comentario.idPost = viewModel.Id;
                comentario.Nome = viewModel.ComentarioNome;
                comentario.PaginaWEB = viewModel.ComentarioPaginaWeb;
                comentario.DataHora = DateTime.Now;

                try
                {
                    conexaoBanco.Comentarios.Add(comentario);
                    conexaoBanco.SaveChanges();
                    return Redirect(Url.Action("Post", new
                    {
                        ano = post.dataPublicacao.Year,
                        mes = post.dataPublicacao.Month,
                        dia = post.dataPublicacao.Day,
                        titulo = post.Titulo,
                        id = post.id
                    })+ "#comentarios");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);                    
                }                
            }
            preencherViewModel(post, null, viewModel);
            return View(viewModel);
        }
        #endregion

        public ActionResult _PaginacaoPost()
        {
            return PartialView();
        }
    }
}