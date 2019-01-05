using System;
using System.Collections.Generic;

namespace workTime
{

    /// <summary>
    /// WorkTimeCalculator helpers
    /// <para lang="tr">Çalışma zamanı yardımcıları</para>
    /// </summary>
    public static class WorkTimeCalculatorHelpers
    {

        /// <summary>
        /// Türkiye için o yıla ait tatilleri verir, dini tatilleri hesaplar
        /// <para>2429 sayılı kanun : http://www.mevzuat.gov.tr/MevzuatMetin/1.5.2429.pdf </para>
        /// <para>Wikizeroo : http://www.wikizeroo.net/index.php?q=aHR0cHM6Ly90ci53aWtpcGVkaWEub3JnL3dpa2kvVMO8cmtpeWUnZGVraV9yZXNtw65fdGF0aWxsZXI </para>
        /// <para>Hicri takfim : https://www.yazilimkodlama.com/programlama/c-miladi-hicri-takvim-cevirici/ </para>
        /// </summary>
        public static ICollection<Holiday> GetDefaultHolidaysForTR(this WorkTimeCalculator wtc, int year)
        {
            var ret = new List<Holiday>();
            ret.Add(new Holiday(HolidayTypeEnum.Formal, new DateTime(year, 1, 1), new DateTime(year, 1, 1, 23, 59, 59), "Yılbaşı"));
            ret.Add(new Holiday(HolidayTypeEnum.National, new DateTime(year, 4, 23), new DateTime(year, 4, 23, 23, 59, 59), "Ulusal Egemenlik ve Çocuk Bayramı"));
            ret.Add(new Holiday(HolidayTypeEnum.Formal, new DateTime(year, 5, 1), new DateTime(year, 5, 1, 23, 59, 59), "Emek ve Dayanışma Günü"));
            ret.Add(new Holiday(HolidayTypeEnum.National, new DateTime(year, 5, 19), new DateTime(year, 5, 19, 23, 23, 59, 59), "Atatürk'ü Anma, Gençlik ve Spor Bayramı"));
            ret.Add(new Holiday(HolidayTypeEnum.National, new DateTime(year, 7, 15), new DateTime(year, 7, 15, 23, 23, 59, 59), "Demokrasi ve Millî Birlik Günü"));
            ret.Add(new Holiday(HolidayTypeEnum.National, new DateTime(year, 8, 30), new DateTime(year, 8, 30, 23, 23, 59, 59), "Zafer Bayramı"));
            ret.Add(new Holiday(HolidayTypeEnum.National, new DateTime(year, 10, 28, 13, 0, 0), new DateTime(year, 10, 29, 23, 23, 59, 59), "Cumhuriyet Bayramı"));

            var HicriMonths = new string[] { "Muharrem", "Sefer", "Rebiül Evvel", "Rebiül Ahir", "Rebiül Ahir", "Recep", "Şaban", "Ramazan", "Şevval", "Zilkadde", "Zilhicce" };
            //Hicri takvime göre 9. ay Ramazan
            var HicriCulture = System.Globalization.CultureInfo.GetCultureInfo("ar-SA");
            var HicriBeginYear = int.Parse((new DateTime(year, 1, 1)).ToString("yyyy", HicriCulture));
            var HicriEndYear = int.Parse((new DateTime(year, 12, 31)).ToString("yyyy", HicriCulture));
            for (int HicriYear = HicriBeginYear; HicriYear < HicriEndYear; HicriYear++)
            {
                var RamazanHolidayBegin = DateTime.Parse("01/10/" + HicriYear.ToString()).AddDays(-1).AddHours(13);
                ret.Add(new Holiday(HolidayTypeEnum.Religious, RamazanHolidayBegin, RamazanHolidayBegin.AddHours(-13).AddDays(4), "Ramazan bayramı"));
                var KurbanHolidayBegin = RamazanHolidayBegin.AddDays(70);
                ret.Add(new Holiday(HolidayTypeEnum.Religious, KurbanHolidayBegin, KurbanHolidayBegin.AddHours(-13).AddDays(5), "Kurban bayramı"));
            }
            return ret;
        }
    }

}