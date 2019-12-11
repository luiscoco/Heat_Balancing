using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class FlashTankElement : RectangleElement, IControllable
    {
        protected Image image;
        protected Int32 tipoequipo1 = 21;
        protected Image imagen1 = null;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private FlashTankController controller;

        public FlashTankElement() : base() { }

        public FlashTankElement(Rectangle rec, Int32 tipoequipo, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoequipo, imagen2, NumEquipo)
        { }

        public FlashTankElement(Point l, Size s, Int32 tipoequipo, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoequipo, imagen2, NumEquipo)
        { }

        public FlashTankElement(int top, int left, int width, int height, Int32 tipoequipo, Image imagen2, Double NumEquipo)
        {
            NumEquipo1 = NumEquipo;
            tipoequipo1 = tipoequipo;
            imagen1 = imagen2;
            location = new Point(top, left);
            size = new Size(width, height);
        }

        public FlashTankElement(Rectangle rec) : base(rec) { }

        public FlashTankElement(Point l, Size s) : base(l, s) { }

        public FlashTankElement(int top, int left, int width, int height) : base(top, left, width, height) { }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;

            Rectangle r = GetUnsignedRectangle();

            if (imagen1 != null)

                g.DrawImage(imagen1, r);

            //draw Text 
            using (Font font1 = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                PointF pointF1 = new PointF(r.X + (r.Width / 2) - 7, r.Y + (r.Height / 2) - 7);
                g.DrawString(Convert.ToString(NumEquipo1), font1, Brushes.Black, pointF1);
            }
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new FlashTankController(this);
            return controller;
        }
    }
}
