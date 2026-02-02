using Inventor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingCADventureCSharp
{
    public class DrawingUtilities
    {

        public static Point2d DrawingPointOnSheet()
        {

            Point2d getPoint = ClosestGetPoint.GetDrawingPoint("Click on point on sheet", MouseButtonEnum.kLeftMouseButton);

            //Debug.Print(getPoint.X.ToString() + ", " + getPoint.Y.ToString());

            MessageBox.Show(getPoint.X.ToString() + ", " + getPoint.Y.ToString());

            return getPoint;

        }




    }
}
