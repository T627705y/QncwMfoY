// 代码生成时间: 2025-09-20 03:18:33
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FolderOrganizerApp
{
    /// <summary>
    /// Class responsible for organizing the structure of folders.
    /// </summary>
    public class FolderOrganizer
    {
        /// <summary>
        /// The source directory to organize.
        /// </summary>
        private readonly string sourceDirectory;

        /// <summary>
        /// Initializes a new instance of FolderOrganizer.
        /// </summary>
        /// <param name="sourceDirectory">The path to the directory to organize.</param>
        public FolderOrganizer(string sourceDirectory)
        {
            this.sourceDirectory = sourceDirectory;
        }

        /// <summary>
        /// Starts the organization process.
        /// </summary>
        public async Task OrganizeAsync()
        {
            try
            {
                if (!Directory.Exists(sourceDirectory))
                {
                    Console.WriteLine($"The directory {sourceDirectory} does not exist.");
                    return;
                }

                // Get all files from the source directory.
                var files = Directory.GetFiles(sourceDirectory);

                // Create a dictionary to categorize files by extension.
                var fileCategories = files.GroupBy(f => Path.GetExtension(f)).ToDictionary(group => group.Key, group => group.ToList());

                // Organize files by creating subdirectories for each file extension.
                foreach (var fileCategory in fileCategories)
                {
                    var directoryName = fileCategory.Key.TrimStart('.');
                    var directoryPath = Path.Combine(sourceDirectory, directoryName);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Move files into the appropriate subdirectory.
                    foreach (var file in fileCategory.Value)
                    {
                        File.Move(file, Path.Combine(directoryPath, Path.GetFileName(file)));
                    }
                }

                Console.WriteLine("Folder organization complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the path to the directory you want to organize: ");
            var path = Console.ReadLine();

            var organizer = new FolderOrganizer(path);
            await organizer.OrganizeAsync();
        }
    }
}