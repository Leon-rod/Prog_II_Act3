using Practica02.Contracts;
using Practica02.Implementations;
using Practica02.Models;

namespace Practica02.Services
{
    public class ArticleManager
    {
        private IApplicationRepository _repository;
        public ArticleManager()
        {
            this._repository = new ApplicationRepositoryADO();
        }
        public List<Articulo> GetAllArticles()
        {
            return _repository.GetAll();
        }
        public bool SaveArticle(Articulo articulo)
        {
            return _repository.Save(articulo);
        }
        public bool DeleteArticle(int id)
        {
            return this._repository.Delete(id);
        }
        public Articulo GetArticleById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
