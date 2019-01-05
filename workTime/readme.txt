workTime 1.0.0

Examples:
    csharp
        using(var wtc = new WorkTimeCalculator())
        {
            //Getting Turkey holidays
            var TRHolidays = wtc.GetDefaultHolidaysForTR(DateTime.Now.Year);
            wtc.AddHolidays(TRHolidays);

            //Or adding custom holiday
            var customHoliday = new Holiday(HolidayTypeEnum.Other, new DateTime(2012, 12, 12), new DateTime(2012, 12, 12, 23, 59, 59), "Custom event");
            wtc.AddHoliday(customHoliday);

            //Adding work times
            var workTimes = new WorkTime[]
                {
                    new WorkTime()
                    {
                        DisplayName = "Work AM",
                        Begin = TimeSpan.FromHours(8),
                        End = TimeSpan.FromHours(12),
                        WeekDays = new DayOfWeek[]
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
                        WeekDays = new DayOfWeek[]
                        {
                            DayOfWeek.Monday,
                            DayOfWeek.Tuesday,
                            DayOfWeek.Wednesday,
                            DayOfWeek.Thursday,
                            DayOfWeek.Friday
                        }
                    }
                };
            wtc.AddWorkTimes(workTimes);

            //Add custom work time
            var customWorkTime = new WorkTime(21, 0, 23, 59, "Night work", DayOfWeek.Monday);
            wtc.AddWorkTime(customWorkTime);

            //Define test date
            DateTime testDate;

            //Check work time
            testDate = new Date(2019, 1, 2, 9, 15, 0);
            WorkTime workTime = null;
            if(wtc.TryFindWorkTime(testDate, out workTime))
            {
                Debug.WriteLine($"Test date ({testDate}) is work time ({workTime}).");
            }
            else
            {
                Debug.WriteLine($"Test date ({testDate}) is NOT work time.");
            }

            //Check holiday
            testDate = new Date(2019, 1, 1);
            Holiday holiday = null;
            if(wtc.TryFindHoliday(testDate, out holiday))
            {
                Debug.WriteLine($"Test date ({testDate}) is holiday ({holiday}).");
            }
            else
            {
                Debug.WriteLine($"Test date ({testDate}) is NOT holiday.");
            }
        }