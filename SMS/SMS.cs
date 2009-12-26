using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

/* set project type to console application to test the options dialog */

namespace SMS
{
    class SMS
    {
        static void Main()
        {


            Options myPrefWnd = new Options();
                myPrefWnd.Show();
         
            Application.Run();
        }
    }
}
