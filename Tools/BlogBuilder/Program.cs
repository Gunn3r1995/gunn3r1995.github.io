using System;
using BlogBuilder.Common;
using NDesk.Options;

namespace BlogBuilder
{
    public class Program
    {
        private const string DefaultBlogFileNameRegex = @"";
        private static string _blogFileNameRegex;

        private static readonly OptionSet Options = new OptionSet
        {
            {"blog|pathToBlog=","[Required]",x => BlogPath = x},
            {"o|hugo|hugoprojectpath=", "[Required] the path to the root directory of the hugo project for the release notes folder structure to be generated" +
                                        "/releasenotesdrop: this folder is where the unmodified release notes is dropped too." +
                                        "/content: this folder is where the generated folder structure is created.", x => HugoProjectPath = x.TrimEnd('/').TrimEnd('\\')},
            {"br|blogRegex|blogFileNameRegex=","[Optional] the regex expression for capturing individual blog files. " +
                                               $"Default is {DefaultBlogFileNameRegex}.", x => BlogFileNameRegex = x},
            {"?|help|h", "show this", x => Help = x != null}
        };

        private static string BlogPath { get; set; }

        private static string HugoProjectPath { get; set; }

        private static string BlogFileNameRegex
        {
            get => _blogFileNameRegex ?? DefaultBlogFileNameRegex;
            set => _blogFileNameRegex = value;
        }

        private static bool Help { get; set; }

        private static string drop;

        public static void Main(string[] args)
        {
            Options.Parse(args);

            if(Help)
            {
                ShowHelp();
            }

            DirectoryRepository.EnsureDirectoryWhichMustExist(BlogPath);
            DirectoryRepository.EnsureDirectoryWhichMustExist(HugoProjectPath);

            drop = HugoProjectPath + @"\BlogDrop";

            // Delete and copy Notes to drop location
            DirectoryRepository.DeleteDirectory(drop);
            DirectoryRepository.CopyDirectory(BlogPath, drop);

            //var notes = ReleaseNotesRepository.GetNotesFromReleaseNotes(drop, PathToNotes, NotesFileNameRegex, Url);

            //Console.Out.WriteLine("Updating Feature Notes");
            //UpdateFeatures(notes);
        }

        private static void EnsureRequiredFields()
        {
            if (!string.IsNullOrEmpty(BlogPath) && !string.IsNullOrEmpty(HugoProjectPath))
            {
                return;
            }

            if (string.IsNullOrEmpty(BlogPath))
            {
                Console.Error.WriteLine("Error: ");
            }

            if (string.IsNullOrEmpty(HugoProjectPath))
            {
                Console.Error.WriteLine("Error: ");
            }

            Options.WriteOptionDescriptions(Console.Out);
            Environment.Exit((int)ExitCode.InvalidParameters);
        }

        private static void ShowHelp()
        {
            Options.WriteOptionDescriptions(Console.Out);
            Environment.Exit((int) ExitCode.ShowHelp);
        }
    }
}