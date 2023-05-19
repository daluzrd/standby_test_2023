namespace SharedKernel.Utils;

public static class DateTimeUtils
{
    public static string GetBrazilianDateString(DateTime date)
    {
        return $"{date.Day}/{date.Month.ToString("D2")}/{date.Year}";
    }
}