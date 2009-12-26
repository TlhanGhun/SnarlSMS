using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using libSnarlStyles;
using System.Net;
using System.IO;

namespace SMS
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]

    public class StyleInstance : IStyleInstance
    {
        

        public StyleInstance()
        {

        }

         

        #region IStyleInstance Members

        [ComVisible(true)]
        void IStyleInstance.AdjustPosition(ref int x, ref int y, ref short Alpha, ref bool Done)
        {
            return;
        }

        [ComVisible(true)]
        melon.MImage IStyleInstance.GetContent()
        {
            
            throw new NotImplementedException();
        }

        [ComVisible(true)]
        bool IStyleInstance.Pulse()
        {
            return false;
            throw new NotImplementedException();
        }

        [ComVisible(true)]
        void IStyleInstance.Show(bool Visible)
        {
            return;
            throw new NotImplementedException();
        }

        [ComVisible(true)]
        void IStyleInstance.UpdateContent(ref notification_info NotificationInfo)
        {
            if (Properties.Settings.Default.username == string.Empty || Properties.Settings.Default.password == string.Empty || Properties.Settings.Default.phonenumber == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("Please setup your credentials and phonenumber in the style settings first");
                return;
            }
            WebClient client = new WebClient ();
            // Add a user agent header in case the requested URI contains a query.
            client.Headers.Add ("user-agent", "Snarl style to send SMS");
            client.QueryString.Add("user", Properties.Settings.Default.username);
            client.QueryString.Add("password", Properties.Settings.Default.password);
            client.QueryString.Add("api_id", "3212636");
            client.QueryString.Add("to", Properties.Settings.Default.phonenumber);
            client.QueryString.Add("text", NotificationInfo.Title + " :" + NotificationInfo.Text);
            string baseurl ="http://api.clickatell.com/http/sendmsg";
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader (data);
            string s = reader.ReadToEnd ();
            data.Close ();
            reader.Close ();
            if(!s.ToLower().StartsWith("id")) {
                System.Windows.Forms.MessageBox.Show("Error in sending - response from clickatell:\n" + s);
            }
            return;
        }

        #endregion


    }
}