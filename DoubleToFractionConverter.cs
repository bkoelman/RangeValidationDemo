namespace RangeDemo;

// From https://stackoverflow.com/questions/5124743/algorithm-for-simplifying-decimal-to-fractions/45314258#45314258
public static class DoubleToFractionConverter
{
    public static Fraction DoubleToFraction(double value, double accuracy = 0.01)
    {
        int sign = value < 0 ? -1 : 1;
        value = value < 0 ? -value : value;
        int integerPart = (int)value;
        value -= integerPart;
        double minimalValue = value - accuracy;
        if (minimalValue < 0.0) return new Fraction(sign * integerPart, 1);
        double maximumValue = value + accuracy;
        if (maximumValue > 1.0) return new Fraction(sign * (integerPart + 1), 1);
        int a = 0;
        int b = 1;
        int c = 1;
        int d = (int)(1 / maximumValue);
        while (true)
        {
            int n = (int)((b * minimalValue - a) / (c - d * minimalValue));
            if (n == 0) break;
            a += n * c;
            b += n * d;
            n = (int)((c - d * maximumValue) / (b * maximumValue - a));
            if (n == 0) break;
            c += n * a;
            d += n * b;
        }

        int denominator = b + d;
        return new Fraction(sign * (integerPart * denominator + (a + c)), denominator);
    }
}
