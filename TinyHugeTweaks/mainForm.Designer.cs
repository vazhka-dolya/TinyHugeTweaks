using TinyHugeTweaks.Controls.kataraktaTreeView;
namespace TinyHugeTweaks
{
    partial class mainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesrefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupStarsAppearance = new System.Windows.Forms.GroupBox();
            this.btnComboStarReload = new System.Windows.Forms.Button();
            this.comboBoxStar = new System.Windows.Forms.ComboBox();
            this.btnAppearCollected = new System.Windows.Forms.Button();
            this.btnAppearUncollected = new System.Windows.Forms.Button();
            this.lbStarAddresses = new System.Windows.Forms.Label();
            this.groupBlackTextures = new System.Windows.Forms.GroupBox();
            this.btnFixBlackTextures = new System.Windows.Forms.Button();
            this.comboBlackOutput = new System.Windows.Forms.ComboBox();
            this.comboBlackInput = new System.Windows.Forms.ComboBox();
            this.btnFixCam = new System.Windows.Forms.Button();
            this.checkNoHeadRotations = new System.Windows.Forms.CheckBox();
            this.groupMain = new System.Windows.Forms.GroupBox();
            this.btnSmoke = new System.Windows.Forms.Button();
            this.btnBlackBars = new System.Windows.Forms.Button();
            this.checkNoShadowSimple = new System.Windows.Forms.CheckBox();
            this.groupMP = new System.Windows.Forms.GroupBox();
            this.checkMPRightFoot = new System.Windows.Forms.CheckBox();
            this.checkMPLegR2 = new System.Windows.Forms.CheckBox();
            this.checkMPLegR1 = new System.Windows.Forms.CheckBox();
            this.checkMPLeftFoot = new System.Windows.Forms.CheckBox();
            this.checkMPLegL2 = new System.Windows.Forms.CheckBox();
            this.checkMPLegL1 = new System.Windows.Forms.CheckBox();
            this.checkMPRightHand = new System.Windows.Forms.CheckBox();
            this.checkMPArmR2 = new System.Windows.Forms.CheckBox();
            this.checkMPArmR1 = new System.Windows.Forms.CheckBox();
            this.checkMPLeftHand = new System.Windows.Forms.CheckBox();
            this.checkMPArmL2 = new System.Windows.Forms.CheckBox();
            this.checkMPArmL1 = new System.Windows.Forms.CheckBox();
            this.checkMPTorso = new System.Windows.Forms.CheckBox();
            this.checkMPCore = new System.Windows.Forms.CheckBox();
            this.checkMPHead = new System.Windows.Forms.CheckBox();
            this.groupMPHideAll = new System.Windows.Forms.Button();
            this.groupMPShowAll = new System.Windows.Forms.Button();
            this.pictureMP = new System.Windows.Forms.PictureBox();
            this.groupAdvancedTexture = new System.Windows.Forms.GroupBox();
            this.buttonATRShowAll = new System.Windows.Forms.Button();
            this.buttonATRHideAll = new System.Windows.Forms.Button();
            this.buttonATRShow = new System.Windows.Forms.Button();
            this.buttonATRHide = new System.Windows.Forms.Button();
            this.treeView1 = new TinyHugeTweaks.Controls.kataraktaTreeView.kataraktaTreeView();
            this.menuStrip1.SuspendLayout();
            this.groupStarsAppearance.SuspendLayout();
            this.groupBlackTextures.SuspendLayout();
            this.groupMain.SuspendLayout();
            this.groupMP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMP)).BeginInit();
            this.groupAdvancedTexture.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.updatesToolStripMenuItem,
            this.updatesrefreshToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // updatesToolStripMenuItem
            // 
            resources.ApplyResources(this.updatesToolStripMenuItem, "updatesToolStripMenuItem");
            this.updatesToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.updatesToolStripMenuItem.Image = global::TinyHugeTweaks.Properties.Resources.updates_unknown;
            this.updatesToolStripMenuItem.Name = "updatesToolStripMenuItem";
            this.updatesToolStripMenuItem.Click += new System.EventHandler(this.updatesToolStripMenuItem_Click);
            // 
            // updatesrefreshToolStripMenuItem
            // 
            resources.ApplyResources(this.updatesrefreshToolStripMenuItem, "updatesrefreshToolStripMenuItem");
            this.updatesrefreshToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.updatesrefreshToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.updatesrefreshToolStripMenuItem.Image = global::TinyHugeTweaks.Properties.Resources.updates_refresh;
            this.updatesrefreshToolStripMenuItem.Name = "updatesrefreshToolStripMenuItem";
            this.updatesrefreshToolStripMenuItem.Click += new System.EventHandler(this.updatesrefreshToolStripMenuItem_Click);
            // 
            // groupStarsAppearance
            // 
            resources.ApplyResources(this.groupStarsAppearance, "groupStarsAppearance");
            this.groupStarsAppearance.Controls.Add(this.btnComboStarReload);
            this.groupStarsAppearance.Controls.Add(this.comboBoxStar);
            this.groupStarsAppearance.Controls.Add(this.btnAppearCollected);
            this.groupStarsAppearance.Controls.Add(this.btnAppearUncollected);
            this.groupStarsAppearance.Controls.Add(this.lbStarAddresses);
            this.groupStarsAppearance.Name = "groupStarsAppearance";
            this.groupStarsAppearance.TabStop = false;
            // 
            // btnComboStarReload
            // 
            resources.ApplyResources(this.btnComboStarReload, "btnComboStarReload");
            this.btnComboStarReload.Name = "btnComboStarReload";
            this.btnComboStarReload.UseVisualStyleBackColor = true;
            this.btnComboStarReload.Click += new System.EventHandler(this.btnComboStarReload_Click);
            // 
            // comboBoxStar
            // 
            resources.ApplyResources(this.comboBoxStar, "comboBoxStar");
            this.comboBoxStar.FormattingEnabled = true;
            this.comboBoxStar.Name = "comboBoxStar";
            // 
            // btnAppearCollected
            // 
            resources.ApplyResources(this.btnAppearCollected, "btnAppearCollected");
            this.btnAppearCollected.Image = global::TinyHugeTweaks.Properties.Resources.Rev1_StarMissing;
            this.btnAppearCollected.Name = "btnAppearCollected";
            this.btnAppearCollected.UseVisualStyleBackColor = true;
            this.btnAppearCollected.Click += new System.EventHandler(this.btnAppearCollected_Click);
            // 
            // btnAppearUncollected
            // 
            resources.ApplyResources(this.btnAppearUncollected, "btnAppearUncollected");
            this.btnAppearUncollected.Image = global::TinyHugeTweaks.Properties.Resources.Rev11_Star;
            this.btnAppearUncollected.Name = "btnAppearUncollected";
            this.btnAppearUncollected.UseVisualStyleBackColor = true;
            this.btnAppearUncollected.Click += new System.EventHandler(this.btnAppearUncollected_Click);
            // 
            // lbStarAddresses
            // 
            resources.ApplyResources(this.lbStarAddresses, "lbStarAddresses");
            this.lbStarAddresses.Name = "lbStarAddresses";
            // 
            // groupBlackTextures
            // 
            resources.ApplyResources(this.groupBlackTextures, "groupBlackTextures");
            this.groupBlackTextures.Controls.Add(this.btnFixBlackTextures);
            this.groupBlackTextures.Controls.Add(this.comboBlackOutput);
            this.groupBlackTextures.Controls.Add(this.comboBlackInput);
            this.groupBlackTextures.Name = "groupBlackTextures";
            this.groupBlackTextures.TabStop = false;
            // 
            // btnFixBlackTextures
            // 
            resources.ApplyResources(this.btnFixBlackTextures, "btnFixBlackTextures");
            this.btnFixBlackTextures.Name = "btnFixBlackTextures";
            this.btnFixBlackTextures.UseVisualStyleBackColor = true;
            this.btnFixBlackTextures.Click += new System.EventHandler(this.btnFixBlackTextures_Click);
            // 
            // comboBlackOutput
            // 
            resources.ApplyResources(this.comboBlackOutput, "comboBlackOutput");
            this.comboBlackOutput.FormattingEnabled = true;
            this.comboBlackOutput.Items.AddRange(new object[] {
            resources.GetString("comboBlackOutput.Items"),
            resources.GetString("comboBlackOutput.Items1"),
            resources.GetString("comboBlackOutput.Items2"),
            resources.GetString("comboBlackOutput.Items3")});
            this.comboBlackOutput.Name = "comboBlackOutput";
            // 
            // comboBlackInput
            // 
            resources.ApplyResources(this.comboBlackInput, "comboBlackInput");
            this.comboBlackInput.FormattingEnabled = true;
            this.comboBlackInput.Items.AddRange(new object[] {
            resources.GetString("comboBlackInput.Items"),
            resources.GetString("comboBlackInput.Items1"),
            resources.GetString("comboBlackInput.Items2"),
            resources.GetString("comboBlackInput.Items3")});
            this.comboBlackInput.Name = "comboBlackInput";
            // 
            // btnFixCam
            // 
            resources.ApplyResources(this.btnFixCam, "btnFixCam");
            this.btnFixCam.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFixCam.Name = "btnFixCam";
            this.btnFixCam.UseVisualStyleBackColor = true;
            this.btnFixCam.Click += new System.EventHandler(this.btnFixCam_Click);
            // 
            // checkNoHeadRotations
            // 
            resources.ApplyResources(this.checkNoHeadRotations, "checkNoHeadRotations");
            this.checkNoHeadRotations.Name = "checkNoHeadRotations";
            this.checkNoHeadRotations.UseVisualStyleBackColor = true;
            this.checkNoHeadRotations.CheckedChanged += new System.EventHandler(this.checkNoHeadRotations_CheckedChanged);
            // 
            // groupMain
            // 
            resources.ApplyResources(this.groupMain, "groupMain");
            this.groupMain.Controls.Add(this.btnSmoke);
            this.groupMain.Controls.Add(this.btnBlackBars);
            this.groupMain.Controls.Add(this.checkNoShadowSimple);
            this.groupMain.Controls.Add(this.btnFixCam);
            this.groupMain.Controls.Add(this.checkNoHeadRotations);
            this.groupMain.Name = "groupMain";
            this.groupMain.TabStop = false;
            // 
            // btnSmoke
            // 
            resources.ApplyResources(this.btnSmoke, "btnSmoke");
            this.btnSmoke.Name = "btnSmoke";
            this.btnSmoke.UseVisualStyleBackColor = true;
            this.btnSmoke.Click += new System.EventHandler(this.btnSmoke_Click);
            // 
            // btnBlackBars
            // 
            resources.ApplyResources(this.btnBlackBars, "btnBlackBars");
            this.btnBlackBars.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBlackBars.Name = "btnBlackBars";
            this.btnBlackBars.UseVisualStyleBackColor = true;
            this.btnBlackBars.Click += new System.EventHandler(this.btnBlackBars_Click);
            // 
            // checkNoShadowSimple
            // 
            resources.ApplyResources(this.checkNoShadowSimple, "checkNoShadowSimple");
            this.checkNoShadowSimple.Name = "checkNoShadowSimple";
            this.checkNoShadowSimple.UseVisualStyleBackColor = true;
            this.checkNoShadowSimple.CheckedChanged += new System.EventHandler(this.checkNoShadowSimple_CheckedChanged);
            // 
            // groupMP
            // 
            resources.ApplyResources(this.groupMP, "groupMP");
            this.groupMP.Controls.Add(this.checkMPRightFoot);
            this.groupMP.Controls.Add(this.checkMPLegR2);
            this.groupMP.Controls.Add(this.checkMPLegR1);
            this.groupMP.Controls.Add(this.checkMPLeftFoot);
            this.groupMP.Controls.Add(this.checkMPLegL2);
            this.groupMP.Controls.Add(this.checkMPLegL1);
            this.groupMP.Controls.Add(this.checkMPRightHand);
            this.groupMP.Controls.Add(this.checkMPArmR2);
            this.groupMP.Controls.Add(this.checkMPArmR1);
            this.groupMP.Controls.Add(this.checkMPLeftHand);
            this.groupMP.Controls.Add(this.checkMPArmL2);
            this.groupMP.Controls.Add(this.checkMPArmL1);
            this.groupMP.Controls.Add(this.checkMPTorso);
            this.groupMP.Controls.Add(this.checkMPCore);
            this.groupMP.Controls.Add(this.checkMPHead);
            this.groupMP.Controls.Add(this.groupMPHideAll);
            this.groupMP.Controls.Add(this.groupMPShowAll);
            this.groupMP.Controls.Add(this.pictureMP);
            this.groupMP.Name = "groupMP";
            this.groupMP.TabStop = false;
            // 
            // checkMPRightFoot
            // 
            resources.ApplyResources(this.checkMPRightFoot, "checkMPRightFoot");
            this.checkMPRightFoot.BackColor = System.Drawing.Color.Transparent;
            this.checkMPRightFoot.Checked = true;
            this.checkMPRightFoot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPRightFoot.Name = "checkMPRightFoot";
            this.checkMPRightFoot.UseVisualStyleBackColor = false;
            this.checkMPRightFoot.CheckedChanged += new System.EventHandler(this.checkMPRightFoot_CheckedChanged);
            // 
            // checkMPLegR2
            // 
            resources.ApplyResources(this.checkMPLegR2, "checkMPLegR2");
            this.checkMPLegR2.BackColor = System.Drawing.Color.Transparent;
            this.checkMPLegR2.Checked = true;
            this.checkMPLegR2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPLegR2.Name = "checkMPLegR2";
            this.checkMPLegR2.UseVisualStyleBackColor = false;
            this.checkMPLegR2.CheckedChanged += new System.EventHandler(this.checkMPLegR2_CheckedChanged);
            // 
            // checkMPLegR1
            // 
            resources.ApplyResources(this.checkMPLegR1, "checkMPLegR1");
            this.checkMPLegR1.BackColor = System.Drawing.Color.Transparent;
            this.checkMPLegR1.Checked = true;
            this.checkMPLegR1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPLegR1.Name = "checkMPLegR1";
            this.checkMPLegR1.UseVisualStyleBackColor = false;
            this.checkMPLegR1.CheckedChanged += new System.EventHandler(this.checkMPLegR1_CheckedChanged);
            // 
            // checkMPLeftFoot
            // 
            resources.ApplyResources(this.checkMPLeftFoot, "checkMPLeftFoot");
            this.checkMPLeftFoot.BackColor = System.Drawing.Color.Transparent;
            this.checkMPLeftFoot.Checked = true;
            this.checkMPLeftFoot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPLeftFoot.Name = "checkMPLeftFoot";
            this.checkMPLeftFoot.UseVisualStyleBackColor = false;
            this.checkMPLeftFoot.CheckedChanged += new System.EventHandler(this.checkMPLeftFoot_CheckedChanged);
            // 
            // checkMPLegL2
            // 
            resources.ApplyResources(this.checkMPLegL2, "checkMPLegL2");
            this.checkMPLegL2.BackColor = System.Drawing.Color.Transparent;
            this.checkMPLegL2.Checked = true;
            this.checkMPLegL2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPLegL2.Name = "checkMPLegL2";
            this.checkMPLegL2.UseVisualStyleBackColor = false;
            this.checkMPLegL2.CheckedChanged += new System.EventHandler(this.checkMPLegL2_CheckedChanged);
            // 
            // checkMPLegL1
            // 
            resources.ApplyResources(this.checkMPLegL1, "checkMPLegL1");
            this.checkMPLegL1.BackColor = System.Drawing.Color.Transparent;
            this.checkMPLegL1.Checked = true;
            this.checkMPLegL1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPLegL1.Name = "checkMPLegL1";
            this.checkMPLegL1.UseVisualStyleBackColor = false;
            this.checkMPLegL1.CheckedChanged += new System.EventHandler(this.checkMPLegL1_CheckedChanged);
            // 
            // checkMPRightHand
            // 
            resources.ApplyResources(this.checkMPRightHand, "checkMPRightHand");
            this.checkMPRightHand.BackColor = System.Drawing.Color.Transparent;
            this.checkMPRightHand.Checked = true;
            this.checkMPRightHand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPRightHand.Name = "checkMPRightHand";
            this.checkMPRightHand.UseVisualStyleBackColor = false;
            this.checkMPRightHand.CheckedChanged += new System.EventHandler(this.checkMPRightHand_CheckedChanged);
            // 
            // checkMPArmR2
            // 
            resources.ApplyResources(this.checkMPArmR2, "checkMPArmR2");
            this.checkMPArmR2.BackColor = System.Drawing.Color.Transparent;
            this.checkMPArmR2.Checked = true;
            this.checkMPArmR2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPArmR2.Name = "checkMPArmR2";
            this.checkMPArmR2.UseVisualStyleBackColor = false;
            this.checkMPArmR2.CheckedChanged += new System.EventHandler(this.checkMPArmR2_CheckedChanged);
            // 
            // checkMPArmR1
            // 
            resources.ApplyResources(this.checkMPArmR1, "checkMPArmR1");
            this.checkMPArmR1.BackColor = System.Drawing.Color.Transparent;
            this.checkMPArmR1.Checked = true;
            this.checkMPArmR1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPArmR1.Name = "checkMPArmR1";
            this.checkMPArmR1.UseVisualStyleBackColor = false;
            this.checkMPArmR1.CheckedChanged += new System.EventHandler(this.checkMPArmR1_CheckedChanged);
            // 
            // checkMPLeftHand
            // 
            resources.ApplyResources(this.checkMPLeftHand, "checkMPLeftHand");
            this.checkMPLeftHand.BackColor = System.Drawing.Color.Transparent;
            this.checkMPLeftHand.Checked = true;
            this.checkMPLeftHand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPLeftHand.Name = "checkMPLeftHand";
            this.checkMPLeftHand.UseVisualStyleBackColor = false;
            this.checkMPLeftHand.CheckedChanged += new System.EventHandler(this.checkMPLeftHand_CheckedChanged);
            // 
            // checkMPArmL2
            // 
            resources.ApplyResources(this.checkMPArmL2, "checkMPArmL2");
            this.checkMPArmL2.BackColor = System.Drawing.Color.Transparent;
            this.checkMPArmL2.Checked = true;
            this.checkMPArmL2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPArmL2.Name = "checkMPArmL2";
            this.checkMPArmL2.UseVisualStyleBackColor = false;
            this.checkMPArmL2.CheckedChanged += new System.EventHandler(this.checkMPArmL2_CheckedChanged);
            // 
            // checkMPArmL1
            // 
            resources.ApplyResources(this.checkMPArmL1, "checkMPArmL1");
            this.checkMPArmL1.BackColor = System.Drawing.Color.Transparent;
            this.checkMPArmL1.Checked = true;
            this.checkMPArmL1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPArmL1.Name = "checkMPArmL1";
            this.checkMPArmL1.UseVisualStyleBackColor = false;
            this.checkMPArmL1.CheckedChanged += new System.EventHandler(this.checkMPArmL1_CheckedChanged);
            // 
            // checkMPTorso
            // 
            resources.ApplyResources(this.checkMPTorso, "checkMPTorso");
            this.checkMPTorso.BackColor = System.Drawing.Color.Transparent;
            this.checkMPTorso.Checked = true;
            this.checkMPTorso.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPTorso.Name = "checkMPTorso";
            this.checkMPTorso.UseVisualStyleBackColor = false;
            this.checkMPTorso.CheckedChanged += new System.EventHandler(this.checkMPTorso_CheckedChanged);
            // 
            // checkMPCore
            // 
            resources.ApplyResources(this.checkMPCore, "checkMPCore");
            this.checkMPCore.BackColor = System.Drawing.Color.Transparent;
            this.checkMPCore.Checked = true;
            this.checkMPCore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPCore.Name = "checkMPCore";
            this.checkMPCore.UseVisualStyleBackColor = false;
            this.checkMPCore.CheckedChanged += new System.EventHandler(this.checkMPCore_CheckedChanged);
            // 
            // checkMPHead
            // 
            resources.ApplyResources(this.checkMPHead, "checkMPHead");
            this.checkMPHead.BackColor = System.Drawing.Color.Transparent;
            this.checkMPHead.Checked = true;
            this.checkMPHead.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMPHead.Name = "checkMPHead";
            this.checkMPHead.UseVisualStyleBackColor = false;
            this.checkMPHead.CheckedChanged += new System.EventHandler(this.checkMPHead_CheckedChanged);
            // 
            // groupMPHideAll
            // 
            resources.ApplyResources(this.groupMPHideAll, "groupMPHideAll");
            this.groupMPHideAll.Name = "groupMPHideAll";
            this.groupMPHideAll.UseVisualStyleBackColor = true;
            this.groupMPHideAll.Click += new System.EventHandler(this.groupMPHideAll_Click);
            // 
            // groupMPShowAll
            // 
            resources.ApplyResources(this.groupMPShowAll, "groupMPShowAll");
            this.groupMPShowAll.Name = "groupMPShowAll";
            this.groupMPShowAll.UseVisualStyleBackColor = true;
            this.groupMPShowAll.Click += new System.EventHandler(this.groupMPShowAll_Click);
            // 
            // pictureMP
            // 
            resources.ApplyResources(this.pictureMP, "pictureMP");
            this.pictureMP.BackgroundImage = global::TinyHugeTweaks.Properties.Resources.mario_body;
            this.pictureMP.Name = "pictureMP";
            this.pictureMP.TabStop = false;
            // 
            // groupAdvancedTexture
            // 
            resources.ApplyResources(this.groupAdvancedTexture, "groupAdvancedTexture");
            this.groupAdvancedTexture.Controls.Add(this.buttonATRShowAll);
            this.groupAdvancedTexture.Controls.Add(this.buttonATRHideAll);
            this.groupAdvancedTexture.Controls.Add(this.buttonATRShow);
            this.groupAdvancedTexture.Controls.Add(this.buttonATRHide);
            this.groupAdvancedTexture.Controls.Add(this.treeView1);
            this.groupAdvancedTexture.Name = "groupAdvancedTexture";
            this.groupAdvancedTexture.TabStop = false;
            // 
            // buttonATRShowAll
            // 
            resources.ApplyResources(this.buttonATRShowAll, "buttonATRShowAll");
            this.buttonATRShowAll.Name = "buttonATRShowAll";
            this.buttonATRShowAll.UseVisualStyleBackColor = true;
            this.buttonATRShowAll.Click += new System.EventHandler(this.buttonATRShowAll_Click);
            // 
            // buttonATRHideAll
            // 
            resources.ApplyResources(this.buttonATRHideAll, "buttonATRHideAll");
            this.buttonATRHideAll.Name = "buttonATRHideAll";
            this.buttonATRHideAll.UseVisualStyleBackColor = true;
            this.buttonATRHideAll.Click += new System.EventHandler(this.buttonATRHideAll_Click);
            // 
            // buttonATRShow
            // 
            resources.ApplyResources(this.buttonATRShow, "buttonATRShow");
            this.buttonATRShow.Name = "buttonATRShow";
            this.buttonATRShow.UseVisualStyleBackColor = true;
            this.buttonATRShow.Click += new System.EventHandler(this.buttonATRShow_Click);
            // 
            // buttonATRHide
            // 
            resources.ApplyResources(this.buttonATRHide, "buttonATRHide");
            this.buttonATRHide.Name = "buttonATRHide";
            this.buttonATRHide.UseVisualStyleBackColor = true;
            this.buttonATRHide.Click += new System.EventHandler(this.buttonATRHide_Click);
            // 
            // treeView1
            // 
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.ItemHeight = 18;
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            // 
            // mainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupAdvancedTexture);
            this.Controls.Add(this.groupMP);
            this.Controls.Add(this.groupMain);
            this.Controls.Add(this.groupBlackTextures);
            this.Controls.Add(this.groupStarsAppearance);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupStarsAppearance.ResumeLayout(false);
            this.groupStarsAppearance.PerformLayout();
            this.groupBlackTextures.ResumeLayout(false);
            this.groupMain.ResumeLayout(false);
            this.groupMain.PerformLayout();
            this.groupMP.ResumeLayout(false);
            this.groupMP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMP)).EndInit();
            this.groupAdvancedTexture.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupStarsAppearance;
        private System.Windows.Forms.Label lbStarAddresses;
        private System.Windows.Forms.Button btnAppearCollected;
        private System.Windows.Forms.Button btnAppearUncollected;
        private System.Windows.Forms.GroupBox groupBlackTextures;
        private System.Windows.Forms.Button btnFixBlackTextures;
        private System.Windows.Forms.ComboBox comboBlackOutput;
        private System.Windows.Forms.ComboBox comboBlackInput;
        private System.Windows.Forms.Button btnFixCam;
        private System.Windows.Forms.ComboBox comboBoxStar;
        private System.Windows.Forms.Button btnComboStarReload;
        private System.Windows.Forms.CheckBox checkNoHeadRotations;
        private System.Windows.Forms.GroupBox groupMain;
        private System.Windows.Forms.GroupBox groupMP;
        private System.Windows.Forms.PictureBox pictureMP;
        private System.Windows.Forms.Button groupMPHideAll;
        private System.Windows.Forms.Button groupMPShowAll;
        private System.Windows.Forms.CheckBox checkMPHead;
        private System.Windows.Forms.CheckBox checkMPRightHand;
        private System.Windows.Forms.CheckBox checkMPArmR2;
        private System.Windows.Forms.CheckBox checkMPArmR1;
        private System.Windows.Forms.CheckBox checkMPLeftHand;
        private System.Windows.Forms.CheckBox checkMPArmL2;
        private System.Windows.Forms.CheckBox checkMPArmL1;
        private System.Windows.Forms.CheckBox checkMPTorso;
        private System.Windows.Forms.CheckBox checkMPCore;
        private System.Windows.Forms.CheckBox checkMPRightFoot;
        private System.Windows.Forms.CheckBox checkMPLegR2;
        private System.Windows.Forms.CheckBox checkMPLegR1;
        private System.Windows.Forms.CheckBox checkMPLeftFoot;
        private System.Windows.Forms.CheckBox checkMPLegL2;
        private System.Windows.Forms.CheckBox checkMPLegL1;
        private System.Windows.Forms.CheckBox checkNoShadowSimple;
        private System.Windows.Forms.Button btnBlackBars;
        private System.Windows.Forms.GroupBox groupAdvancedTexture;
        private System.Windows.Forms.Button btnSmoke;
        private System.Windows.Forms.ToolStripMenuItem updatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatesrefreshToolStripMenuItem;
        private kataraktaTreeView treeView1;
        private System.Windows.Forms.Button buttonATRShow;
        private System.Windows.Forms.Button buttonATRHide;
        private System.Windows.Forms.Button buttonATRShowAll;
        private System.Windows.Forms.Button buttonATRHideAll;
    }
}

