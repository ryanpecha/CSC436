using GitVisualizer.UI;
using GitVisualizer.UI.UI_Forms;
using System.Diagnostics;

using GithubSpace;

namespace GitVisualizer
{
    /// <summary>
    /// Code for the Workspace Form Window, including event handlers, color theme settings, and component initialization
    /// </summary>
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
            this.FormClosing += new FormClosingEventHandler(LoadMainAppFormLocal); // Open main window when closing this one, skipping Auth
            Debug.Write("Setup Form opened\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event handler that highlights the Github login button to signify login is required for user's desired use case
        /// </summary>
        private void HighlightLoginButton(object sender, EventArgs e)
        {
            githubLoginButton.Select();
        }
        /// <summary>
        /// Event handler that highlights the Local Workspace button to signify no login is required for user's desired use case
        /// </summary>
        private void HighlightLocalButton(object sender, EventArgs e)
        {
            localWorkspaceButton.Select();
        }

        /// <summary>
        /// Loads main page routed from Remote login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadMainAppFormRemote(object sender, EventArgs e)
        {
            GetPermissionGithub();
            OpenExternalWebsite(Github.deviceLoginCodeURL);

            //this.Hide();
        }

        /// <summary>
        /// Requests Github App for permission, and shows user code on window. Waits until user authorizes OAuth app, 
        /// then on success closes window to open main app
        /// </summary>
        private async void GetPermissionGithub()
        {
            authorizationPanel.Visible = true;
            await Program.Github.GivePermission();
            ShowUserCode(Github.userCode);


            if (Github.userCode != null)
            {
                Debug.Write(Github.userCode);
                await Program.Github.WaitForAuthorization();
            }

            this.Hide();
        }

        private void ShowUserCode(string userCode)
        {
            userCodeLabel.Text = userCode;
            userCodeLabel.Visible = true;
        }

        private void OpenExternalWebsite(string siteURL)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = siteURL,
                UseShellExecute = true
            });
        }
        /// <summary>
        /// Loads main page routed from local repository button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadMainAppFormLocal(object sender, EventArgs e)
        {

            this.Hide();
        }

        /// <summary>
        /// Changes Github remember user auth bool based on result of remember me checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RememberMeCheckboxChanged(object sender, EventArgs e)
        {
            bool toRemember = rememberMeCheckbox.Checked;
            Program.Github.SetRememberUserAccessBool(toRemember);
            Debug.WriteLine("Remember user? " + Program.Github.RememberUserAccess);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void userCodeLabelHeader_Click(object sender, EventArgs e)
        {

        }

        private void rememberMeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}