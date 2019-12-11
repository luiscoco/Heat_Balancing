using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class BoundaryConditionElement : RectangleElement, IControllable
    {

        //[NonSerialized]
        //private BoundaryConditionController controller;

        //public BoundaryConditionElement() : base() { }

        //public BoundaryConditionElement(Rectangle rec) : base(rec) { }

        //public BoundaryConditionElement(Point l, Size s) : base(l, s) { }

        //public BoundaryConditionElement(int top, int left, int width, int height) : base(top, left, width, height) { }

        protected Image image;
        protected Int32 tipoequipo1 = 9;
        protected Image imagen1 = null;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private BoundaryConditionController controller;

        public BoundaryConditionElement() : base() { }

        public BoundaryConditionElement(Rectangle rec, Int32 tipoequipo, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoequipo, imagen2, NumEquipo)
        { }

        public BoundaryConditionElement(Point l, Size s, Int32 tipoequipo, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoequipo, imagen2, NumEquipo)
        { }

        public BoundaryConditionElement(int top, int left, int width, int height, Int32 tipoequipo, Image imagen2, Double NumEquipo)
        {
            NumEquipo1 = NumEquipo;
            tipoequipo1 = tipoequipo;
            imagen1 = imagen2;
            location = new Point(top, left);
            size = new Size(width, height);
        }

        public BoundaryConditionElement(Rectangle rec) : base(rec) { }

        public BoundaryConditionElement(Point l, Size s) : base(l, s) { }

        public BoundaryConditionElement(int top, int left, int width, int height) : base(top, left, width, height) { }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;

            Rectangle r = GetUnsignedRectangle();

            if (imagen1 != null)

                g.DrawImage(imagen1, r);

            //IsInvalidated = false;

            //Rectangle r = GetUnsignedRectangle(
            //    new Rectangle(
            //    location.X, location.Y,
            //    size.Width, size.Height));

            ////Fill 
            //Color fill1;
            //Color fill2;
            //Brush b;
            //if (opacity == 100)
            //{
            //    fill1 = fillColor1;
            //    fill2 = fillColor2;
            //}
            //else
            //{
            //    fill1 = Color.FromArgb((int)(255.0f * (opacity / 100.0f)), fillColor1);
            //    fill2 = Color.FromArgb((int)(255.0f * (opacity / 100.0f)), fillColor2);
            //}

            //if (fillColor2 == Color.Empty)
            //    b = new SolidBrush(fill1);
            //else
            //{
            //    Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
            //    b = new LinearGradientBrush(
            //        rb,
            //        fill1,
            //        fill2,
            //        LinearGradientMode.Horizontal);
            //}

            //Pen p1 = new Pen(Color.Black, 1);
            //Point[] puntos = new Point[4];

            //puntos[0].X = this.Location.X + 10;
            //puntos[0].Y = this.Location.Y + 12;

            //puntos[1].X = this.Location.X - 10 + this.Size.Width;
            //puntos[1].Y = this.Location.Y + 12;

            //puntos[2].X = this.Location.X + this.Size.Width - 10;
            //puntos[2].Y = this.Location.Y + this.Size.Height - 12;

            //puntos[3].X = this.Location.X + 10;
            //puntos[3].Y = this.Location.Y + this.Size.Height - 12;

            //g.DrawPolygon(p1, puntos);

            //g.FillPolygon(b, puntos);

            //p1.Dispose();
            //b.Dispose();

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
                controller = new BoundaryConditionController(this);
            return controller;
        }
    }
}

