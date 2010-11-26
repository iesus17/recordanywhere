using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using mshtml;
using Recorder.Core;

namespace Recorder
{
    public partial class UserBar : SpicIE.Controls.Toolbar
    {
        #region member

        public static UserBar instance;

        public bool IsRecorder = false;

        public SHDocVw.IWebBrowser2 ActiveBrowser;
       

        #endregion

        public UserBar()
        {
            instance = this;
            InitializeComponent();
            this.ToolbarStyle = ToolbarEnum.Vertical;
            this.ToolbarHelpText = this.ToolbarName = this.ToolbarTitle = PluginProgID;
            this.Size = new System.Drawing.Size(188, 100);
        }

        #region SpicIE required COM properties

        public override string PluginGuid
        {
            get
            {
                return "44CC6754-7D52-4c73-BDD0-3B5D71848958";
            }
        }

        public override string PluginProgID
        {
            get
            {
                return "Recorder.UserBar";
            }
        }

        #endregion SpicIE required COM properties


        #region Events
        public void record_Click(object sender, EventArgs e)
        {
            Recorder.isRecorder = true;
            Recorder.isPerfRecorder = true;
        }


        private void stop_Click(object sender, EventArgs e)
        {
            Recorder.isRecorder = false;
            Recorder.isPerfRecorder = false;
        }
      
        #endregion

      
    }
}
