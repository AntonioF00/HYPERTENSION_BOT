namespace hypertension_bot.Services
{
    internal class ChartWorker
    {
        /// <summary>
        /// using ScottPlot nuget package
        /// https://scottplot.net/quickstart/console/
        /// </summary>
        public ChartWorker() { }

        public void Run(List<Dictionary<string, object>> list)
        {
            double[] dataX = new double[] { };
            double[] dataY = new double[] { };

            foreach(var e in list)
            {
                dataX[list.IndexOf(e)] = Double.Parse(e["data"].ToString());
                dataY[list.IndexOf(e)] = Double.Parse(e["systolic"].ToString());
            }

            var plt = new ScottPlot.Plot(400, 300);
            plt.AddScatter(dataX, dataY);
            plt.SaveFig("grafico.png");
        }
    }
}
