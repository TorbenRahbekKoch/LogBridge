using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

namespace SoftwarePassion.LogBridge
{
    internal static class ThreadPrincipal
    {
        public static string Resolve(bool allowTraceMessage)
        {
            // add data related to the current principal
            string threadPrincipal = string.Empty;
            try
            {
                IPrincipal principal = Thread.CurrentPrincipal;
                if (principal != null)
                {
                    IIdentity identity = principal.Identity;
                    if (identity != null)
                    {
                        threadPrincipal = identity.Name;
                    }
                }
            }
            catch (Exception exception)
            {
                if (allowTraceMessage)
                    Trace.WriteLine("Exception resolving thread principal. " + exception.ToString());
                // Nothing much we can do here. Just don't crash.
            }

            return threadPrincipal;
        }
    }
}