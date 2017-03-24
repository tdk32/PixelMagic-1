// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

/*
Marksman by Vectarius
rev1 - 7.1.5 Rotation

to-do: 
- improve dps?
- add opener
- add all spec variations
*/


using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class HunterMarksman : CombatRoutine
    {
        private CheckBox BarrageBox;
        private CheckBox CrowBox;
        private CheckBox TrueshotBox;
        private CheckBox TurtleBox;
        private CheckBox DeathBox;
        private CheckBox ExhilBox;
		private CheckBox CounterShotBox;		

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {

            Log.Write("Welcome to Hunter Marksman by Vectarius", Color.Green);
            Log.Write("Please use 1/1/1/3/1/3/2 Talents", Color.Green);			

            SettingsForm = new Form
            {
                Text = "Settings",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 480,
                Height = 300,
                ShowIcon = false
            };



            var lblBarrageText = new Label //Barrage LABEL - 114
            {
                Text = "Barrage",
                Size = new Size(81, 13),
                Left = 12,
                Top = 114
            };
            SettingsForm.Controls.Add(lblBarrageText); //Barrage TEXT - 114

            BarrageBox = new CheckBox
            {
                Checked = Barrage,
                TabIndex = 2,
                Size = new Size(15, 14),
                Left = 115,
                Top = 114
            };
            SettingsForm.Controls.Add(BarrageBox); //Barrage BOX

            var lblTrueshotText = new Label //Trueshot LABEL - 129
            {
                Text = "Trueshot",
                Size = new Size(81, 13),
                Left = 12,
                Top = 129
            };
            SettingsForm.Controls.Add(lblTrueshotText); //Trueshot TEXT - 129

            TrueshotBox = new CheckBox
            {
                Checked = Trueshot,
                TabIndex = 4,
                Size = new Size(15, 14),
                Left = 115,
                Top = 129
            };
            SettingsForm.Controls.Add(TrueshotBox);	//Trueshot BOX		

            var lblCrowText = new Label  // Crow LABEL
            {
                Text = "Crow",
                Size = new Size(81, 13),
                Left = 12,
                Top = 145
            };
            SettingsForm.Controls.Add(lblCrowText); //Crow TEXT

            CrowBox = new CheckBox
            {
                Checked = Crow,
                TabIndex = 6,
                Size = new Size(15, 14),
                Left = 115,
                Top = 145
            };
            SettingsForm.Controls.Add(CrowBox);   // Crow BOX

            var lblExhilText = new Label  // Exhil LABEL
            {
                Text = "Exhil",
                Size = new Size(81, 13),
                Left = 12,
                Top = 161
            };
            SettingsForm.Controls.Add(lblExhilText); //EXHIL TEXT

            ExhilBox = new CheckBox
            {
                Checked = Exhil,
                TabIndex = 8,
                Size = new Size(15, 14),
                Left = 115,
                Top = 161
            };
            SettingsForm.Controls.Add(ExhilBox); //Exhil Box

            var lblTurtleText = new Label //Turtle label
            {
                Text = "Turtle",
                Size = new Size(81, 13),
                Left = 12,
                Top = 178
            };
            SettingsForm.Controls.Add(lblTurtleText);  //turtle text

            TurtleBox = new CheckBox
            {
                Checked = Turtle,
                TabIndex = 10,
                Size = new Size(15, 14),
                Left = 115,
                Top = 178
            };
            SettingsForm.Controls.Add(TurtleBox);   //turtle box

            var lblDeathText = new Label //Death label
            {
                Text = "Death",
                Size = new Size(81, 13),
                Left = 12,
                Top = 194
            };
            SettingsForm.Controls.Add(lblDeathText); //death text

            DeathBox = new CheckBox
            {
                Checked = Death,
                TabIndex = 12,
                Size = new Size(15, 14),
                Left = 115,
                Top = 194
            };
            SettingsForm.Controls.Add(DeathBox);   //death box	
			
            var lblCounterShotText = new Label //CounterShot label
            {
                Text = "CounterShot",
                Size = new Size(81, 13),
                Left = 12,
                Top = 210
            };
            SettingsForm.Controls.Add(lblCounterShotText); //CounterShot text

            CounterShotBox = new CheckBox
            {
                Checked = CounterShot,
                TabIndex = 12,
                Size = new Size(15, 14),
                Left = 115,
                Top = 210
            };
            SettingsForm.Controls.Add(CounterShotBox);   //CounterShot box			

            var cmdSave = new Button
            {
                Text = "Save",
                Width = 65,
                Height = 25,
                Left = 332,
                Top = 190,
                Size = new Size(120, 31)
            };

            BarrageBox.Checked = Barrage;
            CrowBox.Checked = Crow;
            DeathBox.Checked = Death;
            ExhilBox.Checked = Exhil;
            TrueshotBox.Checked = Trueshot;
            TurtleBox.Checked = Turtle;
            CounterShotBox.Checked = CounterShot;			


            cmdSave.Click += CmdSave_Click;
            BarrageBox.CheckedChanged += Barrage_Click;
            CrowBox.CheckedChanged += Crow_Click;
            DeathBox.CheckedChanged += Death_Click;
            TurtleBox.CheckedChanged += Turtle_Click;
            ExhilBox.CheckedChanged += Exhil_Click;
            TrueshotBox.CheckedChanged += Trueshot_Click;
            CounterShotBox.CheckedChanged += CounterShot_Click;			

            SettingsForm.Controls.Add(cmdSave);
            lblBarrageText.BringToFront();
            lblCrowText.BringToFront();
            lblTrueshotText.BringToFront();
            lblExhilText.BringToFront();
            lblDeathText.BringToFront();
            lblTurtleText.BringToFront();
            lblCounterShotText.BringToFront();			

            Log.Write("Barrage = " + Barrage);
            Log.Write("Crow = " + Crow);
            Log.Write("Exhil = " + Exhil);
            Log.Write("Death = " + Death);
            Log.Write("Turtle = " + Turtle);
            Log.Write("Trueshot = " + Trueshot);
            Log.Write("CounterShot = " + Trueshot);			
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Barrage = BarrageBox.Checked;
            Crow = CrowBox.Checked;
            Exhil = ExhilBox.Checked;
            Death = DeathBox.Checked;
            Trueshot = TrueshotBox.Checked;
            Turtle = TurtleBox.Checked;
            CounterShot = CounterShotBox.Checked;			
            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void Barrage_Click(object sender, EventArgs e)
        {
            Barrage = BarrageBox.Checked;
        }

        private void Crow_Click(object sender, EventArgs e)
        {
            Crow = CrowBox.Checked;
        }
        private void Death_Click(object sender, EventArgs e)
        {
            Death = DeathBox.Checked;
        }
        private void Exhil_Click(object sender, EventArgs e)
        {
            Exhil = ExhilBox.Checked;
        }
        private void Turtle_Click(object sender, EventArgs e)
        {
            Turtle = TurtleBox.Checked;
        }
        private void Trueshot_Click(object sender, EventArgs e)
        {
            Trueshot = TrueshotBox.Checked;
        }
        private void CounterShot_Click(object sender, EventArgs e)
        {
            CounterShot = CounterShotBox.Checked;
        }		

        private readonly Stopwatch interruptwatch = new Stopwatch();
        private readonly Stopwatch openerwatch = new Stopwatch();
		private readonly Stopwatch windburstwatch = new Stopwatch();
		private readonly Stopwatch barragewatch = new Stopwatch();		

        public override void Stop()
        {
        }


        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
			 if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {				
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {	
					if (WoW.CanCast("Salve") && !WoW.PlayerHasBuff("Salve")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Salve");
                        return;
                    }				
					/*if (WoW.CanCast("Arcane Torrent") && WoW.Focus <= 100) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }
*/					
					if (WoW.CanCast("Trueshot")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Trueshot");
                        return;
                    }	
					if (WoW.CanCast("Piercing Shot") && WoW.TargetHasDebuff("Vulnerable") && WoW.Focus >= 100 && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Piercing Shot");
                        return;
                    }						
                    if (WoW.CanCast("Murder of Crows") && Crow && (WoW.Focus >= 30) && WoW.IsSpellOnCooldown("Piercing Shot") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }
					if (WoW.CanCast("Marked Shot") && (WoW.Focus >= 25) && WoW.TargetHasDebuff("Hunters Mark") && WoW.IsSpellInRange("Windburst"))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}	
					if (WoW.CanCast("Windburst") && WoW.Focus >= 20 && WoW.IsSpellOnCooldown("Piercing Shot") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }					
                    if (WoW.CanCast("Arcane Shot") && WoW.TargetHasDebuff("Marking Targets") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark")
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }	
                    if (!WoW.IsMoving && WoW.Focus >= 50 && WoW.TargetHasDebuff("Vulnerable") && WoW.CanCast("AS") && (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 2.1) && WoW.IsSpellInRange("Windburst") && 
					!WoW.PlayerIsChanneling && WoW.IsSpellOnCooldown("Piercing Shot"))
                    {
                        WoW.CastSpell("AS");
                        return;
                    }	
                    if (!WoW.IsMoving && WoW.Focus >= 95 && !WoW.TargetHasDebuff("Vulnerable") && WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling && WoW.IsSpellOnCooldown("Piercing Shot"))
                    {
                        WoW.CastSpell("AS");
                        return;
                    }
                    if (WoW.PlayerHasBuff ("Lock and Load") && WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("AS");
                        return;
                    }						
                    if (!WoW.IsMoving && WoW.CanCast("Arcane Shot") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }	
			} 
			}
            if (combatRoutine.Type == RotationType.AOE)
            {
				 if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {		
					if (WoW.CanCast("Salve") && !WoW.PlayerHasBuff("Salve")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Salve");
                        return;
                    }			
					if (WoW.CanCast("Trueshot")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Trueshot");
                        return;
                    }					
					if (WoW.CanCast("Piercing Shot") && WoW.TargetHasDebuff("Vulnerable") && WoW.Focus >= 100 && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Piercing Shot");
                        return;
                    }					
                    if (WoW.CanCast("Murder of Crows") && Crow && (WoW.Focus >= 30) && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }
                    if (WoW.CanCast("Barrage") && Barrage && (WoW.Focus >= 60) && WoW.IsSpellOnCooldown("Piercing Shot") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Barrage");
                        return;
                    }						
					if (WoW.CanCast("Marked Shot") && (WoW.Focus >= 25) && WoW.TargetHasDebuff("Hunters Mark") && WoW.IsSpellInRange("Windburst"))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}	
					if (WoW.CanCast("Windburst") && WoW.Focus >= 20 && WoW.IsSpellOnCooldown("Piercing Shot") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }					
                    if (WoW.CanCast("Multi-Shot") && WoW.Focus >= 40 && WoW.TargetHasDebuff("Marking Targets") && WoW.IsSpellOnCooldown("Piercing Shot") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark")
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }	
                    if (!WoW.IsMoving && WoW.Focus >= 50 && WoW.TargetHasDebuff("Vulnerable") && WoW.CanCast("AS") && (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 2.1) && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling && WoW.IsSpellOnCooldown("Piercing Shot"))
                    {
                        WoW.CastSpell("AS");
                        return;
                    }	
                    if (!WoW.IsMoving && WoW.Focus >= 95 && !WoW.TargetHasDebuff("Vulnerable") && WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling && WoW.IsSpellOnCooldown("Piercing Shot"))
                    {
                        WoW.CastSpell("AS");
                        return;
                    }
                    if (WoW.PlayerHasBuff ("Lock and Load") && WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("AS");
                        return;
                    }						
                    if (!WoW.IsMoving && WoW.CanCast("Arcane Shot") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }				
			}
			}
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
				 if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {	
					if (WoW.CanCast("Salve") && !WoW.PlayerHasBuff("Salve")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Salve");
                        return;
                    }			
					/*if (WoW.CanCast("Arcane Torrent") && WoW.Focus <= 100) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }
*/					
					if (WoW.CanCast("Trueshot")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Trueshot");
                        return;
                    }	
					if (WoW.CanCast("Piercing Shot") && WoW.TargetHasDebuff("Vulnerable") && WoW.Focus >= 100 && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Piercing Shot");
                        return;
                    }						
                    if (WoW.CanCast("Murder of Crows") && Crow && (WoW.Focus >= 30) && WoW.IsSpellOnCooldown("Piercing Shot") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }
					if (WoW.CanCast("Marked Shot") && (WoW.Focus >= 25) && WoW.TargetHasDebuff("Hunters Mark") && WoW.IsSpellInRange("Windburst"))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}	
					if (WoW.CanCast("Windburst") && WoW.Focus >= 20 && WoW.IsSpellOnCooldown("Piercing Shot") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }					
                    if (WoW.CanCast("Arcane Shot") && WoW.TargetHasDebuff("Marking Targets") && WoW.IsSpellInRange("Windburst")) //!WoW.HasBuff("Hunters Mark")
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }	
                    if (!WoW.IsMoving && WoW.Focus >= 50 && WoW.TargetHasDebuff("Vulnerable") && WoW.CanCast("AS") && (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 2.1) && WoW.IsSpellInRange("Windburst") && 
					!WoW.PlayerIsChanneling && WoW.IsSpellOnCooldown("Piercing Shot"))
                    {
                        WoW.CastSpell("AS");
                        return;
                    }	
                    if (!WoW.IsMoving && WoW.Focus >= 95 && !WoW.TargetHasDebuff("Vulnerable") && WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling && WoW.IsSpellOnCooldown("Piercing Shot"))
                    {
                        WoW.CastSpell("AS");
                        return;
                    }
                    if (WoW.PlayerHasBuff ("Lock and Load") && WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("AS");
                        return;
                    }						
                    if (!WoW.IsMoving && WoW.CanCast("Arcane Shot") && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }	
				}
			}
                
				}



        public override string Name
        {
            get { return "HunterMarksman"; }
        }

        public override string Class
        {
            get { return "Hunter"; }
        }




        public static bool Barrage
        {
            get
            {
                var Barrage = ConfigFile.ReadValue("HunterMarksman", "Barrage").Trim();

                return Barrage != "" && Convert.ToBoolean(Barrage);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "Barrage", value.ToString()); }
        }

        private static bool Crow
        {
            get
            {
                var Crow = ConfigFile.ReadValue("HunterMarksman", "Crow").Trim();

                return Crow != "" && Convert.ToBoolean(Crow);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "Crow", value.ToString()); }
        }
        private static bool Death
        {
            get
            {
                var Death = ConfigFile.ReadValue("HunterMarksman", "Death").Trim();

                return Death != "" && Convert.ToBoolean(Death);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "Death", value.ToString()); }
        }
        private static bool Exhil
        {
            get
            {
                var Exhil = ConfigFile.ReadValue("HunterMarksman", "Exhil").Trim();

                return Exhil != "" && Convert.ToBoolean(Exhil);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "Exhil", value.ToString()); }
        }
        private static bool Trueshot
        {
            get
            {
                var Trueshot = ConfigFile.ReadValue("HunterMarksman", "Trueshot").Trim();

                return Trueshot != "" && Convert.ToBoolean(Trueshot);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "Trueshot", value.ToString()); }
        }
        private static bool Turtle
        {
            get
            {
                var Turtle = ConfigFile.ReadValue("HunterMarksman", "Turtle").Trim();

                return Turtle != "" && Convert.ToBoolean(Turtle);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "Turtle", value.ToString()); }
        }
        private static bool CounterShot
        {
            get
            {
                var CounterShot = ConfigFile.ReadValue("HunterMarksman", "CounterShot").Trim();

                return CounterShot != "" && Convert.ToBoolean(CounterShot);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "CounterShot", value.ToString()); }
        }		
    }
}



/*
[AddonDetails.db]
AddonAuthor=Vectarius
AddonName=myspellpriority
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,194386,Salve,D3
Spell,204147,Windburst,D2
Spell,120360,Barrage,D3
Spell,131894,Murder of Crows,D9
Spell,19434,AS,D4
Spell,185358,Arcane Shot,D5
Spell,185901,Marked Shot,D6
Spell,186387,Bursting Shot,D7
Spell,147362,Counter Shot,D8
Spell,198670,Piercing Shot,D1
Spell,2643,Multi-Shot,D0
Spell,109304,Exhil,V
Spell,193526,Trueshot,C
Spell,186265,Turtle,G
Spell,5384,Death,F
Aura,194386,Salve
Aura,223138,Marking Targets
Aura,185987,Hunters Mark
Aura,194594,Lock and Load
Aura,187131,Vulnerable
Aura,193526,Trueshot
Aura,2825,Bloodlust
Item,5512,Healthstone
Item,127834,Ancient Healing Potion
Item,133940,Silkweave Bandage
*/
