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
        result = "Error " + ex.ToString();
    }

    Information($"Response = {System.Environment.NewLine}{result}");
} 

foreach(string url_csv in urls_csv)
{
    Information($"Url = {url_csv}");

    Uri url = new Uri(url_csv);

    string result = null;
    try
    {
        result = await http_client.GetStringAsync(url);        
    }
    catch (Exception ex)
    {
        result = "Error " + ex.ToString();
    }

    Information($"Response = {System.Environment.NewLine}{result}");
} 

{
// Folder, where a file is created.  
// Make sure to change this folder to your own folder  
string folder = @"/Users/darko/Documents";  
// Filename  
string fileName = "Results.txt";  
// Fullpath. You can direct hardcode it if you like.  
string fullPath = folder + fileName;  
// An array of strings  
string[] result;  
// Write array of strings to a file using WriteAllLines.  
// If the file does not exists, it will create a new file.  
// This method automatically opens the file, writes to it, and closes file  
File.WriteAllLines(fullPath, result);  
// Read a file  
string readText = File.ReadAllText(fullPath);  
Console.WriteLine(readText);  
}

http_client.Dispose();

System.Threading.Tasks.Task.WaitAll();