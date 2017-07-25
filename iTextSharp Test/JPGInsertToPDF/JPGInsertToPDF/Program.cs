using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace JPGInsertToPDF
{
    class Program
    {
        static void Main(string[] args)
        {

            using (Stream inputPdfStream = new FileStream(@"C:\dev\PV Drones\iTextSharp Test\input.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new FileStream(@"C:\dev\PV Drones\iTextSharp Test\test.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(@"C:\dev\PV Drones\iTextSharp Test\result.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);
                ColumnText ct = new ColumnText(stamper.GetOverContent(2));

                Image image = Image.GetInstance(inputImageStream);
                image.SetAbsolutePosition(150, 100);
                image.ScalePercent(10);
                pdfContentByte.AddImage(image);
                
                Font font8 = FontFactory.GetFont("ARIAL", 7);
                
                Paragraph paragraph = new Paragraph("Datatable Test");

                PdfPTable table = new PdfPTable(3);

                table.AddCell("Row 1, Col 1");
                table.AddCell("Row 1, Col 2");
                table.AddCell("Row 1, Col 3");

                table.AddCell("Row 2, Col 1");
                table.AddCell("Row 2, Col 2");
                table.AddCell("Row 2, Col 3");

                table.AddCell("Row 3, Col 1");
                table.AddCell("Row 3, Col 2");
                table.AddCell("Row 3, Col 3");

                ct.AddElement(table);
                Rectangle rect = new Rectangle(146, 500, 530, 36); //1 - moves to the left or right, 2 - moves up or down, 3 - width of ccells, 4 - not sure
                ct.SetSimpleColumn(rect);
                ct.Go();
                
                stamper.Close();
            }
        }
        
    }
}
