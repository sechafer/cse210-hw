using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string answer = Console.ReadLine();
        int percent = int.Parse(answer);

        string letter = "";

        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Core Requirement: Print the letter grade
        Console.WriteLine($"Your grade is: {letter}");

        // Core Requirement: Check if the user passed and display a message
        if (percent >= 70)
        {
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }

        // Stretch Challenge: Determine the sign for the grade (e.g., +, -)
        string sign = "";

        // Check for A grades
        if (letter == "A")
        {
            
            if ( percent % 10 >= 7)
            {
                sign = "";
            }
        
            else if ( percent != 100 && percent % 10 < 3)
            {
                sign = "-";
              
            }
        }
        // Check for other grades
       else if (percent % 10 >= 7)
        {
            sign = "+";
        }
        else if (percent % 10 < 3)
        {
            sign = "-";
        }

        // Display both grade and sign only for non-F grades
        if (letter != "F")
        {
            Console.WriteLine($"Your grade with sign is: {letter}{sign}");
           
           
        }
        else
        {
              Console.WriteLine("No sign applicable or F grade.");
        }
    }
}