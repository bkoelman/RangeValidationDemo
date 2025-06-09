using System.ComponentModel.DataAnnotations;

namespace RangeDemo;

public sealed class ExampleModel
{
    [Range(typeof(Fraction),
#if PARSE_RANGE_CONSTRAINTS_USING_CURRENT_CULTURE
        "1:10", "1:2",
#else
        "1/10", "1/2", ParseLimitsInInvariantCulture = true,
#endif
        MaximumIsExclusive = true, ErrorMessage = "Please specify a fraction in range 1/10 (= 0.1) to 1/2 (= 0.5)")]
    public Fraction? Fraction { get; set; }

    [Range(typeof(double), "11.1", "22.2", ParseLimitsInInvariantCulture = true)]
    public double? SomeNumber1 { get; set; }

    [Range(33.3, 44.4)]
    public double? SomeNumber2 { get; set; }
}
