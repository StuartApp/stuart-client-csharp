using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StuartDelivery
{
    public static class Version
    {

        public static string GetCurrent()
        {
            try {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch (Exception) {
                // Use default version
                return "1.0";
            }
        }
    }
}
