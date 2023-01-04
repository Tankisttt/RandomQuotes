namespace RandomQuotes.Contract.Common;

/// <summary>
/// Paging Settings
/// </summary>
public class PagingSettings
{
    private const int MaxLimit = 30;

    private int _mLimit = MaxLimit;

    /// <summary>
    /// Limit
    /// </summary>
    public int? Limit
    {
        get => _mLimit;
        set
        {
            var val = value ?? MaxLimit;
            _mLimit = val is >= MaxLimit or <= 0 ? MaxLimit : val;
        }
    }

    /// <summary>
    /// Offset
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    /// Sort Column
    /// </summary>
    public string SortColumn { get; set; }
}