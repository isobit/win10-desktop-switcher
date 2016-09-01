using System;
using System.Runtime.InteropServices;

namespace DesktopSwitcher
{

    internal static class Guids
    {
        public static Guid ImmersiveShell =
            new Guid(0xC2F03A33, 0x21F5, 0x47FA, 0xB4, 0xBB, 0x15, 0x63, 0x62, 0xA2, 0xF2, 0x39);
        public static Guid VirtualDesktopManagerInternal =
            new Guid(0xC5E0CDCA, 0x7B6E, 0x41B2, 0x9F, 0xC4, 0xD9, 0x39, 0x75, 0xCC, 0x46, 0x7B);
        public static Guid IVirtualDesktopManagerInternal =
            new Guid("F31574D6-B682-4CDC-BD56-1827860ABEC6");
        public static Guid IVirtualDesktop =
            new Guid("FF72FFDD-BE7E-43FC-9C03-AD81681E88E4");
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("92CA9DCD-5622-4bba-A805-5E9F541BD8C9")]
    internal interface IObjectArray
    {
        void GetCount(out int count);
        void GetAt(int index, ref Guid iid, [MarshalAs(UnmanagedType.Interface)]out object obj);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
    internal interface IServiceProvider10
    {
        [return: MarshalAs(UnmanagedType.IUnknown)]
        object QueryService(ref Guid service, ref Guid riid);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("FF72FFDD-BE7E-43FC-9C03-AD81681E88E4")]
    internal interface IVirtualDesktop
    {
        void notimpl1(); // void IsViewVisible(IApplicationView view, out int visible);
        Guid GetId();
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("F31574D6-B682-4CDC-BD56-1827860ABEC6")]
    internal interface IVirtualDesktopManagerInternal
    {
        int GetCount();
        void notimpl1();  // void MoveViewToDesktop(IApplicationView view, IVirtualDesktop desktop);
        void notimpl2();  // void CanViewMoveDesktops(IApplicationView view, out int itcan);
        IVirtualDesktop GetCurrentDesktop();
        void GetDesktops(out IObjectArray desktops);
        [PreserveSig]
        int GetAdjacentDesktop(IVirtualDesktop from, int direction, out IVirtualDesktop desktop);
        void SwitchDesktop(IVirtualDesktop desktop);
        IVirtualDesktop CreateDesktop();
        void RemoveDesktop(IVirtualDesktop desktop, IVirtualDesktop fallback);
        IVirtualDesktop FindDesktop(ref Guid desktopid);
    }

    class Program
    {
        private static IServiceProvider10 _shell;
        private static IVirtualDesktopManagerInternal _manager;

        private static void GoToDesktop(int n)
        {
            IObjectArray objectArray;
            _manager.GetDesktops(out objectArray);
            if (objectArray == null) return;

            int count;
            objectArray.GetCount(out count);
            if (count == 0) return;

            object desktop;
            objectArray.GetAt(n, ref Guids.IVirtualDesktop, out desktop);
            if (desktop == null) return;

            _manager.SwitchDesktop((IVirtualDesktop) desktop);
        }

        public static void Main(string[] args)
        {
            _shell = (IServiceProvider10)Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.ImmersiveShell));
            _manager = (IVirtualDesktopManagerInternal)_shell.QueryService(Guids.VirtualDesktopManagerInternal, Guids.IVirtualDesktopManagerInternal);

            GoToDesktop(int.Parse(args[0]));
        }
    }
}
