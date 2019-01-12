using System;
using System.IO;
using System.Text.RegularExpressions;

namespace BlogBuilder.Common
{
    public static class DirectoryRepository
    {
        public static bool EnsureDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath) ? true : false;
        }

        public static void EnsureDirectoryWhichMustExist(string directoryPath)
        {
            if(EnsureDirectory(directoryPath))
            {
                return;
            }

            Console.Error.WriteLine($"No directoryPath found at: {directoryPath}, please ensure the path is correct or the directories exists.");
            Environment.Exit((int)ExitCode.DirectoryDoesNotExist);
        }

        /// <summary>
        /// Depth-first recursive delete, with handling for descendant 
        /// directories open in Windows Explorer.
        /// </summary>
        public static void DeleteDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                DeleteDirectory(directory);
            }

            try
            {
                Directory.Delete(directoryPath, true);
            }
            catch (IOException)
            {
                Directory.Delete(directoryPath, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(directoryPath, true);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error trying to delete directory '{directoryPath}'");
                Console.Error.WriteLine(ex);
            }
        }

        public static void CopyDirectory(string filePath, string dropPath)
        {
            var dir = new DirectoryInfo(filePath);

            var dirs = dir.GetDirectories();
            if (!Directory.Exists(dropPath))
            {
                Directory.CreateDirectory(dropPath);
            }

            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(dropPath, file.Name);
                file.CopyTo(temppath, true);
            }

            foreach (var subdir in dirs)
            {
                var temppath = Path.Combine(dropPath, subdir.Name);
                CopyDirectory(subdir.FullName, temppath);
            }
        }
    }
}