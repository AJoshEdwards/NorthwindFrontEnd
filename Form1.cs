using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using LiveCharts.Wpf;
using LiveCharts;

namespace Cost_Capture_Template
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //queries
        string sqlquery = @"SELECT  o.[OrderID], o.[CustomerID], c.CompanyName, c.Country[Company Country], o.[EmployeeID], e.FirstName + ' ' + 
         e.LastName[Employee Name],[OrderDate],[RequiredDate],[ShippedDate],[ShipVia], s.CompanyName[Ship Name], sum(od.Quantity * od.UnitPrice)[OrderPrice]  
         FROM[Northwind].[dbo].[Orders] o left join Northwind.dbo.Employees e on e.EmployeeID = o.EmployeeID left join Customers c on c.CustomerID = o.CustomerID 
         left join Shippers s on s.ShipperID = o.ShipVia left join[Order Details] od on od.OrderID = o.OrderID  where c.country like @Country and s.companyname like @company
         Group by  o.[OrderID], o.[CustomerID], c.CompanyName, c.Country, o.[EmployeeID], e.FirstName + ' ' + e.LastName,[OrderDate],[RequiredDate],[ShippedDate],[ShipVia], s.CompanyName";

        string sqlquery2 = @"SELECT distinct c.Country[Company Country] FROM [Northwind].[dbo].[Orders] o left join Northwind.dbo.Employees e on e.EmployeeID = o.EmployeeID
        left join Customers c on c.CustomerID = o.CustomerID left join Shippers s on s.ShipperID = o.ShipVia left join [Order Details] od on od.OrderID = o.OrderID";

        string sqlquery3 = @"SELECT distinct  CompanyName[Ship Name] from Shippers";
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlquery2, cn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataSet ds1 = new DataSet();
                            adapter.Fill(ds1);
                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            {
                                cboCountry.Items.Add(ds1.Tables[0].Rows[i][0].ToString());
                            }
                        }
                        using (SqlCommand cmd = new SqlCommand(sqlquery3, cn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataSet ds2 = new DataSet();
                            adapter.Fill(ds2);
                            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                            {
                                cboShipName.Items.Add(ds2.Tables[0].Rows[i][0].ToString());
                            }
                        }
                    }
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void querydatabase(object sender, EventArgs e)
        {
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
                    {
                        if (cn.State == ConnectionState.Closed)
                            cn.Open();
                        using (DataTable dt = new DataTable("Company/Country"))
                        {
                            using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                            {
                                if (cboCountry.SelectedItem == null)
                                {
                                    cmd.Parameters.AddWithValue("Country", "%");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("Country", cboCountry.SelectedItem);
                                }
                                if (cboShipName.SelectedItem == null)
                                {
                                    cmd.Parameters.AddWithValue("Company", "%");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("Company", cboShipName.SelectedItem);
                                }
                                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                adapter.Fill(dt);
                                dataGridView.DataSource = dt;
                                //totalrows calculated
                                lblTotalRows.Text = $"Total records: {dataGridView.RowCount}";
                                //sum of orderprice calculated
                                int sum = 0;
                                for (int i = 0; i < dataGridView.Rows.Count; ++i)
                                {
                                    sum += Convert.ToInt32(dataGridView.Rows[i].Cells[11].Value);
                                }
                                //average of orderprice calculated and shown
                                int count_row = dataGridView.RowCount;
                                Double avg = sum / count_row;
                                lblorder.Text = $"Average Order Price: £{avg.ToString()} ";


                                //using (NorthwindEntities db = new NorthwindEntities())
                                //{


                                //    var data = db.mainquery();
                                //    ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<int>(), LabelPoint = Point => Point.Y.ToString() };
                                //    Axis ax = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };
                                //    ax.Labels = new List<string>();
                                //    foreach (var x in data)
                                //    {
                                //        col.Values.Add(x.Order_Price.Value);
                                //        ax.Labels.Add(x.OrderID.ToString());
                                //    }
                                //    cartesianChart1.Series.Add(col);
                                //    cartesianChart1.AxisX.Add(ax);
                                //    cartesianChart1.AxisY.Add(new Axis
                                //    {
                                //        LabelFormatter = value => value.ToString(),
                                //        Separator = new Separator()
                                //    });
                                //}


                                //cartesianchart1 link
                                //ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<int>(), LabelPoint = Point => Point.Y.ToString() };
                                //Axis ax = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };
                                //ax.Labels = new List<string>();
                                //foreach (DataRow row in dt.Rows.Cast<DataRow>())
                                //{
                                //    col.Values.Add((string)row["OrderPrice"]);
                                //    ax.Labels.Add(row["OrderID"].ToString());
                                //}
                                //cartesianChart1.Series.Add(col);
                                //cartesianChart1.AxisX.Add(ax);
                                //cartesianChart1.AxisY.Add(new Axis
                                //{
                                //    LabelFormatter = value => value.ToString(),
                                //    Separator = new Separator()
                                //});



                                //chart1 link
                                chart1.Series["Order Cost"].YValueMembers = "OrderPrice";
                                chart1.ChartAreas[0].AxisX.Title = "Amount of Orders";
                                chart1.ChartAreas[0].AxisY.LabelStyle.Format = "£{0:0}";
                                chart1.ChartAreas[0].AxisY.Title = "Order Price";
                                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                                chart1.DataSource = dt;
                                chart1.DataBind();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
    
}
