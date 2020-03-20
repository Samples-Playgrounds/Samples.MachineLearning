
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

System.Net.Http.HttpClient http_client = new System.Net.Http.HttpClient();

int index = 1;

foreach(string url_json in urls_json)
{
    Information($"Url = {url_json}");

    System.Uri url = new System.Uri(url_json);

    string result = null;

    try
    {
        result = await http_client.GetStringAsync(url);        
    }
    catch (Exception ex)
    {
        result = $"Error {ex.ToString()}";
    }

    Information($"Response = {System.Environment.NewLine}{result}");

    string path_json =  System.IO.Path.Combine
                                        (
                                            System.Environment.CurrentDirectory,
                                            "covidtracking.com",
                                            "json"
                                        );

    if ( ! System.IO.Directory.Exists(path_json))
    {
        System.IO.Directory.CreateDirectory(path_json);
    }
    System.IO.File.WriteAllText
                            (
                                System.IO.Path.Combine
                                                (
                                                    path_json,
                                                    $"{index}.json"
                                                ),
                                result
                            );
    index++;

} 


http_client.Dispose();

System.Threading.Tasks.Task.WaitAll();