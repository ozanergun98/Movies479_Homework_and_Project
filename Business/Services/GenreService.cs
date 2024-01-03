using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IGenreService
    {
        IQueryable<GenreModel> Query();
        bool Add(GenreModel model);
    }

    public class GenreService : IGenreService
    {
        private readonly Db _db;

        public GenreService(Db db)
        {
            _db = db;
        }

        public bool Add(GenreModel model)
        {
            if (_db.Movies.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return false;
            Genre entity = new Genre()
            {
                Name = model.Name.Trim(),
            };
            _db.Genres.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.Include(s => s.Movies)
                .OrderByDescending(s => s.Name)
                .Select(s => new GenreModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                });
        }
    }
}
