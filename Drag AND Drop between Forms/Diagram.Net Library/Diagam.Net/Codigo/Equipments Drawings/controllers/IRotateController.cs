using System;
using System.Drawing;
namespace Dalssoft.DiagramNet
{
    /// <summary>
    /// If a class controller implements this interface, it can resize the element.
    /// </summary>
    internal interface IRotateController : IController
    {
        RectangleElement[] Corners { get; }
        void UpdateCornersPos();

        CornerPosition HitTestCorner(Point p);

        void Start(Point posStart, CornerPosition corner);

        void Rotate(Point posCurrent);

        void End(Point posEnd);

        bool IsRotating { get; }

        bool CanRotate { get; }

    }
}

