namespace LINGYUN.Abp.OpenObserve.Models.Search;

/// <summary>
/// Search source
/// </summary>
/// <remarks>
/// Documentation gives ui/dashboards/reports/alerts, implementation also accepts values/other/rum/
/// derived_stream / search_job / download / insights, illegal value returns 400
/// </remarks>
public enum SearchType
{
    UI = 0,
    Dashboards = 1,
    Reports = 2,
    Alerts = 3,
    Values = 4,
    Other = 5,
    Rum = 6,
    DerivedStream = 7,
    SearchJob = 8,
    Download = 9,
    Insights = 10,
}

public static class SearchTypeExtensions
{
    /// <summary>
    /// Convert to the string value required by the interface
    /// </summary>
    public static string AsString(this SearchType searchType)
    {
        return searchType switch
        {
            SearchType.Dashboards => "dashboards",
            SearchType.Reports => "reports",
            SearchType.Alerts => "alerts",
            SearchType.Values => "values",
            SearchType.Other => "other",
            SearchType.Rum => "rum",
            SearchType.DerivedStream => "derived_stream",
            SearchType.SearchJob => "search_job",
            SearchType.Download => "download",
            SearchType.Insights => "insights",
            _ => "ui",
        };
    }
}
