using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
	/// <summary>
	/// This class control the elements collection and visualization.
	/// </summary>
	[Serializable]
	public class Document: IDeserializationCallback 
	{
        //Método Dibujo de Elementos
        private OpcionDibujo metododibujo = OpcionDibujo.FixedSize;
       
		//Draw properties
		private SmoothingMode smoothingMode = SmoothingMode.HighQuality;
		private PixelOffsetMode pixelOffsetMode = PixelOffsetMode.Default;
		private CompositingQuality compositingQuality = CompositingQuality.AssumeLinear;

		//Action
		private DesignerAction action = DesignerAction.Select;
		private ElementType elementType = ElementType.RectangleNode;
		private LinkType linkType = LinkType.RightAngle;
       
		// Element Collection
		public ElementCollection elements = new ElementCollection();
        
        //BoundaryNode List (Type 1)
        public List<BoundaryConditionNode> BoundaryConditionNode_List = new List<BoundaryConditionNode>();

        //SplitterNode List (Type 2)
        public List<SplitterNode> SplitterNode_List = new List<SplitterNode>();

        //PressureDropNode List (Type 3)
        public List<PressureDropNode> PressureDropNode_List = new List<PressureDropNode>();

        //PumpNode List (Type 4)
        public List<PumpNode> PumpNode_List = new List<PumpNode>();

        //MixerNode List (Type 5)
        public List<MixerNode> MixerNode_List = new List<MixerNode>();

        //ReactorNode List (Type 6)
        public List<ReactorNode> ReactorNode_List = new List<ReactorNode>();

        //FeedWaterHeaterNode List (Type 7)
        public List<FeedWaterHeaterNode> FeedWaterHeaterNode_List = new List<FeedWaterHeaterNode>();

        //CondenserNode List (Type 8)
        public List<CondenserNode> CondenserNode_List = new List<CondenserNode>();    

        //TurbinaNode List (Type 9)
        public List<TurbinaNode> TurbinaNode_List = new List<TurbinaNode>();

        //TurbineWithoutExhaustLossesNode List (Type 10)
        public List<TurbineWithoutExhaustLossesNode> TurbineWithoutExhaustLossesNode_List = new List<TurbineWithoutExhaustLossesNode>();

        //BoundaryNode List (Type 11)
        public List<AuxiliarTurbineNode> AuxiliarTurbineNode_List = new List<AuxiliarTurbineNode>();

        //BoundaryNode List (Type 12)
        //public List<BoundaryConditionNode> BoundaryConditionNode_List = new List<BoundaryConditionNode>();

        //MoistureSeparationNode List (Type 13)
        public List<MoistureSeparationNode> MoistureSeparationNode_List = new List<MoistureSeparationNode>();

        //MoistureReheaterNode List (Type 14)
        public List<MoistureReheaterNode> MoistureReheaterNode_List = new List<MoistureReheaterNode>();

        //OffGasCondenserNode List (Type 15)
        public List<OffGasCondenserNode> OffGasCondenserNode_List = new List<OffGasCondenserNode>();

        //DrainageCoolerNode List (Type 16)
        public List<DrainageCoolerNode> DrainageCoolerNode_List = new List<DrainageCoolerNode>();

        //DeSuperHeaterNode List (Type 17)
        public List<DeSuperHeaterNode> DeSuperHeaterNode_List = new List<DeSuperHeaterNode>();

        //DeaeratorNode List (Type 18)
        public List<DeaeratorNode> DeaeratorNode_List = new List<DeaeratorNode>();

        //ValveNode List (Type 19)
        public List<ValveNode> ValveNode_List = new List<ValveNode>();

        //FixedEnthalpySplitterNode List (Type 20)
        public List<FixedEnthalpySplitterNode> FixedEnthalpySplitterNode_List = new List<FixedEnthalpySplitterNode>();

        //FlashTankNode List (Type 21)
        public List<FlashTankNode> FlashTankNode_List = new List<FlashTankNode>();

        //HeatExchangerNode List (Type 22)
        public List<HeatExchangerNode> HeatExchangerNode_List = new List<HeatExchangerNode>();
        
        //Element Number
        public int ElementNumber = 0;

        //Connection Number
        public int ConnectionNumber = 1;

        // Selections Collections
        public ElementCollection selectedElements = new ElementCollection();
		public ElementCollection selectedNodes = new ElementCollection();      

		//Document Size
		private Point location = new Point(100, 100);
		private Size size = new Size(0, 0);
		private Size windowSize = new Size(0, 0);

		//Zoom
		public float zoom = 1.0f;

        //Pan
        public int _panX = 0;
        public int _panY = 0;
        public Point lastPoint;

        //Grid
        public Size gridSize = new Size(10, 10);
        public Boolean gridView = false;
        public Boolean gridType = true;
        public Color gridColor = Color.FromName("control");
        public Boolean gridHash = true;
        public Int32 gridanchura = 1;
        public bool snapToGrid = true;

		//Events
		private bool canFireEvents = true;

        //Link 
        public BaseLinkElement lnk;

        //Arrows in Connections
        public bool arrowInConnections = true;

        //Arrows Fill Color
        public Color arrowColor = new Color();

        //Lines Border Color 
        public Color linesBorderColor = new Color();

        //Arrows Border Color 
        public Color arrowsBorderColor = new Color();

        //Fill with color the connections arrows
        public bool fillArrowsWithColor = true;
        
        //Arrow Width
        public int ArrowWith = 5;

        //Arrow Angle
        public float ArrowAngle = 45;

        //Arrow Border Width
        public float arrowsBorderWidth;

        //Line Border Width
        public float linesBorderWidth;

        //Arrow at the begining and at the connection end
        public bool arrowsAtBeginingAndAtEnd = false;

        public Document(){

            //Arrows Fill Color
            arrowColor = Color.Black;

            //Lines Border Color 
            linesBorderColor = Color.Black;

            //Arrows Border Color 
            arrowsBorderColor = Color.Black;
        }

		#region Add Methods
		public void AddElement(BaseElement el)
		{
            el.baseElementNumber = ElementNumber;
            elements.Add(el);          

            //BoundaryConditionNode_List (Type 1)
            if (el is BoundaryConditionNode)
            {
                BoundaryConditionNode BoundaryConditionNode = new BoundaryConditionNode();
                BoundaryConditionNode.baseElementNumber = el.baseElementNumber;
                BoundaryConditionNode_List.Add(BoundaryConditionNode);
            }

            //SplitterNode_List (Type_2)
            if (el is SplitterNode)
            {
                SplitterNode SplitterNode = new SplitterNode();
                SplitterNode.baseElementNumber = el.baseElementNumber;
                SplitterNode_List.Add(SplitterNode);
            }

            //PressureDropNode_List (Type_3)
            if (el is PressureDropNode)
            {
                PressureDropNode PressureDropNode = new PressureDropNode();
                PressureDropNode.baseElementNumber = el.baseElementNumber;
                PressureDropNode_List.Add(PressureDropNode);
            }

            //PumpNode_List (Type_4)
            if (el is PumpNode)
            {
                PumpNode PumpNode = new PumpNode();
                PumpNode.baseElementNumber = el.baseElementNumber;
                PumpNode_List.Add(PumpNode);
            }

            //MixerNode_List (Type_5)
            if (el is MixerNode)
            {
                MixerNode MixerNode = new MixerNode();
                MixerNode.baseElementNumber = el.baseElementNumber;
                MixerNode_List.Add(MixerNode);
            }

            //ReactorNode_List (Type_6)
            if (el is ReactorNode)
            {
                ReactorNode ReactorNode = new ReactorNode();
                ReactorNode.baseElementNumber = el.baseElementNumber;
                ReactorNode_List.Add(ReactorNode);
            }

            //FeedWaterHeaterNode_List (Type_7)
            if (el is FeedWaterHeaterNode)
            {
                FeedWaterHeaterNode FeedWaterHeaterNode = new FeedWaterHeaterNode();
                FeedWaterHeaterNode.baseElementNumber = el.baseElementNumber;
                FeedWaterHeaterNode_List.Add(FeedWaterHeaterNode);
            }

            //CondenserNode_List (Type_8)
            if (el is CondenserNode)
            {
                CondenserNode CondenserNode = new CondenserNode();
                CondenserNode.baseElementNumber = el.baseElementNumber;
                CondenserNode_List.Add(CondenserNode);
            }
                        
            //TurbineNode_List (Type_9)
            if (el is TurbinaNode)
            {
                TurbinaNode TurbinaElmentNode = new TurbinaNode();
                TurbinaElmentNode.baseElementNumber = el.baseElementNumber;
                TurbinaNode_List.Add(TurbinaElmentNode);
            }

            //TurbineNode_List (Type_10)
            if (el is TurbineWithoutExhaustLossesNode)
            {
                TurbineWithoutExhaustLossesNode TurbineWithoutExhaustLossesNode = new TurbineWithoutExhaustLossesNode();
                TurbineWithoutExhaustLossesNode.baseElementNumber = el.baseElementNumber;
                TurbineWithoutExhaustLossesNode_List.Add(TurbineWithoutExhaustLossesNode);
            }

            //TurbineNode_List (Type_11)
            if (el is AuxiliarTurbineNode)
            {
                AuxiliarTurbineNode AuxiliarTurbineNode = new AuxiliarTurbineNode();
                AuxiliarTurbineNode.baseElementNumber = el.baseElementNumber;
                AuxiliarTurbineNode_List.Add(AuxiliarTurbineNode);
            }

            //MoistureSeparationNode_List (Type_13)
            if (el is MoistureSeparationNode)
            {
                MoistureSeparationNode MoistureSeparationNode = new MoistureSeparationNode();
                MoistureSeparationNode.baseElementNumber = el.baseElementNumber;
                MoistureSeparationNode_List.Add(MoistureSeparationNode);
            }

            //MoistureReheaterNode_List (Type_14)
            if (el is MoistureReheaterNode)
            {
                MoistureReheaterNode MoistureReheaterNode = new MoistureReheaterNode();
                MoistureReheaterNode.baseElementNumber = el.baseElementNumber;
                MoistureReheaterNode_List.Add(MoistureReheaterNode);
            }

            //OffGasCondenserNode_List (Type_15)
            if (el is OffGasCondenserNode)
            {
                OffGasCondenserNode OffGasCondenserNode = new OffGasCondenserNode();
                OffGasCondenserNode.baseElementNumber = el.baseElementNumber;
                OffGasCondenserNode_List.Add(OffGasCondenserNode);
            }

            //DrainageCoolerNode_List (Type_16)
            if (el is DrainageCoolerNode)
            {
                DrainageCoolerNode DrainageCoolerNode = new DrainageCoolerNode();
                DrainageCoolerNode.baseElementNumber = el.baseElementNumber;
                DrainageCoolerNode_List.Add(DrainageCoolerNode);
            }

            //DeSuperHeaterNode_List (Type_17)
            if (el is DeSuperHeaterNode)
            {
                DeSuperHeaterNode DeSuperHeaterNode = new DeSuperHeaterNode();
                DeSuperHeaterNode.baseElementNumber = el.baseElementNumber;
                DeSuperHeaterNode_List.Add(DeSuperHeaterNode);
            }

            //DeaeratorNode_List (Type_18)
            if (el is DeaeratorNode)
            {
                DeaeratorNode DeaeratorNode = new DeaeratorNode();
                DeaeratorNode.baseElementNumber = el.baseElementNumber;
                DeaeratorNode_List.Add(DeaeratorNode);
            }

            //ValveNode_List (Type_19)
            if (el is ValveNode)
            {
                ValveNode ValveNode = new ValveNode();
                ValveNode.baseElementNumber = el.baseElementNumber;
                ValveNode_List.Add(ValveNode);
            }

            //FixedEnthalpySplitterNode_List (Type_20)
            if (el is FixedEnthalpySplitterNode)
            {
                FixedEnthalpySplitterNode FixedEnthalpySplitterNode = new FixedEnthalpySplitterNode();
                FixedEnthalpySplitterNode.baseElementNumber = el.baseElementNumber;
                FixedEnthalpySplitterNode_List.Add(FixedEnthalpySplitterNode);
            }

            //FlashTankNode_List (Type_21)
            if (el is FlashTankNode)
            {
                FlashTankNode FlashTankNode = new FlashTankNode();
                FlashTankNode.baseElementNumber = el.baseElementNumber;
                FlashTankNode_List.Add(FlashTankNode);
            }

            //HeatExchangerNode_List (Type_22)
            if (el is HeatExchangerNode)
            {
                HeatExchangerNode HeatExchangerNode = new HeatExchangerNode();
                HeatExchangerNode.baseElementNumber = el.baseElementNumber;
                HeatExchangerNode_List.Add(HeatExchangerNode);
            }

            el.AppearanceChanged +=new EventHandler(element_AppearanceChanged);
			OnAppearancePropertyChanged(new EventArgs());
		}

		public void AddElements(ElementCollection els)
		{
			AddElements(els.GetArray());
		}

		public void AddElements(BaseElement[] els)
		{
			elements.EnabledCalc = false;
			foreach (BaseElement el in els)
			{
				this.AddElement(el);
			}
			elements.EnabledCalc = true;
		}

		internal bool CanAddLink(ConnectorElement connStart, ConnectorElement connEnd)
		{
			return ((connStart != connEnd) && (connStart.ParentElement != connEnd.ParentElement));
		}

		public BaseLinkElement AddLink(ConnectorElement connStart, ConnectorElement connEnd)
		{
			if (CanAddLink(connStart, connEnd))
			{				
				if (linkType == LinkType.Straight)
					lnk = new StraightLinkElement(connStart, connEnd, arrowInConnections, arrowColor, fillArrowsWithColor, ArrowWith, ArrowAngle, linesBorderColor, arrowsBorderColor, arrowsBorderWidth, linesBorderWidth, arrowsAtBeginingAndAtEnd);
				else // (linkType == LinkType.RightAngle)
					lnk = new RightAngleLinkElement(connStart, connEnd, arrowInConnections, arrowColor, fillArrowsWithColor, ArrowWith, ArrowAngle, linesBorderColor, arrowsBorderColor, arrowsBorderWidth, linesBorderWidth, arrowsAtBeginingAndAtEnd);
                                
                elements.Add(lnk);
				lnk.AppearanceChanged +=new EventHandler(element_AppearanceChanged);
				OnAppearancePropertyChanged(new EventArgs());

                //Assigned the connection start element number
                lnk.ConnectionStartElementNumber = connStart.ParentElement.baseElementNumber;

                //Assigned the connection end element number
                lnk.ConnectionEndElementNumber= connEnd.ParentElement.baseElementNumber;

                //Assigned the connection number
                lnk.baseConnectionNumber = ConnectionNumber;
                             

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is BoundaryConditionNode)
                {
                    if ((BoundaryConditionNode_List != null) && (BoundaryConditionNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START TurbineNode 
                        for (int i = 0; i < BoundaryConditionNode_List.Count; i++)
                        {
                            if (BoundaryConditionNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a BoundaryConditionNode, with an element number: " + Convert.ToString(BoundaryConditionNode_List[i].baseElementNumber));

                                for (int j = 0; j < BoundaryConditionNode_List[i].connects.Length; j++)
                                {
                                    if (BoundaryConditionNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        BoundaryConditionNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("BoundaryConditionNode nº:" + BoundaryConditionNode_List[i].baseElementNumber + " START Left Red connection " + BoundaryConditionNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + BoundaryConditionNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("BoundaryConditionNode nº:" + BoundaryConditionNode_List[i].baseElementNumber + " START Right Green connection " + BoundaryConditionNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + BoundaryConditionNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is BoundaryConditionNode)
                {
                    if ((BoundaryConditionNode_List != null) && (BoundaryConditionNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < BoundaryConditionNode_List.Count; i++)
                        {
                            if (BoundaryConditionNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a Turbine Node, with an element number: " + Convert.ToString(BoundaryConditionNode_List[i].baseElementNumber));

                                for (int j = 0; j < BoundaryConditionNode_List[i].connects.Length; j++)
                                {
                                    if (BoundaryConditionNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        BoundaryConditionNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Boundary ConditionNode_List nº:" + BoundaryConditionNode_List[i].baseElementNumber + " END Left Red connection " + BoundaryConditionNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + BoundaryConditionNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Boundary ConditionNode_List nº:" + BoundaryConditionNode_List[i].baseElementNumber + " END Right Green connection " + BoundaryConditionNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + BoundaryConditionNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is SplitterNode)
                {
                    if ((SplitterNode_List != null) && (SplitterNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START SplitterNode 
                        for (int i = 0; i < SplitterNode_List.Count; i++)
                        {
                            if (SplitterNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a SplitterNode, with an element number: " + Convert.ToString(SplitterNode_List[i].baseElementNumber));

                                for (int j = 0; j < SplitterNode_List[i].connects.Length; j++)
                                {
                                    if (SplitterNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        SplitterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + SplitterNode_List[i].baseElementNumber + " START Left Red connection " + SplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + SplitterNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + SplitterNode_List[i].baseElementNumber + " START Right Top Green connection " + SplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + SplitterNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + SplitterNode_List[i].baseElementNumber + " START Right Bottom Green connection " + SplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + SplitterNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is SplitterNode)
                {
                    if ((SplitterNode_List != null) && (SplitterNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END SplitterNode
                        for (int i = 0; i < SplitterNode_List.Count; i++)
                        {
                            if (SplitterNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a SplitterNode, with an element number: " + Convert.ToString(SplitterNode_List[i].baseElementNumber));

                                for (int j = 0; j < SplitterNode_List[i].connects.Length; j++)
                                {
                                    if (SplitterNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        SplitterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + SplitterNode_List[i].baseElementNumber + " END Left Red connection " + SplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + SplitterNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + SplitterNode_List[i].baseElementNumber + " END Right Top Green connection " + SplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + SplitterNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + SplitterNode_List[i].baseElementNumber + " END Right Bottom Green connection " + SplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + SplitterNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is PressureDropNode)
                {
                    if ((PressureDropNode_List != null) && (PressureDropNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START TurbineNode 
                        for (int i = 0; i < PressureDropNode_List.Count; i++)
                        {
                            if (PressureDropNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a PressureDropNode, with an element number: " + Convert.ToString(PressureDropNode_List[i].baseElementNumber));

                                for (int j = 0; j < PressureDropNode_List[i].connects.Length; j++)
                                {
                                    if (PressureDropNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        PressureDropNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("PressureDropNode nº:" + PressureDropNode_List[i].baseElementNumber + " START Left Red connection " + PressureDropNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + PressureDropNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("PressureDropNode nº:" + PressureDropNode_List[i].baseElementNumber + " START Right Green connection " + PressureDropNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + PressureDropNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is PressureDropNode)
                {
                    if ((PressureDropNode_List != null) && (PressureDropNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END PressureDropNode 
                        for (int i = 0; i < PressureDropNode_List.Count; i++)
                        {
                            if (PressureDropNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a PressureDropNode, with an element number: " + Convert.ToString(PressureDropNode_List[i].baseElementNumber));

                                for (int j = 0; j < PressureDropNode_List[i].connects.Length; j++)
                                {
                                    if (PressureDropNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        PressureDropNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("PressureDropNode nº:" + PressureDropNode_List[i].baseElementNumber + " END Left Red connection " + PressureDropNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + PressureDropNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("PressureDropNode nº:" + PressureDropNode_List[i].baseElementNumber + " END Right Green connection " + PressureDropNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + PressureDropNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is PumpNode)
                {
                    if ((PumpNode_List != null) && (PumpNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START PumpNode 
                        for (int i = 0; i < PumpNode_List.Count; i++)
                        {
                            if (PumpNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a PumpNode, with an element number: " + Convert.ToString(PumpNode_List[i].baseElementNumber));

                                for (int j = 0; j < PumpNode_List[i].connects.Length; j++)
                                {
                                    if (PumpNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        PumpNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("PumpNode nº:" + PumpNode_List[i].baseElementNumber + " START Left Red connection " + PumpNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + PumpNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("PumpNode nº:" + PumpNode_List[i].baseElementNumber + " START Right Green connection " + PumpNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + PumpNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is PumpNode)
                {
                    if ((PumpNode_List != null) && (PumpNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < PumpNode_List.Count; i++)
                        {
                            if (PumpNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a PumpNode, with an element number: " + Convert.ToString(PumpNode_List[i].baseElementNumber));

                                for (int j = 0; j < PumpNode_List[i].connects.Length; j++)
                                {
                                    if (PumpNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        PumpNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("PumpNode nº:" + PumpNode_List[i].baseElementNumber + " END Left Red connection " + PumpNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + PumpNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("PumpNode nº:" + PumpNode_List[i].baseElementNumber + " END Right Green connection " + PumpNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + PumpNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is MixerNode)
                {
                    if ((MixerNode_List != null) && (MixerNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START TurbineNode 
                        for (int i = 0; i < MixerNode_List.Count; i++)
                        {
                            if (MixerNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a MixerNode, with an element number: " + Convert.ToString(MixerNode_List[i].baseElementNumber));

                                for (int j = 0; j < MixerNode_List[i].connects.Length; j++)
                                {
                                    if (MixerNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        MixerNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("MixerNode nº:" + MixerNode_List[i].baseElementNumber + " START Left Red connection " + MixerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MixerNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("MixerNode nº:" + MixerNode_List[i].baseElementNumber + " START Right Green connection " + MixerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MixerNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("MixerNode nº:" + MixerNode_List[i].baseElementNumber + " START Right Green connection " + MixerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MixerNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is MixerNode)
                {
                    if ((MixerNode_List != null) && (MixerNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < MixerNode_List.Count; i++)
                        {
                            if (MixerNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a MixerNode, with an element number: " + Convert.ToString(MixerNode_List[i].baseElementNumber));

                                for (int j = 0; j < MixerNode_List[i].connects.Length; j++)
                                {
                                    if (MixerNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        MixerNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Mixer Node nº:" + MixerNode_List[i].baseElementNumber + " END Left Red connection " + MixerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MixerNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Mixer Node nº:" + MixerNode_List[i].baseElementNumber + " END Right Green connection " + MixerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MixerNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Mixer Node nº:" + MixerNode_List[i].baseElementNumber + " END Right Green connection " + MixerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MixerNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is ReactorNode)
                {
                    if ((ReactorNode_List != null) && (ReactorNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START TurbineNode 
                        for (int i = 0; i < ReactorNode_List.Count; i++)
                        {
                            if (ReactorNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a Turbine Node, with an element number: " + Convert.ToString(TurbinaNode_List[i].baseElementNumber));

                                for (int j = 0; j < ReactorNode_List[i].connects.Length; j++)
                                {
                                    if (ReactorNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        ReactorNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Reactor Node nº:" + ReactorNode_List[i].baseElementNumber + " START Left Red connection " + ReactorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + ReactorNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Reactor Node nº:" + ReactorNode_List[i].baseElementNumber + " START Right Green connection " + ReactorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + ReactorNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is ReactorNode)
                {
                    if ((ReactorNode_List != null) && (ReactorNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < ReactorNode_List.Count; i++)
                        {
                            if (ReactorNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a Turbine Node, with an element number: " + Convert.ToString(TurbinaNode_List[i].baseElementNumber));

                                for (int j = 0; j < ReactorNode_List[i].connects.Length; j++)
                                {
                                    if (ReactorNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        ReactorNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Reactor Node nº:" + ReactorNode_List[i].baseElementNumber + " END Left Red connection " + ReactorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + ReactorNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Reactor Node nº:" + ReactorNode_List[i].baseElementNumber + " END Right Green connection " + ReactorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + ReactorNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is FeedWaterHeaterNode)
                {
                    if ((FeedWaterHeaterNode_List != null) && (FeedWaterHeaterNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START TurbineNode 
                        for (int i = 0; i < FeedWaterHeaterNode_List.Count; i++)
                        {
                            if (FeedWaterHeaterNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a Turbine Node, with an element number: " + Convert.ToString(TurbinaNode_List[i].baseElementNumber));

                                for (int j = 0; j < FeedWaterHeaterNode_List[i].connects.Length; j++)
                                {
                                    if (FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        FeedWaterHeaterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Right-Bottom (Red)
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("FeedWaterHeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " START Right-Bottom (Red) connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Top-Center (Blue)
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("FeedWaterHeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " START Top-Center (Blue) connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Top-Left (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("FeedWaterHeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " START Top-Left (Green) connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Yellow)
                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("FeedWaterHeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " START Right-Top (Yellow) connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Bottom (Black)
                                        else if (connStart.ConnectorElementName == "N5")
                                        {
                                            MessageBox.Show("FeedWaterHeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " START Left-Bottom (Black) connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is FeedWaterHeaterNode)
                {
                    if ((FeedWaterHeaterNode_List != null) && (FeedWaterHeaterNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < FeedWaterHeaterNode_List.Count; i++)
                        {
                            if (FeedWaterHeaterNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a Turbine Node, with an element number: " + Convert.ToString(TurbinaNode_List[i].baseElementNumber));

                                for (int j = 0; j < FeedWaterHeaterNode_List[i].connects.Length; j++)
                                {
                                    if (FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        FeedWaterHeaterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Right-Bottom (Red)
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("FeedWater HeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " END Left Red connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Top-Center (Blue)
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("FeedWater HeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " END Right Green connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Top-Left (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("FeedWaterHeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " END Right Green connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Yellow)
                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("FeedWaterHeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " END Right Green connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Bottom (Black)
                                        else if (connStart.ConnectorElementName == "N5")
                                        {
                                            MessageBox.Show("FeedWaterHeaterNode nº:" + FeedWaterHeaterNode_List[i].baseElementNumber + " END Right Green connection " + FeedWaterHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FeedWaterHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is CondenserNode)
                {
                    if ((CondenserNode_List != null) && (CondenserNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START CondenserNode
                        for (int i = 0; i < CondenserNode_List.Count; i++)
                        {
                            if (CondenserNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a Condenser Node, with an element number: " + Convert.ToString(CondenserNode_List[i].baseElementNumber));

                                for (int j = 0; j < CondenserNode_List[i].connects.Length; j++)
                                {
                                    if (CondenserNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        CondenserNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("CondenserNode nº:" + CondenserNode_List[i].baseElementNumber + " START Left Red connection " + CondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + CondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Down (Blue)
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("CondenserNode nº:" + CondenserNode_List[i].baseElementNumber + " START Right Green connection " + CondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + CondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("CondenserNode nº:" + CondenserNode_List[i].baseElementNumber + " START Right Green connection " + CondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + CondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Down (Yellow)
                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("CondenserNode nº:" + CondenserNode_List[i].baseElementNumber + " START Right Green connection " + CondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + CondenserNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is CondenserNode)
                {
                    if ((CondenserNode_List != null) && (CondenserNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END CondenserNode
                        for (int i = 0; i < CondenserNode_List.Count; i++)
                        {
                            if (CondenserNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a Turbine Node, with an element number: " + Convert.ToString(TurbinaNode_List[i].baseElementNumber));

                                for (int j = 0; j < CondenserNode_List[i].connects.Length; j++)
                                {
                                    if (CondenserNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        CondenserNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Condenser Node nº:" + CondenserNode_List[i].baseElementNumber + " END Left Red connection " + CondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + CondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Down (Blue)
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Condenser Node nº:" + CondenserNode_List[i].baseElementNumber + " END Right Green connection " + CondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + CondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Condenser Node nº:" + CondenserNode_List[i].baseElementNumber + " END Right Green connection " + CondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + CondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Down (Yellow)
                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Condenser Node nº:" + CondenserNode_List[i].baseElementNumber + " END Right Green connection " + CondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + CondenserNode_List[i].connects[j].linkNumber);
                                        }  
                                        
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is TurbinaNode)
                {
                    if ((TurbinaNode_List != null) && (TurbinaNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START TurbineNode 
                        for (int i = 0; i < TurbinaNode_List.Count; i++)
                        {
                            if (TurbinaNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a Turbine Node, with an element number: " + Convert.ToString(TurbinaNode_List[i].baseElementNumber));

                                for (int j = 0; j < TurbinaNode_List[i].connects.Length; j++)
                                {
                                    if (TurbinaNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        TurbinaNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Turbine Node nº:" + TurbinaNode_List[i].baseElementNumber + " START Left Red connection " + TurbinaNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + TurbinaNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Turbine Node nº:" + TurbinaNode_List[i].baseElementNumber + " START Right Green connection " + TurbinaNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + TurbinaNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is TurbinaNode)
                {
                    if ((TurbinaNode_List != null) && (TurbinaNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < TurbinaNode_List.Count; i++)
                        {
                            if (TurbinaNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a Turbine Node, with an element number: " + Convert.ToString(TurbinaNode_List[i].baseElementNumber));

                                for (int j = 0; j < TurbinaNode_List[i].connects.Length; j++)
                                {
                                    if (TurbinaNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        TurbinaNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Turbine Node nº:" + TurbinaNode_List[i].baseElementNumber + " END Left Red connection " + TurbinaNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + TurbinaNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Turbine Node nº:" + TurbinaNode_List[i].baseElementNumber + " END Right Green connection " + TurbinaNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + TurbinaNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is TurbineWithoutExhaustLossesNode)
                {
                    if ((TurbineWithoutExhaustLossesNode_List != null) && (TurbineWithoutExhaustLossesNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START TurbineWithoutExhaustLossesNode
                        for (int i = 0; i < TurbineWithoutExhaustLossesNode_List.Count; i++)
                        {
                            if (TurbineWithoutExhaustLossesNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a TurbineWithoutExhaustLossesNode, with an element number: " + Convert.ToString(TurbineWithoutExhaustLossesNode_List[i].baseElementNumber));

                                for (int j = 0; j < TurbineWithoutExhaustLossesNode_List[i].connects.Length; j++)
                                {
                                    if (TurbineWithoutExhaustLossesNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        TurbineWithoutExhaustLossesNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Turbine Without Exhaust Losses Node nº:" + TurbineWithoutExhaustLossesNode_List[i].baseElementNumber + " START Left Red connection " + TurbineWithoutExhaustLossesNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + TurbineWithoutExhaustLossesNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Turbine Without Exhaust Losses Node nº:" + TurbineWithoutExhaustLossesNode_List[i].baseElementNumber + " START Right Green connection " + TurbineWithoutExhaustLossesNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + TurbineWithoutExhaustLossesNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is TurbineWithoutExhaustLossesNode)
                {
                    if ((TurbineWithoutExhaustLossesNode_List != null) && (TurbineWithoutExhaustLossesNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < TurbineWithoutExhaustLossesNode_List.Count; i++)
                        {
                            if (TurbineWithoutExhaustLossesNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a TurbineWithoutExhaustLossesNode, with an element number: " + Convert.ToString(TurbineWithoutExhaustLossesNode_List[i].baseElementNumber));

                                for (int j = 0; j < TurbineWithoutExhaustLossesNode_List[i].connects.Length; j++)
                                {
                                    if (TurbineWithoutExhaustLossesNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)

                                    {
                                        TurbineWithoutExhaustLossesNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Turbine Without Exhaust Losses Node nº:" + TurbineWithoutExhaustLossesNode_List[i].baseElementNumber + " END Left Red connection " + TurbineWithoutExhaustLossesNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + TurbineWithoutExhaustLossesNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Turbine Without Exhaust Losses Node nº:" + TurbineWithoutExhaustLossesNode_List[i].baseElementNumber + " END Right Green connection " + TurbineWithoutExhaustLossesNode_List[i].baseElementNumber + " END Left Red connection " + TurbineWithoutExhaustLossesNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + TurbineWithoutExhaustLossesNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is AuxiliarTurbineNode)
                {
                    if ((AuxiliarTurbineNode_List != null) && (AuxiliarTurbineNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START AuxiliarTurbineNode_List
                        for (int i = 0; i < AuxiliarTurbineNode_List.Count; i++)
                        {
                            if (AuxiliarTurbineNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a AuxiliarTurbineNode_List, with an element number: " + Convert.ToString(AuxiliarTurbineNode_List[i].baseElementNumber));

                                for (int j = 0; j < AuxiliarTurbineNode_List[i].connects.Length; j++)
                                {
                                    if (AuxiliarTurbineNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        AuxiliarTurbineNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Auxiliar Turbine Node nº:" + AuxiliarTurbineNode_List[i].baseElementNumber + " START Left Red connection " + AuxiliarTurbineNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + AuxiliarTurbineNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Auxiliar Turbine Node nº:" + AuxiliarTurbineNode_List[i].baseElementNumber + " START Right Green connection " + AuxiliarTurbineNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + AuxiliarTurbineNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is AuxiliarTurbineNode)
                {
                    if ((AuxiliarTurbineNode_List != null) && (AuxiliarTurbineNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < AuxiliarTurbineNode_List.Count; i++)
                        {
                            if (AuxiliarTurbineNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a AuxiliarTurbineNode, with an element number: " + Convert.ToString(AuxiliarTurbineNode_List[i].baseElementNumber));

                                for (int j = 0; j < AuxiliarTurbineNode_List[i].connects.Length; j++)
                                {
                                    if (AuxiliarTurbineNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        AuxiliarTurbineNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Auxiliar Turbine Node nº:" + AuxiliarTurbineNode_List[i].baseElementNumber + " END Left Red connection " + AuxiliarTurbineNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + AuxiliarTurbineNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Auxiliar Turbine Node nº:" + AuxiliarTurbineNode_List[i].baseElementNumber + " END Right Green connection " + AuxiliarTurbineNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + AuxiliarTurbineNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is MoistureSeparationNode)
                {
                    if ((MoistureSeparationNode_List != null) && (MoistureSeparationNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START MoistureSeparationNode_List
                        for (int i = 0; i < MoistureSeparationNode_List.Count; i++)
                        {
                            if (MoistureSeparationNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a MoistureSeparationNode_List, with an element number: " + Convert.ToString(MoistureSeparationNode_List[i].baseElementNumber));

                                for (int j = 0; j < MoistureSeparationNode_List[i].connects.Length; j++)
                                {
                                    if (MoistureSeparationNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        MoistureSeparationNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Moisture Separation Node_List nº:" + MoistureSeparationNode_List[i].baseElementNumber + " START Left Red connection " + MoistureSeparationNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Moisture Separation Node_List nº:" + MoistureSeparationNode_List[i].baseElementNumber + " START Right Green connection " + MoistureSeparationNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Moisture Separation Node_List nº:" + MoistureSeparationNode_List[i].baseElementNumber + " START Right Green connection " + MoistureSeparationNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is MoistureSeparationNode)
                {
                    if ((MoistureSeparationNode_List != null) && (MoistureSeparationNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < MoistureSeparationNode_List.Count; i++)
                        {
                            if (MoistureSeparationNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a MoistureSeparationNode, with an element number: " + Convert.ToString(MoistureSeparationNode_List[i].baseElementNumber));

                                for (int j = 0; j < MoistureSeparationNode_List[i].connects.Length; j++)
                                {
                                    if (MoistureSeparationNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        MoistureSeparationNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Moisture Separation Node nº:" + MoistureSeparationNode_List[i].baseElementNumber + " END Left Red connection " + MoistureSeparationNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connEnd.ConnectorElementName == "N2")
                                        { 
                                            MessageBox.Show("Moisture Separation Node nº:" + MoistureSeparationNode_List[i].baseElementNumber + " END Right Green connection " + MoistureSeparationNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Moisture Separation Node nº:" + MoistureSeparationNode_List[i].baseElementNumber + " END Right Green connection " + MoistureSeparationNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is MoistureReheaterNode)
                {
                    if ((MoistureReheaterNode_List != null) && (MoistureReheaterNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START MoistureSeparationNode_List
                        for (int i = 0; i < MoistureReheaterNode_List.Count; i++)
                        {
                            if (MoistureReheaterNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a MoistureSeparationNode_List, with an element number: " + Convert.ToString(MoistureSeparationNode_List[i].baseElementNumber));

                                for (int j = 0; j < MoistureReheaterNode_List[i].connects.Length; j++)
                                {
                                    if (MoistureReheaterNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        MoistureReheaterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Moisture Reheater Node_List nº:" + MoistureReheaterNode_List[i].baseElementNumber + " START Left Red connection " + MoistureReheaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureReheaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Bottom (Blue)
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Moisture Reheater Node_List nº:" + MoistureReheaterNode_List[i].baseElementNumber + " START Right Green connection " + MoistureReheaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureReheaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Moisture Reheater Node_List nº:" + MoistureReheaterNode_List[i].baseElementNumber + " START Right Green connection " + MoistureReheaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureReheaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Moisture Reheater Node_List nº:" + MoistureReheaterNode_List[i].baseElementNumber + " START Right Green connection " + MoistureReheaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureReheaterNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is MoistureReheaterNode)
                {
                    if ((MoistureReheaterNode_List != null) && (MoistureReheaterNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END MoistureReheaterNode 
                        for (int i = 0; i < MoistureReheaterNode_List.Count; i++)
                        {
                            if (MoistureReheaterNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a MoistureReheaterNode, with an element number: " + Convert.ToString(MoistureReheaterNode_List[i].baseElementNumber));

                                for (int j = 0; j < MoistureReheaterNode_List[i].connects.Length; j++)
                                {
                                    if (MoistureReheaterNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        MoistureReheaterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Moisture Reheater Node nº:" + MoistureReheaterNode_List[i].baseElementNumber + " END Left Red connection " + MoistureReheaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureReheaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Bottom (Blue)
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Moisture Reheater Node nº:" + MoistureReheaterNode_List[i].baseElementNumber + " END Right Green connection " + MoistureReheaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureReheaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Moisture Reheater Node nº:" + MoistureReheaterNode_List[i].baseElementNumber + " END Right Green connection " + MoistureReheaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureReheaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connEnd.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Moisture Reheater Node nº:" + MoistureReheaterNode_List[i].baseElementNumber + " END Right Green connection " + MoistureReheaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureReheaterNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is OffGasCondenserNode)
                {
                    if ((OffGasCondenserNode_List != null) && (OffGasCondenserNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START OffGasCondenserNode_List
                        for (int i = 0; i < OffGasCondenserNode_List.Count; i++)
                        {
                            if (OffGasCondenserNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a OffGasCondenserNode_List, with an element number: " + Convert.ToString(OffGasCondenserNode_List[i].baseElementNumber));

                                for (int j = 0; j < OffGasCondenserNode_List[i].connects.Length; j++)
                                {
                                    if (OffGasCondenserNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        OffGasCondenserNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Center-Top (Red)
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Off-Gas Condenser Node nº:" + OffGasCondenserNode_List[i].baseElementNumber + " START Left Red connection " + OffGasCondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + OffGasCondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Blue)
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Off-Gas Condenser Node nº:" + OffGasCondenserNode_List[i].baseElementNumber + " START Right Green connection " + OffGasCondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + OffGasCondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Center-Bottom (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Off-Gas Condenser Node nº:" + OffGasCondenserNode_List[i].baseElementNumber + " START Right Green connection " + OffGasCondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + OffGasCondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Off-Gas Condenser Node nº:" + OffGasCondenserNode_List[i].baseElementNumber + " START Right Green connection " + OffGasCondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + OffGasCondenserNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is OffGasCondenserNode)
                {
                    if ((OffGasCondenserNode_List != null) && (OffGasCondenserNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END MoistureReheaterNode 
                        for (int i = 0; i < OffGasCondenserNode_List.Count; i++)
                        {
                            if (OffGasCondenserNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a OffGasCondenserNode, with an element number: " + Convert.ToString(OffGasCondenserNode_List[i].baseElementNumber));

                                for (int j = 0; j < OffGasCondenserNode_List[i].connects.Length; j++)
                                {
                                    if (OffGasCondenserNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        OffGasCondenserNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Off-Gas Condenser Node nº:" + OffGasCondenserNode_List[i].baseElementNumber + " END Left Red connection " + OffGasCondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + OffGasCondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Bottom (Blue)
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Off-Gas Condenser Node nº:" + OffGasCondenserNode_List[i].baseElementNumber + " END Right Green connection " + OffGasCondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + OffGasCondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Off-Gas Condenser Node nº:" + OffGasCondenserNode_List[i].baseElementNumber + " END Right Green connection " + OffGasCondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + OffGasCondenserNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connEnd.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Off-Gas Condenser Node nº:" + OffGasCondenserNode_List[i].baseElementNumber + " END Right Green connection " + OffGasCondenserNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + OffGasCondenserNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is DrainageCoolerNode)
                {
                    if ((DrainageCoolerNode_List != null) && (DrainageCoolerNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START DrainageCoolerNode_List
                        for (int i = 0; i < DrainageCoolerNode_List.Count; i++)
                        {
                            if (DrainageCoolerNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a DrainageCoolerNode_List, with an element number: " + Convert.ToString(DrainageCoolerNode_List[i].baseElementNumber));

                                for (int j = 0; j < DrainageCoolerNode_List[i].connects.Length; j++)
                                {
                                    if (DrainageCoolerNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        DrainageCoolerNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Center-Top (Red)
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Drainage Cooler Node nº:" + DrainageCoolerNode_List[i].baseElementNumber + " START Left Red connection " + DrainageCoolerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DrainageCoolerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Blue)
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Drainage Cooler Node nº:" + DrainageCoolerNode_List[i].baseElementNumber + " START Right Green connection " + DrainageCoolerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DrainageCoolerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Center-Bottom (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Drainage Cooler Node nº:" + DrainageCoolerNode_List[i].baseElementNumber + " START Right Green connection " + DrainageCoolerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DrainageCoolerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Drainage Cooler Node nº:" + DrainageCoolerNode_List[i].baseElementNumber + " START Right Green connection " + DrainageCoolerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DrainageCoolerNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is DrainageCoolerNode)
                {
                    if ((DrainageCoolerNode_List != null) && (DrainageCoolerNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END DrainageCoolerNode
                        for (int i = 0; i < DrainageCoolerNode_List.Count; i++)
                        {
                            if (DrainageCoolerNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a DrainageCoolerNode_List, with an element number: " + Convert.ToString(DrainageCoolerNode_List[i].baseElementNumber));

                                for (int j = 0; j < DrainageCoolerNode_List[i].connects.Length; j++)
                                {
                                    if (DrainageCoolerNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        DrainageCoolerNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Drainage Cooler Node nº:" + DrainageCoolerNode_List[i].baseElementNumber + " END Left Red connection " + DrainageCoolerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DrainageCoolerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Bottom (Blue)
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Drainage Cooler Node nº:" + DrainageCoolerNode_List[i].baseElementNumber + " END Right Green connection " + DrainageCoolerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DrainageCoolerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Drainage Cooler Node nº:" + DrainageCoolerNode_List[i].baseElementNumber + " END Right Green connection " + DrainageCoolerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DrainageCoolerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connEnd.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Drainage Cooler Node nº:" + DrainageCoolerNode_List[i].baseElementNumber + " END Right Green connection " + DrainageCoolerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DrainageCoolerNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is DeSuperHeaterNode)
                {
                    if ((DeSuperHeaterNode_List != null) && (DeSuperHeaterNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START DeSuperHeaterNode_List
                        for (int i = 0; i < DeSuperHeaterNode_List.Count; i++)
                        {
                            if (DeSuperHeaterNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a DeSuperHeaterNode_List, with an element number: " + Convert.ToString(DeSuperHeaterNode_List[i].baseElementNumber));

                                for (int j = 0; j < DeSuperHeaterNode_List[i].connects.Length; j++)
                                {
                                    if (DeSuperHeaterNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        DeSuperHeaterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Center-Top (Red)
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("DeSuperHeater Node nº:" + DeSuperHeaterNode_List[i].baseElementNumber + " START Left Red connection " + DeSuperHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeSuperHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Center-Left (Blue)
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("DeSuperHeater Node nº:" + DeSuperHeaterNode_List[i].baseElementNumber + " START Right Green connection " + DeSuperHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeSuperHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Center-Right (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("DeSuperHeater Node nº:" + DeSuperHeaterNode_List[i].baseElementNumber + " START Right Green connection " + DeSuperHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeSuperHeaterNode_List[i].connects[j].linkNumber);
                                        }                                       

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is DeSuperHeaterNode)
                {
                    if ((DeSuperHeaterNode_List != null) && (DeSuperHeaterNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END DeSuperHeaterNode
                        for (int i = 0; i < DeSuperHeaterNode_List.Count; i++)
                        {
                            if (DeSuperHeaterNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a DeSuperHeaterNode_List, with an element number: " + Convert.ToString(DeSuperHeaterNode_List[i].baseElementNumber));

                                for (int j = 0; j < DeSuperHeaterNode_List[i].connects.Length; j++)
                                {
                                    if (DeSuperHeaterNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        DeSuperHeaterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Center-Top (Red)
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("DeSuperHeater Node nº:" + DeSuperHeaterNode_List[i].baseElementNumber + " END Left Red connection " + DeSuperHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeSuperHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Center-Left (Blue)
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("DeSuperHeater Node nº:" + DeSuperHeaterNode_List[i].baseElementNumber + " END Right Green connection " + DeSuperHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeSuperHeaterNode_List[i].connects[j].linkNumber);
                                        }
                                        //Center-Right (Green)
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("DeSuperHeater Node nº:" + DeSuperHeaterNode_List[i].baseElementNumber + " END Right Green connection " + DeSuperHeaterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeSuperHeaterNode_List[i].connects[j].linkNumber);
                                        }                                      

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is DeaeratorNode)
                {
                    if ((DeaeratorNode_List != null) && (DeaeratorNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START DeaeratorNode_List
                        for (int i = 0; i < DeaeratorNode_List.Count; i++)
                        {
                            if (DeaeratorNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a DeaeratorNode_List, with an element number: " + Convert.ToString(DeaeratorNode_List[i].baseElementNumber));

                                for (int j = 0; j < DeaeratorNode_List[i].connects.Length; j++)
                                {
                                    if (DeaeratorNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        DeaeratorNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " START Left Red connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }
                                    
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " START Right Green connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }
                                        
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " START Right Green connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }

                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " START Right Green connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }

                                        else if (connStart.ConnectorElementName == "N5")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " START Right Green connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is DeaeratorNode)
                {
                    if ((DeaeratorNode_List != null) && (DeaeratorNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END DeSuperHeaterNode
                        for (int i = 0; i < DeaeratorNode_List.Count; i++)
                        {
                            if (DeaeratorNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a DeaeratorNode_List, with an element number: " + Convert.ToString(DeaeratorNode_List[i].baseElementNumber));

                                for (int j = 0; j < DeaeratorNode_List[i].connects.Length; j++)
                                {
                                    if (DeaeratorNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        DeaeratorNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                       
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " END Left Red connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }
                                       
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " END Right Green connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }
                                        
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " END Right Green connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }

                                        else if (connEnd.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " END Right Green connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }

                                        else if (connEnd.ConnectorElementName == "N5")
                                        {
                                            MessageBox.Show("Deaerator Node nº:" + DeaeratorNode_List[i].baseElementNumber + " END Right Green connection " + DeaeratorNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + DeaeratorNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is ValveNode)
                {
                    if ((ValveNode_List != null) && (ValveNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START ValveNode 
                        for (int i = 0; i < ValveNode_List.Count; i++)
                        {
                            if (ValveNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a ValveNode, with an element number: " + Convert.ToString(ValveNode_List[i].baseElementNumber));

                                for (int j = 0; j < ValveNode_List[i].connects.Length; j++)
                                {
                                    if (ValveNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        ValveNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("ValveNode_List nº:" + ValveNode_List[i].baseElementNumber + " START Left Red connection " + ValveNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + ValveNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("ValveNode_List nº:" + ValveNode_List[i].baseElementNumber + " START Right Green connection " + ValveNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + ValveNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is ValveNode)
                {
                    if ((ValveNode_List != null) && (ValveNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END TurbineNode 
                        for (int i = 0; i < ValveNode_List.Count; i++)
                        {
                            if (ValveNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a Valve Node, with an element number: " + Convert.ToString(ValveNode_List[i].baseElementNumber));

                                for (int j = 0; j < ValveNode_List[i].connects.Length; j++)
                                {
                                    if (ValveNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        ValveNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("ValveNode_List nº:" + ValveNode_List[i].baseElementNumber + " END Left Red connection " + ValveNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + ValveNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("ValveNode_List nº:" + ValveNode_List[i].baseElementNumber + " END Right Green connection " + ValveNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + ValveNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is FixedEnthalpySplitterNode)
                {
                    if ((FixedEnthalpySplitterNode_List != null) && (FixedEnthalpySplitterNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START FixedEnthalpySplitterNode
                        for (int i = 0; i < FixedEnthalpySplitterNode_List.Count; i++)
                        {
                            if (FixedEnthalpySplitterNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a FixedEnthalpySplitterNode, with an element number: " + Convert.ToString(FixedEnthalpySplitterNode_List[i].baseElementNumber));

                                for (int j = 0; j < FixedEnthalpySplitterNode_List[i].connects.Length; j++)
                                {
                                    if (FixedEnthalpySplitterNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        FixedEnthalpySplitterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("FixedEnthalpy Splitter Node nº:" + FixedEnthalpySplitterNode_List[i].baseElementNumber + " START Left Red connection " + FixedEnthalpySplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FixedEnthalpySplitterNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("FixedEnthalpy Splitter Node nº:" + FixedEnthalpySplitterNode_List[i].baseElementNumber + " START Right Top Green connection " + FixedEnthalpySplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FixedEnthalpySplitterNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("FixedEnthalpy Splitter Node nº:" + FixedEnthalpySplitterNode_List[i].baseElementNumber + " START Right Bottom Green connection " + FixedEnthalpySplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FixedEnthalpySplitterNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is FixedEnthalpySplitterNode)
                {
                    if ((FixedEnthalpySplitterNode_List != null) && (FixedEnthalpySplitterNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END SplitterNode
                        for (int i = 0; i < FixedEnthalpySplitterNode_List.Count; i++)
                        {
                            if (FixedEnthalpySplitterNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a FixedEnthalpySplitterNode, with an element number: " + Convert.ToString(FixedEnthalpySplitterNode_List[i].baseElementNumber));

                                for (int j = 0; j < FixedEnthalpySplitterNode_List[i].connects.Length; j++)
                                {
                                    if (FixedEnthalpySplitterNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        FixedEnthalpySplitterNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + FixedEnthalpySplitterNode_List[i].baseElementNumber + " END Left Red connection " + FixedEnthalpySplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FixedEnthalpySplitterNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + FixedEnthalpySplitterNode_List[i].baseElementNumber + " END Right Top Green connection " + FixedEnthalpySplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FixedEnthalpySplitterNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("SplitterNode nº:" + FixedEnthalpySplitterNode_List[i].baseElementNumber + " END Right Bottom Green connection " + FixedEnthalpySplitterNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FixedEnthalpySplitterNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is FlashTankNode)
                {
                    if ((FlashTankNode_List != null) && (FlashTankNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START FlashTankNode_List
                        for (int i = 0; i < FlashTankNode_List.Count; i++)
                        {
                            if (FlashTankNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a FlashTankNode_List, with an element number: " + Convert.ToString(FlashTankNode_List[i].baseElementNumber));

                                for (int j = 0; j < FlashTankNode_List[i].connects.Length; j++)
                                {
                                    if (FlashTankNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        FlashTankNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("FlashTank Node nº:" + FlashTankNode_List[i].baseElementNumber + " START Left Red connection " + FlashTankNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FlashTankNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("FlashTank Node nº:" + FlashTankNode_List[i].baseElementNumber + " START Right Top Green connection " + FlashTankNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FlashTankNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("FlashTank Node nº:" + FlashTankNode_List[i].baseElementNumber + " START Right Bottom Green connection " + FlashTankNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FlashTankNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is FlashTankNode)
                {
                    if ((FlashTankNode_List != null) && (FlashTankNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END SplitterNode
                        for (int i = 0; i < FlashTankNode_List.Count; i++)
                        {
                            if (FlashTankNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a FlashTankNode, with an element number: " + Convert.ToString(FlashTankNode_List[i].baseElementNumber));

                                for (int j = 0; j < FlashTankNode_List[i].connects.Length; j++)
                                {
                                    if (FlashTankNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        FlashTankNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("FlashTank Node nº:" + FlashTankNode_List[i].baseElementNumber + " END Left Red connection " + FlashTankNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FlashTankNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("FlashTank Node nº:" + FlashTankNode_List[i].baseElementNumber + " END Right Top Green connection " + FlashTankNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FlashTankNode_List[i].connects[j].linkNumber);
                                        }
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("FlashTank Node nº:" + FlashTankNode_List[i].baseElementNumber + " END Right Bottom Green connection " + FlashTankNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + FlashTankNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------------------------------
                if (connStart.ParentElement is HeatExchangerNode)
                {
                    if ((HeatExchangerNode_List != null) && (HeatExchangerNode_List.Count > 0))
                    {
                        // First, find with the START Connection the START HeatExchangerNode_List
                        for (int i = 0; i < HeatExchangerNode_List.Count; i++)
                        {
                            if (HeatExchangerNode_List[i].baseElementNumber == connStart.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an START connection and the parent element is a HeatExchangerNode_List, with an element number: " + Convert.ToString(HeatExchangerNode_List[i].baseElementNumber));

                                for (int j = 0; j < HeatExchangerNode_List[i].connects.Length; j++)
                                {
                                    if (HeatExchangerNode_List[i].connects[j].ConnectorElementName == connStart.ConnectorElementName)
                                    {
                                        HeatExchangerNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connStart.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("HeatExchangerNode_List nº:" + HeatExchangerNode_List[i].baseElementNumber + " START Left Red connection " + HeatExchangerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + HeatExchangerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Bottom (Blue)
                                        else if (connStart.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("HeatExchangerNode_List nº:" + HeatExchangerNode_List[i].baseElementNumber + " START Right Green connection " + HeatExchangerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + HeatExchangerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connStart.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("HeatExchangerNode_List nº:" + HeatExchangerNode_List[i].baseElementNumber + " START Right Green connection " + HeatExchangerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + HeatExchangerNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connStart.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("HeatExchangerNode_List nº:" + HeatExchangerNode_List[i].baseElementNumber + " START Right Green connection " + HeatExchangerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + HeatExchangerNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (connEnd.ParentElement is HeatExchangerNode)
                {
                    if ((HeatExchangerNode_List != null) && (HeatExchangerNode_List.Count > 0))
                    {
                        // First, find with the END Connection the END HeatExchangerNode 
                        for (int i = 0; i < HeatExchangerNode_List.Count; i++)
                        {
                            if (HeatExchangerNode_List[i].baseElementNumber == connEnd.ParentElement.baseElementNumber)
                            {
                                //MessageBox.Show("The clicked connection is an END connection and the parent element is a HeatExchangerNode, with an element number: " + Convert.ToString(HeatExchangerNode_List[i].baseElementNumber));

                                for (int j = 0; j < HeatExchangerNode_List[i].connects.Length; j++)
                                {
                                    if (HeatExchangerNode_List[i].connects[j].ConnectorElementName == connEnd.ConnectorElementName)
                                    {
                                        HeatExchangerNode_List[i].connects[j].linkNumber = ConnectionNumber;

                                        //Left-Top (Red)
                                        if (connEnd.ConnectorElementName == "N1")
                                        {
                                            MessageBox.Show("HeatExchanger Node nº:" + HeatExchangerNode_List[i].baseElementNumber + " END Left Red connection " + HeatExchangerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }
                                        //Left-Bottom (Blue)
                                        else if (connEnd.ConnectorElementName == "N2")
                                        {
                                            MessageBox.Show("HeatExchanger Node nº:" + HeatExchangerNode_List[i].baseElementNumber + " END Right Green connection " + HeatExchangerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Top (Green)
                                        else if (connEnd.ConnectorElementName == "N3")
                                        {
                                            MessageBox.Show("HeatExchanger Node nº:" + HeatExchangerNode_List[i].baseElementNumber + " END Right Green connection " + HeatExchangerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }
                                        //Right-Bottom (Yellow)
                                        else if (connEnd.ConnectorElementName == "N4")
                                        {
                                            MessageBox.Show("HeatExchanger Node nº:" + HeatExchangerNode_List[i].baseElementNumber + " END Right Green connection " + HeatExchangerNode_List[i].connects[j].ConnectorElementName + ". Link number nº:" + MoistureSeparationNode_List[i].connects[j].linkNumber);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                return lnk;
			}
			else
			{
				return null;
			}
		}
		#endregion

		#region Delete Methods

		public void DeleteElement(BaseElement el)
		{
			if ((el != null) && !(el is ConnectorElement))
			{
				//Delete link
				if (el is BaseLinkElement)
				{
					BaseLinkElement lnk = (BaseLinkElement) el;
					DeleteLink(lnk);
					return;
				}

				//Delete node
				if (el is NodeElement)
				{
					NodeElement conn = ((NodeElement) el);
					foreach (ConnectorElement elconn in conn.Connectors)
					{
						BaseLinkElement lnk;
						for (int i = elconn.Links.Count - 1; i>=0; i--)
						{
							lnk = (BaseLinkElement) elconn.Links[i];
							DeleteLink(lnk);
						}
					}
					
					if (selectedNodes.Contains(el))
						selectedNodes.Remove(el);

                    if (el is TurbinaNode)
                    {
                        for (int j=0;j<TurbinaNode_List.Count;j++)
                        {
                            if (TurbinaNode_List[j].baseElementNumber == el.baseElementNumber)
                            {
                                TurbinaNode_List.RemoveAt(j);
                            }
                        }
                    }

                    if (el is BoundaryConditionNode)
                    {
                        for (int j = 0; j < BoundaryConditionNode_List.Count; j++)
                        {
                            if (BoundaryConditionNode_List[j].baseElementNumber == el.baseElementNumber)
                            {
                                BoundaryConditionNode_List.RemoveAt(j);
                            }
                        }
                    }
                }

				if (SelectedElements.Contains(el))
					selectedElements.Remove(el);

				elements.Remove(el);
				
				OnAppearancePropertyChanged(new EventArgs());	
			}
			el = null;
		}

		public void DeleteElement(Point p)
		{
			BaseElement selectedElement = FindElement(p);
			DeleteElement(selectedElement);
		}

		public void DeleteSelectedElements()
		{
			selectedElements.EnabledCalc = false;
			selectedNodes.EnabledCalc = false;

			for(int i = selectedElements.Count - 1; i >= 0; i-- )
			{
				DeleteElement(selectedElements[i]);
			}

			selectedElements.EnabledCalc = true;
			selectedNodes.EnabledCalc = true;
		}

		public void DeleteLink(BaseLinkElement lnk)
		{
			if (lnk != null)
			{
				lnk.Connector1.RemoveLink(lnk);
				lnk.Connector2.RemoveLink(lnk);
							
				if (elements.Contains(lnk))
					elements.Remove(lnk);
				if (selectedElements.Contains(lnk))
					selectedElements.Remove(lnk);
				OnAppearancePropertyChanged(new EventArgs());
			}
		}
		#endregion

		#region Select Methods
		public void ClearSelection()
		{
			selectedElements.Clear();
			selectedNodes.Clear();
			OnElementSelection(this, new ElementSelectionEventArgs(selectedElements));
		}

		public void SelectElement(BaseElement el)
		{
			selectedElements.Add(el);
			if (el is NodeElement)
			{
				selectedNodes.Add(el);
			}
			if (canFireEvents)
				OnElementSelection(this, new ElementSelectionEventArgs(selectedElements));
		}

		public void SelectElements(BaseElement[] els)
		{
			selectedElements.EnabledCalc = false;
			selectedNodes.EnabledCalc = false;

			canFireEvents = false;
			
			try
			{
				foreach(BaseElement el in els)
				{
					SelectElement(el);
				}
			}
			finally
			{
				canFireEvents = true;
			}
			selectedElements.EnabledCalc = true;
			selectedNodes.EnabledCalc = true;
			
			OnElementSelection(this, new ElementSelectionEventArgs(selectedElements));
		}

		public void SelectElements(Rectangle selectionRectangle)
		{
			selectedElements.EnabledCalc = false;
			selectedNodes.EnabledCalc = false;
			
			// Add all "hitable" elements
			foreach(BaseElement element in elements)
			{
				if (element is IControllable)
				{
					IController ctrl = ((IControllable)element).GetController();
					if (ctrl.HitTest(selectionRectangle))
					{
						if (!(element is ConnectorElement))
							selectedElements.Add(element);
						
						if (element is NodeElement)
							selectedNodes.Add(element);
					}
				}
			}

			//if the seleciont isn't a expecific link, remove links
			// without 2 elements in selection
			if (selectedElements.Count > 1)
			{
				foreach(BaseElement el in elements)
				{
					BaseLinkElement lnk = el as BaseLinkElement;
					if (lnk == null) continue;
					
					if ((!selectedElements.Contains(lnk.Connector1.ParentElement)) ||
						(!selectedElements.Contains(lnk.Connector2.ParentElement)))
					{
						selectedElements.Remove(lnk);
					}
				}
			}

			selectedElements.EnabledCalc = true;
			selectedNodes.EnabledCalc = true;
			
			OnElementSelection(this, new ElementSelectionEventArgs(selectedElements));
		}

		public void SelectAllElements()
		{
			selectedElements.EnabledCalc = false;
			selectedNodes.EnabledCalc = false;

			foreach(BaseElement element in elements)
			{
				if (!(element is ConnectorElement))
					selectedElements.Add(element);
					
				if (element is NodeElement)
					selectedNodes.Add(element);
			}

			selectedElements.EnabledCalc = true;
			selectedNodes.EnabledCalc = true;
			
		}

		public BaseElement FindElement(Point point)
		{
			BaseElement el;
			if ((elements != null) && (elements.Count > 0))
			{
				// First, find elements
				for(int i = elements.Count - 1; i >=0 ; i--)
				{
					el = elements[i];

					if (el is BaseLinkElement)
						continue;

					//Find element in a Connector array
					if (el is NodeElement)
					{
						NodeElement nel = (NodeElement) el;
						foreach(ConnectorElement cel in nel.Connectors)
						{
							IController ctrl = ((IControllable) cel).GetController();
							if (ctrl.HitTest(point))
								return cel;
						}
					}

					//Find element in a Container Element
					if (el is IContainer)
					{
						BaseElement inner = FindInnerElement((IContainer) el, point);
						if (inner != null)
							return inner;
					}

					//Find element by hit test
					if (el is IControllable)
					{
						IController ctrl = ((IControllable) el).GetController();
						if (ctrl.HitTest(point))
							return el;
					}
				}

				// Then, find links
				for(int i = elements.Count - 1; i >=0 ; i--)
				{
					el = elements[i];

					if (!(el is BaseLinkElement))
						continue;
					
					if (el is IControllable)
					{
						IController ctrl = ((IControllable) el).GetController();
						if (ctrl.HitTest(point))
							return el;
					}
				} 
			}
			return null;
		}

		private BaseElement FindInnerElement(IContainer parent, Point hitPos)
		{
			foreach (BaseElement el in parent.Elements)
			{
				if (el is IContainer)
				{
					BaseElement retEl = FindInnerElement((IContainer)el, hitPos);
					if (retEl != null)
						return retEl;
				}
				
				if (el is IControllable)
				{
					IController ctrl = ((IControllable) el).GetController();

					if (ctrl.HitTest(hitPos))
						return el;
				}
			}
			return null;
		}
		#endregion

		#region Position Methods
		public void MoveUpElement(BaseElement el)
		{
			int i = elements.IndexOf(el);
			if (i != elements.Count - 1)
			{
				elements.ChangeIndex(i, i + 1);
				OnAppearancePropertyChanged(new EventArgs());
			}
		}

		public void MoveDownElement(BaseElement el)
		{
			int i = elements.IndexOf(el);
			if (i != 0)
			{
				elements.ChangeIndex(i, i - 1);
				OnAppearancePropertyChanged(new EventArgs());
			}
		}

		public void BringToFrontElement(BaseElement el)
		{
			int i = elements.IndexOf(el);
			for (int x = i + 1; x <= elements.Count - 1; x++)
			{
				elements.ChangeIndex(i, x);
				i = x;
			}
			OnAppearancePropertyChanged(new EventArgs());
		}

		public void SendToBackElement(BaseElement el)
		{
			int i = elements.IndexOf(el);
			for (int x = i - 1; x >= 0; x--)
			{
				elements.ChangeIndex(i, x);
				i = x;
			}
			OnAppearancePropertyChanged(new EventArgs());
		}
		#endregion

		internal void CalcWindow(bool forceCalc)
		{
			elements.CalcWindow(forceCalc);
			selectedElements.CalcWindow(forceCalc);
			selectedNodes.CalcWindow(forceCalc);
		}

		#region Properties

        public OpcionDibujo ElementDrawingMethod
        {
            get
            {
                return metododibujo;
            }

            set
            {
                metododibujo = value;
            }
        }

        public Int32 GridWidth
        {
            get
            {
                return gridanchura;
            }

            set
            {
                gridanchura = value;

            }
        }
      
        public Boolean GridView
        {
            get
            {
                return gridView;
            }

            set
            {
                gridView = value;            
            }
        }

        public Boolean GridType
        {
            get
            {
                return gridType;
            }

            set
            {
                gridType = value;
            }
        }

        public Boolean GridHash
        {
            get
            {
                return gridHash;
            }

            set
            {
                gridHash = value;
            }
        }

        public Color GridColor
        {
            get
            {
                return gridColor;
            }

            set
            {
                gridColor = value;
            }
        }

		public ElementCollection Elements
		{
			get
			{
				return elements;
			}
		}

		public ElementCollection SelectedElements
		{
			get
			{
				return selectedElements;
			}
		}

		public ElementCollection SelectedNodes
		{
			get
			{
				return selectedNodes;
			}
		}

		public Point Location
		{
			get
			{
				return elements.WindowLocation;
			}
		}

		public Size Size
		{
			get
			{
				return elements.WindowSize;
			}
		}

		internal Size WindowSize
		{
			set
			{
				windowSize = value;
			}
		}

		public SmoothingMode SmoothingMode
		{
			get
			{
				return smoothingMode;
			}
			set
			{
				smoothingMode = value;
				OnAppearancePropertyChanged(new EventArgs());
			}
		}

		public PixelOffsetMode PixelOffsetMode
		{
			get
			{
				return pixelOffsetMode;
			}
			set
			{
				pixelOffsetMode = value;
				OnAppearancePropertyChanged(new EventArgs());
			}
		}

		public CompositingQuality CompositingQuality
		{
			get
			{
				return compositingQuality;
			}
			set
			{
				compositingQuality = value;
				OnAppearancePropertyChanged(new EventArgs());
			}
		}

		public DesignerAction Action
		{
			get
			{
				return action;
			}
			set
			{
				action = value;
				OnPropertyChanged(new EventArgs());
			}
		}

		public float Zoom
		{
			get
			{
				return zoom;
			}
			set
			{
				zoom = value;
				OnPropertyChanged(new EventArgs());
			}
		}    
      
        public ElementType ElementType
		{
			get
			{
				return elementType;
			}
			set
			{
				elementType = value;
				OnPropertyChanged(new EventArgs());
			}
		}

		public LinkType LinkType
		{
			get
			{
				return linkType;
			}
			set
			{
				linkType = value;
				OnPropertyChanged(new EventArgs());
			}
		}

		public Size GridSize
		{
			get
			{
				return gridSize;
			}
			set
			{
				gridSize = value;
				OnAppearancePropertyChanged(new EventArgs());
			}
		}

        public bool SnapToGrid
        {
            get
            {
                return snapToGrid;
            }
            set
            {
                snapToGrid = value;
                OnPropertyChanged(new EventArgs());
            }
        }
		#endregion

		#region Draw Methods

        //Dibuja los Elementos dentro del rectángulo "clippingRegion"
		internal void DrawElements(Graphics g, Rectangle clippingRegion)
		{
			//Draw Links first
			for (int i = 0; i <= elements.Count - 1; i++)
			{
				BaseElement el = elements[i];
				if ((el is BaseLinkElement) && (NeedDrawElement(el, clippingRegion)))
					el.Draw(g);
											
				if (el is ILabelElement)
					((ILabelElement) el).Label.Draw(g);
			}

			//Draw the other elements
			for (int i = 0; i <= elements.Count - 1; i++)
			{
				BaseElement el = elements[i];

				if (!(el is BaseLinkElement) && (NeedDrawElement(el, clippingRegion)))
				{
					if (el is NodeElement)
					{
						NodeElement n = (NodeElement) el;
						n.Draw(g, (action == DesignerAction.Connect));
					}
					else
					{
						el.Draw(g);
					}

					if (el is ILabelElement)
						((ILabelElement) el).Label.Draw(g);
				}
			}
		}

        //Esta función evalua si un Elemento dado es cortado por el Rectángulo de Selección y por tanto si es necesario redibujarlo o no.
        //Esta función recibe dos argumentos:
        //a) Un el Rectángulo de Selección enviado por el Usuario.
        //b) El Elemento que vamos a comprobar si corta el rectángulo "clippingRegion"
		private bool NeedDrawElement(BaseElement el, Rectangle clippingRegion)
		{
			if (!el.Visible) return false;

			Rectangle elRectangle = el.GetUnsignedRectangle();
			elRectangle.Inflate(5, 5);
			return clippingRegion.IntersectsWith(elRectangle);
		}

        //Esta función Dibuja los elementos dentro del rectángulo de selección "clippingRegion"
		internal void DrawSelections(Graphics g, Rectangle clippingRegion)
		{
			for(int i = selectedElements.Count - 1; i >=0 ; i--)
			{
				if (selectedElements[i] is IControllable)
				{
					IController ctrl = ((IControllable) selectedElements[i]).GetController();
					ctrl.DrawSelection(g);

					if (selectedElements[i] is BaseLinkElement)
					{
						BaseLinkElement link = (BaseLinkElement) selectedElements[i];
						ctrl = ((IControllable) link.Connector1).GetController();
						ctrl.DrawSelection(g);

						ctrl = ((IControllable) link.Connector2).GetController();
						ctrl.DrawSelection(g);
					}
				}
			}
		}

        //Función para Dibujar el GRID
		internal void DrawGrid(Graphics g, Rectangle clippingRegion)
		{
//			ControlPaint.DrawGrid(g, clippingRegion, gridSize, Color.LightGray);
            Pen p=new Pen(Color.Red,1);
            Pen p1=new Pen(Color.Blue,1);

            int gridWidth = gridSize.Width;
            int gridHeight = gridSize.Height;
            if (gridWidth == 0 || gridHeight == 0)
                return;
            if (gridWidth < 10)
                gridWidth *= 10 / gridWidth + 1;
            if (gridHeight < 10)
                gridHeight *= 10 / gridHeight + 1;

            if (GridHash == true)
            {
                p = new Pen(new HatchBrush(HatchStyle.LargeGrid | HatchStyle.Percent90, GridColor, Color.Transparent), GridWidth);
            }
            else if (GridHash == false)
            {
                p1 = new Pen(GridColor, GridWidth);
            }
			
			int maxX = location.X + this.Size.Width;
			int maxY = location.Y + this.Size.Height;

			if (windowSize.Width / zoom > maxX)
				maxX = (int)(windowSize.Width / zoom);

			if (windowSize.Height / zoom > maxY)
				maxY = (int)(windowSize.Height / zoom);

            // Create a 1 x 1 bitmap and set the color
            Bitmap pt = new Bitmap(1, 1);
            pt.SetPixel(0, 0, Color.Black);

            if (GridView == false)
            { 
            
            }

            else if (GridView==true)
            {
              if (GridType == true)
              {

                for (int i = 0; i < maxX; i += gridWidth)
                {
                    if (GridHash == true)
                    {
                        g.DrawLine(p, i, 0, i, maxY);

                    }
                    else if (GridHash == false)
                    {
                        g.DrawLine(p1, i, 0, i, maxY);
                    }
                }

                for (int i = 0; i < maxY; i += gridHeight)
                {
                    if (GridHash == true)
                    {
                        g.DrawLine(p, 0, i, maxX, i);
                    }
                    else if (GridHash == false)
                    {
                        g.DrawLine(p1, 0, i, maxX, i);
                    }
                }
              }

              else if (GridType==false)
              {
                for (int i = 0; i < maxX; i += gridWidth)
                {
                    for (int j = 0; j < maxY; j += gridHeight)
                    {
                        g.DrawImageUnscaled(pt, i, j);
                    }
                }
            
              }
            }
			p.Dispose();
		}
		#endregion

		#region Events Raising
		
		// Property Changed
		[field: NonSerialized]
		public event EventHandler PropertyChanged; 

		protected virtual void OnPropertyChanged(EventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);            
		}

		// Appearance Property Changed
		[field: NonSerialized]
		public event EventHandler AppearancePropertyChanged;

		protected virtual void OnAppearancePropertyChanged(EventArgs e)
		{
			OnPropertyChanged(e);

			if (AppearancePropertyChanged != null)
				AppearancePropertyChanged(this, e);
		}

		// Element Property Changed
		[field: NonSerialized]
		public event EventHandler ElementPropertyChanged;

		protected virtual void OnElementPropertyChanged(object sender, EventArgs e)
		{
			if (ElementPropertyChanged != null)
				ElementPropertyChanged(sender, e);
		}

		// Element Selection
		public delegate void ElementSelectionEventHandler(object sender, ElementSelectionEventArgs e);
		
		[field: NonSerialized]
		public event ElementSelectionEventHandler ElementSelection;

		protected virtual void OnElementSelection(object sender, ElementSelectionEventArgs e)
		{
			if (ElementSelection != null)
				ElementSelection(sender, e);
		}
		

		#endregion

		#region Events Handling
		private void RecreateEventsHandlers()
		{
			foreach(BaseElement el in elements)
				el.AppearanceChanged +=new EventHandler(element_AppearanceChanged);			
		}

		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		private void element_AppearanceChanged(object sender, EventArgs e)
		{
			OnElementPropertyChanged(sender, e);
		}
		#endregion
	
		#region IDeserializationCallback Members
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			RecreateEventsHandlers();
		}
		#endregion
	}
}
