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
                               select p).Skip(qtdRegistrosPular).Take(registrosPorPagina).ToList();
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

        public ActionResult _Paginacao()
        {
            return PartialView();
        }

        public ActionResult Post(int id)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts
                        where p.id == id
                        select new DetalharPostViewModel
                        {
                            Id = p.id,
                            Titulo = p.Titulo,
                            Autor = p.Autor,
                            DataPublicacao = p.dataPublicacao,
                            HoraPublicacao = p.dataPublicacao,
                            Descricao = p.Descricao,
                            Tags = p.TagPosts.Select(x => x.TagClass).ToList()
                        }).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não encontrado", id));
            }
            return View(post);            
        }      
    }    
}