using System;
using System.Windows.Forms;

namespace RecurringIntegrationApp
{
    public partial class DIXFRecurringJobsProcessor : Form
    {
        private readonly object stateLock = new object();

        private FileProcessor fileProcessor;
           
        public DIXFRecurringJobsProcessor()
        {
            InitializeComponent();

            this.InitializeSettings();
        }

        private void InitializeSettings()
        {
            this.inputLocationTextBox.Text = Settings.InputDir;            

            this.inProcessLocationTextBox.Text = Settings.InProcessDir;

            this.successLocationTextBox.Text = Settings.SuccessDir;

            this.failedLocationTextBox.Text = Settings.ErrorDir;

            this.statusPollingIntervalTextBox.Text = Settings.StatusPollingInterval.ToString();

            this.aadTenantTextBox.Text = Settings.AadTenant;

            this.azureAuthEndpointTextBox.Text = Settings.AzureAuthEndpoint;

            this.ax7EndpointTextBox.Text = Settings.RainierUri;

            this.ax7UserNameTextBox.Text = Settings.RainierUserName;

            this.ax7UserpasswordTextBox.Text = Settings.RainierUserPassword;

            this.clientAppIdtextBox.Text = Settings.ClientId;

            this.recurringJobQueueIdTextBox.Text = Settings.RecurringJobId.ToString();
            this.recurringJobQueueIdTextBox.TextChanged += RecurringJobQueueIdTextBox_TextChanged;

            this.entityNameTextBox.Text = Settings.EntityName;            

            this.companyTextBox.Text = Settings.Company;            
            
            this.isDataPackageInputCheckBox.Checked = Settings.IsDataPackage;            

            this.stopButton.Enabled = false;
            
        }
    
        private void RecurringJobQueueIdTextBox_TextChanged(object sender, EventArgs e)
        {
            Guid _guid = Guid.Empty;
            if(Guid.TryParse(this.recurringJobQueueIdTextBox.Text, out _guid))
            {
                Settings.RecurringJobId = _guid;
            }
        }

        private void clearLogButton_Click(object sender, EventArgs e)
        {            
            logTextBox.Clear();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.fileProcessor = new FileProcessor(this);
            this.fileProcessor.Start();
            this.startButton.Enabled = false;
            this.stopButton.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.fileProcessor.Stop();
            this.stopButton.Enabled = false;
            this.startButton.Enabled = true;
            
        }

        public void logText(string message)
        {
            if (this.InvokeRequired)
            {
                this.logTextBox.Invoke(new Action<string>(logText), new object[] { message });
                return;
            }
            this.logTextBox.Text += "\r\n" + message;            
        }

        public void updateStats(StatType statType, int count)
        {
            int tmpCount = 0;

            switch (statType)
            {
                case StatType.Input:
                    if (this.InvokeRequired)
                    {
                        this.submittedJobsLabel.Invoke(new Action<StatType, int>(updateStats), new object[] { statType, count });
                    }                    
                    lock (stateLock)
                    {
                        tmpCount = count;
                    }
                    this.submittedJobsLabel.Text = tmpCount.ToString();
                    break;

                case StatType.Inprocess:
                    if (this.InvokeRequired)
                    {
                        this.inProcessJobsLabel.Invoke(new Action<StatType, int>(updateStats), new object[] { statType, count });
                    }
                    lock (stateLock)
                    {
                        tmpCount = count;
                    }
                    this.inProcessJobsLabel.Text = tmpCount.ToString();
                    break;

                case StatType.Success:
                    if (this.InvokeRequired)
                    {
                        this.successJobsLabel.Invoke(new Action<StatType, int>(updateStats), new object[] { statType, count });
                    }
                    lock (stateLock)
                    {
                        tmpCount = count;
                    }
                    this.successJobsLabel.Text = tmpCount.ToString();
                    break;

                case StatType.Failure:
                    if (this.InvokeRequired)
                    {
                        this.failedJobsLabel.Invoke(new Action<StatType, int>(updateStats), new object[] { statType, count });
                    }
                    lock (stateLock)
                    {
                        tmpCount = count;
                    }
                    this.failedJobsLabel.Text = count.ToString();
                    break;
            }
        }

        private void DIXFRecurringJobsProcessor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

    public enum StatType
    {
        Input,

        Inprocess,

        Success,

        Failure
    }

}
