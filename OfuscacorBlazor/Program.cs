using System;
using System.Diagnostics;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        string jsDirectory = "C:\\Users\\SBARREIRO\\source\\repos\\portal-tramites\\Main\\Sources\\PortalTramites\\ProyectoPortalTramitesServer\\wwwroot\\mteyss\\js";

        if (!Directory.Exists(jsDirectory))
        {
            Console.WriteLine("JavaScript directory not found.");
            return;
        }
        string obfuscatorPath = "C:\\Users\\SBARREIRO\\AppData\\Roaming\\npm\\javascript-obfuscator.cmd"; // Actualiza esto con la ruta correcta

        // Get all JS files in the directory
        var jsFiles = Directory.GetFiles(jsDirectory, "*.js");
        foreach (var file in jsFiles)
        {
            // Run javascript-obfuscator for each file
            var startInfo = new ProcessStartInfo
            {
                FileName = obfuscatorPath,
                Arguments = $"{file} --output {file}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process { StartInfo = startInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    Console.WriteLine($"Successfully obfuscated: {file}");
                }
                else
                {
                    Console.WriteLine($"Error obfuscating {file}: {error}");
                }
            }
        }
    }
}