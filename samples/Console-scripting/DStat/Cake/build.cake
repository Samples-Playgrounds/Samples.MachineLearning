Information("Datasets for teaching");

using System.IO;
using System;

string[] urls_csv = 
{
    "https://www.sheffield.ac.uk/polopoly_fs/1.814978!/file/stcp-Rdataset-NormR.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.569443!/file/stcp-Rdataset-Titanic.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.814977!/file/birthweight_reduced.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.570199!/file/stcp-Rdataset-Diet.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.569434!/file/stcp-Rdataset-Crime.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.569449!/file/stcp-Rdataset-Video.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.569432!/file/stcp-Rdataset-Cholesterol.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.760286!/file/stcp-Rdataset-ice_cream.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.760287!/file/stcp-Rdataset-smoker.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.761681!/file/awards.csv",
    "https://www.sheffield.ac.uk/polopoly_fs/1.762417!/file/Graduate.csv",
};

string path_csv =  System.IO.Path.Combine
                                    (
                                        System.Environment.CurrentDirectory,
                                        "sheffield.ac.uk",
                                        "csv"
                                    );

 if ( ! System.IO.Directory.Exists(path_csv))
{
    System.IO.Directory.CreateDirectory(path_csv);
}

System.Net.Http.HttpClient http_client_1 = new System.Net.Http.HttpClient();

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