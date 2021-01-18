using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Casing
{
    public partial class CasingDrawing : System.Web.UI.Page
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(connectionString);
        public string location;
        protected void Page_Load(object sender, EventArgs e)
        {
            //location = Request.QueryString["location"].ToString();

            if (!Page.IsPostBack)
            {
                SetInitialRow();
                GridViewRow row = GridView1.Rows[GridView1.Rows.Count - 1];
                TextBox uIdTextBox = (TextBox)row.FindControl("TextBox1");
                uIdTextBox.Focus();

                /*------Getting Reference number from  [Parámetros] Table------------*/

                try
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand("Select * from [Parámetros] where CodPar='Última referencia'", conn);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            refTxtBox.Text = (Convert.ToInt32(dr["ValPar"]) + 1).ToString();        //Increamenting reference data by 1........
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Reference Value can't found in database...');</script>");
                    }
                    conn.Close();

                    currentDateTxtBox.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                }
                catch (Exception ex)
                {
                    //Response.Write("Something went wrong! while fetching the 'Reference Textbox' Data from the Database in onPageLoad()...." + ex.Message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong! while fetching the 'Reference Textbox' Data from the Database in onPageLoad()....'" + ex.Message + " ')", true);
                }
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("UID", typeof(string)));
            dt.Columns.Add(new DataColumn("Pie", typeof(string)));
            dt.Columns.Add(new DataColumn("Multiplier", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Total", typeof(string)));
            dt.Columns.Add(new DataColumn("Make", typeof(string)));
            dt.Columns.Add(new DataColumn("Grade", typeof(string)));
            dt.Columns.Add(new DataColumn("Hardness", typeof(string)));
            dt.Columns.Add(new DataColumn("Model", typeof(string)));
            dt.Columns.Add(new DataColumn("OuterDiameter", typeof(string)));
            dt.Columns.Add(new DataColumn("Length", typeof(string)));
            dt.Columns.Add(new DataColumn("InternalDiaMeter", typeof(string)));
            dt.Columns.Add(new DataColumn("Dimen4", typeof(string)));
            dt.Columns.Add(new DataColumn("Dimen5", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["UID"] = string.Empty;
            dr["Pie"] = string.Empty;
            dr["Multiplier"] = string.Empty;
            dr["Qty"] = string.Empty;
            dr["Total"] = string.Empty;
            dr["Make"] = string.Empty;
            dr["Grade"] = string.Empty;
            dr["Hardness"] = string.Empty;
            dr["Model"] = string.Empty;
            dr["OuterDiameter"] = string.Empty;
            dr["Length"] = string.Empty;
            dr["InternalDiaMeter"] = string.Empty;
            dr["Dimen4"] = string.Empty;
            dr["Dimen5"] = string.Empty;

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
                        TextBox box1 = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("TextBox4");
                        TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("TextBox5");
                        TextBox box6 = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("TextBox6");
                        TextBox box7 = (TextBox)GridView1.Rows[rowIndex].Cells[7].FindControl("TextBox7");
                        TextBox box8 = (TextBox)GridView1.Rows[rowIndex].Cells[8].FindControl("TextBox8");

                        DropDownList ModalDDL = (DropDownList)GridView1.Rows[rowIndex].Cells[10].FindControl("DropDownList1");
                        TextBox box11 = (TextBox)GridView1.Rows[rowIndex].Cells[11].FindControl("TextBox11");
                        TextBox box12 = (TextBox)GridView1.Rows[rowIndex].Cells[12].FindControl("TextBox12");
                        TextBox box13 = (TextBox)GridView1.Rows[rowIndex].Cells[13].FindControl("TextBox13");
                        TextBox box14 = (TextBox)GridView1.Rows[rowIndex].Cells[14].FindControl("TextBox14");
                        TextBox box15 = (TextBox)GridView1.Rows[rowIndex].Cells[15].FindControl("TextBox15");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["UID"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Pie"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["Multiplier"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["Qty"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Total"] = box5.Text;
                        dtCurrentTable.Rows[i - 1]["Make"] = box6.Text;
                        dtCurrentTable.Rows[i - 1]["Grade"] = box7.Text;
                        dtCurrentTable.Rows[i - 1]["Hardness"] = box8.Text;

                        dtCurrentTable.Rows[i - 1]["Model"] = ModalDDL.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["OuterDiameter"] = box11.Text;
                        dtCurrentTable.Rows[i - 1]["Length"] = box12.Text;
                        dtCurrentTable.Rows[i - 1]["InternalDiameter"] = box13.Text;
                        dtCurrentTable.Rows[i - 1]["Dimen4"] = box14.Text;
                        dtCurrentTable.Rows[i - 1]["Dimen5"] = box15.Text;

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
                        TextBox box1 = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("TextBox4");
                        TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("TextBox5");
                        TextBox box6 = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("TextBox6");
                        TextBox box7 = (TextBox)GridView1.Rows[rowIndex].Cells[7].FindControl("TextBox7");
                        TextBox box8 = (TextBox)GridView1.Rows[rowIndex].Cells[8].FindControl("TextBox8");

                        DropDownList ModalDDL = (DropDownList)GridView1.Rows[rowIndex].Cells[10].FindControl("DropDownList1");
                        TextBox box11 = (TextBox)GridView1.Rows[rowIndex].Cells[11].FindControl("TextBox11");
                        TextBox box12 = (TextBox)GridView1.Rows[rowIndex].Cells[12].FindControl("TextBox12");
                        TextBox box13 = (TextBox)GridView1.Rows[rowIndex].Cells[13].FindControl("TextBox13");
                        TextBox box14 = (TextBox)GridView1.Rows[rowIndex].Cells[14].FindControl("TextBox14");
                        TextBox box15 = (TextBox)GridView1.Rows[rowIndex].Cells[15].FindControl("TextBox15");

                        if (ViewState["ModaldropDownData"] != null)
                        {
                            DataTable data = ViewState["ModaldropDownData"] as DataTable;
                            DataTable NewData;
                            NewData = data.Clone();

                            for (int s = 0; s < data.Rows.Count; s++)
                            { 
                                //'C12','CC1','KC1','M02','M04'
                                if (data.Rows[s]["Modelo"].ToString() != "C12" && data.Rows[s]["Modelo"].ToString() != "CC1" && data.Rows[s]["Modelo"].ToString() != "KC1" && data.Rows[s]["Modelo"].ToString() != "M02" && data.Rows[s]["Modelo"].ToString() != "M04") 
                                {
                                    NewData.Rows.Add(data.Rows[s]["Modelo"].ToString());
                                }
                            }

                            ModalDDL.DataSource = NewData;

                            ModalDDL.DataTextField = "Modelo";
                            ModalDDL.DataValueField = "Modelo";
                            ModalDDL.DataBind();
                            ModalDDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select"));
                            ModalDDL.Items.Insert(1, new System.Web.UI.WebControls.ListItem("C12"));
                            ModalDDL.Items.Insert(2, new System.Web.UI.WebControls.ListItem("CC1"));
                            ModalDDL.Items.Insert(3, new System.Web.UI.WebControls.ListItem("KC1"));
                            ModalDDL.Items.Insert(4, new System.Web.UI.WebControls.ListItem("M02"));
                            ModalDDL.Items.Insert(5, new System.Web.UI.WebControls.ListItem("M04"));
                        }

                        box1.Text = dt.Rows[i]["UID"].ToString();
                        box2.Text = dt.Rows[i]["Pie"].ToString();
                        box3.Text = dt.Rows[i]["Multiplier"].ToString();
                        box4.Text = dt.Rows[i]["Qty"].ToString();
                        box5.Text = dt.Rows[i]["Total"].ToString();
                        box6.Text = dt.Rows[i]["Make"].ToString();
                        box7.Text = dt.Rows[i]["Grade"].ToString();
                        box8.Text = dt.Rows[i]["Hardness"].ToString();

                        ModalDDL.SelectedItem.Text = dt.Rows[i]["Model"].ToString();
                        box11.Text = dt.Rows[i]["OuterDiameter"].ToString();
                        box12.Text = dt.Rows[i]["Length"].ToString();
                        box13.Text = dt.Rows[i]["InternalDiameter"].ToString();
                        box14.Text = dt.Rows[i]["Dimen4"].ToString();
                        box15.Text = dt.Rows[i]["Dimen5"].ToString();

                        rowIndex++;
                    }
                }
            }
        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
            GridViewRow row = GridView1.Rows[GridView1.Rows.Count - 1];
            TextBox uIdTextBox = (TextBox)row.FindControl("TextBox1");
            uIdTextBox.Focus();
        }
        protected void OnUidChanged(object sender, EventArgs e)
        {
            TextBox uidTextBox = null;
            try
            {
                uidTextBox = sender as TextBox;
                GridViewRow rows = uidTextBox.NamingContainer as GridViewRow;
                int rowIndex = rows.RowIndex;

                GridViewRow row = GridView1.Rows[rowIndex];
                TextBox pieTextBox = (TextBox)row.FindControl("TextBox2");
                TextBox multiplierTextBox = (TextBox)row.FindControl("TextBox3");
                TextBox qtyTextBox = (TextBox)row.FindControl("TextBox4");
                TextBox totalTextBox = (TextBox)row.FindControl("TextBox5");
                TextBox makeTextBox = (TextBox)row.FindControl("TextBox6");
                TextBox gradeTextBox = (TextBox)row.FindControl("TextBox7");
                TextBox hardnessTextBox = (TextBox)row.FindControl("TextBox8");
                TextBox outerDiameterTextBox = (TextBox)row.FindControl("TextBox11");
                TextBox lengthTextBox = (TextBox)row.FindControl("TextBox12");
                TextBox internalDiameterTextBox = (TextBox)row.FindControl("TextBox13");
                TextBox dimen4TextBox = (TextBox)row.FindControl("TextBox14");
                TextBox dimen5TextBox = (TextBox)row.FindControl("TextBox15");

                articleNumTxtBox.Text = "";
                qtyTextBox.Text = "";
                descriptionTxtBox.Text = "";
                pieTextBox.Text = "";
                multiplierTextBox.Text = "";
                totalTextBox.Text = "";
                gradeTextBox.Text = "";
                hardnessTextBox.Text = "";
                outerDiameterTextBox.Text = "";
                lengthTextBox.Text = "";
                internalDiameterTextBox.Text = "";
                dimen4TextBox.Text = "";
                dimen5TextBox.Text = "";

                DropDownList ModelDDL = (DropDownList)row.FindControl("DropDownList1");

                try
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    OleDbCommand c = new OleDbCommand("Select Modelo from [Modelos de piezas] where Modelo not in('C12','CC1','KC1','M02','M04')", conn);
                    OleDbDataAdapter adp = new OleDbDataAdapter(c);
                    adp.Fill(dt);
                    conn.Close();
                    ViewState["ModaldropDownData"] = dt;
                    ModelDDL.DataSource = dt;
                    ModelDDL.DataTextField = "Modelo";
                    ModelDDL.DataValueField = "Modelo";
                    ModelDDL.DataBind();
                    ModelDDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select"));
                    ModelDDL.Items.Insert(1, new System.Web.UI.WebControls.ListItem("C12"));
                    ModelDDL.Items.Insert(2, new System.Web.UI.WebControls.ListItem("CC1"));
                    ModelDDL.Items.Insert(3, new System.Web.UI.WebControls.ListItem("KC1"));
                    ModelDDL.Items.Insert(4, new System.Web.UI.WebControls.ListItem("M02"));
                    ModelDDL.Items.Insert(5, new System.Web.UI.WebControls.ListItem("M04"));
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
                }

                conn.Open();
                OleDbCommand cmd = new OleDbCommand("Select artord, pieord, nomart from [Ordenes de fabricación] inner join [Artículos de clientes] on [Ordenes de fabricación].artord = [Artículos de clientes].codart where numord= " + uidTextBox.Text, conn);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        articleNumTxtBox.Text = dr["artord"].ToString();
                        qtyTextBox.Text = dr["pieord"].ToString();
                        descriptionTxtBox.Text = dr["nomart"].ToString();
                    }
                }

                conn.Close();

                conn.Open();

                OleDbCommand command = new OleDbCommand("Select top 1 CodPie, ctdpie, calpie, durpie, modpie, diaext, longit, diaint, dimen4, dimen5, dimen6, dimen7, dimen8, dimen9, dimen10, dimen11, dimen12 from [Artículos de clientes (piezas)] where codart= '" + articleNumTxtBox.Text + "' and codpie like 'A%'", conn);
                OleDbDataReader read = command.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        pieTextBox.Text = read["CodPie"].ToString();
                        multiplierTextBox.Text = read["CtdPie"].ToString();
                        totalTextBox.Text = (Convert.ToInt32(multiplierTextBox.Text) * Convert.ToInt32(qtyTextBox.Text)).ToString();
                        int total = Convert.ToInt32(totalTextBox.Text);
                        if (total < 10)
                        {
                            makeTextBox.Text = total.ToString();
                        }
                        else
                        {
                            int quotient = total / 10;
                            total += quotient;
                            makeTextBox.Text = total.ToString();
                        }
                        gradeTextBox.Text = read["CalPie"].ToString();
                        hardnessTextBox.Text = read["DurPie"].ToString();

                        foreach (System.Web.UI.WebControls.ListItem li in ModelDDL.Items)
                        {
                            if (li.Text == read["ModPie"].ToString())
                            {
                                li.Selected = true;
                            }
                        }

                        outerDiameterTextBox.Text = read["DiaExt"].ToString();
                        lengthTextBox.Text = read["Longit"].ToString();
                        internalDiameterTextBox.Text = read["DiaInt"].ToString();
                        dimen4TextBox.Text = read["Dimen4"].ToString();
                        dimen5TextBox.Text = read["Dimen5"].ToString();

                        if (ModelDDL.SelectedItem.Text == "*0")
                        {
                            Image1.ImageUrl = "~/modelImages/0.jpg";
                            Image1.BorderStyle = BorderStyle.Solid;
                            Image1.BorderColor = Color.Black;
                            Image1.BorderWidth = 1;
                            modelName.Text = "Casing Model " + ModelDDL.SelectedItem.Text;
                        }
                        else
                        {
                            Image1.ImageUrl = "~/modelImages/" + ModelDDL.SelectedItem.Text + ".jpg";
                            Image1.BorderStyle = BorderStyle.None;
                            modelName.Text = "Casing Model " + ModelDDL.SelectedItem.Text;
                        }
                    }
                }
                conn.Close();
                pieTextBox.Focus();
                pieTextBox.Attributes.Add("onfocusin", " select();");
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Something went wrong! while fetching all data of uid " + uidTextBox + " in OnUidChanged()...." + ex.Message + "');</script> ");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong! while fetching all data of uid " + uidTextBox + " in OnUidChanged()...." + ex.Message + "')", true);

            }
        }

        protected void onHardnessText_Changed(object sender, EventArgs e)
        {
            try
            {
                TextBox hardenessTxtBox = sender as TextBox;
                GridViewRow row = hardenessTxtBox.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;

                int secondValue=Convert.ToInt32(hardenessTxtBox.Text)+2;

                hardenessTxtBox.Text = hardenessTxtBox.Text + "-" + secondValue + " " + "Hrc.";
                DropDownList ModelDDL = (DropDownList)row.FindControl("DropDownList1");
                ModelDDL.Focus();
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void OnElementChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox elementTextBox = sender as TextBox;
                GridViewRow row = elementTextBox.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;

                TextBox pieTextBox = (TextBox)row.FindControl("TextBox2");
                TextBox multiplierTextBox = (TextBox)row.FindControl("TextBox3");
                TextBox qtyTextBox = (TextBox)row.FindControl("TextBox4");
                TextBox totalTextBox = (TextBox)row.FindControl("TextBox5");
                TextBox makeTextBox = (TextBox)row.FindControl("TextBox6");
                TextBox gradeTextBox = (TextBox)row.FindControl("TextBox7");
                TextBox hardnessTextBox = (TextBox)row.FindControl("TextBox8");
                TextBox markingTextBox = (TextBox)row.FindControl("TextBox9");
                TextBox outerDiameterTextBox = (TextBox)row.FindControl("TextBox11");
                TextBox lengthTextBox = (TextBox)row.FindControl("TextBox12");
                TextBox internalDiameterTextBox = (TextBox)row.FindControl("TextBox13");
                TextBox dimen4TextBox = (TextBox)row.FindControl("TextBox14");
                TextBox dimen5TextBox = (TextBox)row.FindControl("TextBox15");

                DropDownList ModelDDL = (DropDownList)row.FindControl("DropDownList1");

                try
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    OleDbCommand c = new OleDbCommand("Select Modelo from [Modelos de piezas]", conn);
                    OleDbDataAdapter adp = new OleDbDataAdapter(c);
                    adp.Fill(dt);
                    conn.Close();
                    ViewState["ModaldropDownData"] = dt;
                    ModelDDL.DataSource = dt;
                    ModelDDL.DataTextField = "Modelo";
                    ModelDDL.DataValueField = "Modelo";
                    ModelDDL.DataBind();
                    ModelDDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select"));
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);

                }



                OleDbCommand cmd = conn.CreateCommand();
                conn.Open();

                cmd = new OleDbCommand("Select top 1 CodPie, ctdpie, calpie, durpie, modpie, diaext, longit, diaint, dimen4, dimen5, dimen6, dimen7, dimen8, dimen9, dimen10, dimen11, dimen12 from [Artículos de clientes (piezas)] where CodPie ='" + elementTextBox.Text + "' And CodArt = '" + articleNumTxtBox.Text + "'", conn);

                OleDbDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    pieTextBox.Text = read["CodPie"].ToString();
                    multiplierTextBox.Text = read["CtdPie"].ToString();
                    totalTextBox.Text = (Convert.ToInt32(multiplierTextBox.Text) * Convert.ToInt32(qtyTextBox.Text)).ToString();

                    int total = Convert.ToInt32(totalTextBox.Text);
                    if (total < 10)
                    {
                        makeTextBox.Text = total.ToString();
                    }
                    else
                    {
                        int quotient = total / 10;
                        total += quotient;
                        makeTextBox.Text = total.ToString();
                    }

                    gradeTextBox.Text = read["CalPie"].ToString();
                    hardnessTextBox.Text = read["DurPie"].ToString();

                    foreach (System.Web.UI.WebControls.ListItem li in ModelDDL.Items)
                    {
                        if (li.Text == read["ModPie"].ToString())
                        {
                            li.Selected = true;
                        }
                    }

                    outerDiameterTextBox.Text = read["DiaExt"].ToString();
                    lengthTextBox.Text = read["Longit"].ToString();
                    internalDiameterTextBox.Text = read["DiaInt"].ToString();
                    dimen4TextBox.Text = read["Dimen4"].ToString();
                    dimen5TextBox.Text = read["Dimen5"].ToString();
                    if (ModelDDL.SelectedItem.Text == "*0")
                    {
                        Image1.ImageUrl = "~/modelImages/0.jpg";
                        Image1.BorderStyle = BorderStyle.Solid;
                        Image1.BorderColor = Color.Black;
                        Image1.BorderWidth = 1;
                        modelName.Text = "Casing Model " + ModelDDL.SelectedItem.Text;
                    }
                    else
                    {
                        Image1.ImageUrl = "~/modelImages/" + ModelDDL.SelectedItem.Text + ".jpg";
                        Image1.BorderStyle = BorderStyle.None;
                        modelName.Text = "Casing Model " + ModelDDL.SelectedItem.Text;
                    }
                }
                multiplierTextBox.Focus();
                conn.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);

            }
        }


        protected void MultiPlier_change(object sender, EventArgs e)
        {
            TextBox multiplierTextBox = sender as TextBox;
            GridViewRow rows = multiplierTextBox.NamingContainer as GridViewRow;
            int rowIndex = rows.RowIndex;

            GridViewRow row = GridView1.Rows[rowIndex];
            TextBox qtyTextBox = (TextBox)row.FindControl("TextBox4");
            TextBox totalTextBox = (TextBox)row.FindControl("TextBox5");
            TextBox makeTextBox = (TextBox)row.FindControl("TextBox6");

            totalTextBox.Text = (Convert.ToInt32(multiplierTextBox.Text) * Convert.ToInt32(qtyTextBox.Text)).ToString();
            int total = Convert.ToInt32(totalTextBox.Text);
            if (total < 10)
            {
                makeTextBox.Text = total.ToString();
            }
            else
            {
                int quotient = total / 10;
                total += quotient;
                makeTextBox.Text = total.ToString();
            }
            qtyTextBox.Focus();
        }

        protected void Qty_change(object sender, EventArgs e)
        {
            TextBox QtyTextBox = sender as TextBox;
            GridViewRow rows = QtyTextBox.NamingContainer as GridViewRow;
            int rowIndex = rows.RowIndex;

            GridViewRow row = GridView1.Rows[rowIndex];
            TextBox multiplierTextBox = (TextBox)row.FindControl("TextBox3");
            TextBox totalTextBox = (TextBox)row.FindControl("TextBox5");
            TextBox makeTextBox = (TextBox)row.FindControl("TextBox6");

            totalTextBox.Text = (Convert.ToInt32(QtyTextBox.Text) * Convert.ToInt32(multiplierTextBox.Text)).ToString();
            int total = Convert.ToInt32(totalTextBox.Text);
            if (total < 10)
            {
                makeTextBox.Text = total.ToString();
            }
            else
            {
                int quotient = total / 10;
                total += quotient;
                makeTextBox.Text = total.ToString();
            }
        }


        protected void submitButton_Click(object sender, EventArgs e)
        {
            Boolean textFieldsFilled = true;

            if (isAllFieldsAreEmpty() == false)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    TextBox uidTextBox = null;
                    TextBox pieTextBox = null;
                    TextBox multiplierTextBox = null;
                    TextBox qtyTextBox = null;
                    TextBox totalTextBox = null;
                    TextBox makeTextBox = null;
                    TextBox gradeTextBox = null;
                    TextBox hardnessTextBox = null;
                    DropDownList ModalDLL = null;
                    TextBox outerDiameterTextBox = null;
                    TextBox lengthTextBox = null;
                    TextBox internalDiameterTextBox = null;
                    TextBox dimen4TextBox = null;
                    TextBox dimen5TextBox = null;

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        uidTextBox = (TextBox)row.FindControl("TextBox1");
                        pieTextBox = (TextBox)row.FindControl("TextBox2");
                        multiplierTextBox = (TextBox)row.FindControl("TextBox3");
                        qtyTextBox = (TextBox)row.FindControl("TextBox4");
                        totalTextBox = (TextBox)row.FindControl("TextBox5");
                        makeTextBox = (TextBox)row.FindControl("TextBox6");
                        gradeTextBox = (TextBox)row.FindControl("TextBox7");
                        hardnessTextBox = (TextBox)row.FindControl("TextBox8");
                        ModalDLL = (DropDownList)row.FindControl("DropDownList1");
                        outerDiameterTextBox = (TextBox)row.FindControl("TextBox11");
                        lengthTextBox = (TextBox)row.FindControl("TextBox12");
                        internalDiameterTextBox = (TextBox)row.FindControl("TextBox13");
                        dimen4TextBox = (TextBox)row.FindControl("TextBox14");
                        dimen5TextBox = (TextBox)row.FindControl("TextBox15");

                        if (IsPostBack)
                        {
                            if (textFieldsFilled == true)
                            {
                                if (GridView1.Rows.Count != 0)
                                {
                                    if (articleNumTxtBox.Text != "")
                                    {
                                        if (dimen4TextBox.Text == "")
                                        {
                                            dimen4TextBox.Text = "0";
                                        }
                                        if (dimen5TextBox.Text == "")
                                        {
                                            dimen5TextBox.Text = "0";
                                        }

                                        try
                                        {
                                            conn.Open();
                                            OleDbCommand cmd = new OleDbCommand("Select * from [Artículos de clientes (piezas)] where codart='" + articleNumTxtBox.Text + "'and codpie='" + pieTextBox.Text + "'", conn);
                                            OleDbDataReader dr = cmd.ExecuteReader();

                                            if (dr.HasRows && dr.Read())
                                            {
                                                try
                                                {
                                                    OleDbCommand updateCommand = new OleDbCommand("update [Artículos de clientes (piezas)] set calpie='" + gradeTextBox.Text + "', modpie='" + ModalDLL.SelectedItem.Text + "', diaext=" + outerDiameterTextBox.Text + ", longit=" + lengthTextBox.Text + ", diaint=" + internalDiameterTextBox.Text + ", dimen4=" + dimen4TextBox.Text + ", dimen5=" + dimen5TextBox.Text + " where codart='" + articleNumTxtBox.Text + "' and codpie='" + pieTextBox.Text + "'", conn);

                                                    if (updateCommand.ExecuteNonQuery() > 0)
                                                    {
                                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Successfully!...')", true);
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data can't be saved')", true);
                                                    }
                                                }
                                                catch (Exception x)
                                                {
                                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + x.Message + "')", true);
                                                }
                                            }
                                            else
                                            {
                                                OleDbCommand insertCommand = new OleDbCommand("INSERT INTO [Artículos de clientes (piezas)] (codart, codpie, ctdpie, calpie, durpie, modpie, diaext, longit, diaint, dimen4, dimen5) values (@CodArt,@codpie,@ctdpie,@calpie,@durpie,@modpie,@diaext,@longit,@diaint,@dimen4,@dimen5)", conn);

                                                insertCommand.Parameters.AddWithValue("@CodArt", articleNumTxtBox.Text);
                                                insertCommand.Parameters.AddWithValue("@codpie", pieTextBox.Text);
                                                insertCommand.Parameters.AddWithValue("@ctdpie", multiplierTextBox.Text);
                                                insertCommand.Parameters.AddWithValue("@calpie", gradeTextBox.Text);
                                                insertCommand.Parameters.AddWithValue("@durpie", hardnessTextBox.Text);
                                                insertCommand.Parameters.AddWithValue("@modpie", ModalDLL.SelectedItem.Text);
                                                insertCommand.Parameters.AddWithValue("@diaext", outerDiameterTextBox.Text);
                                                insertCommand.Parameters.AddWithValue("@longit", lengthTextBox.Text);
                                                insertCommand.Parameters.AddWithValue("@diaint", internalDiameterTextBox.Text);
                                                insertCommand.Parameters.AddWithValue("@dimen4", dimen4TextBox.Text);
                                                insertCommand.Parameters.AddWithValue("@dimen5", dimen5TextBox.Text);

                                                if (insertCommand.ExecuteNonQuery() > 0)
                                                {
                                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Successfully!...')", true);
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data can't be saved')", true);
                                                }
                                            }
                                            conn.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);

                                        }


                                        try
                                        {
                                            conn.Open();
                                            OleDbCommand comm = new OleDbCommand("update [Ordenes de fabricación] set RefCorte=" + refTxtBox.Text + " where numord = " + uidTextBox.Text, conn);
                                            comm.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                conn.Open();
                OleDbCommand cmd1 = new OleDbCommand("update [Parámetros] set ValPar='" + Convert.ToInt32(refTxtBox.Text) + "' where codpar = 'Última referencia'", conn);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = GridView1.Rows[rowIndex];

                //Fetch value of Name.
                string name = (row.FindControl("TextBox1") as TextBox).Text;

                //Fetch value of Country
                //string country = row.Cells[1].Text;

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + name + "');", true);
            }
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
                        TextBox box1 = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("TextBox4");
                        TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("TextBox5");
                        TextBox box6 = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("TextBox6");
                        TextBox box7 = (TextBox)GridView1.Rows[rowIndex].Cells[7].FindControl("TextBox7");
                        TextBox box8 = (TextBox)GridView1.Rows[rowIndex].Cells[8].FindControl("TextBox8");
                        DropDownList ModalDDL = (DropDownList)GridView1.Rows[rowIndex].Cells[10].FindControl("DropDownList1");
                        TextBox box11 = (TextBox)GridView1.Rows[rowIndex].Cells[11].FindControl("TextBox11");
                        TextBox box12 = (TextBox)GridView1.Rows[rowIndex].Cells[12].FindControl("TextBox12");
                        TextBox box13 = (TextBox)GridView1.Rows[rowIndex].Cells[13].FindControl("TextBox13");
                        TextBox box14 = (TextBox)GridView1.Rows[rowIndex].Cells[14].FindControl("TextBox14");
                        TextBox box15 = (TextBox)GridView1.Rows[rowIndex].Cells[15].FindControl("TextBox15");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["UID"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Pie"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["Multiplier"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["Qty"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Total"] = box5.Text;
                        dtCurrentTable.Rows[i - 1]["Make"] = box6.Text;
                        dtCurrentTable.Rows[i - 1]["Grade"] = box7.Text;
                        dtCurrentTable.Rows[i - 1]["Hardness"] = box8.Text;
                        dtCurrentTable.Rows[i - 1]["Model"] = ModalDDL.SelectedItem.Text;
                        dtCurrentTable.Rows[i - 1]["OuterDiameter"] = box11.Text;
                        dtCurrentTable.Rows[i - 1]["Length"] = box12.Text;
                        dtCurrentTable.Rows[i - 1]["InternalDiameter"] = box13.Text;
                        dtCurrentTable.Rows[i - 1]["Dimen4"] = box14.Text;
                        dtCurrentTable.Rows[i - 1]["Dimen5"] = box15.Text;
                        rowIndex++;
                    }

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
        protected void printBtn_Click(object sender, EventArgs e)
        {
            if (isAllFieldsAreEmpty() == false)
            {
                DataTable data = new DataTable();

                data.Columns.Add(new DataColumn("UID", typeof(string)));
                data.Columns.Add(new DataColumn("Pie", typeof(string)));
                data.Columns.Add(new DataColumn("Multiplier", typeof(string)));
                data.Columns.Add(new DataColumn("Qty", typeof(string)));
                data.Columns.Add(new DataColumn("Total", typeof(string)));
                data.Columns.Add(new DataColumn("Make", typeof(string)));
                data.Columns.Add(new DataColumn("Grade", typeof(string)));
                data.Columns.Add(new DataColumn("Hardness", typeof(string)));
                data.Columns.Add(new DataColumn("Model", typeof(string)));
                data.Columns.Add(new DataColumn("OuterDiameter", typeof(string)));
                data.Columns.Add(new DataColumn("Length", typeof(string)));
                data.Columns.Add(new DataColumn("InternalDiaMeter", typeof(string)));
                data.Columns.Add(new DataColumn("Dimen4", typeof(string)));
                data.Columns.Add(new DataColumn("Dimen5", typeof(string)));
                data.Columns.Add(new DataColumn("CustDrawingNumber", typeof(string)));
                data.Columns.Add(new DataColumn("Reference", typeof(string)));
                data.Columns.Add(new DataColumn("UidRemarks", typeof(string)));
                data.Columns.Add(new DataColumn("WeekNoOfDelDate", typeof(string)));
                data.Columns.Add(new DataColumn("CasingDate", typeof(string)));

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox uidTextBox = (TextBox)row.FindControl("TextBox1");
                        TextBox pieTextBox = (TextBox)row.FindControl("TextBox2");
                        TextBox multiplierTextBox = (TextBox)row.FindControl("TextBox3");
                        TextBox qtyTextBox = (TextBox)row.FindControl("TextBox4");
                        TextBox totalTextBox = (TextBox)row.FindControl("TextBox5");
                        TextBox makeTextBox = (TextBox)row.FindControl("TextBox6");
                        TextBox gradeTextBox = (TextBox)row.FindControl("TextBox7");
                        TextBox hardnessTextBox = (TextBox)row.FindControl("TextBox8");
                        DropDownList ModelDLL = (DropDownList)row.FindControl("DropDownList1");
                        TextBox outerDiameterTextBox = (TextBox)row.FindControl("TextBox11");
                        TextBox lengthTextBox = (TextBox)row.FindControl("TextBox12");
                        TextBox internalDiameterTextBox = (TextBox)row.FindControl("TextBox13");
                        TextBox dimen4TextBox = (TextBox)row.FindControl("TextBox14");
                        TextBox dimen5TextBox = (TextBox)row.FindControl("TextBox15");


                        String fechaDateValue = "";

                        OleDbCommand com = new OleDbCommand("SELECT Fecha2 FROM [Mecanizados (altas) guardar PML] where NumOrd2=" + uidTextBox.Text, conn);
                        conn.Open();
                        OleDbDataReader reder = com.ExecuteReader();

                        if (reder.Read())
                        {
                            if (reder["Fecha2"].ToString() != "")
                            {
                                string date = reder["Fecha2"].ToString();
                                fechaDateValue = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");  //----------- This is Casing Section column data-------------------
                            }
                        }
                        conn.Close();

                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("Select Top 1 PlaOrd, RefCorte, Datos, EntOrd from [Ordenes de fabricación] Where NumOrd= " + uidTextBox.Text, conn);
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                data.Rows.Add(uidTextBox.Text, pieTextBox.Text, multiplierTextBox.Text, qtyTextBox.Text, totalTextBox.Text, makeTextBox.Text, gradeTextBox.Text, hardnessTextBox.Text, ModelDLL.SelectedItem.Text, outerDiameterTextBox.Text, lengthTextBox.Text, internalDiameterTextBox.Text, dimen4TextBox.Text, dimen5TextBox.Text, dr["PlaOrd"].ToString(), dr["RefCorte"].ToString(), dr["Datos"].ToString(), dr["EntOrd"].ToString(), fechaDateValue);
                            }
                        }
                        conn.Close();
                    }
                }
                Session["PrintDrawingData"] = data;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Newtab", "window.open ('CasingDrawingPrint.aspx','_blank')", true);
            }
        }
        public Boolean isAllFieldsAreEmpty()
        {
            Boolean isTextBoxNull = false;
            Boolean textFieldsFilled = true;
            foreach (GridViewRow row in GridView1.Rows)
            {                
                DropDownList ModelDLL = null;

                if (row.RowType == DataControlRowType.DataRow)
                {
                    ModelDLL = (DropDownList)row.FindControl("DropDownList1");
                    
                    if (ModelDLL.SelectedItem.Text == "Please select")
                    {
                        isTextBoxNull = true;
                        ModelDLL.BackColor = System.Drawing.Color.Red;
                        ModelDLL.Focus();
                        textFieldsFilled = false;
                        //Response.Write("<script>alert('Please Select Model Number...!');</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Model Number...!')", true);
                        break;
                    }
                }
                if (textFieldsFilled != false)
                {
                    textFieldsFilled = true;
                }
            }
            if (isTextBoxNull == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ModelDDL = sender as DropDownList;
            GridViewRow rows = ModelDDL.NamingContainer as GridViewRow;
            int rowIndex = rows.RowIndex;
            GridViewRow row = GridView1.Rows[rowIndex];
            if (ModelDDL.SelectedItem.Text == "*0")
            {
                Image1.ImageUrl = "~/modelImages/0.jpg";
                Image1.BorderStyle = BorderStyle.Solid;
                Image1.BorderColor = Color.Black;
                Image1.BorderWidth = 1;
                modelName.Text = "Casing Model " + ModelDDL.SelectedItem.Text;
            }
            else
            {
                Image1.ImageUrl = "~/modelImages/" + ModelDDL.SelectedItem.Text + ".jpg";
                Image1.BorderStyle = BorderStyle.None;
                modelName.Text = "Casing Model " + ModelDDL.SelectedItem.Text;
            }
        }

        protected void modelPreviewLink_Click(object sender, EventArgs e)
        {
            LinkButton modelPreviewLink = sender as LinkButton;
            GridViewRow rows = modelPreviewLink.NamingContainer as GridViewRow;
            int rowIndex = rows.RowIndex;
            GridViewRow row = GridView1.Rows[rowIndex];
            DropDownList ModelDDL = (DropDownList)row.FindControl("DropDownList1");
            if (ModelDDL.SelectedItem.Text == "*0")
            {
                Image1.ImageUrl = "~/modelImages/0.jpg";
                Image1.BorderStyle = BorderStyle.Solid;
                Image1.BorderColor = Color.Black;
                Image1.BorderWidth = 1;
                modelName.Text = "Casing Model " + ModelDDL.SelectedItem.Text;
            }
            else
            {
                Image1.ImageUrl = "~/modelImages/" + ModelDDL.SelectedItem.Text + ".jpg";
                Image1.BorderStyle = BorderStyle.None;
                modelName.Text = "Casing Model " + ModelDDL.SelectedItem.Text;
            }
        }

        protected void printSteelIndentBtn_Click(object sender, EventArgs e)
        {
            if (isAllFieldsAreEmpty() == false)
            {
                DataTable data = new DataTable();

                data.Columns.Add(new DataColumn("uid", typeof(string)));
                data.Columns.Add(new DataColumn("element", typeof(string)));
                data.Columns.Add(new DataColumn("article", typeof(string)));
                data.Columns.Add(new DataColumn("Qty", typeof(string)));
                data.Columns.Add(new DataColumn("dimension", typeof(string)));
                data.Columns.Add(new DataColumn("grade", typeof(string)));
                data.Columns.Add(new DataColumn("hardness", typeof(string)));
                data.Columns.Add(new DataColumn("weigth", typeof(string)));
                data.Columns.Add(new DataColumn("deliveryDate", typeof(string)));
                data.Columns.Add(new DataColumn("datos", typeof(string)));
                data.Columns.Add(new DataColumn("reference", typeof(string)));
                data.Columns.Add(new DataColumn("fetchaDate", typeof(string)));

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox uidTextBox = (TextBox)row.FindControl("TextBox1");
                        TextBox pieTextBox = (TextBox)row.FindControl("TextBox2");
                        TextBox qtyTextBox = (TextBox)row.FindControl("TextBox4");
                        TextBox gradeTextBox = (TextBox)row.FindControl("TextBox7");
                        TextBox hardnessTextBox = (TextBox)row.FindControl("TextBox8");
                        TextBox outerDiameterTextBox = (TextBox)row.FindControl("TextBox11");
                        TextBox lengthTextBox = (TextBox)row.FindControl("TextBox12");
                        TextBox internalDiameterTextBox = (TextBox)row.FindControl("TextBox13");

                        decimal Diameter = Convert.ToDecimal(outerDiameterTextBox.Text) + 5;
                        decimal Length = Convert.ToDecimal(lengthTextBox.Text) + 5;

                        String fechaDateValue = "";

                        OleDbCommand com = new OleDbCommand("SELECT Fecha2 FROM [Mecanizados (altas) guardar PML] where NumOrd2=" + uidTextBox.Text, conn);
                        conn.Open();
                        OleDbDataReader reder = com.ExecuteReader();

                        if (reder.Read())
                        {
                            if (reder["Fecha2"].ToString() != "")
                            {
                                string date = reder["Fecha2"].ToString();
                                fechaDateValue = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");  //----------- This is Casing Section column data-------------------
                            }
                        }
                        conn.Close();

                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("Select Top 1 ArtOrd, PlaOrd, RefCorte, Datos, EntOrd from [Ordenes de fabricación] Where NumOrd= " + uidTextBox.Text, conn);
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                data.Rows.Add(uidTextBox.Text, pieTextBox.Text, dr["ArtOrd"].ToString(), qtyTextBox.Text, Diameter + " X " + Length, gradeTextBox.Text, hardnessTextBox.Text, "", Convert.ToDateTime(dr["EntOrd"]).ToString("dd-MMM-yyyy"), dr["Datos"].ToString());
                            }
                        }
                        conn.Close();
                    }
                }
                Session["fetchaDate"] = DateTime.Today.ToString("dd-MMM-yyyy");
                Session["reference"] = refTxtBox.Text;
                Session["PrintSteelIndentData"] = data;
                //Response.Redirect("~/PrintSteelIndentPdf.aspx");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Newtab", "window.open ('PrintSteelIndentPdf.aspx','_blank')", true);
            }
        }

        protected void prinBandsaJobCardBtn_Click(object sender, EventArgs e)
        {
            if (isAllFieldsAreEmpty() == false)
            {
                DataTable data = new DataTable();

                data.Columns.Add(new DataColumn("uid", typeof(string)));
                data.Columns.Add(new DataColumn("element", typeof(string)));
                data.Columns.Add(new DataColumn("article", typeof(string)));
                data.Columns.Add(new DataColumn("Qty", typeof(string)));
                data.Columns.Add(new DataColumn("dimension", typeof(string)));
                data.Columns.Add(new DataColumn("grade", typeof(string)));
                data.Columns.Add(new DataColumn("hardness", typeof(string)));
                data.Columns.Add(new DataColumn("weigth", typeof(string)));
                data.Columns.Add(new DataColumn("deliveryDate", typeof(string)));
                data.Columns.Add(new DataColumn("datos", typeof(string)));

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox uidTextBox = (TextBox)row.FindControl("TextBox1");
                        TextBox pieTextBox = (TextBox)row.FindControl("TextBox2");
                        TextBox qtyTextBox = (TextBox)row.FindControl("TextBox4");
                        TextBox gradeTextBox = (TextBox)row.FindControl("TextBox7");
                        TextBox hardnessTextBox = (TextBox)row.FindControl("TextBox8");
                        TextBox outerDiameterTextBox = (TextBox)row.FindControl("TextBox11");
                        TextBox lengthTextBox = (TextBox)row.FindControl("TextBox12");
                        TextBox internalDiameterTextBox = (TextBox)row.FindControl("TextBox13");

                        decimal Diameter = Convert.ToDecimal(outerDiameterTextBox.Text) + 5;
                        decimal Length = Convert.ToDecimal(lengthTextBox.Text) + 5;

                        String fechaDateValue = "";

                        OleDbCommand com = new OleDbCommand("SELECT Fecha2 FROM [Mecanizados (altas) guardar PML] where NumOrd2=" + uidTextBox.Text, conn);
                        conn.Open();
                        OleDbDataReader reder = com.ExecuteReader();

                        if (reder.Read())
                        {
                            if (reder["Fecha2"].ToString() != "")
                            {
                                string date = reder["Fecha2"].ToString();
                                fechaDateValue = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");  //----------- This is Casing Section column data-------------------
                            }
                        }
                        conn.Close();

                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("Select Top 1 ArtOrd, PlaOrd, RefCorte, Datos, EntOrd from [Ordenes de fabricación] Where NumOrd= " + uidTextBox.Text, conn);
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                data.Rows.Add(uidTextBox.Text, pieTextBox.Text, dr["ArtOrd"].ToString(), qtyTextBox.Text, Diameter + " x " + Length, gradeTextBox.Text, hardnessTextBox.Text, "", Convert.ToDateTime(dr["EntOrd"]).ToString("dd-MMM-yyyy"), dr["Datos"].ToString());
                            }
                        }
                        conn.Close();
                    }
                }

                try
                {
                    DataTable groupedTable = new DataTable();
                    groupedTable = data.Clone();
                    groupedTable.Columns.Add("longitudTotal");

                    DataTable gradeGroupTable = new DataTable();
                    gradeGroupTable = groupedTable.Clone();


                    HashSet<string> diameterList = new HashSet<string>();
                    HashSet<string> gradeList = new HashSet<string>();

                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        diameterList.Add(data.Rows[i]["dimension"].ToString().Split(' ').GetValue(0).ToString());
                        gradeList.Add(data.Rows[i]["grade"].ToString().Split(' ').GetValue(0).ToString());
                    }

                    decimal grandTotalLongitud = 0;
                    foreach (var gradeValue in gradeList)
                    {
                        groupedTable.Rows.Add("Grade ", gradeValue, "", "", "", "", "", "", "", "", "");
                        groupedTable.Rows.Add("", "", "", "", "", "", "", "", "", "", "");
                        foreach (var diaValue in diameterList)
                        {
                            for (int j = 0; j < data.Rows.Count; j++)
                            {
                                if (diaValue == data.Rows[j]["dimension"].ToString().Split(' ').GetValue(0).ToString() && gradeValue == data.Rows[j]["grade"].ToString())
                                {
                                    decimal totalLongitud = Convert.ToInt32(data.Rows[j]["Qty"].ToString()) * Convert.ToDecimal(data.Rows[j]["dimension"].ToString().Split(' ').GetValue(2).ToString());

                                    groupedTable.Rows.Add(data.Rows[j]["uid"].ToString(), data.Rows[j]["element"].ToString(), data.Rows[j]["article"].ToString(), data.Rows[j]["Qty"].ToString(), data.Rows[j]["dimension"].ToString(), data.Rows[j]["grade"].ToString(), data.Rows[j]["hardness"].ToString(), "", data.Rows[j]["deliveryDate"].ToString(), data.Rows[j]["datos"].ToString(), totalLongitud);

                                    grandTotalLongitud += totalLongitud;
                                }
                            }
                            if (grandTotalLongitud != 0)
                            {
                                groupedTable.Rows.Add("", "", "", "", "", "", "", "Grand Weight", "", "", "Tot. len.: " + grandTotalLongitud);
                                groupedTable.Rows.Add("", "", "", "", "", "", "", "", "", "", "");
                                grandTotalLongitud = 0;
                            }
                        }
                    }
                    Session["fetchaDate"] = DateTime.Today.ToString("dd-MMM-yyyy");
                    Session["reference"] = refTxtBox.Text;
                    Session["PrintBandSawData"] = groupedTable;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
                }
                //Response.Redirect("~/PrintBandSawPdf.aspx");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Newtab", "window.open ('PrintBandSawPdf.aspx','_blank')", true);

            }
        }
    }
}