using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevEditor
{
    // Change to an interface; same reason as window.
    class Button
    {
        private int id;
        private int columnID;
        private int rowID;
        private Rectangle rect;

        public int ID
        {
            get { return id; }
        }

        public int ColumnID
        {
            get { return columnID; }
        }

        public int RowID
        {
            get { return rowID; }
        }

        public Rectangle Rect
        {
            get { return rect; }
        }

        public Button(Rectangle rectangle, int ID)
        {
            this.rect = rectangle;
            this.id = ID;
            rowID = -1;
            columnID = -1;
        }

        public Button(Rectangle rectangle, int ID, int rowID, int columnID)
        {
            this.rect = rectangle;
            this.id = ID;
            this.rowID = rowID;
            this.columnID = columnID;
        }

        public void ChangeYPosition(int delta)
        {
            rect.Location = new Point(rect.X, rect.Y + delta);
        }

        public void ChangeXPosition(int delta)
        {
            rect.Location = new Point(rect.X + delta, rect.Y);
        }
    }
}
