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

string path_daily_csv =  System.IO.Path.Combine
                                    (
                                        System.Environment.CurrentDirectory,
                                        "github.com-CSSEGISandData-COVID-19",
                                        "daily-csv"
                                    );

if ( ! System.IO.Directory.Exists(path_daily_csv))
{
    System.IO.Directory.CreateDirectory(path_daily_csv);
}

System.Net.Http.HttpClient http_client_2 = new System.Net.Http.HttpClient();

DateTime date_begin = new DateTime(2020, 01, 22);
DateTime date_end = DateTime.Now;
IEnumerable<DateTime> days_reported = Days(date_begin, date_end);

foreach(DateTime day_reported in days_reported)
{    
    Information($"Day reported: {day_reported.ToString("yyyy-MM-dd")}");
    string date_format_reported = day_reported.ToString("MM-dd-yyyy");
    Information($"    format: {date_format_reported}");

    string url_full = url_pattern.Replace("MM-dd-yyyy", date_format_reported);

    System.Uri url = new System.Uri(url_full);

    string result = null;

    try
    {
        result = await http_client_2.GetStringAsync(url);        
    }
    catch (Exception ex)
    {
        result = $"Error {ex.ToString()}";
    }

    string file_name = $"{day_reported.ToString("yyyy-MM-dd")}.csv";
    System.IO.File.WriteAllText
                            (
                                System.IO.Path.Combine
                                                (
                                                    path_daily_csv,
                                                    file_name
                                                ),
                                result
                            );
}

public IEnumerable<DateTime> Days(DateTime from, DateTime thru)
{
    for(DateTime day = from.Date; day.Date < thru.Date; day = day.AddDays(1))
    {
        yield return day;
    }
}

http_client_2.Dispose();
