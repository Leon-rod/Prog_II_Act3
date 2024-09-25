using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica02.Models;
using Practica02.Services;

namespace Practica02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private ArticleManager ArticleManager = new ArticleManager();

        [HttpGet]
        public IActionResult GetAllArticles()
        {
            List<Articulo> articles = ArticleManager.GetAllArticles();
            if (articles.Count > 0)
                return Ok(articles);
            else
                return Ok("No hay ningún artículo en la base de datos");
        }

        [HttpGet("{idArticulo}")]
        public IActionResult GetArticleById(int idArticulo)
        {
            Articulo articulo = ArticleManager.GetArticleById(idArticulo);
            if (articulo.IdArticulo != null)
                return Ok(articulo);
            else
                return Ok($"No hay ningun articulo con el ID: {idArticulo}");
        }

        [HttpPost]   //AL REGISTRAR UN ARTICULO EN SWAGGER, EL CAMPO ID DEBE SER NULL
        public IActionResult SaveArticle([FromBody] Articulo articulo)
        {
            
            if (articulo != null && articulo.IdArticulo == null)
            {
                ArticleManager.SaveArticle(articulo);
                return Ok($"Se ha registrado el articulo {articulo.Descripcion} con éxito!");
            }
            else
                return BadRequest();
        }

        [HttpPut("{idArticle}")] // AL ACTUALIZAR EL ARTICULO DESDE SWAGGER, EL ID DE LA RUTA DEBE COINCIDIR CON EL DEL ARTICULO
        public IActionResult EditArticle([FromRoute]int idArticle, [FromBody] Articulo articulo)
        {
            if (articulo != null && articulo.IdArticulo != null)
            {
                ArticleManager.SaveArticle(articulo);
                return Ok($"Se ha actualizado el articulo {articulo.Descripcion} con éxito!");
            }
            else
                return BadRequest();
        }

        [HttpDelete("{idArticle}")]
        public IActionResult DeleteArticle([FromRoute]int idArticle)
        {
            if (ArticleManager.DeleteArticle(idArticle))
                return Ok($"Se ha eliminado el articulo con el ID: {idArticle} con éxito");
            else 
                return BadRequest();
        }
    }
}
