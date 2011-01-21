using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using mshtml;
using Recorder.Core;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using SHDocVw;
using Recorder.Core.Utils;
using System.Diagnostics;
using System.Threading;
using Recorder.Core.Actions;


namespace Recorder
{
    public struct _RECTL
    {
        public int bottom;
        public int left;
        public int right;
        public int top;
    }

    public partial class UserBar : SpicIE.Controls.Toolbar
    {
        #region member

        public static UserBar instance;

        public bool IsRecorder = false;

        public SHDocVw.IWebBrowser2 ActiveBrowser;

        public const int GW_CHILD = 5;
        public const int GW_HWNDNEXT = 2;

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


        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parent /*HWND*/, IntPtr next /*HWND*/, string sClassName, IntPtr sWindowTitle);

        [DllImport("user32.Dll")]
        public static extern void GetClassName(int h, StringBuilder s, int nMaxCount);


        private void capturePic_Click2(object sender, EventArgs e)
        {
            string captureLocation = @"D:\IECapt\IECapt.exe";
            SHDocVw.WebBrowser m_browser = null;//get browser

            SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass(); //
            string filename;
            foreach (SHDocVw.WebBrowser ie in shellWindows)
            {
                filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();

                if (filename.Equals("iexplore"))
                {
                    m_browser = ie;
                    break;
                }
            }

            Process p = Process.Start(captureLocation, "--url=" + m_browser.LocationURL + " --out=" + @"c:\\IECapture\Captured_" + DateTime.Now.ToString("yyyy-mm-dd") + ".jpg");
            p.WaitForExit();
        }


        private void capturePic_Click(object sender, EventArgs e)
        {
            SHDocVw.WebBrowser m_browser = null;//get browser

            SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass(); //
            string filename;
            foreach (SHDocVw.WebBrowser ie in shellWindows)
            {
                filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();

                if (filename.Equals("iexplore"))
                {
                    m_browser = ie;
                    break;
                }
            }
            //Assign Browser Document
            mshtml.IHTMLDocument2 myDoc = (mshtml.IHTMLDocument2)m_browser.Document;
            DispHTMLDocument myDocImpl = myDoc as DispHTMLDocument;
            HTMLDocument myHtmlDoc = myDoc as HTMLDocument;
            //      IHTMLDocument2 doc2 = (IHTMLDocument2)m_browser.Document;
            //      IHTMLDocument3 doc3 = (IHTMLDocument3)m_browser.Document;
            //      IHTMLElement2 body2 = (IHTMLElement2)doc2.body;
            //      IHTMLElement2 root2 = (IHTMLElement2)doc3.documentElement;

            //      int width = Math.Max(body2.scrollWidth, root2.scrollWidth);
            //      int height = Math.Max(root2.scrollHeight, body2.scrollHeight);

            //      //m_browser.SetBounds(0, 0, width, height);
            //      width = Math.Max(body2.scrollWidth, root2.scrollWidth);
            //      height = Math.Max(root2.scrollHeight, body2.scrollHeight);
            //      //m_browser.SetBounds(0, 0, width, height);

            //      Bitmap image = new Bitmap(width, height);
            //      Graphics g = Graphics.FromImage(image);

            //      _RECTL bounds;
            //      bounds.left = 0;
            //      bounds.top = 0;
            //      bounds.right = width;
            //      bounds.bottom = height;

            //      IntPtr hdc = g.GetHdc();
            //      IViewObject iv = doc2 as IViewObject;

            //      // TODO: Write to Metafile instead if requested.1, -1, (IntPtr)0, (IntPtr)0, (IntPtr)0,(IntPtr)hdc, ref bounds, (IntPtr)0, (IntPtr)0, 0

            //      iv.Draw(1, -1, (IntPtr)0, (IntPtr)0, (IntPtr)0,
            //(IntPtr)hdc, ref bounds, (IntPtr)0, (IntPtr)0, 0);

            //      g.ReleaseHdc(hdc);
            //      image.Save("c:\\1.jpg");
            //      image.Dispose();



            //URL Location

            IHTMLDocument2 doc2 = (IHTMLDocument2)m_browser.Document;
            IHTMLDocument3 doc3 = (IHTMLDocument3)m_browser.Document;
            IHTMLElement2 body2 = (IHTMLElement2)doc2.body;
            IHTMLElement2 root2 = (IHTMLElement2)doc3.documentElement;

            int width = Math.Max(body2.scrollWidth, root2.scrollWidth);
            int height = Math.Max(root2.scrollHeight, body2.scrollHeight);

            string myLocalLink = myDoc.url;
            int URLExtraHeight = 0;
            int URLExtraLeft = 0;

            //Adjustment variable for capture size.
            //if (chkWriteURL.Checked == true)
            //  URLExtraHeight = 25;

            //TrimHeight and TrimLeft trims off some captured IE graphics.
            int trimHeight = 3;
            int trimLeft = 3;
            //Use UrlExtra height to carry trimHeight.
            URLExtraHeight = URLExtraHeight - trimHeight;
            URLExtraLeft = URLExtraLeft - trimLeft;

            myDoc.body.setAttribute("scroll", "yes", 0);

            //Get Browser Window Height
            int heightsize = (int)myDoc.body.getAttribute("scrollHeight", 0);
            int widthsize = (int)myDoc.body.getAttribute("scrollWidth", 0);

            //Get Screen Height
            int screenHeight = (int)myDoc.body.getAttribute("clientHeight", 0);
            int screenWidth = (int)myDoc.body.getAttribute("clientWidth", 0);

            //Get bitmap to hold screen fragment.
            Bitmap bm = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            //Create a target bitmap to draw into.
            Bitmap bm2 = new Bitmap(widthsize + URLExtraLeft, heightsize + URLExtraHeight - trimHeight, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Graphics g2 = Graphics.FromImage(bm2);

            Graphics g = null;
            IntPtr hdc;
            Image screenfrag = null;
            int brwTop = 0;
            int brwLeft = 0;
            int myPage = 0;
            IntPtr myIntptr = (IntPtr)m_browser.HWND;


            //Get inner browser window.
            int hwndInt = myIntptr.ToInt32();
            IntPtr hwnd = myIntptr;

            FindWindow fw = new FindWindow(hwnd, "Internet Explorer_Server");
            hwnd = fw.FoundHandle;

            string bodyId = (string)myDoc.body.getAttribute("id", 0);
            if (bodyId == null)
            {
                myDoc.body.setAttribute("id", "current", 0);
                bodyId =(string) myDoc.body.getAttribute("id", 0);
            }
            //Get Screen Height (for bottom up screen drawing)
            while ((myPage * screenHeight) < height)
            {
                myDoc.body.setAttribute("scrollTop", (screenHeight - 5) * myPage, 0);
                ++myPage;
            }
            //Rollback the page count by one
            --myPage;

            int myPageWidth = 0;

            while ((myPageWidth * screenWidth) < width)
            {
                //myDocImpl.getElementById(bodyId).setAttribute("scrollLeft", (screenWidth - 5) * myPageWidth, 0);
                //myDoc.body.setAttribute("scrollLeft", (screenWidth - 5) * myPageWidth, 0);
                //brwLeft = (int)myDoc.body.getAttribute("scrollLeft", 0);
                //brwLeft = (int)myDocImpl.getElementById(bodyId).getAttribute("scrollLeft", 0);
                for (int i = myPage; i >= 0; --i)
                {
                    //Shoot visible window
                    g = Graphics.FromImage(bm);
                    hdc = g.GetHdc();
                    //myDoc.body.setAttribute("scrollTop", (screenHeight - 5) * i, 0);
                    //brwTop = (int)myDoc.body.getAttribute("scrollTop", 0);
                    //myDocImpl.getElementById(bodyId).setAttribute("scrollTop", (screenHeight - 5) * myPageWidth, 0);
                    //brwTop = (int)myDocImpl.getElementById(bodyId).getAttribute("scrollTop", 0);
                    if (i == 0)
                        myDocImpl.parentWindow.scrollBy(0,(screenHeight - 5) * i);// = height;
                    else
                        myDocImpl.parentWindow.scrollTo((screenWidth - 5) * myPageWidth, (screenHeight - 5) * i);
                    PrintWindow(hwnd, hdc, 0);
                    g.ReleaseHdc(hdc);
                    g.Flush();
                    screenfrag = Image.FromHbitmap(bm.GetHbitmap());
                    g2.DrawImage(screenfrag, brwLeft + URLExtraLeft, brwTop + URLExtraHeight);
                }
                ++myPageWidth;
            }


            //Reduce Resolution Size
            double myResolution = Convert.ToDouble(1024) * 0.01;
            int finalWidth = (int)((widthsize + URLExtraLeft) * myResolution);
            int finalHeight = (int)((heightsize + URLExtraHeight) * myResolution);
            Bitmap finalImage = new Bitmap(finalWidth, finalHeight, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Graphics gFinal = Graphics.FromImage((Image)finalImage);
            gFinal.DrawImage(bm2, 0, 0, finalWidth, finalHeight);

            //Get Time Stamp
            DateTime myTime = DateTime.Now;
            String format = "MM.dd.hh.mm.ss";
        
            //Create Directory to save image to.
            if (!Directory.Exists("C:\\IECapture"))
                Directory.CreateDirectory("C:\\IECapture");

            //Write Image.
            EncoderParameters eps = new EncoderParameters(1);
            long myQuality = 75L;//Convert.ToInt64(cmbQuality.Text);
            eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, myQuality);
            ImageCodecInfo ici = GetEncoderInfo("image/jpeg");
            finalImage.Save(@"c:\\IECapture\Captured_" + myTime.ToString(format) + ".jpg", ici, eps);



            myDoc = null;
            g.Dispose();
            g2.Dispose();
            gFinal.Dispose();
            bm.Dispose();
            bm2.Dispose();
            finalImage.Dispose();

        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        //actually it is replay // mock
        private void pause_Click(object sender, EventArgs e)
        {
            //lauch a new browser
            IHTMLDocument2 doc = null;
            InternetExplorer ie = LauchBrowser();
            TreeNodeCollection nodes = SteptreeView.Nodes[0].Nodes;
            //foreach (TreeNode node in nodes)
            //{
            if (nodes[0].Text.StartsWith("Browser ; "))
            {
                string txt = nodes[0].Text;
                string str = "Browser ; " + BrowserActionEnum.Nav;
                int start = txt.IndexOf(str);
                int end = txt.IndexOf(";time cost:");
                string url = txt.Substring(start + str.Length, end - start - str.Length);
                object Flags = new object();
                object TargetFrameName = new object();
                object PostData = new object();
                object Headers = new object();
                ie.Navigate(url, ref Flags, ref TargetFrameName, ref PostData, ref Headers);

            }
            Thread.Sleep(5000);

            doc = ie.Document as IHTMLDocument2;

            mshtml.IHTMLElementCollection inputs;
            inputs = (mshtml.IHTMLElementCollection)doc.all.tags("INPUT");
            mshtml.IHTMLElement elementTxt = (mshtml.IHTMLElement)inputs.item("q", 0);
            ((IHTMLInputElement)elementTxt).value = "GBS";
            Thread.Sleep(3000);
            mshtml.IHTMLElement element = (mshtml.IHTMLElement)inputs.item("btnG", 0);
            element.click();

            //}
        }

        private InternetExplorer LauchBrowser()
        {
            string IELocation = @"C:\Program Files\Internet Explorer\iexplore.exe";
            IELocation = System.Environment.ExpandEnvironmentVariables(IELocation);

            //Console.WriteLine("Launching IE ");
            Process p = Process.Start(IELocation, "about:blank");
            //int handle = (int)p.MainWindowHandle;
            Thread.Sleep(3000);

            //Console.WriteLine("Attaching to IE ... ");
            InternetExplorer ie = null;
            try
            {
                //if (p != null)
                //{
                //Console.WriteLine("Process handle is: " + p.MainWindowHandle.ToString());
                SHDocVw.ShellWindows allBrowsers = new ShellWindows();
                //Console.WriteLine("Number of active IEs :" + allBrowsers.Count.ToString());
                if (allBrowsers.Count != 0)
                {
                    for (int i = 0; i < allBrowsers.Count; i++)
                    {
                        InternetExplorer e = (InternetExplorer)allBrowsers.Item(i);
                        if (e != null)
                        {
                            if (e.LocationURL == "about:blank")
                            //if (e.HWND == (int)p.MainWindowHandle)
                            {
                                ie = e;
                                break;
                            }
                        }
                    }
                }
                else
                    throw new Exception("Faul to find IE");
                //}
                //else
                //    throw new Exception("Fail to launch IE");
            }
            catch (Exception e)
            {
                throw e;
            }
            return ie;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            //popup dialog to save the test
            //string path = "c:\\testscript";
            saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //save the test case 
                string name = saveFileDialog1.FileName;
                using (StreamWriter file = new StreamWriter(File.Create(name)))
                {
                    StreamReader sr = new StreamReader(File.Open("c:\\Simple.txt",FileMode.Open));
                    while(!sr.EndOfStream)
                    {
                        file.WriteLine(sr.ReadLine());
                    }
                   sr.Close();
                    file.Flush();
                    file.Close();
                    
                }
            }
        }



    }
}
