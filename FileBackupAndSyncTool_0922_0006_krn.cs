// 代码生成时间: 2025-09-22 00:06:16
// 文件备份和同步工具
// 使用CSHARP和MAUI框架实现

using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel; // 用于实现了属性更改通知
using CommunityToolkit.Mvvm.Input; // 用于实现命令

namespace FileBackupAndSyncTool
{
    public class BackupAndSyncService
    {
        private readonly string sourceFolderPath;
        private readonly string backupFolderPath;

        public BackupAndSyncService(string sourceFolderPath, string backupFolderPath)
        {
            this.sourceFolderPath = sourceFolderPath;
            this.backupFolderPath = backupFolderPath;
        }

        // 备份文件
        public async Task BackupFilesAsync()
        {
            try
            {
                var files = Directory.GetFiles(sourceFolderPath);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var backupFile = Path.Combine(backupFolderPath, fileName);

                    // 如果目标文件夹中已经存在文件，则跳过
                    if (File.Exists(backupFile))
                        continue;

                    // 复制文件
                    await File.CopyAsync(file, backupFile, overwrite: false);
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error occurred during backup: {ex.Message}");
            }
        }

        // 同步文件
        public async Task SyncFilesAsync()
        {
            try
            {
                var sourceFiles = Directory.GetFiles(sourceFolderPath);
                var backupFiles = Directory.GetFiles(backupFolderPath);
                var sourceFilesSet = new HashSet<string>(sourceFiles);
                var backupFilesSet = new HashSet<string>(backupFiles);

                foreach (var backupFile in backupFilesSet)
                {
                    if (!sourceFilesSet.Contains(backupFile))
                    {
                        // 如果备份文件在源文件夹不存在，则删除
                        File.Delete(backupFile);
                    }
                }

                foreach (var sourceFile in sourceFilesSet)
                {
                    if (!backupFilesSet.Contains(sourceFile))
                    {
                        // 如果源文件夹中的文件不在备份文件夹中，则复制
                        var fileName = Path.GetFileName(sourceFile);
                        var backupFile = Path.Combine(backupFolderPath, fileName);
                        await File.CopyAsync(sourceFile, backupFile, overwrite: false);
                    }
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error occurred during sync: {ex.Message}");
            }
        }
    }

    // ViewModel
    public class BackupAndSyncViewModel : ObservableObject
    {
        private readonly BackupAndSyncService backupAndSyncService;
        private bool isBusy;
        private string status;

        public BackupAndSyncViewModel(string sourceFolderPath, string backupFolderPath)
        {
            backupAndSyncService = new BackupAndSyncService(sourceFolderPath, backupFolderPath);
        }

        [RelayCommand]
        public async Task Backup()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                Status = "Backup started...";
                await backupAndSyncService.BackupFilesAsync();
                Status = "Backup completed.";
            }
            catch (Exception ex)
            {
                Status = $"Backup failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Sync()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                Status = "Sync started...";
                await backupAndSyncService.SyncFilesAsync();
                Status = "Sync completed.";
            }
            catch (Exception ex)
            {
                Status = $"Sync failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
    }
}
