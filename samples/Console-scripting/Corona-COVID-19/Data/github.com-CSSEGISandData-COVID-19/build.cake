using System.Data;

/*
https://github.com/CSSEGISandData/COVID-19
*/

using System;
Information("github.com-CSSEGISandData-COVID-19/build.cake");

string url_root = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master";

string date = "MM-dd-yyyy";
string date_format = "MM-dd-yyyy";

string url_pattern = $"{url_root}/csse_covid_19_data/csse_covid_19_daily_reports/{date}.csv";

DateTime date_begin = new DateTime(2020, 01, 22);
DateTime date_end = DateTime.Now;
IEnumerable<DateTime> days_reported = Days(date_begin, date_end);

foreach(DateTime day_reported in days_reported)
{    
    Information($"Day reported: {day_reported.ToString("yyyy-MM-dd")}");
    string date_format_reported = day_reported.ToString("MM-dd-yyyy");
    Information($"    format: {date_format_reported}");
}


public IEnumerable<DateTime> Days(DateTime from, DateTime thru)
{
    for(var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
    {
        yield return day;
    }
}