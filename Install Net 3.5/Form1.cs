using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Install_Net_3._5
{
    public partial class Form1 : Form
    {
        readonly bool ru = true;
        public Form1()
        {
            InitializeComponent();
            if (!CultureInfo.CurrentUICulture.ToString().Contains("ru-RU"))
                ru = false;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            if (MessageBox.Show($"{(ru ? "Начать установку" : "Start installation")} Net 3.5?", "Install Net 3.5", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Directory.CreateDirectory("C:\\35");
                File.WriteAllBytes("C:\\35\\microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab", Properties.Resources.microsoft_windows_netfx3_ondemand_package_31bf3856ad364e35_amd64__);
                Process.Start(new ProcessStartInfo { FileName = "cmd", Arguments = "/c dism /online /enable-feature /featurename:NetFX3 /source:C:\\35 /limitaccess" }).WaitForExit();
                MessageBox.Show($"{(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5")?.GetValue("Install") != null ? $"Net Framework 3.5 {(ru ? "успешно установлен" : "installed successfully")}" : $"{(ru ? "Что-то пошло не так" : "Something went wrong")}")}", "Install Net 3.5", MessageBoxButtons.OK);
                Directory.Delete("C:\\35", true);
                Close();
            }
            else Close();
        }
    }
}
