using System;

namespace AplikasiInventarisToko.Utils
{
    public static class Contract
    {
        public static void Requires<TException>(bool condition, string message) where TException : Exception
        {
            if (!condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }
    }
}