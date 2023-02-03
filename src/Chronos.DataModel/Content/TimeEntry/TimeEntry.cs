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

    public class TimeEntry : ObservableEntity<TimeEntryKey>
    {
        // ---- private fields ----
        private TimeRange currentBreak;
        private DateTime? startTime;
        private DateTime? endTime;
        private TimeSpan duration;

        // ---- public properties ----
        public DateTime? StartTime
        {
            get { return this.startTime; }
            set { this.SetProperty(ref this.startTime, value); }
        }


        public DateTime? EndTime
        {
            get { return this.endTime; }
            set
            {
                this.SetProperty(ref this.endTime, value);

                this.UpdateDuration();
            }
        }


        public IList<TimeRange> Breaks { get; set; }

        public TimeSpan Duration
        {
            get { return this.duration; }
            set { this.SetProperty(ref this.duration, value); }
        }

        public bool IsStartTimeSet { get { return this.StartTime != null; } }
        public bool IsEndTimeSet { get { return this.EndTime != null; } }
        public bool IsFinished { get { return this.IsStartTimeSet && this.IsEndTimeSet; } }

        // ---- constructor ----
        /// <summary>
        /// Initializes a new instance of <see cref="TimeEntry"/>
        /// </summary>
        /// <param name="key"></param>
        public TimeEntry(EntityKey key)
            : base(key)
        {
            this.Breaks = new List<TimeRange>();
        }

        // ---- methods ----

        public void StartBreak()
        {
            this.currentBreak = new TimeRange()
            {
                StartTime = DateTime.Now
            };
        }

        public void EndBreak()
        {
            this.currentBreak.EndTime = DateTime.Now;

            this.Breaks.Add(this.currentBreak);

        }

        private void UpdateStartTime()
        {
            this.StartTime = DateTime.Now;
        }

        private void UpdateEndTime()
        {
            this.EndTime = DateTime.Now;
        }

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

        public void UpdateDuration()
        {
            double breakTime = 0;

            if (this.Breaks.Any())
            {
                breakTime = this.Breaks.Sum(x => x.EndTime.Subtract(x.StartTime).TotalSeconds);
            }

            this.Duration =
                this.IsStartTimeSet && this.IsEndTimeSet
                    ? this.EndTime.GetValueOrDefault().Subtract(this.StartTime.GetValueOrDefault()).Subtract(TimeSpan.FromSeconds(breakTime))
                    : TimeSpan.Zero;
        }

    }
}
