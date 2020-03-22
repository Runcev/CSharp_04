using System;
using System.IO;

namespace Keneyz_03.Tools
{
    internal static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string AppFolderPath =
            Path.Combine(AppDataPath, "Keneyz_04");

        internal static readonly string StorageFilePath =
            Path.Combine(AppFolderPath, "Storage.Keneyz");

        internal static bool CreateFolderAndCheckFileExistance(string filePath)
        {
            var file = new FileInfo(filePath);
            return file.CreateFolderAndCheckFileExistance();
        }

        internal static bool CreateFolderAndCheckFileExistance(this FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            return file.Exists;
        }
    }
}