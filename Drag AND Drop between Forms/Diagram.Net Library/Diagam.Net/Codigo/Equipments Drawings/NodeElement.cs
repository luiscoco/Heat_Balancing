using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
	/// <summary>
	/// This is the base for all node element.
	/// </summary>
	[Serializable]
	public abstract class NodeElement: BaseElement
	{
        public ConnectorElement[] connects;
       
        protected const int connectSize = 3;

        protected Int32 tipoelemento = 1;
        protected Image imagen2;
		
//		public NodeElement(): base() 
//		{
//			InitConnectors();
//		}

		protected NodeElement(int top, int left, int width, int height,int tipoelemento1): base(top, left, width, height, 0)
		{
            tipoelemento = tipoelemento1;  
			InitConnectors();
		}

		[Browsable(false)]

        [Category("Graphical Properties")]
        public virtual ConnectorElement[] Connectors
		{
			get
			{
				return connects;
			}
		}

        [Category("Graphical Properties")]
        public override Point Location
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
				UpdateConnectorsPosition();
				OnAppearanceChanged(new EventArgs());
			}
		}

        [Category("Graphical Properties")]
        public override Size Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
				UpdateConnectorsPosition();
				OnAppearanceChanged(new EventArgs());
			}
		}

        [Category("Graphical Properties")]
        public override bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;
				foreach (ConnectorElement c in connects)
				{
					c.Visible = value;
				}
				OnAppearanceChanged(new EventArgs());
			}
		}

        [Category("Graphical Properties")]
        public virtual bool IsConnected
		{
			get
			{
				foreach (ConnectorElement c in connects)
				{
					if (c.Links.Count > 0)
						return true;
				}
				return false;
			}
		}

		protected void InitConnectors()
		{           
            //Boundary Condition (Type 1)
            if (tipoelemento == 1)
            {
                connects = new ConnectorElement[2];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
            }

           //Divisor (Type 2)
           else if (tipoelemento == 2)
            {
                connects = new ConnectorElement[3];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
            }

            //PressureDrop (Type 3)
            else if (tipoelemento == 3)
            {
                connects = new ConnectorElement[2];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);        
            }

            //Pump (Type 4)
            else if (tipoelemento == 4)
            {
                connects = new ConnectorElement[2];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
            }

            //Mixer Type (5)
            else if (tipoelemento == 5)
            {
                connects = new ConnectorElement[3];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
            }

            //Reactor (Type 6)
            else if (tipoelemento == 6)
            {
                connects = new ConnectorElement[2];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
            }

            //FeedWater Heater (Type 7)
            else if (tipoelemento == 7)
            {
                connects = new ConnectorElement[5];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
                connects[3] = new ConnectorElement(this);
                connects[4] = new ConnectorElement(this);              
            }

            //Condenser (Type 8)
            else if (tipoelemento == 8)
            {
                connects = new ConnectorElement[4];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
                connects[3] = new ConnectorElement(this);
            }

            //Turbine (Type 9)
            else if (tipoelemento == 9)
            {
                connects = new ConnectorElement[2];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
            }

            //Turbine (Type 10)
            else if (tipoelemento == 10)
            {
                connects = new ConnectorElement[2];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
            }

            //Turbine (Type 11)
            else if (tipoelemento == 11)
            {
                connects = new ConnectorElement[2];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
            }

            //Moisture Separator (Type 13)
            else if (tipoelemento == 13)
            {
                connects = new ConnectorElement[3];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
            }

            //Moisture Reheater (Type 14)
            else if (tipoelemento == 14)
            {
                connects = new ConnectorElement[4];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
                connects[3] = new ConnectorElement(this);
            }

            //OffGas/Ejector Condenser (Type 15)
            else if (tipoelemento == 15)
            {
                connects = new ConnectorElement[4];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
                connects[3] = new ConnectorElement(this);
            }

            //Drainage_Cooler (Type 16)
            else if (tipoelemento == 16)
            {
                connects = new ConnectorElement[4];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
                connects[3] = new ConnectorElement(this);
            }

            //DeSuperHeater (Type 17)
            else if (tipoelemento == 17)
            {
                connects = new ConnectorElement[3];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
            }

            //Deaerator (Type 18)
            else if (tipoelemento == 18)
            {
                connects = new ConnectorElement[5];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
                connects[3] = new ConnectorElement(this);
                connects[4] = new ConnectorElement(this);
            }

            //Valve (Type 19)
            else if (tipoelemento == 19)
            {
                connects = new ConnectorElement[2];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
            }

            //FixedEnthalpy_Splitter (Type 20)
            else if (tipoelemento == 20)
            {
                connects = new ConnectorElement[3];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
            }

            //Flash_Tank (Type 21)
            else if (tipoelemento == 21)
            {
                connects = new ConnectorElement[3];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
            }

            //HeatExchanger (Type 22)
            else if (tipoelemento == 22)
            {
                connects = new ConnectorElement[4];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
                connects[3] = new ConnectorElement(this);
            }

            else
            {
                connects = new ConnectorElement[4];
                connects[0] = new ConnectorElement(this);
                connects[1] = new ConnectorElement(this);
                connects[2] = new ConnectorElement(this);
                connects[3] = new ConnectorElement(this);
            }

            UpdateConnectorsPosition();
		}

		protected void UpdateConnectorsPosition()
		{
			Point loc;
			ConnectorElement connect;

            //Boundary Condition (Type 1)
            if (tipoelemento == 1)
            {               
                //Left
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height / 2);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Splitter (Type 2)
            else if (tipoelemento == 2)
            {
                //Left-Center
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);

                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top
                loc = new Point(this.location.X + this.size.Width, this.location.Y);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);                              
            }

            //Pressure Drop (Type 3)
            else if (tipoelemento == 3)
            {
                //Left
                loc = new Point(this.location.X + this.size.Width / 2, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width / 2, this.location.Y + this.size.Height);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Pump (Type 4)
            else if (tipoelemento == 4)
            {
                //Left
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height / 2);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Mixer (Type 5)
            else if (tipoelemento == 5)
            {
                //Left-Top
                loc = new Point(this.location.X, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Left-Down
                loc = new Point(this.location.X, this.location.Y + this.size.Height);
                connects[1].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height / 2);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Reactor (Type 6)
            else if (tipoelemento == 6)
            {
                //Left
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height / 2);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);               
            }

            //FeedWater-Heater (Type 7)
            else if (tipoelemento == 7)
            {
                //Right-Bottom (Red)
                loc = new Point(this.location.X + this.size.Width,
                    this.location.Y + this.size.Height);

                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Top-Center (Blue)
                loc = new Point(this.location.X + this.size.Width/2,
                    this.location.Y);
                connects[1].FillColor1 = Color.Blue;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Top-Left (Green)
                loc = new Point(this.location.X,
                    this.location.Y);
                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top (Yellow)
                loc = new Point(this.location.X + this.size.Width,
                    this.location.Y);

                connects[3].FillColor1 = Color.Yellow;

                connect = (ConnectorElement)connects[3];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Left-Bottom (Black)
                loc = new Point(this.location.X, this.location.Y + this.size.Height);

                connects[4].FillColor1 = Color.Black;

                connect = (ConnectorElement)connects[4];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Condenser (Type 8)
            else if (tipoelemento == 8)
            {
                //Left-Top (Red)
                loc = new Point(this.location.X, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Left-Down (Blue)
                loc = new Point(this.location.X, this.location.Y + this.size.Height);
                connects[1].FillColor1 = Color.Blue;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top (Green)
                loc = new Point(this.location.X + this.size.Width, this.location.Y);
                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Down (Yellow)
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);
                connects[3].FillColor1 = Color.Yellow;

                connect = (ConnectorElement)connects[3];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Turbine (Type 9)
            else if (tipoelemento == 9)
            {             
                //Left-Top
                loc = new Point(this.location.X,
                    this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom
                loc = new Point(this.location.X + this.size.Width,
                    this.location.Y + this.size.Height);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Turbine Without_Exhaust_Losses (Type 10)
            else if (tipoelemento == 10)
            {
                //Left
                loc = new Point(this.location.X,
                    this.location.Y + this.size.Height / 2);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width,
                    this.location.Y + this.size.Height / 2);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Turbine Auxiliar_Turbine (Type 11)
            else if (tipoelemento == 11)
            {
                //Left
                loc = new Point(this.location.X,
                    this.location.Y + this.size.Height / 2);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width,
                    this.location.Y + this.size.Height / 2);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Moisture_Separator (Type 13)
            else if (tipoelemento == 13)
            {
                //Left-Top (Red)
                loc = new Point(this.location.X, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top (Green)
                loc = new Point(this.location.X + this.size.Width, this.location.Y);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom (Yellow)
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);

                connects[2].FillColor1 = Color.Yellow;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Moisture_Reheater (Type 14)
            else if (tipoelemento == 14)
            {
                //Left-Top (Red)
                loc = new Point(this.location.X, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Left-Bottom (Blue)
                loc = new Point(this.location.X, this.location.Y + this.size.Height);
                connects[1].FillColor1 = Color.Blue;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top (Green)
                loc = new Point(this.location.X + this.size.Width, this.location.Y);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom (Yellow)
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);

                connects[3].FillColor1 = Color.Yellow;

                connect = (ConnectorElement)connects[3];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Off-Gas_Condenser (Type 15)
            else if (tipoelemento == 15)
            {
                //Center-Top (Red)
                loc = new Point(this.location.X + this.size.Width / 2, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top (Blue)
                loc = new Point(this.location.X + this.size.Width, this.location.Y);
                connects[1].FillColor1 = Color.Blue;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Center-Bottom (Green)
                loc = new Point(this.location.X + this.size.Width/2, this.location.Y + this.size.Height);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom (Yellow)
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);

                connects[3].FillColor1 = Color.Yellow;

                connect = (ConnectorElement)connects[3];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Drainage_Cooler (Type 16)
            else if (tipoelemento == 16)
            {
                //Center-Top (Red)
                loc = new Point(this.location.X + this.size.Width / 2, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom (Blue)
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);
                connects[1].FillColor1 = Color.Blue;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Center-Bottom (Green)
                loc = new Point(this.location.X + this.size.Width / 2, this.location.Y + this.size.Height);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Left-Bottom (Yellow)
                loc = new Point(this.location.X, this.location.Y + this.size.Height);

                connects[3].FillColor1 = Color.Yellow;

                connect = (ConnectorElement)connects[3];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //DeSuperHeater (Type 17)
            else if (tipoelemento == 17)
            {
                //Center-Top (Red)
                loc = new Point(this.location.X + this.size.Width / 2, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Center-Left (Blue)
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);
                connects[1].FillColor1 = Color.Blue;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Center-Right (Green)
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height / 2);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Deaerator (Type 18)
            else if (tipoelemento == 18)
            {
                //Left-Center (Red)
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Left-Bottom (Blue)
                loc = new Point(this.location.X, this.location.Y);
                connects[1].FillColor1 = Color.Blue;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Center (Green)
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height / 2);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top (Green)
                loc = new Point(this.location.X + this.size.Width, this.location.Y);

                connects[3].FillColor1 = Color.Yellow;

                connect = (ConnectorElement)connects[3];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Center-Top (Green)
                loc = new Point(this.location.X + this.size.Width / 2, this.location.Y);

                connects[4].FillColor1 = Color.Magenta;

                connect = (ConnectorElement)connects[4];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //Boundary Condition (Type 19)
            else if (tipoelemento == 19)
            {
                //Left
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height / 2);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //FixedEnthalpy_Splitter (Type 20)
            else if (tipoelemento == 20)
            {
                //Left-Center
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);

                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top
                loc = new Point(this.location.X + this.size.Width, this.location.Y);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //FlashTank (Type 21)
            else if (tipoelemento == 21)
            {
                //Left-Center
                loc = new Point(this.location.X, this.location.Y + this.size.Height / 2);

                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top
                loc = new Point(this.location.X + this.size.Width, this.location.Y);

                connects[1].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            //HeatExchanger(Type 22)
            else if (tipoelemento == 22)
            {
                //Left-Top (Red)
                loc = new Point(this.location.X, this.location.Y);
                connects[0].FillColor1 = Color.Red;

                connect = (ConnectorElement)connects[0];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Left-Bottom (Blue)
                loc = new Point(this.location.X, this.location.Y + this.size.Height);
                connects[1].FillColor1 = Color.Blue;

                connect = (ConnectorElement)connects[1];

                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Top (Green)
                loc = new Point(this.location.X + this.size.Width, this.location.Y);

                connects[2].FillColor1 = Color.Green;

                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right-Bottom (Yellow)
                loc = new Point(this.location.X + this.size.Width, this.location.Y + this.size.Height);

                connects[3].FillColor1 = Color.Yellow;

                connect = (ConnectorElement)connects[3];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }

            else
            {
                //Top
                loc = new Point(this.location.X + this.size.Width / 2,
                    this.location.Y);
                connect = (ConnectorElement)connects[0];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Botton
                loc = new Point(this.location.X + this.size.Width / 2,
                    this.location.Y + this.size.Height);
                connect = (ConnectorElement)connects[1];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Left
                loc = new Point(this.location.X,
                    this.location.Y + this.size.Height / 2);
                connect = (ConnectorElement)connects[2];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);

                //Right
                loc = new Point(this.location.X + this.size.Width,
                    this.location.Y + this.size.Height / 2);
                connect = (ConnectorElement)connects[3];
                connect.Location = new Point(loc.X - connectSize, loc.Y - connectSize);
                connect.Size = new Size(connectSize * 2, connectSize * 2);
            }
		}

		public override void Invalidate()
		{
			base.Invalidate ();

			for(int i = connects.Length - 1; i >= 0; i--)
			{
				//connects[i].Invalidate();

				for(int ii = connects[i].Links.Count - 1; ii >= 0; ii--)
				{
					connects[i].Links[ii].Invalidate();
				}				
			}
		}

		internal virtual void Draw(Graphics g, bool drawConnector)
		{
			this.Draw(g);
			if (drawConnector)
				DrawConnectors(g);
		}

		protected void DrawConnectors(Graphics g)
		{
			foreach (ConnectorElement ce in connects)
			{
				ce.Draw(g);
			}
		}

		public virtual ElementCollection GetLinkedNodes()
		{
			ElementCollection ec = new ElementCollection();

			foreach(ConnectorElement ce in connects)
			{
				foreach(BaseLinkElement le in ce.Links)
				{
					if (le.Connector1 == ce)
					{
						ec.Add(le.Connector2.ParentElement);
					}
					else
					{
						ec.Add(le.Connector1.ParentElement);
					}
				}
			}
			
			return ec;
		}
	}
}
