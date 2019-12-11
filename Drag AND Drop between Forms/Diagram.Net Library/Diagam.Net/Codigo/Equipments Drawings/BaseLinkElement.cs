using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Dalssoft.DiagramNet
{
	/// <summary>
	/// This is the base for all link element.
	/// </summary>
	[Serializable]
	public abstract class BaseLinkElement: BaseElement
	{
		protected ConnectorElement connector1;
		protected ConnectorElement connector2;
		protected LineCap startCap;
		protected LineCap endCap;
		protected bool needCalcLink = true;
        protected bool linetypearrow = true;
        public int baseConnectionNumber;
        public int ConnectionStartElementNumber;
        public int ConnectionEndElementNumber;

        internal BaseLinkElement(ConnectorElement conn1, ConnectorElement conn2): base()
		{
			borderWidth = 1;
			borderColor = Color.Black;

			connector1 = conn1;
			connector2 = conn2;

			connector1.AddLink(this);
			connector2.AddLink(this);
		}

		[Browsable(false)]
		public ConnectorElement Connector1
		{
			get
			{
				return connector1;
			}
			set
			{
				if (value == null)
					return;
				
				connector1.RemoveLink(this);
				connector1 = value;
				needCalcLink = true;
				connector1.AddLink(this);
				OnConnectorChanged(new EventArgs());
			}
		}

		[Browsable(false)]
		public ConnectorElement Connector2
		{
			get
			{
				return connector2;
			}
			set
			{
				if (value == null)
					return;
				
				connector2.RemoveLink(this);
				connector2 = value;
				needCalcLink = true;
				connector2.AddLink(this);
				OnConnectorChanged(new EventArgs());
			}
		}

		[Browsable(false)]
		internal bool NeedCalcLink
		{
			get
			{
				return needCalcLink;
			}
			set
			{
				needCalcLink = value;
			}
		}

		public abstract override Point Location
		{	
			get; set;
		}

		public abstract override Size Size
		{
			get;
		}

		public abstract LineElement[] Lines
		{
			get;
		}

		[Browsable(false)]
		public abstract Point Point1
		{
			get;
		}

		[Browsable(false)]
		public abstract Point Point2
		{
			get;
		}

		public virtual LineCap StartCap
		{
			get
			{
				return startCap;
			}
			set
			{
				startCap = value;
			}
		}

		public virtual LineCap EndCap
		{
			get
			{
				return endCap;
			}
			set
			{
				endCap = value;
			}
		}

        public virtual bool LineTypeArrow
        {
            get
            {
                return linetypearrow;
            }
            set
            {
                linetypearrow = value;
            }
        }

		internal abstract void CalcLink();

		public override Rectangle GetRectangle()
		{
			//if (needCalcLink) CalcLink();

			return base.GetRectangle();
		}


		#region Events
		[field: NonSerialized]
		public event EventHandler ConnectorChanged;

		protected virtual void OnConnectorChanged(EventArgs e)
		{
			if (ConnectorChanged != null)
				ConnectorChanged(this, e);
		}
		#endregion

	}
}

