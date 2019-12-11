using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class ConnectionNode : NodeElement, IControllable, ILabelElement
    {
        public ConnectionElement Connection;

        protected ImageElement imagen1;
        protected LabelElement label = new LabelElement();
        protected ConnectorElement[] connectors12 = new ConnectorElement[10];
        protected Image imagen10;
        private Int32 tipoelemento1 = 12;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private ConnectionController controller;

        public ConnectionNode(Rectangle rec, int tipoelemento2, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public ConnectionNode(Point l, Size s, int tipoelemento2, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public ConnectionNode(int top, int left, int width, int height, int tipoelemento2, Image imagen2, Double NumEquipo) : base(top, left, width, height, tipoelemento2)
        {
            NumEquipo1 = NumEquipo;
            tipoelemento1 = tipoelemento2;
            imagen10 = imagen2;
            connectors12 = base.connects;           
            Connection = new ConnectionElement(top, left, width, height, tipoelemento1, imagen10, NumEquipo1);
            SyncContructors();
        }

        public ConnectionNode() : this(0, 0, 100, 100, 12, null, 0)
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
                Connection.BorderColor = value;
                base.BorderColor = value;
            }
        }

        public Color FillColor1
        {
            get
            {
                return Connection.FillColor1;
            }
            set
            {
                Connection.FillColor1 = value;
            }
        }

        public Color FillColor2
        {
            get
            {
                return Connection.FillColor2;
            }
            set
            {
                Connection.FillColor2 = value;
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
                Connection.Opacity = value;
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
                Connection.Visible = value;
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
                Connection.Location = value;
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
                Connection.Size = value;
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
                Connection.BorderWidth = value;
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
            base.location = Connection.Location;
            base.size = Connection.Size;
            base.borderColor = Connection.BorderColor;
            base.borderWidth = Connection.BorderWidth;
            base.opacity = Connection.Opacity;
            base.visible = Connection.Visible;
            //IMPORTANT, include the streams names in each equipment type.
            connects[0].ConnectorElementName = "N1"; //Center-Top (Red) (Input Stream)
            connects[1].ConnectorElementName = "N2"; //Right-Bottom (Blue) (Input Stream)
            connects[2].ConnectorElementName = "N3"; //Center-Bottom (Green) (Output Steam)
            connects[3].ConnectorElementName = "N4"; //Right-Bottom (Yellow) (Output Steam)
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            Connection.Draw(g);
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new ConnectionController(this);
            return controller;
        }
    }
}


