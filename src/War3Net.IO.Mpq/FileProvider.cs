﻿// ------------------------------------------------------------------------------
// <copyright file="FileProvider.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

using War3Net.IO.Mpq.Extensions;

namespace War3Net.IO.Mpq
{
    public static class FileProvider
    {
        /// <summary>
        /// <see cref="File.Create(string)"/>, and <see cref="DirectoryInfo.Create()"/> if needed.
        /// </summary>
        public static FileStream CreateFileAndFolder(string path)
        {
            var directory = new FileInfo(path).Directory!;
            if (!directory.Exists)
            {
                directory.Create();
            }

            return File.Create(path);
        }

        public static IEnumerable<(string fileName, MpqLocale locale, Stream stream)> EnumerateFiles(string path)
        {
            if (File.Exists(path))
            {
                // Assume file at path is an mpq archive.
                using var archive = MpqArchive.Open(path);
                if (archive.TryOpenFile(ListFile.FileName, out var listFileStream))
                {
                    using var reader = new StreamReader(listFileStream);
                    var listFile = reader.ReadListFile();
                    foreach (var fileName in listFile.FileNames)
                    {
                        var memoryStream = new MemoryStream();
                        using (var mpqStream = archive.OpenFile(fileName))
                        {
                            mpqStream.CopyTo(memoryStream);
                        }

                        memoryStream.Position = 0;
                        yield return (fileName, MpqLocale.Neutral, memoryStream);
                    }
                }
            }
            else if (Directory.Exists(path))
            {
                var pathPrefixLength = path.Length + (path.EndsWith(@"\", StringComparison.Ordinal) ? 0 : 1);
                foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
                {
                    var fileName = new FileInfo(file).ToString().Substring(pathPrefixLength);
                    // var memoryStream = new MemoryStream();
                    // File.OpenRead(file).CopyTo(memoryStream);

                    var locale = MpqLocaleProvider.GetPathLocale(fileName, out var filePath);

                    yield return (filePath, locale, File.OpenRead(file));
                }
            }
        }
    }
}