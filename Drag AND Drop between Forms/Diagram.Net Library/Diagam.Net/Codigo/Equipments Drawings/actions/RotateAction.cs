using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Dalssoft.DiagramNet
{
    /// <summary>
    /// This class control the size of elements
    /// </summary>
    internal class RotateAction
    {
        public delegate void OnElementRotateDelegate(ElementEventArgs e);
        private OnElementRotateDelegate onElementRotatingDelegate;

        private bool isRotating = false;
        private IRotateController rotateCtrl = null;
        private Document document = null;

        public RotateAction()
        {
        }

        public bool IsRotating
        {
            get
            {
                return isRotating;
            }
        }

        public bool IsRotatingLink
        {
            get
            {
                return ((rotateCtrl != null) && (rotateCtrl.OwnerElement is BaseLinkElement));
            }
        }

        public void Select(Document document)
        {
            this.document = document;

            // Get Rotate Controller
            if ((document.SelectedElements.Count == 1) && (document.SelectedElements[0] is IControllable))
            {
                IController ctrl = ((IControllable)document.SelectedElements[0]).GetController();

                if (ctrl is IRotateController)
                {
                    ctrl.OwnerElement.Invalidate();

                    rotateCtrl = (IRotateController)ctrl;
                    ShowRotateCorner(true);
                }
            }

            else
                rotateCtrl = null;
        }

        public void Start(Point mousePoint, OnElementRotateDelegate onElementRotatingDelegate)
        {
            isRotating = false;

            if (rotateCtrl == null) return;

            this.onElementRotatingDelegate = onElementRotatingDelegate;

            rotateCtrl.OwnerElement.Invalidate();

            CornerPosition corPos = rotateCtrl.HitTestCorner(mousePoint);

            if (corPos != CornerPosition.Nothing)
            {
                //Events
                ElementEventArgs eventRotateArg = new ElementEventArgs(rotateCtrl.OwnerElement);
                onElementRotatingDelegate(eventRotateArg);

                rotateCtrl.Start(mousePoint, corPos);

                UpdateRotateCorner();

                isRotating = true;
            }
        }

        public void Rotate(Point dragPoint)
        {
            if (document.SnapToGrid)
                dragPoint = DiagramUtil.RoundPoint(dragPoint, document.GridSize);

            if ((rotateCtrl != null) && (rotateCtrl.CanRotate))
            {
                //Events
                ElementEventArgs eventResizeArg = new ElementEventArgs(rotateCtrl.OwnerElement);
                onElementRotatingDelegate(eventResizeArg);

                rotateCtrl.OwnerElement.Invalidate();

                rotateCtrl.Rotate(dragPoint);

                ILabelController lblCtrl = ControllerHelper.GetLabelController(rotateCtrl.OwnerElement);
                if (lblCtrl != null)
                    lblCtrl.SetLabelPosition();
                else
                {
                    if (rotateCtrl.OwnerElement is ILabelElement)
                    {
                        LabelElement label = ((ILabelElement)rotateCtrl.OwnerElement).Label;
                        label.PositionBySite(rotateCtrl.OwnerElement);
                    }
                }

                UpdateRotateCorner();
            }
        }

        public void End(Point posEnd)
        {
            if (document.SnapToGrid)
                posEnd = DiagramUtil.RoundPoint(posEnd, document.GridSize);

            if (rotateCtrl != null)
            {
                rotateCtrl.OwnerElement.Invalidate();

                rotateCtrl.End(posEnd);

                if (document.SnapToGrid)
                {
                    BaseElement el = rotateCtrl.OwnerElement;
                    
                    //IMPORTANT TODO SET ROTATION ANGLE
                    el.Size = DiagramUtil.RoundSize(el.Size, document.GridSize);
                }

                //Events
                ElementEventArgs eventResizeArg = new ElementEventArgs(rotateCtrl.OwnerElement);
                onElementRotatingDelegate(eventResizeArg);

                isRotating = false;
            }
        }


        //Función para Dibujar los Rectangulitos de Rotación
        public void DrawRotateCorner(Graphics g)
        {
            if (rotateCtrl != null)
            {
                foreach (RectangleElement r in rotateCtrl.Corners)
                {
                    if (document.Action == DesignerAction.Select)
                    {
                        if (r.Visible) r.Draw(g);
                    }
                    else if (document.Action == DesignerAction.Connect)
                    {
                        // if is Connect Mode, then resize only Links.
                        if (rotateCtrl.OwnerElement is BaseLinkElement)
                            if (r.Visible) r.Draw(g);
                    }
                }
            }
        }

        public void UpdateRotateCorner()
        {
            if (rotateCtrl != null)
                rotateCtrl.UpdateCornersPos();
        }

        public Cursor UpdateRotateCornerCursor(Point mousePoint)
        {
            if ((rotateCtrl == null) || (!rotateCtrl.CanRotate)) return Cursors.Default;

            CornerPosition corPos = rotateCtrl.HitTestCorner(mousePoint);

            switch (corPos)
            {
                case CornerPosition.TopLeft:
                    return Cursors.SizeNWSE;

                case CornerPosition.TopCenter:
                    return Cursors.SizeNS;

                case CornerPosition.TopRight:
                    return Cursors.SizeNESW;

                case CornerPosition.MiddleLeft:
                case CornerPosition.MiddleRight:
                    return Cursors.SizeWE;

                case CornerPosition.BottomLeft:
                    return Cursors.SizeNESW;

                case CornerPosition.BottomCenter:
                    return Cursors.SizeNS;

                case CornerPosition.BottomRight:
                    return Cursors.SizeNWSE;
                default:
                    return Cursors.Default;
            }
        }

        //Show corners for rotation
        public void ShowRotateCorner(bool show)
        {
            if (rotateCtrl != null)
            {
                bool canRotate = rotateCtrl.CanRotate;
                for (int i = 0; i < rotateCtrl.Corners.Length; i++)
                {
                    if (canRotate)
                        rotateCtrl.Corners[i].Visible = show;
                    else
                        rotateCtrl.Corners[i].Visible = false;
                }
                if (rotateCtrl.Corners.Length >= (int)CornerPosition.MiddleCenter)
                    rotateCtrl.Corners[(int)CornerPosition.MiddleCenter].Visible = false;
            }
        }
    }
}
