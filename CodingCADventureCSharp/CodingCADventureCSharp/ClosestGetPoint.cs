using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCADventureCSharp
{
    public class ClosestGetPoint
    {

        private static InteractionEvents _interaction;
        private static MouseEvents _mouse;
        private static Point2d _position;
        private static bool _continue;
        private static MouseButtonEnum _button;

        public static Point2d GetDrawingPoint(string prompt, MouseButtonEnum button)
        {
            _position = null;
            _button = button;
            _continue = true;

            _interaction = Globals.InvApp.CommandManager.CreateInteractionEvents();
            _mouse = _interaction.MouseEvents;

            

            _mouse.OnMouseClick += Mouse_OnMouseClick;

            _interaction.OnTerminate += Interaction_OnTerminate;

            Globals.InvApp.StatusBarText = prompt;
            _interaction.StatusBarText = prompt;
            _interaction.Start();

            Globals.InvApp.UserInterfaceManager.DoEvents();
            Globals.InvApp.StatusBarText = prompt;


            while (_continue)
            {
                Globals.InvApp.UserInterfaceManager.DoEvents();
            }

            try { _interaction.Stop(); } catch { }

            _mouse.OnMouseClick -= Mouse_OnMouseClick;
            _interaction.OnTerminate -= Interaction_OnTerminate;

            return _position;


        }


        private static void Mouse_OnMouseClick(MouseButtonEnum Button, ShiftStateEnum ShiftKeys, Point modelPosition, Point2d ViewPosition, View View)
        {
            if (Button == _button)
            {
                _position = Globals.InvApp.TransientGeometry.CreatePoint2d(modelPosition.X, modelPosition.Y);


            }
            _continue = false;
        }

        private static void Interaction_OnTerminate()
        {
            _continue = false;
        }

        public void CleanUp()
        {
            _mouse = null;
            _interaction = null;
        }


    }
}
