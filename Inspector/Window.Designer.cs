namespace Transit.Inspector
{
    partial class Window
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
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerLarge = new System.Windows.Forms.SplitContainer();
            this.showExcludedRows = new System.Windows.Forms.CheckBox();
            this.filterHelpLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.filterBox = new System.Windows.Forms.TextBox();
            this.captureGrid = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.readsGrid = new System.Windows.Forms.DataGridView();
            this.zoomBar = new System.Windows.Forms.TrackBar();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.status.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLarge)).BeginInit();
            this.splitContainerLarge.Panel1.SuspendLayout();
            this.splitContainerLarge.Panel2.SuspendLayout();
            this.splitContainerLarge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captureGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBar)).BeginInit();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.status.Location = new System.Drawing.Point(0, 640);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(878, 22);
            this.status.TabIndex = 0;
            this.status.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(100, 17);
            this.statusLabel.Text = "                               ";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(878, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripMenuItem2,
            this.helpToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(104, 6);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // splitContainerLarge
            // 
            this.splitContainerLarge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLarge.Location = new System.Drawing.Point(0, 24);
            this.splitContainerLarge.Name = "splitContainerLarge";
            this.splitContainerLarge.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLarge.Panel1
            // 
            this.splitContainerLarge.Panel1.Controls.Add(this.showExcludedRows);
            this.splitContainerLarge.Panel1.Controls.Add(this.filterHelpLink);
            this.splitContainerLarge.Panel1.Controls.Add(this.label1);
            this.splitContainerLarge.Panel1.Controls.Add(this.filterBox);
            this.splitContainerLarge.Panel1.Controls.Add(this.captureGrid);
            // 
            // splitContainerLarge.Panel2
            // 
            this.splitContainerLarge.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainerLarge.Size = new System.Drawing.Size(878, 616);
            this.splitContainerLarge.SplitterDistance = 309;
            this.splitContainerLarge.TabIndex = 2;
            // 
            // showExcludedRows
            // 
            this.showExcludedRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showExcludedRows.AutoSize = true;
            this.showExcludedRows.Checked = true;
            this.showExcludedRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showExcludedRows.Location = new System.Drawing.Point(751, 4);
            this.showExcludedRows.Name = "showExcludedRows";
            this.showExcludedRows.Size = new System.Drawing.Size(124, 17);
            this.showExcludedRows.TabIndex = 4;
            this.showExcludedRows.Text = "Show excluded rows";
            this.showExcludedRows.UseVisualStyleBackColor = true;
            this.showExcludedRows.CheckedChanged += new System.EventHandler(this.ShowExcludedRowsCheckedChanged);
            // 
            // filterHelpLink
            // 
            this.filterHelpLink.AutoSize = true;
            this.filterHelpLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterHelpLink.Location = new System.Drawing.Point(29, 5);
            this.filterHelpLink.Name = "filterHelpLink";
            this.filterHelpLink.Size = new System.Drawing.Size(14, 13);
            this.filterHelpLink.TabIndex = 3;
            this.filterHelpLink.TabStop = true;
            this.filterHelpLink.Text = "?";
            this.filterHelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FilterHelpLinkLinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter:";
            // 
            // filterBox
            // 
            this.filterBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filterBox.Location = new System.Drawing.Point(49, 3);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(696, 20);
            this.filterBox.TabIndex = 1;
            this.filterBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FilterBoxKeyUp);
            // 
            // captureGrid
            // 
            this.captureGrid.AllowUserToAddRows = false;
            this.captureGrid.AllowUserToDeleteRows = false;
            this.captureGrid.AllowUserToResizeRows = false;
            this.captureGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.captureGrid.BackgroundColor = System.Drawing.Color.White;
            this.captureGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.captureGrid.Location = new System.Drawing.Point(3, 29);
            this.captureGrid.Name = "captureGrid";
            this.captureGrid.RowHeadersVisible = false;
            this.captureGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.captureGrid.Size = new System.Drawing.Size(872, 277);
            this.captureGrid.TabIndex = 0;
            this.captureGrid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.captureGrid_CellMouseDown);
            this.captureGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CaptureGridCellMouseUp);
            this.captureGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.CaptureGridCellValueChanged);
            this.captureGrid.SelectionChanged += new System.EventHandler(this.CaptureGridSelectionChanged);
            this.captureGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CaptureGridKeyPress);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.readsGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.zoomBar);
            this.splitContainer1.Panel2.Controls.Add(this.map);
            this.splitContainer1.Size = new System.Drawing.Size(878, 303);
            this.splitContainer1.SplitterDistance = 437;
            this.splitContainer1.TabIndex = 0;
            // 
            // readsGrid
            // 
            this.readsGrid.AllowUserToAddRows = false;
            this.readsGrid.AllowUserToDeleteRows = false;
            this.readsGrid.AllowUserToResizeRows = false;
            this.readsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.readsGrid.BackgroundColor = System.Drawing.Color.White;
            this.readsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.readsGrid.Location = new System.Drawing.Point(3, 3);
            this.readsGrid.MultiSelect = false;
            this.readsGrid.Name = "readsGrid";
            this.readsGrid.RowHeadersVisible = false;
            this.readsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.readsGrid.Size = new System.Drawing.Size(431, 297);
            this.readsGrid.TabIndex = 0;
            this.readsGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReadsGridCellValueChanged);
            // 
            // zoomBar
            // 
            this.zoomBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomBar.AutoSize = false;
            this.zoomBar.Location = new System.Drawing.Point(404, 13);
            this.zoomBar.Name = "zoomBar";
            this.zoomBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.zoomBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zoomBar.Size = new System.Drawing.Size(21, 104);
            this.zoomBar.TabIndex = 1;
            this.zoomBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.zoomBar.Scroll += new System.EventHandler(this.ZoomBarScroll);
            // 
            // map
            // 
            this.map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.map.BackColor = System.Drawing.SystemColors.Control;
            this.map.Bearing = 0F;
            this.map.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.map.CanDragMap = true;
            this.map.GrayScaleMode = false;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(3, 3);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(431, 297);
            this.map.TabIndex = 0;
            this.map.Zoom = 0D;
            this.map.DoubleClick += new System.EventHandler(this.MapDoubleClick);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 662);
            this.Controls.Add(this.splitContainerLarge);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "Window";
            this.Text = "Transit Inspector";
            this.Load += new System.EventHandler(this.WindowLoad);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.splitContainerLarge.Panel1.ResumeLayout(false);
            this.splitContainerLarge.Panel1.PerformLayout();
            this.splitContainerLarge.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLarge)).EndInit();
            this.splitContainerLarge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.captureGrid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.readsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerLarge;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView captureGrid;
        private System.Windows.Forms.DataGridView readsGrid;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filterBox;
        private System.Windows.Forms.LinkLabel filterHelpLink;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.CheckBox showExcludedRows;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.TrackBar zoomBar;
    }
}

