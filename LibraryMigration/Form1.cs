// <copyright file="LibraryMigrationForm.cs" company="Private">
// Copyright (c) 2012 All Rights Reserved
// </copyright>
// <author>Christopher Simusokwe</author>
// <date>2012-12-07</date>
// <summary>This contains the LibraryMigration.</summary>

namespace LibraryMigration
{
    using System;
    using System.Windows.Forms;
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;
    using Microsoft.SharePoint;

    public partial class LibraryMigrationForm : Form
    {
        public LibraryMigrationForm()
        {
            InitializeComponent();
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtsourceWeb.Text))
            {
                if (!String.IsNullOrEmpty(txtdestinationWeb.Text))
                {
                    txtdestinationWeb.Enabled = false;
                    txtsourceWeb.Enabled = false;
                    btnMigrate.Enabled = false;
                     
                    SPSite sourceSite = new SPSite(txtsourceWeb.Text);
                    SPSite destinationSite = new SPSite(txtdestinationWeb.Text);

                    SPWeb sourceWeb = sourceSite.OpenWeb();
                    SPWeb destinationWeb = destinationSite.OpenWeb();

                    string exportString = string.Empty;
                    string importString = string.Empty;
                    string sWeb = string.Empty;
                    string dWeb = string.Empty;
                    string filename = string.Empty;
                    string library = string.Empty;
                    string message = string.Empty;
                    int count = 0;
                    string[] newLines = new string[2];

                    SPListCollection ListCollection = sourceWeb.Lists;

                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 100;

                    try
                    {
                        #region Enumerate and Export Document Libraries

                        foreach (SPList list in ListCollection)
                        {
                            if (list.BaseType == SPBaseType.DocumentLibrary && list.Title != "Master Page Gallery"
                                && list.Title != "Form Templates" && list.Title != "Site Assets"
                                && list.Title != "Site Collection Documents" && list.Title != "Style Library"
                                && list.Title != "Site Pages" && list.Title != "Customized Reports"
                                && list.Title != "Site Collection Images" && list.Hidden != true)
                            {
                                sWeb = sourceWeb.Url.ToString();
                                dWeb = destinationWeb.Url.ToString();
                                filename = @"C:\LibraryMigration\" + sourceWeb.Title + list.Title + ".dat";
                                library = "/" + list.Title;

                                exportString = @"C:\LibraryMigration\Export.ps1";
                                RunScript(exportString, sWeb, dWeb, filename, library);

                                importString = @"C:\LibraryMigration\Import.ps1";
                                RunScript(importString, sWeb, dWeb, filename, library);
                                count++;

                                progressBar1.Value = (count / ListCollection.Count) * 100;
                                label5.Text = progressBar1.Value + "%";
                                message += count + ". " + list.Title + " Library from " + sourceWeb.Title + " Site Migrated at " + DateTime.Now + Environment.NewLine;

                                Log.WriteToEventLog(count + ". " + list.Title + " Library from " + sourceWeb.Title + " Site Migrated at " + DateTime.Now, "LibraryMigrationTool");
                            }
                        }

                        txtDisplay.Text = message + Environment.NewLine + "Done...";
                        progressBar1.Value = 100;
                        label5.Text = progressBar1.Value + "%";

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        Log.WriteToEventLog(ex.Message + " at " + DateTime.Now, "LibraryMigrationTool");
                    }

                    txtdestinationWeb.Enabled = true;
                    txtsourceWeb.Enabled = true;
                    btnMigrate.Enabled = true;

                    sourceSite.Dispose();
                    destinationSite.Dispose();
                }                            
                else
                {
                    MessageBox.Show("The Destination Url text field is null or empty.");
                }

            }
            else
            {
                MessageBox.Show("The Source Url text field is null or empty.");
            }
        }

        /// <summary>
        /// Runs the given powershell script and returns the script output.
        /// </summary>
        /// <param name="scriptText">the powershell script text to run</param>
        /// <returns>output of the script</returns>
        private static void RunScript(string scriptText, string sourceWeb, string destinationWeb, string filename, string library)
        {
            // create Powershell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it
            runspace.Open();
            runspace.SessionStateProxy.SetVariable("sourceWeb", sourceWeb);
            runspace.SessionStateProxy.SetVariable("destinationWeb", destinationWeb);
            runspace.SessionStateProxy.SetVariable("filename", filename);
            runspace.SessionStateProxy.SetVariable("library", library);

            // create a pipeline and feed it the script text
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);

            pipeline.Commands.Add("Out-String");

            // execute the script
            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace
            runspace.Close();
        }
    }
}
