using System.Diagnostics.CodeAnalysis;

namespace MicroDotNet.Packages.Cqrs.UnitTests.MessageLevelTests;

public class UniquenessTests
{
    [Fact]
    public void EachMemberOfEnumerationShouldHaveValueThatIsPowerOfTwo()
    {
        var values = Enum.GetValues<MessageLevel>();
        foreach (var current in values)
        {
            var numberRepresentation = (int)current;
            Assert.True(IsPowerOfTwo(numberRepresentation), $"'{current}' value ({numberRepresentation}) is not power of two.");
        }
    }
    
    private static bool IsPowerOfTwo(int n)
    {
         return n >= 0 && (n & (n - 1)) == 0;
    }
}