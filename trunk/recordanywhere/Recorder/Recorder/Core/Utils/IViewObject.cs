using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;


namespace Recorder.Core.Utils
{

    //[ComImport, Guid("0000010D-0000-0000-C000-000000000046"), InterfaceType((short)1), ComConversionLoss]
    //public interface IViewObject
    //{
    //    void Draw([MarshalAs(UnmanagedType.U4)] UInt32 dwDrawAspect,
    //              int lindex,
    //              IntPtr pvAspect,
    //              [In] IntPtr ptd,
    //              IntPtr hdcTargetDev,
    //              IntPtr hdcDraw,
    //              [MarshalAs(UnmanagedType.Struct)] ref _RECTL lprcBounds,
    //              [In] IntPtr lprcWBounds,
    //              IntPtr pfnContinue,
    //              [MarshalAs(UnmanagedType.U4)] UInt32 dwContinue);

    //    void RemoteGetColorSet([In] uint dwDrawAspect, [In] int lindex, [In] uint pvAspect, [In] ref tagDVTARGETDEVICE ptd, [In] uint hicTargetDev, [Out] IntPtr ppColorSet);
    //    void RemoteFreeze([In] uint dwDrawAspect, [In] int lindex, [In] uint pvAspect, out uint pdwFreeze);
    //    void Unfreeze([In] uint dwFreeze);
    //    void SetAdvise([In] uint aspects, [In] uint advf, [In, MarshalAs(UnmanagedType.Interface)] IECaptComImports.IAdviseSink pAdvSink);
    //    void RemoteGetAdvise(out uint pAspects, out uint pAdvf, [MarshalAs(UnmanagedType.Interface)] out IECaptComImports.IAdviseSink ppAdvSink);
    //}

    [ComVisible(true), ComImport()]
    [Guid("04C9C413-C6BF-4d77-9603-6511DBEEFE35")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IViewObject
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Draw([MarshalAs(UnmanagedType.U4)] UInt32 dwDrawAspect,
        int lindex,
        IntPtr pvAspect,
        [In] IntPtr ptd,
        IntPtr hdcTargetDev,
        IntPtr hdcDraw,
        [MarshalAs(UnmanagedType.Struct)] ref Rectangle lprcBounds,
        [MarshalAs(UnmanagedType.Struct)] ref Rectangle lprcWBounds,
        IntPtr pfnContinue,
        [MarshalAs(UnmanagedType.U4)] UInt32 dwContinue);
        [PreserveSig]
        int GetColorSet([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect,
           int lindex, IntPtr pvAspect, [In] IntPtr ptd,
            IntPtr hicTargetDev, [Out] IntPtr ppColorSet);
        [PreserveSig]
        int Freeze([In, MarshalAs(UnmanagedType.U4)] int dwDrawAspect,
                        int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);
        [PreserveSig]
        int Unfreeze([In, MarshalAs(UnmanagedType.U4)] int dwFreeze);
        void SetAdvise([In, MarshalAs(UnmanagedType.U4)] int aspects,
          [In, MarshalAs(UnmanagedType.U4)] int advf,
          [In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink);
        void GetAdvise([In, Out, MarshalAs(UnmanagedType.LPArray)] int[] paspects,
          [In, Out, MarshalAs(UnmanagedType.LPArray)] int[] advf,
          [In, Out, MarshalAs(UnmanagedType.LPArray)] IAdviseSink[] pAdvSink);

    }
}
