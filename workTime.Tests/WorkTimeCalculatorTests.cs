using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using workTime;

namespace workTime.Tests
{
    [TestClass]
    public class WorkTimeCalculatorTests
    {
        private WorkTimeCalculator calculator;

        [TestInitialize]
        public void Init()
        {
            calculator = new WorkTimeCalculator();
            var TrHolidays = calculator.GetDefaultHolidaysForTR(DateTime.Now.Year);
            IEnumerable<WorkTime> workTimes = new WorkTime[]
                {
                    new WorkTime()
                    {
                        DisplayName = "Work AM",
                        Begin = TimeSpan.FromHours(8),
                        End = TimeSpan.FromHours(12),
                        DaysOfWeek = new DayOfWeek[]
                        {
                            DayOfWeek.Monday,
                            DayOfWeek.Tuesday,
                            DayOfWeek.Wednesday,
                            DayOfWeek.Thursday,
                            DayOfWeek.Friday
                        }
                    },
                    new WorkTime()
                    {
                        DisplayName = "Work PM",
                        Begin = TimeSpan.FromHours(13) + TimeSpan.FromMinutes(30),
                        End = TimeSpan.FromHours(18) + TimeSpan.FromMinutes(30),
                        DaysOfWeek = new DayOfWeek[]
                        {
                            DayOfWeek.Monday,
                            DayOfWeek.Tuesday,
                            DayOfWeek.Wednesday,
                            DayOfWeek.Thursday,
                            DayOfWeek.Friday
                        }
                    }
                };
            // foreach (var item in TrHolidays)
            // {
            //     Trace.WriteLine(string.Format("Holiday {0,-10} {3,-40} {1} - {2}", item.Type, item.Begin.ToString("dd.MM.yyyy HH:mm.ss"), item.End.ToString("dd.MM.yyyy HH:mm.ss"), item.DisplayName));
            // }
            // foreach (var item in workTimes)
            // {
            //     Trace.WriteLine(string.Format("WorkTime {0} - {1} Days: {2}", item.Begin, item.End, string.Join(", ", item.WeekDays.Select(e => e.ToString()))));
            // }
            calculator.AddHolidays(TrHolidays);
            calculator.AddWorkTimes(workTimes);
        }

        [TestCleanup]
        public void Clean()
        {
            calculator.Dispose();
        }

        [TestMethod]
        public void WorkTimeCalculatorTest()
        {
            using (var wtc = new WorkTimeCalculator())
            {
                var TrHolidays = wtc.GetDefaultHolidaysForTR(DateTime.Now.Year);

                IEnumerable<WorkTime> workTimes = new WorkTime[]
                {
                    new WorkTime()
                    {
                        DisplayName = "Work AM",
                        Begin = TimeSpan.FromHours(8),
                        End = TimeSpan.FromHours(12),
                        DaysOfWeek = new DayOfWeek[]
                        {
                            DayOfWeek.Monday,
                            DayOfWeek.Tuesday,
                            DayOfWeek.Wednesday,
                            DayOfWeek.Thursday,
                            DayOfWeek.Friday
                        }
                    },
                    new WorkTime()
                    {
                        DisplayName = "Work PM",
                        Begin = TimeSpan.FromHours(13) + TimeSpan.FromMinutes(30),
                        End = TimeSpan.FromHours(18) + TimeSpan.FromMinutes(30),
                        DaysOfWeek = new DayOfWeek[]
                        {
                            DayOfWeek.Monday,
                            DayOfWeek.Tuesday,
                            DayOfWeek.Wednesday,
                            DayOfWeek.Thursday,
                            DayOfWeek.Friday
                        }
                    }
                };
                foreach (var item in TrHolidays)
                {
                    Trace.WriteLine(string.Format("Holiday {0,-10} {3,-40} {1} - {2}", item.Type, item.Begin.ToString("dd.MM.yyyy HH:mm.ss"), item.End.ToString("dd.MM.yyyy HH:mm.ss"), item.DisplayName));
                }
                foreach (var item in workTimes)
                {
                    Trace.WriteLine(string.Format("WorkTime {0} - {1} Days: {2}", item.Begin, item.End, string.Join(", ", item.DaysOfWeek.Select(e => e.ToString()))));
                }
                wtc.AddHolidays(TrHolidays);
                wtc.AddWorkTimes(workTimes);

                var holidayDate = DateTime.Parse("01.01.2019 15:10");
                var workDate = DateTime.Parse("02.01.2019 15:10");
                var beginDate = DateTime.Parse("01.01.2019");
                var endDate = DateTime.Parse("01.01.2020");
                var sw = new Stopwatch();
                Holiday holiday = null;
                WorkTime workTime = null;

                Trace.Write("TryFindHoliday check holiday in holiday ");
                holiday = null;
                sw.Start();
                var isHoliday = wtc.TryFindHoliday(holidayDate.AddSeconds(1), out holiday);
                sw.Stop();
                Trace.Write($"Cost:{sw.ElapsedMilliseconds}ms.\n holiday:{holiday}\n ");
                Assert.IsTrue(isHoliday, "TryFindHoliday check holiday in holiday. Return value must be true.");
                Assert.IsNotNull(holiday, "TryFindHoliday check holiday in holiday fail. Holiday must be NOT null.");
                Trace.WriteLine($"okay.\n Holiday at {holiday}");

                Trace.Write("TryFindHoliday check holiday in work time");
                holiday = null;
                sw.Restart();
                isHoliday = wtc.TryFindHoliday(workDate, out holiday);
                sw.Stop();
                Trace.Write($"Cost:{sw.ElapsedMilliseconds}ms.\n holiday:{holiday}\n ");
                Assert.IsFalse(isHoliday, "TryFindHoliday check holiday in work time. Return value must be false.");
                Assert.IsNull(holiday, "TryFindHoliday check holiday in work time. Holiday must be null.");
                Trace.WriteLine("okay.");

                Trace.Write("TryFindWorkTime check work time in holidayDate ");
                workTime = null;
                sw.Restart();
                var isWorktime = wtc.TryFindWorkTime(holidayDate, out workTime);
                sw.Stop();
                Trace.Write($"Cost:{sw.ElapsedMilliseconds}ms.\n workTime: {workTime}\n ");
                Assert.IsFalse(isWorktime, "TryFindWorkTime check work time in holidayDate fail. Return value must be false.");
                Assert.IsNull(workTime, "TryFindWorkTime check work time in holidayDate fail. Work time must be null.");
                Trace.WriteLine("okay.");

                Trace.Write("TryFindWorkTime check work time in workDate");
                workTime = null;
                sw.Restart();
                isWorktime = wtc.TryFindWorkTime(workDate, out workTime);
                sw.Stop();
                Trace.Write($"Cost:{sw.ElapsedMilliseconds}ms.\n workTime: {workTime}\n ");
                Assert.IsTrue(isWorktime, "TryFindWorkTime check work time in workDate. Return value must be true.");
                Assert.IsNotNull(workTime, "TryFindWorkTime check work time in workDate. Work time must be NOT null.");
                Trace.WriteLine($"okay.\n workTime {workTime}");

                // double calculetedWorkTime = ((12 - 8) + (18 - 13)) * 60 * 60;
                // Trace.Write($"CalculateWorkTime {beginDate.ToString("dd.MM.yyyy HH:mm:ss")} - {endDate.ToString("dd.MM.yyyy HH:mm:ss")} ");
                // sw.Restart();
                // var time = wtc.CalculateWorkTime(beginDate, endDate);
                // sw.Stop();
                // Trace.Write($"Cost:{TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds)} result : {time}");
                // Assert.AreEqual(time.TotalSeconds, calculetedWorkTime, $"CalculateWorkTime {beginDate.ToString("dd.MM.yyyy HH:mm:ss")} - {endDate.ToString("dd.MM.yyyy HH:mm:ss")} fail.");

                sw = null;
            }
        }

        [TestMethod]
        public void WorkTimeCalculatorValidationTest()
        {
            var beginDate = new DateTime(2019, 1, 1);
            var endDate = new DateTime(2019, 1, 2, 13, 0, 0);
            var workTime = calculator.WorkTimes.First(e => e.DisplayName == "Work AM");
            var calculetedWorkTime = workTime.End - workTime.Begin;
            var sw = new Stopwatch();
            Trace.Write($"WorkTimeCalculatorValidationTest {beginDate.ToString("dd.MM.yyyy HH:mm:ss")} - {endDate.ToString("dd.MM.yyyy HH:mm:ss")} ");
            sw.Restart();
            var time = calculator.CalculateWorkTime(beginDate, endDate);
            sw.Stop();
            Trace.Write($"Cost:{TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds)} result : {time}");
            Assert.AreEqual(time, calculetedWorkTime, $"WorkTimeCalculatorValidationTest {beginDate.ToString("dd.MM.yyyy HH:mm:ss")} - {endDate.ToString("dd.MM.yyyy HH:mm:ss")} fail.");
            sw = null;
        }

        [TestMethod]
        public void WorkTimeCalculatorPerformanceTest()
        {
            var maxTime = TimeSpan.FromSeconds(1);
            var beginDate = new DateTime(2019, 1, 1);
            var endDate = new DateTime(2020, 1, 1);
            var sw = new Stopwatch();
            Trace.Write($"WorkTimeCalculatorPerformanceTest {beginDate.ToString("dd.MM.yyyy HH:mm:ss")} - {endDate.ToString("dd.MM.yyyy HH:mm:ss")} ");
            sw.Restart();
            var time = calculator.CalculateWorkTime(beginDate, endDate);
            sw.Stop();
            Trace.Write($"Cost:{TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds)} result : {time}");
            Assert.IsTrue(sw.Elapsed > maxTime, $"Calculate time : {sw.Elapsed} over {maxTime}.");
            sw = null;
            
        }
    }
}
