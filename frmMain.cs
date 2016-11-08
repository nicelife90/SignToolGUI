using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SignToolGUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtCertificate.Text = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "SignToolGUI - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void btnApplication_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtApplication.Text = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "SignToolGUI - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtCertificate.Text == "" || txtApplication.Text == "")
                {
                    MessageBox.Show(
                        this,
                        "All fields is required except cetificate password!",
                        "SignToolGUI - Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                string cmd = " sign /d \"" + txtName.Text + "\" /f \"" + txtCertificate.Text + "\" /p " + txtPassword.Text + " \"" + txtApplication.Text + "\"";

                Process p = new Process();
                ProcessStartInfo pi = new ProcessStartInfo();
                pi.Arguments = cmd;
                pi.FileName = Application.StartupPath + "\\Resources\\signtool.exe";
                pi.RedirectStandardError = true;
                pi.RedirectStandardOutput = true;
                pi.UseShellExecute = false;
                p.StartInfo = pi;
                p.Start();
                string stderr = p.StandardError.ReadToEnd();
                p.WaitForExit();

                if (p.HasExited)
                {
                    if (p.ExitCode == 0)
                    {
                        MessageBox.Show(
                                      this,
                                      "Your application has been successfully signed!",
                                      "SignToolGUI - Success",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                                         this,
                                         stderr,
                                         "SignToolGUI - Error",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                                       this,
                                       ex.Message,
                                       "SignToolGUI - Error",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
            }
        }
    }
}
