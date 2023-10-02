namespace UFE;

/// <summary>
/// Current Date Provider. Isolated to allow unit testing on UFESimulator.
/// </summary>
public interface ICurrentDateProvider
{
    /// <summary>
    /// Get Current Date
    /// </summary>
    /// <returns>Current Date</returns>
    DateTime GetCurrentDate();
}