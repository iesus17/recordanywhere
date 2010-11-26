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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserBar));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Steps");
            this.record = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.pause = new System.Windows.Forms.Button();
            this.SteptreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // record
            // 
            this.record.Image = ((System.Drawing.Image)(resources.GetObject("record.Image")));
            this.record.Location = new System.Drawing.Point(14, 4);
            this.record.Name = "record";
            this.record.Size = new System.Drawing.Size(33, 25);
            this.record.TabIndex = 0;
            this.record.UseVisualStyleBackColor = true;
            this.record.Click += new System.EventHandler(this.record_Click);
            // 
            // stop
            // 
            this.stop.Image = ((System.Drawing.Image)(resources.GetObject("stop.Image")));
            this.stop.Location = new System.Drawing.Point(76, 4);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(33, 25);
            this.stop.TabIndex = 1;
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // pause
            // 
            this.pause.Image = ((System.Drawing.Image)(resources.GetObject("pause.Image")));
            this.pause.Location = new System.Drawing.Point(139, 4);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(33, 25);
            this.pause.TabIndex = 2;
            this.pause.UseVisualStyleBackColor = true;
            // 
            // SteptreeView
            // 
            this.SteptreeView.Location = new System.Drawing.Point(3, 35);
            this.SteptreeView.Name = "SteptreeView";
            treeNode1.Name = "";
            treeNode1.Text = "Steps";
            this.SteptreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.SteptreeView.Size = new System.Drawing.Size(1000, 720);
            this.SteptreeView.TabIndex = 3;
            // 
            // UserBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
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
    }
}
