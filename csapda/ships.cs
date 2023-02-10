using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace trap
{
    class ships
    {
        Color color;
        int iDiameter;
        int formWidth;
        int formHeight;
        SolidBrush shipBrush;
        List<Rectangle> recListShips;
        static Random rm = new Random();
        bool bHasCollided = false;

        public ships(Color color2, int iDiameter2, int formWidth2, int formHeight2)
        {
            color = color2;
            iDiameter = iDiameter2;
            formWidth = formWidth2;
            formHeight = formHeight2;
            recListShips = new List<Rectangle>();
            shipBrush = new SolidBrush(color);

        }

        public void NewShip()
        {
            int iX, iY;
            iX = rm.Next(0, formWidth-iDiameter + 1);
            iY = rm.Next(0, formHeight / 10 * 7 + 1);
            recListShips.Add(new Rectangle(iX, iY, iDiameter, iDiameter));

        }

        public void Draw(Graphics domain)
        {
            foreach(Rectangle currentRec in recListShips)
            {
                domain.FillEllipse(shipBrush, currentRec);
            }

        }

        public bool HasCollided(Rectangle recHit)
        {
            bHasCollided = false;
            foreach(Rectangle currentRec in recListShips)
            {
                if (currentRec.IntersectsWith(recHit))
                {
                    bHasCollided = true;
                }

            }
            return bHasCollided;
        }
    }
}
