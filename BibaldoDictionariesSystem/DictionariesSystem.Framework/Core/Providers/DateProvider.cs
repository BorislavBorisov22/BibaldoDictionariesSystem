using System;
using DictionariesSystem.Contracts.Core.Providers;

namespace DictionariesSystem.Framework.Core.Providers
{
    public class DateProvider : IDateProvider
    {
        public static IDateProvider Provider { get; set; } = new DateProvider();

        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
