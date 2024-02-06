using System;
using System.Collections.Generic;
using System.IO; // Para guardar y cargar archivos
using System.Linq;
using System.Threading;


// Clase base para las actividades
abstract class Activity
{
    protected int duration;
    protected string name;
    protected string description;
    protected static Random random = new Random();
    protected static List<string> shuffledPrompts = new List<string>(); // Lista de prompts mezclados

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Starting {name} Activity");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Get ready to begin...");
        Thread.Sleep(3000);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine($"Congratulations! You have completed the {name} Activity for {duration} seconds.");
        Thread.Sleep(2000);
    }

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("/");
            Thread.Sleep(500);
            Console.Write("\b");
            Console.Write("-");
            Thread.Sleep(500);
            Console.Write("\b");
            Console.Write("\\");
            Thread.Sleep(500);
            Console.Write("\b");
            Console.Write("|");
            Thread.Sleep(500);
            Console.Write("\b");
        }
    }

    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"Time left: {i} seconds");
            Thread.Sleep(1000);
        }
    }

    public abstract void Run();
}

class StorytellingActivity : Activity
{
    public StorytellingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Let's embark on a storytelling adventure...");
        Console.WriteLine("You will create a short story based on a given prompt.");
        ShowCountDown(duration);

        Console.WriteLine("Once upon a time, in a land far, far away...");

        // Allow user to tell the story
        Console.WriteLine("Tell your story:");
        string story = Console.ReadLine();

        // You can save the story to a file if needed
        // For simplicity, let's just display the story
        Console.WriteLine("Here's your story:");
        Console.WriteLine(story);

        DisplayEndingMessage();
    }
}

// Clase para la actividad de respiración
class BreathingActivity : Activity
{
    public BreathingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Console.WriteLine("Let's begin the breathing exercise...");
        ShowCountDown(duration);

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(3000);
            Console.WriteLine("Breathe out...");
            Thread.Sleep(3000);
        }

        DisplayEndingMessage();
    }
}

// Clase para la actividad de reflexión
class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
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

        string prompt = GetRandomPrompt();
        Console.WriteLine(prompt);

        DisplayQuestions();

        DisplayEndingMessage();
    }

    private string GetRandomPrompt()
    {
        // Si la lista de prompts mezclados está vacía, la llenamos y la mezclamos
        if (shuffledPrompts.Count == 0)
        {
            shuffledPrompts = prompts.OrderBy(x => random.Next()).ToList();
        }

        // Tomamos y removemos el primer elemento de la lista mezclada
        string prompt = shuffledPrompts[0];
        shuffledPrompts.RemoveAt(0);

        return prompt;
    }

    private void DisplayQuestions()
    {
        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Thread.Sleep(3000); // Pausa para reflexión
            ShowSpinner(3);
        }
    }
}

// Clase para la actividad de listado
class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
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

        string prompt = GetRandomPrompt();
        Console.WriteLine(prompt);

        GetListFromUser();

        DisplayEndingMessage();
    }

    private string GetRandomPrompt()
    {
        // Mezcla y selecciona un prompt aleatorio
        return prompts[random.Next(prompts.Count)];
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

        Console.WriteLine($"You listed {items.Count} items.");
    }
}