﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using M64MM.Additions;
using M64MM.Utils;
using TinyHugeTweaks.Properties;

namespace TinyHugeTweaks
{
    public class Module : IModule
    {
        static mainForm frmMain = new mainForm();

        public string SafeName => "Tiny-Huge Tweaks";

        public string Description => "Various tweaks that may come in handy for Super Mario 64 machinimas.";

        public Image AddonIcon => Resources.tbk_icon_m;

        public void Close(EventArgs e)
        {

        }

        public List<ToolCommand> GetCommands()
        {
            List<ToolCommand> tcl = new List<ToolCommand>();
            ToolCommand tcOpen = new ToolCommand("Open Tiny-Huge Tweaks");
            tcOpen.Summoned += (a, b) => openForm();
            tcl.Add(tcOpen);
            return tcl;
        }

        public void openForm()
        {
            if (frmMain == null || frmMain.IsDisposed)
            {
                frmMain = new mainForm();
            }
            frmMain.Show();
        }

        public void Initialize()
        {
        }

        public void OnBaseAddressFound()
        {
        }

        public void OnBaseAddressZero()
        {
        }

        public void Reset()
        {
        }

        public void Update()
        {
        }

        public void OnCoreEntAddressChange(uint addr)
        {
            // :P
        }
    }
}
