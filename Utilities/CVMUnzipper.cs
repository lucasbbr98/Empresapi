using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities
{
    public static class CVMUnzipper
    {
        public static string OpenFile(Stream response, string rootExtension, string filename)
        {
            var unzippedRoot = Unzip(response, rootExtension);
            if (unzippedRoot == null)
                return null;

            ZipArchive archive = new ZipArchive(unzippedRoot);
            foreach (ZipArchiveEntry entry in archive.Entries)
                if (entry.FullName.ToLower() == filename.ToLower())
                return new StreamReader(entry.Open(), Encoding.UTF8).ReadToEnd();

            return null;

        }

        private static Stream Unzip(Stream data, string extension)
        {
            extension = extension.Replace(".", "").ToLower();
            ZipArchive archive = new ZipArchive(data);
            foreach (ZipArchiveEntry entry in archive.Entries)
                if (entry.FullName.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase))
                    return entry.Open();

            return null;
        }
    }
}
