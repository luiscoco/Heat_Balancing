using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class FeedWaterHeaterNode : NodeElement, IControllable, ILabelElement
    {       
        public FeedWaterHeaterElement feedWaterHeater;

        protected ImageElement imagen1;
        protected LabelElement label = new LabelElement();
        protected ConnectorElement[] connectors12 = new ConnectorElement[10];
        protected Image imagen10;
        private Int32 tipoelemento1 = 7;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private FeedWaterHeaterController controller;

        public FeedWaterHeaterNode(Rectangle rec, int tipoelemento2, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public FeedWaterHeaterNode(Point l, Size s, int tipoelemento2, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public FeedWaterHeaterNode(int top, int left, int width, int height, int tipoelemento2, Image imagen2, Double NumEquipo) : base(top, left, width, height, tipoelemento2)
        {
            NumEquipo1 = NumEquipo;
            tipoelemento1 = tipoelemento2;
            imagen10 = imagen2;
            connectors12 = base.connects;
            feedWaterHeater = new FeedWaterHeaterElement(top, left, width, height, tipoelemento1, imagen10, NumEquipo1);
            SyncContructors();
        }

        public FeedWaterHeaterNode() : this(0, 0, 100, 100, 7, null, 0)
        {

        }

        public int Element_Type
        {
            get
            {
                return tipoelemento1;
            }

            set
            {
                tipoelemento1 = value;
            }
        }

        public double Equipment_Number
        {
            get
            {
                return NumEquipo1;
            }

            set
            {
                NumEquipo1 = value;
            }
        }

        public ConnectorElement[] Conectores
        {
            get
            {
                return base.connects;
            }

            set
            {
                base.connects = value;
            }
        }

        public override Color BorderColor
        {
            get
            {
                return base.BorderColor;
            }
            set
            {
                feedWaterHeater.BorderColor = value;
                base.BorderColor = value;
            }
        }

        public Color FillColor1
        {
            get
            {
                return feedWaterHeater.FillColor1;
            }
            set
            {
                feedWaterHeater.FillColor1 = value;
            }
        }

        public Color FillColor2
        {
            get
            {
                return feedWaterHeater.FillColor2;
            }
            set
            {
                feedWaterHeater.FillColor2 = value;
            }
        }

        public override int Opacity
        {
            get
            {
                return base.Opacity;
            }
            set
            {
                feedWaterHeater.Opacity = value;
                base.Opacity = value;
            }
        }

        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                feedWaterHeater.Visible = value;
                base.Visible = value;
            }
        }

        public override Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                feedWaterHeater.Location = value;
                base.Location = value;
            }
        }

        public override Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                feedWaterHeater.Size = value;
                base.Size = value;
            }
        }

        public override int BorderWidth
        {
            get
            {
                return base.BorderWidth;
            }
            set
            {
                feedWaterHeater.BorderWidth = value;
                base.BorderWidth = value;
            }
        }

        public virtual LabelElement Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        private void SyncContructors()
        {
            base.location = feedWaterHeater.Location;
            base.size = feedWaterHeater.Size;
            base.borderColor = feedWaterHeater.BorderColor;
            base.borderWidth = feedWaterHeater.BorderWidth;
            base.opacity = feedWaterHeater.Opacity;
            base.visible = feedWaterHeater.Visible;
            //IMPORTANT, include the streams names in each equipment type.
            connects[0].ConnectorElementName = "N1"; //Right-Bottom (Red)
            connects[1].ConnectorElementName = "N2"; //Top-Center (Blue)
            connects[2].ConnectorElementName = "N3"; //Top-Left (Green)
            connects[3].ConnectorElementName = "N4"; //Right-Top (Yellow)
            connects[4].ConnectorElementName = "N5"; //Left-Bottom (Black)
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            feedWaterHeater.Draw(g);
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new FeedWaterHeaterController(this);
            return controller;
        }
    }
}
