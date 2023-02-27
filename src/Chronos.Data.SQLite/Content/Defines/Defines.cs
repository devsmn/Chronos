using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Data.SQLite.Content.Defines
{
    internal static class Defines
    {
        public const string DatabaseFilename = "persistantTimeStore.db3";

        public const SQLiteOpenFlags Flags =
        // open the databaseDeferrer in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the databaseDeferrer if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded databaseDeferrer access
        SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
