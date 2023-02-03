// ************************************************
//      Project: Chronos.DataModel
//         File: TimeEntryKey.cs
//       Author:
// ************************************************


namespace Chronos.DataModel
{
    using Chronos.DataModel.Core;

    public class TimeEntryKey : IntegerEntityKey
    {
        // ---- constructor ----

        /// <summary>
        /// Initializes a new instance of <see cref="TimeEntryKey"/>.
        /// </summary>
        /// <param name="key"></param>
        public TimeEntryKey(int key) 
            : base(key)
        {
        }
    }
}
