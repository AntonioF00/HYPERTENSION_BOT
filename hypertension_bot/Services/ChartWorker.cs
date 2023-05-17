using hypertension_bot.Loggers;
using ScottPlot;
using System;
using static ScottPlot.Plottable.PopulationPlot;

namespace hypertension_bot.Services
{
    internal class ChartWorker
    {
        /// <summary>
        /// using ScottPlot nuget package
        /// https://scottplot.net/quickstart/console/
        /// </summary>
        /// 

        public long _id { get; set; }

        public ChartWorker() { }

        public void Run(List<Dictionary<string, object>> list)
        {
            try
            {
                double[] dataYs = new double[list.Count];
                double[] dataYd = new double[list.Count];
                string[] dataXLabel = new string[list.Count];                
                
                int y = 0;
                foreach(var e in list)
                {
                    dataXLabel[y] = e["datetime"].ToString();
                    dataYs[y] = Double.Parse(e["systolic"].ToString());
                    dataYd[y] = Double.Parse(e["diastolic"].ToString());
                    y++;
                }
                y = 0;

                double[] xPositions = new double[list.Count];
                string[] xLabels = new string[list.Count];

                foreach (var e in dataYs)
                {
                    xPositions[y] = y;
                    xLabels[y] = dataXLabel[y];
                    y++;
                }

                var plt = new ScottPlot.Plot(600, 400);
                plt.AddSignal(dataYs, label: "Sistolic");
                plt.AddSignal(dataYd, label: "Diastolic");
                plt.XAxis.ManualTickPositions(xPositions, xLabels);
                plt.Title("Sistolic/Diastolic");
                plt.SaveFig($"grafico_{_id}.png");

            }
            catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | Message: {ex.Message} | StackTrace: {ex.StackTrace}");
            }
        }
    }
}
