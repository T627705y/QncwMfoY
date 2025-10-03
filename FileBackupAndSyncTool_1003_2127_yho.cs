// 代码生成时间: 2025-10-03 21:27:32
using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FileBackupAndSyncTool
{
    public partial class FileBackupAndSyncViewModel : ObservableObject
    {
        private string sourceFolderPath = string.Empty;
        private string targetFolderPath = string.Empty;
        private bool isSynchronizationInProgress;
        private string synchronizationStatus = "Sync is not started.";

        [ObservableProperty]
        private string? errorMessage;

        public FileBackupAndSyncViewModel()
        {
            StartSyncCommand = new RelayCommand(ExecuteStartSync, CanExecuteStartSync);
        }

        [RelayCommand]
        private async Task ExecuteStartSync()
        {
            if (!Directory.Exists(sourceFolderPath))
            {
                ErrorMessage = "Source folder does not exist.";
                return;
            }

            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }

            try
            {
                await SynchronizeFoldersAsync(sourceFolderPath, targetFolderPath);
                synchronizationStatus = "Synchronization completed successfully.";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                synchronizationStatus = "Synchronization failed.";
            }
        }

        private bool CanExecuteStartSync() => !isSynchronizationInProgress;

        private async Task SynchronizeFoldersAsync(string sourceDir, string targetDir)
        {
            isSynchronizationInProgress = true;
            var files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var targetFile = Path.Combine(targetDir, file.Substring(sourceDir.Length + 1).Replace("\", "/"));
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile));
                File.Copy(file, targetFile, true);
            }
            isSynchronizationInProgress = false;
        }
    }
}
