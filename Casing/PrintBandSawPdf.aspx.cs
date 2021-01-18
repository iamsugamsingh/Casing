using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aws;

namespace Casing
{
    public partial class PrintBandSawPdf : System.Web.UI.Page
    {
        static DataTable PrintBandSawData;
        protected void Page_Load(object sender, EventArgs e)
        {
            DeletePdf deletePdfs = new DeletePdf();
            deletePdfs.DeletePdfFiles(Server.MapPath(@"~/PDFs/BandSawJobCardPdf"),".pdf");

            if (!IsPostBack)
            {
                string reference = "", fechaDate = "";
                if (Session["reference"] != null)
                {
                    reference = Session["reference"].ToString();
                }

                if (Session["fetchaDate"] != null)
                {
                    fechaDate = Session["fetchaDate"].ToString();
                }

                if (Session["PrintBandSawData"] != null)
                {
                    PrintBandSawData = Session["PrintBandSawData"] as DataTable;

                    ReportParameter fetchaDateParameter = new ReportParameter("fetchaDate", fechaDate);

                    ReportParameter referenceDateParameter = new ReportParameter("reference", reference);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrintBandSaw.rdlc");
                    CasingDrawingReportData casingDrawingInfo = new CasingDrawingReportData();

                    ReportDataSource datasource = new ReportDataSource("DataSet1", PrintBandSawData);
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportViewer1.LocalReport.SetParameters(fetchaDateParameter);
                    ReportViewer1.LocalReport.SetParameters(referenceDateParameter);
                    ReportViewer1.LocalReport.DataSources.Add(datasource);

                    SavePDF(ReportViewer1, Server.MapPath(@"~\PDFs\BandSawJobCardPdf\BandSawPdf.pdf"));

                    if (File.Exists(Server.MapPath(@"~\PDFs\BandSawJobCardPdf\BandSawPdf.pdf")))
                    {
                        string path = Server.MapPath(@"~\PDFs\BandSawJobCardPdf\BandSawPdf.pdf");
                        WebClient client = new WebClient();
                        Byte[] buffer = client.DownloadData(path);

                        if (buffer != null)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", buffer.Length.ToString());
                            Response.BinaryWrite(buffer);
                        }

                    }
                    else
                    {
                        Response.Write("Error: Print Steel indent Pdf Cant Be Generated");
                    }
                }
            }
        }

        public void SavePDF(ReportViewer viewer, string savePath)
        {
            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.4in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.1in</MarginTop>" +
                    "  <MarginLeft>0.1in</MarginLeft>" +
                    "  <MarginRight>0.1in</MarginRight>" +
                    "  <MarginBottom>0.1in</MarginBottom>" +
                    "  <HumanReadablePDF>True</HumanReadablePDF>" +
                    "</DeviceInfo>";
            byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: deviceInfo);

            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                stream.Write(Bytes, 0, Bytes.Length);
            }
        }

    }
}