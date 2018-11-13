using System;

namespace InteractTechnicalTest.Helpers
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// This is a workaround for IIS Express so we can get at the bin folder when running in dev
        /// </summary>
        /// <returns>The executing assembly folder</returns>
        public static string GetExecutingAssemblyFolder()
        {
            string binPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            Uri uri = new Uri(binPath);
            string localPath = uri.LocalPath;
            return System.IO.Path.GetDirectoryName(localPath);
        }
    }
}