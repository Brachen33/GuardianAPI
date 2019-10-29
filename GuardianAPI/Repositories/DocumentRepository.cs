using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using GuardianAPI.BLL;
using System.Threading.Tasks;
using System.Text;

namespace GuardianAPI.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Document GetDocumentbyId(int id)
        {
            return _context.Documents.Include(x => x.DocumentData).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Document> GetDocuments()
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> GetLatestParticipantPhotoByParticipantId(int participantId)
        {
            var document = await _context.Documents.Include(x => x.DocumentData)
                 .OrderByDescending(x => x.ParticipantId == participantId && x.DocType == "TestDay Photo").FirstOrDefaultAsync();

            // var document = _context.Documents.Include(x => x.)


            // TEST DOCDATA
            // var docdata = _context.DocumentDatas.Find(5);
            var FileString = Encoding.ASCII.GetString(document.DocumentData.FileData);
            // END TEST DOCDATA

            // Decode the base 64 image
            //   var base64Bytes = document.DocumentData.FileData;

            //   var isBase64 = ImageHelper.IsBase64(FileString);
            //   var isMd5 = ImageHelper.IsMD5(FileString);

            var imgByteArr = Convert.FromBase64String(FileString);

            //  var imgString = ImageHelper.FromBase64String(FileString);

            //   var imageDecoded = ImageHelper.FromBase64Bytes(base64Bytes);

            return imgByteArr;
            // return base64Bytes;
        }

        public byte[] GetPhotoByResult(int participantId)
        {
            //var document = _context.Documents.Include(x => x.DocumentData)
            //     .Where( x= > x.)

            return null;

        }
    }
}
