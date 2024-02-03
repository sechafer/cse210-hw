using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class ListingActivity : Activity
{
    private List<string> listingPrompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Let's list some things...");
        ShowCountDown(duration);

        Random random = new Random();
        string prompt = GetRandomPrompt();
        Console.WriteLine(prompt);

        GetListFromUser();

        DisplayEndingMessage();
    }

    private void GetListFromUser()
    {
        List<string> items = new List<string>();
        Console.WriteLine("Enter items (one per line), press enter twice to finish:");

        string input;
        while (!string.IsNullOrWhiteSpace(input = Console.ReadLine()))
        {
            items.Add(input);
        }

        Console.WriteLine(<span class="math-inline">"You listed \{items\.Count\} items\."\);
\}
protected override List<string\> prompts \=\> listingPrompts;
\}
class ActivityLog
\{
private Dictionary<string, int\> activityCounts \= new Dictionary<string, int\>\(\);
public void LogActivity\(string activityName\)
\{
if \(\!activityCounts\.ContainsKey\(activityName\)\)
\{
activityCounts\[activityName\] \= 0;
\}
activityCounts\[activityName\]\+\+;
\}
public void DisplayActivityLog\(\)
\{
Console\.WriteLine\("Activity Log\:"\);
foreach \(var entry in activityCounts\)
\{
Console\.WriteLine\(</span>"{entry.Key}: {entry.Value} times");
        }
    }

    public void SaveActivityLog(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in activityCounts)
            {
                writer.WriteLine($"{entry.Key},{entry.Value}");
            }
        }
    }

    public void LoadActivityLog(string filename)
    {
        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(',');
                    string activityName = parts[0];
                    int count = int.Parse(parts[1]);

                    activityCounts[activityName] = count;
                }
            }
        }
    }
}
