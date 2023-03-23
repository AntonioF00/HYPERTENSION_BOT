using hypertension_bot.Loggers;

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
            try
            {
                double[] dataY = new double[] { };
                double[] dataX = new double[] { };

                foreach(var e in list)
                {
                    dataX[list.IndexOf(e)] = Double.Parse(e["data"].ToString());
                    dataY[list.IndexOf(e)] = Double.Parse(e["systolic"].ToString());
                }

                var plt = new ScottPlot.Plot(400, 300);
                plt.AddScatter(dataX, dataY);
                plt.SaveFig("grafico.png");

            }catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | Message: {ex.Message} | StackTrace: {ex.StackTrace}");
            }
        }
    }
}
