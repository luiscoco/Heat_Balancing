using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class MixerNode : NodeElement, IControllable, ILabelElement
    {
        public MixerElement mixer;

        protected ImageElement imagen1;
        protected LabelElement label = new LabelElement();
        protected ConnectorElement[] connectors12 = new ConnectorElement[10];
        protected Image imagen10;
        private Int32 tipoelemento1 = 5;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
        private MixerController controller;

        public MixerNode(Rectangle rec, int tipoelemento2, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public MixerNode(Point l, Size s, int tipoelemento2, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public MixerNode(int top, int left, int width, int height, int tipoelemento2, Image imagen2, Double NumEquipo) : base(top, left, width, height, tipoelemento2)
        {
            NumEquipo1 = NumEquipo;
            tipoelemento1 = tipoelemento2;
            imagen10 = imagen2;
            connectors12 = base.connects;
            mixer = new MixerElement(top, left, width, height, tipoelemento1, imagen10, NumEquipo1);
            SyncContructors();
        }

        public MixerNode() : this(0, 0, 100, 100, 5, null, 0)
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
                mixer.BorderColor = value;
                base.BorderColor = value;
            }
        }

        public Color FillColor1
        {
            get
            {
                return mixer.FillColor1;
            }
            set
            {
                mixer.FillColor1 = value;
            }
        }

        public Color FillColor2
        {
            get
            {
                return mixer.FillColor2;
            }
            set
            {
                mixer.FillColor2 = value;
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
                mixer.Opacity = value;
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
                mixer.Visible = value;
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
                mixer.Location = value;
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
                mixer.Size = value;
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
                mixer.BorderWidth = value;
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
            base.location = mixer.Location;
            base.size = mixer.Size;
            base.borderColor = mixer.BorderColor;
            base.borderWidth = mixer.BorderWidth;
            base.opacity = mixer.Opacity;
            base.visible = mixer.Visible;
            //IMPORTANT, include the streams names in each equipment type.
            connects[0].ConnectorElementName = "N1"; //Red Connector (Input Stream)
            connects[1].ConnectorElementName = "N2"; //Green Connector (Output Steam)
            connects[2].ConnectorElementName = "N3"; //Green Connector (Output Steam)
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            mixer.Draw(g);
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new MixerController(this);
            return controller;
        }
    }
}

