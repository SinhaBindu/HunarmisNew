using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
//using HtmlToOpenXml;

namespace Hunarmis.Models
{
    public class ExportService
    {
        //public MemoryStream ExportToWord()
        //{
        //    string htmlValue = "";

        //    MemoryStream stream = new MemoryStream();
        //    WordprocessingDocument package = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document);

        //    MainDocumentPart mainPart = package.MainDocumentPart;
        //    if (mainPart == null)
        //    {
        //        mainPart = package.AddMainDocumentPart();
        //        new Document(new Body()).Save(mainPart);
        //    }

        //    HtmlConverter converter = new HtmlConverter(mainPart);
        //    converter.ParseHtml(htmlValue);

        //    mainPart.Document.Save();

        //    return stream;
        //}
    }
}