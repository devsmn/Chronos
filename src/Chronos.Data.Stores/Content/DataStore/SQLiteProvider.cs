using Chronos.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Data.SQLite.Provider
{
    public class SQLiteProvider : DataProvider
    {
        public override void Initialize()
        {
            DataModel.DataStore.Register<SQLiteTimeEntryRepository>();
        }
    }
}
