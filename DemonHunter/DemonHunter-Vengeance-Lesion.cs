// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class DHVengLesion : CombatRoutine
    {
		
		//t1
		private CheckBox AbyssalStrikeBox;
		private CheckBox RazorSpikeBox;
		//t2
		private CheckBox FeastofSoulsBox;
		private CheckBox FalloutBox;
		private CheckBox BurningAliveBox;
		//t3
		private CheckBox FelbladeBox;
		private CheckBox FlameCrashBox;
		private CheckBox FelErruptionBox;
		//t4
		private CheckBox FeedtheDemonBox;
		private CheckBox FractureBox;
		
		//t5
		private CheckBox SigilOfChainsBox;
		
		//t6
		private CheckBox FelDevastationBox;
		
		//t7
		private CheckBox LastResortBox;
		private CheckBox DemonicInfusionBox;
		private CheckBox SoulBarrierBox;
		
		private CheckBox Preset1Box;
		private CheckBox Preset2Box;
		private CheckBox Preset3Box;
		private CheckBox Preset4Box;
		private CheckBox Preset5Box;
		private CheckBox Preset6Box;
		
		
		
		
		public static bool AbyssalStrike
        {
            get
            {
                var AbyssalStrike = ConfigFile.ReadValue("DHVengLesion", "AbyssalStrike").Trim();
                return AbyssalStrike != "" && Convert.ToBoolean(AbyssalStrike);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "AbyssalStrike", value.ToString()); }
        }
		public static bool RazorSpike
        {
            get
            {
                var RazorSpike = ConfigFile.ReadValue("DHVengLesion", "RazorSpike").Trim();
                return RazorSpike != "" && Convert.ToBoolean(RazorSpike);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "RazorSpike", value.ToString()); }
        }
		
		public static bool FeastofSouls
        {
            get
            {
                var FeastofSouls = ConfigFile.ReadValue("DHVengLesion", "FeastofSouls").Trim();
                return FeastofSouls != "" && Convert.ToBoolean(FeastofSouls);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "FeastofSouls", value.ToString()); }
        }
		public static bool Fallout
        {
            get
            {
                var Fallout = ConfigFile.ReadValue("DHVengLesion", "Fallout").Trim();
                return Fallout != "" && Convert.ToBoolean(Fallout);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Fallout", value.ToString()); }
        }
		public static bool BurningAlive
        {
            get
            {
                var BurningAlive = ConfigFile.ReadValue("DHVengLesion", "BurningAlive").Trim();
                return BurningAlive != "" && Convert.ToBoolean(BurningAlive);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "BurningAlive", value.ToString()); }
        }
		public static bool Felblade
        {
            get
            {
                var Felblade = ConfigFile.ReadValue("DHVengLesion", "Felblade").Trim();
                return Felblade != "" && Convert.ToBoolean(Felblade);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Felblade", value.ToString()); }
        }
		public static bool FlameCrash
        {
            get
            {
                var FlameCrash = ConfigFile.ReadValue("DHVengLesion", "FlameCrash").Trim();
                return FlameCrash != "" && Convert.ToBoolean(FlameCrash);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "FlameCrash", value.ToString()); }
        }
		public static bool FelErruption
        {
            get
            {
                var FelErruption = ConfigFile.ReadValue("DHVengLesion", "FelErruption").Trim();
                return FelErruption != "" && Convert.ToBoolean(FelErruption);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "FelErruption", value.ToString()); }
        }
		public static bool FeedtheDemon
        {
            get
            {
                var FeedtheDemon = ConfigFile.ReadValue("DHVengLesion", "FeedtheDemon").Trim();
                return FeedtheDemon != "" && Convert.ToBoolean(FeedtheDemon);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "FeedtheDemon", value.ToString()); }
        }
		public static bool Fracture
        {
            get
            {
                var Fracture = ConfigFile.ReadValue("DHVengLesion", "Fracture").Trim();
                return Fracture != "" && Convert.ToBoolean(Fracture);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Fracture", value.ToString()); }
        }
		public static bool SigilOfChains
        {
            get
            {
                var SigilOfChains = ConfigFile.ReadValue("DHVengLesion", "SigilOfChains").Trim();
                return SigilOfChains != "" && Convert.ToBoolean(SigilOfChains);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "SigilOfChains", value.ToString()); }
        }
		public static bool FelDevastation
        {
            get
            {
                var FelDevastation = ConfigFile.ReadValue("DHVengLesion", "FelDevastation").Trim();
                return FelDevastation != "" && Convert.ToBoolean(FelDevastation);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "FelDevastation", value.ToString()); }
        }
		public static bool LastResort
        {
            get
            {
                var LastResort = ConfigFile.ReadValue("DHVengLesion", "LastResort").Trim();
                return LastResort != "" && Convert.ToBoolean(LastResort);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "LastResort", value.ToString()); }
        }
		public static bool DemonicInfusion
        {
            get
            {
                var DemonicInfusion = ConfigFile.ReadValue("DHVengLesion", "DemonicInfusion").Trim();
                return DemonicInfusion != "" && Convert.ToBoolean(DemonicInfusion);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "DemonicInfusion", value.ToString()); }
        }
		public static bool SoulBarrier
        {
            get
            {
                var SoulBarrier = ConfigFile.ReadValue("DHVengLesion", "SoulBarrier").Trim();
                return SoulBarrier != "" && Convert.ToBoolean(SoulBarrier);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "SoulBarrier", value.ToString()); }
        }
		
		public static bool Preset1
        {
            get
            {
                var Preset1 = ConfigFile.ReadValue("DHVengLesion", "Preset1").Trim();
                return Preset1 != "" && Convert.ToBoolean(Preset1);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Preset1", value.ToString()); }
        }
		public static bool Preset2
        {
            get
            {
                var Preset2 = ConfigFile.ReadValue("DHVengLesion", "Preset2").Trim();
                return Preset2 != "" && Convert.ToBoolean(Preset2);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Preset2", value.ToString()); }
        }
		public static bool Preset3
        {
            get
            {
                var Preset3 = ConfigFile.ReadValue("DHVengLesion", "Preset3").Trim();
                return Preset3 != "" && Convert.ToBoolean(Preset3);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Preset3", value.ToString()); }
        }
		public static bool Preset4
        {
            get
            {
                var Preset4 = ConfigFile.ReadValue("DHVengLesion", "Preset4").Trim();
                return Preset4 != "" && Convert.ToBoolean(Preset4);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Preset4", value.ToString()); }
        }
		public static bool Preset5
        {
            get
            {
                var Preset5 = ConfigFile.ReadValue("DHVengLesion", "Preset5").Trim();
                return Preset5 != "" && Convert.ToBoolean(Preset5);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Preset5", value.ToString()); }
        }
		public static bool Preset6
        {
            get
            {
                var Preset5 = ConfigFile.ReadValue("DHVengLesion", "Preset5").Trim();
                return Preset5 != "" && Convert.ToBoolean(Preset5);
            }
            set { ConfigFile.WriteValue("DHVengLesion", "Preset5", value.ToString()); }
        }
		
		
		
        
       
		public override string Name
        {
            get { return "DH Vengeance"; }
        }

        
		 public override string Class
        {
            get { return "Demon Hunter"; }
        }

        public override Form SettingsForm { get; set; }
		
		
        public override void Initialize()
        {
            
            SettingsForm = new Form
            {
                Text = "Vengeance DemonHunter",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 800,
                Height = 500,
                ShowIcon = false
            };

            
			
			var lblTitle = new Label
            {
                Text =
                    "Welcome to Lesion's Veng DemonHunter CR",
                Size = new Size(270, 13),
                Left = 61,
                Top = 1
	       };
			lblTitle.ForeColor = Color.Black;
			Font myFont = new Font(lblTitle.Font,FontStyle.Bold|FontStyle.Underline);
			lblTitle.Font = myFont;
            SettingsForm.Controls.Add(lblTitle);
			
			var lblTextBox = new Label
            {
                Text =
                    "Please Select your Talent choices",
                Size = new Size(270, 13),
                Left = 100,
                Top = 17
            };
			lblTextBox.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox);
			
			var lblTextBox2 = new Label
            {
                Text =
                    "Presets based on Wowhead Guide",
                Size = new Size(200, 17),
                Left = 100,
                Top = 50
            };
			lblTextBox2.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox2);
			
			var lblPreset1Box = new Label
            {
                Text =
                    "Basic Raid",
                Size = new Size(270, 15),
                Left = 100,
                Top = 75
            };
			lblPreset1Box.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblPreset1Box);
			
			var lblPreset2Box = new Label
            {
                Text =
                    "Basic M+ Dungeon",
                Size = new Size(270, 15),
                Left = 100,
                Top = 100
            };
			lblPreset2Box.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblPreset2Box);
			
			var lblPreset3Box = new Label
            {
                Text =
                    "Advanced Raid",
                Size = new Size(270, 15),
                Left = 100,
                Top = 125
            };
			lblPreset3Box.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblPreset3Box);
			
			var lblPreset4Box = new Label
            {
                Text =
                    "Advanced M+ Dungeon",
                Size = new Size(270, 15),
                Left = 100,
                Top = 150
            };
			lblPreset4Box.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblPreset4Box);
			
			var lblPreset5Box = new Label
            {
                Text =
                    "ST/Raid DPS",
                Size = new Size(270, 15),
                Left = 100,
                Top = 175
            };
			lblPreset5Box.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblPreset5Box);
			
			var lblPreset6Box = new Label
            {
                Text =
                    "AoE/M+ Dps",
                Size = new Size(270, 15),
                Left = 100,
                Top = 200
            };
			lblPreset6Box.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblPreset6Box);
			
           
			var lblDiscordBox = new Label
            {
                Text =
                    "Please report any issues on #DemonHunter",
                Size = new Size(270, 250),
                Left = 560,
                Top = 445
            };
			lblDiscordBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblDiscordBox);
			
			var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 5, Top = 425, Size = new Size(120, 31)};
			
			var cmdReadme = new Button {Text = "Macros! Use Them", Width = 65, Height = 25, Left = 125, Top = 425, Size = new Size(120, 31)};
			// Checkboxes
			//T1 Checkboxes
			AbyssalStrikeBox = new CheckBox {Checked = AbyssalStrike, TabIndex = 8, Size = new Size(13, 13), Left = 415, Top = 63};
            SettingsForm.Controls.Add(AbyssalStrikeBox);
			RazorSpikeBox = new CheckBox {Checked = RazorSpike, TabIndex = 8, Size = new Size(13, 13), Left = 675, Top = 63};
            SettingsForm.Controls.Add(RazorSpikeBox);
			//T2 Checkboxes
			FeastofSoulsBox = new CheckBox {Checked = FeastofSouls, TabIndex = 8, Size = new Size(13, 13), Left = 415, Top = 96};
            SettingsForm.Controls.Add(FeastofSoulsBox);
			FalloutBox = new CheckBox {Checked = Fallout, TabIndex = 8, Size = new Size(13, 13), Left = 545, Top = 96};
            SettingsForm.Controls.Add(FalloutBox);
			BurningAliveBox = new CheckBox {Checked = BurningAlive, TabIndex = 8, Size = new Size(13, 13), Left = 675, Top = 96};
            SettingsForm.Controls.Add(BurningAliveBox);
			//T3
			FelbladeBox = new CheckBox {Checked = Felblade, TabIndex = 8, Size = new Size(13, 13), Left = 415, Top = 138};
            SettingsForm.Controls.Add(FelbladeBox);
			FlameCrashBox = new CheckBox {Checked = FlameCrash, TabIndex = 8, Size = new Size(13, 13), Left = 545, Top = 138};
            SettingsForm.Controls.Add(FlameCrashBox);
			FelErruptionBox = new CheckBox {Checked = FelErruption, TabIndex = 8, Size = new Size(13, 13), Left = 675, Top = 138};
            SettingsForm.Controls.Add(FelErruptionBox);
			//T4
			FeedtheDemonBox = new CheckBox {Checked = FeedtheDemon, TabIndex = 8, Size = new Size(13, 13), Left = 415, Top = 175};
            SettingsForm.Controls.Add(FeedtheDemonBox);
			FractureBox = new CheckBox {Checked = Fracture, TabIndex = 8, Size = new Size(13, 13), Left = 545, Top = 175};
            SettingsForm.Controls.Add(FractureBox);
			//T5
			SigilOfChainsBox = new CheckBox {Checked = SigilOfChains, TabIndex = 8, Size = new Size(13, 13), Left = 545, Top = 210};
            SettingsForm.Controls.Add(SigilOfChainsBox);
			//T6
			FelDevastationBox = new CheckBox {Checked = FelDevastation, TabIndex = 8, Size = new Size(13, 13), Left = 415, Top = 245};
            SettingsForm.Controls.Add(FelDevastationBox);
			//T7
			LastResortBox = new CheckBox {Checked = LastResort, TabIndex = 8, Size = new Size(13, 13), Left = 415, Top = 285};
            SettingsForm.Controls.Add(LastResortBox);
			DemonicInfusionBox = new CheckBox {Checked = DemonicInfusion, TabIndex = 8, Size = new Size(13, 13), Left = 545, Top = 285};
            SettingsForm.Controls.Add(DemonicInfusionBox);
			SoulBarrierBox = new CheckBox {Checked = SoulBarrier, TabIndex = 8, Size = new Size(13, 13), Left = 675, Top = 285};
            SettingsForm.Controls.Add(SoulBarrierBox);
			
			Preset1Box = new CheckBox {Checked = Preset1, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 75};
            SettingsForm.Controls.Add(Preset1Box);
			Preset2Box = new CheckBox {Checked = Preset1, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 100};
            SettingsForm.Controls.Add(Preset2Box);
			Preset3Box = new CheckBox {Checked = Preset1, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 125};
            SettingsForm.Controls.Add(Preset3Box);
			Preset4Box = new CheckBox {Checked = Preset1, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 150};
            SettingsForm.Controls.Add(Preset4Box);
			Preset5Box = new CheckBox {Checked = Preset1, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 175};
            SettingsForm.Controls.Add(Preset5Box);
			Preset6Box = new CheckBox {Checked = Preset1, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 200};
            SettingsForm.Controls.Add(Preset6Box);
			
			
			// Box Check
            AbyssalStrikeBox.Checked = AbyssalStrike;
			RazorSpikeBox.Checked = RazorSpike;
			
			FeastofSoulsBox.Checked = FeastofSouls;
			FalloutBox.Checked = Fallout;
			BurningAliveBox.Checked = BurningAlive;
			
			FelbladeBox.Checked = Felblade;
			FlameCrashBox.Checked = FlameCrash;
			FelErruptionBox.Checked = FelErruption;
			
			FeedtheDemonBox.Checked = FeedtheDemon;
			FractureBox.Checked = Fracture;
			
			SigilOfChainsBox.Checked = SigilOfChains;
			
			FelDevastationBox.Checked = FelDevastation;
			
			LastResortBox.Checked = LastResort;
			DemonicInfusionBox.Checked = DemonicInfusion;
			SoulBarrierBox.Checked = SoulBarrier;
			
			
			Preset1Box.Checked = Preset1;
			Preset2Box.Checked = Preset2;
			Preset3Box.Checked = Preset3;
			Preset4Box.Checked = Preset4;
			Preset5Box.Checked = Preset5;
			Preset6Box.Checked = Preset6;
		
			
			//cmdSave
            cmdSave.Click += CmdSave_Click;
			cmdReadme.Click += CmdReadme_Click;
            AbyssalStrikeBox.CheckedChanged += AbyssalStrike_Click;
			RazorSpikeBox.CheckedChanged += RazorSpike_Click;
			FeastofSoulsBox.CheckedChanged += FeastofSouls_Click;
			FalloutBox.CheckedChanged += Fallout_Click;;
			BurningAliveBox.CheckedChanged += BurningAlive_Click;
			FelbladeBox.CheckedChanged += Felblade_Click;
			FlameCrashBox.CheckedChanged += FlameCrash_Click;
			FelErruptionBox.CheckedChanged += FelErruption_Click;
			FeedtheDemonBox.CheckedChanged += FeedtheDemon_Click;
			FractureBox.CheckedChanged += Fracture_Click;
			SigilOfChainsBox.CheckedChanged += SigilOfChains_Click;
			FelDevastationBox.CheckedChanged += FelDevastation_Click;
			LastResortBox.CheckedChanged += LastResort_Click;
			DemonicInfusionBox.CheckedChanged += DemonicInfusion_Click;
			SoulBarrierBox.CheckedChanged += SoulBarrier_Click;
			Preset1Box.CheckedChanged += Preset1_Click;
			Preset2Box.CheckedChanged += Preset2_Click;
			Preset3Box.CheckedChanged += Preset3_Click;
			Preset4Box.CheckedChanged += Preset4_Click;
			Preset5Box.CheckedChanged += Preset5_Click;
			Preset6Box.CheckedChanged += Preset6_Click;
			
			SettingsForm.Controls.Add(cmdSave);
			SettingsForm.Controls.Add(cmdReadme);
			lblDiscordBox.BringToFront();
			lblTextBox.BringToFront();
			lblTextBox2.BringToFront();
			lblTitle.BringToFront();
			AbyssalStrikeBox.BringToFront();
			RazorSpikeBox.BringToFront();
			FeastofSoulsBox.BringToFront();
			FalloutBox.BringToFront();
			BurningAliveBox.BringToFront();
			FelbladeBox.BringToFront();
			FlameCrashBox.BringToFront();
			FelErruptionBox.BringToFront();
			FeedtheDemonBox.BringToFront();
			FractureBox.BringToFront();
			SigilOfChainsBox.BringToFront();
			FelDevastationBox.BringToFront();
			LastResortBox.BringToFront();
			DemonicInfusionBox.BringToFront();
			SoulBarrierBox.BringToFront();
			Preset1Box.BringToFront();
			Preset2Box.BringToFront();
			Preset3Box.BringToFront();
			Preset4Box.BringToFront();
			Preset5Box.BringToFront();
			Preset6Box.BringToFront();
			
			
		}
			
			private void CmdSave_Click(object sender, EventArgs e)
        {
            AbyssalStrike = AbyssalStrikeBox.Checked;
			RazorSpike = RazorSpikeBox.Checked;
			FeastofSouls = FeastofSoulsBox.Checked;
			Fallout = FalloutBox.Checked;
			BurningAlive = BurningAliveBox.Checked;
			Felblade = FelbladeBox.Checked;
			FlameCrash = FlameCrashBox.Checked;
			FelErruption = FelErruptionBox.Checked;
			FeedtheDemon = FeedtheDemonBox.Checked;
			Fracture = FractureBox.Checked;
			SigilOfChains = SigilOfChainsBox.Checked;
			FelDevastation = FelDevastationBox.Checked;
			LastResort = LastResortBox.Checked;
			DemonicInfusion = DemonicInfusionBox.Checked;
			SoulBarrier = SoulBarrierBox.Checked;
			Preset1 = Preset1Box.Checked;
			Preset2 = Preset2Box.Checked;
			Preset3 = Preset3Box.Checked;
			Preset4 = Preset4Box.Checked;
			Preset5 = Preset5Box.Checked;
			Preset6 = Preset6Box.Checked;
			
			
			
			
            MessageBox.Show("Settings saved.", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }
		private void CmdReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Macros:\n \n \n Please Use the following Macros: \n \n Infernal Strike Macro \n \n #showtooltip \n /cast [mod:shift,target=player][nomod,target=player][mod:ctrl,target=player] Infernal Strike \n /cast [mod:alt] Infernal Strike \n /script UIErrorsFrame:Clear() \n \n Sigil of Flame Macro \n \n #showtooltip \n /cast [mod:shift,target=player][nomod,target=player][mod:ctrl,target=player] Sigil of Flame \n /cast [mod:alt] Sigil of Flame \n /script UIErrorsFrame:Clear() ",
                "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
			// Checkboxes
			//T1 Clicky deealz
        private void AbyssalStrike_Click(object sender, EventArgs e)
        {
            AbyssalStrike = AbyssalStrikeBox.Checked;
			RazorSpikeBox.Checked = false;
        }
		private void RazorSpike_Click(object sender, EventArgs e)
        {
            RazorSpike = RazorSpikeBox.Checked;
			AbyssalStrikeBox.Checked = false;
        }
		//T2 Clicky Shiz
		private void FeastofSouls_Click(object sender, EventArgs e)
        {
            FeastofSouls = FeastofSoulsBox.Checked;
			FalloutBox.Checked = false;
			BurningAliveBox.Checked = false;
        }
		private void Fallout_Click(object sender, EventArgs e)
        {
            Fallout = FalloutBox.Checked;
			FeastofSoulsBox.Checked = false;
			BurningAliveBox.Checked = false;
        }
		private void BurningAlive_Click(object sender, EventArgs e)
        {
            BurningAlive = BurningAliveBox.Checked;
			FeastofSoulsBox.Checked = false;
			FalloutBox.Checked = false;
        }	
		//T3
		private void Felblade_Click(object sender, EventArgs e)
        {
            Felblade = FelbladeBox.Checked;
			FlameCrashBox.Checked = false;
			FelErruptionBox.Checked = false;
        }
		private void FlameCrash_Click(object sender, EventArgs e)
        {
            FlameCrash = FlameCrashBox.Checked;
			FelbladeBox.Checked = false;
			FelErruptionBox.Checked = false;
        }
		private void FelErruption_Click(object sender, EventArgs e)
        {
            FelErruption = FelErruptionBox.Checked;
			FelbladeBox.Checked = false;
			FlameCrashBox.Checked = false;
        }	
		//T4
		private void FeedtheDemon_Click(object sender, EventArgs e)
        {
            FeedtheDemon = FeedtheDemonBox.Checked;
			FractureBox.Checked = false;
        }
		private void Fracture_Click(object sender, EventArgs e)
        {
            Fracture = FractureBox.Checked;
			FeedtheDemonBox.Checked = false;
			
        }	
		//T5
		private void SigilOfChains_Click(object sender, EventArgs e)
        {
            SigilOfChains = SigilOfChainsBox.Checked;
			
        }
		//T6
		private void FelDevastation_Click(object sender, EventArgs e)
        {
            FelDevastation = FelDevastationBox.Checked;
			
        }
		//T7
        private void LastResort_Click(object sender, EventArgs e)
        {
            LastResort = LastResortBox.Checked;
			DemonicInfusionBox.Checked = false;
			SoulBarrierBox.Checked = false;
        }
		private void DemonicInfusion_Click(object sender, EventArgs e)
        {
            DemonicInfusion = DemonicInfusionBox.Checked;
			LastResortBox.Checked = false;
			SoulBarrierBox.Checked = false;
        }
		private void SoulBarrier_Click(object sender, EventArgs e)
        {
            SoulBarrier = SoulBarrierBox.Checked;
			LastResortBox.Checked = false;
			DemonicInfusionBox.Checked = false;
        }
		//t7
		private void Preset1_Click(object sender, EventArgs e)
        {
            Preset1 = Preset1Box.Checked;
			Preset2Box.Checked = false;
			Preset3Box.Checked = false;
			Preset4Box.Checked = false;
			Preset5Box.Checked = false;
			Preset6Box.Checked = false;
			AbyssalStrikeBox.Checked = false;
			RazorSpikeBox.Checked = true;
			FeastofSoulsBox.Checked= true;
			FalloutBox.Checked = false;
			BurningAliveBox.Checked = false;
			FelbladeBox.Checked = true;
			FlameCrashBox.Checked = false;
			FelErruptionBox.Checked = false;
			FeedtheDemonBox.Checked = true;
			FractureBox.Checked = false;
			SigilOfChainsBox.Checked = true;
			FelDevastationBox.Checked = true;
			LastResortBox.Checked = true;
			DemonicInfusionBox.Checked = false;
			SoulBarrierBox.Checked = false;
			
        }
		private void Preset2_Click(object sender, EventArgs e)
        {
            Preset2 = Preset2Box.Checked;
			Preset1Box.Checked = false;
			Preset3Box.Checked = false;
			Preset4Box.Checked = false;
			Preset5Box.Checked = false;
			Preset6Box.Checked = false;
			AbyssalStrikeBox.Checked = true;
			RazorSpikeBox.Checked = false;
			FeastofSoulsBox.Checked = false;
			FalloutBox.Checked = true;
			BurningAliveBox.Checked = false;
			FelbladeBox.Checked = false;
			FlameCrashBox.Checked = true;
			FelErruptionBox.Checked = false;
			FeedtheDemonBox.Checked = true;
			FractureBox.Checked = false;
			SigilOfChainsBox.Checked = true;
			FelDevastationBox.Checked = true;
			LastResortBox.Checked = false;
			DemonicInfusionBox.Checked= false;
			SoulBarrierBox.Checked = true;
			
        }
		private void Preset3_Click(object sender, EventArgs e)
        {
            Preset3 = Preset3Box.Checked;
			Preset2Box.Checked = false;
			Preset1Box.Checked = false;
			Preset4Box.Checked = false;
			Preset5Box.Checked = false;
			Preset6Box.Checked = false;
			AbyssalStrikeBox.Checked = false;
			RazorSpikeBox.Checked = true;
			FeastofSoulsBox.Checked = false;
			FalloutBox.Checked = false;
			BurningAliveBox.Checked = true;
			FelbladeBox.Checked = false;
			FlameCrashBox.Checked = true;
			FelErruptionBox.Checked = false;
			FeedtheDemonBox.Checked = false;
			FractureBox.Checked = true;
			SigilOfChainsBox.Checked = true;
			FelDevastationBox.Checked = true;
			LastResortBox.Checked = false;
			DemonicInfusionBox.Checked = false;
			SoulBarrierBox.Checked = true;
        }
		private void Preset4_Click(object sender, EventArgs e)
        {
            Preset4 = Preset4Box.Checked;
			Preset2Box.Checked = false;
			Preset3Box.Checked = false;
			Preset1Box.Checked = false;
			Preset5Box.Checked = false;
			Preset6Box.Checked = false;
			AbyssalStrikeBox.Checked = false;
			RazorSpikeBox.Checked = true;
			FeastofSoulsBox.Checked = false;
			FalloutBox.Checked = false;
			BurningAliveBox.Checked = true;
			FelbladeBox.Checked = false;
			FlameCrashBox.Checked = true;
			FelErruptionBox.Checked = false;
			FeedtheDemonBox.Checked = false;
			FractureBox.Checked = true;
			SigilOfChainsBox.Checked = true;
			FelDevastationBox.Checked = true;
			LastResortBox.Checked = true;
			DemonicInfusionBox.Checked = false;
			SoulBarrierBox.Checked = false;
        }
		private void Preset5_Click(object sender, EventArgs e)
        {
            Preset5 = Preset5Box.Checked;
			Preset2Box.Checked = false;
			Preset3Box.Checked = false;
			Preset4Box.Checked = false;
			Preset1Box.Checked = false;
			Preset6Box.Checked = false;
			AbyssalStrikeBox.Checked = false;
			RazorSpikeBox.Checked = true;
			FeastofSoulsBox.Checked = false;
			FalloutBox.Checked = false;
			BurningAliveBox.Checked = true;
			FelbladeBox.Checked = false;
			FlameCrashBox.Checked = false;
			FelErruptionBox.Checked = true;
			FeedtheDemonBox.Checked = false;
			FractureBox.Checked = true;
			SigilOfChainsBox.Checked = true;
			FelDevastationBox.Checked = true;
			LastResortBox.Checked = false;
			DemonicInfusionBox.Checked = true;
			SoulBarrierBox.Checked = false;
        }
		private void Preset6_Click(object sender, EventArgs e)
        {
           Preset6 = Preset6Box.Checked;
			Preset2Box.Checked = false;
			Preset3Box.Checked = false;
			Preset4Box.Checked = false;
			Preset5Box.Checked = false;
			Preset1Box.Checked = false;
			AbyssalStrikeBox.Checked = false;
			RazorSpikeBox.Checked = true;
			FeastofSoulsBox.Checked = false;
			FalloutBox.Checked = false;
			BurningAliveBox.Checked = true;
			FelbladeBox.Checked = false;
			FlameCrashBox.Checked = true;
			FelErruptionBox.Checked = false;
			FeedtheDemonBox.Checked = false;
			FractureBox.Checked = true;
			SigilOfChainsBox.Checked = true;
			FelDevastationBox.Checked = true;
			LastResortBox.Checked = false;
			DemonicInfusionBox.Checked = true;
			SoulBarrierBox.Checked = false;
        }
		
		
        public override void Stop()
        {
			
			
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
				// Defence Cooldowns
				if (WoW.HealthPercent < 30 && !WoW.IsSpellOnCooldown("Metamorphasis") && WoW.IsInCombat)
                    {
                        WoW.CastSpell("Metamorphasis");
                    }
					
				if (WoW.PlayerHasBuff("Metamorphasis") && WoW.CanCast("Shear") && !WoW.CanCast("Soul Carver") && !WoW.CanCast("Immolation Aura") && WoW.PlayerBuffStacks("Soul Fragments") < 5)
					{
						WoW.CastSpell("Shear");
						return;
					}
				if (WoW.PlayerHasBuff("Metamorphasis") && WoW.CanCast("Soul Cleave") && WoW.Pain > 50 && WoW.PlayerBuffStacks("Soul Fragments") > 4  )
					{
						WoW.CastSpell("Soul Cleave");
						return;
					}
				
				
				
				
				
				if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsChanneling && WoW.IsInCombat && WoW.IsSpellInRange("Shear"))
				// Rotation
				{
								
					if (WoW.CanCast("Demon Spikes") && WoW.PlayerSpellCharges("Demon Spikes") > 1 && !WoW.PlayerHasBuff("Demon Spikes") && WoW.Pain >= 70)
					{
						WoW.CastSpell("Demon Spikes");
						return;
					}			
								
					
					if (WoW.CanCast("Soul Cleave") && WoW.Pain > 50 && WoW.HealthPercent < 70 && !WoW.PlayerHasBuff("Demon Spikes"))
					{
						WoW.CastSpell("Soul Cleave");
						return;
					}
					if (!SoulBarrier && WoW.CanCast("Soul Cleave") && WoW.HealthPercent < 70 && WoW.Pain > 50)
					{
						WoW.CastSpell("Soul Cleave");
						return;
					}
					if (AbyssalStrike && FlameCrash && SoulBarrier && WoW.CanCast("Soul Cleave") && WoW.Pain > 80)
					{
						WoW.CastSpell("Soul Cleave");
						return;
					}
					if (SoulBarrier && WoW.CanCast("Soul Cleave") 
						
					&& WoW.HealthPercent < 70 
					&& WoW.Pain > 50 
					&& !WoW.PlayerHasBuff("Demon Spikes"))
					{
						WoW.CastSpell("Soul Cleave");
						return;
					}
					if (WoW.CanCast("Soul Carver") && !WoW.IsSpellOnCooldown("Soul Carver") && !WoW.PlayerHasBuff("Demon Spikes"))
					{
						WoW.CastSpell("Soul Carver");
						return;
					}
					if (SoulBarrier && WoW.CanCast("Soul Barrier") && WoW.Pain >= 10 && WoW.PlayerHasBuff("Soul Fragments") && WoW.PlayerBuffStacks("Soul Fragments") >= 4)
                    {
                        WoW.CastSpell("Soul Barrier");
                        return;
                    }
					
					if (!WoW.IsMoving && WoW.CanCast("Fel Devastation") && WoW.Pain > 30
					&& FelDevastation)
					{
						WoW.CastSpell("Fel Devastation");
						return;
					}
				
					if (WoW.CanCast("Immolation Aura") && !WoW.IsSpellOnCooldown("Immolation Aura"))
					{
						WoW.CastSpell("Immolation Aura");
						return;
					}
					if (Felblade && WoW.CanCast("Felblade") && !WoW.IsSpellOnCooldown("Felblade"))
					{
						WoW.CastSpell("Felblade");
						return;
					}
					if (FelErruption && WoW.CanCast("Fel Eruption") && !WoW.IsSpellOnCooldown("Fel Eruption") && WoW.TargetHasDebuff("Fiery Demise"))
					{
						WoW.CastSpell("Fel Eruption");
						return;
					}
					if (FelErruption && WoW.CanCast("Fel Eruption") && !WoW.IsSpellOnCooldown("Fel Eruption") && WoW.SpellCooldownTimeRemaining("Fiery Brand") > 23 && WoW.TargetHasDebuff("Fiery Demise"))
					{
						WoW.CastSpell("Fel Eruption");
						return;
					}
					if (Fracture && WoW.CanCast("Fracture") && !WoW.IsSpellOnCooldown("Fracture") && WoW.Pain > 60 && WoW.HealthPercent > 70)
					{
						WoW.CastSpell("Fracture");
						return;
					}
					if (Fracture && RazorSpike && WoW.CanCast("Fracture") && !WoW.IsSpellOnCooldown("Fracture") && WoW.Pain > 60 && WoW.HealthPercent > 70)
					{
						WoW.CastSpell("Fracture");
						return;
					}
					
					if (!WoW.IsMoving && FlameCrash && ! AbyssalStrike && WoW.CanCast("Sigil of Flame") && !WoW.IsSpellOnCooldown("Sigil of Flame"))
					{
						WoW.CastSpell("Sigil of Flame");
						return;
					}
					if (!FlameCrash && ! AbyssalStrike && WoW.CanCast("Sigil of Flame") && !WoW.IsSpellOnCooldown("Sigil of Flame"))
					{
						WoW.CastSpell("Sigil of Flame");
						return;
					}
					//If Flame Crash Selected
				if (FlameCrash && AbyssalStrike && !WoW.IsMoving)
				{
				if (WoW.CanCast("Sigil of Flame") && !WoW.IsSpellOnCooldown("Sigil of Flame"))
					{
						WoW.CastSpell("Sigil of Flame");
						return;
					}
				if (WoW.CanCast("Infernal Strike") && !WoW.IsSpellOnCooldown("Infernal Strike") && WoW.TargetHasDebuff("Sigil of Flame") && WoW.TargetDebuffTimeRemaining("Sigil of Flame") <=3)
					{
						WoW.CastSpell("Infernal Strike");
						return;
					}
				if (WoW.CanCast("Infernal Strike") && WoW.PlayerSpellCharges("Infernal Strike") == 1 && !WoW.TargetHasDebuff("Sigil of Flame"))
					{
						WoW.CastSpell("Infernal Strike");
						return;
					}
				
				if (FlameCrash && !AbyssalStrike)
				{
				if (WoW.CanCast("Sigil of Flame") && !WoW.TargetHasDebuff("Sigil of Flame"))
					{
						WoW.CastSpell("Sigil of Flame");
						return;
					}
									
					
				if (WoW.CanCast("Infernal Strike") 
					&& !WoW.IsSpellOnCooldown("Infernal Strike") 
					&& !WoW.WasLastCasted("Infernal Strike") 
					&& WoW.IsSpellOnCooldown("Sigil of Flame") 
					&& WoW.PlayerSpellCharges("Infernal Strike") > 2)
					{
						WoW.CastSpell("Infernal Strike");
						return;
					}
				}
				}
				//Razor Spike Talent Selected
				if (RazorSpike)
				{
					if (WoW.CanCast("Demon Spikes") && WoW.PlayerSpellCharges("Demon Spikes") > 1 && WoW.Pain >=90)
					{
						WoW.CastSpell("Demon Spikes");
						return;
				}
					if (WoW.CanCast("Soul Cleave") && WoW.PlayerHasBuff("Demon Spikes") && WoW.Pain > 80)
					{
						WoW.CastSpell("Soul Cleave");
						return;
					}
					if (WoW.CanCast("Shear") && WoW.PlayerHasBuff("Demon Spikes") && WoW.Pain < 80)
					{
						WoW.CastSpell("Shear");
						return;
					}
					if (WoW.CanCast("Fracture") && !WoW.IsSpellOnCooldown("Fracture") && WoW.Pain > 60 && WoW.HealthPercent > 90)
					{
						WoW.CastSpell("Fracture");
						return;
					}
				}
				
				
				if (WoW.CanCast("Fiery Brand") && !WoW.TargetHasDebuff("Fiery Demise"))
                    {
                        WoW.CastSpell("Fiery Brand");
                    }
				if (!Fracture && WoW.CanCast("Shear") && !WoW.CanCast("Soul Carver") && !WoW.CanCast("Immolation Aura"))
					{
						WoW.CastSpell("Shear");
						
						return;
					}
					if (Fracture && WoW.CanCast("Shear") && !WoW.CanCast("Soul Carver") && !WoW.CanCast("Immolation Aura"))
					{
						WoW.CastSpell("Shear");
						return;
					}
				
                /*
                {
                    

                    if (WoW.CanCast("Throw Glaive") && !WoW.IsSpellInRange("Soul Carver") && WoW.IsSpellInRange("Fel Eruption")) //Need to implement range 30y for Throw Glaive spell 204157
                    {
                        WoW.CastSpell("Throw Glaive");
                        return;
                    }

                    if (!WoW.IsSpellInRange("Soul Carver")) // If we are out of melee range return
                        return;

                    if (WoW.TargetIsCasting && interruptwatch.ElapsedMilliseconds > 1200)
                    {
                        if (!WoW.IsSpellOnCooldown("Sigal of Silence") && WoW.WasLastCasted("Arcane Torrent"))
                        {
                            
                            WoW.CastSpell("Sigal of Silence");
                            interruptwatch.Reset();
                            interruptwatch.Start();
                            return;
                        }

                        if (!WoW.IsSpellOnCooldown("Arcane Torrent") && WoW.WasLastCasted("Sigal of Silence"))
                        {
                            
                            WoW.CastSpell("Arcane Torrent");
                            interruptwatch.Reset();
                            interruptwatch.Start();
                            return;
                        }
                    }

                    
                }*/
            }
			}
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                
                }
            
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Lesion
AddonName=badgggggggggerui
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,203782,Shear,D1
Spell,235964,Sever,D1
Spell,228477,Soul Cleave,D3
Spell,207407,Soul Carver,D5
Spell,178740,Immolation Aura,F2
Spell,204596,Sigil of Flame,D2
Spell,204157,Throw Glaive,None
Spell,207682,Sigal of Silence,D6
Spell,202719,Arcane Torrent,F6
Spell,187827,Metamorphasis,F4
Spell,204021,Fiery Brand,F7
Spell,218679,Spirit Bomb,None
Spell,203720,Demon Spikes,E
Spell,189110,Infernal Strike,F5
Spell,209795,Fracture,F8
Spell,227225,Soul Barrier,F1
Spell,211881,Fel Eruption,F9
Spell,212084,Fel Devastation,F3
Spell,232893,Felblade,D7
Aura,203819,Demon Spikes
Aura,204598,Sigil of Flame

Aura,212818,Fiery Demise
Aura,187827,Metamorphasis
Aura,224509,Frailty
Aura,203981,Soul Fragments
Aura,41252,Mount
Aura,207472,Magnum Opus
Item,80610,Mana
*/
