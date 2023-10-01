
namespace UFE;

public class CurrentDateProvider : ICurrentDateProvider
{
    public DateTime GetCurrentDate()
    {
        return DateTime.Now;
    }
}