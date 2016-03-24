namespace RecurringIntegrationApp
{
    public partial class DIXFRecurringJobsProcessor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DIXFRecurringJobsProcessor));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.serverGroupBox = new System.Windows.Forms.GroupBox();
            this.ax7UserpasswordTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ax7UserNameTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.clientAppIdtextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.aadTenantTextBox = new System.Windows.Forms.TextBox();
            this.ax7EndpointTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.azureAuthEndpointTextBox = new System.Windows.Forms.TextBox();
            this.fileTransferGroupBox = new System.Windows.Forms.GroupBox();
            this.isDataPackageInputCheckBox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.companyTextBox = new System.Windows.Forms.TextBox();
            this.recurringJobQueueIdTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.entityNameTextBox = new System.Windows.Forms.TextBox();
            this.clientGroupBox = new System.Windows.Forms.GroupBox();
            this.statusPollingIntervalTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.failedLocationTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.successLocationTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.inProcessLocationTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inputLocationTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.executionTabPage = new System.Windows.Forms.TabPage();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.failedJobsLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.successJobsLabel = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.inProcessJobsLabel = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.submittedJobsLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            this.serverGroupBox.SuspendLayout();
            this.fileTransferGroupBox.SuspendLayout();
            this.clientGroupBox.SuspendLayout();
            this.executionTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.settingsTabPage);
            this.tabControl1.Controls.Add(this.executionTabPage);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(22, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1213, 696);
            this.tabControl1.TabIndex = 0;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.BackgroundImage = global::RecurringIntegrationApp.Properties.Resources.background;
            this.settingsTabPage.Controls.Add(this.serverGroupBox);
            this.settingsTabPage.Controls.Add(this.fileTransferGroupBox);
            this.settingsTabPage.Controls.Add(this.clientGroupBox);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 26);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(1205, 666);
            this.settingsTabPage.TabIndex = 0;
            this.settingsTabPage.Text = "Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // serverGroupBox
            // 
            this.serverGroupBox.Controls.Add(this.ax7UserpasswordTextBox);
            this.serverGroupBox.Controls.Add(this.label9);
            this.serverGroupBox.Controls.Add(this.ax7UserNameTextBox);
            this.serverGroupBox.Controls.Add(this.label10);
            this.serverGroupBox.Controls.Add(this.clientAppIdtextBox);
            this.serverGroupBox.Controls.Add(this.label8);
            this.serverGroupBox.Controls.Add(this.label5);
            this.serverGroupBox.Controls.Add(this.aadTenantTextBox);
            this.serverGroupBox.Controls.Add(this.ax7EndpointTextBox);
            this.serverGroupBox.Controls.Add(this.label7);
            this.serverGroupBox.Controls.Add(this.label6);
            this.serverGroupBox.Controls.Add(this.azureAuthEndpointTextBox);
            this.serverGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverGroupBox.Location = new System.Drawing.Point(23, 214);
            this.serverGroupBox.Name = "serverGroupBox";
            this.serverGroupBox.Size = new System.Drawing.Size(1163, 198);
            this.serverGroupBox.TabIndex = 2;
            this.serverGroupBox.TabStop = false;
            this.serverGroupBox.Text = "Server settings";
            // 
            // ax7UserpasswordTextBox
            // 
            this.ax7UserpasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ax7UserpasswordTextBox.Location = new System.Drawing.Point(748, 129);
            this.ax7UserpasswordTextBox.Name = "ax7UserpasswordTextBox";
            this.ax7UserpasswordTextBox.Size = new System.Drawing.Size(250, 23);
            this.ax7UserpasswordTextBox.TabIndex = 19;
            this.ax7UserpasswordTextBox.UseSystemPasswordChar = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(529, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "AX7 user password";
            // 
            // ax7UserNameTextBox
            // 
            this.ax7UserNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ax7UserNameTextBox.Location = new System.Drawing.Point(240, 129);
            this.ax7UserNameTextBox.Name = "ax7UserNameTextBox";
            this.ax7UserNameTextBox.Size = new System.Drawing.Size(250, 23);
            this.ax7UserNameTextBox.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(24, 134);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 17);
            this.label10.TabIndex = 16;
            this.label10.Text = "AX7 user name";
            // 
            // clientAppIdtextBox
            // 
            this.clientAppIdtextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientAppIdtextBox.Location = new System.Drawing.Point(748, 77);
            this.clientAppIdtextBox.Name = "clientAppIdtextBox";
            this.clientAppIdtextBox.Size = new System.Drawing.Size(250, 23);
            this.clientAppIdtextBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(24, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "AAD tenant";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(529, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Native client app id";
            // 
            // aadTenantTextBox
            // 
            this.aadTenantTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aadTenantTextBox.Location = new System.Drawing.Point(240, 27);
            this.aadTenantTextBox.Name = "aadTenantTextBox";
            this.aadTenantTextBox.Size = new System.Drawing.Size(250, 23);
            this.aadTenantTextBox.TabIndex = 9;
            // 
            // ax7EndpointTextBox
            // 
            this.ax7EndpointTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ax7EndpointTextBox.Location = new System.Drawing.Point(240, 82);
            this.ax7EndpointTextBox.Name = "ax7EndpointTextBox";
            this.ax7EndpointTextBox.Size = new System.Drawing.Size(250, 23);
            this.ax7EndpointTextBox.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(529, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(190, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Azure authorization endpoint";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "AX7 endpoint";
            // 
            // azureAuthEndpointTextBox
            // 
            this.azureAuthEndpointTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.azureAuthEndpointTextBox.Location = new System.Drawing.Point(748, 26);
            this.azureAuthEndpointTextBox.Name = "azureAuthEndpointTextBox";
            this.azureAuthEndpointTextBox.Size = new System.Drawing.Size(250, 23);
            this.azureAuthEndpointTextBox.TabIndex = 11;
            // 
            // fileTransferGroupBox
            // 
            this.fileTransferGroupBox.Controls.Add(this.isDataPackageInputCheckBox);
            this.fileTransferGroupBox.Controls.Add(this.label16);
            this.fileTransferGroupBox.Controls.Add(this.companyTextBox);
            this.fileTransferGroupBox.Controls.Add(this.recurringJobQueueIdTextBox);
            this.fileTransferGroupBox.Controls.Add(this.label11);
            this.fileTransferGroupBox.Controls.Add(this.label13);
            this.fileTransferGroupBox.Controls.Add(this.label12);
            this.fileTransferGroupBox.Controls.Add(this.entityNameTextBox);
            this.fileTransferGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileTransferGroupBox.Location = new System.Drawing.Point(23, 431);
            this.fileTransferGroupBox.Name = "fileTransferGroupBox";
            this.fileTransferGroupBox.Size = new System.Drawing.Size(1163, 162);
            this.fileTransferGroupBox.TabIndex = 3;
            this.fileTransferGroupBox.TabStop = false;
            this.fileTransferGroupBox.Text = "Data transfer settings";
            // 
            // isDataPackageInputCheckBox
            // 
            this.isDataPackageInputCheckBox.AutoSize = true;
            this.isDataPackageInputCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isDataPackageInputCheckBox.Location = new System.Drawing.Point(896, 90);
            this.isDataPackageInputCheckBox.Name = "isDataPackageInputCheckBox";
            this.isDataPackageInputCheckBox.Size = new System.Drawing.Size(15, 14);
            this.isDataPackageInputCheckBox.TabIndex = 28;
            this.isDataPackageInputCheckBox.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(529, 87);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(253, 17);
            this.label16.TabIndex = 26;
            this.label16.Text = "Are input files in Data package format?";
            // 
            // companyTextBox
            // 
            this.companyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.companyTextBox.Location = new System.Drawing.Point(236, 87);
            this.companyTextBox.Name = "companyTextBox";
            this.companyTextBox.Size = new System.Drawing.Size(250, 23);
            this.companyTextBox.TabIndex = 25;
            // 
            // recurringJobQueueIdTextBox
            // 
            this.recurringJobQueueIdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recurringJobQueueIdTextBox.Location = new System.Drawing.Point(236, 37);
            this.recurringJobQueueIdTextBox.Name = "recurringJobQueueIdTextBox";
            this.recurringJobQueueIdTextBox.Size = new System.Drawing.Size(250, 23);
            this.recurringJobQueueIdTextBox.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(24, 93);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 17);
            this.label11.TabIndex = 24;
            this.label11.Text = "Company";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(529, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 17);
            this.label13.TabIndex = 22;
            this.label13.Text = "Entity name (label)*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(24, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 17);
            this.label12.TabIndex = 20;
            this.label12.Text = "Activity Id*";
            // 
            // entityNameTextBox
            // 
            this.entityNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entityNameTextBox.Location = new System.Drawing.Point(748, 36);
            this.entityNameTextBox.Name = "entityNameTextBox";
            this.entityNameTextBox.Size = new System.Drawing.Size(250, 23);
            this.entityNameTextBox.TabIndex = 23;
            // 
            // clientGroupBox
            // 
            this.clientGroupBox.Controls.Add(this.statusPollingIntervalTextBox);
            this.clientGroupBox.Controls.Add(this.label18);
            this.clientGroupBox.Controls.Add(this.failedLocationTextBox);
            this.clientGroupBox.Controls.Add(this.label4);
            this.clientGroupBox.Controls.Add(this.successLocationTextBox);
            this.clientGroupBox.Controls.Add(this.label3);
            this.clientGroupBox.Controls.Add(this.inProcessLocationTextBox);
            this.clientGroupBox.Controls.Add(this.label2);
            this.clientGroupBox.Controls.Add(this.inputLocationTextBox);
            this.clientGroupBox.Controls.Add(this.label1);
            this.clientGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientGroupBox.Location = new System.Drawing.Point(23, 20);
            this.clientGroupBox.Name = "clientGroupBox";
            this.clientGroupBox.Size = new System.Drawing.Size(1163, 176);
            this.clientGroupBox.TabIndex = 1;
            this.clientGroupBox.TabStop = false;
            this.clientGroupBox.Text = "Client side settings";
            // 
            // statusPollingIntervalTextBox
            // 
            this.statusPollingIntervalTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusPollingIntervalTextBox.Location = new System.Drawing.Point(241, 139);
            this.statusPollingIntervalTextBox.Name = "statusPollingIntervalTextBox";
            this.statusPollingIntervalTextBox.Size = new System.Drawing.Size(250, 23);
            this.statusPollingIntervalTextBox.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(25, 144);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(197, 17);
            this.label18.TabIndex = 8;
            this.label18.Text = "Status polling interval (msecs)";
            // 
            // failedLocationTextBox
            // 
            this.failedLocationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedLocationTextBox.Location = new System.Drawing.Point(757, 84);
            this.failedLocationTextBox.Name = "failedLocationTextBox";
            this.failedLocationTextBox.Size = new System.Drawing.Size(250, 23);
            this.failedLocationTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(529, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Failed jobs location";
            // 
            // successLocationTextBox
            // 
            this.successLocationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.successLocationTextBox.Location = new System.Drawing.Point(241, 85);
            this.successLocationTextBox.Name = "successLocationTextBox";
            this.successLocationTextBox.Size = new System.Drawing.Size(250, 23);
            this.successLocationTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Successful jobs location";
            // 
            // inProcessLocationTextBox
            // 
            this.inProcessLocationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inProcessLocationTextBox.Location = new System.Drawing.Point(757, 30);
            this.inProcessLocationTextBox.Name = "inProcessLocationTextBox";
            this.inProcessLocationTextBox.Size = new System.Drawing.Size(250, 23);
            this.inProcessLocationTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(529, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "In-process jobs location";
            // 
            // inputLocationTextBox
            // 
            this.inputLocationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputLocationTextBox.Location = new System.Drawing.Point(241, 27);
            this.inputLocationTextBox.Name = "inputLocationTextBox";
            this.inputLocationTextBox.Size = new System.Drawing.Size(250, 23);
            this.inputLocationTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input location";
            // 
            // executionTabPage
            // 
            this.executionTabPage.BackgroundImage = global::RecurringIntegrationApp.Properties.Resources.background;
            this.executionTabPage.Controls.Add(this.clearLogButton);
            this.executionTabPage.Controls.Add(this.groupBox1);
            this.executionTabPage.Controls.Add(this.logTextBox);
            this.executionTabPage.Controls.Add(this.stopButton);
            this.executionTabPage.Controls.Add(this.startButton);
            this.executionTabPage.Location = new System.Drawing.Point(4, 26);
            this.executionTabPage.Name = "executionTabPage";
            this.executionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.executionTabPage.Size = new System.Drawing.Size(1205, 666);
            this.executionTabPage.TabIndex = 1;
            this.executionTabPage.Text = "Execution";
            this.executionTabPage.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(964, 102);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(130, 37);
            this.clearLogButton.TabIndex = 4;
            this.clearLogButton.Text = "Clear log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            this.clearLogButton.Click += new System.EventHandler(this.clearLogButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.failedJobsLabel);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.successJobsLabel);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.inProcessJobsLabel);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.submittedJobsLabel);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(57, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(689, 118);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Execution statistics";
            // 
            // failedJobsLabel
            // 
            this.failedJobsLabel.AutoSize = true;
            this.failedJobsLabel.BackColor = System.Drawing.Color.Transparent;
            this.failedJobsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedJobsLabel.Location = new System.Drawing.Point(530, 75);
            this.failedJobsLabel.Name = "failedJobsLabel";
            this.failedJobsLabel.Size = new System.Drawing.Size(16, 17);
            this.failedJobsLabel.TabIndex = 7;
            this.failedJobsLabel.Text = "0";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(326, 75);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 17);
            this.label22.TabIndex = 6;
            this.label22.Text = "Failed jobs:";
            // 
            // successJobsLabel
            // 
            this.successJobsLabel.AutoSize = true;
            this.successJobsLabel.BackColor = System.Drawing.Color.Transparent;
            this.successJobsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.successJobsLabel.Location = new System.Drawing.Point(530, 30);
            this.successJobsLabel.Name = "successJobsLabel";
            this.successJobsLabel.Size = new System.Drawing.Size(16, 17);
            this.successJobsLabel.TabIndex = 5;
            this.successJobsLabel.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(326, 29);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(110, 17);
            this.label24.TabIndex = 4;
            this.label24.Text = "Successful jobs:";
            // 
            // inProcessJobsLabel
            // 
            this.inProcessJobsLabel.AutoSize = true;
            this.inProcessJobsLabel.BackColor = System.Drawing.Color.Transparent;
            this.inProcessJobsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inProcessJobsLabel.Location = new System.Drawing.Point(203, 75);
            this.inProcessJobsLabel.Name = "inProcessJobsLabel";
            this.inProcessJobsLabel.Size = new System.Drawing.Size(16, 17);
            this.inProcessJobsLabel.TabIndex = 3;
            this.inProcessJobsLabel.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(14, 75);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(111, 17);
            this.label19.TabIndex = 2;
            this.label19.Text = "Jobs in process:";
            // 
            // submittedJobsLabel
            // 
            this.submittedJobsLabel.AutoSize = true;
            this.submittedJobsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submittedJobsLabel.Location = new System.Drawing.Point(203, 30);
            this.submittedJobsLabel.Name = "submittedJobsLabel";
            this.submittedJobsLabel.Size = new System.Drawing.Size(16, 17);
            this.submittedJobsLabel.TabIndex = 1;
            this.submittedJobsLabel.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(14, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 17);
            this.label17.TabIndex = 0;
            this.label17.Text = "Submitted jobs:";
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(57, 160);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(1040, 484);
            this.logTextBox.TabIndex = 2;
            this.logTextBox.Text = "";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(772, 102);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(130, 37);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(772, 35);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(125, 37);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // DIXFRecurringJobsProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1257, 747);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DIXFRecurringJobsProcessor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AX7 Recurring Data Jobs Processor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DIXFRecurringJobsProcessor_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.settingsTabPage.ResumeLayout(false);
            this.serverGroupBox.ResumeLayout(false);
            this.serverGroupBox.PerformLayout();
            this.fileTransferGroupBox.ResumeLayout(false);
            this.fileTransferGroupBox.PerformLayout();
            this.clientGroupBox.ResumeLayout(false);
            this.clientGroupBox.PerformLayout();
            this.executionTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.TabPage executionTabPage;
        private System.Windows.Forms.GroupBox serverGroupBox;
        private System.Windows.Forms.GroupBox fileTransferGroupBox;
        private System.Windows.Forms.GroupBox clientGroupBox;
        private System.Windows.Forms.TextBox failedLocationTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox successLocationTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inProcessLocationTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inputLocationTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox clientAppIdtextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox aadTenantTextBox;
        private System.Windows.Forms.TextBox ax7EndpointTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox azureAuthEndpointTextBox;
        private System.Windows.Forms.TextBox ax7UserpasswordTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ax7UserNameTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox companyTextBox;
        private System.Windows.Forms.TextBox recurringJobQueueIdTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox entityNameTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox isDataPackageInputCheckBox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Label failedJobsLabel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label successJobsLabel;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label inProcessJobsLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label submittedJobsLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.TextBox statusPollingIntervalTextBox;
        private System.Windows.Forms.Label label18;
    }
}

