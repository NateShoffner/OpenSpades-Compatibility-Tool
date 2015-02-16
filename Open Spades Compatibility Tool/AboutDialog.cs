#region

using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace OpenSpadesCompatibilityTool
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            label1.Text += string.Format(" v{0}", Common.TruncateVersion(Application.ProductVersion));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel) sender).Text);
        }
    }
}