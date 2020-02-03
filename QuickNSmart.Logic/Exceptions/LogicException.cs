//@QnSBaseCode
//MdStart
using System;

namespace QuickNSmart.Adapters.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Stellt Fehler dar, die beim Ausf�hren der Anwendung auftreten.
    /// </summary>
    public partial class LogicException : Exception
    {
        public int ErrorId { get; } = -1;

        /// <summary>
        /// Initialisiert eine neue Instanz der AdapterException-Klasse 
        /// mit einer angegebenen Fehlermeldung.
        /// </summary>
        /// <param name="identity">Identification der Fehlermeldung.</param>
        /// <param name="message">Die Meldung, in der der Fehler beschrieben wird.</param>
        public LogicException(int identity, string message)
            : base(message)
        {
            ErrorId = identity;
        }

        public LogicException(Exception ex)
            : base(ex.Message, ex.InnerException)
        {
        }
    }
}
//MdEnd