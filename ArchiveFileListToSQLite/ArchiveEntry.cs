using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveFileListToSQLite
{
    class ArchiveEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string Key { get; set; }
        [Indexed]
        public string FileExtension { get; set; }
        [Indexed]
        public long Size { get; set; } // for example "youtube"
        [Indexed]
        public long CompressedSize { get; set; } // for example "youtube"
        [Indexed]
        public DateTime? CreatedDate { get; set; }
        [Indexed]
        public DateTime? LastAccessedDate { get; set; }
        [Indexed]
        public DateTime? LastModifiedDate { get; set; }
        [Indexed]
        public bool IsDirectory { get; set; }
    }
}
