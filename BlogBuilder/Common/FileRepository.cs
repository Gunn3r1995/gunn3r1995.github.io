using System;
using System.IO;
using System.Text.RegularExpressions;

namespace BlogBuilder.Common
{
    public static class FileRepository
    {
        public static void CopyFile(string filePath, string dropPath)
        {
            var file = new FileInfo(filePath);

            if (!Directory.Exists(dropPath))
            {
                Directory.CreateDirectory(dropPath);
            }

            var temppath = Path.Combine(dropPath, file.Name);
            file.CopyTo(temppath, true);
        }

        public static void RenameFile(string filePath, string newName) => File.Move(filePath, newName);

        public static void RenameDirectoryToLower(string pathToRename)
        {
            var directory = new DirectoryInfo(pathToRename);
            if (!directory.Exists)
            {
                return;
            }

            var files = directory.GetFiles();
            foreach (var file in files)
            {
                RenameFile(file.FullName, file.FullName.ToLower());
            }

            var dirs = directory.GetDirectories();
            foreach (var subdir in dirs)
            {
                RenameDirectoryToLower(subdir.FullName);
            }
        }

        public static void PrependToFile(string filePath, string prependContent)
        {
            var currentContent = string.Empty;

            if (File.Exists(filePath))
            {
                currentContent = File.ReadAllText(filePath);
            }

            File.WriteAllText(filePath, prependContent + currentContent);
        }

        public static void AppendToFile(string filePath, string content)
        {
            if (File.Exists(filePath))
            {
                File.AppendAllText(filePath, content);
            }
        }

        public static void ReplaceMatchingText(string filePath, string pattern, MatchEvaluator evaluator)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                content = Regex.Replace(content, pattern, evaluator);
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error trying to replace '{pattern}' with evaluator '{evaluator}' for file '{filePath}'");
                Console.Error.WriteLine(ex);
            }
        }

        public static void ReplaceMatchingText(string filePath, string pattern, string replacement)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                content = Regex.Replace(content, pattern, replacement);
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error trying to replace '{pattern}' with replacement '{replacement}' for file '{filePath}'");
                Console.Error.WriteLine(ex);
            }
        }

    }
}
