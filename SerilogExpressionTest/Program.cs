using Destructurama;
using Serilog;
using Serilog.Templates;

namespace SerilogExpressionTest {
    class Program {

        private static Serilog.Core.Logger Logger;

        static void Main(string[] args) {
            SetLogger();
            Logger.Error("Test");
            SetDestructuringLogger();
            Logger.Error("Test");
        }

        /// <summary>
        /// Initialize the Logger with a new Logger configuration
        /// </summary>
        private static void SetLogger() {
            LoggerConfiguration config = new LoggerConfiguration()
            .Destructure.JsonNetTypes()
            .Enrich.With(new Enricher())
            .WriteTo.Debug(new ExpressionTemplate("QueryString[?] like '%DROP%' ci: {#if QueryString[?] like '%DROP%' ci}True{#else}False{#end}\n\n{@p}\n\n"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, levelSwitch: null);

            Logger = config.CreateLogger();
        }

        /// <summary>
        /// Initialize the Logger with a new Logger configuration
        /// </summary>
        private static void SetDestructuringLogger() {
            LoggerConfiguration config = new LoggerConfiguration()
            .Destructure.JsonNetTypes()
            .Enrich.With(new DestructuringEnricher())
            .WriteTo.Debug(new ExpressionTemplate("QueryString[?] like '%DROP%' ci: {#if QueryString[?] like '%DROP%' ci}True{#else}False{#end}\n\n{@p}\n\n"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, levelSwitch: null);

            Logger = config.CreateLogger();
        }

    }

}
