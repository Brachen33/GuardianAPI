using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_document_data")]
    public class DocumentData
    {
        public int Id { get; set; }
        public int MetaId { get; set; }
        public string FileType { get; set; }     
        public byte[] FileData { get; set; }
        public string FileLoaderType { get; set; }

        public Document Document { get; set; }
    }
}
