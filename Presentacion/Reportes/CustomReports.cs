using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;

using System.Web.UI.WebControls;


namespace Presentacion.Reportes
{
    public class CustomReports
    {
        DateTime PrintTime = DateTime.Now;
        public MemoryStream CreatePDF(string Title, ArrayList dt)
        {
            MemoryStream PDFData = new MemoryStream();
            Document document = new Document(PageSize.LETTER, 50, 50, 80, 50);
            PdfWriter PDFWriter = PdfWriter.GetInstance(document, PDFData);
            PDFWriter.ViewerPreferences = PdfWriter.PageModeUseOutlines;
            // Our custom Header and Footer is done using Event Handler
            TwoColumnHeaderFooter PageEventHandler = new TwoColumnHeaderFooter();
            PDFWriter.PageEvent = PageEventHandler;
            // Define the page header
            PageEventHandler.Title = Title;
            PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.TIMES_BOLD, 10, Font.BOLD);
            PageEventHandler.HeaderLeft = " ";
            PageEventHandler.HeaderRight = "Fecha de impresion: " + PrintTime.ToString();
            document.Open();
            if (dt != null)
            {
                ArrayList headCol = (ArrayList)dt[0];
                int contGen = 1;
                for (int i = 1; contGen < dt.Count; i++)
                {
                    //PageEventHandler.HeaderRight = i.ToString();
                    if (i != 1)
                    {
                        document.NewPage();
                    }

                    AddOutline(PDFWriter, "Fecha de impresion: " + PrintTime.ToString(), document.PageSize.Height);
                    document.Add(new Paragraph("\r\n"));
                    document.Add(new Paragraph("\r\n"));
                    document.Add(new Paragraph("\r\n"));
                    document.Add(new Paragraph("\r\n"));
                    document.Add(new Paragraph("\r\n"));
                    //Header columnas
                    PdfPTable tableHead = new PdfPTable(headCol.Count);
                    for (int j = 0; j < headCol.Count; j++)
                    {

                        PdfPCell cell = new PdfPCell(new Phrase((string)headCol[j], new Font(Font.FontFamily.TIMES_ROMAN, 9f, Font.BOLD, BaseColor.BLACK)));

                        cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightGray);
                        tableHead.AddCell(cell);
                    }

                    document.Add(tableHead);

                    for (int j = 1; contGen < dt.Count && j < 30;)
                    {
                        ArrayList reg = (ArrayList)dt[j];
                        PdfPTable ItemTable = new PdfPTable(reg.Count); //Table(/*reg.Count*/);
                                                                        //ItemTable.TableFitsPage = true;
                                                                        //ItemTable.Width = 95;
                                                                        //ItemTable.Offset = 0;
                                                                        //ItemTable.Border = 0;
                                                                        //ItemTable.DefaultCellBorder = 0;
                        for (int k = 0; k < reg.Count; k++)
                        {
                            ItemTable.AddCell(new PdfPCell(new Phrase((string)reg[k], new Font(Font.FontFamily.TIMES_ROMAN, 8f, Font.NORMAL, BaseColor.BLACK))));
                        }
                        document.Add(ItemTable);
                        //document.Add(new Paragraph("\r\n"));
                        contGen++;
                        j++;

                    }


                }
            }
            document.Close();
            return PDFData;
        }
        public void AddOutline(PdfWriter writer, string Title, float Position)
        {
            PdfDestination destination = new PdfDestination(PdfDestination.FITH, Position);
            PdfOutline outline = new PdfOutline(writer.DirectContent.RootOutline, destination, Title);
            writer.DirectContent.AddOutline(outline, "Name = " + Title);
        }
    }

}
