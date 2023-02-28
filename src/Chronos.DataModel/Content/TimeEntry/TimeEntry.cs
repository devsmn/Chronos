// ************************************************
//      Project: Chronos.DataModel
//         File: TimeEntry.cs
//       Author:
// ************************************************


namespace Chronos.DataModel
{
    using Chronos.DataModel.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public partial class TimeEntry : ObservableEntity<TimeEntryKey>
    {
        // ---- private fields ----
        private TimeRange currentBreak;
        private DateTime? startTime;
        private DateTime? endTime;
        private TimeSpan duration;

        // ---- public properties ----

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime? StartTime
        {
            get { return this.startTime; }
            set { this.SetProperty(ref this.startTime, value); }
        }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        public DateTime? EndTime
        {
            get { return this.endTime; }
            set
            {
                this.SetProperty(ref this.endTime, value);

                this.UpdateDuration();
            }
        }

        /// <summary>
        /// Gets or sets the breaks.
        /// </summary>
        public IList<TimeRange> Breaks { get; set; }

        /// <summary>
        /// Gets or sets the duration without any breaks.
        /// </summary>
        public TimeSpan Duration
        {
            get { return this.duration; }
            set { this.SetProperty(ref this.duration, value); }
        }

        /// <summary>
        /// Gets whether the <see cref="StartTime"/> is set.
        /// </summary>
        public bool IsStartTimeSet { get { return this.StartTime != null; } }

        /// <summary>
        /// Gets whether the <see cref="EndTime"/> is set.
        /// </summary>
        public bool IsEndTimeSet { get { return this.EndTime != null; } }

        /// <summary>
        /// Gets whether the <see cref="TimeEntry"/> is finished.
        /// </summary>
        public bool IsFinished { get { return this.IsStartTimeSet && this.IsEndTimeSet; } }

        // ---- constructor ----
        /// <summary>
        /// Initializes a new instance of <see cref="TimeEntry"/>
        /// </summary>
        /// <param name="key"></param>
        public TimeEntry(TimeEntryKey key)
            : base(key)
        {
            this.Breaks = new List<TimeRange>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TimeEntry"/>.
        /// </summary>
        /// <param name="key"></param>
        public TimeEntry(int key)
            : this(new TimeEntryKey(key)) 
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TimeEntry"/>.
        /// </summary>
        public TimeEntry()
            : this(IntegerEntityKey.TemporaryId)
        {
            // Needed for SQLite reflection.
        }

        // ---- methods ----

        /// <summary>
        /// Starts a new break.
        /// </summary>
        public void StartBreak()
        {
            this.currentBreak = new TimeRange()
            {
                StartTime = DateTime.Now
            };
        }

        /// <summary>
        /// Ends the current break.
        /// </summary>
        public void EndBreak()
        {
            this.currentBreak.EndTime = DateTime.Now;

            this.Breaks.Add(this.currentBreak);

        }

        // ---- private methods ----

        /// <summary>
        /// Updates the <see cref="StartTime"/> to <see cref="DateTime.Now"/>.
        /// </summary>
        private void UpdateStartTime()
        {
            this.StartTime = DateTime.Now;
        }

        /// <summary>
        /// Updates the <see cref="EndTime"/> to <see cref="DateTime.Now"/>.
        /// </summary>
        private void UpdateEndTime()
        {
            this.EndTime = DateTime.Now;
        }

        /// <summary>
        /// Updates either the <see cref="StartTime"/> or the <see cref="EndTime"/> to <see cref="DateTime.Now"/>.
        /// </summary>
        public void UpdateTime()
        {
            if (!this.IsStartTimeSet)
            {
                this.UpdateStartTime();
            }
            else if (!this.IsEndTimeSet)
            {
                this.UpdateEndTime();
            }
        }

        /// <summary>
        /// Updates the <see cref="Duration"/>.
        /// </summary>
        private void UpdateDuration()
        {
            double breakTime = 0;

            if (this.Breaks.Any())
            {
                breakTime = this.Breaks.Sum(x => x.EndTime.Subtract(x.StartTime).TotalSeconds);
            }

            this.Duration =
                this.IsStartTimeSet && this.IsEndTimeSet
                    ? this.EndTime.GetValueOrDefault().Subtract(
                        this.StartTime.GetValueOrDefault()).Subtract(TimeSpan.FromSeconds(breakTime))
                    : TimeSpan.Zero;
        }


    }
}
