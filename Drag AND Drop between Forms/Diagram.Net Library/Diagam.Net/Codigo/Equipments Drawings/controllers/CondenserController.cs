using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Dalssoft.DiagramNet
{
    /// <summary>
    /// This class is the controller for ElipseElement
    /// </summary>
    internal class CondenserController : RectangleController, IController
    {
        public CondenserController(BaseElement element) : base(element)
        {
        }

        #region IController Members

        public override bool HitTest(Point p)
        {
            GraphicsPath gp = new GraphicsPath();
            Matrix mtx = new Matrix();

            Point elLocation = el.Location;
            Size elSize = el.Size;
            gp.AddRectangle(new Rectangle(elLocation.X,
                elLocation.Y,
                elSize.Width,
                elSize.Height));
            gp.Transform(mtx);

            //MessageBox.Show("Has clickado dentro de la Turbina");

            return gp.IsVisible(p);
        }

        public override bool HitTest(Rectangle r)
        {
            GraphicsPath gp = new GraphicsPath();
            Matrix mtx = new Matrix();

            Point elLocation = el.Location;
            Size elSize = el.Size;
            gp.AddRectangle(new Rectangle(elLocation.X,
                elLocation.Y,
                elSize.Width,
                elSize.Height));
            gp.Transform(mtx);
            Rectangle retGp = Rectangle.Round(gp.GetBounds());

            return r.Contains(retGp);
        }

        public override void DrawSelection(System.Drawing.Graphics g)
        {
            Color selColor = Color.Red;
            int border = 3;

            Rectangle r = BaseElement.GetUnsignedRectangle(
                new Rectangle(
                    el.Location.X - border, el.Location.Y - border,
                    el.Size.Width + (border * 2), el.Size.Height + (border * 2)));

            //Pen p=new Pen(Color,Anchura del pincel)
            Pen p = new Pen(Color.Yellow, 2);

            Point[] puntos = new Point[4];

            puntos[0].X = el.Location.X;
            puntos[0].Y = el.Location.Y;

            puntos[1].X = el.Location.X + el.Size.Width;
            puntos[1].Y = el.Location.Y;

            puntos[2].X = el.Location.X + el.Size.Width;
            puntos[2].Y = el.Location.Y + el.Size.Height;

            puntos[3].X = el.Location.X;
            puntos[3].Y = el.Location.Y + el.Size.Height;

            g.DrawPolygon(p, puntos);

            //g.FillPolygon(brush, puntos);

            p.Dispose();
            //brush.Dispose();
        }

        #endregion
    }
}

