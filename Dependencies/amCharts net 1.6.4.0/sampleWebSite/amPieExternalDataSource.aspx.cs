using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using am.Charts;

public partial class amPieExternalDataSource : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // We will generate random values everyt time this page is requested
        Random rnd = new Random();

        PieChart pc = new PieChart();
        pc.Items.Add(new PieChartDataItem("Men", rnd.Next(30, 70)));
        pc.Items.Add(new PieChartDataItem("Women", rnd.Next(30, 70)));

        Response.Write(pc.GetDataXml().OuterXml);
    }
}
