using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.IO;
using System.Net;
using Aws;

namespace Casing
{
    public partial class PrintSteelIndentPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DeletePdf deletePdfs = new DeletePdf();
            deletePdfs.DeletePdfFiles(Server.MapPath(@"~/PDFs/PrintSteelIndentPdf"), ".pdf");

            if (!IsPostBack)
            {
                DataTable data = null;
                String fetchaDate = null, reference = null;

                if (Session["PrintSteelIndentData"] != null)
                {
                    data = Session["PrintSteelIndentData"] as DataTable;
                }

                if (Session["fetchaDate"] != null)
                {
                    fetchaDate = Session["fetchaDate"].ToString();

                }

                if (Session["reference"] != null)
                {
                    reference = Session["reference"].ToString();

                }

                ReportParameter fetchaDateParameter = new ReportParameter("fetchaDate", fetchaDate);

                ReportParameter referenceDateParameter = new ReportParameter("reference", reference);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrintSteelIndent.rdlc");
                CasingDrawingReportData casingDrawingInfo = new CasingDrawingReportData();

                ReportDataSource datasource = new ReportDataSource("DataSet1", data);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.SetParameters(fetchaDateParameter);
                ReportViewer1.LocalReport.SetParameters(referenceDateParameter);

                ReportViewer1.LocalReport.DataSources.Add(datasource);

                SavePDF(ReportViewer1, Server.MapPath(@"~\PDFs\PrintSteelIndentPdf\PrintSteelIndentPdf.pdf"));


                if (File.Exists(Server.MapPath(@"~\PDFs\PrintSteelIndentPdf\PrintSteelIndentPdf.pdf")))
                {
                    string path = Server.MapPath(@"~\PDFs\PrintSteelIndentPdf\PrintSteelIndentPdf.pdf");
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
        public void SavePDF(ReportViewer viewer, string savePath)
        {
            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.2in</MarginTop>" +
                    "  <MarginLeft>0.2in</MarginLeft>" +
                    "  <MarginRight>0.2in</MarginRight>" +
                    "  <MarginBottom>0.2in</MarginBottom>" +
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