using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using System.Drawing.Imaging;


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

        public void GetPDF(PDFType type, int resultId)
        {
            var result = _resultRepo.GetResultWithDetailById(resultId);
            var participantPhoto = _documentRepository.GetLatestParticipantPhotoByParticipantId(result.ParticipantId);

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

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                returnImage = Image.FromStream(ms);
            }
            return returnImage;
        }

        // Convert the Bitmap object to a byte array to insert into the database.
        private byte[] ConvertImageToByteArray(Image imageToConvert, ImageFormat formatOfImage)
        {
            byte[] byteImage;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    byteImage = ms.ToArray();
                }
            }

            catch (Exception) { throw; }
            return byteImage;
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

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var font = new XFont("Arial", 10, XFontStyle.Regular);

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

            // Get the Result and Result Detail data to add to the PDF
            gfx.DrawString(result.OBR_2_1, TestResultFont, XBrushes.Black, xPoint.X + 115, xPoint.Y + 70);
            gfx.DrawString(result.PID_5_2 + ", " + result.PID_5_1, font, XBrushes.Black, xPoint.X + 100, xPoint.Y + 85);
            gfx.DrawString(result.OBR_3_1, font, XBrushes.Black, xPoint.X + 100, xPoint.Y + 100);
            gfx.DrawString(result.OBR_3_1, font, XBrushes.Black, xPoint.X + 100, xPoint.Y + 115);
            gfx.DrawString(result.PID_2_1, font, XBrushes.Black, xPoint.X + 100, xPoint.Y + 130);


            gfx.DrawString(result.OBR_7_1.ToString("MM/dd/yyyy") + " " + result.OBR_7_2.ToString(), font, XBrushes.Black, xPoint.X + 350, xPoint.Y + 85);
            gfx.DrawString(result.OBR_14_1.ToString("MM/dd/yyyy") + " " + result.OBR_14_2.ToString(), font, XBrushes.Black, xPoint.X + 350, xPoint.Y + 100);

            gfx.DrawString(result.OBX_14_1.ToString("MM/dd/yyyy") + " " + result.OBX_14_2.ToString(), font, XBrushes.Black, xPoint.X + 350, xPoint.Y + 130);
            gfx.DrawString(result.Panel.SpecimenType, font, XBrushes.Black, xPoint.X + 350, xPoint.Y + 145);

            // Panel Name
            gfx.DrawString("Test", font, XBrushes.Black, xPoint.X + 12, xPoint.Y + 187);
            gfx.DrawString("Result", font, XBrushes.Black, xPoint.X + 202, xPoint.Y + 187);
            gfx.DrawString("Level", font, XBrushes.Black, xPoint.X + 455, xPoint.Y + 187);
            gfx.DrawString("Cutoff", font, XBrushes.Black, xPoint.X + 528, xPoint.Y + 187);


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



            
            MemoryStream ms = new MemoryStream(participantPhoto);       
            XImage participantImage = XImage.FromStream(ms);
                gfx.DrawImage(participantImage, xPoint.X + 460, xPoint.Y + 10, 175, 140);


            // Run this if, if the Result Details Record contains a header note (NTE_3_1)
            if (result.ResultDetails.FirstOrDefault().NTE_3_1 != null)
            {
                // Create the header record if the NTE_3_1 is not empty
                gfx.DrawLine(pen, xPoint.X + xTableVal, xPoint.X + xTableVal2 + 60, xPoint.Y + yTableVal, xPoint.Y + yTableVal2 + 60);
                gfx.DrawString(result.ResultDetails.FirstOrDefault().NTE_3_1, font, XBrushes.Black, detailsXPoint.X + 10, detailsXPoint.Y);

                // Line that gets looped through
                gfx.DrawLine(pen, xPoint.X + xTableVal, xPoint.X + xTableValLooped + 75, xPoint.Y + yTableVal, xPoint.Y + yTableValLooped + 75);


                // Loop through result details that have a header note
                result.ResultDetails.ForEach(x =>
                {
                    // bottom line under Test /Result / Level / Cutoff
                    gfx.DrawLine(pen, xPoint.X + xTableVal, xPoint.X + xTableVal2 + 15, xPoint.Y + yTableVal, xPoint.Y + yTableVal2 + 15);

                    if (x.OBX_3_2 != null)
                    {
                        // Test Name
                        gfx.DrawString(x.OBX_3_2, font, XBrushes.Black, detailsXPoint.X, detailsXPoint.Y + 45);

                        // Result
                        var resultAsString = x.OBX_8_1 == "N" ? x.OBX_8_1 = "Negative" : x.OBX_8_1 == "A" ?
                        x.OBX_8_1 = "Abnormal" : "No Result";

                        gfx.DrawString(x.OBX_8_1, font, XBrushes.Black, detailsXPoint.X + 195, detailsXPoint.Y + 45);


                        // Cutoff
                        if (x.OBX_7_1 != null)
                        {

                            gfx.DrawString(x.OBX_7_1, font, XBrushes.Black, detailsXPoint.X + 520, detailsXPoint.Y + 45);
                        }


                        gfx.DrawLine(pen, xPoint.X + 10, xPoint.X + xTableValLooped + 250, xPoint.Y + 600, xPoint.Y + yTableValLooped + 250);


                        xTableValLooped = xTableValLooped + 15;
                        yTableValLooped = yTableValLooped + 15;
                        detailsXPoint.Y = detailsXPoint.Y + 15;
                    }
                    else
                    {
                        // AM testing For Footer Note logic
                        // Check the row for the Footer notes
                        if (x.ItemIndex > 0 && x.LineType == "NTE")
                        {
                            var xFooterValLooped = 475;
                            var yFooterValLooped = 475;
                            var stringY = 65;


                            gfx.DrawLine(pen, xPoint.X + 10, xPoint.X + xTableValLooped + 250, xPoint.Y + 600, xPoint.Y + yTableValLooped + 250);

                            // Set NoteDrawLine Settings                           
                            gfx.DrawString(x.NTE_3_1, font, XBrushes.Black, detailsXPoint.X, detailsXPoint.Y + stringY);
                            gfx.DrawLine(pen, xPoint.X + 10, xPoint.X + xFooterValLooped, xPoint.Y + 600, xPoint.Y + yFooterValLooped);

                            xFooterValLooped = xFooterValLooped + 15;
                            yFooterValLooped = yFooterValLooped + 15;
                            stringY = stringY + 15;

                        }

                        // END AM Testing
                    }



                });




            }

            //TODO: PDF will be sent as a stream back to the requestor 
            document.Save($"C:\\Temp\\TestGuardianPDF_" + DateTime.Now.ToString("yyyy_MM") + ".pdf");



        }




    }
}
