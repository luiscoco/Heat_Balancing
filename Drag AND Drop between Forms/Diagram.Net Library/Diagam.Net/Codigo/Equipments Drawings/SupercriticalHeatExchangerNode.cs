using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class SupercriticalHeatExchangerNode : NodeElement, IControllable, ILabelElement
    {
        public SupercriticalHeatExchangerElement SupercriticalHeatExchanger;

        protected ImageElement imagen1;
        protected LabelElement label = new LabelElement();
        protected ConnectorElement[] connectors12 = new ConnectorElement[10];
        protected Image imagen10;
        private Int32 tipoelemento1 = 23;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private SupercriticalHeatExchangerController controller;

        public SupercriticalHeatExchangerNode(Rectangle rec, int tipoelemento2, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public SupercriticalHeatExchangerNode(Point l, Size s, int tipoelemento2, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public SupercriticalHeatExchangerNode(int top, int left, int width, int height, int tipoelemento2, Image imagen2, Double NumEquipo) : base(top, left, width, height, tipoelemento2)
        {
            NumEquipo1 = NumEquipo;
            tipoelemento1 = tipoelemento2;
            imagen10 = imagen2;
            connectors12 = base.connects;
            SupercriticalHeatExchanger = new SupercriticalHeatExchangerElement(top, left, width, height, tipoelemento1, imagen10, NumEquipo1);
            SyncContructors();
        }

        public SupercriticalHeatExchangerNode() : this(0, 0, 100, 100, 23, null, 0)
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
                SupercriticalHeatExchanger.BorderColor = value;
                base.BorderColor = value;
            }
        }

        public Color FillColor1
        {
            get
            {
                return SupercriticalHeatExchanger.FillColor1;
            }
            set
            {
                SupercriticalHeatExchanger.FillColor1 = value;
            }
        }

        public Color FillColor2
        {
            get
            {
                return SupercriticalHeatExchanger.FillColor2;
            }
            set
            {
                SupercriticalHeatExchanger.FillColor2 = value;
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
                SupercriticalHeatExchanger.Opacity = value;
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
                SupercriticalHeatExchanger.Visible = value;
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
                SupercriticalHeatExchanger.Location = value;
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
                SupercriticalHeatExchanger.Size = value;
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
                SupercriticalHeatExchanger.BorderWidth = value;
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
            base.location = SupercriticalHeatExchanger.Location;
            base.size = SupercriticalHeatExchanger.Size;
            base.borderColor = SupercriticalHeatExchanger.BorderColor;
            base.borderWidth = SupercriticalHeatExchanger.BorderWidth;
            base.opacity = SupercriticalHeatExchanger.Opacity;
            base.visible = SupercriticalHeatExchanger.Visible;
            //IMPORTANT, include the streams names in each equipment type.
            connects[0].ConnectorElementName = "N1"; //Red Connector (Input Stream)
            connects[1].ConnectorElementName = "N2"; //Green Connector (Output Steam)
            connects[2].ConnectorElementName = "N3"; //Red Connector (Input Stream)
            connects[3].ConnectorElementName = "N4"; //Green Connector (Output Steam)
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            SupercriticalHeatExchanger.Draw(g);
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new SupercriticalHeatExchangerController(this);
            return controller;
        }
    }
}

