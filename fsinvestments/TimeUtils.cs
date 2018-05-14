using System;
namespace fsinvestments
{
    public static class TimeUtils
    {
        public static DateTime GetStartOfMonth()
        {
            return DateTime.Today.AddDays(-(DateTime.Today.Day - 1));
        }

        public static DateTime GetStartOfQuarter()
        {
            switch (DateTime.Today.Month)
            {
                case 1:
                case 2:
                case 3:
                    { return DateTime.Parse("01/01/" + DateTime.Today.Year); }
                case 4:
                case 5:
                case 6:
                    { return DateTime.Parse("04/01/" + DateTime.Today.Year); }
                case 7:
                case 8:
                case 9:
                    { return DateTime.Parse("07/01/" + DateTime.Today.Year); }
                case 10:
                case 11:
                case 12:
                    { return DateTime.Parse("10/01/" + DateTime.Today.Year); }
                default:
                    return DateTime.Parse("01/01/" + DateTime.Today.Year);
            }
        }

        public static DateTime GetStartOfYear()
        {
            return DateTime.Parse("01/01/" + DateTime.Today.Year);
        }

    }
}
