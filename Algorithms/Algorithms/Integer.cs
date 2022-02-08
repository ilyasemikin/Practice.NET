namespace Algorithms;

public static class Integer
{
    public static int NumberOfDigits(long num)
    {
        return num switch
        {
            0 => 1,
            long.MinValue => 19,
            _ => (int) Math.Log10(Math.Abs(num)) + 1
        };
    }
}