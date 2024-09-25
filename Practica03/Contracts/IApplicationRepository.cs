using Practica02.Models;

namespace Practica02.Contracts
{
    public interface IApplicationRepository
    {
        bool Save(Articulo articulo);
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool Delete(int id);
    }
}
