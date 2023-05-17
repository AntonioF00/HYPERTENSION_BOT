using hypertension_bot.Loggers;
using System;

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
                double[] dataY = new double[100];
                double[] dataX = new double[100];

                foreach(var e in list)
                {
                    var i = 0;
                    //SISTEMATE IL DATAX[i]
                    dataX[i] = DateTime.Parse(e["datetime"].ToString()).ToOADate();
                    dataY[i] = Double.Parse(e["systolic"].ToString());
                    i++;
                }

                var plt = new ScottPlot.Plot(400, 300);
                plt.AddScatter(dataX,dataY);
                plt.SaveFig($"grafico_{_id}.png");

            }catch(Exception ex)
            {
                LogHelper.Log($"{System.DateTime.Now} | Message: {ex.Message} | StackTrace: {ex.StackTrace}");
            }
        }
    }
}
