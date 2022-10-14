using SharpCompress.Archives.SevenZip;
using SharpCompress.Readers;
using SQLite;
using System;
using System.IO;

namespace ArchiveFileListToSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                processFile(arg);
            }
        }

        static void processFile(string path)
        {

            using (FileStream fs = File.Open(path,FileMode.Open,FileAccess.Read,FileShare.Read))
            {

                using(SevenZipArchive archive = SevenZipArchive.Open(fs))
                {

                    using (var db = new SQLiteConnection(path + ".db", false))
                    {

                        db.CreateTable<ArchiveEntry>();

                        db.BeginTransaction();
                        foreach (SevenZipArchiveEntry entry in archive.Entries)
                        {
                            //Console.WriteLine(entry.ToString());

                            // Key: full path to compressed file
                            // IsDirectory: obvious
                            // Size: obvious
                            // LastAccessedTime
                            // LastModifiedTime
                            // CreatedTime
                            // CompressedSize

                            ArchiveEntry archiveEntry = new ArchiveEntry()
                            {
                                Key = entry.Key,
                                FileExtension = Path.GetExtension(entry.Key).Trim('.'),
                                IsDirectory = entry.IsDirectory,
                                Size = entry.Size,
                                CompressedSize = entry.CompressedSize,
                                LastAccessedDate = entry.LastAccessedTime,
                                LastModifiedDate = entry.LastModifiedTime,
                                CreatedDate = entry.CreatedTime,
                            };


                            db.Insert(archiveEntry);
                            //Console.WriteLine(entry.ToString());
                        }


                        db.Commit();
                        db.Close();
                    }
                }

            }
        }
    }
}
