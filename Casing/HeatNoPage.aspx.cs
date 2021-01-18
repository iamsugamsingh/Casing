using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Casing
{
    public partial class HeatNoPage : System.Web.UI.Page
    {
        OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRow();
                GridViewRow row = GridView1.Rows[GridView1.Rows.Count - 1];
                TextBox uIdTextBox = (TextBox)row.FindControl("uidTxtBox");
                TextBox dateTxtBox = (TextBox)row.FindControl("dateTxtBox");
                uIdTextBox.Focus();
                dateTxtBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("uid", typeof(string)));
            dt.Columns.Add(new DataColumn("heatNo", typeof(string)));
            dt.Columns.Add(new DataColumn("date", typeof(string)));
            dt.Columns.Add(new DataColumn("length", typeof(string)));
            dt.Columns.Add(new DataColumn("diameter", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["uid"] = string.Empty;
            dr["heatNo"] = string.Empty;
            dr["date"] = string.Empty;
            dr["length"] = string.Empty;
            dr["diameter"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox uid = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("uidTxtBox");
                        TextBox heatNo = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("heatNoTxtBox");
                        TextBox date = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("dateTxtBox");
                        TextBox length = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("lengthTxtBox");
                        TextBox diameter = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("diameterTxtBox");

                        date.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["uid"] = uid.Text;
                        dtCurrentTable.Rows[i - 1]["heatNo"] = heatNo.Text;
                        dtCurrentTable.Rows[i - 1]["date"] = date.Text;
                        dtCurrentTable.Rows[i - 1]["length"] = length.Text;
                        dtCurrentTable.Rows[i - 1]["diameter"] = diameter.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox uid = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("uidTxtBox");
                        TextBox heatNo = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("heatNoTxtBox");
                        TextBox date = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("dateTxtBox");
                        TextBox length = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("lengthTxtBox");
                        TextBox diameter = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("diameterTxtBox");

                        date.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                        uid.Text = dt.Rows[i]["uid"].ToString();
                        heatNo.Text = dt.Rows[i]["heatNo"].ToString();
                        date.Text = dt.Rows[i]["date"].ToString();
                        length.Text = dt.Rows[i]["length"].ToString();
                        diameter.Text = dt.Rows[i]["diameter"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
            GridViewRow row = GridView1.Rows[GridView1.Rows.Count - 1];
            TextBox uIdTextBox = (TextBox)row.FindControl("uidTxtBox");
            TextBox dateTxtBox = (TextBox)row.FindControl("dateTxtBox");
            dateTxtBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            uIdTextBox.Focus();
        }

        protected void removeBtn_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox uid = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("uidTxtBox");
                        TextBox heatNo = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("heatNoTxtBox");
                        TextBox date = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("dateTxtBox");
                        TextBox length = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("lengthTxtBox");
                        TextBox diameter = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("diameterTxtBox");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["uid"] = uid.Text;
                        dtCurrentTable.Rows[i - 1]["heatNo"] = heatNo.Text;
                        dtCurrentTable.Rows[i - 1]["date"] = date.Text;
                        dtCurrentTable.Rows[i - 1]["length"] = length.Text;
                        dtCurrentTable.Rows[i - 1]["diameter"] = diameter.Text;
                        rowIndex++;
                    }

                    //dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            Button lb = (Button)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                dt.Rows.Remove(dt.Rows[rowID]);

                //Store the current data in ViewState for future reference  
                ViewState["CurrentTable"] = dt;

                //Re bind the GridView for the updated data  
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            //Set Previous Data on Postbacks  
            //totalRowsCount.Text = GridView1.Rows.Count.ToString();
            SetPreviousData();
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox uidTxtBox = row.FindControl("uidTxtBox") as TextBox;
                TextBox heatNoTxtBox = row.FindControl("heatNoTxtBox") as TextBox;
                TextBox dateTxtBox = row.FindControl("dateTxtBox") as TextBox;
                TextBox lengthTxtBox = row.FindControl("lengthTxtBox") as TextBox;
                TextBox diameterTxtBox = row.FindControl("diameterTxtBox") as TextBox;
                try
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO [HeatNoTable] (uid, heatNo, dateTimes, length, diameter) values (uid, heatNo, dateTimes, length, diameter)", con);

                    cmd.Parameters.Add("uid", OleDbType.Integer).Value = uidTxtBox.Text;
                    cmd.Parameters.Add("heatNo", OleDbType.VarChar).Value = heatNoTxtBox.Text;
                    cmd.Parameters.Add("dateTimes", OleDbType.Date).Value = dateTxtBox.Text;
                    cmd.Parameters.Add("length", OleDbType.Decimal).Value = lengthTxtBox.Text;
                    cmd.Parameters.Add("diameter", OleDbType.Decimal).Value = diameterTxtBox.Text;

                    if (cmd.ExecuteNonQuery()>0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Successfully...!')", true);
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);

                }
            }
        }
    }
}