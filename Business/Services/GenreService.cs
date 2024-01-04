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
        bool Update(GenreModel model);
		bool Delete(int id);
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
            if (_db.Genres.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
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
            return _db.Genres.Select(s => new GenreModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                });
        }

		public bool Update(GenreModel model)
		{
			if (_db.Genres.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
				return false;
			Genre existingEntity = _db.Genres.SingleOrDefault(s => s.Id == model.Id);
			if (existingEntity is null)
				return false;
			existingEntity.Name = model.Name.Trim();
			_db.Genres.Update(existingEntity);
			_db.SaveChanges();
			return true;
		}
		public bool Delete(int id)
		{
			Genre entity = _db.Genres.SingleOrDefault(s => s.Id == id);
			if (entity is null)
				return false;
			_db.Genres.Remove(entity);
			_db.SaveChanges();
			return true;
		}
	}
}
