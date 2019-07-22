using System;
using System.IO;
using IWshRuntimeLibrary;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// 快捷方式辅助类
    /// </summary>
    public class ShortcutHelper
    {
        /// <summary>
        /// Check to see if a shortcut exists in a given directory with a specified file name
        /// </summary>
        /// <param name="directoryPath">The directory in which to look</param>
        /// <param name="linkPathName">The name of the shortcut (without the .lnk extension) or the full path to a file of the same name</param>
        /// <returns>Returns true if the link exists</returns>
        public static bool Exists(string directoryPath, string linkPathName)
        {
            // Get some file and directory information
            var specialDir = new DirectoryInfo(directoryPath);
            // First get the filename for the original file and create a new file
            // name for a link in the Startup directory
            //
            var originalfile = new FileInfo(linkPathName);
            var newFileName = specialDir.FullName + "\\" + originalfile.Name + ".lnk";
            var linkfile = new FileInfo(newFileName);
            return linkfile.Exists;
        }

        /// <summary>
        /// Check to see if a shell link exists to the given path in the specified special folder
        /// </summary>
        /// <returns>return true if it exists</returns>
        public static bool Exists(Environment.SpecialFolder folder, string linkPathName)
        {
            return Exists(Environment.GetFolderPath(folder), linkPathName);
        }

        /// <summary>
        /// Update the specified folder by creating or deleting a Shell Link if necessary
        /// </summary>
        /// <param name="folder">A SpecialFolder in which the link will reside</param>
        /// <param name="targetPathName">The path name of the target file for the link</param>
        /// <param name="linkPathName">The file name for the link itself or, if a path name the directory information will be ignored.</param>
        /// <param name="install">If true, create the link, otherwise delete it</param>
        public static void Update(Environment.SpecialFolder folder, string targetPathName, string linkPathName, bool install)
        {
            // Get some file and directory information
            Update(Environment.GetFolderPath(folder), targetPathName, linkPathName, install);
        }

        // boolean variable "install" determines whether the link should be there or not.
        // Update the folder by creating or deleting the link as required.

        /// <summary>
        /// Update the specified folder by creating or deleting a Shell Link if necessary
        /// </summary>
        /// <param name="directoryPath">The full path of the directory in which the link will reside</param>
        /// <param name="targetPathName">The path name of the target file for the link</param>
        /// <param name="linkPathName">The file name for the link itself or, if a path name the directory information will be ignored.</param>
        /// <param name="create">If true, create the link, otherwise delete it</param>
        public static void Update(string directoryPath, string targetPathName, string linkPathName, bool create)
        {
            // Get some file and directory information
            var specialDir = new DirectoryInfo(directoryPath);
            // First get the filename for the original file and create a new file
            // name for a link in the Startup directory
            //
            var originalFile = new FileInfo(linkPathName);
            var newFileName = specialDir.FullName + "\\" + originalFile.Name + ".lnk";
            var linkFile = new FileInfo(newFileName);

            if (create) // If the link doesn't exist, create it
            {
                if (linkFile.Exists) return; // We're all done if it already exists
                //Place a shortcut to the file in the special folder 
                try
                {
                    // Create a shortcut in the special folder for the file
                    // Making use of the Windows Scripting Host
                    var shell = new WshShell();
                    var link = (IWshShortcut)shell.CreateShortcut(linkFile.FullName);
                    link.TargetPath = targetPathName;
                    link.Save();
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                    //MessageBox.Show("Unable to create link in special directory: "+newFileName,
                    //    "Shell Link Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else // otherwise delete it from the startup directory
            {
                if (!linkFile.Exists) return; // It doesn't exist so we are done!
                try
                {
                    linkFile.Delete();
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                    //MessageBox.Show("Error deleting link in special directory: "+newFileName,
                    //    "Shell Link Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

    }
}
