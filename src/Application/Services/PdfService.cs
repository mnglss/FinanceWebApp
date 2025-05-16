using Domain.Entities;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Fields;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace Application.Services
{
    public static class PdfService
    {
        public static PdfDocument MovementPdf(List<Movement> movementList)
        {
            var document = new Document();

            BuildDocument(document, movementList);

            var pdfRederer = new PdfDocumentRenderer();        
            pdfRederer.Document = document;

            pdfRederer.RenderDocument();

            return pdfRederer.PdfDocument;
        }

        private static void BuildDocument(Document document, List<Movement> movementList)
        {
            Section section = new Section();
            section.PageSetup.StartingNumber = 1;

            

            var paragraph = section.AddParagraph();
            paragraph.AddText("Lista Movimenti");
            paragraph.AddLineBreak();
            paragraph.Format.SpaceAfter = 20;

            paragraph = section.AddParagraph();
            paragraph.AddText($"Aggiornata al: ");
            paragraph.Add(new DateField { Format = "dd/MM/yyy" });
            paragraph.AddLineBreak();
            paragraph.Format.SpaceAfter = 20;
            
            document.Sections.Add(section);
            var table = document.LastSection.AddTable();
            table.Borders.Width = 0.5;
            table.AddColumn("2cm");
            table.AddColumn("1,5cm");
            table.AddColumn("3cm");
            table.AddColumn("4cm");
            table.AddColumn("4cm");
            table.AddColumn("3cm");

            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Font.Bold = true;
            row.Cells[0].AddParagraph("Anno");
            row.Cells[1].AddParagraph("Mese");
            row.Cells[2].AddParagraph("Data");
            row.Cells[3].AddParagraph("Categoria");
            row.Cells[4].AddParagraph("Descrizione");
            row.Cells[5].AddParagraph("Importo");

            var totale = 0.0;
            foreach (var movement in movementList)
            {
                row = table.AddRow();
                row.Cells[0].AddParagraph($"{movement.Year}");
                row.Cells[1].AddParagraph($"{movement.Month}");
                row.Cells[2].AddParagraph(movement.Date.ToShortDateString());
                row.Cells[3].AddParagraph(movement.Category);
                row.Cells[4].AddParagraph(movement.Description);
                row.Cells[5].AddParagraph($"{movement.Amount}");
                totale += movement.Amount;
            }

            row = table.AddRow();
            row[4].AddParagraph("Rimanenza");
            row[5].AddParagraph($"{Math.Round(totale,2)}");


        }
    }
}
