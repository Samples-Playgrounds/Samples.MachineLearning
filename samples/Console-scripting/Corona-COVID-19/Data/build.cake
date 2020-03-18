
using System.Threading.Tasks;
using System.Reflection;
string[] urls_json = 
{
    "https://covidtracking.com/api/states",
    "https://covidtracking.com/api/states/daily",
    "https://covidtracking.com/api/states/info",
    "https://covidtracking.com/api/states/us",
    "https://covidtracking.com/api/states/us/daily",
    "https://covidtracking.com/api/counties",
    "https://covidtracking.com/api/urls",
};

string[] urls_csv = 
{
    "https://covidtracking.com/api/states.csv",
    "https://covidtracking.com/api/states/daily.csv",
    "https://covidtracking.com/api/states/info.csv",
    "https://covidtracking.com/api/states/us.csv",
    "https://covidtracking.com/api/states/us/daily.csv",
    "https://covidtracking.com/api/counties.csv",
    "https://covidtracking.com/api/urls.csv",
};

System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient();

foreach(string url_csv in urls_csv)
{
    Information($"Url = {url_csv}");

    Uri url = new Uri(url_csv);

    string result = null;
    try
    {
        result = await hc.GetStringAsync(url);        
    }
    catch (Exception ex)
    {
        result = "Error " + ex.ToString();
    }

    Information($"Response = {System.Environment.NewLine}{result}");
} 

hc.Dispose();

System.Threading.Tasks.Task.WaitAll();