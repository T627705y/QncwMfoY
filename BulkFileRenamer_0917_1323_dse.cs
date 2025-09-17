// 代码生成时间: 2025-09-17 13:23:17
using System;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;

namespace BulkFileRenamerApp
{
    public class BulkFileRenamer
    {
        private string _directoryPath;
        private string _newNamePattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkFileRenamer"/> class.
        /// </summary>
        /// <param name="directoryPath">The directory path where files are located.</param>
        /// <param name="newNamePattern">The pattern for the new name of the files.</param>
        public BulkFileRenamer(string directoryPath, string newNamePattern)
        {
            _directoryPath = directoryPath;
            _newNamePattern = newNamePattern;
        }

        /// <summary>
        /// Renames all files in the specified directory based on the given pattern.
        /// </summary>
        public void RenameFiles()
        {
            // Check if the directory exists
            if (!Directory.Exists(_directoryPath))
            {
                throw new DirectoryNotFoundException($"The directory {_directoryPath} does not exist.");
            }

            // Get all files in the directory
            var files = Directory.GetFiles(_directoryPath);
            foreach (var file in files)
            {
                try
                {
                    // Create a new filename based on the pattern and file extension
                    string fileName = Path.GetFileName(file);
                    string extension = Path.GetExtension(file);
                    string newName = $