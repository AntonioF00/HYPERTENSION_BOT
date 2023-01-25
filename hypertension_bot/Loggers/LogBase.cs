namespace hypertension_bot.Loggers
{
    public abstract class LogBase
    {
        public abstract void Log(string message);
        protected readonly object lockObj = new object();
    }
}
