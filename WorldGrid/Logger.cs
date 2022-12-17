using System.Collections.Generic;
using System.Linq;

namespace WorldGrid
{
    public class Logger
    {
        private static Logger _logger;

        public static Logger Instance => _logger ??= new Logger();

        private Dictionary<string, string> _logDictionary;

        public Logger()
        {
            _logDictionary = new Dictionary<string, string>();
        }

        public void AddLog(string logKey, string logMessege)
        {
            if (!_logDictionary.ContainsKey(logKey))
            {
                _logDictionary.Add(logKey, logMessege);
            }
            else
            {
                var currentLog = _logDictionary[logKey];
                currentLog += "\n";
                currentLog += logMessege;

                _logDictionary[logKey] = currentLog;
            }
        }
    }
}