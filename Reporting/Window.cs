using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ProtoBuf;
using ZedGraph;

namespace Reporting
{
    public partial class Window : Form
    {
        private Dictionary<XDate, int> points = new Dictionary<XDate, int>();

        public Window()
        {
            Serializer.Deserialize<>()







            InitializeComponent();

            int value = 10;

            for (int i = 4; i <= 24; i++)
            {
                points[new XDate(2000, 0, 0, i, 15, 30)] = Convert.ToInt32(Math.Sin(i * 2 * Math.PI / 60.0) * 100);
                points[new XDate(2000, 0, 0, i, 45, 30)] = Convert.ToInt32(Math.Sin((i * 2 + 1) * Math.PI / 60.0) * 100);
            }
        }

        private void Window_Load(object sender, EventArgs e)
        {
            GraphPane myPane = graph.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "Time Blocks";
            myPane.XAxis.Title.Text = "Time of Day";
            myPane.YAxis.Title.Text = "Travel Time";

            myPane.XAxis.Type = AxisType.Date;
            myPane.XAxis.Scale.Min = new XDate(2000, 0, 0, 4, 0, 0);
            myPane.XAxis.Scale.MajorStep = 30;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Minute;
            myPane.XAxis.Scale.Max = new XDate(2000, 0, 0, 24, 0, 0);
            myPane.XAxis.Scale.Format = "%h";

            // Make up some data points from the Sine function
            PointPairList list = new PointPairList();
//            for (double x = 4; x < 24; x++)
//            {
//                double y = Math.Sin(x * Math.PI / 45.0);
//
//                list.Add(new XDate(2000, 0, 0, Convert.ToInt32(x), 15, 0), y);
//            }
            foreach (XDate date in points.Keys)
            {
                list.Add(date, points[date]);
            }

            PointPairList list2 = new PointPairList();
            for (double x = 4; x < 24; x++)
            {
                double y = Math.Sin(x * Math.PI / 30.0);

                list2.Add(new XDate(2000, 0, 0, Convert.ToInt32(x), 0, 0), y);
            }

            LineItem myCurve = myPane.AddCurve("Average", list, Color.Blue,
                                    SymbolType.None);
//            LineItem myCurve2 = myPane.AddCurve("Average", list, Color.Blue,
//                                    SymbolType.None);
            myCurve.Line.Fill = new Fill(Color.FromArgb(100, Color.DodgerBlue));
//            myCurve2.Line.Fill = new Fill(Color.FromArgb(100, Color.DimGray));
//
            myCurve.Points = list;
//            myCurve2.Points = list2;

            BarItem item = myPane.AddBar(null, list, Color.DodgerBlue);
            item.Bar.Fill = new Fill(Color.FromArgb(100, Color.DodgerBlue));
            item.Bar.Border.Width = 0;
            myPane.BarSettings.MinBarGap = 0;
            myPane.BarSettings.MinClusterGap = 0;
            myPane.BarSettings.MinClusterGap = 0.55f;

            graph.AxisChange();
        }








        // Fill the area under the curve with a white-red gradient at 45 degrees
        //myCurve.Line.Fill = new Fill( Color.White, Color.Red, 45F );
        // Make the symbols opaque by filling them with white
        //myCurve.Symbol.Fill = new Fill( Color.White );

        // Fill the axis background with a color gradient
        //myPane.Chart.Fill = new Fill( Color.White, Color.LightGoldenrodYellow, 45F );

        // Fill the pane background with a color gradient
        //myPane.Fill = new Fill( Color.White, Color.FromArgb( 220, 220, 255 ), 45F );

        // Calculate the Axis Scale Ranges
    }
}
