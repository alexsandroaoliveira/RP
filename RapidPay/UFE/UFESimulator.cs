namespace UFE;

public class UFESimulator : IUFEClient
{
    private readonly ICurrentDateProvider _currentDateProvider;
    private decimal fee = -1;
    private DateTime feeCreatedDate = new(1990, 1, 1);

    public UFESimulator(ICurrentDateProvider currentDateProvider)
    {
        _currentDateProvider = currentDateProvider;
    }

    public decimal GetCurrentFee()
    {
        var currentDate = _currentDateProvider.GetCurrentDate();
        if (currentDate.Day != feeCreatedDate.Day || currentDate.Hour != feeCreatedDate.Hour)
        {
            fee = (decimal)new Random().Next(200) / 100;
            feeCreatedDate = currentDate;
        }

        return fee;
    }
}