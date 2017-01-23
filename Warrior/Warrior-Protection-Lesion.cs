// winifix@gmail.com
// ReSharper disable UnusedMember.Global

using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using PixelMagic.Helpers;
using PixelMagic.GUI;

namespace PixelMagic.Rotation
{
    public class ProtectionLesion : CombatRoutine
    {	
	 
		private CheckBox generalint;
        private CheckBox mplusint;
		private CheckBox CDint;
		
		
        public override string Name => "Protection Warrior";

        public override string Class => "Warrior";
		
		private static bool generalInterrupts
        {
            get
            {
                var generalInterrupts = ConfigFile.ReadValue("ProtectionLesion", "generalInterrupts").Trim();

                return generalInterrupts != "" && Convert.ToBoolean(generalInterrupts);
            }
            set { ConfigFile.WriteValue("ProtectionLesion", "generalInterrupts", value.ToString()); }
        }

        private static bool mythicplusinterrupts
        {
            get
            {
                var mythicplusinterrupts = ConfigFile.ReadValue("ProtectionLesion", "mythicplusinterrupts").Trim();

                return mythicplusinterrupts != "" && Convert.ToBoolean(mythicplusinterrupts);
            }
            set { ConfigFile.WriteValue("ProtectionLesion", "mythicplusinterrupts", value.ToString()); }
        }
		
		private static bool cooldowns
        {
            get
            {
                var cooldowns = ConfigFile.ReadValue("ProtectionLesion", "cooldowns").Trim();

                return cooldowns != "" && Convert.ToBoolean(cooldowns);
            }
            set { ConfigFile.WriteValue("ProtectionLesion", "cooldowns", value.ToString()); }
        }
	
		private readonly Stopwatch swingwatch = new Stopwatch();

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Protection Warrior", Color.Green);
            Log.Write("Suggested build: 1213112", Color.Green);
			Log.Write("3.0", Color.Green);
			Log.Write("Last Edited by Lesion 22/01/17 - interruption controlz", Color.Green);
            WoW.Speak("Welcome to PixelMagic Protection Warrior by Lesion");
			
			SettingsForm = new Form {Text = "Settings", StartPosition = FormStartPosition.CenterScreen, Width = 150, Height = 150, ShowIcon = false};

            //var picBox = new PictureBox {Left = 0, Top = 0, Width = 800, Height = 100, Image = TopLogo};
            //SettingsForm.Controls.Add(picBox);

            var lblGeneralInterruptsText = new Label //12; 114 LEFT is first value, Top is second.
            {
                Text = "Interrupt all Spells",
                Size = new Size(81, 13), //81; 13
                Left = 12,
                Top = 14
            };
            SettingsForm.Controls.Add(lblGeneralInterruptsText); //113; 114 

            generalint = new CheckBox {Checked = generalInterrupts, TabIndex = 2, Size = new Size(15, 14), Left = 115, Top = 14};
            SettingsForm.Controls.Add(generalint);

            var lblMythicPlusText = new Label //12; 129 is first value, Top is second.
            {
                Text = "Mythic Plus Interrupts",
                Size = new Size(95, 13), //95; 13
                Left = 12,
                Top = 29
            };
            SettingsForm.Controls.Add(lblMythicPlusText); //113; 114 

            mplusint = new CheckBox {Checked = mythicplusinterrupts, TabIndex = 4, Size = new Size(15, 14), Left = 115, Top = 29};
            SettingsForm.Controls.Add(mplusint);
			
			var lblcooldownsText = new Label //12; 129 is first value, Top is second.
            {
                Text = "Def Cooldowns used automatically SW/LS/SR",
                Size = new Size(95, 13), //95; 13
                Left = 12,
                Top = 44
            };
            SettingsForm.Controls.Add(lblcooldownsText); //113; 114 

            CDint = new CheckBox {Checked = cooldowns, TabIndex = 5, Size = new Size(15, 14), Left = 115, Top = 44};
            SettingsForm.Controls.Add(CDint);
			 
            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 15, Top = 60, Size = new Size(108, 48)};

            generalint.Checked = generalInterrupts;
            mplusint.Checked = mythicplusinterrupts;
			CDint.Checked = cooldowns;
            

            cmdSave.Click += CmdSave_Click;
            generalint.CheckedChanged += GI_Click;
            mplusint.CheckedChanged += MP_Click;
            CDint.CheckedChanged += CD_Click;

            SettingsForm.Controls.Add(cmdSave);
            lblGeneralInterruptsText.BringToFront();
            lblMythicPlusText.BringToFront();
			lblcooldownsText.BringToFront();
            

            Log.Write("Interupt all = " + generalInterrupts);
            Log.Write("Mythic Plus = " + mythicplusinterrupts);
			Log.Write("Cooldowns being used = " + cooldowns);
			
        }
		private void CmdSave_Click(object sender, EventArgs e)
        {
            generalInterrupts = generalint.Checked;
            mythicplusinterrupts = mplusint.Checked;
			cooldowns = CDint.Checked;
			
            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void GI_Click(object sender, EventArgs e)
        {
            generalInterrupts = generalint.Checked;
        }

        private void MP_Click(object sender, EventArgs e)
        {
            mythicplusinterrupts = mplusint.Checked;
        }
		
		private void CD_Click(object sender, EventArgs e)
        {
            mythicplusinterrupts = CDint.Checked;
        }
		
		
        public override void Stop()
        {
        }
			
        public override void Pulse()
		{
			
			if (cooldowns)
			{
			if (WoW.IsInCombat && WoW.HealthPercent < 35 && WoW.CanCast("Last Stand") && !WoW.IsSpellOnCooldown("Last Stand"))
            {
                WoW.CastSpell("Last Stand");
                return;
            }
            if (WoW.IsInCombat && WoW.HealthPercent < 20 && WoW.CanCast("Shield Wall") && !WoW.IsSpellOnCooldown("Shield Wall"))
            {
                WoW.CastSpell("Shield Wall");
                return;
            }
			if (WoW.TargetIsCasting && WoW.CanCast("SpellReflect") && !WoW.IsSpellOnCooldown("SpellReflect"))
                        {
                            WoW.CastSpell("SpellReflect");
                        }
			}
			
			if (WoW.IsInCombat &&WoW.IsSpellInRange("Shield Slam"))
            {
                swingwatch.Start();
            }
			
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
							
				
                if (WoW.HasTarget && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Shield Slam"))
                {
					if (generalInterrupts)
					{
					if (WoW.CanCast("Pummel")&&WoW.TargetIsCasting &&WoW.TargetIsCastingAndSpellIsInterruptible &&WoW.TargetPercentCast >= 80&& !WoW.IsSpellOnCooldown("Pummel"))
                        {
						WoW.CastSpell("Pummel");
							return;	
						}
							
					}
					
					if (mythicplusinterrupts)
					{
                    	if (WoW.CanCast("Pummel")&&WoW.TargetIsCasting &&WoW.TargetIsCastingAndSpellIsInterruptible &&WoW.TargetPercentCast >= 40&& !WoW.IsSpellOnCooldown("Pummel"))
                        {
//int spell list for all important spells in M+                        
if ( WoW.TargetCastingSpellID == 200248
//Court Of Stars Mythic+ Interrupt list
|| WoW.TargetCastingSpellID == 225573
|| WoW.TargetCastingSpellID == 208165
|| WoW.TargetCastingSpellID == 211401
|| WoW.TargetCastingSpellID == 21147
|| WoW.TargetCastingSpellID == 211299
|| WoW.TargetCastingSpellID == 2251
|| WoW.TargetCastingSpellID == 209413
|| WoW.TargetCastingSpellID == 209404
|| WoW.TargetCastingSpellID == 215204
|| WoW.TargetCastingSpellID == 210261
//Darkheart Thicket Mythic+ Interrupt list
|| WoW.TargetCastingSpellID == 200658
|| WoW.TargetCastingSpellID == 200631
|| WoW.TargetCastingSpellID == 204246
|| WoW.TargetCastingSpellID == 2014
//Eye of Azshara Mythic+ Interrupt list
|| WoW.TargetCastingSpellID == 19687
|| WoW.TargetCastingSpellID == 218532
|| WoW.TargetCastingSpellID == 195129
|| WoW.TargetCastingSpellID == 195046
|| WoW.TargetCastingSpellID == 197502
|| WoW.TargetCastingSpellID == 196027
|| WoW.TargetCastingSpellID == 196175
|| WoW.TargetCastingSpellID == 192003
|| WoW.TargetCastingSpellID == 191848
//Halls of Valor Mythic+ Interrupt list
|| WoW.TargetCastingSpellID == 198595
|| WoW.TargetCastingSpellID == 198962
|| WoW.TargetCastingSpellID == 198931
|| WoW.TargetCastingSpellID == 192563
|| WoW.TargetCastingSpellID == 192288
|| WoW.TargetCastingSpellID == 199726
//Maw of Souls Mythic+ Interrupt list
|| WoW.TargetCastingSpellID == 198495
|| WoW.TargetCastingSpellID == 195293
|| WoW.TargetCastingSpellID == 199589
|| WoW.TargetCastingSpellID == 194266
|| WoW.TargetCastingSpellID == 198405
|| WoW.TargetCastingSpellID == 199514
|| WoW.TargetCastingSpellID == 194657
//Neltharions Lair Mythic+ Interrupt list
|| WoW.TargetCastingSpellID == 193585
|| WoW.TargetCastingSpellID == 202181
//The Arcway Mythic+ Interrupt list
|| WoW.TargetCastingSpellID == 226269
|| WoW.TargetCastingSpellID == 211007 
|| WoW.TargetCastingSpellID == 211757 
|| WoW.TargetCastingSpellID == 226285 
|| WoW.TargetCastingSpellID == 226206 
|| WoW.TargetCastingSpellID == 211115 
|| WoW.TargetCastingSpellID == 196392 
// Advisor Vandros (Interrupt manually) Spell,203176,Accelerating Blast
|| WoW.TargetCastingSpellID == 203957 
//Vault of the Wardens Mythic+ Interrupt list
|| WoW.TargetCastingSpellID == 193069
|| WoW.TargetCastingSpellID == 191823
|| WoW.TargetCastingSpellID == 202661
|| WoW.TargetCastingSpellID == 201488
|| WoW.TargetCastingSpellID == 195332)
								
							{
							WoW.CastSpell("Pummel");
							return;
							}
						}
					}
						
						if (WoW.CanCast("Shield Block") &&WoW.HealthPercent <= 90 && WoW.Rage >= 15 && !WoW.IsSpellOnCooldown("Shield Block") &&!WoW.PlayerHasBuff("Shield Block"))
                        {
                            WoW.CastSpell("Shield Block");
                            return;
                        }
						
						if (WoW.CanCast("Shield Block") &&WoW.Rage >=15 && WoW.PlayerBuffTimeRemaining("Shield Block") <= 2)
						{
                            WoW.CastSpell("Shield Block");
                            return;
                        }
						
						
                        // IP Control
						if (WoW.CanCast("Ignore Pain") && (WoW.Rage >= 50) && !WoW.PlayerHasBuff("Vengeance Ignore Pain"))
                        {
                            WoW.CastSpell("Ignore Pain");
                            return;
                        }
                        if (WoW.CanCast("Ignore Pain") && (WoW.Rage >= 13) && WoW.PlayerHasBuff("Vengeance Ignore Pain"))
                        {
                            WoW.CastSpell("Ignore Pain");
						}
						
						// Revenge Control
                        if (WoW.CanCast("Revenge") && WoW.Rage >= 45 && !WoW.PlayerHasBuff("Vengeance Revenge"))
                        {
                            WoW.CastSpell("Revenge");
                        }
						if (WoW.CanCast("Revenge") && WoW.IsSpellOverlayed("Revenge") && WoW.PlayerHasBuff("Vengeance Revenge"))
						{
							WoW.CastSpell("Revenge");
						}
						if (WoW.CanCast("Revenge") && WoW.Rage >= 34 && WoW.PlayerHasBuff("Vengeance Revenge") &&!WoW.IsSpellOnCooldown("Revenge"))
						{
							WoW.CastSpell("Revenge");
						}
						
						//Rotational shiz
						//It will wait 2.7 seconds for a devastate too proc shield slam. (bastardized swing timer)
						if (!WoW.IsSpellOnCooldown("Shield Slam") &&swingwatch.ElapsedMilliseconds > 2700)
					{
						if (!WoW.IsSpellOverlayed("Shield Slam"))
						{
							WoW.CastSpell("Shield Slam");
							swingwatch.Reset();
							swingwatch.Start();
							return;
										
						}		
					}
						//will cast SS when proc's
						if (WoW.CanCast("Shield Slam") && WoW.IsSpellOverlayed("Shield Slam"))
                        {
                            WoW.CastSpell("Shield Slam");
							return;
                        }
					
						//if (WoW.CanCast("Devastate") && WoW.IsSpellOnCooldown("Revenge"))
                        //{
                        //   WoW.CastSpell("Devastate");
                        //    return;
                        //}
                        if (WoW.CanCast("Thunder Clap") && !WoW.IsSpellOnCooldown("Thunder Clap"))
                        {
                            WoW.CastSpell("Thunder Clap");
                            return;
                        }
                        
						//if (WoW.CanCast("Impending Victory") && WoW.Rage >= 10 && !WoW.IsSpellOnCooldown("Impending Victory") && WoW.HealthPercent <= 80)
						//{
						//	WoW.CastSpell("Impending Victory");
						//}
															
                      
                        
                    }
					//Artifact / Shockwave Combo.
                    if (WoW.CanCast("Neltharion's Fury") && WoW.TargetHasDebuff("ShockWavestun"))
                    {
                        WoW.CastSpell("Neltharion's Fury");
                        return;
                    }
                
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE Stuff here
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
Spell,23922,Shield Slam,D1
Spell,20243,Devastate,D2
Spell,6572,Revenge,D3
Spell,204488,Focused Rage,F4
Spell,6343,Thunder Clap,F9
Spell,2565,Shield Block,D4
Spell,190456,Ignore Pain,D5
Spell,203526,Neltharion's Fury,F8
Spell,6552,Pummel,F3
Spell,34428,Victory Rush,D9
Spell,46968,Shockwave,None
Spell,202168,Impending Victory,D9
Spell,871,Shield Wall,F6
Spell,12975,Last Stand,F5
Spell,23920,SpellReflect,D0
Aura,2565,Shield Block
Aura,132168,ShockWavestun
Aura,122510,Ultimatum
Aura,202573,Vengeance Revenge
Aura,202574,Vengeance Ignore Pain
Aura,190456,Ignore Pain
Aura,203576,Dragon Scales
*/
