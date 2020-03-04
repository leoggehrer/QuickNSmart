﻿//@QnSBaseCode
//MdStart
using System;
using QuickNSmart.Logic;

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
        /// Initialisiert eine neue Instanz der LogicException-Klasse 
        /// mit einer angegebenen Fehlermeldung.
        /// </summary>
        /// <param name="errorType">Identification der Fehlermeldung.</param>
        public LogicException(ErrorType errorType)
            : base(ErrorMessage.GetAt(errorType))
        {
            ErrorId = (int)errorType;
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der LogicException-Klasse 
        /// mit einer angegebenen Fehlermeldung.
        /// </summary>
        /// <param name="errorType">Identification der Fehlermeldung.</param>
        /// <param name="message">Die Meldung, in der der Fehler beschrieben wird.</param>
        public LogicException(ErrorType errorType, string message)
            : base(message)
        {
            ErrorId = (int)errorType;
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der LogicException-Klasse 
        /// mit einer angegebenen Fehlermeldung.
        /// </summary>
        /// <param name="errorType">Identification der Fehlermeldung.</param>
        /// <param name="ex">Exception die aufgetreten ist.</param>
        public LogicException(ErrorType errorType, Exception ex)
            : base(ex.Message, ex.InnerException)
        {
            ErrorId = (int)errorType;
        }
    }
}
//MdEnd