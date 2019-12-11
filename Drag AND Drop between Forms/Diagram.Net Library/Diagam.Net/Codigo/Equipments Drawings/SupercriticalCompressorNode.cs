using System;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class SupercriticalCompressorNode : NodeElement, IControllable, ILabelElement
    {
        public SupercriticalCompressorElement SupercriticalCompressor;

        protected ImageElement imagen1;
        protected LabelElement label = new LabelElement();
        protected ConnectorElement[] connectors12 = new ConnectorElement[10];
        protected Image imagen10;
        private Int32 tipoelemento1 = 25;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private SupercriticalCompressorController controller;

        public SupercriticalCompressorNode(Rectangle rec, int tipoelemento2, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public SupercriticalCompressorNode(Point l, Size s, int tipoelemento2, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public SupercriticalCompressorNode(int top, int left, int width, int height, int tipoelemento2, Image imagen2, Double NumEquipo) : base(top, left, width, height, tipoelemento2)
        {
            NumEquipo1 = NumEquipo;
            tipoelemento1 = tipoelemento2;
            imagen10 = imagen2;
            connectors12 = base.connects;
            SupercriticalCompressor = new SupercriticalCompressorElement(top, left, width, height, tipoelemento1, imagen10, NumEquipo1);
            SyncContructors();
        }

        public SupercriticalCompressorNode() : this(0, 0, 100, 100, 25, null, 0)
        {

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
                SupercriticalCompressor.BorderColor = value;
                base.BorderColor = value;
            }
        }

        public Color FillColor1
        {
            get
            {
                return SupercriticalCompressor.FillColor1;
            }
            set
            {
                SupercriticalCompressor.FillColor1 = value;
            }
        }

        public Color FillColor2
        {
            get
            {
                return SupercriticalCompressor.FillColor2;
            }
            set
            {
                SupercriticalCompressor.FillColor2 = value;
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
                SupercriticalCompressor.Opacity = value;
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
                SupercriticalCompressor.Visible = value;
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
                SupercriticalCompressor.Location = value;
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
                SupercriticalCompressor.Size = value;
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
                SupercriticalCompressor.BorderWidth = value;
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
            base.location = SupercriticalCompressor.Location;
            base.size = SupercriticalCompressor.Size;
            base.borderColor = SupercriticalCompressor.BorderColor;
            base.borderWidth = SupercriticalCompressor.BorderWidth;
            base.opacity = SupercriticalCompressor.Opacity;
            base.visible = SupercriticalCompressor.Visible;
            //IMPORTANT, include the streams names in each equipment type.
            connects[0].ConnectorElementName = "N1"; //Red Connector (Input Stream)
            connects[1].ConnectorElementName = "N2"; //Green Connector (Output Steam)
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            SupercriticalCompressor.Draw(g);
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new SupercriticalCompressorController(this);
            return controller;
        }
    }
}


