using OxyPlot;
using OxyPlot.Annotations;


namespace Sinus
{
    public class PlotViewModel
    {
        public PlotModel Model { get; private set; }

        public PlotViewModel()
        {
            this.Model = new PlotModel() { Title = "Plot" };
            
        }



    }
}
