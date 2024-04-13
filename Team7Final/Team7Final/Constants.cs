using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Team7Final
{
    public class Constants
    {
        public const string DatabaseFilename = "TaskSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                Console.WriteLine(Path.Combine(basePath, DatabaseFilename));
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
