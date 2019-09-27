using GuardianAPI.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.BLL
{
    public static class PDFCreator
    {        

        public static void GetPDF(PDFType type, Participant participant)
        {
            switch (type)
            {
                case PDFType.GuardianExportPDF:

                    // Call the Create GuardianPDFResult method
                    CreateGuardianPDFResult(participant.Id);
                    break;

                case PDFType.None:
                    break;

                case PDFType.SomeOtherReport:

                default:
                    break;
            }
        }

        public static void CreateGuardianPDFResult(int id)
        {
            // TODO : PDF Logic to create PDF goes here
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 10, XFontStyle.Regular);

            //Header info font
            var TestResultFont = new XFont("Arial", 20, XFontStyle.Bold);

            // PDFSharp Settings
            XPoint xPoint = new XPoint();
            xPoint.X = 0;
            xPoint.Y = 0;
            var color = new XColor();
            var pen = new XPen(color);



            // Header Image
            XImage headerImage = XImage.FromFile(@"C:\temp\psi.jpg");
            gfx.DrawImage(headerImage, xPoint.X + 10, xPoint.Y + 10, 300, 45);

            // Title Result Header
            gfx.DrawString($"Test Result #123456789", TestResultFont, XBrushes.Black, xPoint.X + 5, xPoint.Y + 70);

            //Top Left Column Labels
            gfx.DrawString($"Name", font, XBrushes.Black, xPoint.X + 5, xPoint.Y + 85);
            gfx.DrawString($"Accession #", font, XBrushes.Black, xPoint.X + 5, xPoint.Y + 100);
            gfx.DrawString($"Chain of Custody #", font, XBrushes.Black, xPoint.X + 5, xPoint.Y + 115);
            gfx.DrawString($"ID / SSN", font, XBrushes.Black, xPoint.X + 5, xPoint.Y + 130);

            // TODO: Values for the PDF HERE

            // Top Right Column Labels
            gfx.DrawString($"Collected", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 85);
            gfx.DrawString($"Ordered", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 100);
            gfx.DrawString($"Received", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 115);
            gfx.DrawString($"Completed", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 130);
            gfx.DrawString($"Specimen Type", font, XBrushes.Black, xPoint.X + 260, xPoint.Y + 145);

            // TODO: Insert Image here
            XImage participantImage = XImage.FromFile(@"C:\temp\sample.jpg");
            gfx.DrawImage(participantImage, xPoint.X + 450, xPoint.Y + 10);


            //    Create Table
            // 16 regular horizontal lines in regular table
            var xTableVal = 10;
            var xTableVal2 = 175;
            var yTableVal = 600;
            var yTableVal2 = 175;

            // Create horizontal lines
            for (var i = 0; i <= 19; i++)
            {
                if (i <= 15)
                {
                    gfx.DrawLine(pen, xPoint.X + xTableVal, xPoint.X + xTableVal2, xPoint.Y + yTableVal, xPoint.Y + yTableVal2);
                    xTableVal2 = xTableVal2 + 15;
                    yTableVal2 = yTableVal2 + 15;
                }

                if (i == 16)
                {
                    gfx.DrawLine(pen, xTableVal, xTableVal2 + 60, yTableVal, yTableVal2 + 60);
                }
            }

            gfx.DrawLine(pen, xTableVal, xTableVal2 + 100, yTableVal, yTableVal2 + 100);


            gfx.DrawLine(pen, xTableVal, xTableVal2 + 200, yTableVal, yTableVal2 + 200);
            gfx.DrawLine(pen, xTableVal, xTableVal2 + 150, yTableVal, yTableVal2 + 150);


            // Far left Horizontal line
            gfx.DrawLine(pen, xPoint.X = 10, xPoint.X = 175, xPoint.Y = 10, xPoint.Y + 605);

            // Far right Horizontal line
            gfx.DrawLine(pen, xPoint.X = 600, xPoint.X = 175, xPoint.Y = 600, xPoint.Y = 615);

            // Table Vertical Lines
            gfx.DrawLine(pen, xPoint.X = 200, xPoint.X = 175, xPoint.Y = 200, xPoint.Y = 385);
            gfx.DrawLine(pen, xPoint.X = 450, xPoint.X = 175, xPoint.Y = 450, xPoint.Y = 385);
            gfx.DrawLine(pen, xPoint.X = 525, xPoint.X = 175, xPoint.Y = 525, xPoint.Y = 385);




















            //    gfx.DrawString($"DCS Invoice for some month", font, XBrushes.OrangeRed, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);
            //    gfx.DrawString($"PROVIDER NAME: ", headerInfoFont, XBrushes.Black, xPoint.X + 30, xPoint.Y + 50);
            //    gfx.DrawString($"PROVIDER ID / FEIN#: ", headerInfoFont, XBrushes.Black, xPoint.X + 14, xPoint.Y + 65);

            //TODO: PDF will be sent as a stream back to the requestor 
            document.Save($"C:\\Temp\\TestGuardianPDF_" + DateTime.Now.ToString("yyyy_MM") + ".pdf");



        }



    }
}
