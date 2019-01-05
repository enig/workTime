namespace workTime
{
    /// <summary>
    /// Holiday types
    /// <para lang="tr">Tatil tipleri</para>
    /// </summary>
    public enum HolidayTypeEnum
    {
        
        /// <summary>
        /// Unspecified
        /// <para lang="tr">Belirtilmemiş</para>
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// General or global holiday
        /// <para lang="tr">Genel tatil</para>
        /// </summary>
        General,

        /// <summary>
        /// Formal holiday type
        /// <para lang="tr">Resmî</para>
        /// </summary>
        Formal,

        /// <summary>
        /// Religious holiday type
        /// <para lang="tr">Dinî</para>
        /// </summary>
        Religious,

        /// <summary>
        /// National holiday
        /// <para lang="tr">Ulusal tatil</para>
        /// </summary>
        National,

        /// <summary>
        /// Other
        /// <para lang="tr">Diğer</para>
        /// </summary>
        Other
    }
}