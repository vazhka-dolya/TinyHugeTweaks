using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using M64MM.Utils;

namespace TinyHugeTweaks
{
    public partial class mainForm : Form
    {

        static frmAbout about = new frmAbout();

        /*
        (0) Solid RGBA texture without fog (FC 12 7E 24 FF FF F9 FC;
                                            24 7E 12 FC FC F9 FF FF;
                                            FF FF F9 FC FC 12 7E 24)

        (1) Alpha RGBA texture without fog (FC 12 18 24 FF 33 FF FF;
                                            24 18 12 FC FF FF 33 FF;
                                            FF 33 FF FF FC 12 18 24)

        (2) Solid RGBA texture with fog (FC 12 7F FF FF FF F8 38;
                                         FF 7F 12 FC 38 F8 FF FF;
                                         FF FF F8 38 FC 12 7F FF)

        (3) Alpha RGBA texture with fog (FC FF FF FF FF FC F2 38;
                                         FF FF FF FC 38 F2 FC FF;
                                         FF FC F2 38 FC FF FF FF)
        */

        public ReadOnlyCollection<ulong> ColorCombinerCommands = new ReadOnlyCollection<ulong>(new ulong[] { 0xFFFFF9FCFC127E24, 0xFF33FFFFFC121824, 0xFFFFF838FC127FFF, 0xFFFCF238FCFFFFFF });

        /*
        8008C520 00B8 — left hand closed

        8008D088 00B8 — right hand closed

        8008B8D8 00B8 — core

        8008BE08 00B8 — left arm 1

        8008CA18 00B8 — right arm 1

        8008D3F0 00B8 — left leg 1

        8008DBE8 00B8 — right leg 1

        8008E118 00B8 — right foot

        8008D8D0 00B8 — left foot

        80093278 00B8 — idk

        80093470 00B8 — idk

        80093B70 00B8 — idk

        80094790 00B8 — idk

        80094CF8 00B8 — idk

        8008D540 00B8 — left leg 2

        8008DE00 00B8 — right leg 2

        800939F0 00B8 — idk

        80094110 00B8 — idk

        80093578 00B8 — idk

        80093C78 00B8 — idk

        8008BF20 00B8 — left arm 2

        8008CB30 00B8 — right arm 2

        800942F0 00B8 — idk

        80094A38 00B8 — idk

        80094930 00B8 — idk

        80094470 00B8 — idk

        8008EB50 00B8 — remove torso buttons

        8008EF80 00B8 — remove torso shirt

        8008EBB0 00B8 — remove torso overalls

        80095290 00B8 — idk

        800954D8 00B8 — idk

        800952E0 00B8 — idk
        */

        public byte[][] MPWriteStates = { new byte[] { 0xB8 } /*Disable*/, new byte[] { 0x06 } /*Enable 1*/, new byte[] { 0xBF } /*Enable 2*/ };
        public ReadOnlyCollection<uint> MPHeadAddresses = new ReadOnlyCollection<uint>(new uint[] { 0x90610, 0x90700, 0x907F0, 0x908E0, 0x909D0, 0x90AC0, 0x90BB0, 0x90CA0 });
        public ReadOnlyCollection<uint> MPLeftHandAddresses = new ReadOnlyCollection<uint>(new uint[] { 0x8C520 });
        public ReadOnlyCollection<uint> MPRightHandAddresses = new ReadOnlyCollection<uint>(new uint[] { 0x8D088 });
        public ReadOnlyCollection<uint> MPCoreAddresses = new ReadOnlyCollection<uint>(new uint[] { 0x8B8D8 });
        public ReadOnlyCollection<uint> MPArmL1Addresses = new ReadOnlyCollection<uint>(new uint[] { 0x8BE08 });
        public ReadOnlyCollection<uint> MPArmR1Addresses = new ReadOnlyCollection<uint>(new uint[] { 0x8CA18 });
        public ReadOnlyCollection<uint> MPLegL1Addresses = new ReadOnlyCollection<uint>(new uint[] { 0x8D3F0 });
        public ReadOnlyCollection<uint> MPLegR1Addresses = new ReadOnlyCollection<uint>(new uint[] { 0x8DBE8 });
        public ReadOnlyCollection<uint> MPRightFootAddresses = new ReadOnlyCollection<uint>(new uint[] { 0x8E118 });
        public ReadOnlyCollection<uint> MPLeftFootAddresses = new ReadOnlyCollection<uint>(new uint[] { 0x8D8D0 });
        public ReadOnlyCollection<uint> MPLegL2Addresses = new ReadOnlyCollection<uint>(new uint[] { 0x8D540 });
        public ReadOnlyCollection<uint> MPLegR2Addresses = new ReadOnlyCollection<uint>(new uint[] { 0x8DE00 });
        public ReadOnlyCollection<uint> MPArmL2Addresses = new ReadOnlyCollection<uint>(new uint[] { 0x8BF20 });
        public ReadOnlyCollection<uint> MPArmR2Addresses = new ReadOnlyCollection<uint>(new uint[] { 0x8CB30 });
        public ReadOnlyCollection<uint> MPTorsoAddresses1 = new ReadOnlyCollection<uint>(new uint[] { 0x8EF80 });
        public ReadOnlyCollection<uint> MPTorsoAddresses2 = new ReadOnlyCollection<uint>(new uint[] { 0x8EB50, 0x8EBB0 });

        /*
        0F0F0F0F 0F0F0F0F 0F0F0F0F 0E0B0400
        0F0F0F0F 0F0F0F0F 0F0F0F0F 0E0B0400
        0F0F0F0F 0F0F0F0F 0F0F0F0F 0E0A0300
        0F0F0F0F 0F0F0F0F 0F0F0F0F 0D090200
        0F0F0F0F 0F0F0F0F 0F0F0F0E 0B060000
        0F0F0F0F 0F0F0F0F 0F0F0F0D 09040000
        0F0F0F0F 0F0F0F0F 0F0F0E0B 06020000
        0F0F0F0F 0F0F0F0F 0F0E0D09 04010000
        0F0F0F0F 0F0F0F0F 0F0D0A05 02000000
        0F0F0F0F 0F0F0F0E 0D0A0502 00000000
        0F0F0F0F 0F0F0E0D 0A050201 00000000
        0F0F0F0F 0E0D0B09 05020100 00000000
        0E0E0E0D 0B090604 02000000 00000000
        0B0B0A09 06040201 00000000 00000000
        04040403 02010000 00000000 00000000
        00000000 00000000 00000000 00000000
        */
        static byte[] RoundShadowTexture = new byte[]
        {
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0B, 0x04, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0B, 0x04, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0A, 0x03, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0D, 0x09, 0x02, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0B, 0x06, 0x00, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0D, 0x09, 0x04, 0x00, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0B, 0x06, 0x02, 0x00, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0D, 0x09, 0x04, 0x01, 0x00, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0D, 0x0A, 0x05, 0x02, 0x00, 0x00, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0D, 0x0A, 0x05, 0x02, 0x00, 0x00, 0x00, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0D, 0x0A, 0x05, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00,
            0x0F, 0x0F, 0x0F, 0x0F, 0x0E, 0x0D, 0x0B, 0x09, 0x05, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x0E, 0x0E, 0x0E, 0x0D, 0x0B, 0x09, 0x06, 0x04, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x0B, 0x0B, 0x0A, 0x09, 0x06, 0x04, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x04, 0x04, 0x04, 0x03, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        public mainForm()
        {
            this.KeyPreview = true;
            this.KeyDown += mainForm_KeyDown;
            this.KeyUp += mainForm_KeyUp;

            InitializeComponent();

            this.Text = $"Tiny-Huge Tweaks {ProductVersion}";
            this.ClientSize = new System.Drawing.Size(355, 374); //355; 374

            StarFill();

            comboBlackInput.SelectedIndex = 2;
            comboBlackOutput.SelectedIndex = 1;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about.ShowDialog();
        }

        public void FixSmokeTexture()
        {
            Core.WriteBytes(Core.BaseAddress + 0xA0C98 + 2, new byte[] { 0x70 });
            Core.WriteBytes(Core.BaseAddress + 0xA0CA0 + 2, new byte[] { 0x70 });
            Core.WriteBytes(Core.BaseAddress + 0xA0CC0 + 2, new byte[] { 0x70 });
        }

        public void BlackBarsEdit()
        {/*
            Core.WriteBytes(Core.BaseAddress + 0x2473A0 + 7, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x2473B0 + 3, new byte[] { 0x00  });
            Core.WriteBytes(Core.BaseAddress + 0x247490 + 3, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x2474A0 + 3, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x2475A0 + 3, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x2475A8 + 7, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x247D18 + 4, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B468 + 7, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B478 + 7, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27B4F8 + 3, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B508 + 3, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27B580 + 7, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B590 + 7, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27C998 + 7, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x27C9A8 + 3, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7B0 + 7, new byte[] { 0x50 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7B8 + 3, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7A8 + 4, new byte[] { 0x00, 0x00, 0x00, 0x00 });*/

            Core.WriteBytes(Core.BaseAddress + 0x2473A0 + 4, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x2473B0 + 0, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x247490 + 0, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x2474A0 + 0, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x2475A0 + 0, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x2475A8 + 4, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x247D18 + 4, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B468 + 4, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B478 + 4, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27B4F8 + 0, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B508 + 0, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27B580 + 4, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B590 + 4, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27C998 + 4, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x27C9A8 + 0, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7B0 + 4, new byte[] { 0x50 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7B8 + 0, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7A8 + 4, new byte[] { 0x00, 0x00, 0x00, 0x00 });
        }

        public void MPEdit(ReadOnlyCollection<uint> AddressList, bool Show, bool OtherShow)
        {
            byte[] WriteState;
            if (Show == false)
            {
                WriteState = MPWriteStates[0];
            }
            else if (OtherShow is false)
            {
                WriteState = MPWriteStates[1];
            }
            else
            {
                WriteState = MPWriteStates[2];
            }
            foreach (uint Address in AddressList)
            {
                Core.WriteBytes(Core.BaseAddress + Address + 3, WriteState);
            }
        }

        public void StarFill()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/Addons/TinyHugeTweaks/starAddresses.config";
            string[] lines = File.ReadAllLines(filePath);
            List<List<string>> entries = new List<List<string>>();

            for (int i = 0; i < lines.Length; i += 3)
            {
                if (i + 2 < lines.Length)
                {
                    List<string> entry = new List<string>
                    {
                        lines[i],
                        lines[i + 1],
                        lines[i + 2]
                    };

                    entries.Add(entry);
                }
            }

            comboBoxStar.Items.Clear();
            foreach (var entry in entries)
            {
                comboBoxStar.Items.Add($"{entry[0]}: {entry[1]}, {entry[2]}");
            }

            if (comboBoxStar.Items.Count > 0)
            {
                comboBoxStar.SelectedIndex = 0;
            }
        }

        private void btnComboStarReload_Click(object sender, EventArgs e)
        {
            StarFill();
        }

        private void NoShadowSimple(bool ShadowState)
        {
            switch (ShadowState)
            {
                case true:
                    Core.WriteBytes(Core.BaseAddress + 0xF0860 + 2, new byte[] { 0x80, 0x3E });
                    break;
                default:
                    Core.WriteBytes(Core.BaseAddress + 0xF0860 + 2, new byte[] { 0x28, 0x00 });
                    break;
            }
        }

        private void ShadowSwap()
        {
            for (int i = 0; i < 256; i++) // A 16x16 IA8 Texture
            {
                Core.WriteBytes(Core.SegmentedToVirtual(0x020120B8) + i, new byte[] { 0x00 });
            }
        }

        private void DustSwap()
        {
            for (int i = 0; i < 2048; i++) // A 32x32 IA16 Texture
            {
                Core.WriteBytes(Core.SegmentedToVirtual(0x03000080) + i, new byte[] { 0x00 });

                Core.WriteBytes(Core.SegmentedToVirtual(0x0401DEA0) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0401E6A0) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0401EEA0) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0401F6A0) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0401FEA0) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x040206A0) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04020EA0) + i, new byte[] { 0x00 });
            }
        }

        private void SparkleSwap()
        {
            for (int i = 0; i < 2048; i++) // A 32x32 RGBA16 Texture
            {
                Core.WriteBytes(Core.SegmentedToVirtual(0x04029C90) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04029490) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04028C90) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04028490) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04027C90) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04027490) + i, new byte[] { 0x00 });
            }
        }

        private void BubbleSwap()
        {
            for (int i = 0; i < 2048; i++) // A 32x32 RGBA16 Texture
            {
                Core.WriteBytes(Core.SegmentedToVirtual(0x0401CD60) + i, new byte[] { 0x00 });
            }
        }

        private void WaterSurfaceSwap()
        {
            for (int i = 0; i < 2048; i++) // A 32x32 IA16 Texture
            {
                Core.WriteBytes(Core.SegmentedToVirtual(0x04022148) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04022948) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04023148) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04023948) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04024148) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04024948) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04025358) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04025B58) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04026358) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x04026B58) + i, new byte[] { 0x00 });
            }
        }

        private void WaterSplashSwap()
        {
            for (int i = 0; i < 4096; i++) // A 32x64 IA16 Texture
            {
                Core.WriteBytes(Core.SegmentedToVirtual(0x0402A5C8) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0402B5C8) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0402C5C8) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0402D5C8) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0402E5C8) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x0402F5C8) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x040305C8) + i, new byte[] { 0x00 });
                Core.WriteBytes(Core.SegmentedToVirtual(0x040315C8) + i, new byte[] { 0x00 });
            }
            for (int i = 0; i < 512; i++) // A 16x16 IA16 Texture
            {
                Core.WriteBytes(Core.SegmentedToVirtual(0x04032780) + i, new byte[] { 0x00 });
            }
        }

        private void StarModelSwap(UInt32 fromModel, UInt32 toModel)
        {
            for (int i = 0; i <= 2097151; i++)
            {
                if (BitConverter.ToUInt32(Core.ReadBytes(Core.BaseAddress + (i * 4), 4), 0) == fromModel)
                {
                    Core.WriteBytes(Core.BaseAddress + (i * 4), BitConverter.GetBytes(toModel));
                }
            }
        }

        private void ColorCombinerCommandSwap(UInt64 fromCommand, UInt64 toCommand)
        {
            for (int i = 0; i <= 1048575; i++)
            {
                if (BitConverter.ToUInt64(Core.ReadBytes(Core.BaseAddress + (i * 8), 8), 0) == fromCommand)
                {
                    Core.WriteBytes(Core.BaseAddress + (i * 8), BitConverter.GetBytes(toCommand));
                }
            }
        }

        private List<uint> getComboBoxStarAddresses()
        {
            string comboBoxStarText = comboBoxStar.Text;

            int lastColonIndex = comboBoxStarText.LastIndexOf(": ");
            string remainingText = comboBoxStarText.Substring(lastColonIndex + 2); // Remove everything before the last ": " inclusive

            List<string> stringItems = remainingText.Split(new[] { ", " }, StringSplitOptions.None).ToList();

            List<uint> hexValues = stringItems
                .Select(item => Convert.ToUInt32(item, 16))
                .ToList();

            return hexValues;
        }

        private void btnAppearUncollected_Click(object sender, EventArgs e)
        {
            List<uint> starAddresses = getComboBoxStarAddresses();
            StarModelSwap(starAddresses[0], starAddresses[1]);
        }

        private void btnAppearCollected_Click(object sender, EventArgs e)
        {
            List<uint> starAddresses = getComboBoxStarAddresses();
            StarModelSwap(starAddresses[1], starAddresses[0]);
        }

        private void btnFixCam_Click(object sender, EventArgs e)
        {
            Core.WriteBytes(Core.BaseAddress + 0x32F872, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F873, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F874, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F875, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F876, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F877, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F878, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F879, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F87A, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F87B, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F87C, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F87E, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F87F, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F880, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F881, new byte[] { 0x0 });
            Core.WriteBytes(Core.BaseAddress + 0x32F882, new byte[] { 0x0 });
        }

        private void checkNoHeadRotations_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkNoHeadRotations.Checked)
            {
                case true:
                    Core.WriteAnimationSwap(Core.animList.Find(anim => anim.RealIndex == 195), Core.animList.Find(anim => anim.RealIndex == 194));
                    Core.WriteAnimationSwap(Core.animList.Find(anim => anim.RealIndex == 196), Core.animList.Find(anim => anim.RealIndex == 194));
                    Core.WriteAnimationSwap(Core.animList.Find(anim => anim.RealIndex == 197), Core.animList.Find(anim => anim.RealIndex == 194));
                    break;
                default:
                    Core.WriteAnimationSwap(Core.animList.Find(anim => anim.RealIndex == 195), Core.animList.Find(anim => anim.RealIndex == 195));
                    Core.WriteAnimationSwap(Core.animList.Find(anim => anim.RealIndex == 196), Core.animList.Find(anim => anim.RealIndex == 196));
                    Core.WriteAnimationSwap(Core.animList.Find(anim => anim.RealIndex == 197), Core.animList.Find(anim => anim.RealIndex == 197));
                    break;
            }
        }

        private void btnFixBlackTextures_Click(object sender, EventArgs e)
        {
            int selectedIndexInput = comboBlackInput.SelectedIndex;
            int selectedIndexOutput = comboBlackOutput.SelectedIndex;
            ColorCombinerCommandSwap(ColorCombinerCommands[selectedIndexInput++], ColorCombinerCommands[selectedIndexOutput++]);
        }

        private void checkMPHead_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPHeadAddresses, checkMPHead.Checked, false);
        }

        private void checkMPTorso_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPTorsoAddresses1, checkMPTorso.Checked, false);
            MPEdit(MPTorsoAddresses2, checkMPTorso.Checked, true);
        }

        private void checkMPCore_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPCoreAddresses, checkMPCore.Checked, false);
        }

        private void checkMPArmL1_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPArmL1Addresses, checkMPArmL1.Checked, false);
        }

        private void checkMPArmL2_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPArmL2Addresses, checkMPArmL2.Checked, true);
        }

        private void checkMPLeftHand_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPLeftHandAddresses, checkMPLeftHand.Checked, false);
        }

        private void checkMPLegL1_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPLegL1Addresses, checkMPLegL1.Checked, false);
        }

        private void checkMPLegL2_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPLegL2Addresses, checkMPLegL2.Checked, true);
        }

        private void checkMPLeftFoot_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPLeftFootAddresses, checkMPLeftFoot.Checked, false);
        }

        private void checkMPArmR1_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPArmR1Addresses, checkMPArmR1.Checked, false);
        }

        private void checkMPArmR2_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPArmR2Addresses, checkMPArmR2.Checked, true);
        }

        private void checkMPRightHand_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPRightHandAddresses, checkMPRightHand.Checked, false);
        }

        private void checkMPLegR1_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPLegR1Addresses, checkMPLegR1.Checked, false);
        }

        private void checkMPLegR2_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPLegR2Addresses, checkMPLegR2.Checked, true);
        }

        private void checkMPRightFoot_CheckedChanged(object sender, EventArgs e)
        {
            MPEdit(MPRightFootAddresses, checkMPRightFoot.Checked, false);
        }

        private void groupMPShowAll_Click(object sender, EventArgs e)
        {/*
            MPEdit(MPHeadAddresses, true, false);
            MPEdit(MPTorsoAddresses1, true, false);
            MPEdit(MPTorsoAddresses2, true, true);
            MPEdit(MPCoreAddresses, true, false);
            MPEdit(MPArmL1Addresses, true, false);
            MPEdit(MPArmL2Addresses, true, true);
            MPEdit(MPLeftHandAddresses, true, false);
            MPEdit(MPLegL1Addresses, true, false);
            MPEdit(MPLegL2Addresses, true, true);
            MPEdit(MPLeftFootAddresses, true, false);
            MPEdit(MPArmR1Addresses, true, false);
            MPEdit(MPArmR2Addresses, true, true);
            MPEdit(MPRightHandAddresses, true, false);
            MPEdit(MPLegR1Addresses, true, false);
            MPEdit(MPLegR2Addresses, true, true);
            MPEdit(MPRightFootAddresses, true, false);*/

            checkMPHead.Checked = true;
            checkMPTorso.Checked = true;
            checkMPCore.Checked = true;
            checkMPArmL1.Checked = true;
            checkMPArmL2.Checked = true;
            checkMPLeftHand.Checked = true;
            checkMPLegL1.Checked = true;
            checkMPLegL2.Checked = true;
            checkMPLeftFoot.Checked = true;
            checkMPArmR1.Checked = true;
            checkMPArmR2.Checked = true;
            checkMPRightHand.Checked = true;
            checkMPLegR1.Checked = true;
            checkMPLegR2.Checked = true;
            checkMPRightFoot.Checked = true;
        }

        private void groupMPHideAll_Click(object sender, EventArgs e)
        {/*
            MPEdit(MPHeadAddresses, false, false);
            MPEdit(MPTorsoAddresses1, false, false);
            MPEdit(MPTorsoAddresses2, false, true);
            MPEdit(MPCoreAddresses, false, false);
            MPEdit(MPArmL1Addresses, false, false);
            MPEdit(MPArmL2Addresses, false, true);
            MPEdit(MPLeftHandAddresses, false, false);
            MPEdit(MPLegL1Addresses, false, false);
            MPEdit(MPLegL2Addresses, false, true);
            MPEdit(MPLeftFootAddresses, false, false);
            MPEdit(MPArmR1Addresses, false, false);
            MPEdit(MPArmR2Addresses, false, true);
            MPEdit(MPRightHandAddresses, false, false);
            MPEdit(MPLegR1Addresses, false, false);
            MPEdit(MPLegR2Addresses, false, true);
            MPEdit(MPRightFootAddresses, false, false);*/

            checkMPHead.Checked = false;
            checkMPTorso.Checked = false;
            checkMPCore.Checked = false;
            checkMPArmL1.Checked = false;
            checkMPArmL2.Checked = false;
            checkMPLeftHand.Checked = false;
            checkMPLegL1.Checked = false;
            checkMPLegL2.Checked = false;
            checkMPLeftFoot.Checked = false;
            checkMPArmR1.Checked = false;
            checkMPArmR2.Checked = false;
            checkMPRightHand.Checked = false;
            checkMPLegR1.Checked = false;
            checkMPLegR2.Checked = false;
            checkMPRightFoot.Checked = false;
        }

        private void checkNoShadowSimple_CheckedChanged(object sender, EventArgs e)
        {
            NoShadowSimple(checkNoShadowSimple.Checked);
        }

        private void btnBlackBars_Click(object sender, EventArgs e)
        {
            BlackBarsEdit();
        }

        private void btnAdvShRemove_Click(object sender, EventArgs e)
        {
            ShadowSwap();
        }

        private void btnRemoveDust_Click(object sender, EventArgs e)
        {
            DustSwap();
        }

        private void btnRemoveSparkles_Click(object sender, EventArgs e)
        {
            SparkleSwap();
        }

        private void btnRemoveBubbles_Click(object sender, EventArgs e)
        {
            BubbleSwap();
        }

        private void btnRemoveWaterEffects_Click(object sender, EventArgs e)
        {
            WaterSurfaceSwap();
        }

        private void btnRemoveSplashes_Click(object sender, EventArgs e)
        {
            WaterSplashSwap();
        }

        private void btnSmoke_Click(object sender, EventArgs e)
        {
            FixSmokeTexture();
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                btnAppearUncollected.Image = Properties.Resources.Land94_Star;
            }
        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                btnAppearUncollected.Image = Properties.Resources.Rev11_Star;
            }

        }
    }
}