namespace KingstButtonClicker
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.recordPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.saveCurrentDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.updateDatabaseFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDatabasemanualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.clearCurrentDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateScenarioFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.moveMouseToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loopThroughScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enablePipeClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showExampleScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showExampleDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enablePipeOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.ContextMenuStrip = this.contextMenuStrip1;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(0, 27);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(572, 334);
            this.txtOutput.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearOutputToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 28);
            // 
            // clearOutputToolStripMenuItem
            // 
            this.clearOutputToolStripMenuItem.Name = "clearOutputToolStripMenuItem";
            this.clearOutputToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.clearOutputToolStripMenuItem.Text = "Clear output";
            this.clearOutputToolStripMenuItem.Click += new System.EventHandler(this.clearOutputToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.executeToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(572, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.closeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimizeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(54, 23);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // minimizeToolStripMenuItem
            // 
            this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.minimizeToolStripMenuItem.Text = "Minimize";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testColorsToolStripMenuItem,
            this.toolStripSeparator4,
            this.recordPointsToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripSeparator7,
            this.saveCurrentDatabaseToolStripMenuItem,
            this.toolStripSeparator5,
            this.updateDatabaseFromFileToolStripMenuItem,
            this.editDatabasemanualToolStripMenuItem,
            this.toolStripSeparator8,
            this.clearCurrentDatabaseToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.editScenarioToolStripMenuItem,
            this.updateScenarioFromFileToolStripMenuItem,
            this.toolStripSeparator6,
            this.moveMouseToToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // testColorsToolStripMenuItem
            // 
            this.testColorsToolStripMenuItem.Name = "testColorsToolStripMenuItem";
            this.testColorsToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.testColorsToolStripMenuItem.Text = "Test colors";
            this.testColorsToolStripMenuItem.Click += new System.EventHandler(this.testColorsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(236, 6);
            // 
            // recordPointsToolStripMenuItem
            // 
            this.recordPointsToolStripMenuItem.Name = "recordPointsToolStripMenuItem";
            this.recordPointsToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.recordPointsToolStripMenuItem.Text = "Record points";
            this.recordPointsToolStripMenuItem.Click += new System.EventHandler(this.recordPointsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(236, 6);
            // 
            // saveCurrentDatabaseToolStripMenuItem
            // 
            this.saveCurrentDatabaseToolStripMenuItem.Name = "saveCurrentDatabaseToolStripMenuItem";
            this.saveCurrentDatabaseToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.saveCurrentDatabaseToolStripMenuItem.Text = "Save current database";
            this.saveCurrentDatabaseToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentDatabaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(236, 6);
            // 
            // updateDatabaseFromFileToolStripMenuItem
            // 
            this.updateDatabaseFromFileToolStripMenuItem.Name = "updateDatabaseFromFileToolStripMenuItem";
            this.updateDatabaseFromFileToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.updateDatabaseFromFileToolStripMenuItem.Text = "Update database from file";
            this.updateDatabaseFromFileToolStripMenuItem.Click += new System.EventHandler(this.updateDatabaseFromFileToolStripMenuItem_Click);
            // 
            // editDatabasemanualToolStripMenuItem
            // 
            this.editDatabasemanualToolStripMenuItem.Name = "editDatabasemanualToolStripMenuItem";
            this.editDatabasemanualToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.editDatabasemanualToolStripMenuItem.Text = "Edit database (manual)";
            this.editDatabasemanualToolStripMenuItem.Click += new System.EventHandler(this.editDatabasemanualToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(236, 6);
            // 
            // clearCurrentDatabaseToolStripMenuItem
            // 
            this.clearCurrentDatabaseToolStripMenuItem.Name = "clearCurrentDatabaseToolStripMenuItem";
            this.clearCurrentDatabaseToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.clearCurrentDatabaseToolStripMenuItem.Text = "Clear current database";
            this.clearCurrentDatabaseToolStripMenuItem.Click += new System.EventHandler(this.clearCurrentDatabaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(236, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(236, 6);
            // 
            // editScenarioToolStripMenuItem
            // 
            this.editScenarioToolStripMenuItem.Name = "editScenarioToolStripMenuItem";
            this.editScenarioToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.editScenarioToolStripMenuItem.Text = "Edit scenario (manual)";
            this.editScenarioToolStripMenuItem.Click += new System.EventHandler(this.editScenarioToolStripMenuItem_Click);
            // 
            // updateScenarioFromFileToolStripMenuItem
            // 
            this.updateScenarioFromFileToolStripMenuItem.Name = "updateScenarioFromFileToolStripMenuItem";
            this.updateScenarioFromFileToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.updateScenarioFromFileToolStripMenuItem.Text = "Update scenario from file";
            this.updateScenarioFromFileToolStripMenuItem.Click += new System.EventHandler(this.updateScenarioFromFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(236, 6);
            // 
            // moveMouseToToolStripMenuItem
            // 
            this.moveMouseToToolStripMenuItem.Name = "moveMouseToToolStripMenuItem";
            this.moveMouseToToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.moveMouseToToolStripMenuItem.Text = "Move mouse to...";
            this.moveMouseToToolStripMenuItem.Click += new System.EventHandler(this.moveMouseToToolStripMenuItem_Click);
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeScenarioToolStripMenuItem,
            this.loopThroughScenarioToolStripMenuItem,
            this.enablePipeClientToolStripMenuItem});
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(67, 23);
            this.executeToolStripMenuItem.Text = "Execute";
            // 
            // executeScenarioToolStripMenuItem
            // 
            this.executeScenarioToolStripMenuItem.Name = "executeScenarioToolStripMenuItem";
            this.executeScenarioToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.executeScenarioToolStripMenuItem.Text = "Execute Scenario";
            this.executeScenarioToolStripMenuItem.Click += new System.EventHandler(this.executeScenarioToolStripMenuItem_Click);
            // 
            // loopThroughScenarioToolStripMenuItem
            // 
            this.loopThroughScenarioToolStripMenuItem.Name = "loopThroughScenarioToolStripMenuItem";
            this.loopThroughScenarioToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.loopThroughScenarioToolStripMenuItem.Text = "Loop through scenario";
            this.loopThroughScenarioToolStripMenuItem.Click += new System.EventHandler(this.loopThroughScenarioToolStripMenuItem_Click);
            // 
            // enablePipeClientToolStripMenuItem
            // 
            this.enablePipeClientToolStripMenuItem.CheckOnClick = true;
            this.enablePipeClientToolStripMenuItem.Name = "enablePipeClientToolStripMenuItem";
            this.enablePipeClientToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.enablePipeClientToolStripMenuItem.Text = "Enable pipe client";
            this.enablePipeClientToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enablePipeClientToolStripMenuItem_CheckedChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showExampleScenarioToolStripMenuItem,
            this.showExampleDatabaseToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // showExampleScenarioToolStripMenuItem
            // 
            this.showExampleScenarioToolStripMenuItem.Name = "showExampleScenarioToolStripMenuItem";
            this.showExampleScenarioToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.showExampleScenarioToolStripMenuItem.Text = "Show example scenario";
            this.showExampleScenarioToolStripMenuItem.Click += new System.EventHandler(this.showExampleScenarioToolStripMenuItem_Click);
            // 
            // showExampleDatabaseToolStripMenuItem
            // 
            this.showExampleDatabaseToolStripMenuItem.Name = "showExampleDatabaseToolStripMenuItem";
            this.showExampleDatabaseToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.showExampleDatabaseToolStripMenuItem.Text = "Show example database";
            this.showExampleDatabaseToolStripMenuItem.Click += new System.EventHandler(this.showExampleDatabaseToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(59, 23);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "UI Automation";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableMessagesToolStripMenuItem,
            this.enableLogToolStripMenuItem,
            this.enablePipeOnStartupToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // enableMessagesToolStripMenuItem
            // 
            this.enableMessagesToolStripMenuItem.CheckOnClick = true;
            this.enableMessagesToolStripMenuItem.Name = "enableMessagesToolStripMenuItem";
            this.enableMessagesToolStripMenuItem.Size = new System.Drawing.Size(184, 24);
            this.enableMessagesToolStripMenuItem.Text = "Enable messages";
            this.enableMessagesToolStripMenuItem.Click += new System.EventHandler(this.enableMessagesToolStripMenuItem_Click);
            // 
            // enableLogToolStripMenuItem
            // 
            this.enableLogToolStripMenuItem.CheckOnClick = true;
            this.enableLogToolStripMenuItem.Name = "enableLogToolStripMenuItem";
            this.enableLogToolStripMenuItem.Size = new System.Drawing.Size(184, 24);
            this.enableLogToolStripMenuItem.Text = "Enable log";
            // 
            // enablePipeOnStartupToolStripMenuItem
            // 
            this.enablePipeOnStartupToolStripMenuItem.CheckOnClick = true;
            this.enablePipeOnStartupToolStripMenuItem.Name = "enablePipeOnStartupToolStripMenuItem";
            this.enablePipeOnStartupToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.enablePipeOnStartupToolStripMenuItem.Text = "Enable pipe on startup";
            this.enablePipeOnStartupToolStripMenuItem.Click += new System.EventHandler(this.enablePipeOnStartupToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 361);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "UI Automation Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testColorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordPointsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCurrentDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateDatabaseFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editDatabasemanualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem moveMouseToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem updateScenarioFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showExampleScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showExampleDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enablePipeClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loopThroughScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enablePipeOnStartupToolStripMenuItem;
    }
}

