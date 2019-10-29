using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using System.Drawing.Imaging;
using PdfSharp.Drawing.Layout;

namespace GuardianAPI.BLL
{
    public class PDFCreator : IPDFCreatorRepository
    {
        private readonly IResultRepository _resultRepo;
        private readonly IDocumentRepository _documentRepository;

        public PDFCreator(IResultRepository resultRepo, IDocumentRepository documentRepository)
        {
            _resultRepo = resultRepo;
            _documentRepository = documentRepository;
        }

        public async void GetPDF(PDFType type, int resultId)
        {
            var result = await _resultRepo.GetResultWithDetailById(resultId);
            var participantPhoto = await _documentRepository.GetLatestParticipantPhotoByParticipantId(result.ParticipantId);

            switch (type)
            {
                case PDFType.GuardianExportPDF:

                    // Call the Create GuardianPDFResult method
                    CreateGuardianPDFResult(result, participantPhoto);
                    break;

                case PDFType.None:
                    break;

                case PDFType.SomeOtherReport:

                default:
                    break;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void CreateGuardianPDFResult(Result result, byte[] participantPhoto)
        {

            // Create PDF
            var document = new PdfSharp.Pdf.PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var xtf = new XTextFormatter(gfx);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var font = new XFont("Arial", 10, XFontStyle.Regular);
            var noteFont = new XFont("Arial", 8, XFontStyle.Regular);

            //Header info font
            var TestResultFont = new XFont("Arial", 16, XFontStyle.Bold);


            // PDFSharp Settings
            PdfSharp.Drawing.XPoint xPoint = new XPoint
            {
                X = 0,
                Y = 0
            };
            var color = new XColor();
            var pen = new XPen(color);


            // Header PSI Logo Image         
            XImage psiLogoImage = XImage.FromFile(@".\images\psi.jpg");
            gfx.DrawImage(psiLogoImage, 10, 10, 290, 45);

            // AM testing For Result Details to be dynamic
            XPoint detailsXPoint = new XPoint
            {
                X = 12,
                Y = 202
            };

            var xTableVal = 10;
            var xTableVal2 = 175;
            var yTableVal = 600;
            var yTableVal2 = 175;

            var xTableValLooped = 0;
            var yTableValLooped = 0;



            // Title Result Header
            gfx.DrawString($"Test Result #", TestResultFont, XBrushes.Black, xPoint.X + 5, xPoint.Y + 70);

            //Top Left Column Labels
            gfx.DrawString($"Name", font, XBrushes.Black, xPoint.X + 5, xPoint.Y + 85);
            gfx.DrawString($"Accession #", font, XBrushes.Black, xPoint.X + 5, xPoint.Y + 100);
            gfx.DrawString($"Chain of Custody #", font, XBrushes.Black, xPoint.X + 5, xPoint.Y + 115);
            gfx.DrawString($"ID / SSN", font, XBrushes.Black, xPoint.X + 5, xPoint.Y + 130);


            // Top Right Column Labels
            gfx.DrawString($"Collected", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 85);
            gfx.DrawString($"Ordered", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 100);
            gfx.DrawString($"Received", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 115);
            gfx.DrawString($"Completed", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 130);
            gfx.DrawString($"Specimen Type", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 145);



            // Participant Image from stream
            MemoryStream ms = new MemoryStream(participantPhoto);
            XImage participantImage = XImage.FromStream(ms);
            gfx.DrawImage(participantImage, xPoint.X + 445, xPoint.Y + 10, 175, 140);


            // Get the Result and Result Detail data to add to the PDF
            gfx.DrawString(result.OBR_2_1, TestResultFont, XBrushes.Black, xPoint.X + 115, xPoint.Y + 70);
            gfx.DrawString(result.PID_5_2 + ", " + result.PID_5_1, font, XBrushes.Black, xPoint.X + 100, xPoint.Y + 85);
            gfx.DrawString(result.OBR_2_1, font, XBrushes.Black, xPoint.X + 100, xPoint.Y + 100);
            gfx.DrawString(result.OBR_3_1, font, XBrushes.Black, xPoint.X + 100, xPoint.Y + 115);
            gfx.DrawString(result.PID_2_1, font, XBrushes.Black, xPoint.X + 100, xPoint.Y + 130);


            gfx.DrawString(result.OBR_7_1.ToString("MM/dd/yyyy") + " " + result.OBR_7_2.ToString(), font, XBrushes.Black, xPoint.X + 350, xPoint.Y + 85);
            gfx.DrawString(result.OBR_14_1.ToString("MM/dd/yyyy") + " " + result.OBR_14_2.ToString(), font, XBrushes.Black, xPoint.X + 350, xPoint.Y + 100);
            gfx.DrawString(result.OBX_14_1.ToString("MM/dd/yyyy") + " " + result.OBX_14_2.ToString(), font, XBrushes.Black, xPoint.X + 350, xPoint.Y + 130);


            // Specimen Type           
            var panelAsString = result.Panel.SpecimenType == "U" ? result.Panel.SpecimenType = "Urine" :
                result.Panel.SpecimenType == "O" ? "Oral" :
                result.Panel.SpecimenType == "H" ? "Hair" : "No Type";
            gfx.DrawString(panelAsString, font, XBrushes.Black, xPoint.X + 350, xPoint.Y + 145);

            // Panel Name
            XRect rectHeaderNote = new XRect(10, 175, 580, 45);
            gfx.DrawRectangle(XBrushes.SeaShell, rectHeaderNote);
            // TODO: Change the first or default
            if (result.ResultDetails.FirstOrDefault().NTE_3_1 != null)
            {
                xtf.DrawString(result.ResultDetails.FirstOrDefault().NTE_3_1, noteFont, XBrushes.Black, rectHeaderNote);
            }

            // Table Headers
            XRect rectTableHeaders = new XRect(10, 155, 580, 45);
            gfx.DrawRectangle(XBrushes.Beige, rectTableHeaders);
            xtf.DrawString("Test", font, XBrushes.Black, rectTableHeaders);
            rectTableHeaders.X = rectTableHeaders.X + 200;
            xtf.DrawString("Result", font, XBrushes.Black, rectTableHeaders);
            rectTableHeaders.X = rectTableHeaders.X + 200;
            xtf.DrawString("Level", font, XBrushes.Black, rectTableHeaders);
            rectTableHeaders.X = rectTableHeaders.X + 125;
            xtf.DrawString("Cutoff", font, XBrushes.Black, rectTableHeaders);



            // Run this if, if the Result Details Record contains a header note (NTE_3_1)
            //   if (result.ResultDetails.FirstOrDefault().NTE_3_1 != null)
            //   {
            // bottom line under Test /Result / Level / Cutoff
            gfx.DrawLine(pen, xPoint.X + xTableVal, xPoint.X + xTableVal2 - 3, xPoint.Y + yTableVal, xPoint.Y + yTableVal2 - 3);



            if (result.ResultDetails.FirstOrDefault().NTE_3_1 != null)
            {
                XRect rect = new XRect(10, 175, 580, 45);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                xtf.DrawString(result.ResultDetails.FirstOrDefault().NTE_3_1, noteFont, XBrushes.Black, rect);
            }
            //  gfx.DrawString(result.ResultDetails.FirstOrDefault().NTE_3_1, font, XBrushes.Black, detailsXPoint.X + 10, detailsXPoint.Y);

            // Line that gets looped through
            //   gfx.DrawLine(pen, xPoint.X + xTableVal, xPoint.X + xTableValLooped + 75, xPoint.Y + yTableVal, xPoint.Y + yTableValLooped + 75);


            // Loop through result details that have a header note



            // AM Testing    
            // Results Rectangle
            XRect rectTable = new XRect(10, 215, 580, 400);

            gfx.DrawRectangle(XBrushes.Khaki, rectTable);

            // RectTable for the Result
            XRect rectTableResults = new XRect(215, 215, 580, 200);

            // RectTable for the Cutoff               
            XRect rectTableCutOff = new XRect(540, 215, 580, 200);

            result.ResultDetails.ForEach(x =>
            {
                    // Test Name Header (i.e. DCS 9 Panel)
                    if (x.OBR_4_1 != null)
                {
                    xtf.DrawString(x.OBR_4_2, font, XBrushes.Black, rectTable);
                }

                if (x.OBX_3_2 != null)
                {
                        // Test Name                        
                        xtf.DrawString(x.OBX_3_2.Replace("(DCS)", ""), font, XBrushes.Black, rectTable);

                        // Result
                        var resultAsString = x.OBX_8_1 == "N" ? x.OBX_8_1 = "Negative" : x.OBX_8_1 == "A" ?
                             x.OBX_8_1 = "Abnormal" : "No Result";

                    xtf.DrawString(x.OBX_8_1, font, XBrushes.Black, rectTableResults);

                        // Level TODO
                        // MIGHT NOT BE NEEDED

                        // Cutoff                       
                        if (x.OBX_7_1 != null)
                    {
                        xtf.DrawString(x.OBX_7_1, font, XBrushes.Black, rectTableCutOff);
                    }
                }
                else
                {
                        // Set up Notes Rectangle
                        //   XRect footerNotesRectangle = new XRect(10, 415, 580, 400);
                        //   gfx.DrawRectangle(XBrushes.DodgerBlue, footerNotesRectangle);

                        // Look at everything except the header note
                        if (x.NTE_1_1.HasValue && x.ItemIndex > 0 && x.NTE_3_1 != "")
                    {

                        xtf.DrawString(x.NTE_3_1, font, XBrushes.Black, rectTable);
                        if (x.NTE_3_1.Count() > 300)
                        {
                            rectTable.Y = rectTable.Y + 30;
                        }
                            //    rectTable.Y = rectTable.Y + 20;                          
                        }
                }

                    // Test Name Coordiants Count
                    rectTable.Y = rectTable.Y + 15;
                    //Results Coordinants count
                    rectTableResults.Y = rectTableResults.Y + 15;
                    // Cutoff Coordiants count
                    rectTableCutOff.Y = rectTableCutOff.Y + 15;
            });




            //TODO: PDF will be sent as a stream back to the requestor 
            document.Save($"C:\\Temp\\TestGuardianPDF_" + DateTime.Now.ToString("yyyy_MM") + ".pdf");

        }
    }
}
