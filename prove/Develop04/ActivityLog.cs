using System;
using System.Collections.Generic;
using System.IO; // Para guardar y cargar archivos
using System.Linq;
using System.Threading;

// Clase para llevar un registro de la actividad realizada
class ActivityLog
{

    private Dictionary<string, int> activityCount = new Dictionary<string, int>();
    
    // Incrementa el conteo de una actividad
    public void LogActivity(string activityName)
    {
        if (activityCount.ContainsKey(activityName))
            activityCount[activityName]++;
        else
            activityCount[activityName] = 1;
    }

    // Muestra el historial de actividad
    public void DisplayActivityLog()
    {
        Console.WriteLine("Activity Log:");
        foreach (var entry in activityCount)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value} times");
        }
    }

    // Guarda el historial de actividad en un archivo
    public void SaveActivityLog(string filePath)
    {
        
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var entry in activityCount)
            {
                writer.WriteLine($"{entry.Key},{entry.Value}");
            }
        }
    }

    // Carga el historial de actividad desde un archivo
    public void LoadActivityLog(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    activityCount[parts[0]] = int.Parse(parts[1]);
                }
            }
        }
    }
}