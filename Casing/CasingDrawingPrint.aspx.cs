using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.UI;
using Aws;

namespace Casing
{
    public partial class CasingDrawingPrint : System.Web.UI.Page
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(connectionString);
        bool isModelImageExist;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable data = Session["PrintDrawingData"] as DataTable;
            String ModelImage = "";
            Exception ex = null;

            if (!IsPostBack)
            {

                DeletePdf deletePdfs = new DeletePdf();
                deletePdfs.DeletePdfFiles(Server.MapPath(@"~/PDFs/casingDrawingPdfs/"), ".pdf");
                deletePdfs.DeletePdfFiles(Server.MapPath(@"~/PDFs/mergedPdfs"), ".pdf");
                deletePdfs.DeletePdfFiles(Server.MapPath(@"~/QrCode"), ".Jpeg");


                try
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        DataTable dt = new DataTable();
                        dt = data.Clone();

                        for (int j = i; j == i; j++)
                        {
                            if (data.Rows[j][8].ToString() == "*0")
                            {
                                isModelImageExist = File.Exists(Server.MapPath("~/modelImages/0.jpg"));
                            }
                            else
                            {
                                isModelImageExist = File.Exists(Server.MapPath("~/modelImages/" + data.Rows[j][8].ToString() + ".jpg"));
                            }

                            if (isModelImageExist)
                            {
                                if (data.Rows[j][8].ToString() == "*0")
                                {
                                    ModelImage = "0.jpg";
                                }
                                else
                                {
                                    ModelImage = data.Rows[j][8].ToString() + ".jpg";                                    
                                }

                                dt.Rows.Add(data.Rows[j][0].ToString(), data.Rows[j][1].ToString(), data.Rows[j][2].ToString(), data.Rows[j][3].ToString(), data.Rows[j][4].ToString(), data.Rows[j][5].ToString(), data.Rows[j][6].ToString(), data.Rows[j][7].ToString(), data.Rows[j][8].ToString(), data.Rows[j][9].ToString(), data.Rows[j][10].ToString(), data.Rows[j][11].ToString(), data.Rows[j][12].ToString(), data.Rows[j][13].ToString(), data.Rows[j][14].ToString(), data.Rows[j][15].ToString(), data.Rows[j][16].ToString(), data.Rows[j][17].ToString(), data.Rows[j][18].ToString());

                                QrCodeGenerator qrcode = new QrCodeGenerator();
                                qrcode.qrCode(data.Rows[j][0].ToString(), Server.MapPath(@"~/QrCode/" + data.Rows[j][0].ToString() + ".Jpeg"));


                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CasingDrawingReport.rdlc");
                                CasingDrawingReportData casingDrawingInfo = new CasingDrawingReportData();

                                string ModelImagePath = new Uri(Server.MapPath(@"~/modelImages/" + ModelImage)).AbsoluteUri;
                                ReportParameter modelImageParameter = new ReportParameter("ModelImagePath", ModelImagePath);
                                ReportViewer1.LocalReport.SetParameters(modelImageParameter);


                                String QRCODEIMAGE = new Uri(Server.MapPath(@"~/QrCode/" + data.Rows[j][0].ToString() + ".Jpeg")).AbsoluteUri;

                                ReportParameter QrCodeParameter = new ReportParameter("QRCODEIMAGE", QRCODEIMAGE);
                                if (QrCodeParameter!=null)
                                ReportViewer1.LocalReport.SetParameters(QrCodeParameter);

                                if (data.Rows[j][0].ToString() != "")
                                {
                                    conn.Open();
                                    OleDbCommand cmd = new OleDbCommand("SELECT EntOrd from [Ordenes de fabricación] where NumOrd= " + data.Rows[j][0].ToString(), conn);
                                    OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
                                    DataTable deldatedata=new DataTable();
                                    adap.Fill(deldatedata);

                                    if (deldatedata.Rows[0]!=null)
                                    {
                                        CultureInfo cul = CultureInfo.CurrentCulture;
                                        string WeekNoOfDelDate = cul.Calendar.GetWeekOfYear(Convert.ToDateTime(deldatedata.Rows[0]["EntOrd"].ToString()), CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
                                        ReportParameter WeekNoOfDelDateParameter = new ReportParameter("WeekNoOfDelDate", WeekNoOfDelDate);
                                        ReportViewer1.LocalReport.SetParameters(WeekNoOfDelDateParameter);


                                        string fifteenDaysBeforeDeliveries = Convert.ToDateTime(deldatedata.Rows[0]["EntOrd"].ToString()).AddDays(-15).ToString("dd-MMM-yyyy");
                                        ReportParameter fifteenDaysBeforeDeliveriesParameter = new ReportParameter("fifteenDaysBeforeDeliveries", fifteenDaysBeforeDeliveries);
                                        ReportViewer1.LocalReport.SetParameters(fifteenDaysBeforeDeliveriesParameter);
                                    }

                                    conn.Close();
                                    deldatedata.Clear();
                                }

                                string CasingDate = data.Rows[j][18].ToString();
                                if (CasingDate != "")
                                {
                                    ReportParameter CasingDateParameter = new ReportParameter("CasingDate", CasingDate);
                                    ReportViewer1.LocalReport.SetParameters(CasingDateParameter);
                                }
                                
                                ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(datasource);

                                SavePDF(ReportViewer1, Server.MapPath(@"~\PDFs\casingDrawingPdfs\" + data.Rows[j][0].ToString() + ".pdf"));

                                DirectoryInfo d = new DirectoryInfo(Server.MapPath(@"~/PDFs/casingDrawingPdfs"));//Assuming Test is your Folder
                                FileInfo[] Files = d.GetFiles("*.pdf"); //Getting Text files

                                string[] str = new string[Files.Length];
                                int p = 0;
                                foreach (FileInfo fil in Files)
                                {
                                    str[p] = Server.MapPath(@"~/PDFs/casingDrawingPdfs/") + fil.Name;
                                    p++;
                                }

                                CombineMultiplePDFs(str, Server.MapPath(@"~/PDFs/mergedPdfs/PdfAfterMerged.pdf"));
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ModelImage + "'Casing Model Image Not Found...!')", true);
                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    ex = x;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.ToString() + "')", true);
                }
                if (ex == null)
                {
                    if (File.Exists(Server.MapPath("~/PDFs/mergedPdfs/PdfAfterMerged.pdf")))
                    {
                        string path = Server.MapPath("~/PDFs/mergedPdfs/PdfAfterMerged.pdf");
                        WebClient client = new WebClient();
                        Byte[] buffer = client.DownloadData(path);

                        if (buffer != null)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", buffer.Length.ToString());
                            Response.BinaryWrite(buffer);
                        }

                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ ex.Message + " ')", true);
                    Response.Write("Error: " + ex.Message);
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
        public static void CombineMultiplePDFs(string[] fileNames, string outFile)
        {
            Document document = new Document();
            using (FileStream newFileStream = new FileStream(outFile, FileMode.Create))
            {
                PdfCopy writer = new PdfCopy(document, newFileStream);
                if (writer == null)
                {
                    return;
                }
                document.Open();

                foreach (string fileName in fileNames)
                {
                    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(fileName);
                    reader.ConsolidateNamedDestinations();

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, i);
                        writer.AddPage(page);
                    }

                    PRAcroForm form = reader.AcroForm;
                    if (form != null)
                    {
                        writer.CopyAcroForm(reader);
                    }

                    reader.Close();
                }

                writer.Close();
                document.Close();
            }
        }
    }
}