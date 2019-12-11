using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
	[Serializable]
	public class TurbinaNode: NodeElement, IControllable, ILabelElement
	{
		public TurbinaElement turbina;

        protected ImageElement imagen1;
        protected LabelElement label = new LabelElement();
        protected ConnectorElement[] connectors12 = new ConnectorElement[10];
        protected Image imagen10;
        private Int32 tipoelemento1 = 9;
        protected Double NumEquipo1 = 0;

        [NonSerialized]
		private TurbinaController controller;

        public TurbinaNode(Rectangle rec, int tipoelemento2, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public TurbinaNode(Point l, Size s, int tipoelemento2, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public TurbinaNode(int top, int left, int width, int height, int tipoelemento2, Image imagen2, Double NumEquipo) : base(top, left, width, height, tipoelemento2)
        {
            NumEquipo1 = NumEquipo;
            tipoelemento1 = tipoelemento2;
            imagen10 = imagen2;
            connectors12 = base.connects;
            turbina = new TurbinaElement(top, left, width, height, tipoelemento1, imagen10, NumEquipo1);
            SyncContructors();
        }

        public TurbinaNode() : this(0, 0, 100, 100, 9, null, 0)
        {

        }

        //public TurbinaNode(): this(0, 0, 100, 100,9)
        //{

        //      }

        //public TurbinaNode(Rectangle rec,int tipoelemento2): this(rec.Location, rec.Size,tipoelemento2)
        //{ 

        //      }

        //public TurbinaNode(Point l, Size s,int tipoelemento2): this(l.X, l.Y, s.Width, s.Height,tipoelemento2) 
        //{

        //      }

        //public TurbinaNode(int top, int left, int width, int height,int tipoelemento2): base(top, left, width,height,tipoelemento2)
        //{
        //	turbina = new TurbinaElement(top, left, width, height);
        //	SyncContructors();
        //}

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
				turbina.BorderColor = value;
				base.BorderColor = value;
			}
		}

		public Color FillColor1
		{
			get
			{
				return turbina.FillColor1;
			}
			set
			{
				turbina.FillColor1 = value;
			}
		}

		public Color FillColor2
		{
			get
			{
				return turbina.FillColor2;
			}
			set
			{
				turbina.FillColor2 = value;
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
				turbina.Opacity = value;
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
				turbina.Visible = value;
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
				turbina.Location = value;
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
				turbina.Size = value;
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
				turbina.BorderWidth = value;
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
			base.location = turbina.Location;
			base.size = turbina.Size;
			base.borderColor = turbina.BorderColor;
			base.borderWidth = turbina.BorderWidth;
			base.opacity = turbina.Opacity;
			base.visible = turbina.Visible;
            //IMPORTANT, include the streams names in each equipment type.
            connects[0].ConnectorElementName = "N1"; //Red Connector (Input Stream)
            connects[1].ConnectorElementName = "N2"; //Green Connector (Output Steam)
        }

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;
			turbina.Draw(g);
		}

		IController IControllable.GetController()
		{
			if (controller == null)
				controller = new TurbinaController(this);
			return controller;
		}
	}
}
