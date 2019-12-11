using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class BoundaryConditionNode : NodeElement, IControllable, ILabelElement
    {
        //    public BoundaryConditionElement boundaryCondition;
        //    public LabelElement label = new LabelElement();

        //    [NonSerialized]
        //    private BoundaryConditionController controller;


        //    public BoundaryConditionNode() : this(0, 0, 100, 100, 1)
        //    {

        //    }

        //    public BoundaryConditionNode(Rectangle rec, int tipoelemento2) : this(rec.Location, rec.Size, tipoelemento2)
        //    {

        //    }

        //    public BoundaryConditionNode(Point l, Size s, int tipoelemento2) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2)
        //    {

        //    }

        //    public BoundaryConditionNode(int top, int left, int width, int height, int tipoelemento2) : base(top, left, width, height, tipoelemento2)
        //    {
        //        boundaryCondition = new BoundaryConditionElement(top, left, width, height);
        //        SyncContructors();
        //    }


        public BoundaryConditionElement boundaryCondition;

        protected ImageElement imagen1;
        protected LabelElement label = new LabelElement();
        protected ConnectorElement[] connectors12 = new ConnectorElement[10];
        protected Image imagen10;
        private Int32 tipoelemento1 = 1;
        protected Double NumEquipo1 = 0;

        //Caudal ()
        public double D1;
        //Presión ()
        public double D2;
        //Entalpía ()
        public double D3;
        //No plantea la ecuación del caudal en sistemas cerrados (D5=1)
        //pero mantiene D1 si es distinto de cero. 
        public double D5;
        //Valor de la presión, o la temperatura si es negativo
        public double D6;
        //Título 
        public double D7;

        [NonSerialized]
        private BoundaryConditionController controller;

        public BoundaryConditionNode(Rectangle rec, int tipoelemento2, Image imagen2, Double NumEquipo) : this(rec.Location, rec.Size, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public BoundaryConditionNode(Point l, Size s, int tipoelemento2, Image imagen2, Double NumEquipo) : this(l.X, l.Y, s.Width, s.Height, tipoelemento2, imagen2, NumEquipo)
        {

        }

        public BoundaryConditionNode(int top, int left, int width, int height, int tipoelemento2, Image imagen2, Double NumEquipo) : base(top, left, width, height, tipoelemento2)
        {
            NumEquipo1 = NumEquipo;
            tipoelemento1 = tipoelemento2;
            imagen10 = imagen2;
            connectors12 = base.connects;
            boundaryCondition = new BoundaryConditionElement(top, left, width, height, tipoelemento1, imagen10, NumEquipo1);
            SyncContructors();
        }

        public BoundaryConditionNode() : this(0, 0, 100, 100, 1, null, 0)
        {

        }

        [Category("Equipment Properties")]
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

        [Category("Equipment Properties")]
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

        [Category("Data Input")]
        public double Mass_Flow_D1
        {
            get
            {
                return D1;
            }

            set
            {
                D1 = value;
            }
        }

        [Category("Data Input")]
        public double Pressure_D2
        {
            get
            {
                return D2;
            }

            set
            {
                D2 = value;
            }
        }

        [Category("Data Input")]
        public double Enthalpy_D3
        {
            get
            {
                return D3;
            }

            set
            {
                D3 = value;
            }
        }

        [Category("Graphical Properties")]
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

        [Category("Graphical Properties")]
        public override Color BorderColor
        {
            get
            {
                return base.BorderColor;
            }
            set
            {
                boundaryCondition.BorderColor = value;
                base.BorderColor = value;
            }
        }

        [Category("Graphical Properties")]
        public Color FillColor1
        {
            get
            {
                return boundaryCondition.FillColor1;
            }
            set
            {
                boundaryCondition.FillColor1 = value;
            }
        }

        [Category("Graphical Properties")]
        public Color FillColor2
        {
            get
            {
                return boundaryCondition.FillColor2;
            }
            set
            {
                boundaryCondition.FillColor2 = value;
            }
        }

        [Category("Graphical Properties")]
        public override int Opacity
        {
            get
            {
                return base.Opacity;
            }
            set
            {
                boundaryCondition.Opacity = value;
                base.Opacity = value;
            }
        }

        [Category("Graphical Properties")]
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                boundaryCondition.Visible = value;
                base.Visible = value;
            }
        }

        [Category("Graphical Properties")]
        public override Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                boundaryCondition.Location = value;
                base.Location = value;
            }
        }

        [Category("Graphical Properties")]
        public override Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                boundaryCondition.Size = value;
                base.Size = value;
            }
        }

        [Category("Graphical Properties")]
        public override int BorderWidth
        {
            get
            {
                return base.BorderWidth;
            }
            set
            {
                boundaryCondition.BorderWidth = value;
                base.BorderWidth = value;
            }
        }

        [Category("Graphical Properties")]
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
            base.location = boundaryCondition.Location;
            base.size = boundaryCondition.Size;
            base.borderColor = boundaryCondition.BorderColor;
            base.borderWidth = boundaryCondition.BorderWidth;
            base.opacity = boundaryCondition.Opacity;
            base.visible = boundaryCondition.Visible;
            //IMPORTANT, include the streams names in each equipment type.
            connects[0].ConnectorElementName = "N1"; //Red Connector (Input Stream)
            connects[1].ConnectorElementName = "N2"; //Green Connector (Output Steam)
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            boundaryCondition.Draw(g);
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new BoundaryConditionController(this);
            return controller;
        }
    }
}

