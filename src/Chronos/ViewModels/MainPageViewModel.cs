// ************************************************
//      Project: Chronos.UI
//         File: MainPageViewModel.cs
//       Author:
// ************************************************

namespace Chronos.UI
{
    using Chronos.DataModel;
    using Chronos.DataModel.Core;
    using Chronos.UI.Core;
    using System.Collections.ObjectModel;

    internal class MainPageViewModel : BaseViewModel
    {
        // ---- private fields ----
        Command onToggleTimeClickedCommand;
        Command onTogglePauseClickedCommand;
        private ObservableCollection<TimeEntry> timeEntries;
        private TimeEntry activeTimeEntry;
        private TimeMode timeMode;

        // ---- public properties ----
        /// <summary>
        /// Gets or sets the current <see cref="TimeMode"/>.
        /// </summary>
        public TimeMode TimeMode
        {
            get { return this.timeMode; }
            set { this.SetProperty(ref this.timeMode, value); }
        }

        /// <summary>
        /// Gets or sets the current <see cref="TimeEntry"/>.
        /// </summary>
        public TimeEntry ActiveTimeEntry
        {
            get { return this.activeTimeEntry; }
            set { this.SetProperty(ref this.activeTimeEntry, value); }
        }

        /// <summary>
        /// </summary>
        public ObservableCollection<TimeEntry> TimeEntries
        {
            get
            {
                if (this.timeEntries == null)
                {
                    this.timeEntries = new ObservableCollection<TimeEntry>();
                }

                return this.timeEntries;
            }

            private set
            {
                timeEntries = value;
            }
        }


        public Command OnToggleTimeClickedCommand
        {
            get
            {
                this.onToggleTimeClickedCommand ??= new Command(this.OnToggleTimeClicked);

                return this.onToggleTimeClickedCommand;
            }
        }

        public Command OnTogglePauseClickedCommand
        {
            get
            {
                this.onTogglePauseClickedCommand ??= new Command(this.OnTogglePauseClicked);

                return this.onTogglePauseClickedCommand;
            }
        }



        // ---- constructor ----

        /// <summary>
        /// Initializes a new instance of <see cref="MainPageViewModel"/>.
        /// </summary>
        public MainPageViewModel()
        {
            this.TimeMode = TimeMode.Stopped;

            this.TimeEntries = new ObservableCollection<TimeEntry>(TimeEntry.ReadAll(null));

            var startTime = Preferences.Get("StartTime", DateTime.MinValue);

            if (startTime != DateTime.MinValue)
            {
                this.CreateNewActiveTimeEntry();
                this.ActiveTimeEntry.StartTime = startTime;
                this.TimeMode = TimeMode.Tracking;
            }
        }

        // ---- methods ----

        /// <summary>
        /// Callback for the <see cref="OnToggleTimeClickedCommand"/>.
        /// </summary>
        /// <param name="args"></param>
        private void OnToggleTimeClicked(object args)
        {
            if (this.TimeMode == TimeMode.Tracking)
            {
                this.TimeMode = TimeMode.Stopped;
            }
            else if (this.TimeMode == TimeMode.Stopped)
            {
                this.TimeMode = TimeMode.Tracking;
            }
            else
            {
                this.TimeMode = TimeMode.Stopped;
                this.ActiveTimeEntry.EndBreak();
            }

            this.AssureTimeEntryIsValid();

            this.ActiveTimeEntry.UpdateTime();

            if (this.ActiveTimeEntry.IsFinished)
            {
                this.TimeEntries.Add(this.ActiveTimeEntry);

                this.ActiveTimeEntry.Save(null);

                Preferences.Remove("StartTime");
            }
            else
            {
                if (this.ActiveTimeEntry.IsStartTimeSet)
                {
                    Preferences.Set("StartTime", this.ActiveTimeEntry.StartTime.GetValueOrDefault());
                }
            }
        }

        /// <summary>
        /// Callback for the <see cref="OnTogglePauseClickedCommand"/>.
        /// </summary>
        /// <param name="args"></param>
        private void OnTogglePauseClicked(object args)
        {
            if (this.TimeMode == TimeMode.Tracking)
            {
                this.TimeMode = TimeMode.Paused;

                this.activeTimeEntry.StartBreak();
            }
            else
            {
                this.TimeMode = TimeMode.Tracking;

                this.activeTimeEntry.EndBreak();
            }
        }

        /// <summary>
        /// Assures that <see cref="ActiveTimeEntry"/> is valid.
        /// </summary>
        private void AssureTimeEntryIsValid()
        {
            if (this.ActiveTimeEntry == null || this.ActiveTimeEntry.IsFinished)
            {
                this.CreateNewActiveTimeEntry();
            }
        }

        /// <summary>
        /// Creates a new <see cref="TimeEntry"/>.
        /// </summary>
        private void CreateNewActiveTimeEntry()
        {
            this.ActiveTimeEntry = new TimeEntry(IntegerEntityKey.TemporaryId);
        }

    }
}
