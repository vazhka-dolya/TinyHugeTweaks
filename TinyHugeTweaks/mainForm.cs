using M64MM.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyHugeTweaks.Controls.kataraktaTreeView;
using TinyHugeTweaks.Properties;

namespace TinyHugeTweaks
{
    public partial class mainForm : Form
    {
        public class RAMApplyItem
        {
            public byte[] Pixels { get; set; }
            public uint SegAddress { get; set; }
        }

        string IsLatestVersion = "Unknown";
        string LatestVersion = "Unknown";
        static string CreatorName = "vazhka-dolya";
        static string AddonLinkName = "TinyHugeTweaks";
        static string AddonReleaseName = "Tiny-Huge Tweaks";

        public bool isSelecting = false;
        public static string TinyHugeTweaksPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Addons\\TinyHugeTweaks\\";

        static frmAbout about = new frmAbout();

        public ReadOnlyCollection<ulong> ColorCombinerCommands = new ReadOnlyCollection<ulong>(new ulong[] { 0xFFFFF9FCFC127E24, 0xFF33FFFFFC121824, 0xFFFFF838FC127FFF, 0xFFFCF238FCFFFFFF });

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

        public mainForm()
        {
            this.KeyPreview = true;
            this.KeyDown += mainForm_KeyDown;
            this.KeyUp += mainForm_KeyUp;

            InitializeComponent();

            this.Load += mainForm_Load;
            this.Text = $"Tiny-Huge Tweaks {ProductVersion}";

            StarFill();

            comboBlackInput.SelectedIndex = 2;
            comboBlackOutput.SelectedIndex = 1;

            SetUpTreeViewDPI();
            LoadATR();

            AdjustButtonImageDPI();
        }

        private async void mainForm_Load(object sender, EventArgs e)
        {
            await CheckForUpdates();
        }

        private double CalculateDPIDifference()
        {
            // 96 is 1080p DPI
            return (double)(this.DeviceDpi - 96) / Math.Abs(96) + 1;
        }

        private int AdjustDPIInt(int ValueToAdjust)
        {
            return (int)(ValueToAdjust * CalculateDPIDifference());
        }

        private IEnumerable<Button> GetButtonsWithImages(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button && button.Image != null)
                    yield return button;

                foreach (var childbutton in GetButtonsWithImages(control))
                    yield return childbutton;
            }
        }

        private void AdjustButtonImageDPI()
        {
            // Upscale button icons for higher DPI
            // Because WinForms doesn't want to do it by itself
            if (this.DeviceDpi > 96f)
            {
                foreach (var button in GetButtonsWithImages(this))
                {
                    Bitmap originalIcon = new Bitmap(button.Image);
                    float scaleFactor = (float)(this.DeviceDpi - 96) / Math.Abs(96) + 1;
                    int newWidth = (int)(originalIcon.Width * scaleFactor);
                    int newHeight = (int)(originalIcon.Height * scaleFactor);

                    Bitmap scaled = new Bitmap(newWidth, newHeight);
                    using (Graphics g = Graphics.FromImage(scaled))
                    {
                        // NearestNeighbor looks ugly with DPIs that are
                        // increments of 25% but not of 50% (e. g. 125%, 175% etc.),
                        // and not scaling them up at all also looks bad,
                        // so we'll set HighQualityBicubic for those.
                        float remainder = scaleFactor % 1.0f;
                        if (Math.Abs(remainder - 0.25f) < 0.01f || Math.Abs(remainder - 0.75f) < 0.01f)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        else g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                        g.DrawImage(originalIcon, 0, 0, newWidth, newHeight);
                    }

                    button.Image = scaled;
                }
            }
        }

        private void SetUpTreeViewDPI()
        {
            //treeView1.ImageList.ImageSize = new Size(AdjustDPIInt(16), AdjustDPIInt(16));
            treeView1.Font = new Font("Microsoft Sans Serif",
                // A bit complex but ensures that the size is good on all DPIs
                AdjustDPIInt(9) + (float)0.6 - (float)((CalculateDPIDifference() - 1) * 5),
                GraphicsUnit.Point);
            treeView1.ItemHeight = AdjustDPIInt(20);
        }

        private void LoadATR()
        {
            TreeNode CacheShadowRound = new TreeNode(Resources.CacheShadowRound);
            CacheShadowRound.Tag = "shadow_round";
            treeView1.Nodes.Add(CacheShadowRound);

            TreeNode CacheShadowSquare = new TreeNode(Resources.CacheShadowSquare);
            CacheShadowSquare.Tag = "shadow_square";
            treeView1.Nodes.Add(CacheShadowSquare);

            TreeNode CacheDust = new TreeNode(Resources.CacheDust);
            CacheDust.Tag = "dust";
            treeView1.Nodes.Add(CacheDust);

            TreeNode CacheSnowParticle = new TreeNode(Resources.CacheSnowParticle);
            CacheSnowParticle.Tag = "snow_particle";
            treeView1.Nodes.Add(CacheSnowParticle);

            TreeNode CacheWaterParticle = new TreeNode(Resources.CacheWaterParticle);
            CacheWaterParticle.Tag = "water_particle";
            treeView1.Nodes.Add(CacheWaterParticle);

            TreeNode CacheWater = new TreeNode(Resources.CacheWater);
            CacheWater.Tag = "water";
            treeView1.Nodes.Add(CacheWater);

            TreeNode CacheWaterSplash = new TreeNode(Resources.CacheWaterSplash);
            CacheWaterSplash.Tag = "water_splash";
            treeView1.Nodes.Add(CacheWaterSplash);

            TreeNode CacheBubble = new TreeNode(Resources.CacheBubble);
            CacheBubble.Tag = "bubble";
            treeView1.Nodes.Add(CacheBubble);

            TreeNode CacheExplosion = new TreeNode(Resources.CacheExplosion);
            CacheExplosion.Tag = "explosion";
            treeView1.Nodes.Add(CacheExplosion);

            TreeNode CacheSparkle = new TreeNode(Resources.CacheSparkle);
            CacheSparkle.Tag = "sparkle";
            treeView1.Nodes.Add(CacheSparkle);

            TreeNode CacheButterfly = new TreeNode(Resources.CacheButterfly);
            CacheButterfly.Tag = "butterfly";
            treeView1.Nodes.Add(CacheButterfly);

            TreeNode CacheLeaf = new TreeNode(Resources.CacheLeaf);
            CacheLeaf.Tag = "leaf";
            treeView1.Nodes.Add(CacheLeaf);

            TreeNode CacheCoin = new TreeNode(Resources.CacheCoin);
            CacheCoin.Tag = "coin";
            treeView1.Nodes.Add(CacheCoin);
        }

        private void ATRHide(TreeNode node)
        {
            if (node != null)
            {
                string filePath = Path.Combine
                    (TinyHugeTweaksPath,
                    $"ImageData\\{node.Tag}_empty.kcscache.bin");
                ApplyCache(filePath);
            }
        }

        private void ATRShow(TreeNode node)
        {
            if (node != null)
            {
                string filePath = Path.Combine
                    (TinyHugeTweaksPath,
                    $"ImageData\\{node.Tag}.kcscache.bin");
                ApplyCache(filePath);
            }
        }

        private void ATRHideAll()
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
                ATRHide(node);
            }
        }

        private void ATRShowAll()
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
                ATRShow(node);
            }
        }

        private List<RAMApplyItem> CacheRead(string CachePath)
        {
            byte[] Cache = File.ReadAllBytes(CachePath);

            List<RAMApplyItem> FoundCacheTextures = new List<RAMApplyItem>();

            using (FileStream fs = new FileStream(CachePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    // Get first four bytes as the address
                    uint address = reader.ReadUInt32();

                    // Get the next four bytes as the length (aka the amount of bytes in the texture)
                    uint length = reader.ReadUInt32();

                    // Give an error if the specified length is longer than the rest of the file
                    if (reader.BaseStream.Position + length > reader.BaseStream.Length)
                    {
                        throw new Exception
                            ("Invalid cache file: Specified length of the last texture is longer than the rest of the file.");
                    }

                    // Treat the specified amount (length) of bytes as the texture.
                    byte[] TextureData = reader.ReadBytes((int)length);
                    FoundCacheTextures.Add(new RAMApplyItem
                    {
                        Pixels = TextureData,
                        SegAddress = BitConverter.ToUInt32(Core.SwapEndian(BitConverter.GetBytes(address), 4), 0)
                    });
                }
            }

            return FoundCacheTextures;
        }

        private void ApplyCache(string CachePath)
        {
            {
                List<RAMApplyItem> CurrentCache = CacheRead(CachePath);
                foreach (var item in CurrentCache)
                {
                    ApplyTextureRAM(item.Pixels, item.SegAddress);
                }
            }
        }

        public void ApplyTextureRAM(byte[] Pixels, uint SegAddress)
        {
            Core.WriteBytes(Core.SegmentedToVirtual(SegAddress), Pixels);
        }

        private void UpdatesButtonPress()
        {
            switch (IsLatestVersion)
            {
                case "True":
                    Process.Start($"https://github.com/{CreatorName}/{AddonLinkName}/releases");
                    break;
                case "False":
                    Process.Start($"https://github.com/{CreatorName}/{AddonLinkName}/releases/latest");
                    break;
                default:
                    DialogResult result = MessageBox.Show(
                        Resources.updates_unknown_elaborate,
                        Resources.updates_unknown_string,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        Process.Start($"https://github.com/{CreatorName}/{AddonLinkName}/releases/latest");
                    }
                    break;
            }
        }

        private async Task CheckForUpdates()
        {
            updatesToolStripMenuItem.Image = Resources.updates_unknown;
            updatesToolStripMenuItem.Text = Resources.updates_checking_string;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", $"{AddonLinkName}")
;
                    var LatestResponse = await client.GetStringAsync($"https://api.github.com/repos/{CreatorName}/{AddonLinkName}/releases/latest");

                    JObject json = JObject.Parse(LatestResponse);
                    LatestVersion = (string)json["name"];

                    if ($"{AddonReleaseName} v" + ProductVersion == LatestVersion)
                        IsLatestVersion = "True";
                    else IsLatestVersion = "False";
                }
            }
            catch
            {
                IsLatestVersion = "Unknown";
                LatestVersion = "Unknown";
            }

            switch (IsLatestVersion)
            {
                case "True":
                    updatesToolStripMenuItem.Image = Resources.updates_latest;
                    updatesToolStripMenuItem.Text = Resources.updates_latest_string;
                    break;
                case "False":
                    updatesToolStripMenuItem.Image = Resources.updates_outdated;
                    updatesToolStripMenuItem.Text = Resources.updates_outdated_string;
                    break;
                default:
                    updatesToolStripMenuItem.Image = Resources.updates_unknown;
                    updatesToolStripMenuItem.Text = Resources.updates_unknown_string;
                    break;
            }
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
        {
            Core.WriteBytes(Core.BaseAddress + 0x2473A4, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x2473B0, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x247490, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x2474A0, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x2475A0, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x2475AC, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x247D1C, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B46C, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B47C, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27B4F8, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B508, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27B584, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x27B594, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x27C99C, new byte[] { 0xBC });
            Core.WriteBytes(Core.BaseAddress + 0x27C9A8, new byte[] { 0x00 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7B4, new byte[] { 0x50 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7B8, new byte[] { 0xC0 });
            Core.WriteBytes(Core.BaseAddress + 0x2DA7AC, new byte[] { 0x00, 0x00, 0x00, 0x00 });
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
            string filePath = Path.GetDirectoryName
                (Assembly.GetEntryAssembly().Location) + "/Addons/TinyHugeTweaks/starAddresses.config";
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
        {
            // Uncheck them first in case they were already checked
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
        {
            // Check them first in case they were already uchecked
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

        private async void updatesrefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await CheckForUpdates();
        }

        private void updatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatesButtonPress();
        }

        private void buttonATRHide_Click(object sender, EventArgs e)
        {
            ATRHide(treeView1.SelectedNode);
        }

        private void buttonATRShow_Click(object sender, EventArgs e)
        {
            ATRShow(treeView1.SelectedNode);
        }

        protected void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (isSelecting) // Should prevent the method from repeating when it's not needed
            {
                return;
            }

            isSelecting = true;

            e.Cancel = true;

            kataraktaTreeView treeView = sender as kataraktaTreeView;

            // The canceling and programmatic selecting are done because otherwise
            // it would scroll to the selected node even when it's not needed
            treeView.SelectedNode = e.Node;

            isSelecting = false;
        }

        private void buttonATRHideAll_Click(object sender, EventArgs e)
        {
            ATRHideAll();
        }

        private void buttonATRShowAll_Click(object sender, EventArgs e)
        {
            ATRShowAll();
        }
    }
}