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

            return ClosestGetPoint.GetDrawingPoint("Click on point on sheet", MouseButtonEnum.kLeftMouseButton);

        }

        public static Point2d DrawingPointOnSheet(bool prompt)
        {

            Point2d getPoint = ClosestGetPoint.GetDrawingPoint("Click on point on sheet", MouseButtonEnum.kLeftMouseButton);

            if (prompt)
            {
                MessageBox.Show(getPoint.X.ToString() + ", " + getPoint.Y.ToString());
            }

            Debug.Print(getPoint.X.ToString() + ", " + getPoint.Y.ToString());


            return getPoint;

        }



        public static void AddAngleDim()
        {

            DrawingCurveSegment curveSeg1;
            DrawingCurveSegment curveSeg2;


            

            if (Globals.InvApp.ActiveDocument is DrawingDocument)
            {
                DrawingDocument activeDwg= Globals.InvApp.ActiveDocument as DrawingDocument;
                Sheet activeSheet = activeDwg.ActiveSheet;
                Point2d textPosition = null;


                try
                {
                    curveSeg1 = (DrawingCurveSegment)Globals.InvApp.CommandManager.Pick(SelectionFilterEnum.kDrawingCurveSegmentFilter, "Pick a drawing curve for the angle dimension...");

                }
                catch (Exception)
                {

                    throw;
                }

                try
                {
                    curveSeg2 = (DrawingCurveSegment)Globals.InvApp.CommandManager.Pick(SelectionFilterEnum.kDrawingCurveSegmentFilter, "Pick another drawing curve for the angle dimension...");

                }
                catch (Exception)
                {

                    throw;
                }


                //If both curves are valid then continue
                if (curveSeg1 != null && curveSeg2 != null)
                {


                    double textX = 20.2582;
                    double textY = 35.867;

                    DrawingView activeView = curveSeg1.Parent.Parent;

                    GeometryIntent intent1 = activeSheet.CreateGeometryIntent(curveSeg1.Parent, PointIntentEnum.kStartPointIntent);
                    GeometryIntent intent2 = activeSheet.CreateGeometryIntent(curveSeg2.Parent, PointIntentEnum.kStartPointIntent);

                    double leftViewX = activeView.Center.X - (activeView.Width / 2);
                    double topViewY = activeView.Center.Y - (activeView.Height / 2);


                    double textPositionX = leftViewX - (leftViewX - textX);
                    double textPositionY = topViewY - (topViewY - textY);


                    textPosition = Globals.InvApp.TransientGeometry.CreatePoint2d(textPositionX, textPositionY);

                    try
                    {
                        AngularGeneralDimension angleDim = activeSheet.DrawingDimensions.GeneralDimensions.AddAngular(textPosition, intent1, intent2);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    
                }
            }
            

           


        }




    }
}
