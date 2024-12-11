using CEMS_Server.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

public class PdfService
{
    public byte[] GenerateApproverPdf(List<CemsApprover> approvers)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Content().Column(column =>
                {
                    column.Item().Text("Approver List")
                        .FontSize(20)
                        .Bold();

                    column.Item().Text($"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm}");

                    foreach (var approver in approvers)
                    {
                        column.Item().Text($"ID: {approver.ApId}, User ID: {approver.ApUsrId}, Sequence: {approver.ApSequence ?? 0}")
                            .FontSize(14);

                        if (approver.ApUsr != null)
                        {
                            column.Item().Text($"User Info: {approver.ApUsr}")
                                .FontSize(12)
                                .Italic();
                        }

                        if (approver.CemsApproverRequistions.Any())
                        {
                            // Define the custom TextStyle and apply it
                            var requisitionStyle = TextStyle.Default.FontSize(12).FontColor("#808080"); // Gray color

                            column.Item().Text($"Requisitions: {approver.CemsApproverRequistions.Count}")
                                .Style(requisitionStyle); // Apply the style
                        }

                        column.Item().Text("-------------------------------");
                    }
                });
            });
        });

        return document.GeneratePdf();
    }
}
