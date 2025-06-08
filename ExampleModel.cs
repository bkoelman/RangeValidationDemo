using System.ComponentModel.DataAnnotations;

namespace RangeDemo;

public sealed class ExampleModel
{
    [Range(typeof(Fraction),
#if PARSE_RANGE_CONSTRAINTS_USING_SYSTEM_CULTURE
        "0:1", "1:1",
#else
        "0/1", "1/1", ParseLimitsInInvariantCulture = true,
#endif
        MaximumIsExclusive = true, ErrorMessage = "Please specify a positive fraction lower than 1.")]
    public Fraction? Fraction { get; set; }
}
