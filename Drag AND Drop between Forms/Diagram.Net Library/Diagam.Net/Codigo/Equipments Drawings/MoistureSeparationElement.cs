﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class MoistureSeparationElement : RectangleElement, IControllable
    {
        protected Image image;
        protected Int32 tipoequipo1 = 13;
        protected Image imagen1 = null;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private MoistureSeparatorController controller;

        public MoistureSeparationElement() : base() { }

        public MoistureSeparationElement(Rectangle rec, Int32 tipoequipo, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoequipo, imagen2, NumEquipo)
        { }

        public MoistureSeparationElement(Point l, Size s, Int32 tipoequipo, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoequipo, imagen2, NumEquipo)
        { }

        public MoistureSeparationElement(int top, int left, int width, int height, Int32 tipoequipo, Image imagen2, Double NumEquipo)
        {
            NumEquipo1 = NumEquipo;
            tipoequipo1 = tipoequipo;
            imagen1 = imagen2;
            location = new Point(top, left);
            size = new Size(width, height);
        }

        public MoistureSeparationElement(Rectangle rec) : base(rec) { }

        public MoistureSeparationElement(Point l, Size s) : base(l, s) { }

        public MoistureSeparationElement(int top, int left, int width, int height) : base(top, left, width, height) { }

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

            //p1.Dispose();
            //b.Dispose();
        }

        public Image Image
        {
            get
            {
                return imagen1;
            }
            set
            {
                imagen1 = value;
                if (imagen1 != null)
                    // Size = image.Size;
                    OnAppearanceChanged(new EventArgs());
            }
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new MoistureSeparatorController(this);
            return controller;
        }
    }
}
