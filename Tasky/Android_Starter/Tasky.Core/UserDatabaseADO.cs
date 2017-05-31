using System;
using System.Linq;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.IO;
using System.Data;

namespace Tasky.Core
{
	/// <summary>
	/// TaskDatabase uses ADO.NET to create the [Items] table and create,read,update,delete data
	/// </summary>
	public class UserDatabase 
	{
		static object locker = new object ();

		public SqliteConnection connection;

		public string path;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		public UserDatabase(string dbPath) 
		{
			var output = "";
			path = dbPath;
			// create the tables
			bool exists = File.Exists (dbPath);

			if (!exists) {
				connection = new SqliteConnection ("Data Source=" + dbPath);

				connection.Open ();
				var commands = new[] {
					"CREATE TABLE [tblUser] (_id INTEGER PRIMARY KEY ASC, Name NTEXT);"
                };
				foreach (var command in commands) {
					using (var c = connection.CreateCommand ()) {
						c.CommandText = command;
						var i = c.ExecuteNonQuery ();
					}
				}
			} else {
                // already exists, do nothing. 
                connection = new SqliteConnection("Data Source=" + dbPath);                
               
                connection.Open();
                var commands = new[] {
                    "CREATE TABLE IF NOT EXISTS  [tblUser] (_id INTEGER PRIMARY KEY ASC, Name NTEXT);"
                };
                foreach (var command in commands)
                {
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = command;
                        var i = c.ExecuteNonQuery();
                    }
                }
            }
			Console.WriteLine (output);
		}

		/// <summary>Convert from DataReader to Task object</summary>
		User FromReader (SqliteDataReader r) {
			var t = new User ();
			t.UserID = Convert.ToInt32 (r ["_id"]);
			t.UserName = r ["Name"].ToString ();
			return t;
		}

		public IEnumerable<User> GetUserItems ()
		{
			var tl = new List<User> ();

			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var contents = connection.CreateCommand ()) {
					contents.CommandText = "SELECT [_id], [Name] from [tblUser] ";
					var r = contents.ExecuteReader ();
					while (r.Read ()) {
						tl.Add (FromReader(r));
					}
				}
				connection.Close ();
			}
			return tl;
		}


        public User GetItem(int id)
        {
            var t = new User();
            lock (locker)
            {
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [_id], [Name] from [tblUser] WHERE [_id] = ?";
                    command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = id });
                    var r = command.ExecuteReader();
                    while (r.Read())
                    {
                        t = FromReader(r);
                        break;
                    }
                }
                connection.Close();
            }
            return t;
        }

        public User GetItem(int id,int userId)
        {
            var t = new User();
            lock (locker)
            {
                connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [_id], [Name] from [tblUser] WHERE [_id] = ?";
                    command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = id });
                    var r = command.ExecuteReader();
                    while (r.Read())
                    {
                        t = FromReader(r);
                        break;
                    }
                }
                connection.Close();
            }
            return t;
        }

        public int SaveItem (User item) 
		{
			int r;
			lock (locker) {
				if (item.UserID != 0) {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "UPDATE [tblUser] SET [Name] = ? WHERE [_id] = ?;";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.UserName });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.UserID });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				} else {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [tblUser] ([Name]) VALUES (?)";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.UserName });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				}

			}
		}

      

        public int DeleteItem(int id) 
		{
			lock (locker) {
				int r;
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "DELETE FROM [tblUser] WHERE [_id] = ?;";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});
					r = command.ExecuteNonQuery ();
				}
				connection.Close ();
				return r;
			}
		}
	}
}