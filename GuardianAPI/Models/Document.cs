using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_document")]
    public class Document
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ParticipantId { get; set; }
        public string ParticipantIssuedId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string DocType { get; set; }

        public DocumentData DocumentData { get; set; }

        public Document()
        {
            DocumentData = new DocumentData();
        }

    }
}
