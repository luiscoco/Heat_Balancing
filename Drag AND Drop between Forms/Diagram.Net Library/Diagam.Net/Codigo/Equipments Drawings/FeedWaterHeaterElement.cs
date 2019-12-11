using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class FeedWaterHeaterElement : RectangleElement, IControllable
    {
        protected Image image;
        protected Int32 tipoequipo1 = 7;
        protected Image imagen1 = null;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private FeedWaterHeaterController controller;

        public FeedWaterHeaterElement() : base() { }

        public FeedWaterHeaterElement(Rectangle rec, Int32 tipoequipo, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoequipo, imagen2, NumEquipo)
        { }

        public FeedWaterHeaterElement(Point l, Size s, Int32 tipoequipo, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoequipo, imagen2, NumEquipo)
        { }

        public FeedWaterHeaterElement(int top, int left, int width, int height, Int32 tipoequipo, Image imagen2, Double NumEquipo)
        {
            NumEquipo1 = NumEquipo;
            tipoequipo1 = tipoequipo;
            imagen1 = imagen2;
            location = new Point(top, left);
            size = new Size(width, height);
        }

        public FeedWaterHeaterElement(Rectangle rec) : base(rec) { }

        public FeedWaterHeaterElement(Point l, Size s) : base(l, s) { }

        public FeedWaterHeaterElement(int top, int left, int width, int height) : base(top, left, width, height) { }

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
                controller = new FeedWaterHeaterController(this);
            return controller;
        }
    }
}
