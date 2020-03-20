/*
https://github.com/CSSEGISandData/COVID-19
*/

using System;
Information("github.com-CSSEGISandData-COVID-19/build.cake");

string url_root = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master";

DateTime date_begin = new DateTime(2020, 01, 22);
DateTime date_end = DateTime.Now;

string date = "MM-dd-yyyy";
string date_format = "MM-dd-yyyy";

string url_pattern = $"{url_root}/csse_covid_19_data/csse_covid_19_daily_reports/{date}.csv";
