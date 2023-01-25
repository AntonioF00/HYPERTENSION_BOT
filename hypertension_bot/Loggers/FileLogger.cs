using System;
using System.IO;

namespace hypertension_bot.Loggers
{
    public class FileLogger : LogBase
    {
        public string filePath = AppDomain.CurrentDomain.BaseDirectory;

        public override void Log(string message)
        {
            if (!File.Exists(filePath + @"\log.txt"))
                File.CreateText(filePath + @"\log.txt");

            lock (lockObj)
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath + @"\log.txt"))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
        }
    }
}
