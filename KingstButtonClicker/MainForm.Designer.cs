﻿namespace UIAutomationTool
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
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.editWindowTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateWindowTitleFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recordPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveMouseToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activateWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enablePipeOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loopThroughScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enablePipeClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showExampleScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showExampleDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.disableWindowFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.trayContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.ContextMenuStrip = this.contextMenuStrip1;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(0, 28);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(763, 416);
            this.txtOutput.TabIndex = 1;
            this.txtOutput.TextChanged += new System.EventHandler(this.txtOutput_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearOutputToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 28);
            // 
            // clearOutputToolStripMenuItem
            // 
            this.clearOutputToolStripMenuItem.Name = "clearOutputToolStripMenuItem";
            this.clearOutputToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(763, 28);
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
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // minimizeToolStripMenuItem
            // 
            this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.minimizeToolStripMenuItem.Text = "Minimize";
            this.minimizeToolStripMenuItem.Click += new System.EventHandler(this.minimizeToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.toolStripSeparator7,
            this.toolStripSeparator6,
            this.editWindowTitleToolStripMenuItem,
            this.updateWindowTitleFromFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.recordPointsToolStripMenuItem,
            this.testColorsToolStripMenuItem,
            this.moveMouseToToolStripMenuItem,
            this.activateWindowToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // saveCurrentDatabaseToolStripMenuItem
            // 
            this.saveCurrentDatabaseToolStripMenuItem.Name = "saveCurrentDatabaseToolStripMenuItem";
            this.saveCurrentDatabaseToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.saveCurrentDatabaseToolStripMenuItem.Text = "Save current database";
            this.saveCurrentDatabaseToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentDatabaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(277, 6);
            // 
            // updateDatabaseFromFileToolStripMenuItem
            // 
            this.updateDatabaseFromFileToolStripMenuItem.Name = "updateDatabaseFromFileToolStripMenuItem";
            this.updateDatabaseFromFileToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.updateDatabaseFromFileToolStripMenuItem.Text = "Update database from file";
            this.updateDatabaseFromFileToolStripMenuItem.Click += new System.EventHandler(this.updateDatabaseFromFileToolStripMenuItem_Click);
            // 
            // editDatabasemanualToolStripMenuItem
            // 
            this.editDatabasemanualToolStripMenuItem.Name = "editDatabasemanualToolStripMenuItem";
            this.editDatabasemanualToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.editDatabasemanualToolStripMenuItem.Text = "Edit database (manual)";
            this.editDatabasemanualToolStripMenuItem.Click += new System.EventHandler(this.editDatabasemanualToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(277, 6);
            // 
            // clearCurrentDatabaseToolStripMenuItem
            // 
            this.clearCurrentDatabaseToolStripMenuItem.Name = "clearCurrentDatabaseToolStripMenuItem";
            this.clearCurrentDatabaseToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.clearCurrentDatabaseToolStripMenuItem.Text = "Clear current database";
            this.clearCurrentDatabaseToolStripMenuItem.Click += new System.EventHandler(this.clearCurrentDatabaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(277, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(277, 6);
            // 
            // editScenarioToolStripMenuItem
            // 
            this.editScenarioToolStripMenuItem.Name = "editScenarioToolStripMenuItem";
            this.editScenarioToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.editScenarioToolStripMenuItem.Text = "Edit scenario (manual)";
            this.editScenarioToolStripMenuItem.Click += new System.EventHandler(this.editScenarioToolStripMenuItem_Click);
            // 
            // updateScenarioFromFileToolStripMenuItem
            // 
            this.updateScenarioFromFileToolStripMenuItem.Name = "updateScenarioFromFileToolStripMenuItem";
            this.updateScenarioFromFileToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.updateScenarioFromFileToolStripMenuItem.Text = "Update scenario from file";
            this.updateScenarioFromFileToolStripMenuItem.Click += new System.EventHandler(this.updateScenarioFromFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(277, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(277, 6);
            // 
            // editWindowTitleToolStripMenuItem
            // 
            this.editWindowTitleToolStripMenuItem.Name = "editWindowTitleToolStripMenuItem";
            this.editWindowTitleToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.editWindowTitleToolStripMenuItem.Text = "Edit window title (manual)";
            this.editWindowTitleToolStripMenuItem.Click += new System.EventHandler(this.editWindowTitleToolStripMenuItem_Click);
            // 
            // updateWindowTitleFromFileToolStripMenuItem
            // 
            this.updateWindowTitleFromFileToolStripMenuItem.Name = "updateWindowTitleFromFileToolStripMenuItem";
            this.updateWindowTitleFromFileToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.updateWindowTitleFromFileToolStripMenuItem.Text = "Update window title from file";
            this.updateWindowTitleFromFileToolStripMenuItem.Click += new System.EventHandler(this.updateWindowTitleFromFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(277, 6);
            // 
            // recordPointsToolStripMenuItem
            // 
            this.recordPointsToolStripMenuItem.Name = "recordPointsToolStripMenuItem";
            this.recordPointsToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.recordPointsToolStripMenuItem.Text = "Record points";
            this.recordPointsToolStripMenuItem.Click += new System.EventHandler(this.recordPointsToolStripMenuItem_Click);
            // 
            // testColorsToolStripMenuItem
            // 
            this.testColorsToolStripMenuItem.Name = "testColorsToolStripMenuItem";
            this.testColorsToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.testColorsToolStripMenuItem.Text = "Test colors";
            this.testColorsToolStripMenuItem.Click += new System.EventHandler(this.testColorsToolStripMenuItem_Click);
            // 
            // moveMouseToToolStripMenuItem
            // 
            this.moveMouseToToolStripMenuItem.Name = "moveMouseToToolStripMenuItem";
            this.moveMouseToToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.moveMouseToToolStripMenuItem.Text = "Move mouse to...";
            this.moveMouseToToolStripMenuItem.Click += new System.EventHandler(this.moveMouseToToolStripMenuItem_Click);
            // 
            // activateWindowToolStripMenuItem
            // 
            this.activateWindowToolStripMenuItem.Name = "activateWindowToolStripMenuItem";
            this.activateWindowToolStripMenuItem.Size = new System.Drawing.Size(280, 26);
            this.activateWindowToolStripMenuItem.Text = "Activate window...";
            this.activateWindowToolStripMenuItem.Click += new System.EventHandler(this.activateWindowToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableMessagesToolStripMenuItem,
            this.enableLogToolStripMenuItem,
            this.enablePipeOnStartupToolStripMenuItem,
            this.disableWindowFilterToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // enableMessagesToolStripMenuItem
            // 
            this.enableMessagesToolStripMenuItem.CheckOnClick = true;
            this.enableMessagesToolStripMenuItem.Name = "enableMessagesToolStripMenuItem";
            this.enableMessagesToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.enableMessagesToolStripMenuItem.Text = "Enable messages";
            this.enableMessagesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enableMessagesToolStripMenuItem_CheckedChanged);
            // 
            // enableLogToolStripMenuItem
            // 
            this.enableLogToolStripMenuItem.CheckOnClick = true;
            this.enableLogToolStripMenuItem.Name = "enableLogToolStripMenuItem";
            this.enableLogToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.enableLogToolStripMenuItem.Text = "Enable log";
            this.enableLogToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enableLogToolStripMenuItem_CheckedChanged);
            // 
            // enablePipeOnStartupToolStripMenuItem
            // 
            this.enablePipeOnStartupToolStripMenuItem.CheckOnClick = true;
            this.enablePipeOnStartupToolStripMenuItem.Name = "enablePipeOnStartupToolStripMenuItem";
            this.enablePipeOnStartupToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.enablePipeOnStartupToolStripMenuItem.Text = "Enable pipe on startup";
            this.enablePipeOnStartupToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enablePipeOnStartupToolStripMenuItem_CheckedChanged);
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeScenarioToolStripMenuItem,
            this.loopThroughScenarioToolStripMenuItem,
            this.enablePipeClientToolStripMenuItem});
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.executeToolStripMenuItem.Text = "Execute";
            // 
            // executeScenarioToolStripMenuItem
            // 
            this.executeScenarioToolStripMenuItem.CheckOnClick = true;
            this.executeScenarioToolStripMenuItem.Name = "executeScenarioToolStripMenuItem";
            this.executeScenarioToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.executeScenarioToolStripMenuItem.Text = "Execute Scenario";
            this.executeScenarioToolStripMenuItem.CheckedChanged += new System.EventHandler(this.executeScenarioToolStripMenuItem_CheckedChanged);
            // 
            // loopThroughScenarioToolStripMenuItem
            // 
            this.loopThroughScenarioToolStripMenuItem.CheckOnClick = true;
            this.loopThroughScenarioToolStripMenuItem.Name = "loopThroughScenarioToolStripMenuItem";
            this.loopThroughScenarioToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.loopThroughScenarioToolStripMenuItem.Text = "Loop through scenario";
            this.loopThroughScenarioToolStripMenuItem.CheckedChanged += new System.EventHandler(this.loopThroughScenarioToolStripMenuItem_CheckedChanged);
            // 
            // enablePipeClientToolStripMenuItem
            // 
            this.enablePipeClientToolStripMenuItem.CheckOnClick = true;
            this.enablePipeClientToolStripMenuItem.Name = "enablePipeClientToolStripMenuItem";
            this.enablePipeClientToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.enablePipeClientToolStripMenuItem.Text = "Enable pipe client";
            this.enablePipeClientToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enablePipeClientToolStripMenuItem_CheckedChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showExampleScenarioToolStripMenuItem,
            this.showExampleDatabaseToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // showExampleScenarioToolStripMenuItem
            // 
            this.showExampleScenarioToolStripMenuItem.Name = "showExampleScenarioToolStripMenuItem";
            this.showExampleScenarioToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
            this.showExampleScenarioToolStripMenuItem.Text = "Show example scenario";
            this.showExampleScenarioToolStripMenuItem.Click += new System.EventHandler(this.showExampleScenarioToolStripMenuItem_Click);
            // 
            // showExampleDatabaseToolStripMenuItem
            // 
            this.showExampleDatabaseToolStripMenuItem.Name = "showExampleDatabaseToolStripMenuItem";
            this.showExampleDatabaseToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
            this.showExampleDatabaseToolStripMenuItem.Text = "Show example database";
            this.showExampleDatabaseToolStripMenuItem.Click += new System.EventHandler(this.showExampleDatabaseToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.trayContextMenuStrip;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "UI Automation";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // trayContextMenuStrip
            // 
            this.trayContextMenuStrip.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.trayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.trayContextMenuStrip.Name = "trayContextMenuStrip";
            this.trayContextMenuStrip.Size = new System.Drawing.Size(100, 28);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(99, 24);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // disableWindowFilterToolStripMenuItem
            // 
            this.disableWindowFilterToolStripMenuItem.CheckOnClick = true;
            this.disableWindowFilterToolStripMenuItem.Name = "disableWindowFilterToolStripMenuItem";
            this.disableWindowFilterToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.disableWindowFilterToolStripMenuItem.Text = "Disable window filter";
            this.disableWindowFilterToolStripMenuItem.Click += new System.EventHandler(this.disableWindowFilterToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 444);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "UI Automation Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.trayContextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem editWindowTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateWindowTitleFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem activateWindowToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip trayContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem disableWindowFilterToolStripMenuItem;
    }
}

