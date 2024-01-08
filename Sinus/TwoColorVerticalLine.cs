using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinus
{
    public class TwoColorVerticalLine : TwoColorLineSeries
    {
        public TwoColorVerticalLine()
        {
        }

        protected override void RenderLine(IRenderContext rc, IList<ScreenPoint> pointsToRender)
        {
            var clippingRect = this.GetClippingRect();
            var p1 = this.InverseTransform(clippingRect.BottomLeft);
            var p2 = this.InverseTransform(clippingRect.TopRight);
            
            var clippingRectLo = new OxyRect(
                this.Transform(Math.Min(p1.X, p2.X), p1.Y),
                this.Transform(this.Limit,p2.Y )).Clip(clippingRect);

            var clippingRectHi = new OxyRect(
                this.Transform(Math.Max(p1.X, p2.X), p1.Y),
                this.Transform(this.Limit, p2.Y)).Clip(clippingRect);

            if (this.StrokeThickness <= 0 || this.ActualLineStyle == LineStyle.None)
            {
                return;
            }

            void RenderLine(OxyColor color)
            {
                rc.DrawReducedLine(
                    pointsToRender,
                    this.MinimumSegmentLength * this.MinimumSegmentLength,
                    this.GetSelectableColor(color),
                    this.StrokeThickness,
                    this.EdgeRenderingMode,
                    this.ActualDashArray,
                    this.LineJoin);
            }

            using (rc.AutoResetClip(clippingRectLo))
            {
                RenderLine(this.ActualColor);
            }

            using (rc.AutoResetClip(clippingRectHi))
            {
                RenderLine(this.ActualColor2);
            }
        }
    }
}
