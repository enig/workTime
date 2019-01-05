using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace workTime
{
    /// <summary>
    /// Work time calculator
    /// </summary>
    public class WorkTimeCalculator : IDisposable
    {
        /// <summary>
        /// Holidays
        /// <para lang="tr">Tatiller</para>
        /// </summary>
        public ICollection<Holiday> Holidays { get; private set; }

        /// <summary>
        /// Add holidays
        /// </summary>
        /// <param name="holidays">Holidays</param>
        /// <returns></returns>
        public WorkTimeCalculator AddHolidays(IEnumerable<Holiday> holidays)
        {
            if (holidays == null)
            {
                throw new ArgumentNullException("holidays");
            }
            foreach (var holiday in holidays)
            {
                AddHoliday(holiday);
            }
            return this;
        }

        /// <summary>
        /// Work times
        /// <para lang="tr">Çalışma zamanları</para>
        /// </summary>
        public ICollection<WorkTime> WorkTimes { get; private set; }

        /// <summary>
        /// Create new work time calculator
        /// </summary>
        public WorkTimeCalculator()
        {
            Holidays = new List<Holiday>();
            WorkTimes = new List<WorkTime>();
        }

        /// <summary>
        /// Add holiday
        /// <para lang="tr">Tatil ekle</para>
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public WorkTimeCalculator AddHoliday(Holiday holiday)
        {
            if (holiday == null)
            {
                throw new ArgumentNullException("holiday");
            }
            Holidays.Add(holiday);
            return this;
        }

        /// <summary>
        /// Remove holiday
        /// <para lang="tr">Tatil çıkar</para>
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public WorkTimeCalculator RemoveHoliday(Holiday holiday)
        {
            if (holiday == null)
            {
                throw new ArgumentNullException("holiday");
            }
            var isRemoved = Holidays.Remove(holiday);
            if (!isRemoved)
            {
                throw new ArgumentOutOfRangeException("holiday", holiday, "Holiday is not contains.");
            }
            return this;
        }

        /// <summary>
        /// Add work times
        /// <para lang="tr">Çalışma zamanlarını ekler.</para>
        /// </summary>
        /// <param name="workTimes"></param>
        /// <returns></returns>
        public WorkTimeCalculator AddWorkTimes(IEnumerable<WorkTime> workTimes)
        {
            if (workTimes == null)
            {
                throw new ArgumentNullException("workTimes");
            }
            foreach (var workTime in workTimes)
            {
                AddWorkTime(workTime);
            }
            return this;
        }

        /// <summary>
        /// Try find holiday for date field. If found return true and set founded holiday parameter.
        /// <para lang="tr">date alanı ile belirtilen tarih için tatil olup olmadığını belirler. Eğer varsa holiday alanına atar.</para>
        /// </summary>
        /// <param name="date">Check date</param>
        /// <param name="holiday">Return holiday if date in holiday</param>
        /// <returns></returns>
        public bool TryFindHoliday(DateTime date, out Holiday holiday)
        {
            holiday = Holidays.FirstOrDefault(e => e.Begin <= date && e.End > date);
            var ret = holiday != null;
            return ret;
        }

        /// <summary>
        /// Add work time
        /// <para lang="tr">Çalışma zamanı ekle</para>
        /// </summary>
        /// <param name="worktime"></param>
        /// <returns></returns>
        public WorkTimeCalculator AddWorkTime(WorkTime worktime)
        {
            WorkTimes.Add(worktime);
            return this;
        }

        /// <summary>
        /// Add work time
        /// <para lang="tr">Çalışma zamanı ekle</para>
        /// </summary>
        /// <param name="begin">Begin time</param>
        /// <param name="end">End time</param>
        /// <param name="daysOfWeek">Days of weeks for work time</param>
        /// <returns></returns>
        public WorkTimeCalculator AddWorkTime(TimeSpan begin, TimeSpan end, params DayOfWeek[] daysOfWeek)
        {
            if (daysOfWeek == null || daysOfWeek.Count() == 0)
            {
                throw new ArgumentNullException("weekdays");
            }
            if (begin >= end)
            {
                throw new ArgumentOutOfRangeException("begin", begin, "Begin timespan must be before end timespan.");
            }
            var diffTime = new TimeSpan(end.Ticks - begin.Ticks);
            if (diffTime.TotalDays > 0)
            {
                throw new ArgumentOutOfRangeException("end", end, "The End and Start time interval must be less than one day.");
            }
            var worktime = new WorkTime()
            {
                Begin = begin,
                End = end,
                DaysOfWeek = daysOfWeek
            };
            WorkTimes.Add(worktime);
            return this;
        }

        /// <summary>
        /// Add work time
        /// <para lang="tr">Çalışma zamanı ekle</para>
        /// </summary>
        /// <param name="beginHour">Begin hour</param>
        /// <param name="beginMinute">Begin minute</param>
        /// <param name="endHour">End hour</param>
        /// <param name="endMinute">End minute</param>
        /// <param name="daysOfWeek">Days of weeks for work time</param>
        /// <returns></returns>
        public WorkTimeCalculator AddWorkTime(int beginHour, int beginMinute, int beginSeconds, int endHour, int endMinute, int endSeconds, params DayOfWeek[] daysOfWeek)
        {
            var begin = new TimeSpan(beginHour, beginMinute, beginSeconds);
            var end = new TimeSpan(endHour, endMinute, endSeconds);
            return AddWorkTime(begin, end, daysOfWeek);
        }

        /// <summary>
        /// Remove work time
        /// <para lang="tr">Çalışma zamanını kaldırır</para>
        /// </summary>
        /// <param name="worktime"></param>
        /// <returns></returns>
        public WorkTimeCalculator RemoveWorkTime(WorkTime worktime)
        {
            if (worktime == null)
            {
                throw new ArgumentNullException("holiday");
            }
            var isRemoved = WorkTimes.Remove(worktime);
            if (!isRemoved)
            {
                throw new ArgumentOutOfRangeException("worktime", worktime, "Worktime is not contains.");
            }
            return this;
        }

        /// <summary>
        /// Try find work time for date field. If found return true and set founded workTime parameter.
        /// <para lang="tr">date alanı ile belirtilen tarih için çalışma zamanı olup olmadığını belirler. Eğer varsa workTime alanına atar.</para>
        /// </summary>
        /// <param name="date"></param>
        /// <para name="workTime">Return work time if date in work time</param>
        /// <returns></returns>
        public bool TryFindWorkTime(DateTime date, out WorkTime workTime)
        {
            var ret = false;
            var weekday = date.DayOfWeek;
            var time = TimeSpan.Parse(date.ToString("HH:mm:ss"));
            workTime = null;
            Holiday holiday = null;
            ret = !TryFindHoliday(date, out holiday);
            if (ret)
            {
                workTime = WorkTimes.FirstOrDefault(e => e.DaysOfWeek.Contains(weekday) && (e.Begin <= time) && (e.End > time));
                ret = ret && (workTime != null);
            }
            return ret;
        }

        /// <summary>
        /// Return total work time for beetween begin and end with out holiday.
        /// <para lang="tr">İki tarih arasındaki çalışma zamanını, belirlenen (tatiller hariç) çalışma zamanları içinde geçen süreyi verir.</para>
        /// </summary>
        /// <param name="begin">Begin date</param>
        /// <param name="end">End date</param>
        /// <returns></returns>
        public TimeSpan CalculateWorkTime(DateTime begin, DateTime end)
        {
            var ret = TimeSpan.Zero;
            if (begin >= end)
            {
                throw new ArgumentOutOfRangeException("begin", begin, "Begin date must be before end date.");
            }
            var beginEnd = end - begin;
            double seconds = 0;
            for (double second = 0; second < beginEnd.TotalSeconds; second++)
            {
                Holiday holiday = null;
                WorkTime workTime = null;
                var current = begin.AddSeconds(second);
                if (TryFindHoliday(current, out holiday))
                {
                    second += (holiday.End - current).TotalSeconds;
                }
                else
                {
                    if (TryFindWorkTime(current, out workTime))
                    {
                        var workTimeSeconds = (workTime.End - workTime.Begin).TotalSeconds;
                        var workTimeEnd = current.AddSeconds(workTimeSeconds);
                        if (TryFindHoliday(workTimeEnd, out holiday))
                        {
                            workTimeSeconds = (workTimeEnd - holiday.Begin).TotalSeconds;
                        }
                        seconds += workTimeSeconds;
                        second += workTimeSeconds;
                    }
                    else
                    {
                        var nextBeginWorkTime = GetBeginNextWorkTime(current);
                        var nextTimeSeconds = (nextBeginWorkTime - current).TotalSeconds;
                        second += nextTimeSeconds;
                    }
                }
            }
            ret = TimeSpan.FromSeconds(seconds);
            return ret;
        }

        /// <summary>
        /// Return next work time begin date
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private DateTime GetBeginNextWorkTime(DateTime current)
        {
            var ret = new DateTime(current.Ticks);
            var time = TimeSpan.Parse(current.ToString("HH:mm:ss"));
            var workTime = WorkTimes.Where(e => e.DaysOfWeek.Contains(current.DayOfWeek) && e.Begin > time).OrderBy(e => e.Begin).FirstOrDefault();
            if (workTime != null)
            {
                var seconds = (workTime.Begin - time).TotalSeconds;
                ret = current.AddSeconds(seconds);
            }
            else
            {
                var nextDay = (DayOfWeek)(((int)current.DayOfWeek + 1) % 7);
                workTime = WorkTimes.Where(e => e.DaysOfWeek.Contains(nextDay)).OrderBy(e => e.Begin).FirstOrDefault();
                if (workTime != null)
                {
                    var tempDate = DateTime.Parse(current.ToShortDateString()).AddDays(1);
                    var seconds = (tempDate - current).TotalSeconds;
                    seconds += workTime.Begin.TotalSeconds;
                    ret = current.AddSeconds(seconds);
                }
                else
                {
                    //TODO Performance optimization required.
                }
            }
            return ret;
        }

        /// <summary>
        /// Release object and sources
        /// </summary>
        public void Dispose()
        {
            Holidays.Clear();
            WorkTimes.Clear();
        }
    }

}
