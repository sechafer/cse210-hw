using System;
using System.Collections.Generic;
using System.IO;

// Clase para representar el diario
public class Journal
{
    public List<Entry> Entries { get; set; } = new List<Entry>();

    // Agrega una nueva entrada al diario
    public void NewEntry()
    {
        Entry userEntry = new Entry();
        userEntry.GenerateDate();
        userEntry.GeneratePrompt();
        userEntry.GetResponse();
        Entries.Add(userEntry);
    }

    // Muestra todas las entradas del diario
    public void DisplayEntries()
    {
        foreach (Entry entry in Entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"{entry.Response}");
            Console.WriteLine();
        }
    }

    // Carga entradas desde un archivo
    public void LoadEntries(string filename)
    {
        Entries.Clear(); // Limpiar entradas actuales antes de cargar nuevas

        if (filename.EndsWith(".csv"))
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                string date = parts[0];
                string prompt = parts[1];
                string response = parts[2];

                Entry loadedEntry = new Entry
                {
                    Date = date,
                    Prompt = prompt,
                    Response = response
                };

                Entries.Add(loadedEntry);
            }
        }
        else
        {
            // Cargar desde otro formato si es necesario
        }
    }

    // Guarda entradas en un archivo
    public void SaveEntries(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            if (filename.EndsWith(".csv"))
            {
                foreach (Entry entry in Entries)
                {
                    outputFile.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
                }
            }
            else
            {
                // Guardar en otro formato si es necesario
            }
        }
    }
}