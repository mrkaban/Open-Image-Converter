namespace SimpleImageResizer
{
    partial class ImageResizer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageResizer));
            this.buttonStartResize = new System.Windows.Forms.Button();
            this.buttonBrowseImages = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemResizeSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.radioButtonMultiThread = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleThread = new System.Windows.Forms.RadioButton();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelOriginalResolution = new System.Windows.Forms.Label();
            this.textBoxSourceImagesPath = new System.Windows.Forms.TextBox();
            this.textBoxDestinationImagePath = new System.Windows.Forms.TextBox();
            this.buttonBrowseDestination = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelCurrentResolution = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonOpenAllInSource = new System.Windows.Forms.Button();
            this.buttonLoadDestinationImages = new System.Windows.Forms.Button();
            this.comboBoxImageFormat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarQuality = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.labelImageQuality = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelFileSize = new System.Windows.Forms.Label();
            this.folderBrowserDialogInputFiles = new System.Windows.Forms.FolderBrowserDialog();
            this.linkLabelNext = new System.Windows.Forms.LinkLabel();
            this.linkLabelPrevious = new System.Windows.Forms.LinkLabel();
            this.contextMenuStripCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.StatusLabel = new System.Windows.Forms.Label();
            this.textBoxPresetCropWidth = new System.Windows.Forms.TextBox();
            this.textBoxPresetCropHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panelImage = new System.Windows.Forms.Panel();
            this.buttonZoomFit = new System.Windows.Forms.Button();
            this.buttonZoomOut = new System.Windows.Forms.Button();
            this.buttonZoomIn = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonCrop = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).BeginInit();
            this.contextMenuStripCopyPaste.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartResize
            // 
            this.buttonStartResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartResize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartResize.ForeColor = System.Drawing.Color.Navy;
            this.buttonStartResize.Location = new System.Drawing.Point(167, 527);
            this.buttonStartResize.Name = "buttonStartResize";
            this.buttonStartResize.Size = new System.Drawing.Size(102, 49);
            this.buttonStartResize.TabIndex = 0;
            this.buttonStartResize.Text = "Начать изменения";
            this.buttonStartResize.UseVisualStyleBackColor = true;
            this.buttonStartResize.Click += new System.EventHandler(this.buttonStartResize_Click);
            // 
            // buttonBrowseImages
            // 
            this.buttonBrowseImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBrowseImages.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.buttonBrowseImages.Location = new System.Drawing.Point(301, 469);
            this.buttonBrowseImages.Name = "buttonBrowseImages";
            this.buttonBrowseImages.Size = new System.Drawing.Size(49, 23);
            this.buttonBrowseImages.TabIndex = 1;
            this.buttonBrowseImages.Text = "Обзор";
            this.buttonBrowseImages.UseVisualStyleBackColor = true;
            this.buttonBrowseImages.Click += new System.EventHandler(this.buttonBrowseImages_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(12, 498);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(419, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 531);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Разрешение:";
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.buttonClear.Location = new System.Drawing.Point(275, 527);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 49);
            this.buttonClear.TabIndex = 8;
            this.buttonClear.Text = "Сброс/Стоп";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select destination folder for resized images:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemResizeSelected,
            this.removeFromListToolStripMenuItem,
            this.toolStripMenuItemDelete,
            this.openContainingFolderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(234, 114);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItemOpen.Text = "&Открыть";
            this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // toolStripMenuItemResizeSelected
            // 
            this.toolStripMenuItemResizeSelected.Name = "toolStripMenuItemResizeSelected";
            this.toolStripMenuItemResizeSelected.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItemResizeSelected.Text = "&Изменить";
            this.toolStripMenuItemResizeSelected.Click += new System.EventHandler(this.toolStripMenuItemResizeSelected_Click);
            // 
            // removeFromListToolStripMenuItem
            // 
            this.removeFromListToolStripMenuItem.Name = "removeFromListToolStripMenuItem";
            this.removeFromListToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.removeFromListToolStripMenuItem.Text = "&Удалить из списка";
            this.removeFromListToolStripMenuItem.Click += new System.EventHandler(this.removeFromListToolStripMenuItem_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItemDelete.Text = "&Удалить с диска";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.openContainingFolderToolStripMenuItem.Text = "&Открыть содержащую папку";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.openContainingFolderToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(419, 411);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImageResizer_DragDrop);
            this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImageResizer_DragEnter);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // radioButtonMultiThread
            // 
            this.radioButtonMultiThread.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonMultiThread.AutoSize = true;
            this.radioButtonMultiThread.Checked = true;
            this.radioButtonMultiThread.Location = new System.Drawing.Point(326, 576);
            this.radioButtonMultiThread.Name = "radioButtonMultiThread";
            this.radioButtonMultiThread.Size = new System.Drawing.Size(103, 17);
            this.radioButtonMultiThread.TabIndex = 11;
            this.radioButtonMultiThread.TabStop = true;
            this.radioButtonMultiThread.Text = "Многопоточное";
            this.radioButtonMultiThread.UseVisualStyleBackColor = true;
            // 
            // radioButtonSingleThread
            // 
            this.radioButtonSingleThread.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSingleThread.AutoSize = true;
            this.radioButtonSingleThread.Location = new System.Drawing.Point(221, 576);
            this.radioButtonSingleThread.Name = "radioButtonSingleThread";
            this.radioButtonSingleThread.Size = new System.Drawing.Size(97, 17);
            this.radioButtonSingleThread.TabIndex = 12;
            this.radioButtonSingleThread.Text = "Однопоточное";
            this.radioButtonSingleThread.UseVisualStyleBackColor = true;
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Items.AddRange(new object[] {
            "3:2",
            "720x480",
            "1152x768",
            "1280x854",
            "1440x960",
            "2880x1920",
            "",
            "4:3",
            "320x240",
            "640x480",
            "800x600",
            "1024x768",
            "1152x864",
            "1280x960",
            "1400x1050",
            "1600x1200",
            "2048x1536",
            "3200x2400",
            "4000x3000",
            "6400x4800",
            "",
            "5:3",
            "800x480",
            "1280x768",
            "",
            "5:4",
            "1280x1024",
            "2560x2048",
            "5120x4096",
            "",
            "16:9",
            "852x480",
            "1280x720",
            "1365x768",
            "1600x900",
            "1920x1080",
            "",
            "16:10",
            "320x200",
            "640x400",
            "1280x800",
            "1440x900",
            "1680x1050",
            "1920x1200",
            "2560x1600",
            "3840x2400",
            "7680x4800",
            "",
            "Icons",
            "16x16",
            "20x20",
            "22x22",
            "30x30",
            "32x32",
            "40x40",
            "48x48",
            "64x64",
            "96x96",
            "120x120"});
            this.comboBoxResolution.Location = new System.Drawing.Point(76, 528);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(85, 21);
            this.comboBoxResolution.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(435, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "Исходное разреш.:";
            // 
            // labelOriginalResolution
            // 
            this.labelOriginalResolution.AutoSize = true;
            this.labelOriginalResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOriginalResolution.ForeColor = System.Drawing.Color.Navy;
            this.labelOriginalResolution.Location = new System.Drawing.Point(515, 0);
            this.labelOriginalResolution.Name = "labelOriginalResolution";
            this.labelOriginalResolution.Size = new System.Drawing.Size(0, 12);
            this.labelOriginalResolution.TabIndex = 16;
            this.labelOriginalResolution.Tag = "";
            // 
            // textBoxSourceImagesPath
            // 
            this.textBoxSourceImagesPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSourceImagesPath.BackColor = System.Drawing.Color.White;
            this.textBoxSourceImagesPath.ForeColor = System.Drawing.Color.Navy;
            this.textBoxSourceImagesPath.Location = new System.Drawing.Point(13, 471);
            this.textBoxSourceImagesPath.Name = "textBoxSourceImagesPath";
            this.textBoxSourceImagesPath.Size = new System.Drawing.Size(282, 20);
            this.textBoxSourceImagesPath.TabIndex = 17;
            this.textBoxSourceImagesPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSourceImagesPath_MouseDoubleClick);
            // 
            // textBoxDestinationImagePath
            // 
            this.textBoxDestinationImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDestinationImagePath.BackColor = System.Drawing.Color.White;
            this.textBoxDestinationImagePath.ForeColor = System.Drawing.Color.Navy;
            this.textBoxDestinationImagePath.Location = new System.Drawing.Point(13, 438);
            this.textBoxDestinationImagePath.Name = "textBoxDestinationImagePath";
            this.textBoxDestinationImagePath.Size = new System.Drawing.Size(282, 20);
            this.textBoxDestinationImagePath.TabIndex = 19;
            this.textBoxDestinationImagePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDestinationImagePath_MouseDoubleClick);
            // 
            // buttonBrowseDestination
            // 
            this.buttonBrowseDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBrowseDestination.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.buttonBrowseDestination.Location = new System.Drawing.Point(301, 438);
            this.buttonBrowseDestination.Name = "buttonBrowseDestination";
            this.buttonBrowseDestination.Size = new System.Drawing.Size(49, 23);
            this.buttonBrowseDestination.TabIndex = 18;
            this.buttonBrowseDestination.Text = "Обзор";
            this.buttonBrowseDestination.UseVisualStyleBackColor = true;
            this.buttonBrowseDestination.Click += new System.EventHandler(this.buttonBrowseDestination_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 459);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "Исходные файлы изображений:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 426);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "Каталог назначения для измененных изображений:";
            // 
            // labelCurrentResolution
            // 
            this.labelCurrentResolution.AutoSize = true;
            this.labelCurrentResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrentResolution.ForeColor = System.Drawing.Color.Navy;
            this.labelCurrentResolution.Location = new System.Drawing.Point(700, 0);
            this.labelCurrentResolution.Name = "labelCurrentResolution";
            this.labelCurrentResolution.Size = new System.Drawing.Size(0, 12);
            this.labelCurrentResolution.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(619, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "Текущее разреш.:";
            // 
            // buttonOpenAllInSource
            // 
            this.buttonOpenAllInSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpenAllInSource.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.buttonOpenAllInSource.Location = new System.Drawing.Point(356, 471);
            this.buttonOpenAllInSource.Name = "buttonOpenAllInSource";
            this.buttonOpenAllInSource.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenAllInSource.TabIndex = 24;
            this.buttonOpenAllInSource.Text = "Загрузить";
            this.buttonOpenAllInSource.UseVisualStyleBackColor = true;
            this.buttonOpenAllInSource.Click += new System.EventHandler(this.buttonOpenAllInSource_Click);
            // 
            // buttonLoadDestinationImages
            // 
            this.buttonLoadDestinationImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoadDestinationImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLoadDestinationImages.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.buttonLoadDestinationImages.Location = new System.Drawing.Point(356, 438);
            this.buttonLoadDestinationImages.Name = "buttonLoadDestinationImages";
            this.buttonLoadDestinationImages.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadDestinationImages.TabIndex = 25;
            this.buttonLoadDestinationImages.Text = "Загрузить";
            this.buttonLoadDestinationImages.UseVisualStyleBackColor = true;
            this.buttonLoadDestinationImages.Click += new System.EventHandler(this.buttonLoadDestinationImages_Click);
            // 
            // comboBoxImageFormat
            // 
            this.comboBoxImageFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImageFormat.FormattingEnabled = true;
            this.comboBoxImageFormat.Location = new System.Drawing.Point(76, 555);
            this.comboBoxImageFormat.Name = "comboBoxImageFormat";
            this.comboBoxImageFormat.Size = new System.Drawing.Size(85, 21);
            this.comboBoxImageFormat.TabIndex = 27;
            this.comboBoxImageFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxImageFormat_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 558);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Формат:";
            // 
            // trackBarQuality
            // 
            this.trackBarQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarQuality.Location = new System.Drawing.Point(10, 594);
            this.trackBarQuality.Maximum = 100;
            this.trackBarQuality.Name = "trackBarQuality";
            this.trackBarQuality.Size = new System.Drawing.Size(422, 45);
            this.trackBarQuality.TabIndex = 28;
            this.trackBarQuality.Value = 55;
            this.trackBarQuality.Scroll += new System.EventHandler(this.trackBarQuality_Scroll);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 627);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(185, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Настройка качества изображения:";
            // 
            // labelImageQuality
            // 
            this.labelImageQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelImageQuality.AutoSize = true;
            this.labelImageQuality.ForeColor = System.Drawing.Color.Navy;
            this.labelImageQuality.Location = new System.Drawing.Point(121, 627);
            this.labelImageQuality.Name = "labelImageQuality";
            this.labelImageQuality.Size = new System.Drawing.Size(0, 13);
            this.labelImageQuality.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(492, 635);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 31;
            this.label9.Text = "Размер файла:";
            // 
            // labelFileSize
            // 
            this.labelFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFileSize.AutoSize = true;
            this.labelFileSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFileSize.ForeColor = System.Drawing.Color.Navy;
            this.labelFileSize.Location = new System.Drawing.Point(533, 635);
            this.labelFileSize.Name = "labelFileSize";
            this.labelFileSize.Size = new System.Drawing.Size(0, 12);
            this.labelFileSize.TabIndex = 32;
            this.labelFileSize.Tag = "";
            // 
            // folderBrowserDialogInputFiles
            // 
            this.folderBrowserDialogInputFiles.Description = "Please select the images you would like to resize";
            // 
            // linkLabelNext
            // 
            this.linkLabelNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelNext.AutoSize = true;
            this.linkLabelNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelNext.Location = new System.Drawing.Point(957, 634);
            this.linkLabelNext.Name = "linkLabelNext";
            this.linkLabelNext.Size = new System.Drawing.Size(56, 13);
            this.linkLabelNext.TabIndex = 34;
            this.linkLabelNext.TabStop = true;
            this.linkLabelNext.Text = "Вперед>>";
            this.linkLabelNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNext_LinkClicked);
            // 
            // linkLabelPrevious
            // 
            this.linkLabelPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelPrevious.AutoSize = true;
            this.linkLabelPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelPrevious.Location = new System.Drawing.Point(434, 634);
            this.linkLabelPrevious.Name = "linkLabelPrevious";
            this.linkLabelPrevious.Size = new System.Drawing.Size(51, 13);
            this.linkLabelPrevious.TabIndex = 33;
            this.linkLabelPrevious.TabStop = true;
            this.linkLabelPrevious.Text = "<<Назад";
            this.linkLabelPrevious.Click += new System.EventHandler(this.linkLabelPrevious_Click);
            // 
            // contextMenuStripCopyPaste
            // 
            this.contextMenuStripCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripCopyPaste.Name = "contextMenuStripCopyPaste";
            this.contextMenuStripCopyPaste.Size = new System.Drawing.Size(123, 70);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pasteToolStripMenuItem.Text = "&Вставить";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.removeToolStripMenuItem.Text = "&Убрать";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.deleteToolStripMenuItem.Text = "&Удалить";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 1000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StatusLabel.ForeColor = System.Drawing.Color.Blue;
            this.StatusLabel.Location = new System.Drawing.Point(1, 653);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            this.StatusLabel.TabIndex = 35;
            // 
            // textBoxPresetCropWidth
            // 
            this.textBoxPresetCropWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPresetCropWidth.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPresetCropWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPresetCropWidth.ForeColor = System.Drawing.Color.Blue;
            this.textBoxPresetCropWidth.Location = new System.Drawing.Point(910, 651);
            this.textBoxPresetCropWidth.Name = "textBoxPresetCropWidth";
            this.textBoxPresetCropWidth.Size = new System.Drawing.Size(30, 20);
            this.textBoxPresetCropWidth.TabIndex = 36;
            this.textBoxPresetCropWidth.TextChanged += new System.EventHandler(this.textBoxPresetCropWidth_TextChanged);
            // 
            // textBoxPresetCropHeight
            // 
            this.textBoxPresetCropHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPresetCropHeight.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPresetCropHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPresetCropHeight.ForeColor = System.Drawing.Color.Blue;
            this.textBoxPresetCropHeight.Location = new System.Drawing.Point(962, 651);
            this.textBoxPresetCropHeight.Name = "textBoxPresetCropHeight";
            this.textBoxPresetCropHeight.Size = new System.Drawing.Size(30, 20);
            this.textBoxPresetCropHeight.TabIndex = 37;
            this.textBoxPresetCropHeight.TextChanged += new System.EventHandler(this.textBoxPresetCropHeight_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(732, 654);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Предустановленный размер:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(893, 654);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "X:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(946, 654);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "Y:";
            // 
            // panelImage
            // 
            this.panelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelImage.AutoScroll = true;
            this.panelImage.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelImage.BackgroundImage")));
            this.panelImage.Controls.Add(this.buttonZoomFit);
            this.panelImage.Controls.Add(this.buttonZoomOut);
            this.panelImage.Controls.Add(this.buttonZoomIn);
            this.panelImage.Controls.Add(this.pictureBoxPreview);
            this.panelImage.Location = new System.Drawing.Point(437, 16);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(571, 615);
            this.panelImage.TabIndex = 41;
            // 
            // buttonZoomFit
            // 
            this.buttonZoomFit.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomFit.Image")));
            this.buttonZoomFit.Location = new System.Drawing.Point(62, 11);
            this.buttonZoomFit.Name = "buttonZoomFit";
            this.buttonZoomFit.Size = new System.Drawing.Size(45, 45);
            this.buttonZoomFit.TabIndex = 45;
            this.buttonZoomFit.UseVisualStyleBackColor = true;
            this.buttonZoomFit.Click += new System.EventHandler(this.buttonZoomFit_Click);
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomOut.Image")));
            this.buttonZoomOut.Location = new System.Drawing.Point(113, 11);
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.Size = new System.Drawing.Size(45, 45);
            this.buttonZoomOut.TabIndex = 44;
            this.buttonZoomOut.UseVisualStyleBackColor = true;
            this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
            // 
            // buttonZoomIn
            // 
            this.buttonZoomIn.BackColor = System.Drawing.Color.Transparent;
            this.buttonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomIn.Image")));
            this.buttonZoomIn.Location = new System.Drawing.Point(11, 11);
            this.buttonZoomIn.Name = "buttonZoomIn";
            this.buttonZoomIn.Size = new System.Drawing.Size(45, 45);
            this.buttonZoomIn.TabIndex = 43;
            this.buttonZoomIn.UseVisualStyleBackColor = false;
            this.buttonZoomIn.Click += new System.EventHandler(this.buttonZoomIn_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.White;
            this.pictureBoxPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxPreview.BackgroundImage")));
            this.pictureBoxPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 4);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(419, 499);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPreview.TabIndex = 11;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPreview_MouseClick);
            this.pictureBoxPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPreview_MouseDown);
            this.pictureBoxPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPreview_MouseMove);
            this.pictureBoxPreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPreview_MouseUp);
            // 
            // buttonCrop
            // 
            this.buttonCrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCrop.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.buttonCrop.Image = ((System.Drawing.Image)(resources.GetObject("buttonCrop.Image")));
            this.buttonCrop.Location = new System.Drawing.Point(356, 527);
            this.buttonCrop.Name = "buttonCrop";
            this.buttonCrop.Size = new System.Drawing.Size(75, 49);
            this.buttonCrop.TabIndex = 42;
            this.buttonCrop.UseVisualStyleBackColor = true;
            this.buttonCrop.Click += new System.EventHandler(this.buttonCrop_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(121, 578);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "Преобразование:";
            // 
            // ImageResizer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 672);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.buttonCrop);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPresetCropHeight);
            this.Controls.Add(this.textBoxPresetCropWidth);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.linkLabelNext);
            this.Controls.Add(this.linkLabelPrevious);
            this.Controls.Add(this.comboBoxImageFormat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelFileSize);
            this.Controls.Add(this.labelImageQuality);
            this.Controls.Add(this.trackBarQuality);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonLoadDestinationImages);
            this.Controls.Add(this.buttonOpenAllInSource);
            this.Controls.Add(this.textBoxDestinationImagePath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonBrowseDestination);
            this.Controls.Add(this.textBoxSourceImagesPath);
            this.Controls.Add(this.comboBoxResolution);
            this.Controls.Add(this.labelCurrentResolution);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.radioButtonSingleThread);
            this.Controls.Add(this.radioButtonMultiThread);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.labelOriginalResolution);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonBrowseImages);
            this.Controls.Add(this.buttonStartResize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1024, 710);
            this.Name = "ImageResizer";
            this.Text = "Open Image Converter от КонтинентСвободы.рф";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageResizer_FormClosing);
            this.Load += new System.EventHandler(this.ImageResizer_Load);
            this.SizeChanged += new System.EventHandler(this.ImageResizer_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImageResizer_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImageResizer_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageResizer_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).EndInit();
            this.contextMenuStripCopyPaste.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            this.panelImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartResize;
        private System.Windows.Forms.Button buttonBrowseImages;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemResizeSelected;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioButtonMultiThread;
        private System.Windows.Forms.RadioButton radioButtonSingleThread;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelOriginalResolution;
        private System.Windows.Forms.TextBox textBoxSourceImagesPath;
        private System.Windows.Forms.ToolStripMenuItem removeFromListToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxDestinationImagePath;
        private System.Windows.Forms.Button buttonBrowseDestination;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelCurrentResolution;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonOpenAllInSource;
        private System.Windows.Forms.Button buttonLoadDestinationImages;
        private System.Windows.Forms.ComboBox comboBoxImageFormat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarQuality;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelImageQuality;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelFileSize;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogInputFiles;
        private System.Windows.Forms.LinkLabel linkLabelNext;
        private System.Windows.Forms.LinkLabel linkLabelPrevious;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.TextBox textBoxPresetCropWidth;
        private System.Windows.Forms.TextBox textBoxPresetCropHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button buttonCrop;
        private System.Windows.Forms.Button buttonZoomIn;
        private System.Windows.Forms.Button buttonZoomOut;
        private System.Windows.Forms.Button buttonZoomFit;
        private System.Windows.Forms.Label label12;
    }
}

