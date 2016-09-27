// <copyright file="Log.cs" company="GTConsult">
// Copyright (c) 2012 All Rights Reserved
// </copyright>
// <author>Christopher Simusokwe</author>
// <date>2012-12-07</date>
// <summary>This contains the LibraryMigration.</summary>

namespace LibraryMigration
{
    using System.Diagnostics;

    /// <summary>
    /// This class logs to the windows event log.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// This method writes to the event log.
        /// </summary>
        /// <param name="message">The message written to the event log.</param>
        /// <param name="appName">The Application writing to the event log.</param>
        public static void WriteToEventLog(string message, string appName)
        {
            string cs = appName;
            EventLog elog = new EventLog();

            if (!EventLog.SourceExists(cs))
            {
                EventLog.CreateEventSource(cs, cs);
            }

            elog.Source = cs;
            elog.EnableRaisingEvents = true;
            elog.WriteEntry(message);
        }
    }
}
