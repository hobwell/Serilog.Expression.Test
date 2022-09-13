using Serilog;
using Serilog.Templates;
using System;
namespace SerilogExpressionTest {
    class Program {

        private static Serilog.Core.Logger Logger;

        static void Main(string[] args) {
            SetLogger();
            Logger.Error("Test");
            Console.ReadKey();
        }

        /// <summary>
        /// Initialize the Logger with a new Logger configuration
        /// </summary>
        private static void SetLogger() {
            LoggerConfiguration config = new LoggerConfiguration()
            .Enrich.With(new Enricher())
            .WriteTo.Console(new ExpressionTemplate("QueryString[?] like '%DROP%' ci: {#if QueryString[?] like '%DROP%' ci}True{#else}False{#end}\n\n"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, levelSwitch: null)
            .WriteTo.Console(new ExpressionTemplate("QueryString['y'] like '%DROP%' ci: {#if QueryString['y'] like '%DROP%' ci}True{#else}False{#end}\n\n"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, levelSwitch: null)
            .WriteTo.Console(new ExpressionTemplate("{@p}\n\n"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose, levelSwitch: null);
            Logger = config.CreateLogger();
        }

    }

}
