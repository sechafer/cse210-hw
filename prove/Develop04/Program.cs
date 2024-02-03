using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
// **Mindfulness Program**

// This program offers a variety of activities to help you
// relax, reflect, and develop your well-being.

// The available activities are:

// * Breathing: Guided breathing for relaxation and focus.
// * Reflection: Exploration of past experiences of strength and resilience.
// * Listing: Creating lists on various topics to foster gratitude and self-awareness.

// You can also customize the duration of each activity and keep track of your progress.

// Start your journey towards greater inner peace and well-being!

class Program
{
    static void Main(string[] args)
    {
        ActivityLog activityLog = new ActivityLog(); // Create log object

        while (true)
        {
            Console.WriteLine("Welcome to Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
              Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an activity: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            Activity activity = null;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
                    break;
                case 2:
                    activity = new ReflectionActivity("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
                    break;
                case 3:
                    activity = new ListingActivity("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (activity != null)
            {
                activityLog.LogActivity(activity.GetType().Name);
                activity.Run();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}