using System;
using System.Collections.Generic;
using System.Text;

namespace OS
{
    class operatingSystem
    {
        private static string _OSstring = "";
        private static int _OS = 0;
        private const int OSwin32 = 1;
        private const int OSunix = 2;

        public operatingSystem()
        {
            _OS = OSwin32;         
            _OSstring = "Win32";
            if (Environment.NewLine == "\n")
            {
                _OSstring = "Unix";
                _OS = OSunix;
            }
        }

        public string OSstring
        {
            get { return _OSstring; }
        }

        public int OS
        {
            get { return _OS; }
        }
	
    }
}
