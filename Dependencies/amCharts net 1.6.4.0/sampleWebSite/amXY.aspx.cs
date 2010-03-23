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
using System.Collections.Generic;
using System.Drawing;

public partial class amXY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<PointF> parabola = new List<PointF>();

        for(float f1=-20; f1<20; f1+=0.1F)
            parabola.Add(new PointF(f1, (float)Math.Pow(f1, 2)));

        XyChart1.Graphs[0].DataSource = parabola;
        XyChart1.Graphs[0].DataXField = "X";
        XyChart1.Graphs[0].DataYField = "Y";

        List<PointF> hyperbola = new List<PointF>();

        for (float f2 = 0.1F; f2 < 20; f2 += 0.1F)
            hyperbola.Add(new PointF(f2, ((float)40)/f2));

        XyChart1.Graphs[1].DataSource = hyperbola;
        XyChart1.Graphs[1].DataXField = "X";
        XyChart1.Graphs[1].DataYField = "Y";


        XyChart1.DataBind();


    }
}
