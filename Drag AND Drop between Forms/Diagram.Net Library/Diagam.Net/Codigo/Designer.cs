using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

using Drag_AND_Drop_between_Forms;

namespace Dalssoft.DiagramNet
{
	public class Designer : System.Windows.Forms.UserControl
	{
        //Puntero hacia la Aplicación Principal
        public Aplicacion punteroaplicacion;

        private System.ComponentModel.IContainer components;

        public Int32 TipodeEquipo=1;
        //public Image imagen=Image.FromFile("C:\\Users\\luisc\\Desktop\\Bal LUIS COCO2\\Bal LUIS COCO2\\Bal LUIS COCO\\Drag AND Drop between Forms\\Resources\\WATER DROP.jpg");
        public Image imagen0 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Boundary_Condition_Type_1.png");
        public Image imagen1 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Boundary_Condition_Type_1.png");
        public Image imagen2 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Divisor_Type_2.png");
        public Image imagen3 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Pressure_Drop_Type_3.png");
        public Image imagen4 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Pump_Type_4.png");
        public Image imagen5 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Mixer_Type_5.png");
        public Image imagen6 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Boiler_Type_6.png");
        public Image imagen7 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Heat_Exchanger_Type_7.png");
        public Image imagen8 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Condenser_Type_8b.png");
        public Image imagen9 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Turbine_Type_9e.png");
        public Image imagen10 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Turbine_Type_10.png");
        public Image imagen11 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Auxiliar Turbine_Type_11.png");
        //public Image imagen12 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\ Heat_Exchanger_Type_7.png");
        public Image imagen13 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Moisture_Separator_Type_13.png");
        public Image imagen14 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Moisture_ReHeater_Type_14.png");
        public Image imagen15 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\OffGasCondenser_Type_15.png");
        public Image imagen16 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Drainage_Cooler_Type_16.png");
        public Image imagen17 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\DeSuperHeater_Type_17.png");
        public Image imagen18 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Deaerator_Type_18.png");
        public Image imagen19 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Valve_Type_19.png");
        public Image imagen20 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\FixedEnthalpySplitter_Type_20.png");
        public Image imagen21 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Flash_Tank_Type_21.png");
        public Image imagen22 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\Heat_Exchanger_Type_22.png");
        //Supercritical_Equipments
        //public Image imagen23 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\ Heat_Exchanger_Type_7.png");
        //public Image imagen24 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\ Heat_Exchanger_Type_7.png");
        //public Image imagen25 = Image.FromFile("C:\\HeatBalancing_Gitlab\\Drag AND Drop between Forms\\Resources\\Steam_Equipment_Symbols\\Drawing_Equipments\\ Heat_Exchanger_Type_7.png");

        //ORC_Equipments
        
        #region Designer Control Initialization
        //Document
        private Document document = new Document();

        //Método de Dibujar Elementos
        public OpcionDibujo metododibujar = OpcionDibujo.FixedSize;

		// Drag and Drop
		MoveAction moveAction = null;

		// Selection
		BaseElement selectedElement;
		private bool isMultiSelection = false;
		private RectangleElement selectionArea = new RectangleElement(0,0,0,0);
		private IController[] controllers;
		private BaseElement mousePointerElement;
	
		// Resize
		private ResizeAction resizeAction = null;

        // Rotate
        private RotateAction rotateAction = null;

        // Add Element
        private bool isAddSelection = false;
		
		// Link
		private bool isAddLink = false;
		private ConnectorElement connStart;
		private ConnectorElement connEnd;
		private BaseLinkElement linkLine;

		// Label
		private bool isEditLabel = false;
		private LabelElement selectedLabel;
		private System.Windows.Forms.TextBox labelTextBox = new TextBox();
		private EditLabelAction editLabelAction = null;
       
        //Undo
        [NonSerialized]        
        public UndoManager undo = new UndoManager(5);
        public bool changed = false;               
      
        //
        public bool dragdropluis = false;

        //Arrows in Connections
        public bool arrowInConnections = true;

        //Arrows Fill Color
        public Color arrowColor = new Color();

        //Lines Border Color 
        public Color linesBorderColor = new Color();

        //Arrows Border Color 
        public Color arrowsBorderColor = new Color();

        //Lines Border Color 
        public Color SelectlinesBorderColor = new Color();

        //Arrows Border Color 
        public Color SelectarrowsBorderColor = new Color();

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

        public Designer()
		{             
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

			// This change control to not flick
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			
			// Selection Area Properties
			selectionArea.Opacity = 40;
			selectionArea.FillColor1 = SystemColors.Control;
			selectionArea.FillColor2 = Color.Empty;
			selectionArea.BorderColor = SystemColors.Control;

			// Link Line Properties
			//linkLine.BorderColor = Color.FromArgb(127, Color.DarkGray);
			//linkLine.BorderWidth = 4;

			// Label Edit
			labelTextBox.BorderStyle = BorderStyle.FixedSingle;
			labelTextBox.Multiline = true;
			labelTextBox.Hide();
			this.Controls.Add(labelTextBox);

			//EventsHandlers
			RecreateEventsHandlers();

            //Arrows Fill Color
            arrowColor = Color.Black;

            //Lines Border Color 
            linesBorderColor = Color.Black;

            //Arrows Border Color 
            arrowsBorderColor = Color.Black;

            //Line Border Width
            arrowsBorderWidth = 1;

            //Line Border Width
            linesBorderWidth = 1;

            //Arrow at the begining and at the connection end
            arrowsAtBeginingAndAtEnd = false;
    }
    #endregion

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // Designer
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Designer";
            this.ResumeLayout(false);

		}
		#endregion

		public new void Invalidate()
		{
            if (document.Elements.Count > 0)
            {
                for (int i = 0; i <= document.Elements.Count - 1; i++)
                {
                    BaseElement el = document.Elements[i];

                    Invalidate(el);

                    if (el is ILabelElement)
                    {
                        Invalidate(((ILabelElement)el).Label);
                    }
                }
            }
            else
            {
                base.Invalidate();
            }

           // if ((moveAction != null) && (moveAction.IsMoving))
           // {
           //     this.AutoScrollMinSize = new Size((int)((document.Location.X + document.Size.Width + 10) * document.Zoom), (int)((document.Location.Y + document.Size.Height + 10) * document.Zoom));
           //     base.Invalidate();
           // }
		}

		private void Invalidate(BaseElement el)
		{
			this.Invalidate(el, false);
		}

        public void InvalidateBase()
        {
            base.Invalidate();
        }

		private void Invalidate(BaseElement el, bool force)
		{
			if (el == null) return;

			if ((force) || (el.IsInvalidated))
			{
				Rectangle invalidateRec = Goc2Gsc(el.invalidateRec);
				invalidateRec.Inflate(10, 10);
				base.Invalidate(invalidateRec);
			}			
		}      
  
        #region Events Overrides
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			GraphicsContainer gc;
            Matrix mtx = new Matrix();
            g.PageUnit = GraphicsUnit.Pixel;

			Point scrollPoint = this.AutoScrollPosition;
           
            this.AutoScrollMinSize = new Size((int)((document.Location.X + document.Size.Width + document._panX + 10) * document.Zoom), (int)((document.Location.Y + document.Size.Height + document._panY + 10) * document.Zoom));

            g.TranslateTransform(scrollPoint.X, scrollPoint.Y);

            //Grid Snap
            //g.DrawEllipse(Pens.Teal, pos.X-5, pos.Y-5, 10, 10);                                         

            //Zoom
            mtx = g.Transform;
			gc = g.BeginContainer();
			
			g.SmoothingMode = document.SmoothingMode;
			g.PixelOffsetMode = document.PixelOffsetMode;
			g.CompositingQuality = document.CompositingQuality;

            g.ScaleTransform(document.Zoom, document.Zoom);

            Rectangle clipRectangle = Gsc2Goc(e.ClipRectangle);
         
            document.DrawGrid(g, clipRectangle);

			document.DrawElements(g, clipRectangle);           
            

            if (!((resizeAction != null) && (resizeAction.IsResizing)))
				document.DrawSelections(g, e.ClipRectangle);

			if ((isMultiSelection) || (isAddSelection))
				DrawSelectionRectangle(g);
 
			if (isAddLink)
			{
				linkLine.CalcLink();
				linkLine.Draw(g);
			}
			if ((resizeAction != null) && ( !((moveAction != null) && (moveAction.IsMoving))))
				resizeAction.DrawResizeCorner(g);

			if (mousePointerElement != null)
			{
				if (mousePointerElement is IControllable)
				{
					IController ctrl = ((IControllable) mousePointerElement).GetController();
					ctrl.DrawSelection(g);
				}
			}

			g.EndContainer(gc);
			g.Transform = mtx;

			base.OnPaint(e);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground (e);

			Graphics g = e.Graphics;
			GraphicsContainer gc;
			Matrix mtx;
			g.PageUnit = GraphicsUnit.Pixel;
			mtx = g.Transform;
			gc = g.BeginContainer();
			
			Rectangle clipRectangle = Gsc2Goc(e.ClipRectangle);
			
			//document.DrawGrid(g, clipRectangle);

			g.EndContainer(gc);
			g.Transform = mtx;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			//Delete element
			if (e.KeyCode == Keys.Delete)
			{
				DeleteSelectedElements();
				EndGeneralAction();
				base.Invalidate();
			}

			//Undo
			if (e.Control && e.KeyCode == Keys.Z)
			{
				if (undo.CanUndo)
					Undo();
			}

			//Copy
			if ((e.Control) && (e.KeyCode == Keys.C))
			{
				this.Copy();
			}

			//Paste
			if ((e.Control) && (e.KeyCode == Keys.V))
			{
				this.Paste();
			}

			//Cut
			if ((e.Control) && (e.KeyCode == Keys.X))
			{
				this.Cut();
			}

			base.OnKeyDown (e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			document.WindowSize = this.Size;
		}
//-----------------------------------------------------------------------------------------------------------------------------------
		#region Mouse Events
                  
        protected override void OnMouseDown(MouseEventArgs e)
		{
			Point mousePoint;            

			//ShowSelectionCorner((document.Action==DesignerAction.Select));

			switch (document.Action)
			{
                case DesignerAction.Pan:

                    Document.SelectAllElements();

                    if (e.Button == MouseButtons.Left)
                    {
                        mousePoint = Gsc2Goc(new Point(e.X, e.Y));

                        moveAction = new MoveAction();
                        MoveAction.OnElementMovingDelegate onElementMovingDelegate = new Dalssoft.DiagramNet.MoveAction.OnElementMovingDelegate(OnElementMoving);
                        moveAction.Start(mousePoint, document, onElementMovingDelegate);
                    }
                    base.Invalidate();

                    break;

                // SELECT
                case DesignerAction.Connect:

				case DesignerAction.Select:
                  
                    if (e.Button == MouseButtons.Left)
					{
						mousePoint = Gsc2Goc(new Point(e.X, e.Y));
						
						//Verify resize action
						StartResizeElement(mousePoint);
                        
						if ((resizeAction != null) && (resizeAction.IsResizing)) break;

						//Verify label editing
						if (isEditLabel)
						{
							EndEditLabel();
						}

						// Search element by click
						selectedElement = document.FindElement(mousePoint);	
						
						if (selectedElement != null)
						{
							//Events
							ElementMouseEventArgs eventMouseDownArg = new ElementMouseEventArgs(selectedElement, e.X, e.Y);
							OnElementMouseDown(eventMouseDownArg);

                            // Element selected
                            if (selectedElement is ConnectorElement)
                            {
                                StartAddLink((ConnectorElement)selectedElement, mousePoint);
                                                               
                                selectedElement = null;
                            }
                            
                            else
                            {
                                StartSelectElements(selectedElement, mousePoint);
                            }
						}
						else
						{
							// If click is on neutral area, clear selection
							document.ClearSelection();
							Point p = Gsc2Goc(new Point(e.X, e.Y));;
							isMultiSelection = true;
							selectionArea.Visible = true;
							selectionArea.Location = p;
							selectionArea.Size = new Size(0, 0);
							
							if (resizeAction != null)
								resizeAction.ShowResizeCorner(false);
						}
						base.Invalidate();
					}
					
                    break;

				// ADD
				case DesignerAction.Add:

					if (e.Button == MouseButtons.Left)
					{
						mousePoint = Gsc2Goc(new Point(e.X, e.Y));
						StartAddElement(mousePoint);
                        AddUndo();
                    }
					break;

				// DELETE
				case DesignerAction.Delete:
					if (e.Button == MouseButtons.Left)
					{
						mousePoint = Gsc2Goc(new Point(e.X, e.Y));
						DeleteElement(mousePoint);
                        AddUndo();
                    }					
					break;

                case DesignerAction.DataInput:
                    if (e.Button == MouseButtons.Left)
                    {
                        mousePoint = Gsc2Goc(new Point(e.X, e.Y));
                        
                        // Search element by click
                        selectedElement = document.FindElement(mousePoint);

                        if (selectedElement != null)
                        {
                            //Events
                            ElementMouseEventArgs eventMouseDownArg = new ElementMouseEventArgs(selectedElement, e.X, e.Y);
                            OnElementMouseDown(eventMouseDownArg);

                            //Double-click sobre un Equipo Boundary_Condition (Type 1)
                            if ((e.Clicks == 2) && (selectedElement is BoundaryConditionNode))
                            {
                                punteroaplicacion.numequipos++;

                                Condcontorno luisboundaryCondition = new Condcontorno(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisboundaryCondition.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.BoundaryConditionNode_List.Count; i++)
                                {
                                    if (document.BoundaryConditionNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                    //    if ((selectedElement.baseElementNumber != 1) && (document.BoundaryConditionNode_List[i].connects[1].linkNumber == 0))
                                    //    {
                                    //        luisboundaryCondition.textBox7.Text = Convert.ToString(document.BoundaryConditionNode_List[i].connects[0].linkNumber + 1);
                                    //        luisboundaryCondition.textBox8.Text = Convert.ToString(document.BoundaryConditionNode_List[i].connects[0].linkNumber + 2);
                                    //    }
                                    //    else
                                    //    {
                                            luisboundaryCondition.textBox7.Text = Convert.ToString(document.BoundaryConditionNode_List[i].connects[0].linkNumber);
                                            luisboundaryCondition.textBox8.Text = Convert.ToString(document.BoundaryConditionNode_List[i].connects[1].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 1 equipo Boundary Condition
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 1)
                                                {
                                                    //Multiplicamos por 0.4536 para convertir de Lb/sg a kg/sg
                                                    luisboundaryCondition.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1 * 0.4536);                                                 
                                                    // Multiplicamos por (6.8947572 / 100) para convertir de psia a bar
                                                    luisboundaryCondition.textBox2.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD2 * (6.8947572 / 100));                                                  
                                                    // Multiplicamos por 2.32600 para convertir de BTU/Lb
                                                    luisboundaryCondition.textBox3.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD3 * 2.326009);
                                                  
                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                                }
                                            }
                                        }
                                    }
                                }

                                luisboundaryCondition.ShowDialog();
                            }

                            //Double-click sobre un Equipo Splitter (Type 2)
                            else if ((e.Clicks == 2) && (selectedElement is SplitterNode))
                            {
                                punteroaplicacion.numequipos++;

                                Divisor luisdivisor = new Divisor(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisdivisor.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.SplitterNode_List.Count; i++)
                                {
                                    if (document.SplitterNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.SplitterNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisdivisor.textBox7.Text = Convert.ToString(document.SplitterNode_List[i].connects[0].linkNumber + 1);
                                        //    luisdivisor.textBox8.Text = Convert.ToString(document.SplitterNode_List[i].connects[1].linkNumber + 2);
                                        //    luisdivisor.textBox4.Text = Convert.ToString(document.SplitterNode_List[i].connects[2].linkNumber + 3);
                                        //}
                                        //else
                                        //{
                                            luisdivisor.textBox7.Text = Convert.ToString(document.SplitterNode_List[i].connects[0].linkNumber);
                                            luisdivisor.textBox8.Text = Convert.ToString(document.SplitterNode_List[i].connects[1].linkNumber);
                                            luisdivisor.textBox4.Text = Convert.ToString(document.SplitterNode_List[i].connects[2].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 2 equipo Splitter
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 2)
                                                {
                                                    //Multiplicamos por 0.4536 para convertir de Lb/sg a kg/sg
                                                    luisdivisor.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1 * 0.4536);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                                }
                                            }
                                        }
                                    }
                                }

                                luisdivisor.ShowDialog();
                            }

                            //Double-click sobre un Equipo Pressure_Drop (Type 3)
                            else if ((e.Clicks == 2) && (selectedElement is PressureDropNode))
                            {
                                punteroaplicacion.numequipos++;

                                Perdidacarga pressureDrop = new Perdidacarga(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                pressureDrop.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.PressureDropNode_List.Count; i++)
                                {
                                    if (document.PressureDropNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.PressureDropNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    pressureDrop.textBox10.Text = Convert.ToString(document.PressureDropNode_List[i].connects[0].linkNumber + 1);
                                        //    pressureDrop.textBox11.Text = Convert.ToString(document.PressureDropNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                            pressureDrop.textBox10.Text = Convert.ToString(document.PressureDropNode_List[i].connects[0].linkNumber);
                                            pressureDrop.textBox11.Text = Convert.ToString(document.PressureDropNode_List[i].connects[1].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 1 equipo Boundary Condition
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 3)
                                                {
                                                    //aD1
                                                    pressureDrop.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                                }
                                            }
                                        }
                                    }
                                }

                                pressureDrop.ShowDialog();
                            }

                            //Double-click sobre un Equipo Pressure_Drop (Type 4)
                            else if ((e.Clicks == 2) && (selectedElement is PumpNode))
                            {
                                punteroaplicacion.numequipos++;

                                Bomba pump = new Bomba(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                pump.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.PumpNode_List.Count; i++)
                                {
                                    if (document.PumpNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.PumpNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    pump.textBox7.Text = Convert.ToString(document.PumpNode_List[i].connects[0].linkNumber + 1);
                                        //    pump.textBox8.Text = Convert.ToString(document.PumpNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                            pump.textBox7.Text = Convert.ToString(document.PumpNode_List[i].connects[0].linkNumber);
                                            pump.textBox8.Text = Convert.ToString(document.PumpNode_List[i].connects[1].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 1 equipo Boundary Condition
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 4)
                                                {
                                                    //aD1
                                                    pump.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                                }
                                            }
                                        }
                                    }
                                }

                                pump.ShowDialog();
                            }

                            //Double-click sobre un Equipo Mixer (Type 5)
                            else if ((e.Clicks == 2) && (selectedElement is MixerNode))
                            {
                                punteroaplicacion.numequipos++;

                                Mezclador mixer = new Mezclador(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                mixer.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.MixerNode_List.Count; i++)
                                {
                                    if (document.MixerNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.MixerNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    mixer.textBox3.Text = Convert.ToString(document.MixerNode_List[i].connects[0].linkNumber + 1);
                                        //    mixer.textBox4.Text = Convert.ToString(document.MixerNode_List[i].connects[0].linkNumber + 2);
                                        //    mixer.textBox5.Text = Convert.ToString(document.MixerNode_List[i].connects[0].linkNumber + 3);
                                        //}
                                        //else
                                        //{
                                            mixer.textBox3.Text = Convert.ToString(document.MixerNode_List[i].connects[0].linkNumber);
                                            mixer.textBox4.Text = Convert.ToString(document.MixerNode_List[i].connects[1].linkNumber);
                                            mixer.textBox5.Text = Convert.ToString(document.MixerNode_List[i].connects[2].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 5 equipo Mixer
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 5)
                                                {
                                                    //aD1 convertir de kPa a psig
                                                    mixer.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                                }
                                            }
                                        }
                                    }
                                }

                                mixer.ShowDialog();
                            }

                            //Double-click sobre un Equipo Pressure_Drop (Type 6)
                            else if ((e.Clicks == 2) && (selectedElement is ReactorNode))
                            {
                                punteroaplicacion.numequipos++;

                                Reactor reactor = new Reactor(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                reactor.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.ReactorNode_List.Count; i++)
                                {
                                    if (document.ReactorNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.ReactorNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    reactor.textBox10.Text = Convert.ToString(document.ReactorNode_List[i].connects[0].linkNumber + 1);
                                        //    reactor.textBox8.Text = Convert.ToString(document.ReactorNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                            reactor.textBox10.Text = Convert.ToString(document.ReactorNode_List[i].connects[0].linkNumber);
                                            reactor.textBox8.Text = Convert.ToString(document.ReactorNode_List[i].connects[1].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 1 equipo Boundary Condition
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 6)
                                                {
                                                    //aD1
                                                    reactor.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                                }
                                            }
                                        }
                                    }
                                }

                                reactor.ShowDialog();
                            }

                            //Double-click sobre un Equipo Heat Exchanger (Type 7)
                            else if ((e.Clicks == 2) && (selectedElement is FeedWaterHeaterNode))
                            {
                                punteroaplicacion.numequipos++;

                                Calentador calentador = new Calentador(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                calentador.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.FeedWaterHeaterNode_List.Count; i++)
                                {
                                    if (document.FeedWaterHeaterNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.FeedWaterHeaterNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    calentador.textBox14.Text = Convert.ToString(document.FeedWaterHeaterNode_List[i].connects[3].linkNumber + 1);
                                        //    calentador.textBox15.Text = Convert.ToString(document.FeedWaterHeaterNode_List[i].connects[4].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                            calentador.textBox11.Text = Convert.ToString(document.FeedWaterHeaterNode_List[i].connects[0].linkNumber);
                                            calentador.textBox12.Text = Convert.ToString(document.FeedWaterHeaterNode_List[i].connects[1].linkNumber);
                                            calentador.textBox13.Text = Convert.ToString(document.FeedWaterHeaterNode_List[i].connects[2].linkNumber);
                                            calentador.textBox14.Text = Convert.ToString(document.FeedWaterHeaterNode_List[i].connects[3].linkNumber);
                                            calentador.textBox15.Text = Convert.ToString(document.FeedWaterHeaterNode_List[i].connects[4].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 1 equipo Boundary Condition
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 7)
                                                {
                                                    //aD1
                                                    calentador.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                                }
                                            }
                                        }
                                    }
                                }

                                calentador.ShowDialog();
                            }

                            //Double-click sobre un Equipo Condenser (Type 8)
                            else if ((e.Clicks == 2) && (selectedElement is CondenserNode))
                            {
                                punteroaplicacion.numequipos++;

                                Condensador condensador = new Condensador(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                condensador.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.CondenserNode_List.Count; i++)
                                {
                                    if (document.CondenserNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.CondenserNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    condensador.textBox10.Text = Convert.ToString(document.CondenserNode_List[i].connects[2].linkNumber + 1);
                                        //    condensador.textBox11.Text = Convert.ToString(document.CondenserNode_List[i].connects[3].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                            condensador.textBox7.Text = Convert.ToString(document.CondenserNode_List[i].connects[0].linkNumber);
                                            condensador.textBox8.Text = Convert.ToString(document.CondenserNode_List[i].connects[1].linkNumber);
                                            condensador.textBox10.Text = Convert.ToString(document.CondenserNode_List[i].connects[2].linkNumber);
                                            condensador.textBox11.Text = Convert.ToString(document.CondenserNode_List[i].connects[3].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 1 equipo Boundary Condition
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 8)
                                                {
                                                    //aD1
                                                    condensador.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                                }
                                            }
                                        }
                                    }
                                }

                                condensador.ShowDialog();
                            }

                            //Double-click sobre un Equipo Turbina (Type 9)
                            else if ((e.Clicks == 2) && (selectedElement is TurbinaNode))
                            {
                                punteroaplicacion.numequipos++;

                                Turbina luisturbina = new Turbina(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisturbina.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.TurbinaNode_List.Count; i++)
                                {
                                    if (document.TurbinaNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.TurbinaNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbina.textBox7.Text = Convert.ToString(document.TurbinaNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbina.textBox8.Text = Convert.ToString(document.TurbinaNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                            luisturbina.textBox7.Text = Convert.ToString(document.TurbinaNode_List[i].connects[0].linkNumber);
                                            luisturbina.textBox8.Text = Convert.ToString(document.TurbinaNode_List[i].connects[1].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 9)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisturbina.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 9
                                                }
                                            }
                                        }
                                    }
                                }

                                luisturbina.ShowDialog();
                            }

                            //Double-click sobre un Equipo Turbina (Type 10)
                            else if ((e.Clicks == 2) && (selectedElement is TurbineWithoutExhaustLossesNode))
                            {
                                punteroaplicacion.numequipos++;

                                Turbina10 luisturbina10 = new Turbina10(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisturbina10.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.TurbineWithoutExhaustLossesNode_List.Count; i++)
                                {
                                    if (document.TurbineWithoutExhaustLossesNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.TurbineWithoutExhaustLossesNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbina10.textBox7.Text = Convert.ToString(document.TurbineWithoutExhaustLossesNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbina10.textBox8.Text = Convert.ToString(document.TurbineWithoutExhaustLossesNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                            luisturbina10.textBox7.Text = Convert.ToString(document.TurbineWithoutExhaustLossesNode_List[i].connects[0].linkNumber);
                                            luisturbina10.textBox8.Text = Convert.ToString(document.TurbineWithoutExhaustLossesNode_List[i].connects[1].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 10)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisturbina10.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 9
                                                }
                                            }
                                        }
                                    }
                                }

                                luisturbina10.ShowDialog();
                            }

                            //Double-click sobre un Equipo Auxiliar Turbina (Type 11)
                            else if ((e.Clicks == 2) && (selectedElement is AuxiliarTurbineNode))
                            {
                                punteroaplicacion.numequipos++;

                                TurbinaAuxiliar luisturbinaAuxiliar = new TurbinaAuxiliar(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisturbinaAuxiliar.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.AuxiliarTurbineNode_List.Count; i++)
                                {
                                    if (document.AuxiliarTurbineNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                            luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber);
                                            luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[1].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 11)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisturbinaAuxiliar.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 9
                                                }
                                            }
                                        }
                                    }
                                }

                                luisturbinaAuxiliar.ShowDialog();
                            }

                            //Double-click sobre un Equipo Moisture Separator (Type 13)
                            else if ((e.Clicks == 2) && (selectedElement is MoistureSeparationNode))
                            {
                                punteroaplicacion.numequipos++;

                                Sephumedad luisSephumedad = new Sephumedad(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisSephumedad.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.MoistureSeparationNode_List.Count; i++)
                                {
                                    if (document.MoistureSeparationNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisSephumedad.textBox3.Text = Convert.ToString(document.MoistureSeparationNode_List[i].connects[0].linkNumber);
                                        luisSephumedad.textBox4.Text = Convert.ToString(document.MoistureSeparationNode_List[i].connects[1].linkNumber);
                                        luisSephumedad.textBox5.Text = Convert.ToString(document.MoistureSeparationNode_List[i].connects[2].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 13)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisSephumedad.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisSephumedad.ShowDialog();
                            }

                            //Double-click sobre un Equipo Moisture Reheater(Type 14)
                            else if ((e.Clicks == 2) && (selectedElement is MoistureReheaterNode))
                            {
                                punteroaplicacion.numequipos++;

                                MSR luisMSR = new MSR(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisMSR.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.MoistureReheaterNode_List.Count; i++)
                                {
                                    if (document.MoistureReheaterNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisMSR.textBox10.Text = Convert.ToString(document.MoistureReheaterNode_List[i].connects[0].linkNumber);
                                        luisMSR.textBox11.Text = Convert.ToString(document.MoistureReheaterNode_List[i].connects[1].linkNumber);
                                        luisMSR.textBox12.Text = Convert.ToString(document.MoistureReheaterNode_List[i].connects[2].linkNumber);
                                        luisMSR.textBox13.Text = Convert.ToString(document.MoistureReheaterNode_List[i].connects[3].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 14)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisMSR.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisMSR.ShowDialog();
                            }

                            //Double-click sobre un Equipo Off-Gas Condenser(Type 15)
                            else if ((e.Clicks == 2) && (selectedElement is OffGasCondenserNode))
                            {
                                punteroaplicacion.numequipos++;

                                Condensador luisCondensador = new Condensador(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisCondensador.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.OffGasCondenserNode_List.Count; i++)
                                {
                                    if (document.OffGasCondenserNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisCondensador.textBox7.Text = Convert.ToString(document.OffGasCondenserNode_List[i].connects[0].linkNumber);
                                        luisCondensador.textBox8.Text = Convert.ToString(document.OffGasCondenserNode_List[i].connects[1].linkNumber);
                                        luisCondensador.textBox10.Text = Convert.ToString(document.OffGasCondenserNode_List[i].connects[2].linkNumber);
                                        luisCondensador.textBox11.Text = Convert.ToString(document.OffGasCondenserNode_List[i].connects[3].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 15)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisCondensador.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisCondensador.ShowDialog();
                            }

                            //Double-click sobre un Equipo Drainage Cooler(Type 16)
                            else if ((e.Clicks == 2) && (selectedElement is DrainageCoolerNode))
                            {
                                punteroaplicacion.numequipos++;

                                EnfriadorDrenajes luisEnfriadorDrenajes = new EnfriadorDrenajes(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisEnfriadorDrenajes.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.DrainageCoolerNode_List.Count; i++)
                                {
                                    if (document.DrainageCoolerNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisEnfriadorDrenajes.textBox7.Text = Convert.ToString(document.DrainageCoolerNode_List[i].connects[0].linkNumber);
                                        luisEnfriadorDrenajes.textBox8.Text = Convert.ToString(document.DrainageCoolerNode_List[i].connects[1].linkNumber);
                                        luisEnfriadorDrenajes.textBox10.Text = Convert.ToString(document.DrainageCoolerNode_List[i].connects[2].linkNumber);
                                        luisEnfriadorDrenajes.textBox11.Text = Convert.ToString(document.DrainageCoolerNode_List[i].connects[3].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 16)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisEnfriadorDrenajes.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisEnfriadorDrenajes.ShowDialog();
                            }

                            //Double-click sobre un Equipo DeSuperHeater (Type 17)
                            else if ((e.Clicks == 2) && (selectedElement is DeSuperHeaterNode))
                            {
                                punteroaplicacion.numequipos++;

                                Atemperador luisAtemperador = new Atemperador(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisAtemperador.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.DeSuperHeaterNode_List.Count; i++)
                                {
                                    if (document.DeSuperHeaterNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisAtemperador.textBox7.Text = Convert.ToString(document.DeSuperHeaterNode_List[i].connects[0].linkNumber);
                                        luisAtemperador.textBox8.Text = Convert.ToString(document.DeSuperHeaterNode_List[i].connects[1].linkNumber);
                                        luisAtemperador.textBox10.Text = Convert.ToString(document.DeSuperHeaterNode_List[i].connects[2].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 17)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisAtemperador.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisAtemperador.ShowDialog();
                            }


                            //Double-click sobre un Equipo Deaerator (Type 18)
                            else if ((e.Clicks == 2) && (selectedElement is DeaeratorNode))
                            {
                                punteroaplicacion.numequipos++;

                                Desaireador luisDesaireador = new Desaireador(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisDesaireador.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.DeaeratorNode_List.Count; i++)
                                {
                                    if (document.DeaeratorNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisDesaireador.textBox7.Text = Convert.ToString(document.DeaeratorNode_List[i].connects[0].linkNumber);
                                        luisDesaireador.textBox6.Text = Convert.ToString(document.DeaeratorNode_List[i].connects[1].linkNumber);
                                        luisDesaireador.textBox10.Text = Convert.ToString(document.DeaeratorNode_List[i].connects[2].linkNumber);
                                        luisDesaireador.textBox8.Text = Convert.ToString(document.DeaeratorNode_List[i].connects[3].linkNumber);
                                        luisDesaireador.textBox4.Text = Convert.ToString(document.DeaeratorNode_List[i].connects[4].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 18)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisDesaireador.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisDesaireador.ShowDialog();
                            }

                            //Double-click sobre un Equipo Drainage Cooler(Type 19)
                            else if ((e.Clicks == 2) && (selectedElement is ValveNode))
                            {
                                punteroaplicacion.numequipos++;

                                Valvula luisValvula = new Valvula(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisValvula.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.ValveNode_List.Count; i++)
                                {
                                    if (document.ValveNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisValvula.textBox7.Text = Convert.ToString(document.ValveNode_List[i].connects[0].linkNumber);
                                        luisValvula.textBox8.Text = Convert.ToString(document.ValveNode_List[i].connects[1].linkNumber);

                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 19 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 19)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisValvula.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisValvula.ShowDialog();
                            }

                            //Double-click sobre un Equipo FixedEnthalpy Splitter (Type 20)
                            else if ((e.Clicks == 2) && (selectedElement is FixedEnthalpySplitterNode))
                            {
                                punteroaplicacion.numequipos++;

                                Divisorentalpiafija luisFixedEnthalpySplitter = new Divisorentalpiafija(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisFixedEnthalpySplitter.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.FixedEnthalpySplitterNode_List.Count; i++)
                                {
                                    if (document.FixedEnthalpySplitterNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisFixedEnthalpySplitter.textBox7.Text = Convert.ToString(document.FixedEnthalpySplitterNode_List[i].connects[0].linkNumber);
                                        luisFixedEnthalpySplitter.textBox8.Text = Convert.ToString(document.FixedEnthalpySplitterNode_List[i].connects[1].linkNumber);
                                        luisFixedEnthalpySplitter.textBox4.Text = Convert.ToString(document.FixedEnthalpySplitterNode_List[i].connects[2].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 20)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisFixedEnthalpySplitter.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisFixedEnthalpySplitter.ShowDialog();
                            }

                            //Double-click sobre un Equipo Flash Tank (Type 21)
                            else if ((e.Clicks == 2) && (selectedElement is FlashTankNode))
                            {
                                punteroaplicacion.numequipos++;

                                TanqueVaporizacion luisTanqueVaporizacion = new TanqueVaporizacion(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisTanqueVaporizacion.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.FlashTankNode_List.Count; i++)
                                {
                                    if (document.FlashTankNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisTanqueVaporizacion.textBox7.Text = Convert.ToString(document.FlashTankNode_List[i].connects[0].linkNumber);
                                        luisTanqueVaporizacion.textBox8.Text = Convert.ToString(document.FlashTankNode_List[i].connects[1].linkNumber);
                                        luisTanqueVaporizacion.textBox4.Text = Convert.ToString(document.FlashTankNode_List[i].connects[2].linkNumber);                                      
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 21)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisTanqueVaporizacion.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisTanqueVaporizacion.ShowDialog();
                            }

                            //Double-click sobre un Equipo Drainage Cooler(Type 22)
                            else if ((e.Clicks == 2) && (selectedElement is HeatExchangerNode))
                            {
                                punteroaplicacion.numequipos++;

                                Intercambiador luisIntercambiador= new Intercambiador(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);

                                luisIntercambiador.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);

                                for (int i = 0; i < document.HeatExchangerNode_List.Count; i++)
                                {
                                    if (document.HeatExchangerNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                                    {
                                        //if ((selectedElement.baseElementNumber != 1) && (document.AuxiliarTurbineNode_List[i].connects[1].linkNumber == 0))
                                        //{
                                        //    luisturbinaAuxiliar.textBox7.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 1);
                                        //    luisturbinaAuxiliar.textBox8.Text = Convert.ToString(document.AuxiliarTurbineNode_List[i].connects[0].linkNumber + 2);
                                        //}
                                        //else
                                        //{
                                        luisIntercambiador.textBox7.Text = Convert.ToString(document.HeatExchangerNode_List[i].connects[0].linkNumber);
                                        luisIntercambiador.textBox8.Text = Convert.ToString(document.HeatExchangerNode_List[i].connects[1].linkNumber);
                                        luisIntercambiador.textBox10.Text = Convert.ToString(document.HeatExchangerNode_List[i].connects[2].linkNumber);
                                        luisIntercambiador.textBox11.Text = Convert.ToString(document.HeatExchangerNode_List[i].connects[3].linkNumber);
                                        //}
                                        for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                                        {

                                            //Hay un ERROR en este if el número de equipos de la
                                            if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                            {
                                                //Condición de Tipo 9 equipo turbina
                                                if (punteroaplicacion.equipos11[j].tipoequipo2 == 22)
                                                {
                                                    //Rendimiento Termodinámico (D1)
                                                    luisIntercambiador.textBox1.Text = Convert.ToString(punteroaplicacion.equipos11[j].aD1);

                                                    //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo turbina type 13
                                                }
                                            }
                                        }
                                    }
                                }

                                luisIntercambiador.ShowDialog();
                            }

                            // Double - click sobre un Elemento tipo ImageNode
                            else if ((e.Clicks == 2) && (selectedElement is ImageNode))
                            {
                                punteroaplicacion.numequipos++;
                                Turbina luisturbina = new Turbina(punteroaplicacion, punteroaplicacion.numecuaciones, punteroaplicacion.numvariables, 0, 0);
                                luisturbina.textBox9.Text = Convert.ToString(selectedElement.baseElementNumber);
                                luisturbina.ShowDialog();
                            }
                        }                        
                    }

                    break;
			}
			
			base.OnMouseDown(e);
            //base.OnMouseDown(this.MouseSnap(e));
		}
        public float zoom = 0;
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //punteroaplicacion.textBox10.Text = "Nº Steps: " + Convert.ToString(e.Delta);
            float delta = e.Delta;
            zoom = zoom + Convert.ToSingle(delta / 1000);
            punteroaplicacion.Action_Zoom(1+zoom);

            //base.OnMouseWheel(e);
        }

		protected override void OnMouseMove(MouseEventArgs e)
		{
            Point mousePoint1 = Gsc2Goc(new Point(e.X, e.Y));            
            
            Rectangle luis = new Rectangle(mousePoint1, new Size(60, 60));

            if (dragdropluis == true)
            {                
                EndAddElement(luis);
                dragdropluis = false;
            }

			if (e.Button == MouseButtons.None)
			{
				this.Cursor = Cursors.Arrow;
				Point mousePoint = Gsc2Goc(new Point(e.X, e.Y));

				if ((resizeAction != null)
					&& ((document.Action == DesignerAction.Select)				
						|| ((document.Action == DesignerAction.Connect)
							&& (resizeAction.IsResizingLink))))
				{
					this.Cursor = resizeAction.UpdateResizeCornerCursor(mousePoint);
				}
              
                if (document.Action == DesignerAction.Connect)
				{
					BaseElement mousePointerElementTMP = document.FindElement(mousePoint);
					if (mousePointerElement != mousePointerElementTMP)
					{
						if (mousePointerElementTMP is ConnectorElement)
						{
							mousePointerElement = mousePointerElementTMP;
							mousePointerElement.Invalidate();
							this.Invalidate(mousePointerElement, true);
						}
						else if (mousePointerElement != null)
						{
							mousePointerElement.Invalidate();
							this.Invalidate(mousePointerElement, true);
							mousePointerElement = null;
						}
						
					}
				}
				else
				{
					this.Invalidate(mousePointerElement, true);
					mousePointerElement = null;
				}
			}			

			if (e.Button == MouseButtons.Left)
			{                               
                Point dragPoint = Gsc2Goc(new Point(e.X, e.Y));                                     

                if ((resizeAction != null) && (resizeAction.IsResizing))
				{
					resizeAction.Resize(dragPoint);
					this.Invalidate();					
				}

				if ((moveAction != null) && (moveAction.IsMoving))
				{
					moveAction.Move(dragPoint);
					this.Invalidate();
				}
				
				if ((isMultiSelection) || (isAddSelection))
				{
					Point p = Gsc2Goc(new Point(e.X, e.Y));
					selectionArea.Size = new Size (p.X - selectionArea.Location.X, p.Y - selectionArea.Location.Y);
					selectionArea.Invalidate();
					this.Invalidate(selectionArea, true);
				}
				
				if (isAddLink)
				{
					selectedElement = document.FindElement(dragPoint);
					if ((selectedElement is ConnectorElement) 
						&& (document.CanAddLink(connStart, (ConnectorElement) selectedElement)))
						linkLine.Connector2 = (ConnectorElement) selectedElement;
					else
						linkLine.Connector2 = connEnd;

					IMoveController ctrl = (IMoveController) ((IControllable) connEnd).GetController();
					ctrl.Move(dragPoint);
					
					//this.Invalidate(linkLine, true); //TODO
					base.Invalidate();
				}
			}

            if (e.Button == MouseButtons.Right)
            {

            }

                base.OnMouseMove (e);
            //base.OnMouseMove(this.MouseSnap(e));
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{           
            Rectangle selectionRectangle = selectionArea.GetUnsignedRectangle();
			
			if ((moveAction != null) && (moveAction.IsMoving))
			{
				ElementEventArgs eventClickArg = new ElementEventArgs(selectedElement);
				OnElementClick(eventClickArg);

				moveAction.End();
                AddUndo();

                moveAction = null;

				ElementMouseEventArgs eventMouseUpArg = new ElementMouseEventArgs(selectedElement, e.X, e.Y);
				OnElementMouseUp(eventMouseUpArg);
				
				if (changed)
					AddUndo();
			}

			// Select
			if (isMultiSelection)
			{
				EndSelectElements(selectionRectangle);
                AddUndo();
            }
			// Add element
			else if (isAddSelection)
			{
				EndAddElement(selectionRectangle);
                AddUndo();
            }
			
			// Add link
			else if (isAddLink)
			{
				Point mousePoint = Gsc2Goc(new Point(e.X, e.Y));
				EndAddLink();				
				AddUndo();
			}
			
			// Resize
			if (resizeAction != null)
			{
				if (resizeAction.IsResizing)
				{
					Point mousePoint = Gsc2Goc(new Point(e.X, e.Y));
					resizeAction.End(mousePoint);				
					AddUndo();
				}
				resizeAction.UpdateResizeCorner();
			}

            // Rotate
            if (rotateAction != null)
            {
                if (rotateAction.IsRotating)
                {
                    Point mousePoint = Gsc2Goc(new Point(e.X, e.Y));
                    rotateAction.End(mousePoint);
                    AddUndo();
                }
                rotateAction.UpdateRotateCorner();
            }

            punteroaplicacion.propertyGrid10.SelectedObject = null;

            if (punteroaplicacion.designer1.Document.SelectedElements.Count == 1)
            {
                //TODO with rest of equipments types
                if (punteroaplicacion.designer1.Document.SelectedElements[0] is BoundaryConditionNode)
                {
                    for (int i = 0; i < document.BoundaryConditionNode_List.Count; i++)
                    {
                        if (document.BoundaryConditionNode_List[i].baseElementNumber == punteroaplicacion.designer1.Document.SelectedElements[0].baseElementNumber)
                        {
                            punteroaplicacion.propertyGrid10.SelectedObject = document.BoundaryConditionNode_List[i];                           
                        }
                    }
                }
                else
                {
                    punteroaplicacion.propertyGrid10.SelectedObject = punteroaplicacion.designer1.Document.SelectedElements[0];
                }
            }
            else if (punteroaplicacion.designer1.Document.SelectedElements.Count > 1)
            {
                punteroaplicacion.propertyGrid10.SelectedObjects = punteroaplicacion.designer1.Document.SelectedElements.GetArray();
            }
            else if (punteroaplicacion.designer1.Document.SelectedElements.Count == 0)
            {
                punteroaplicacion.propertyGrid10.SelectedObject = punteroaplicacion.designer1.Document;
            }

            RestartInitValues();

			base.Invalidate();

			base.OnMouseUp(e);
            //base.OnMouseUp(this.MouseSnap(e));

		}

        #endregion

        #endregion

        #region Events Raising

        // element handler
        public delegate void ElementEventHandler(object sender, ElementEventArgs e);

		#region Element Mouse Events
		
		// CLICK
		[Category("Element")]
		public event ElementEventHandler ElementClick;
		
		protected virtual void OnElementClick(ElementEventArgs e)
		{
			if (ElementClick != null)
			{
				ElementClick(this, e);
			}
		}

		// mouse handler
		public delegate void ElementMouseEventHandler(object sender, ElementMouseEventArgs e);

		// MOUSE DOWN
		[Category("Element")]
		public event ElementMouseEventHandler ElementMouseDown;
		
		protected virtual void OnElementMouseDown(ElementMouseEventArgs e)
		{
			if (ElementMouseDown != null)
			{
				ElementMouseDown(this, e);
			}
		}

		// MOUSE UP
		[Category("Element")]
		public event ElementMouseEventHandler ElementMouseUp;
		
		protected virtual void OnElementMouseUp(ElementMouseEventArgs e)
		{
			if (ElementMouseUp != null)
			{
				ElementMouseUp(this, e);
			}
		}

		#endregion
		 
		#region Element Move Events
		// Before Move
		[Category("Element")]
		public event ElementEventHandler ElementMoving;
		
		protected virtual void OnElementMoving(ElementEventArgs e)
		{
			if (ElementMoving != null)
			{
				ElementMoving(this, e);
			}
		}

		// After Move
		[Category("Element")]
		public event ElementEventHandler ElementMoved;
		
		protected virtual void OnElementMoved(ElementEventArgs e)
		{
			if (ElementMoved != null)
			{
				ElementMoved(this, e);
			}
		}
		#endregion

		#region Element Resize Events
		// Before Resize
		[Category("Element")]
		public event ElementEventHandler ElementResizing;
		
		protected virtual void OnElementResizing(ElementEventArgs e)
		{
			if (ElementResizing != null)
			{
				ElementResizing(this, e);
			}
		}

		// After Resize
		[Category("Element")]
		public event ElementEventHandler ElementResized;
		
		protected virtual void OnElementResized(ElementEventArgs e)
		{
			if (ElementResized != null)
			{
				ElementResized(this, e);
			}
		}
        #endregion

        #region Element Rotate Events
        // Before Rotate
        [Category("Element")]
        public event ElementEventHandler ElementRotating;

        protected virtual void OnElementRotating(ElementEventArgs e)
        {
            if (ElementRotating != null)
            {
                ElementRotating(this, e);
            }
        }

        // After Rotate
        [Category("Element")]
        public event ElementEventHandler ElementRotated;

        protected virtual void OnElementRotated(ElementEventArgs e)
        {
            if (ElementRotated != null)
            {
                ElementRotated(this, e);
            }
        }
        #endregion

        #region Element Connect Events
        // connect handler
        public delegate void ElementConnectEventHandler(object sender, ElementConnectEventArgs e);

		// Before Connect
		[Category("Element")]
		public event ElementConnectEventHandler ElementConnecting;
		
		protected virtual void OnElementConnecting(ElementConnectEventArgs e)
		{
			if (ElementConnecting != null)
			{
				ElementConnecting(this, e);
			}
		}

		// After Connect
		[Category("Element")]
		public event ElementConnectEventHandler ElementConnected;
		
		protected virtual void OnElementConnected(ElementConnectEventArgs e)
		{
			if (ElementConnected != null)
			{
				ElementConnected(this, e);
			}
		}
		#endregion

		#region Element Selection Events
		// connect handler
		public delegate void ElementSelectionEventHandler(object sender, ElementSelectionEventArgs e);

		// Selection
		[Category("Element")]
		public event ElementSelectionEventHandler ElementSelection;
		
		protected virtual void OnElementSelection(ElementSelectionEventArgs e)
		{
			if (ElementSelection != null)
			{
				ElementSelection(this, e);
			}
		}

		#endregion

		#endregion

		#region Events Handling
		private void document_PropertyChanged(object sender, EventArgs e)
		{
			if (!IsChanging())
			{
				base.Invalidate();
			}
		}

		private void document_AppearancePropertyChanged(object sender, EventArgs e)
		{
			if (!IsChanging())
			{
				AddUndo();
				base.Invalidate();
			}
		}

		private void document_ElementPropertyChanged(object sender, EventArgs e)
		{
			changed = true;

			if (!IsChanging())
			{
				AddUndo();
				base.Invalidate();
			}
		}

		private void document_ElementSelection(object sender, ElementSelectionEventArgs e)
		{
			OnElementSelection(e);
		}
		#endregion

		#region Properties

     
		public Document Document
		{
			get
			{
				return document;
			}
		}

		public bool CanUndo
		{
			get
			{
				return undo.CanUndo;
			}
		}

		public bool CanRedo
		{
			get
			{
				return undo.CanRedo;
			}
		}


		private bool IsChanging()
		{
			return (
					((moveAction != null) && (moveAction.IsMoving)) //isDragging
					|| isAddLink || isMultiSelection || 
					((resizeAction != null) && (resizeAction.IsResizing)) //isResizing
					);
		}
		#endregion
		
		#region Draw Methods

		/// <summary>
		/// Graphic surface coordinates to graphic object coordinates.
		/// </summary>
		/// <param name="p">Graphic surface point.</param>
		/// <returns></returns>
		public Point Gsc2Goc(Point gsp)
		{
			float zoom = document.Zoom;            
			gsp.X = (int) ((gsp.X - this.AutoScrollPosition.X) / zoom);
			gsp.Y = (int) ((gsp.Y - this.AutoScrollPosition.Y) / zoom);
			return gsp;
		}

		public Rectangle Gsc2Goc(Rectangle gsr)
		{
			float zoom = document.Zoom;         
            gsr.X = (int) ((gsr.X - this.AutoScrollPosition.X) / zoom);
			gsr.Y = (int) ((gsr.Y - this.AutoScrollPosition.Y) / zoom);
			gsr.Width = (int) ((gsr.Width) / zoom);
			gsr.Height = (int) ((gsr.Height) / zoom);
			return gsr;
		}

		public Rectangle Goc2Gsc(Rectangle gsr)
		{
			float zoom = document.Zoom;          
            gsr.X = (int) ((gsr.X  + this.AutoScrollPosition.X) * zoom);
			gsr.Y = (int) ((gsr.Y + this.AutoScrollPosition.Y) * zoom);
			gsr.Width = (int) ((gsr.Width) * zoom);
			gsr.Height = (int) ((gsr.Height) * zoom);
			return gsr;
		}

		internal void DrawSelectionRectangle(Graphics g)
		{
			selectionArea.Draw(g);
		}
		#endregion

		#region Open/Save File
		public void Save(string fileName)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, document);
			stream.Close();
		}

		public void Open(string fileName)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			document = (Document) formatter.Deserialize(stream);
			stream.Close();
			RecreateEventsHandlers();
		}
		#endregion

		#region Copy/Paste
		public void Copy()
		{
			if (document.SelectedElements.Count == 0) return;

			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			formatter.Serialize(stream, document.SelectedElements.GetArray());
			DataObject data = new DataObject(DataFormats.GetFormat("Diagram.NET Element Collection").Name,
				stream);
			Clipboard.SetDataObject(data);
            AddUndo();
        }

		public void Paste()
		{
			const int pasteStep = 20;

			undo.Enabled = false;
			IDataObject iData = Clipboard.GetDataObject();
			DataFormats.Format format = DataFormats.GetFormat("Diagram.NET Element Collection");
			if (iData.GetDataPresent(format.Name))
			{
				IFormatter formatter = new BinaryFormatter();
				Stream stream = (MemoryStream) iData.GetData(format.Name);
				BaseElement[] elCol = (BaseElement[]) formatter.Deserialize(stream);
				stream.Close();

				foreach(BaseElement el in elCol)
				{
					el.Location = new Point(el.Location.X + pasteStep, el.Location.Y + pasteStep);
				}

				document.AddElements(elCol);
                AddUndo();

                document.ClearSelection();
				document.SelectElements(elCol);
			}
			undo.Enabled = true;				
			
			EndGeneralAction();
            
        }

		public void Cut()
		{
			this.Copy();
			DeleteSelectedElements();
            AddUndo();

            EndGeneralAction();
		}
		#endregion

		#region Start/End Actions and General Functions
		
		#region General
		private void EndGeneralAction()
		{
			RestartInitValues();
			
			if (resizeAction != null) resizeAction.ShowResizeCorner(false);
		}
		
		private void RestartInitValues()
		{			
			// Reinitialize status
			moveAction = null;

			isMultiSelection = false;
			isAddSelection = false;
			isAddLink = false;

			changed = false;

			connStart = null;
			
			selectionArea.FillColor1 = SystemColors.Control;
			selectionArea.BorderColor = SystemColors.Control;
			selectionArea.Visible = false;

			document.CalcWindow(true);
		}

		#endregion

		#region Selection
		private void StartSelectElements(BaseElement selectedElement, Point mousePoint)
		{
            // Vefiry if element is in selection
            if (!document.SelectedElements.Contains(selectedElement))
            {
                //Clear selection and add new element to selection
                document.ClearSelection();
                document.SelectElement(selectedElement);

                if (selectedElement is BoundaryConditionNode)
                {
                    for (int i = 0; i < document.BoundaryConditionNode_List.Count; i++)
                    {
                        if (document.BoundaryConditionNode_List[i].baseElementNumber == selectedElement.baseElementNumber)
                        {
                            for (int j = 0; j < punteroaplicacion.equipos11.Count; j++)
                            {
                                if (punteroaplicacion.equipos11[j].numequipo2 == selectedElement.baseElementNumber)
                                {
                                    //Condición de Tipo 1 equipo Boundary Condition
                                    if (punteroaplicacion.equipos11[j].tipoequipo2 == 1)
                                    {
                                        //Multiplicamos por 0.4536 para convertir de Lb/sg a kg/sg                                       
                                        document.BoundaryConditionNode_List[i].D1 = punteroaplicacion.equipos11[j].aD1 * 0.4536;
                                        // Multiplicamos por (6.8947572 / 100) para convertir de psia a bar                                       
                                        document.BoundaryConditionNode_List[i].D2 = punteroaplicacion.equipos11[j].aD2 * (6.8947572 / 100);
                                        // Multiplicamos por 2.32600 para convertir de BTU/Lb                                     
                                        document.BoundaryConditionNode_List[i].D3 = punteroaplicacion.equipos11[j].aD3 * 2.326009;
                                        //...TODO recuperar el resto de variables (aD2, aD3, aD4, etc) del equipo Boundary Condition type 1
                                    }
                                }
                            }
                        }
                    }
                }
            }
			changed = false;
			

			moveAction = new MoveAction();
			MoveAction.OnElementMovingDelegate onElementMovingDelegate = new Dalssoft.DiagramNet.MoveAction.OnElementMovingDelegate(OnElementMoving);
			moveAction.Start(mousePoint, document, onElementMovingDelegate);


			// Get Controllers
			controllers = new IController[document.SelectedElements.Count];
			for(int i = document.SelectedElements.Count - 1; i >= 0; i--)
			{
				if (document.SelectedElements[i] is IControllable)
				{
					// Get General Controller
					controllers[i] = ((IControllable) document.SelectedElements[i]).GetController();
				}
				else
				{
					controllers[i] = null;
				}
			}

			resizeAction = new ResizeAction();
			resizeAction.Select(document);
		}

		private void EndSelectElements(Rectangle selectionRectangle)
		{
			document.SelectElements(selectionRectangle);
		}
		#endregion		

		#region Resize
		private void StartResizeElement(Point mousePoint)
		{
			if ((resizeAction != null)
				&& ((document.Action == DesignerAction.Select)				
					|| ((document.Action == DesignerAction.Connect)
						&& (resizeAction.IsResizingLink))))
			{
				ResizeAction.OnElementResizingDelegate onElementResizingDelegate = new ResizeAction.OnElementResizingDelegate(OnElementResizing);
				resizeAction.Start(mousePoint, onElementResizingDelegate);
				if (!resizeAction.IsResizing)
					resizeAction = null;
			}
		}
        #endregion

        #region Rotate
        private void StartRotateElement(Point mousePoint)
        {
            if ((rotateAction != null)
                && ((document.Action == DesignerAction.Select)
                    || ((document.Action == DesignerAction.Connect)
                        && (rotateAction.IsRotatingLink))))
            {
                ResizeAction.OnElementResizingDelegate onElementResizingDelegate = new ResizeAction.OnElementResizingDelegate(OnElementResizing);
                resizeAction.Start(mousePoint, onElementResizingDelegate);
                if (!resizeAction.IsResizing)
                    resizeAction = null;
            }
        }
        #endregion

        #region Link
        private void StartAddLink(ConnectorElement connStart, Point mousePoint)
		{
			if (document.Action == DesignerAction.Connect)
			{
				this.connStart = connStart;
				this.connEnd = new ConnectorElement(connStart.ParentElement);                

				connEnd.Location = connStart.Location;
				IMoveController ctrl = (IMoveController) ((IControllable) connEnd).GetController();
				ctrl.Start(mousePoint);

				isAddLink = true;
				
				switch(document.LinkType)
				{
					case (LinkType.Straight):                        
                        linkLine = new StraightLinkElement(connStart, connEnd, arrowInConnections, arrowColor, fillArrowsWithColor, ArrowWith, ArrowAngle, linesBorderColor, arrowsBorderColor, arrowsBorderWidth, linesBorderWidth, arrowsAtBeginingAndAtEnd);

                        break;
					case (LinkType.RightAngle):
						linkLine = new RightAngleLinkElement(connStart, connEnd, arrowInConnections, arrowColor, fillArrowsWithColor, ArrowWith, ArrowAngle, linesBorderColor, arrowsBorderColor, arrowsBorderWidth, linesBorderWidth, arrowsAtBeginingAndAtEnd);

						break;
				}
				linkLine.Visible = true;
				linkLine.BorderColor = Color.FromArgb(150, Color.Black);
				linkLine.BorderWidth = 1;
				
				this.Invalidate(linkLine, true);               
                
                OnElementConnecting(new ElementConnectEventArgs(connStart.ParentElement, null, linkLine));
			}
		}

		private void EndAddLink()
		{
			if (connEnd != linkLine.Connector2)
			{
				linkLine.Connector1.RemoveLink(linkLine);
				linkLine = document.AddLink(linkLine.Connector1, linkLine.Connector2);
                this.document.ConnectionNumber++;
                OnElementConnected(new ElementConnectEventArgs(linkLine.Connector1.ParentElement, linkLine.Connector2.ParentElement, linkLine));
            }



			connStart = null;
			connEnd = null;
			linkLine = null;
		}
		#endregion

		#region Add Element
		private void StartAddElement(Point mousePoint)
		{
            document.ClearSelection();

			//Change Selection Area Color
			selectionArea.FillColor1 = Color.LightSteelBlue;
			selectionArea.BorderColor = Color.WhiteSmoke;

			isAddSelection = true;
			selectionArea.Visible = true;
			selectionArea.Location = mousePoint;
			selectionArea.Size = new Size(0, 0);		
		}

		private void EndAddElement(Rectangle selectionRectangle)
		{
            BaseElement el;

            if (metododibujar == OpcionDibujo.FixedSize)
            {
                //IMPORTANTE, ha de fijarse el mismo tamaño de imagen en los siguiente sitios para conseguir la nitidez:
                // a) En el tamaño original de la imagen *.bmp
                // b) En el tamaño de la ImageList 
                // c) En la siguiente línea de Código
                Point localizacion = new Point((selectionRectangle.Location.X - 30), (selectionRectangle.Location.Y - 30));            
                Size tamaño0 = new Size(86, 47);              
                Size tamaño1 = new Size(69, 53);
                Size tamaño2 = new Size(61, 56);
                Size tamaño3 = new Size(40, 64);
                Size tamaño4 = new Size(74, 58);
                Size tamaño5 = new Size(75, 62);
                Size tamaño6 = new Size(94, 58);
                Size tamaño7 = new Size(129, 65);
                Size tamaño8 = new Size(107, 94);
                Size tamaño9 = new Size(53, 80);
                Size tamaño10 = new Size(60, 70);
                Size tamaño11 = new Size(54, 67);
                Size tamaño13 = new Size(59, 52);
                Size tamaño14 = new Size(78, 78);
                Size tamaño15 = new Size(71, 60);
                Size tamaño16 = new Size(83, 48);
                Size tamaño17 = new Size(63, 55);
                Size tamaño18 = new Size(76, 73);
                Size tamaño19 = new Size(65, 26);
                Size tamaño20 = new Size(65, 35);
                Size tamaño21 = new Size(56, 66);
                Size tamaño22 = new Size(76, 68);
                Rectangle recluis0 = new Rectangle(localizacion, tamaño0);
                Rectangle recluis1 = new Rectangle(localizacion, tamaño1);
                Rectangle recluis2 = new Rectangle(localizacion, tamaño2);
                Rectangle recluis3 = new Rectangle(localizacion, tamaño3);
                Rectangle recluis4 = new Rectangle(localizacion, tamaño4);
                Rectangle recluis5 = new Rectangle(localizacion, tamaño5);
                Rectangle recluis6 = new Rectangle(localizacion, tamaño6);
                Rectangle recluis7 = new Rectangle(localizacion, tamaño7);
                Rectangle recluis8 = new Rectangle(localizacion, tamaño8);
                Rectangle recluis9 = new Rectangle(localizacion, tamaño9);
                Rectangle recluis10 = new Rectangle(localizacion, tamaño10);
                Rectangle recluis11 = new Rectangle(localizacion, tamaño11);
                Rectangle recluis13 = new Rectangle(localizacion, tamaño13);
                Rectangle recluis14 = new Rectangle(localizacion, tamaño14);
                Rectangle recluis15 = new Rectangle(localizacion, tamaño15);
                Rectangle recluis16 = new Rectangle(localizacion, tamaño16);
                Rectangle recluis17 = new Rectangle(localizacion, tamaño17);
                Rectangle recluis18 = new Rectangle(localizacion, tamaño18);
                Rectangle recluis19 = new Rectangle(localizacion, tamaño19);
                Rectangle recluis20 = new Rectangle(localizacion, tamaño20);
                Rectangle recluis21 = new Rectangle(localizacion, tamaño21);
                Rectangle recluis22 = new Rectangle(localizacion, tamaño22);

                switch (document.ElementType)
                {
                    case ElementType.Rectangle:
                        el = new RectangleElement(recluis0);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.RectangleNode:
                        el = new RectangleNode(recluis0, 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Elipse1:
                        el = new ElipseElement(recluis0);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Image:
                        el = new ImageElement(recluis0, 0, imagen0);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ImageNode:
                        el = new ImageNode(recluis0, 0, imagen0);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ElipseNode1:
                        el = new ElipseNode(recluis0, 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Turbina:
                        el = new TurbinaElement(recluis9, 9, imagen9, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;                   
                    case ElementType.BoundaryConditionNode:
                        el = new BoundaryConditionNode(recluis1, 1, imagen1, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DivisorNode:
                        el = new SplitterNode(recluis2, 2, imagen2, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.PressureDropNode:
                        el = new PressureDropNode(recluis3, 3, imagen3, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.PumpNode:
                        el = new PumpNode(recluis4, 4, imagen4, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.MixerNode:
                        el = new MixerNode(recluis5, 5, imagen5, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ReactorNode:
                        el = new ReactorNode(recluis6, 6, imagen6, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.FeedWaterHeaterNode:
                        el = new FeedWaterHeaterNode(recluis7, 7, imagen7, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.CondenserNode:
                        el = new CondenserNode(recluis8, 8, imagen8, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.TurbinaNode:
                        el = new TurbinaNode(recluis9, 9, imagen9, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.TurbineWithoutExhaustLossesNode:
                        el = new TurbineWithoutExhaustLossesNode(recluis10, 10, imagen10, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.AuxiliarTurbineNode:
                        el = new AuxiliarTurbineNode(recluis11, 11, imagen11, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.MoistureSeparationNode:
                        el = new MoistureSeparationNode(recluis13, 13, imagen13, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.MoistureReheaterNode:
                        el = new MoistureReheaterNode(recluis14, 14, imagen14, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.OffGasCondenserNode:
                        el = new OffGasCondenserNode(recluis15, 15, imagen15, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DrainageCoolerNode:
                        el = new DrainageCoolerNode(recluis16, 16, imagen16, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DeSuperHeaterNode:
                        el = new DeSuperHeaterNode(recluis17, 17, imagen17, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DeaeratorNode:
                        el = new DeaeratorNode(recluis18, 18, imagen18, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ValveNode:
                        el = new ValveNode(recluis19, 19, imagen19, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.FixedEnthalpySplitterNode:
                        el = new FixedEnthalpySplitterNode(recluis20, 20, imagen20, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.FlashTankNode:
                        el = new FlashTankNode(recluis21, 21, imagen21, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.HeatExchangerNode:
                        el = new HeatExchangerNode(recluis22, 22, imagen22, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.CommentBox:
                        el = new CommentBoxElement(recluis0);
                        this.document.ElementNumber++;
                        break;
                    default:
                        el = new RectangleNode(recluis0, 1);
                        this.document.ElementNumber++;
                        break;
                }

                document.AddElement(el);
                document.Action = DesignerAction.Add;
            }

            else if(metododibujar==OpcionDibujo.DragandDropSize)
            {
                switch (document.ElementType)
                {
                    case ElementType.Linea:
                        el = new LineaElementResultados(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Rectangle:
                        el = new RectangleElement(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.RectangleNode:
                        el = new RectangleNode(selectionRectangle, 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Elipse1:
                        el = new ElipseElement(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Image:
                        el = new ImageElement(selectionRectangle, TipodeEquipo, imagen0);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ImageNode:
                        el = new ImageNode(selectionRectangle, TipodeEquipo, imagen0);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ElipseNode1:
                        el = new ElipseNode(selectionRectangle, 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Turbina:
                        el = new TurbinaElement(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.TurbinaResultadosNode:
                        el = new TurbinaResultadoNode(selectionRectangle,9);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.SeparadorHumedadNode:
                        el = new SeparadorHumedadResultadoNode(selectionRectangle, 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ArcoResultados:
                        el = new ArcoElementResultados (selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.TurbinaResultadosAltaNode:
                        el = new TurbinaAltaResultadoNode(selectionRectangle, 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.CondensadorSellosNode:
                        el = new CondensadorSellosResultadoNode(selectionRectangle, 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DesaireadorNode:
                        el = new DesaireadorResultadoNode(selectionRectangle, 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.RectanguloRedondeado:
                        el = new RectanguloRedondeadoElementResultados(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.BombaResultadosNode:
                        el = new BombaResultadoNode(selectionRectangle, 9);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ValvulaNode:
                        el = new ValvulaResultadoNode(selectionRectangle, 9);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Rectangulo:
                        el = new RectanguloElementResultados(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Circulo:
                        el = new CirculoElementResultados(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.CondensadorNode:
                        el = new CondensadorResultadoNode(selectionRectangle,1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.Generador:
                        el = new GeneradorElementResultados(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.CalentadorNode:
                        el = new CalentadorResultadoNode(selectionRectangle, 1);
                        this.document.ElementNumber++;
                        break;                   
                    case ElementType.BoundaryConditionNode:
                        el = new BoundaryConditionNode(selectionRectangle, 1, imagen1, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DivisorNode:
                        el = new SplitterNode(selectionRectangle, 2, imagen2, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.PressureDropNode:
                        el = new PressureDropNode(selectionRectangle, 3, imagen3, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.PumpNode:
                        el = new PumpNode(selectionRectangle, 4, imagen4, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.MixerNode:
                        el = new MixerNode(selectionRectangle, 5, imagen5, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ReactorNode:
                        el = new ReactorNode(selectionRectangle, 6, imagen6, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.FeedWaterHeaterNode:
                        el = new FeedWaterHeaterNode(selectionRectangle, 7, imagen7, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.CondenserNode:
                        el = new CondenserNode(selectionRectangle, 8, imagen8, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.TurbinaNode:
                        el = new TurbinaNode(selectionRectangle, 9, imagen9, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                     case ElementType.TurbineWithoutExhaustLossesNode:
                        el = new TurbineWithoutExhaustLossesNode(selectionRectangle, 10, imagen10, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.AuxiliarTurbineNode:
                        el = new AuxiliarTurbineNode(selectionRectangle, 11, imagen11, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.MoistureSeparationNode:
                        el = new MoistureSeparationNode(selectionRectangle, 13, imagen13, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.MoistureReheaterNode:
                        el = new MoistureReheaterNode(selectionRectangle, 14, imagen14, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.OffGasCondenserNode:
                        el = new OffGasCondenserNode(selectionRectangle, 15, imagen15, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DrainageCoolerNode:
                        el = new DrainageCoolerNode(selectionRectangle, 16, imagen16, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DeSuperHeaterNode:
                        el = new DeSuperHeaterNode(selectionRectangle, 17, imagen17, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.DeaeratorNode:
                        el = new DeaeratorNode(selectionRectangle, 18, imagen18, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.ValveNode:
                        el = new ValveNode(selectionRectangle, 19, imagen19, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.FixedEnthalpySplitterNode:
                        el = new FixedEnthalpySplitterNode(selectionRectangle, 20, imagen20, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.FlashTankNode:
                        el = new FlashTankNode(selectionRectangle, 21, imagen21, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.HeatExchangerNode:
                        el = new HeatExchangerNode(selectionRectangle, 22, imagen22, this.document.ElementNumber + 1);
                        this.document.ElementNumber++;
                        break;
                    case ElementType.CommentBox:
                        el = new CommentBoxElement(selectionRectangle);
                        this.document.ElementNumber++;
                        break;
                    default:
                        el = new RectangleNode(selectionRectangle, 1);
                        this.document.ElementNumber++;
                        break;
                }

                document.AddElement(el);
                document.Action = DesignerAction.Add;
            }
		}
		#endregion

		#region Edit Label
		private void StartEditLabel()
		{
			isEditLabel = true;

			// Disable resize
			if (resizeAction != null)
			{	
				resizeAction.ShowResizeCorner(false);
				resizeAction = null;
			}
			
			editLabelAction = new EditLabelAction();
			editLabelAction.StartEdit(selectedElement, labelTextBox);
		}

		private void EndEditLabel()
		{
			if (editLabelAction != null)
			{
				editLabelAction.EndEdit();
				editLabelAction = null;
			}
			isEditLabel = false;
		}
		#endregion

		#region Delete
		private void DeleteElement(Point mousePoint)
		{
            document.DeleteElement(mousePoint);            
            selectedElement = null;
			document.Action = DesignerAction.Select;		
		}

		private void DeleteSelectedElements()
		{
			document.DeleteSelectedElements();
		}
		#endregion

		#endregion

		#region Undo/Redo
		public void Undo()
		{
			document = (Document) undo.Undo();
			RecreateEventsHandlers();
			if (resizeAction != null) resizeAction.UpdateResizeCorner();
			base.Invalidate();
		}

		public void Redo()
		{
			document = (Document) undo.Redo();
			RecreateEventsHandlers();
			if (resizeAction != null) resizeAction.UpdateResizeCorner();
			base.Invalidate();
		}

		private void AddUndo()
		{
			undo.AddUndo(document);
		}
		#endregion

		private void RecreateEventsHandlers()
		{
			document.PropertyChanged += new EventHandler(document_PropertyChanged);
			document.AppearancePropertyChanged+=new EventHandler(document_AppearancePropertyChanged);
			document.ElementPropertyChanged += new EventHandler(document_ElementPropertyChanged);
			document.ElementSelection += new Document.ElementSelectionEventHandler(document_ElementSelection);
		}
    }
}
