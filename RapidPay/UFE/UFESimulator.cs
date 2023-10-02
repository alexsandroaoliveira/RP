namespace UFE;

/// <summary>
/// UFE Simulator to generate and return the current Fee.
/// </summary>
public class UFESimulator : IUFEClient
{
    private readonly ICurrentDateProvider _currentDateProvider;
    private decimal fee = -1;
    private DateTime feeCreatedDate = new(1990, 1, 1);

    public UFESimulator(ICurrentDateProvider currentDateProvider)
    {
        _currentDateProvider = currentDateProvider;
    }

    /// <summary>
    /// Get Current Fee
    /// </summary>
    /// <returns>Current Fee</returns>
    public decimal GetCurrentFee()
    {
        // Get Current Date from the provider. On unit test this can be mocked
        var currentDate = _currentDateProvider.GetCurrentDate();

        // Simulate the new fee generation on every hour
        if (currentDate.Day != feeCreatedDate.Day || currentDate.Hour != feeCreatedDate.Hour)
        {
            // Random from 0 to 200 divided by 100, results in a decimal from 0 to 2.
            fee = (decimal)new Random().Next(200) / 100;

            // Set the fee created date to generate a new one after the hour changes.
            feeCreatedDate = currentDate;
        }

        return fee;
    }
}