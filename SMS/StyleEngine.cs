using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using libSnarlStyles;
using Microsoft.Win32;
using System.Reflection;
using prefs_kit_d2;
using Winkle;

namespace SMS
{

    [Guid("AD28BB1F-E908-44C6-A439-E65EDFF6439B"), ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual), ComSourceInterfaces(typeof(libSnarlStyles.IStyleEngine))]
    [ProgId("SMS.styleengine")]    
    public class styleengine : IStyleEngine
    {
        private int hwndOfPrferencesWindows = 0;
        private Options myPrefWindow;
               
        public styleengine() {
            myPrefWindow = new Options();
            hwndOfPrferencesWindows = (int)myPrefWindow.Handle;
            myPrefWindow.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Options_FormClosed);

            Winkle.VersionCheck myUpdateChecker = new Winkle.VersionCheck("SMS", "http://tlhan-ghun.de/files/SMSstyle.xml");
            Winkle.UpdateInfo myUpdateResponse = myUpdateChecker.checkForUpdate(System.Reflection.Assembly.GetExecutingAssembly(), false);
        }

        private void Options_FormClosed(Object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            myPrefWindow = new Options();
            hwndOfPrferencesWindows = (int)myPrefWindow.Handle;
            myPrefWindow.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Options_FormClosed);  
        }

        #region IStyleEngine Members

        [ComVisible(true)]
        int IStyleEngine.CountStyles()
        {
            return 1;
        }

        [ComVisible(true)]
        IStyleInstance IStyleEngine.CreateInstance(string StyleName)
        {
            StyleInstance myInstance = new StyleInstance();
            return myInstance;

        }

        [ComVisible(true)]
        string IStyleEngine.Date()
        {
            return "2009-12-24";
        }

        [ComVisible(true)]
        string IStyleEngine.Description()
        {
            return "Sends the notification to a cell phone using a SMS by http://www.clickatell.com";
        }

        [ComVisible(true)]
        int IStyleEngine.GetConfigWindow(string StyleName)
        {
            return hwndOfPrferencesWindows;
        }

        [ComVisible(true)]
        melon.M_RESULT IStyleEngine.Initialize()
        {
            return melon.M_RESULT.M_OK;
        }

        [ComVisible(true)]
        string IStyleEngine.LastError()
        {
            return "Undefined error";
        }

        [ComVisible(true)]
        string IStyleEngine.Name()
        {
            return "SMS (www.clickatell.com)";

        }

        [ComVisible(true)]
        string IStyleEngine.Path()
        {
            return Assembly.GetExecutingAssembly().CodeBase;
       
        }

        [ComVisible(true)]
        int IStyleEngine.Revision()
        {
            return 3;
        }

        [ComVisible(true)]
        void IStyleEngine.StyleAt(int Index, ref style_info Style)
        {
            string pathToIcon = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\SMS.ico";
            pathToIcon = pathToIcon.Replace("file:\\", "");

            Style.Flags = S_STYLE_FLAGS.S_STYLE_IS_WINDOWLESS | S_STYLE_FLAGS.S_STYLE_IS_CONFIGURABLE;
            Style.IconPath = pathToIcon;
            Style.Major = Assembly.GetExecutingAssembly().GetName().Version.Major;
            Style.Minor = Assembly.GetExecutingAssembly().GetName().Version.Minor;
            Style.Name = "SMS";
            Style.Path = Assembly.GetExecutingAssembly().CodeBase;
            Style.Schemes = "Default";
            Style.Copyright = "Tlhan Ghun";
            Style.SupportEmail = "info@tlhan-ghun.de";
            Style.URL = "http://tlhan-ghun.de/";
            Style.Description = "Sends the notification to a cell phone using a SMS by http://www.clickatell.com";
        }

        [ComVisible(true)]
        void IStyleEngine.TidyUp()
        {
            myPrefWindow.Close();
            return;
        }

        [ComVisible(true)]
        int IStyleEngine.Version()
        {
            return 39;
        }

        #endregion

        #region COM Registration Methods


        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            StringBuilder skey = new StringBuilder(key);
            skey.Replace(@"HKEY_CLASSES_ROOT\", "");
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(skey.ToString(), true);
            RegistryKey ctrl = regKey.CreateSubKey("Control");
            ctrl.Close();
            RegistryKey inprocServer32 = regKey.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();
            regKey.Close();
        }


        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder skey = new StringBuilder(key);
            skey.Replace(@"HKEY_CLASSES_ROOT\", "");
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(skey.ToString(), true);
            regKey.DeleteSubKey("Control", false);
            RegistryKey inprocServer32 = regKey.OpenSubKey("InprocServer32", true);
            regKey.DeleteSubKey("CodeBase", false);
            regKey.Close();
        }
        #endregion
    }
}