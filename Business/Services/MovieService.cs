#nullable disable

using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IMovieService
    {
        IQueryable<MovieModel> Query();
        bool Add(MovieModel model);
        bool Update(MovieModel model);
        bool Delete(int id);
    }

    public class MovieService : IMovieService
    {
        private readonly Db _db;

        public MovieService(Db db)
        {
            _db = db;
        }

        public bool Add(MovieModel model)
        {
            if (_db.Movies.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return false;
            Movie entity = new Movie()
            {
				Id = model.Id,
				Name = model.Name.Trim(),
                Year = model.Year,
                Revenue = model.Revenue,
				DirectorId = model.DirectorId ?? 0,
			};
            _db.Movies.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Movie entity = _db.Movies.SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return false;
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return true;
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies.Include(s => s.Director)
                .OrderByDescending(s => s.Year).ThenByDescending(s => s.Revenue).ThenBy(s => s.Name)
                .Select(s => new MovieModel()
                {
					//DirectorOutput = s.Director.Name,
					//GenreId = s.GenreId,
					//GenreOutput = s.Genre.Name,
					Id = s.Id,
                    Name = s.Name,
                    Revenue = s.Revenue,
                    Year = s.Year,
					DirectorId = s.DirectorId,
					DirectorOutput = s.Director.Name + " " + s.Director.Surname,
				});
        }

        public bool Update(MovieModel model)
        {
            if (_db.Movies.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
                return false;
            Movie existingEntity = _db.Movies.SingleOrDefault(s => s.Id == model.Id);
            if (existingEntity is null)
                return false;
            existingEntity.DirectorId = model.DirectorId ?? 0;
            //existingEntity.GenreId = model.GenreId ?? 0;
            existingEntity.Name = model.Name.Trim();
            existingEntity.Revenue = model.Revenue;
            existingEntity.Year = model.Year;
            _db.Movies.Update(existingEntity);
            _db.SaveChanges();
            return true;
        }
    }
}
