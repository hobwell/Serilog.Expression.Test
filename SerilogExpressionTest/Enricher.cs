using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Events;
using System.Collections.Generic;
using System.Diagnostics;

namespace SerilogExpressionTest {
    public class Enricher : ILogEventEnricher {

        private static Dictionary<string, string> QueryString {
            get {
                return new Dictionary<string, string>() {
                    { "x", "NULL" },
                    { "y", "DROP" }
                };
            }
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("QueryString", QueryString, false));

            Debug.WriteLine(JsonConvert.SerializeObject(logEvent));
        }
    }
}