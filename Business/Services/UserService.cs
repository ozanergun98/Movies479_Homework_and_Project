using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
	public interface IUserService
	{
		IQueryable<DirectorModel> Query();
		bool Add(DirectorModel model);
		bool Delete(int id);
	}
	public class UserService
	{
		private readonly Db _db;

		public UserService(Db db)
		{
			_db = db;
		}

		public IQueryable<UserModel> Query()
		{
			return _db.Users.Select(u => new UserModel()
			{
				UserId = u.UserId,
				UserName = u.UserName,
				Password = u.Password,
				Role = u.Role,
			});
		}

		public bool Add(UserModel model)
		{
			var entity = new User()
			{
				UserId = model.UserId,
				UserName = model.UserName.Trim(),
				Password = model.Password,
				Role = model.Role
			};
			_db.Users.Add(entity);
			_db.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			User entity = _db.Users.SingleOrDefault(u => u.UserId == id);
			if (entity is null)
				return false;
			_db.Users.Remove(entity);
			_db.SaveChanges();
			return true;
		}
	}
}
