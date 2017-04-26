using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFCommon.Common.Misc
{
    /// <SUMMARY>
    /// Check running processes for an already-running instance. Implements a simple and
    /// always effective algorithm to find currently running processes with a main window
    /// matching a given substring and focus it.
    /// Combines code written by Lion Shi (MS) and Sam Allen.
    /// 
    /// 사용법:Program.cs파일의 다음 부분에 아래와 같이 소스를 수정한다.
    ///        "Program Window Text"은 폼의 타이틀 이름이다.
    ///       
    /// [STAThread]
    /// static void Main()
    /// {
    ///    if (ProcessChecker.IsOnlyProcess("Program Window Text"))
    ///    {
    ///        Application.EnableVisualStyles();
    ///        Application.SetCompatibleTextRenderingDefault(false);
    ///        Application.Run(new TextWindow());
    ///    }
    /// }
    /// 
    /// 출처:http://dotnetperls.com/single-instance-windows-form
    /// 출처를 기반으로 수정을 가함.
    /// </SUMMARY>
    static class ProcessChecker
    {
        static string _requiredString;

        /// <SUMMARY>
        /// Contains signatures for C++ DLLs using interop.
        /// </SUMMARY>
        internal static class NativeMethods
        {
            /// <SUMMARY>
            /// 현재 실행중인 윈도우의 상태를 보여준다.
            /// </SUMMARY>
            /// <PARAM name="hWnd"></PARAM>
            /// <PARAM name="nCmdShow"></PARAM>
            /// <RETURNS></RETURNS>
            [DllImport("user32.dll")]
            public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

            /// <SUMMARY>
            /// 선택한 윈도우를 뒤에 숨어있었으면 앞으로, 최소화상태였으면 원래상태로 되돌려놓으며 활성화시킨다.
            /// </SUMMARY>
            /// <PARAM name="hWnd"></PARAM>
            /// <RETURNS></RETURNS>
            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            /// <SUMMARY>
            /// EnumWindows 함수는 모든 최상위 윈도우를 검색해서 그 핸들을 콜백함수로 전달하되
            /// 모든 윈도우를 다 찾거나 콜백함수가 FALSE를 리턴할 때까지 검색을 계속한다.
            /// 콜백함수는 검색된 윈도우의 핸들을 전달받으므로 모든 윈도우에 대해 모든 작업을 다 할 수 있다.
            /// EnumWindows 함수는 차일드 윈도우는 검색에서 제외된다.
            /// 단 시스템이 생성한 일부 최상위 윈도우는 WS_CHILD 스타일을 가지고 있더라도 예외적으로 검색에 포함된다.
            /// </SUMMARY>
            /// <PARAM name="lpEnumFunc">EnumWindows의 실행 결과를 받아줄 콜백함수이다.
            /// EnumWindows는 이 함수 결과가 false가 될 때까지 계속 윈도우를 검색하게 된다.</PARAM>
            /// <PARAM name="lParam"></PARAM>
            /// <RETURNS></RETURNS>
            [DllImport("user32.dll")]
            public static extern bool EnumWindows(EnumWindowsProcDel lpEnumFunc, IntPtr lParam);

            /// <SUMMARY>
            /// HWND 값을 이용하여 프로세스 ID를 알려주는 함수이다.
            /// </SUMMARY>
            /// <PARAM name="hWnd"></PARAM>
            /// <PARAM name="lpdwProcessId"></PARAM>
            /// <RETURNS></RETURNS>
            [DllImport("user32.dll")]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd, ref Int32 lpdwProcessId);

            /// <SUMMARY>
            /// 윈도우의 캡션을 가져온다.
            /// </SUMMARY>
            /// <PARAM name="hWnd"></PARAM>
            /// <PARAM name="lpString"></PARAM>
            /// <PARAM name="nMaxCount"></PARAM>
            /// <RETURNS></RETURNS>
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, Int32 nMaxCount);


            /// <summary>
            /// 윈도우의 상태를 normal로 하게 하는 상수
            /// </summary>
            public const int SW_SHOWNORMAL = 1;

            /// <summary>
            /// 윈도우의 상태를 max로 하게 하는 상수
            /// </summary>
            public const int SW_SHOWMAXIMIZED = 3;
            /// <summary>
            /// 저장된 상태의 크기로 돌려놓는다.
            /// </summary>
            public const int SW_RESTORE = 9;

            /// <summary>
            /// 스타일을 얻기 위한 값
            /// </summary>
            public const int GWL_STYLE = -16;

            /// <summary>
            /// 윈도우 스타일중 최소화값
            /// </summary>
            public const long WS_MINIMIZE = 0x20000000L;


            /// <summary>
            /// 윈도우의 스타일을 가져온다.
            /// </summary>
            /// <param name="hWnd"></param>
            /// <param name="nIndex"></param>
            /// <returns></returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern int GetWindowLong(IntPtr hWnd, int nIndex);


        }

        /// <SUMMARY>
        /// EnumWindows의 실행 결과를 받아줄 콜백함수이다.
        /// EnumWindows는 이 함수 결과가 false가 될 때까지 계속 윈도우를 검색하게 된다.
        /// </SUMMARY>
        /// <PARAM name="hWnd"></PARAM>
        /// <PARAM name="lParam"></PARAM>
        /// <RETURNS></RETURNS>
        public delegate bool EnumWindowsProcDel(IntPtr hWnd, Int32 lParam);

        /// <SUMMARY>
        /// Perform finding and showing of running window.
        /// 모든 실행중인 윈도우를 검색하며 찾고자 하는 캡션의 윈도우를 발견하면 활성화시킨다.
        /// </SUMMARY>
        /// <RETURNS>Bool, which is important and must be kept to match up
        /// with system call.</RETURNS>
        static private bool EnumWindowsProc(IntPtr hWnd, Int32 lParam)
        {
            int processId = 0;
            NativeMethods.GetWindowThreadProcessId(hWnd, ref processId);

            StringBuilder caption = new StringBuilder(1024);
            NativeMethods.GetWindowText(hWnd, caption, 1024); //방금 검색한 윈도우의 캡션을 가져온다.

            //찾을 윈도우명과 가져온 캡션이 일치한다면,
            if (processId == lParam && (caption.ToString().IndexOf(_requiredString, StringComparison.OrdinalIgnoreCase) != -1))
            {



                // 윈도우 스타일을 얻고
                int style = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);

                // 윈도우 스타일이 최소화 값이라면 윈도우를 NORMAL 상태로 만든다.
                if ((style & NativeMethods.WS_MINIMIZE) == NativeMethods.WS_MINIMIZE)
                {
                    //윈도우를 normal 상태로 바꾸고
                    NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_RESTORE);
                }
                // 제일 앞으로 가져온다.
                NativeMethods.SetForegroundWindow(hWnd);
            }
            return true; //왜 계속 true만 반환해야 할까???
        }

        /// <SUMMARY>
        /// 지금 실행하려는 프로그램이 이미 실행중인지 아닌지 찾아보고 결과를 알려준다.
        /// </SUMMARY>
        /// <PARAM name="forceTitle">찾으려는 윈도우의 캡션, 즉 프로그램 타이틀</PARAM>
        /// <RETURNS>해당 캡션의 윈도우가 이미 실행중이라면 False,
        /// 처음 실행하는 것이라면 True를 반환한다.</RETURNS>
        static public bool IsOnlyProcess(string forceTitle)
        {
            _requiredString = forceTitle;
            //먼저 실행파일의 이름으로 이름이 같은 프로세스를 검색해본다.
            foreach (Process proc in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
            {
                if (proc.Id != Process.GetCurrentProcess().Id)
                {
                    NativeMethods.EnumWindows(new EnumWindowsProcDel(EnumWindowsProc), new IntPtr(proc.Id));
                    return false;
                }
            }
            return true;
        }

    }
}
