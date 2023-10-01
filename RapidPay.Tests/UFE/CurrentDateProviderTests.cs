using UFE;

namespace RapidPay.Tests.UFE;

public class CurrentDateProviderTests
{
    [Fact]
    public void GetCurrentDateTests()
    {
        // Arrange
        var currentDateProvider = new CurrentDateProvider(); 
        var currentDate = DateTime.Now;

        // Act
        var date = currentDateProvider.GetCurrentDate();

        // Assert
        var diff = currentDate - date;
        Assert.True(diff <= TimeSpan.FromSeconds(1));
    }
}