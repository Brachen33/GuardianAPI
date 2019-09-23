using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardianAPI.Models;
using PdfSharpCore;

namespace GuardianAPI.Interfaces
{
    public interface IPDFCreatorRepository
    {       
        void GetPDF(PDFType type, int id); 
    }
}
