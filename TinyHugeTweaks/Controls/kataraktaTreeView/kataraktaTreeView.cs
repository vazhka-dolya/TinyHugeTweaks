using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TinyHugeTweaks.Controls.kataraktaTreeView
{
    public class kataraktaTreeView : TreeView
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetWindowTheme(this.Handle, "explorer", null);
        }
    }
}
