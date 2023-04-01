namespace web_api.Common.Extensions;

internal static class StringExtensions
{
    public enum SizeUnits
    {
        Byte, KB, MB, GB, TB, PB, EB, ZB, YB
    }

    public static string ToSize(this Int64 value, SizeUnits unit)
    {
        return (value / (double)Math.Pow(1024, (Int64)unit)).ToString("0.00");
    }

    public static string ToSizeDynamic(this Int64 value)
    {
        var sizeUnit = SizeUnits.KB;
        string resultText = string.Empty;

        bool check = true;

        while (check)
        {
            resultText = value.ToSize(sizeUnit);
            var result = decimal.Parse(resultText);

            if (result > 1000)
            {
                sizeUnit++;
            }
            else
            {
                check = false;
            }
        }

        return $"{resultText} {sizeUnit.ToString()}";
    }
}