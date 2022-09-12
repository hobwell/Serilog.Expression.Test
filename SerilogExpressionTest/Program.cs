using Destructurama;
using Serilog;
using Serilog.Templates;

namespace SerilogExpressionTest {
    class Program {

        private static Serilog.Core.Logger Logger;

        static void Main(string[] args) {
            SetLogger();
            Logger.Error("Test");
        }

        /// <summary>
        /// Overwrite the existing Logger with a new Logger configuration
        /// </summary>
        private static void SetLogger() {
            //intialize the static logger using an enricher to populate the custom columns and event propeties
            //note: this will only filter using the global list of exception filters.  Use the Init() method to add project level exception filtering
            //note: the LevelSwitch seems to apply across all sinks regardless of where it is attached
            LoggerConfiguration config = new LoggerConfiguration()
            .Destructure.JsonNetTypes()
            .Enrich.With(new Enricher())
            .WriteTo.Debug(new ExpressionTemplate("QueryString[?] like '%NULL%' ci: {#if QueryString[?] like '%NULL%' ci}True{#else}False{#end}\n\n"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, levelSwitch: null);

            Logger = config.CreateLogger();
        }

    }

}
