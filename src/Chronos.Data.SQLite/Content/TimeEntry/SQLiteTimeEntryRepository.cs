using Chronos.Data.SQLite.Content.Defines;
using Chronos.DataModel;
using Chronos.DataModel.Core;
using SQLite;

namespace Chronos.Data.SQLite
{
    public class SQLiteTimeEntryRepository : TimeEntryRepository
    {
        // ---- private fields ----
        private Lazy<string> readTimetrackSQL = new Lazy<string>(() => ReadResouce("TIMETRACK_READ.sql"));
        private Lazy<string> insertTimetrackSQL = new Lazy<string>(() => ReadResouce("TIMETRACK_INSERT.sql")); 
        private Lazy<string> updateTimetrackSQL = new Lazy<string>(() => ReadResouce("TIMETRACK_UPDATE.sql")); 
        private Lazy<string> insertTimetrackBreakSql = new Lazy<string>(() => ReadResouce("TIMETRACK_BREAK_INSERT.sql")); 

        private Lazy<SQLiteAsyncConnection> databaseDeferrer
            = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(Defines.DatabasePath, Defines.Flags));

        private string readTimetrackStatement => readTimetrackSQL.Value;
        private string insertTimetrackStatement => insertTimetrackSQL.Value;
        private string updateTimetrackStatement => updateTimetrackSQL.Value;
        private string insertTimetrackBreakStatement => insertTimetrackBreakSql.Value;

        private SQLiteAsyncConnection database => databaseDeferrer.Value;

        // ---- public methods ----

        /// <summary>
        /// Initializes the <see cref="SQLiteTimeEntryRepository"/>.
        /// </summary>
        /// <returns></returns>
        public override Task<bool> InitializeAsync()
        {
            this.RunScript("CREATE_TABLE_TIMETRACK.sql");
            this.RunScript("CREATE_TABLE_TIMETRACK_BREAK.sql");

            return Task.FromResult(false);
        }

        /// <inheritdoc/>
        public override void Save(IContext context, TimeEntry entry)
        {
            if (entry.Key == null)
            {
                this.InsertCore(entry);
            }
            else
            {
                this.UpdateCore(entry);
            }
        }

        /// <inheritdoc/>
        public override TimeEntry Read(IContext context, int id)
        {
            var connection = this.database.GetConnection();

            var command = connection.CreateCommand(this.readTimetrackStatement);

            command.Bind("@TT_ref", id);

            return command.ExecuteQuery<TimeEntry>().FirstOrDefault();
        }

        /// <inheritdoc/>
        public override IEnumerable<TimeEntry> ReadAll(IContext context)
        {
            var connection = this.database.GetConnection();

            var command = connection.CreateCommand(this.readTimetrackStatement);

            command.Bind("@TT_ref", -1);

            return command.ExecuteQuery<TimeEntry>();
        }

        // ---- private methods ----

        /// <summary>
        /// Runs the given <paramref name="script"/>.
        /// </summary>
        /// <param name="script"></param>
        private void RunScript(string script)
        {
            using (Stream fs = TaskHelper.RunAsSync(() => FileSystem.Current.OpenAppPackageFileAsync(script)))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string content = TaskHelper.RunAsSync(() => reader.ReadToEndAsync());

                    TaskHelper.RunAsSync(() => this.database.ExecuteAsync(content));
                }
            }
        }

        /// <summary>
        /// Inserts the given <paramref name="entry"/>.
        /// </summary>
        /// <param name="entry"></param>
        private void InsertCore(TimeEntry entry)
        {
            var connection = this.database.GetConnection();

            SQLiteCommand command = connection.CreateCommand(this.insertTimetrackStatement);

            command.Bind("@tt_startTime", entry.StartTime);
            command.Bind("@tt_endTime", entry.EndTime);

            command.ExecuteNonQuery();

            entry.SetKey(new TimeEntryKey((int)SQLite3.LastInsertRowid(connection.Handle)));

            if (entry.Breaks.Any())
            {
                command = connection.CreateCommand(this.insertTimetrackBreakStatement);

                command.Bind("@TT_ref", entry.Key.Id);

                foreach (var @break in entry.Breaks)
                {
                    command.Bind("@TTB_startTime", @break.StartTime);
                    command.Bind("@TTB_endTime", @break.EndTime);

                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Updates the given <paramref name="entry"/>.
        /// </summary>
        /// <param name="entry"></param>
        private void UpdateCore(TimeEntry entry)
        {
            var connection = this.database.GetConnection();

            SQLiteCommand command = connection.CreateCommand(this.updateTimetrackStatement);

            command.Bind("@tt_startTime", entry.StartTime);
            command.Bind("@tt_endTime", entry.EndTime);
            command.Bind("@tt_ref", entry.Key.Id);

            command.ExecuteNonQuery();
        }


        /// <summary>
        /// Reads the given <paramref name="file"/>.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static string ReadResouce(string file)
        {
            using (Stream fs = TaskHelper.RunAsSync(() => FileSystem.Current.OpenAppPackageFileAsync(file)))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    return TaskHelper.RunAsSync(() => reader.ReadToEndAsync());
                }
            }
        }
    }
}
