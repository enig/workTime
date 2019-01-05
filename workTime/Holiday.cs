using System;
using System.Text;

namespace workTime
{
    /// <summary>
    /// Holiday
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// Create new Holiday
        /// </summary>
        public Holiday()
        {

        }
        
        /// <summary>
        /// Create new Holiday
        /// </summary>
        /// <param name="type">Holiday type</param>
        /// <param name="begin">Holiday begin date</param>
        /// <param name="end">Holiday end date</param>
        /// <param name="displayName">Holiday display name</param>
        /// <returns></returns>
        public Holiday(HolidayTypeEnum type, DateTime begin, DateTime end, string displayName) : this()
        {
            Type = type;
            Begin = begin;
            End = end;
            DisplayName = displayName;
        }

        /// <summary>
        /// Holiday begin date
        /// </summary>
        /// <value></value>
        public DateTime Begin { get; set; }

        /// <summary>
        /// Holiday end date
        /// </summary>
        /// <value></value>
        public DateTime End { get; set; }
        
        /// <summary>
        /// Holiday display name
        /// </summary>
        /// <value></value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Holiday type
        /// </summary>
        /// <value></value>
        public HolidayTypeEnum Type { get; set; }

        /// <summary>
        /// For humman read
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder("Holiday");
            sb.Append( " Type: ").Append(Type)
            .Append(" Begin: ").Append(Begin.ToString("dd.MM.yyyy HH:mm:ss"))
            .Append(" End: ").Append(End.ToString("dd.MM.yyyy HH:mm:ss"))
            .Append(" DisplayName: ").Append(DisplayName);
            return sb.ToString();
        }
    }

}