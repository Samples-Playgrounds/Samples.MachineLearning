
Information("github.com-owid-covid-19-data/build.cake");

using System.IO;
using System;


string[] urls_csv = 
{
    "https://raw.githubusercontent.com/owid/covid-19-data/master/public/data/testing/covid-testing-all-observations.csv",
    "https://raw.githubusercontent.com/owid/covid-19-data/master/public/data/testing/covid-testing-latest-data-source-details.csv",
    "",
};

string path_csv =  System.IO.Path.Combine
                                    (
                                        System.Environment.CurrentDirectory,
                                        "github.com-owid-covid-19-data",
                                        "csv"
                                    );


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