using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using SpicIE;
using Recorder.Core;
using SHDocVw;
using mshtml;
using System.Text;
using Recorder.Core.Find;
using Recorder.Core.Actions;
using System.Net;


namespace Recorder
{
    [ComVisible(true),
    ClassInterfaceAttribute(ClassInterfaceType.AutoDual),
    GuidAttribute(Recorder.PLUGIN_GUID),
    ProgIdAttribute(Recorder.PLUGIN_PROGID)]
    public class Recorder : SpicIE.Host
    {
        #region members
        public const string PLUGIN_GUID = "7b3d8b8c-3fcd-4aa4-986d-68e72e0a5512";
        public const string PLUGIN_PROGID = "Recorder.SpicIE";

        public static Recorder HostInstance = null;

        public static bool isRecorder = false;
        public static bool isPerfRecorder = false;
        public IHTMLElement CloneInputElement;

        public DateTime start;

        public DateTime end;

        public const string head = "HEAD";
        public const string clength = "Content-Length";

        public const string http = "http://";

        public const string https = "https://";
        #endregion

        public Recorder()
            : base()
        {
            HostInstance = this;
            this.OnBeforeNavigate += Recorder_OnBeforeNavigate;
            this.OnDownloadBegin += Recorder_OnDownloadBegin;
            this.OnDocumentComplete += Recorder_OnDocumentComplete2;
            this.OnDownloadComplete += Recorder_OnDownloadComplete;
            this.OnFileDownload += Recorder_OnFileDownload;
            this.OnFullScreen += Recorder_OnFullScreen;
            this.OnNavigateComplete += Recorder_OnNavigateComplete;
            this.OnNavigateError += Recorder_OnNavigateError;
        }

        #region Browser Events
        void GetWebContentSize(string url, out int ContentLength)
        {
            WebRequest req;
            WebResponse resp = null;
            try
            {
                 req = WebRequest.Create(url) as HttpWebRequest;

                req.Method = head;

                 resp = req.GetResponse();
                int.TryParse(resp.Headers.Get(clength), out ContentLength);

                resp.Close();

                req = null;
            }
           
            finally 
            {
                if (resp != null)
                    resp.Close();
            }
        }

        void ParseUrl(string address, out string host)
        {
            if (address.ToLower().IndexOf(http) != -1)
            {
                host = address.Substring(0, address.IndexOf("/", http.Length) );
            }
            else if (address.ToLower().IndexOf(https) != -1)
                host = address.Substring(0, address.IndexOf("/", https.Length));
            else
                host = string.Empty;
        }

        void Recorder_OnDocumentComplete2(object pDisp, ref object url)
        {
            //Mouse Hook use the Dom Event
            //System.Diagnostics.Trace.WriteLine("Recorder_OnDocumentComplete2");
            if (isRecorder)
            {
                if (UserBar.instance != null)
                {
                    //Get Web Browser
                    if (Recorder.HostInstance != null && Recorder.HostInstance.BrowserRef != null)
                    {
                        IWebBrowser2 ActiveBrowser = Recorder.HostInstance.BrowserRef as SHDocVw.IWebBrowser2;


                        if (ActiveBrowser != null)
                        {
                            if (((IWebBrowser2)pDisp) == ActiveBrowser)
                            {
                                //Get the current document
                                IHTMLDocument2 htmlDoc = (IHTMLDocument2)ActiveBrowser.Document;

                                DispHTMLDocument doc = (DispHTMLDocument)htmlDoc;

                                //Record the basic Url
                                string address = ActiveBrowser.LocationURL;

                                string host = string.Empty;
                                ParseUrl(address, out host);
                                //fetch all of the image, css etc
                                HTMLStyleSheetsCollection styles = htmlDoc.styleSheets;
                                foreach (IHTMLStyleSheet style in styles)
                                {
                                    //Get Size
                                    int content;
                                    GetWebContentSize(host + style.href, out content);
                                    StepRecorder.RecordStep(UserBar.instance.SteptreeView, style.href + " size:" + content);
                                }

                                IHTMLElementCollection scripts = htmlDoc.scripts;
                                foreach (IHTMLScriptElement script in scripts)
                                {
                                    int content;
                                    GetWebContentSize(host + script.src, out content);
                                    StepRecorder.RecordStep(UserBar.instance.SteptreeView, script.src + " size:" + content);
                                }
                                //IHTMLElementCollection imgs = doc.images;
                                //foreach (IHTMLElement img in imgs)
                                //{
                                //    StepRecorder.RecordStep(UserBar.instance.SteptreeView, img.);
                                //}

                                IHTMLWindow2 win = htmlDoc.parentWindow;



                                if (htmlDoc != null)
                                {
                                    _BindDomEvent(doc);
                                }
                            }
                        }
                    }
                }
            }
            //record the load time

        }

        void Recorder_OnBeforeNavigate(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
        {
            //System.Diagnostics.Trace.WriteLine( "Recorder_OnBeforeNavigate");
            if (isPerfRecorder)
            {
                //record start time
                start = DateTime.Now;
            }
        }

        void Recorder_OnDownloadBegin()
        {
            throw new Exception("Not Implement");
        }

        void Recorder_OnDownloadComplete()
        {
            throw new Exception("Not Implement");
        }

        void Recorder_OnFileDownload(bool ActiveDocument, ref bool Cancel)
        {
            throw new Exception("Not Implement");
        }

        void Recorder_OnFullScreen(bool active)
        {
            throw new Exception("Not Implement");
        }

        void Recorder_OnNavigateComplete(object pDisp, ref object URL)
        {
            //System.Diagnostics.Trace.WriteLine("Recorder_OnNavigateComplete");
            if (isRecorder)
            {
                if (UserBar.instance != null)
                {
                    //Get Web Browser
                    if (Recorder.HostInstance != null && Recorder.HostInstance.BrowserRef != null)
                    {
                        IWebBrowser2 ActiveBrowser = Recorder.HostInstance.BrowserRef as SHDocVw.IWebBrowser2;

                        if (ActiveBrowser != null)
                        {
                            if (isPerfRecorder)
                            {
                                string currUrl = "Browser ; " + BrowserActionEnum.Nav + ActiveBrowser.LocationURL + ";time cost:" + Convert.ToString(DateTime.Now - start);

                                StepRecorder.RecordStep(UserBar.instance.SteptreeView, currUrl);
                            }
                            //Record the URL
                            else
                            {
                                string currUrl = "Browser ; " + BrowserActionEnum.Nav + ActiveBrowser.LocationURL;

                                StepRecorder.RecordStep(UserBar.instance.SteptreeView, currUrl);
                            }
                        }
                    }
                }
            }

        }

        void Recorder_OnNavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel)
        {
            throw new Exception("Not Implement");
        }


        #endregion

        #region DOM Event
        private void _BindDomEvent(DispHTMLDocument doc)
        {
            DOMEventHandler onclickHandler = new DOMEventHandler(doc, null);
            onclickHandler.NewEventHandlers += new DOMEvent(_click);
            doc.onmouseup = onclickHandler;

            DOMEventHandler onmouseoverHandler = new DOMEventHandler(doc, null);
            onmouseoverHandler.NewEventHandlers += new DOMEvent(_mouseover);
            doc.onmouseover = onmouseoverHandler;

            DOMEventHandler onmouseoutHandler = new DOMEventHandler(doc, null);
            onmouseoutHandler.NewEventHandlers += new DOMEvent(_mouseout);
            doc.onmouseout = onmouseoutHandler;

            DOMEventHandler onkeyupHandler = new DOMEventHandler(doc, null);
            onkeyupHandler.NewEventHandlers += new DOMEvent(_keyup);
            doc.onkeyup = onkeyupHandler;
        }
        string oldborder = string.Empty;

        public void _mouseout(IHTMLEventObj obj)
        {
            //if (oldborder != string.Empty)
            //{
            obj.srcElement.style.border = oldborder;
            oldborder = string.Empty;
            //}

        }
        public void _mouseover(IHTMLEventObj obj)
        {
            oldborder = obj.srcElement.style.border;
            obj.srcElement.style.border = "2px solid red";
        }


        public void _click(IHTMLEventObj obj)
        {
            try
            {

                string identify = GetIdentify(obj.srcElement);

                identify += "; CLICK";

                StepRecorder.RecordStep(UserBar.instance.SteptreeView, identify);

                obj.srcElement.click();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void _keyup(IHTMLEventObj obj)
        {
            //Keys currentKey = (Keys)Enum.Parse(typeof(Keys), obj.keyCode.ToString());

            IHTMLElement current = ((DispHTMLDocument)obj.srcElement.document).activeElement;

            //IHTMLDOMNode currentNode = ((IHTMLDOMNode)current);

            string identify = GetIdentify(obj.srcElement);

            if (CloneInputElement != null)
            {
                if (current == CloneInputElement) //same input
                {
                    StepRecorder.RecordStep(UserBar.instance.SteptreeView, identify + " ; APPEND ;" + (char)obj.keyCode);
                }
                else
                {
                    CloneInputElement = current;
                    StepRecorder.RecordStep(UserBar.instance.SteptreeView, identify + " ; INPUT ;" + current.getAttribute(Finder.valueAttribute, 0));
                }
            }
            else
            {
                CloneInputElement = current;
                StepRecorder.RecordStep(UserBar.instance.SteptreeView, identify + " ; INPUT ;" + current.getAttribute(Finder.valueAttribute, 0));
            }

            //switch (currentKey)
            //{
            //    case Keys.Enter:
            //        break;
            //    default:
            //        break;

            //}
            //string identify = GetIdentify(obj.srcElement);

            //identify += "action : keyinput ; value :"+obj.keyCode ;

        }

        public string GetIdentify(IHTMLElement obj)
        {
            string identify = Finder.GetID(obj);
            if (identify == null || identify.Equals(string.Empty))
            {
                identify = Finder.GetAttributes(obj);
            }

            return identify;
        }
        #endregion


        #region comments obsolete code
        //void keyHook_KeyDown(object sender, KeyEventArgs e)
        //{ }
        //void keyHook_KeyUp(object sender, KeyEventArgs e)
        //{ }
        //void keyHook_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    MessageBox.Show(e.KeyChar.ToString());
        //}

        //void mousehook_Click(object sender, MouseEventArgs e)
        //{
        //    if (UserBar.instance != null)
        //    {
        //        //Get Web Browser
        //        if (Recorder.HostInstance != null && Recorder.HostInstance.BrowserRef != null)
        //        {
        //            IWebBrowser2 ActiveBrowser = Recorder.HostInstance.BrowserRef as SHDocVw.IWebBrowser2;

        //            if (ActiveBrowser != null)
        //            {
        //                //Get the current document
        //                HTMLDocument htmlDoc = (HTMLDocument)ActiveBrowser.Document;

        //                if (htmlDoc != null)
        //                {
        //                    DispHTMLDocument doc = (DispHTMLDocument)htmlDoc;

        //                    //Record the basic Url
        //                    string address = ActiveBrowser.LocationURL;
        //                    //Get 
        //                    IHTMLElement activeElement = doc.activeElement;

        //                    if (activeElement.tagName != null)
        //                    {
        //                        //do something

        //                        //then do the right thing
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //void mouseHook_MouseMove(object sender, MouseEventArgs e)
        //{ }

        //void mouseHook_MouseDown(object sender, MouseEventArgs e)
        //{ }

        //void mouseHook_MouseUp(object sender, MouseEventArgs e)
        //{

        //}

        //void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        //{ }


        //[DllImport("user32.dll")]
        //static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll")]
        //static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        //private string GetActiveWindowTitle()
        //{
        //    const int nChars = 256;
        //    IntPtr handle = IntPtr.Zero;
        //    StringBuilder Buff = new StringBuilder(nChars);
        //    handle = GetForegroundWindow();

        //    if (GetWindowText(handle, Buff, nChars) > 0)
        //    {
        //        return Buff.ToString();
        //    }
        //    return null;
        //}
        //private const int WH_KEYBOARD_LL = 13;

        //protected const int WH_MOUSE_LL = 14;
        //protected const int WH_MOUSE = 7;
        //protected const int WH_KEYBOARD = 2;
        //protected const int WM_MOUSEMOVE = 0x200;
        //protected const int WM_LBUTTONDOWN = 0x201;
        //protected const int WM_RBUTTONDOWN = 0x204;
        //protected const int WM_MBUTTONDOWN = 0x207;
        //protected const int WM_LBUTTONUP = 0x202;
        //protected const int WM_RBUTTONUP = 0x205;
        //protected const int WM_MBUTTONUP = 0x208;
        //protected const int WM_LBUTTONDBLCLK = 0x203;
        //protected const int WM_RBUTTONDBLCLK = 0x206;
        //protected const int WM_MBUTTONDBLCLK = 0x209;
        //protected const int WM_MOUSEWHEEL = 0x020A;

        //protected const int WM_KEYDOWN = 0x100;
        //protected const int WM_KEYUP = 0x101;
        //protected const int WM_SYSKEYDOWN = 0x104;
        //protected const int WM_SYSKEYUP = 0x105;

        //protected const byte VK_SHIFT = 0x10;
        //protected const byte VK_CAPITAL = 0x14;
        //protected const byte VK_NUMLOCK = 0x90;

        //protected const byte VK_LSHIFT = 0xA0;
        //protected const byte VK_RSHIFT = 0xA1;
        //protected const byte VK_LCONTROL = 0xA2;
        //protected const byte VK_RCONTROL = 0x3;
        //protected const byte VK_LALT = 0xA4;
        //protected const byte VK_RALT = 0xA5;

        //protected const byte LLKHF_ALTDOWN = 0x20;

        //private static LowLevelMouseProc _proc = HookCallback;
        //private static IntPtr _hookID = IntPtr.Zero;


        //private static IntPtr SetHook(LowLevelMouseProc proc)
        //{
        //    using (Process curProcess = Process.GetCurrentProcess())
        //    using (ProcessModule curModule = curProcess.MainModule)
        //    {
        //        IntPtr handle = GetModuleHandle(curModule.ModuleName);
        //        IntPtr ret = SetWindowsHookEx(WH_MOUSE_LL, proc,
        //            handle, 0);
        //        //MessageBox.Show(Marshal.GetLastWin32Error().ToString());
        //        return ret;
        //    }
        //}

        //private delegate IntPtr LowLevelMouseProc(
        //    int nCode, IntPtr wParam, IntPtr lParam);

        //private static IntPtr HookCallback(
        //    int nCode, IntPtr wParam, IntPtr lParam)
        //{
        //    if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONUP)
        //    {
        //        int vkCode = Marshal.ReadInt32(lParam);
        //        //Console.WriteLine((Keys)vkCode);
        //        if (UserBar.instance != null)
        //        {
        //            //Get Web Browser
        //            if (Recorder.HostInstance != null && Recorder.HostInstance.BrowserRef != null)
        //            {
        //                IWebBrowser2 ActiveBrowser = Recorder.HostInstance.BrowserRef as SHDocVw.IWebBrowser2;

        //                if (ActiveBrowser != null)
        //                {
        //                    //Get the current document
        //                    HTMLDocument htmlDoc = (HTMLDocument)ActiveBrowser.Document;

        //                    if (htmlDoc != null)
        //                    {
        //                        DispHTMLDocument doc = (DispHTMLDocument)htmlDoc;

        //                        //Record the basic Url
        //                        string address = ActiveBrowser.LocationURL;
        //                        //Get 
        //                        IHTMLElement activeElement = doc.activeElement;

        //                        if (activeElement != null)
        //                        {
        //                            MessageBox.Show(activeElement.tagName);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return CallNextHookEx(_hookID, nCode, wParam, lParam);
        //}

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr SetWindowsHookEx(int idHook,
        //    LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        //    IntPtr wParam, IntPtr lParam);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetModuleHandle(string lpModuleName);


        //void HookMouseUp(object sender, Microsoft.Win32.MouseHookEventArgs e)
        //{
        //    if (UserBar.instance != null)
        //    {
        //        //Get Web Browser
        //        if (Recorder.HostInstance != null && Recorder.HostInstance.BrowserRef != null)
        //        {
        //            IWebBrowser2 ActiveBrowser = Recorder.HostInstance.BrowserRef as SHDocVw.IWebBrowser2;

        //            if (ActiveBrowser != null)
        //            {
        //                //Get the current document
        //                HTMLDocument htmlDoc = (HTMLDocument)ActiveBrowser.Document;

        //                if (htmlDoc != null)
        //                {
        //                    DispHTMLDocument doc = (DispHTMLDocument)htmlDoc;

        //                    //Record the basic Url
        //                    string address = ActiveBrowser.LocationURL;
        //                    //Get 
        //                    IHTMLElement activeElement = doc.activeElement;

        //                    if (activeElement != null)
        //                    {
        //                        MessageBox.Show(activeElement.tagName);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        void Recorder_OnDocumentComplete(object pDisp, ref object url)
        {
            if (UserBar.instance != null)
            {
                //Get Web Browser
                if (Recorder.HostInstance != null && Recorder.HostInstance.BrowserRef != null)
                {
                    IWebBrowser2 ActiveBrowser = Recorder.HostInstance.BrowserRef as SHDocVw.IWebBrowser2;

                    if (ActiveBrowser != null)
                    {

                        //Get the current document
                        HTMLDocument htmlDoc = (HTMLDocument)ActiveBrowser.Document;

                        DispHTMLDocument doc = (DispHTMLDocument)htmlDoc;

                        //Record the basic Url
                        string address = ActiveBrowser.LocationURL;

                        if (htmlDoc != null)
                        {
                            //Bind events                       
                            DOMEventHandler onclickhandler = new DOMEventHandler(doc, null);
                            onclickhandler.NewEventHandlers += new DOMEvent(DOMEventMethods._click);
                            doc.onclick = onclickhandler;



                            DOMEventHandler onclickhandler1 = new DOMEventHandler(doc, doc.onclick);
                            onclickhandler1.NewEventHandlers += new DOMEvent(DOMEventMethods._link_click);
                            doc.onmouseup = onclickhandler1;

                            //Bind Href Click
                            //foreach (mshtml.HTMLAnchorElement link in htmlDoc.anchors)
                            //{

                            //    ((HTMLAnchorEvents_Event)link).onmouseup += new HTMLAnchorEvents_onmouseupEventHandler(() =>
                            //    {
                            //        DOMEventMethods._link_click(link);
                            //        //return true;
                            //    });
                            //}

                            //Bind IFrame events
                            //IHTMLElementCollection elems = htmlDoc.getElementsByTagName("iframe");
                            //foreach (IHTMLElement elem in elems)
                            //{
                            //    HTMLFrameElement frm = (HTMLFrameElement)elem;
                            //    DispHTMLDocument frm_doc = (DispHTMLDocument)((SHDocVw.IWebBrowser2)frm).Document;
                            //    DOMEventHandler onclickfrmhandler = new DOMEventHandler(frm_doc);
                            //    onclickfrmhandler.Handler += new DOMEvent(DOMEventMethods._click);
                            //    (frm as DispHTMLDocument).onclick = onclickfrmhandler;
                            //}

                        }
                    }
                }
            }
        }
        #endregion

        #region Register your new browser UI elements here

        internal static List<SpicIE.Controls.IControlBase> RunOnceCOMRegistration()
        {
            Host.TraceSink.TraceEvent(TraceEventType.Information, 0, "RunOnceRegisterCOMControls");

            List<SpicIE.Controls.IControlBase> controls = new List<SpicIE.Controls.IControlBase>();

            controls.Add(new UserBar());

            return controls;
        }

        #endregion

        #region SpicIE - Required methods, do not change

        [ComRegisterFunction]
        internal static void RegisterControls(Type typeToRegister)
        {
            SpicIE.Host.RegisterHost(typeToRegister, PLUGIN_GUID, PLUGIN_PROGID);

            SpicIE.Host.RegisterControls(RunOnceCOMRegistration());
        }

        [ComUnregisterFunction]
        internal static void UnregisterControls(Type typeToRegister)
        {
            SpicIE.Host.UnregisterHost(typeToRegister, PLUGIN_GUID);

            SpicIE.Host.UnregisterControls(RunOnceCOMRegistration());
        }

        #endregion

    }
}


