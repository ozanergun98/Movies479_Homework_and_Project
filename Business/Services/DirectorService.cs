using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IDirectorService
    {
        IQueryable<DirectorModel> Query();
        bool Add(DirectorModel model);
        bool Update(DirectorModel model);
        bool Delete(int id);
    }

    public class DirectorService : IDirectorService
    {
        private readonly Db _db;

        public DirectorService(Db db)
        {
            _db = db;
        }

        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors.Include(r => r.Movies).OrderBy(r => r.Name).Select(r => new DirectorModel()
            {
                Id = r.Id,
                Name = r.Name,
                Surname = r.Surname,
                BirthDate = r.BirthDate,
                IsRetired = r.IsRetired,
                DirectorOutput = r.Name + " " + r.Surname
            });
        }

        public bool Add(DirectorModel model)
        {
            var entity = new Director()
            {
                Name = model.Name.Trim(),
                Surname = model.Surname.Trim(),
                BirthDate = model.BirthDate,
                IsRetired = model.IsRetired
            };
            _db.Directors.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Update(DirectorModel model)
        {
            if (_db.Directors.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
                return false;
            Director existingEntity = _db.Directors.SingleOrDefault(s => s.Id == model.Id);
            if (existingEntity is null)
                return false;
            existingEntity.Name = model.Name.Trim();
            existingEntity.Surname = model.Surname;
            existingEntity.BirthDate = model.BirthDate;
            existingEntity.IsRetired = model.IsRetired;
            _db.Directors.Update(existingEntity);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Director entity = _db.Directors.SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return false;
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
