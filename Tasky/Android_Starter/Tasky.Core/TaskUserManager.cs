using System;
using System.Collections.Generic;

namespace Tasky.Core {
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public static class TaskUserManager {

		static TaskUserManager ()
		{
		}
		
		public static User GetUser(int id)
		{
			return UserRepositoryADO.GetUser(id);
		}
        public static User GetUser(int id,int userid)
        {
            return UserRepositoryADO.GetUser(id);
        }

        public static IList<User> GetUsers ()
		{
			return new List<User>(UserRepositoryADO.GetUsers());
		}
		
		
        public static int SaveUser(User item)
        {
            return UserRepositoryADO.SaveUser(item);
        }

        public static int DeleteUser(int id)
		{
			return UserRepositoryADO.DeleteUser(id);
		}
	}
}