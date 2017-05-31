using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;


namespace Tasky.Data.Android
{
    //Connection Object
    public class DALConnection
    {
        public string DBName { get; set; }

    }

    public class DAL
    {
        static object locker = new object();

        public SqliteConnection connection;

        public string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        public DAL(string dbPath)
        {
            var output = "";
            path = dbPath;
            // create the tables
            bool exists = File.Exists(dbPath);

            if (!exists)
            {
                connection = new SqliteConnection("Data Source=" + dbPath);

                connection.Open();
                var commands = new[] {
                    "CREATE TABLE IF NOT EXISTS  [Users] (_id INTEGER PRIMARY KEY ASC, Name NTEXT); ","CREATE TABLE IF NOT EXISTS[Items](_id INTEGER PRIMARY KEY ASC, Name NTEXT, Notes NTEXT, Done INTEGER, TaskUserId INETGER, FOREIGN KEY(TaskUserId) REFERENCES Users(id)); "
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
            
            Console.WriteLine(output);
        }



    }


}
