using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Text;


namespace Dalssoft.DiagramNet
{
	[Serializable]
	public class ElipseElement: RectangleElement, IControllable
	{
		[NonSerialized]
		private ElipseController controller;

		public ElipseElement(): base() {}

		public ElipseElement(Rectangle rec): base(rec) {}

		public ElipseElement(Point l, Size s): base(l, s) {}

		public ElipseElement(int top, int left, int width, int height): base(top, left, width, height) {}

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;

			Rectangle r = GetUnsignedRectangle(
				new Rectangle(
				location.X, location.Y, 
				size.Width, size.Height));

			//Fill elipse
			Color fill1;
			Color fill2;
			Brush b;
			if (opacity == 100)
			{
				fill1 = fillColor1;
				fill2 = fillColor2;
			}
			else
			{
				fill1 = Color.FromArgb((int) (255.0f * (opacity / 100.0f)), fillColor1);
				fill2 = Color.FromArgb((int) (255.0f * (opacity / 100.0f)), fillColor2);
			}
			
			if (fillColor2 == Color.Empty)
				b = new SolidBrush(fill1);
			else
			{
				Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
				b = new LinearGradientBrush(
					rb,
					fill1, 
					fill2, 
					LinearGradientMode.Horizontal);
			}

			g.FillEllipse(b, r);

			//Border
			Pen p;
			p = new Pen(borderColor, borderWidth);
			g.DrawEllipse(p, r);
			
			
            //draw Text 
            using (Font font1 = new Font("Times New Roman", 11, FontStyle.Bold, GraphicsUnit.Pixel))
            { 
                PointF pointF1 = new PointF(r.X + (r.Width/2)-7, r.Y + (r.Height/2)-7);
                g.DrawString("10", font1, Brushes.White, pointF1);
            }

            p.Dispose();
            b.Dispose();
        }

        IController IControllable.GetController()
		{
			if (controller == null)
				controller = new ElipseController(this);
			return controller;
		}
	}
}
