﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitVisualizer.UI.UI_Forms
{
    public partial class MainForm : Form
    {
        private bool hasCredentials = false;
        public UITheme.AppTheme AppTheme = UITheme.DarkTheme;
        private Github githubAPI;

        private RepositoriesControl repositoriesControl = new();
        private BranchesControl branchesControl = new();
        public MainForm()
        {
            githubAPI = Program.Github;
            InitializeComponent();
            ApplyColorTheme(AppTheme);
            CheckValidation();
            this.Activated += PopulateReposTable;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Checks if user has already logged in with this device, and show login page if not
        /// </summary>
        private void CheckValidation()
        {
            if (!hasCredentials)
            {
                SetupForm setup = new SetupForm();
                this.Hide();
                // Shows setup page as dialog, so closing it returns here
                setup.ShowDialog();
                this.SetVisibleCore(false);
                ShowControlInMainPanel(repositoriesControl);
            }
        }

        public void OnRepositoriesButtonPress(object sender, EventArgs e)
        {
            ShowControlInMainPanel(repositoriesControl);
        }
        public void OnBranchesButtonPress(object sender, EventArgs e)
        {
            ShowControlInMainPanel(branchesControl);
        }
        public void OnMergingButtonPress(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
        }

        /// <summary>
        /// Clears current main panel control and swaps it for given User Control view
        /// </summary>
        /// <param name="control"></param>
        private void ShowControlInMainPanel(UserControl control)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(control);
            control.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Re opens the main window after it has been hidden
        /// </summary>
        public void ReOpenWindow()
        {
            Debug.WriteLine("REOPEN");
            this.Show();
            this.SetVisibleCore(true);
            this.MaximizeBox = true;
        }

        public void PopulateReposTable(object sender, EventArgs e)
        {

            GetRepositoriesData();
        }

        private async void GetRepositoriesData()
        {
            await githubAPI.GetRepositories();
            string repositoriesJSONContents = githubAPI.repoList;
            if (repositoriesJSONContents == null) { return; }
            string[] reposByName = repositoriesJSONContents.Split("\"name\":");
            foreach (string reposName in reposByName)
            {
                Debug.WriteLine(reposName[..16]);
            }

        }

    }
}
