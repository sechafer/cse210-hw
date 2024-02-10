using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        LoadGoals();
        Console.WriteLine("Welcome to Eternal Quest! Let's embark on our journey...");
        MainMenu();
    }

    private void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. List Goals");
            Console.WriteLine("2. Create New Goal");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Display Player Info");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListGoalNames();
                    break;
                case "2":
                    CreateGoalMenu();
                    break;
                case "3":
                    RecordEventMenu();
                    break;
                case "4":
                    DisplayPlayerInfo();
                    break;
                case "5":
                    SaveGoals();
                    Console.WriteLine("Goodbye! See you on your next quest.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }

    private void ListGoalNames()
    {
        Console.WriteLine("\nGoals:");
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.shortName);
        }
    }

    private void CreateGoalMenu()
    {
        Console.WriteLine("\nCreate New Goal:");

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();

        Console.Write("Enter points for completing the goal: ");
        int points = int.Parse(Console.ReadLine());

        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple");
        Console.WriteLine("2. Eternal");
        Console.WriteLine("3. Checklist");
        Console.Write("Enter your choice: ");
        string typeChoice = Console.ReadLine();

        GoalType type;
        switch (typeChoice)
        {
            case "1":
                type = GoalType.Simple;
                break;
            case "2":
                type = GoalType.Eternal;
                break;
            case "3":
                type = GoalType.Checklist;
                break;
            default:
                Console.WriteLine("Invalid choice. Defaulting to Simple goal.");
                type = GoalType.Simple;
                break;
        }

        if (type == GoalType.Checklist)
        {
            Console.Write("Enter target for checklist goal: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Enter bonus points for completing the checklist goal: ");
            int bonus = int.Parse(Console.ReadLine());
            CreateGoal(name, description, points, type, target, bonus);
        }
        else
        {
            CreateGoal(name, description, points, type);
        }
    }

    private void RecordEventMenu()
    {
        Console.WriteLine("\nRecord Event:");

        Console.Write("Enter the name of the goal you completed: ");
        string goalName = Console.ReadLine();
        RecordEvent(goalName);
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"\nTotal Score: {_score}");
    }

    public void CreateGoal(string name, string description, int points, GoalType type, int target = 0, int bonus = 0)
    {
        Goal goal;
        switch (type)
        {
            case GoalType.Simple:
                goal = new SimpleGoal(name, description, points);
                break;
            case GoalType.Eternal:
                goal = new EternalGoal(name, description, points);
                break;
            case GoalType.Checklist:
                goal = new ChecklistGoal(name, description, points, target, bonus);
                break;
            default:
                throw new ArgumentException("Invalid goal type.");
        }
        _goals.Add(goal);
        SaveGoals();
        Console.WriteLine("Goal created successfully!");
    }

    public void RecordEvent(string goalName)
    {
        foreach (var goal in _goals)
        {
            if (goal.shortName == goalName)
            {
                goal.RecordEvent();
                _score += goal.GetPoints();
                Console.WriteLine($"Event recorded for {goalName}. You earned {goal.GetPoints()} points.");
                SaveGoals();
                return;
            }
        }
        Console.WriteLine($"Goal '{goalName}' not found.");
    }

    private void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    private void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            _goals.Clear();
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 6) // Verificar si hay suficientes partes en la línea
                    {
                        string name = parts[0];
                        string description = parts[1];
                        int points = int.Parse(parts[2]);
                        GoalType type = (GoalType)Enum.Parse(typeof(GoalType), parts[3]);
                        int target = int.Parse(parts[4]);
                        int bonus = int.Parse(parts[5]);
                        switch (type)
                        {
                            case GoalType.Simple:
                                _goals.Add(new SimpleGoal(name, description, points));
                                break;
                            case GoalType.Eternal:
                                _goals.Add(new EternalGoal(name, description, points));
                                break;
                            case GoalType.Checklist:
                                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid format in goals file: {line}");
                    }
                }
            }
        }
    }
}

public enum GoalType
{
    Simple,
    Eternal,
    Checklist
}

public abstract class Goal
{
    public string shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        shortName = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();

    public abstract bool IsComplete();

    public string GetDetailsString()
    {
        return $"{shortName}: {_description} - Points: {_points}";
    }

    public string GetStringRepresentation()
    {
        return $"{shortName},{_description},{_points}";
    }

    public int GetPoints()
    {
        return _points;
    }
}

public class SimpleGoal : Goal
{
    private bool isComplete;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        isComplete = false;
    }

    public override void RecordEvent()
    {
        if (!isComplete)
            isComplete = true;
    }

    public override bool IsComplete()
    {
        return isComplete;
    }
}
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        // Eternal goals are always considered recorded events
    }

    public override bool IsComplete()
    {
        return false; // Eternal goals are never complete
    }
}

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus) : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        _amountCompleted++;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public new string GetDetailsString()
    {
        return $"{shortName}: {_description} - Points: {_points} - Completed {_amountCompleted}/{_target} times";
    }

    public new int GetPoints()
    {
        if (IsComplete())
            return _points + _bonus;
        else
            return _points;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}