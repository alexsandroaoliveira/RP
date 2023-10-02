namespace UFE;

/// <summary>
/// UFE Simulator to generate and return the current Fee.
/// </summary>
public interface IUFEClient
{
    /// <summary>
    /// Get Current Fee
    /// </summary>
    /// <returns>Current Fee</returns>
    decimal GetCurrentFee();
}