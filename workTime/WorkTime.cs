using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace workTime
{

    /// <summary>
    /// Work time    
    /// <para lang="tr">Çalışma zamanı</para>
    /// </summary>
    public class WorkTime
    {
        /// <summary>
        /// Create new work time
        /// </summary>
        public WorkTime()
        {
            DaysOfWeek = new List<DayOfWeek>();
        }

        /// <summary>
        /// Create new work time
        /// </summary>
        /// <param name="beginHour">Begin hour</param>
        /// <param name="beginMinute">Begin minute</param>
        /// <param name="endHour">End hour</param>
        /// <param name="endMinute">End minute</param>
        /// <param name="displayName">Display name</param>
        /// <param name="days">Day of weeks for work time</param>
        /// <returns></returns>
        public WorkTime(int beginHour, int beginMinute, int endHour, int endMinute, string displayName, params DayOfWeek[] daysOfWeek): this()
        {
            if(daysOfWeek == null)
            {
                throw new ArgumentNullException("days");
            }
            Begin = new TimeSpan(beginHour, beginMinute, 0);
            End = new TimeSpan(endHour, endMinute, 0);
            ((List<DayOfWeek>)DaysOfWeek).AddRange(daysOfWeek);
            DisplayName = displayName;
        }

        /// <summary>
        /// Days of weeks for work time
        /// </summary>
        /// <value></value>
        public ICollection<DayOfWeek> DaysOfWeek { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        /// <value></value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Begin time
        /// </summary>
        /// <value></value>
        public TimeSpan Begin { get; set; }

        /// <summary>
        /// End time
        /// </summary>
        /// <value></value>
        public TimeSpan End { get; set; }

        /// <summary>
        /// For humman read
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder("Worktime");
            sb.Append(" Begin: ").Append(Begin)
            .Append(" End: ").Append(End)
            .Append(" DaysOfWeek:  ");
            if (DaysOfWeek != null)
            {
                sb.Append(string.Join(", ", DaysOfWeek.Select(e => e.ToString()).ToArray()));
            }
            else
            {
                sb.Append("no day");
            }
            sb.Append(" DisplayName: ").Append(DisplayName);
            return sb.ToString();
        }
    }

}