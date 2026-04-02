namespace Lab6_FileIO_Manager
{
    // Розмітка форми (генерується/редагується дизайнером VS):
    // panelTop — шлях + «Огляд» + «Завантажити»;
    // splitContainerMain: зліва TreeView (дерево), справа splitContainerRight;
    // splitContainerRight: зверху ListView (вміст папки / результати пошуку), знизу panelBottom (маска пошуку + текст інфо про файл).
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxRootPath = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.treeViewMain = new System.Windows.Forms.TreeView();
            this.splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.listViewContents = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnType = new System.Windows.Forms.ColumnHeader();
            this.columnSize = new System.Windows.Forms.ColumnHeader();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.textBoxFileInfo = new System.Windows.Forms.TextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxSearchPattern = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
            this.splitContainerRight.Panel1.SuspendLayout();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonLoad);
            this.panelTop.Controls.Add(this.buttonBrowse);
            this.panelTop.Controls.Add(this.textBoxRootPath);
            this.panelTop.Controls.Add(this.labelPath);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.panelTop.Size = new System.Drawing.Size(1100, 48);
            this.panelTop.TabIndex = 0;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoad.Location = new System.Drawing.Point(1000, 8);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(88, 28);
            this.buttonLoad.TabIndex = 3;
            this.buttonLoad.Text = "Завантажити";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(896, 8);
            this.buttonBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(96, 28);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Огляд...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxRootPath
            // 
            this.textBoxRootPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRootPath.Location = new System.Drawing.Point(120, 10);
            this.textBoxRootPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxRootPath.Name = "textBoxRootPath";
            this.textBoxRootPath.Size = new System.Drawing.Size(760, 23);
            this.textBoxRootPath.TabIndex = 1;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 14);
            this.labelPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(94, 15);
            this.labelPath.TabIndex = 0;
            this.labelPath.Text = "Коренева папка:";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 48);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.treeViewMain);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerRight);
            this.splitContainerMain.Size = new System.Drawing.Size(1100, 612);
            this.splitContainerMain.SplitterDistance = 320;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 1;
            // 
            // treeViewMain
            // 
            this.treeViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewMain.HideSelection = false;
            this.treeViewMain.Location = new System.Drawing.Point(0, 0);
            this.treeViewMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeViewMain.Name = "treeViewMain";
            this.treeViewMain.Size = new System.Drawing.Size(320, 612);
            this.treeViewMain.TabIndex = 0;
            this.treeViewMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMain_AfterSelect);
            // 
            // splitContainerRight
            // 
            this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerRight.Name = "splitContainerRight";
            this.splitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRight.Panel1
            // 
            this.splitContainerRight.Panel1.Controls.Add(this.listViewContents);
            // 
            // splitContainerRight.Panel2
            // 
            this.splitContainerRight.Panel2.Controls.Add(this.panelBottom);
            this.splitContainerRight.Size = new System.Drawing.Size(775, 612);
            this.splitContainerRight.SplitterDistance = 320;
            this.splitContainerRight.SplitterWidth = 5;
            this.splitContainerRight.TabIndex = 0;
            // 
            // listViewContents
            // 
            this.listViewContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnType,
            this.columnSize});
            this.listViewContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewContents.FullRowSelect = true;
            this.listViewContents.GridLines = true;
            this.listViewContents.HideSelection = false;
            this.listViewContents.Location = new System.Drawing.Point(0, 0);
            this.listViewContents.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewContents.MultiSelect = false;
            this.listViewContents.Name = "listViewContents";
            this.listViewContents.Size = new System.Drawing.Size(775, 320);
            this.listViewContents.TabIndex = 0;
            this.listViewContents.UseCompatibleStateImageBehavior = false;
            this.listViewContents.View = System.Windows.Forms.View.Details;
            this.listViewContents.SelectedIndexChanged += new System.EventHandler(this.listViewContents_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "Назва";
            this.columnName.Width = 320;
            // 
            // columnType
            // 
            this.columnType.Text = "Тип";
            this.columnType.Width = 90;
            // 
            // columnSize
            // 
            this.columnSize.Text = "Розмір (байт)";
            this.columnSize.Width = 120;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.textBoxFileInfo);
            this.panelBottom.Controls.Add(this.labelInfo);
            this.panelBottom.Controls.Add(this.buttonSearch);
            this.panelBottom.Controls.Add(this.textBoxSearchPattern);
            this.panelBottom.Controls.Add(this.labelSearch);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(0, 0);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.panelBottom.Size = new System.Drawing.Size(775, 287);
            this.panelBottom.TabIndex = 0;
            // 
            // textBoxFileInfo
            // 
            this.textBoxFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileInfo.Location = new System.Drawing.Point(12, 88);
            this.textBoxFileInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxFileInfo.Multiline = true;
            this.textBoxFileInfo.Name = "textBoxFileInfo";
            this.textBoxFileInfo.ReadOnly = true;
            this.textBoxFileInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFileInfo.Size = new System.Drawing.Size(751, 187);
            this.textBoxFileInfo.TabIndex = 4;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(12, 64);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(155, 15);
            this.labelInfo.TabIndex = 3;
            this.labelInfo.Text = "Інформація про обраний файл:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(672, 24);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(91, 28);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Пошук";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxSearchPattern
            // 
            this.textBoxSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearchPattern.Location = new System.Drawing.Point(120, 26);
            this.textBoxSearchPattern.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSearchPattern.Name = "textBoxSearchPattern";
            this.textBoxSearchPattern.Size = new System.Drawing.Size(536, 23);
            this.textBoxSearchPattern.TabIndex = 1;
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(12, 30);
            this.labelSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(96, 15);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "Файл / маска:";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Оберіть кореневу папку для перегляду";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лаб. 6 — файловий менеджер (DirectoryInfo, FileInfo, Directory, File)";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerRight.Panel1.ResumeLayout(false);
            this.splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
            this.splitContainerRight.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxRootPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TreeView treeViewMain;
        private System.Windows.Forms.SplitContainer splitContainerRight;
        private System.Windows.Forms.ListView listViewContents;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.TextBox textBoxFileInfo;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearchPattern;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
