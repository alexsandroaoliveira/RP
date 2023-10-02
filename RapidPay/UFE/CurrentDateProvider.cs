
namespace UFE;

/// <summary>
/// Current Date Provider. Isolated to allow unit testing on UFESimulator.
/// </summary>
public class CurrentDateProvider : ICurrentDateProvider
{
    /// <summary>
    /// Get Current Date
    /// </summary>
    /// <returns>Current Date</returns>
    public DateTime GetCurrentDate() => DateTime.Now;
}