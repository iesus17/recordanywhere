namespace Recorder
{
    using ColumnHeader = SourceGrid.Cells.ColumnHeader;
    partial class UserBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Steps");
            this.record = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.pause = new System.Windows.Forms.Button();
            this.SteptreeView = new System.Windows.Forms.TreeView();
            this.capturePic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // record
            // 
            this.record.Location = new System.Drawing.Point(4, 4);
            this.record.Name = "record";
            this.record.Size = new System.Drawing.Size(50, 25);
            this.record.TabIndex = 0;
            this.record.Text = "Record";
            this.record.UseVisualStyleBackColor = true;
            this.record.Click += new System.EventHandler(this.record_Click);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(60, 4);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(50, 25);
            this.stop.TabIndex = 1;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // pause
            // 
            this.pause.Location = new System.Drawing.Point(115, 4);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(50, 25);
            this.pause.TabIndex = 2;
            this.pause.Text = "Replay";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // SteptreeView
            // 
            this.SteptreeView.Location = new System.Drawing.Point(3, 35);
            this.SteptreeView.Name = "SteptreeView";
            treeNode3.Name = "";
            treeNode3.Text = "Steps";
            this.SteptreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.SteptreeView.Size = new System.Drawing.Size(1000, 720);
            this.SteptreeView.TabIndex = 3;
            // 
            // capturePic
            // 
            this.capturePic.Location = new System.Drawing.Point(168, 4);
            this.capturePic.Name = "capturePic";
            this.capturePic.Size = new System.Drawing.Size(55, 25);
            this.capturePic.TabIndex = 4;
            this.capturePic.Text = "Capture";
            this.capturePic.UseVisualStyleBackColor = true;
            this.capturePic.Click += new System.EventHandler(this.capturePic_Click);
            // 
            // UserBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.capturePic);
            this.Controls.Add(this.SteptreeView);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.record);
            this.Name = "UserBar";
            this.Size = new System.Drawing.Size(133, 133);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button record;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button pause;
        public System.Windows.Forms.TreeView SteptreeView;
        private System.Windows.Forms.Button capturePic;
    }
}
