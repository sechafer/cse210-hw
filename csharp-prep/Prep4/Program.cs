using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Read numbers until the user enters 0
        int input;
        do
        {
            Console.Write("Enter number: ");
            input = int.Parse(Console.ReadLine());

            if (input != 0)
            {
                numbers.Add(input);
            }

        } while (input != 0);

        // Core Requirement 1: Compute the sum
        int sum = numbers.Sum();

        // Core Requirement 2: Compute the average
        double average = numbers.Average();

        // Core Requirement 3: Find the maximum number
        int maxNumber = numbers.Max();

        // Display core results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maxNumber}");

        // Stretch Challenge 1: Find the smallest positive number
        int smallestPositive = numbers.Where(x => x > 0).DefaultIfEmpty(0).Min();
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");

        // Stretch Challenge 2: Sort and display the list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (var num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}