using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

abstract class Activity
{
    protected int duration;
    private string name;
    private string description;
    protected List<string> usedPrompts = new List<string>();

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public string Name => name;

    public string Description => description;

    public virtual void DisplayStartingMessage()
    {
        Console.WriteLine(<span class="math-inline">"Starting \{Name\} Activity"\);
Console\.WriteLine\(Description\);
Console\.Write\("Enter duration in seconds\: "\);
duration \= Convert\.ToInt32\(Console\.ReadLine\(\)\);
Console\.WriteLine\("Get ready to begin\.\.\."\);
Thread\.Sleep\(3000\);
\}
public virtual void DisplayEndingMessage\(\)
\{
Console\.WriteLine\("Congratulations\! You have completed the \{0\} Activity for \{1\} seconds\.", Name, duration\);
\}
protected void ShowSpinner\(int seconds\)
\{
for \(int i \= 0; i < seconds; i\+\+\)
\{
Console\.Write\("/"\);
Thread\.Sleep\(500\);
Console\.Write\("\\b"\);
Console\.Write\("\-"\);
Thread\.Sleep\(500\);
Console\.Write\("\\b"\);
Console\.Write\("\\\\"\);
Thread\.Sleep\(500\);
Console\.Write\("\\b"\);
Console\.Write\("\|"\);
Thread\.Sleep\(500\);
Console\.Write\("\\b"\);
\}
\}
protected void ShowCountDown\(int seconds\)
\{
for \(int i \= seconds; i \> 0; i\-\-\)
\{
Console\.WriteLine\(</span>"Time left: {i} seconds");
            Thread.Sleep(1000);
        }
    }

    public abstract void Run();

    protected string GetRandomPrompt()
    {
        if (usedPrompts.Count == prompts.Count)
        {
            usedPrompts.Clear(); // Reset for the next session
        }

        string prompt;
        do
        {
            prompt = prompts[random.Next(prompts.Count)];
        } while (usedPrompts.Contains(prompt));

        usedPrompts.Add(prompt);
        return prompt;
    }

    protected abstract List<string> prompts { get; }

    private static readonly Random random = new Random();
}

class BreathingActivity : Activity
{
    public BreathingActivity(string name, string description) : base(name, description)
    {
    }

    public override void DisplayStartingMessage()
    {
        base.DisplayStartingMessage();
        Console.WriteLine("Focus on your breath and listen to the instructions.");
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Let's begin the breathing exercise...");
        ShowCountDown(duration);

        for (int i = 0; i < duration; i++)
        {
            Console.Write("Breathe in... ");
            for (int j = 0; j < i + 1; j++)
            {
                Console.Write("."); // Gradually increase dots for inhale
                Thread.Sleep(500);
            }
            Console.WriteLine();

            Thread.Sleep(3000);

            Console.Write("Breathe out... ");
            for (int j = i; j >= 0; j--)
            {
                Console.Write("."); // Gradually decrease dots for exhale
                Thread.Sleep(500);
            }
            Console.WriteLine();

            Thread.Sleep(3000);
        }

        DisplayEndingMessage();
    }

    protected override List<string> prompts => new List<string>(); // No prompts for breathing
}

class ReflectionActivity : Activity
{
    private List<string> reflectionPrompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> reflectionQuestions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
         "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Let's reflect on a past experience...");
        ShowCountDown(duration);

        Random random = new Random();
        string prompt = GetRandomPrompt();
        Console.WriteLine(prompt);

        DisplayQuestions();

        DisplayEndingMessage();
    }

    private void DisplayQuestions()
    {
        foreach (string question in reflectionQuestions)
        {
            Console.WriteLine(question);
            Thread.Sleep(3000); // Pause for reflection
            ShowSpinner(3);
        }
    }

    protected override List<string> prompts => reflectionPrompts;
}
