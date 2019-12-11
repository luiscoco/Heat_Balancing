using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class SupercriticalTurbineNode : NodeElement, IControllable, ILabelElement
    {
        public SupercriticalTurbineElement SupercriticalTurbine;

        protected ImageElement imagen1;
        protected LabelElement label = new LabelElement();
        protected ConnectorElement[] connectors12 = new ConnectorElement[10];
        protected Image imagen10;
        private Int32 tipoelemento1 = 24;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private SupercriticalTurbineController controller;

        public SupercriticalTurbineNode(Rectangle rec, int tipoelemento2, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public SupercriticalTurbineNode(Point l, Size s, int tipoelemento2, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public SupercriticalTurbineNode(int top, int left, int width, int height, int tipoelemento2, Image imagen2, Double NumEquipo) : base(top, left, width, height, tipoelemento2)
        {
            NumEquipo1 = NumEquipo;
            tipoelemento1 = tipoelemento2;
            imagen10 = imagen2;
            connectors12 = base.connects;
            SupercriticalTurbine = new SupercriticalTurbineElement(top, left, width, height, tipoelemento1, imagen10, NumEquipo1);
            SyncContructors();
        }

        public SupercriticalTurbineNode() : this(0, 0, 100, 100, 24, null, 0)
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
                SupercriticalTurbine.BorderColor = value;
                base.BorderColor = value;
            }
        }

        public Color FillColor1
        {
            get
            {
                return SupercriticalTurbine.FillColor1;
            }
            set
            {
                SupercriticalTurbine.FillColor1 = value;
            }
        }

        public Color FillColor2
        {
            get
            {
                return SupercriticalTurbine.FillColor2;
            }
            set
            {
                SupercriticalTurbine.FillColor2 = value;
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
                SupercriticalTurbine.Opacity = value;
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
                SupercriticalTurbine.Visible = value;
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
                SupercriticalTurbine.Location = value;
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
                SupercriticalTurbine.Size = value;
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
                SupercriticalTurbine.BorderWidth = value;
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
            base.location = SupercriticalTurbine.Location;
            base.size = SupercriticalTurbine.Size;
            base.borderColor = SupercriticalTurbine.BorderColor;
            base.borderWidth = SupercriticalTurbine.BorderWidth;
            base.opacity = SupercriticalTurbine.Opacity;
            base.visible = SupercriticalTurbine.Visible;
            //IMPORTANT, include the streams names in each equipment type.
            connects[0].ConnectorElementName = "N1"; //Red Connector (Input Stream)
            connects[1].ConnectorElementName = "N2"; //Green Connector (Output Steam)
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            SupercriticalTurbine.Draw(g);
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new SupercriticalTurbineController(this);
            return controller;
        }
    }
}

