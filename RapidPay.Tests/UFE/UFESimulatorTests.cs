using Moq;
using UFE;

namespace RapidPay.Tests.UFE;

public class UFESimulatorTests
{
    [Fact]
    public void GetCurrentFee()
    {
        // Arrange
        var dateTimes = new Queue<DateTime>();
        dateTimes.Enqueue(new DateTime(2023, 1, 1, 10, 0, 0)); // Initial
        dateTimes.Enqueue(new DateTime(2023, 1, 1, 10, 1, 0)); // Same hour from previous
        dateTimes.Enqueue(new DateTime(2023, 1, 1, 11, 0, 0)); // Different hour from previous
        dateTimes.Enqueue(new DateTime(2023, 1, 2, 11, 0, 0)); // Different day from previous
        dateTimes.Enqueue(new DateTime(2023, 1, 3, 20, 0, 0)); // Different day and hour from previous

        var dateProvider = new Mock<ICurrentDateProvider>();
        dateProvider.Setup(o => o.GetCurrentDate()).Returns(() => dateTimes.Dequeue());

        UFESimulator sim = new UFESimulator(dateProvider.Object);

        // Act
        var fee1 = sim.GetCurrentFee();
        var fee2 = sim.GetCurrentFee();
        var fee3 = sim.GetCurrentFee();
        var fee4 = sim.GetCurrentFee();
        var fee5 = sim.GetCurrentFee();

        // Assert
        Assert.True(fee1 >= 0 && fee1 <= 2); // minimal fee should be zero, maximum fee should be two for all fees. 
        Assert.True(fee2 >= 0 && fee2 <= 2);
        Assert.True(fee3 >= 0 && fee3 <= 2);
        Assert.True(fee4 >= 0 && fee4 <= 2);
        Assert.True(fee5 >= 0 && fee5 <= 2);

        Assert.Equal(fee1, fee2);   // Should have the same fee, both retrived in the same hour
        Assert.NotEqual(fee2, fee3); // Should be different fee, retrived on different hour
        Assert.NotEqual(fee3, fee4); // Should be different fee, retrived on different day
        Assert.NotEqual(fee4, fee5); // Should be different fee, retrived on different day and hour
        dateProvider.Verify(o => o.GetCurrentDate(), Times.Exactly(5));
    }
}
