
Information("covidtracking.com/build.cake");

using System.IO;
using System;

string[] urls_json = 
{
    "https://covidtracking.com/api/states",
    "https://covidtracking.com/api/states/daily",
    "https://covidtracking.com/api/states/info",
    "https://covidtracking.com/api/us",
    "https://covidtracking.com/api/us/daily",
    "https://covidtracking.com/api/counties",
    "https://covidtracking.com/api/urls",
};

string[] urls_csv = 
{
    "https://covidtracking.com/api/states.csv",
    "https://covidtracking.com/api/states/daily.csv",
    "https://covidtracking.com/api/states/info.csv",
    "https://covidtracking.com/api/us.csv",
    "https://covidtracking.com/api/us/daily.csv",
    "https://covidtracking.com/api/counties.csv",
    "https://covidtracking.com/api/urls.csv",
};

string path_json =  System.IO.Path.Combine
                                    (
                                        System.Environment.CurrentDirectory,
                                        "covidtracking.com",
                                        "json"
                                    );
string path_csv =  System.IO.Path.Combine
                                    (
                                        System.Environment.CurrentDirectory,
                                        "covidtracking.com",
                                        "csv"
                                    );

if ( ! System.IO.Directory.Exists(path_json))
{
    System.IO.Directory.CreateDirectory(path_json);
}

System.Net.Http.HttpClient http_client_1 = new System.Net.Http.HttpClient();

foreach(string url_json in urls_json)
{
    Information($"Url = {url_json}");

    System.Uri url = new System.Uri(url_json);

    string result = null;

    try
    {
        result = await http_client_1.GetStringAsync(url);        
    }
    catch (Exception ex)
    {
        result = $"Error {ex.ToString()}";
    }

    // Information($"Response = {System.Environment.NewLine}{result}");

    string file_name = url_json.Replace(@"https://covidtracking.com/api/", "");
    file_name = $"{file_name}.json";
    file_name = file_name.Replace(@"/", "_");

    System.IO.File.WriteAllText
                            (
                                System.IO.Path.Combine
                                                (
                                                    path_json,
                                                    file_name
                                                ),
                                result
                            );
} 

if ( ! System.IO.Directory.Exists(path_csv))
{
    System.IO.Directory.CreateDirectory(path_csv);
}

int index = 1;

foreach(string url_csv in urls_csv)
{
    Information($"Url = {url_csv}");

    System.Uri url = new System.Uri(url_csv);

    string result = null;

    try
    {
        result = await http_client_1.GetStringAsync(url);        
    }
    catch (Exception ex)
    {
        result = $"Error {ex.ToString()}";
    }

    //Information($"Response = {System.Environment.NewLine}{result}");

    System.IO.File.WriteAllText
                            (
                                System.IO.Path.Combine
                                                (
                                                    path_csv,
                                                    $"{index}.csv"
                                                ),
                                result
                            );
    index++;
} 


http_client_1.Dispose();

System.Threading.Tasks.Task.WaitAll();