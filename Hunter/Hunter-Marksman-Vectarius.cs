/*Marksman by Vectarius
to-do: 
- improve dps?
- add opener
- add all spec variations
rev4 - improved AS usage
	 - slightly higher dps
	 - changed Salve to Volley
rev3 - added Trickshot checkbox
     - improved ST and AoE dps
rev2 - nice looking code
	 - Piercing Shot Checkbox
	 - Volley CheckBox
	 - Cooldowns Overlay Button implemented
	 - Counter Shot implemented
rev1 - 7.1.5 Rotation
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
        private CheckBox VolleyBox;	
        private CheckBox PiercingShotBox;			
        private CheckBox TrickshotBox;			

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

			
            var lblVolleyText = new Label //Volley label
            {
                Text = "Volley",
                Size = new Size(81, 13),
                Left = 12,
                Top = 226
            };
            SettingsForm.Controls.Add(lblVolleyText); //Volley text

            VolleyBox = new CheckBox
            {
                Checked = Volley,
                TabIndex = 12,
                Size = new Size(15, 14),
                Left = 115,
                Top = 226
            };
            SettingsForm.Controls.Add(VolleyBox);   //Volley box			
			
            var lblPiercingShotText = new Label //Trueshot LABEL - 129
            {
                Text = "PiercingShot",
                Size = new Size(81, 13),
                Left = 12,
                Top = 242
            };
            SettingsForm.Controls.Add(lblPiercingShotText); //Trueshot TEXT - 129

            PiercingShotBox = new CheckBox
            {
                Checked = PiercingShot,
                TabIndex = 4,
                Size = new Size(15, 14),
                Left = 115,
                Top = 242
            };
            SettingsForm.Controls.Add(PiercingShotBox);	//Trueshot BOX					

			            var lblTrickshotText = new Label //Barrage LABEL - 114
            {
                Text = "Trickshot",
                Size = new Size(81, 13),
                Left = 12,
                Top = 258
            };
            SettingsForm.Controls.Add(lblTrickshotText); //Barrage TEXT - 114

            TrickshotBox = new CheckBox
            {
                Checked = Trickshot,
                TabIndex = 2,
                Size = new Size(15, 14),
                Left = 115,
                Top = 258
            };
            SettingsForm.Controls.Add(TrickshotBox); //Barrage BOX
			
			
			
			
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
            VolleyBox.Checked = Volley;	
			PiercingShotBox.Checked = PiercingShot;			
			TrickshotBox.Checked = Trickshot;	


            cmdSave.Click += CmdSave_Click;
            BarrageBox.CheckedChanged += Barrage_Click;
            CrowBox.CheckedChanged += Crow_Click;
            DeathBox.CheckedChanged += Death_Click;
            TurtleBox.CheckedChanged += Turtle_Click;
            ExhilBox.CheckedChanged += Exhil_Click;
            TrueshotBox.CheckedChanged += Trueshot_Click;
            CounterShotBox.CheckedChanged += CounterShot_Click;		
            VolleyBox.CheckedChanged += Volley_Click;		
            PiercingShotBox.CheckedChanged += PiercingShot_Click;		
            TrickshotBox.CheckedChanged += Trickshot_Click;					
			

            SettingsForm.Controls.Add(cmdSave);
            lblBarrageText.BringToFront();
            lblCrowText.BringToFront();
            lblTrueshotText.BringToFront();
            lblExhilText.BringToFront();
            lblDeathText.BringToFront();
            lblTurtleText.BringToFront();
            lblCounterShotText.BringToFront();	
            lblVolleyText.BringToFront();
            lblPiercingShotText.BringToFront();
            lblTrickshotText.BringToFront();				

            Log.Write("Barrage = " + Barrage);
            Log.Write("Crow = " + Crow);
            Log.Write("Exhil = " + Exhil);
            Log.Write("Death = " + Death);
            Log.Write("Turtle = " + Turtle);
            Log.Write("Trueshot = " + Trueshot);
            Log.Write("CounterShot = " + CounterShot);	
            Log.Write("Volley = " + Volley);		
            Log.Write("PiercingShot = " + PiercingShot);		
            Log.Write("Trickshot = " + Trickshot);				
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
            Volley = VolleyBox.Checked;
            PiercingShot = PiercingShotBox.Checked;	
			Trickshot = TrickshotBox.Checked;

			
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
        private void Volley_Click(object sender, EventArgs e)
        {
            Volley = VolleyBox.Checked;
        }	
        private void PiercingShot_Click(object sender, EventArgs e)
        {
            PiercingShot = PiercingShotBox.Checked;
        }		
        private void Trickshot_Click(object sender, EventArgs e)
        {
            Trickshot = TrickshotBox.Checked;
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
					if (WoW.CanCast("Counter Shot") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.TargetIsCasting
						&& WoW.TargetIsCastingAndSpellIsInterruptible
						&& WoW.TargetPercentCast >=24
						&& CounterShot)
                    {
                        WoW.CastSpell("Counter Shot");
                        return;
                    }				
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& Volley)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }				
					/*if (WoW.CanCast("Arcane Torrent") 
						&& WoW.Focus <= 100)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }
*/					
					if (WoW.CanCast("Trueshot")
						&& UseCooldowns
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("Trueshot");
                        return;
                    }	
					if (WoW.CanCast("Piercing Shot") 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.Focus >= 100 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting			
						&& PiercingShot
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Piercing Shot");
                        return;
                    }		
					
                    if (WoW.CanCast("Murder of Crows") 
						&& Crow 
						&& (WoW.Focus >= 30) 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }
					
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& WoW.TargetHasDebuff("Vulnerable")						
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}	
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& !WoW.TargetHasDebuff("Vulnerable"))						

					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}						
/* 					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1)						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }
					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& !WoW.TargetHasDebuff("Vulnerable") 										
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }	
					*/
					
					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting															
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }										
                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						return;
                    }	
					}
					}
                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }
					}
					}
                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }	
					}
					}
                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }	
					}
					}					
                    if (WoW.PlayerHasBuff ("Lock and Load") 
						&& WoW.CanCast("AS") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting		
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.IsSpellInRange("Windburst")) 
                    {
                        WoW.CastSpell("AS");
                        return;
                    }	
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1.7						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }					
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& PiercingShot
						&& !WoW.IsSpellOnCooldown("Piercing Shot")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }	
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& PiercingShot
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.SpellCooldownTimeRemaining ("Piercing Shot") <3						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }						
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.Focus <=49
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }						
                   /* if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }						
                    if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.TargetHasDebuff("Vulnerable")
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1.9 ||	WoW.TargetDebuffTimeRemaining("Vulnerable") >= 5)						
						&& WoW.IsSpellOnCooldown("Windburst")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }
*/					
			} 
			}
            if (combatRoutine.Type == RotationType.AOE)
            {
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {	
					if (WoW.CanCast("Counter Shot") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.TargetIsCasting
						&& WoW.TargetIsCastingAndSpellIsInterruptible
						&& WoW.TargetPercentCast >=24
						&& CounterShot)
                    {
                        WoW.CastSpell("Counter Shot");
                        return;
                    }				
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& Volley)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }				
					/*if (WoW.CanCast("Arcane Torrent") 
						&& WoW.Focus <= 100)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }
*/					
					if (WoW.CanCast("Trueshot")
						&& UseCooldowns
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("Trueshot");
                        return;
                    }	
					if (WoW.CanCast("Piercing Shot") 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.Focus >= 100 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting			
						&& PiercingShot
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Piercing Shot");
                        return;
                    }		
					
                    if (WoW.CanCast("Murder of Crows") 
						&& Crow 
						&& (WoW.Focus >= 30) 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }
					
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& WoW.TargetHasDebuff("Vulnerable")						
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}	
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& !WoW.TargetHasDebuff("Vulnerable"))						

					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}						
/* 					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1)						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }
					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& !WoW.TargetHasDebuff("Vulnerable") 										
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }	
					*/
					
					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting															
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }										
                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						return;
                    }	
					}
					}
                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }
					}
					}
                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }	
					}
					}
                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }	
					}
					}					
                    if (WoW.PlayerHasBuff ("Lock and Load") 
						&& WoW.CanCast("AS") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting		
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.IsSpellInRange("Windburst")) 
                    {
                        WoW.CastSpell("AS");
                        return;
                    }	
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1.7						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }					
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& PiercingShot
						&& !WoW.IsSpellOnCooldown("Piercing Shot")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }	
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& PiercingShot
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.SpellCooldownTimeRemaining ("Piercing Shot") <3						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }						
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.Focus <=49
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }						
                   /* if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }						
                    if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.TargetHasDebuff("Vulnerable")
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1.9 ||	WoW.TargetDebuffTimeRemaining("Vulnerable") >= 5)						
						&& WoW.IsSpellOnCooldown("Windburst")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }
*/					
			}
			}
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {	
					if (WoW.CanCast("Counter Shot") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.TargetIsCasting
						&& WoW.TargetIsCastingAndSpellIsInterruptible
						&& WoW.TargetPercentCast >=24
						&& CounterShot)
                    {
                        WoW.CastSpell("Counter Shot");
                        return;
                    }				
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& Volley)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }				
					/*if (WoW.CanCast("Arcane Torrent") 
						&& WoW.Focus <= 100)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }
*/					
					if (WoW.CanCast("Trueshot")
						&& UseCooldowns
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("Trueshot");
                        return;
                    }	
					if (WoW.CanCast("Piercing Shot") 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.Focus >= 100 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting			
						&& PiercingShot
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Piercing Shot");
                        return;
                    }		
					
                    if (WoW.CanCast("Murder of Crows") 
						&& Crow 
						&& (WoW.Focus >= 30) 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }
					
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& WoW.TargetHasDebuff("Vulnerable")						
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}	
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& !WoW.TargetHasDebuff("Vulnerable"))						

					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}						
/* 					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1)						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }
					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& !WoW.TargetHasDebuff("Vulnerable") 										
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }	
					*/
					
					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting															
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }										
                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						return;
                    }	
					}
					}
                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& PiercingShot
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >3 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }
					}
					}
                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving  // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 1.8) 
						&& WoW.IsSpellInRange("Windburst") 
						&& Trickshot
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }	
					}
					}
                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
						                    if (!WoW.IsMoving 
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& Trickshot						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)

                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }	
					}
					}					
                    if (WoW.PlayerHasBuff ("Lock and Load") 
						&& WoW.CanCast("AS") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting		
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.IsSpellInRange("Windburst")) 
                    {
                        WoW.CastSpell("AS");
                        return;
                    }	
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1.7						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }					
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& PiercingShot
						&& !WoW.IsSpellOnCooldown("Piercing Shot")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }	
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& PiercingShot
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.SpellCooldownTimeRemaining ("Piercing Shot") <3						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }						
						if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.Focus <=49
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }						
                   /* if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }						
                    if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.TargetHasDebuff("Vulnerable")
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1.9 ||	WoW.TargetDebuffTimeRemaining("Vulnerable") >= 5)						
						&& WoW.IsSpellOnCooldown("Windburst")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }
*/				
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

		private static Image Logo
        {
            get
            {
				var newByteArray = "71,73,70,56,57,97,137,0,142,0,247,0,0,0,0,0,0,0,51,0,0,102,0,0,153,0,0,204,0,0,255,0,43,0,0,43,51,0,43,102,0,43,153,0,43,204,0,43,255,0,85,0,0,85,51,0,85,102,0,85,153,0,85,204,0,85,255,0,128,0,0,128,51,0,128,102,0,128,153,0,128,204,0,128,255,0,170,0,0,170,51,0,170,102,0,170,153,0,170,204,0,170,255,0,213,0,0,213,51,0,213,102,0,213,153,0,213,204,0,213,255,0,255,0,0,255,51,0,255,102,0,255,153,0,255,204,0,255,255,51,0,0,51,0,51,51,0,102,51,0,153,51,0,204,51,0,255,51,43,0,51,43,51,51,43,102,51,43,153,51,43,204,51,43,255,51,85,0,51,85,51,51,85,102,51,85,153,51,85,204,51,85,255,51,128,0,51,128,51,51,128,102,51,128,153,51,128,204,51,128,255,51,170,0,51,170,51,51,170,102,51,170,153,51,170,204,51,170,255,51,213,0,51,213,51,51,213,102,51,213,153,51,213,204,51,213,255,51,255,0,51,255,51,51,255,102,51,255,153,51,255,204,51,255,255,102,0,0,102,0,51,102,0,102,102,0,153,102,0,204,102,0,255,102,43,0,102,43,51,102,43,102,102,43,153,102,43,204,102,43,255,102,85,0,102,85,51,102,85,102,102,85,153,102,85,204,102,85,255,102,128,0,102,128,51,102,128,102,102,128,153,102,128,204,102,128,255,102,170,0,102,170,51,102,170,102,102,170,153,102,170,204,102,170,255,102,213,0,102,213,51,102,213,102,102,213,153,102,213,204,102,213,255,102,255,0,102,255,51,102,255,102,102,255,153,102,255,204,102,255,255,153,0,0,153,0,51,153,0,102,153,0,153,153,0,204,153,0,255,153,43,0,153,43,51,153,43,102,153,43,153,153,43,204,153,43,255,153,85,0,153,85,51,153,85,102,153,85,153,153,85,204,153,85,255,153,128,0,153,128,51,153,128,102,153,128,153,153,128,204,153,128,255,153,170,0,153,170,51,153,170,102,153,170,153,153,170,204,153,170,255,153,213,0,153,213,51,153,213,102,153,213,153,153,213,204,153,213,255,153,255,0,153,255,51,153,255,102,153,255,153,153,255,204,153,255,255,204,0,0,204,0,51,204,0,102,204,0,153,204,0,204,204,0,255,204,43,0,204,43,51,204,43,102,204,43,153,204,43,204,204,43,255,204,85,0,204,85,51,204,85,102,204,85,153,204,85,204,204,85,255,204,128,0,204,128,51,204,128,102,204,128,153,204,128,204,204,128,255,204,170,0,204,170,51,204,170,102,204,170,153,204,170,204,204,170,255,204,213,0,204,213,51,204,213,102,204,213,153,204,213,204,204,213,255,204,255,0,204,255,51,204,255,102,204,255,153,204,255,204,204,255,255,255,0,0,255,0,51,255,0,102,255,0,153,255,0,204,255,0,255,255,43,0,255,43,51,255,43,102,255,43,153,255,43,204,255,43,255,255,85,0,255,85,51,255,85,102,255,85,153,255,85,204,255,85,255,255,128,0,255,128,51,255,128,102,255,128,153,255,128,204,255,128,255,255,170,0,255,170,51,255,170,102,255,170,153,255,170,204,255,170,255,255,213,0,255,213,51,255,213,102,255,213,153,255,213,204,255,213,255,255,255,0,255,255,51,255,255,102,255,255,153,255,255,204,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,33,249,4,1,0,0,252,0,44,0,0,0,0,137,0,142,0,0,8,255,0,247,9,28,72,176,160,193,131,8,19,42,92,200,176,161,195,135,16,35,74,156,72,177,162,197,139,24,51,62,84,38,144,163,198,143,32,67,50,228,72,82,89,61,104,39,83,238,83,102,18,90,203,147,251,234,137,156,73,211,97,74,146,49,81,238,67,201,83,166,71,142,41,117,226,244,88,179,168,200,161,59,131,250,204,41,83,169,80,166,61,163,202,52,74,149,34,78,165,42,157,106,125,170,242,234,83,156,85,195,46,4,218,115,101,82,179,202,244,37,155,247,108,222,60,100,249,228,185,189,68,111,152,24,73,55,36,77,26,198,113,153,62,191,93,205,170,20,75,184,35,90,168,62,231,37,147,39,76,30,227,184,248,158,201,67,38,47,95,190,118,243,194,132,217,2,99,5,140,24,49,96,132,17,147,137,216,223,195,78,11,83,245,186,180,158,178,121,195,130,189,157,92,121,50,62,203,183,225,230,75,38,76,146,102,208,161,99,132,1,45,122,179,176,97,167,201,182,86,77,83,57,202,191,250,134,69,218,213,46,152,48,118,200,230,9,155,87,57,123,118,124,220,185,71,255,18,182,72,18,12,21,42,110,16,15,243,153,75,24,45,189,119,177,75,166,111,43,76,230,25,181,162,149,183,104,215,174,96,236,92,215,24,99,142,205,102,25,92,148,97,34,140,48,154,169,16,156,122,49,220,176,133,132,198,253,183,96,101,244,124,197,20,126,22,149,69,22,61,0,178,227,159,34,243,136,216,216,91,214,229,195,29,101,249,100,151,143,48,145,204,35,201,22,236,1,0,218,13,159,125,182,130,102,96,240,2,35,59,193,56,70,153,62,206,45,197,97,68,38,161,6,141,91,193,64,34,79,48,254,237,50,143,127,219,225,115,161,92,42,194,21,222,130,236,108,1,6,103,193,1,39,33,12,91,108,33,137,48,22,6,131,207,147,0,202,67,95,61,209,208,19,231,156,71,218,148,148,80,201,176,243,36,36,181,0,24,140,34,236,152,136,29,59,47,50,166,219,118,45,106,151,89,24,139,56,24,195,1,55,222,32,198,112,94,250,184,139,48,109,6,163,41,166,1,34,151,210,105,244,212,169,144,84,43,209,131,41,62,236,40,2,9,36,0,90,8,36,47,132,98,255,250,226,128,243,88,38,140,138,243,44,2,6,35,231,193,144,227,1,147,136,1,131,176,90,72,210,14,144,152,50,22,143,48,235,200,195,206,58,193,172,35,204,75,127,241,36,234,65,74,173,132,98,160,172,42,178,136,117,151,50,246,31,59,243,88,247,214,124,183,214,170,216,60,152,192,176,200,22,142,130,102,64,12,146,136,33,92,24,42,8,200,139,166,108,2,9,164,159,84,122,24,211,181,3,177,4,21,119,65,54,6,229,34,138,208,162,231,117,37,94,194,139,92,214,233,137,157,48,201,168,152,177,36,61,114,166,130,175,161,65,42,6,142,91,240,218,14,166,242,57,171,105,144,129,62,235,159,46,181,236,146,76,96,68,29,153,213,115,195,56,54,96,128,193,16,2,6,128,229,114,39,204,190,237,48,134,157,208,1,234,163,79,62,188,68,194,11,190,0,124,22,102,0,195,126,22,73,24,39,91,40,15,62,155,58,11,237,46,235,176,3,73,148,236,32,85,51,115,208,160,21,167,50,156,58,70,219,159,138,16,242,173,162,79,114,202,155,60,195,8,237,98,116,146,72,255,247,153,10,84,203,27,218,10,147,138,182,96,48,176,110,93,162,166,254,66,41,173,127,224,10,67,15,96,113,154,133,159,114,250,128,184,206,46,203,114,215,78,62,12,235,234,109,160,249,4,184,96,99,23,186,181,224,60,250,184,37,227,199,14,194,0,0,132,31,11,199,136,10,96,104,199,44,101,91,59,19,224,184,129,6,3,204,184,155,251,55,204,62,128,233,164,154,114,43,9,83,206,127,90,203,37,143,183,90,200,13,201,137,200,4,232,172,118,178,49,182,186,210,201,240,2,131,22,42,204,30,131,10,243,66,24,154,121,139,228,105,110,101,113,33,19,15,244,244,3,40,237,243,155,11,115,154,95,126,17,166,225,117,186,120,94,180,52,197,36,93,249,76,17,128,218,206,100,184,164,162,237,232,142,117,181,226,216,22,200,7,156,208,0,96,66,177,155,4,24,194,112,58,53,193,229,54,140,1,151,166,254,227,44,102,69,203,63,181,144,92,229,250,18,150,165,144,132,23,96,115,92,148,194,6,186,184,105,65,11,139,88,132,158,40,118,29,185,188,37,75,194,64,198,210,255,120,65,35,217,193,32,0,230,59,128,212,86,16,131,146,133,65,74,194,136,135,92,214,4,25,76,157,48,68,64,146,22,148,160,68,37,57,213,3,58,83,41,74,79,42,135,166,152,205,48,134,142,137,68,45,20,65,5,45,128,193,91,141,41,157,117,28,184,155,100,4,35,99,176,137,196,187,202,7,26,72,197,192,51,196,129,193,13,22,49,158,186,33,3,60,149,185,13,180,78,8,45,45,170,44,120,208,227,5,52,188,104,173,154,44,69,78,166,218,98,204,214,1,173,62,133,11,77,133,24,159,22,188,5,160,22,93,7,25,195,200,7,61,94,132,199,118,232,81,11,0,112,144,131,108,116,3,3,172,192,0,91,0,77,177,184,20,69,199,68,166,86,147,121,22,179,208,196,47,100,72,75,139,242,25,23,71,188,120,154,153,140,81,78,34,162,210,216,20,17,37,199,45,134,23,138,208,194,248,192,208,31,5,2,45,99,74,195,213,130,36,1,203,143,217,72,137,54,10,77,142,38,40,9,132,209,38,31,224,185,77,92,216,180,50,32,197,3,59,235,200,158,166,180,104,255,142,105,33,15,58,52,249,212,50,232,97,33,200,173,17,18,138,24,161,117,228,178,11,28,106,129,10,220,28,79,3,83,180,180,214,185,142,156,31,91,65,58,31,21,178,120,177,167,157,201,82,28,92,166,136,176,255,148,131,101,147,185,167,49,5,4,48,201,153,101,160,34,17,168,62,36,177,11,51,94,10,18,218,147,203,117,186,39,12,111,157,7,135,132,4,102,16,233,179,74,215,9,3,12,231,137,154,141,108,244,25,166,222,64,5,154,209,66,140,128,228,203,173,101,167,45,40,250,79,180,120,71,27,45,2,136,83,104,242,39,76,206,118,17,156,248,69,18,102,100,24,23,23,138,176,32,202,69,143,96,184,161,187,164,42,9,185,232,41,31,75,43,42,121,200,231,43,0,76,234,156,48,56,0,250,206,167,25,24,108,231,66,32,204,152,101,106,245,175,122,58,230,24,216,65,70,155,140,70,76,31,189,212,114,24,249,226,50,204,50,163,209,237,130,22,228,89,232,98,23,90,153,96,68,34,174,42,184,225,22,244,232,22,141,229,227,25,173,35,207,34,180,25,181,27,255,12,163,130,54,138,23,24,200,153,51,210,198,197,25,181,178,204,139,172,99,157,120,28,242,177,203,202,103,99,44,134,56,196,9,99,179,25,10,163,69,162,1,198,125,100,98,11,57,212,225,46,190,213,39,68,105,135,119,217,49,237,108,41,184,43,48,96,130,59,175,93,154,138,124,4,134,171,69,2,19,68,202,209,141,2,73,163,69,228,227,76,180,113,6,252,130,123,58,105,73,81,72,134,10,6,120,189,7,164,93,192,112,102,209,56,137,156,46,82,15,74,18,67,51,140,42,132,211,162,20,199,90,145,150,80,201,136,132,34,222,67,62,30,241,130,55,172,91,172,118,116,197,65,240,209,3,19,241,186,160,112,160,42,9,73,188,72,59,132,122,75,112,91,55,79,200,174,233,184,148,161,204,173,8,228,167,195,93,202,36,245,169,92,69,122,242,23,223,208,136,17,57,20,70,45,166,115,29,91,153,86,103,224,9,134,174,224,69,166,205,140,231,188,120,173,21,140,220,200,142,165,13,227,76,195,96,143,124,57,35,154,186,170,203,64,112,161,177,138,220,84,213,5,14,216,255,49,179,58,28,226,46,229,79,178,84,132,35,40,137,134,36,64,51,169,24,224,16,134,144,219,142,62,46,129,38,199,220,245,18,57,148,43,153,180,32,134,28,242,134,62,83,146,68,48,82,153,183,121,156,33,12,200,48,143,122,96,80,47,21,208,232,56,250,211,114,92,130,171,174,22,213,102,50,220,161,13,170,179,99,40,151,161,233,18,84,66,205,68,128,18,167,76,84,144,70,140,66,171,34,106,209,159,18,245,116,64,147,121,209,46,178,73,193,207,44,2,13,28,92,132,254,146,113,38,188,70,34,18,120,61,234,34,134,129,84,51,133,1,19,239,137,145,130,228,146,140,118,172,197,50,223,142,203,14,115,92,32,238,44,102,30,224,121,17,118,136,123,169,93,96,98,23,47,81,30,68,160,91,185,144,141,153,81,253,241,15,36,38,6,163,122,234,73,178,179,245,24,12,170,16,213,171,41,155,93,172,131,209,48,46,117,50,48,28,71,52,201,8,3,35,204,19,9,125,4,113,24,219,89,140,138,118,147,29,197,112,53,123,192,5,183,92,92,244,54,76,153,124,83,193,255,88,102,130,211,6,17,232,108,22,13,55,144,65,31,85,160,68,55,246,103,17,75,134,162,66,131,20,222,53,106,179,137,161,201,229,34,24,117,88,165,41,155,23,220,148,210,105,245,113,181,48,200,8,233,237,28,185,16,125,8,238,29,27,138,54,112,33,23,50,110,69,153,100,88,41,132,206,59,156,216,145,147,188,150,243,79,31,196,136,208,104,212,247,211,254,232,226,230,206,210,97,180,2,149,245,28,178,135,51,84,166,17,14,37,97,170,214,157,65,182,87,107,184,254,220,168,153,97,192,71,198,223,78,165,42,27,104,234,236,29,3,216,58,30,143,118,226,18,196,0,73,203,104,200,2,146,114,164,171,16,56,133,202,47,56,184,209,200,66,111,0,7,113,19,134,253,137,4,185,192,112,61,235,80,177,92,12,163,130,124,65,51,116,70,229,3,57,12,154,7,82,87,247,158,204,136,166,88,243,128,129,162,90,187,150,182,60,67,184,108,22,198,49,222,233,22,100,205,145,64,140,121,124,179,4,92,177,77,81,114,39,14,193,201,36,62,3,161,181,19,199,207,211,241,207,255,183,228,49,236,132,138,86,216,97,248,146,175,50,122,3,45,164,63,111,22,7,67,152,85,192,11,125,176,231,56,55,212,76,121,34,225,109,182,212,10,156,42,178,52,139,33,12,92,115,34,147,145,29,156,178,80,131,242,36,255,198,37,2,150,121,243,176,66,156,135,16,125,65,36,124,52,28,243,69,28,228,179,8,128,6,90,10,35,101,252,226,22,242,240,52,114,69,53,182,84,101,187,165,34,67,151,25,31,51,91,78,55,12,54,32,9,188,66,109,125,227,58,173,67,15,106,161,94,110,177,117,223,69,121,4,114,58,167,212,38,91,135,58,203,242,120,110,243,48,130,193,114,99,177,15,114,50,9,232,195,71,192,129,3,56,18,26,57,180,11,106,20,126,228,226,24,80,194,34,243,192,11,83,230,43,127,243,49,238,231,116,244,96,3,47,232,27,197,65,15,204,230,27,45,134,59,136,162,88,244,145,101,75,227,61,110,99,40,171,51,40,217,115,58,238,83,121,91,87,32,121,8,37,195,176,89,64,177,132,128,177,105,0,16,53,49,144,78,97,224,25,42,224,52,55,255,71,37,23,18,23,65,34,84,137,38,59,137,88,59,100,98,3,195,48,12,42,32,6,194,32,26,243,119,28,97,48,12,152,160,5,156,40,9,229,161,74,110,1,13,114,152,101,188,193,34,167,19,66,231,130,128,147,56,132,87,71,106,37,36,32,46,167,15,10,113,118,245,144,9,155,1,26,229,131,136,108,183,5,61,229,31,145,64,92,88,130,44,181,49,98,236,241,49,150,152,75,90,32,28,201,0,0,97,32,9,135,8,3,195,128,6,164,56,44,216,216,98,97,214,78,138,17,128,74,163,52,234,130,29,159,99,58,140,81,43,200,48,128,67,101,58,46,210,90,29,52,29,26,102,138,65,54,57,9,113,143,209,64,41,99,18,75,126,20,33,169,5,31,56,55,52,48,116,43,146,104,33,47,198,14,79,83,59,243,50,33,235,97,30,195,98,78,156,150,9,190,50,26,44,230,46,21,231,22,229,136,87,174,232,29,170,179,32,241,120,49,65,244,145,228,210,58,22,133,140,8,116,67,249,87,118,8,145,96,115,18,102,195,49,141,195,113,136,142,50,141,48,64,5,67,255,227,45,201,232,93,255,98,114,250,2,6,152,232,32,19,146,75,195,178,62,49,112,130,43,128,9,55,130,141,16,118,141,173,149,131,56,40,128,176,209,109,195,181,58,236,168,59,14,180,55,177,5,35,94,184,93,229,161,43,218,164,2,153,128,22,19,200,132,160,98,47,155,33,33,196,65,140,231,163,77,140,32,91,138,224,35,90,6,107,227,230,61,179,53,62,56,50,59,185,20,33,194,241,84,153,16,75,71,41,28,77,37,9,104,32,26,19,132,9,169,180,74,229,184,52,202,176,88,29,71,138,35,185,27,179,113,102,31,169,71,96,25,87,218,164,146,154,209,98,153,240,121,250,16,13,8,65,111,31,131,107,125,57,140,77,148,35,84,192,129,161,163,8,96,176,11,242,48,29,3,180,32,42,34,101,114,21,59,194,113,155,82,195,125,137,24,38,234,1,0,183,133,30,230,37,104,144,6,13,230,152,12,24,23,135,223,19,29,184,130,43,116,134,9,94,136,138,248,182,25,90,64,102,235,23,3,54,32,6,56,17,42,5,1,29,28,177,125,14,178,25,24,88,62,220,255,199,136,55,180,93,222,146,67,49,50,54,122,18,36,163,38,12,42,233,43,185,52,28,148,242,25,97,16,0,157,104,0,54,50,47,32,51,44,91,0,0,27,116,28,121,85,142,138,193,27,152,176,22,38,105,142,89,166,29,152,240,108,189,49,52,87,195,77,57,228,27,195,97,3,185,228,40,156,177,2,202,64,73,19,72,107,207,88,28,183,121,136,192,65,38,162,148,67,181,240,70,8,20,32,9,5,96,165,51,108,56,4,158,252,169,118,55,48,161,160,17,56,233,81,65,133,165,2,53,136,87,139,185,36,201,96,160,38,169,145,138,129,113,24,227,74,140,178,97,97,160,71,239,209,126,100,50,133,220,39,102,160,17,6,255,116,31,5,99,22,196,192,71,244,57,28,178,67,140,189,130,67,172,153,93,122,212,24,86,72,46,242,180,32,219,149,126,52,50,148,20,242,125,79,101,75,180,36,72,231,131,6,30,115,34,230,72,156,106,193,58,21,213,58,24,199,49,145,144,167,6,183,32,21,57,157,140,16,85,68,25,33,87,42,6,104,128,150,49,112,21,6,225,121,153,255,80,122,127,211,103,137,136,136,26,72,127,57,68,77,222,50,46,145,16,54,148,1,76,94,73,8,50,121,119,146,82,88,130,217,71,129,229,40,79,133,134,211,214,78,58,202,139,27,9,27,189,129,138,187,117,28,216,182,65,120,57,138,248,226,164,231,177,105,193,113,68,159,33,6,144,218,153,43,71,16,212,34,6,76,229,25,16,2,162,129,20,144,11,130,115,85,24,34,56,165,50,112,65,30,111,228,37,238,103,47,247,18,6,18,18,163,107,41,56,193,209,98,91,224,156,49,178,163,251,208,58,198,105,58,219,209,14,210,193,97,155,33,26,100,146,12,19,87,47,141,54,50,52,50,41,91,48,41,195,129,35,216,26,26,104,160,89,205,36,16,105,195,17,132,21,28,88,10,162,249,58,62,49,0,6,199,66,8,122,36,126,219,113,76,200,192,34,200,128,64,220,4,47,81,35,159,224,169,25,19,34,6,142,178,81,231,211,98,71,58,116,144,150,57,153,3,174,72,234,126,238,103,119,218,84,145,64,215,151,184,9,28,185,52,148,163,1,38,33,26,3,115,242,69,3,255,225,26,49,161,12,229,243,27,22,20,75,234,225,171,156,97,115,218,225,45,188,166,108,141,17,27,201,98,74,186,82,8,72,53,140,158,49,41,147,98,30,242,25,26,182,9,28,77,201,77,181,210,58,200,80,52,1,18,9,184,54,65,234,180,65,45,102,47,82,163,62,125,137,165,223,183,151,216,234,144,82,147,168,76,152,96,3,145,54,126,65,145,244,121,62,81,83,59,79,42,74,71,58,180,12,67,77,178,89,40,84,199,24,72,90,62,195,248,157,154,17,168,126,118,155,74,20,0,2,219,68,19,116,164,200,145,12,116,214,24,12,131,67,52,34,33,234,151,171,18,119,168,117,139,129,125,185,5,54,112,155,221,231,178,234,51,9,135,33,16,56,171,12,98,176,151,125,69,115,196,65,102,249,151,66,243,176,100,12,115,58,33,182,34,155,170,46,72,103,154,98,80,122,23,20,6,200,150,175,236,97,111,140,251,167,227,227,98,186,211,52,35,242,30,232,145,184,100,38,75,124,121,141,79,133,35,234,51,26,195,235,103,249,90,65,243,181,175,207,1,154,115,18,42,194,255,91,65,242,21,147,163,212,77,243,48,29,212,180,93,162,214,34,71,227,58,96,9,53,196,176,15,196,122,136,128,249,27,158,187,81,232,65,173,8,171,29,78,115,90,217,21,74,232,145,35,96,24,28,165,7,6,179,179,103,85,163,173,117,43,26,33,138,107,195,219,25,97,160,114,83,65,73,42,0,72,97,34,28,19,52,162,181,0,67,129,114,158,176,249,183,165,195,59,66,67,61,176,228,87,127,169,2,196,128,6,132,235,32,55,192,8,189,73,115,76,117,176,238,183,29,168,120,151,55,244,74,90,64,59,232,100,68,140,139,136,42,128,6,104,64,75,55,226,144,96,82,196,234,97,184,147,114,3,112,66,51,251,144,118,159,65,53,140,171,150,227,147,90,56,87,14,34,130,122,12,99,133,145,0,103,229,130,135,114,81,185,15,149,84,153,144,9,166,251,151,135,136,159,53,250,25,243,18,59,139,198,129,146,96,164,231,193,97,194,0,182,131,69,192,232,3,72,179,99,137,97,146,75,106,73,181,244,217,25,130,228,171,134,234,171,64,214,63,76,97,107,179,39,192,218,255,4,71,229,0,67,229,192,107,230,73,72,50,19,23,132,246,120,239,147,104,67,215,159,0,176,89,6,240,87,244,75,147,1,96,75,6,64,53,130,229,103,187,149,93,153,217,118,228,212,182,243,34,202,194,88,202,76,164,62,66,44,158,43,70,38,77,116,141,147,128,9,245,114,141,146,160,12,79,97,57,123,166,189,231,19,26,64,197,11,141,252,200,94,72,72,202,124,41,165,115,9,235,201,64,34,130,64,138,48,65,244,251,151,105,176,15,105,80,184,248,249,201,188,234,43,238,135,116,83,70,62,158,102,3,72,102,147,131,213,202,187,9,26,28,59,95,1,28,28,218,196,25,215,136,9,153,176,203,119,145,9,202,144,9,195,96,36,104,129,192,231,163,159,126,54,157,223,194,11,155,147,122,114,131,111,227,65,46,207,231,54,91,231,149,40,187,155,49,49,9,202,16,169,159,76,147,217,88,202,115,229,37,2,247,25,216,117,30,161,17,0,74,4,41,128,28,38,158,97,159,49,176,186,22,52,62,16,214,98,199,33,131,188,140,6,140,112,6,241,154,9,212,197,63,56,255,33,44,97,162,209,40,109,96,128,150,67,101,82,195,55,87,96,119,101,110,20,211,183,101,202,71,153,144,6,126,53,9,7,160,81,20,61,209,158,81,101,227,35,112,231,243,181,211,24,28,144,2,0,161,12,28,59,162,206,16,28,188,154,113,6,103,162,29,121,250,210,190,33,6,48,125,168,48,45,9,245,161,60,158,103,211,97,66,65,140,2,67,168,167,146,238,87,8,229,65,52,225,18,30,237,121,9,113,57,52,179,133,30,159,12,176,19,253,201,219,156,141,96,59,141,152,152,166,189,114,148,182,41,59,232,83,145,88,154,210,136,178,52,201,43,9,145,208,98,47,205,8,153,109,189,153,64,111,68,242,182,139,200,209,152,136,93,146,208,52,227,5,159,49,80,5,100,178,107,213,113,41,218,99,74,155,186,162,178,149,35,244,91,210,244,59,140,159,76,53,20,13,141,179,116,62,182,252,219,233,204,216,65,71,161,236,225,126,72,181,167,183,43,61,151,49,52,45,70,72,119,1,211,192,91,47,146,80,22,150,99,173,109,235,192,210,105,155,96,50,157,227,163,122,164,255,213,90,164,54,132,8,203,32,24,123,3,76,221,212,177,116,219,129,153,165,135,8,72,82,19,53,34,42,192,20,237,219,162,177,65,187,5,127,173,149,51,66,218,67,186,115,217,215,184,185,140,128,108,19,247,79,148,35,39,216,170,171,96,128,193,237,44,112,79,245,71,227,163,108,80,162,61,43,242,22,109,209,64,211,145,25,63,23,26,13,142,222,136,168,84,145,58,12,147,240,201,32,157,35,195,241,132,130,37,26,98,192,11,125,131,113,153,160,108,89,134,55,188,177,32,244,200,100,195,96,58,205,141,108,132,249,210,190,250,28,155,245,182,245,96,182,177,83,59,185,196,212,25,117,154,245,181,14,227,39,27,90,162,49,90,34,89,194,208,14,187,128,47,28,125,211,32,26,47,71,228,143,91,176,18,135,120,0,244,171,159,248,66,168,177,52,26,245,210,211,194,112,94,120,234,22,152,64,72,150,205,8,150,57,116,95,165,29,77,99,214,1,30,168,81,235,121,43,113,118,98,16,122,223,135,91,183,13,0,104,60,72,19,71,76,180,178,113,26,247,90,62,4,106,19,255,4,59,28,139,30,214,123,210,18,109,219,166,139,3,93,158,198,126,252,140,114,21,180,190,177,32,28,148,167,188,48,12,42,14,157,132,128,111,11,162,203,44,61,26,48,61,41,46,39,16,252,19,163,158,203,222,217,8,50,14,18,150,156,214,181,153,170,51,236,32,79,139,37,98,52,12,235,144,162,25,43,240,179,129,234,185,193,81,62,55,208,196,157,13,162,244,43,95,189,146,90,28,118,67,129,186,209,156,184,25,146,208,11,190,49,116,139,48,113,81,149,107,42,238,223,80,123,108,98,96,22,114,2,24,251,16,3,92,112,196,201,158,141,107,92,28,135,27,87,7,205,160,141,1,30,165,230,131,170,67,62,84,128,183,214,90,88,16,6,38,82,131,159,91,0,13,196,106,168,131,93,229,162,1,198,69,244,30,244,98,59,37,94,101,80,139,6,139,176,186,77,25,161,155,141,108,147,242,203,120,182,150,233,13,0,198,202,68,63,119,151,251,23,214,8,24,46,169,68,25,146,49,114,110,17,9,38,155,35,195,40,218,128,189,242,18,13,56,49,48,9,197,190,15,231,255,141,219,178,51,138,237,0,171,27,70,5,210,232,103,52,114,173,24,156,150,255,125,203,39,187,8,103,16,168,18,95,244,251,80,179,96,116,3,185,173,155,196,28,116,140,112,100,78,195,14,39,131,25,42,242,108,200,64,104,36,63,132,50,130,84,23,148,165,124,116,183,231,17,192,226,233,32,243,139,6,216,12,240,244,139,68,159,12,85,39,227,52,72,150,239,19,34,159,245,58,141,115,255,30,191,158,237,77,169,227,199,38,230,223,139,186,113,242,84,233,141,211,85,22,245,127,86,218,201,172,71,138,96,187,13,100,135,170,166,24,83,6,85,154,204,229,79,24,75,40,141,119,76,149,141,1,240,151,1,112,207,108,79,191,68,46,127,212,166,204,18,58,148,183,60,181,224,25,122,100,98,175,143,91,123,18,55,26,102,45,9,4,254,182,161,34,3,163,44,53,208,123,184,50,120,215,140,144,127,55,36,58,35,185,52,172,198,106,171,243,34,101,38,179,31,163,219,21,187,251,88,106,154,143,110,93,1,159,222,140,16,29,17,202,40,224,217,243,104,203,179,183,44,28,190,255,202,35,245,53,116,200,38,241,251,10,80,104,209,224,196,33,148,210,137,93,113,63,94,91,224,169,179,149,93,167,179,113,98,151,34,219,241,101,70,4,162,190,59,154,165,29,9,129,58,157,0,1,0,198,141,97,98,98,192,0,0,32,134,138,132,13,17,74,10,19,70,152,62,125,139,194,128,209,114,16,198,65,21,11,195,108,9,179,226,96,196,48,55,182,12,148,132,230,99,196,69,98,194,48,10,131,102,17,26,73,251,150,233,91,182,175,30,180,122,251,208,28,140,177,112,36,140,45,38,97,104,209,114,18,41,210,45,90,22,69,226,37,76,234,176,121,250,242,205,147,26,41,152,212,121,201,180,192,232,184,112,163,129,160,0,84,192,136,4,211,98,152,140,0,194,16,131,17,102,161,10,134,102,205,198,192,4,67,95,178,100,244,170,10,83,145,241,43,80,162,65,111,196,40,185,37,134,73,196,65,37,49,2,25,17,38,68,149,46,51,237,163,23,141,222,62,157,60,209,136,220,168,88,113,12,48,164,99,40,37,26,119,17,47,214,140,20,73,146,4,230,105,187,121,87,255,229,73,197,205,22,6,38,73,42,12,4,64,136,144,225,217,136,32,191,110,132,173,34,12,140,129,204,25,202,197,164,111,170,176,124,250,230,109,1,115,242,180,220,162,7,183,116,31,93,116,139,152,141,103,32,46,114,26,38,146,152,150,230,197,12,139,86,15,39,102,205,209,224,110,196,191,220,228,13,146,136,227,190,180,104,17,236,22,65,143,45,217,184,210,231,54,97,176,90,110,152,130,206,98,40,134,132,56,162,43,61,144,148,99,196,185,136,146,34,73,162,121,134,73,6,153,103,230,169,106,145,236,142,146,171,177,211,130,242,238,134,141,14,227,239,176,48,206,83,175,165,153,206,16,131,17,52,148,145,239,38,206,148,217,71,31,98,12,16,233,187,237,72,155,81,37,146,34,219,48,140,66,154,210,130,173,40,23,145,100,193,218,184,138,100,145,76,102,52,235,0,225,196,128,8,44,162,78,84,110,17,20,165,172,17,170,72,218,73,70,24,100,176,170,78,24,150,54,146,75,5,242,18,99,14,52,239,78,51,106,70,73,168,92,228,12,42,193,156,241,12,32,129,204,41,255,190,249,86,56,234,52,27,182,120,244,6,151,194,16,3,19,185,248,235,208,184,166,22,17,134,145,41,35,17,166,157,171,22,92,16,6,79,37,60,75,32,131,248,59,235,164,48,2,219,240,40,217,8,140,141,83,76,230,65,102,193,55,107,35,48,202,87,63,226,239,72,22,55,18,106,49,196,64,226,69,146,72,254,164,114,198,36,197,216,7,81,125,126,140,118,31,98,151,83,76,174,23,35,18,195,36,27,138,235,79,11,0,22,25,38,12,42,192,82,225,192,218,230,137,100,152,72,180,144,4,93,98,85,184,161,85,230,94,122,104,57,95,193,248,19,12,117,73,124,19,153,234,230,137,109,165,209,248,84,81,11,163,56,114,49,91,196,96,227,37,75,153,98,138,137,145,154,50,219,12,51,155,244,137,134,188,160,10,3,182,56,151,20,187,129,11,24,17,35,233,40,236,98,136,242,36,80,1,126,42,74,176,128,98,104,35,133,126,13,195,45,76,2,51,213,196,72,178,220,133,186,117,229,73,134,68,125,218,1,195,83,182,64,210,136,69,96,73,30,141,216,129,162,4,85,184,152,100,159,98,143,178,76,112,154,143,51,205,48,203,164,163,140,92,101,153,210,143,38,141,72,82,74,37,141,235,36,216,168,132,13,35,73,144,161,72,24,136,180,80,174,220,142,12,80,136,57,58,153,83,70,18,183,46,98,75,152,97,228,177,78,30,92,217,49,186,232,14,63,66,44,163,61,139,226,110,57,142,86,108,42,12,114,217,177,58,75,202,36,121,47,26,250,166,13,242,166,101,148,97,241,216,237,184,56,59,12,28,40,21,131,114,148,75,154,113,24,208,63,146,132,151,163,94,206,39,98,182,52,95,72,161,225,60,170,55,12,219,207,108,183,182,124,242,17,152,196,124,168,163,71,159,63,151,46,14,63,177,152,219,206,197,21,36,237,40,210,184,118,161,202,240,180,206,208,113,38,32,51,235,255,137,179,122,190,134,70,41,71,197,227,111,228,180,93,119,105,191,28,39,61,105,60,57,81,228,197,249,233,149,243,77,170,248,134,31,226,76,130,45,223,106,215,192,148,119,149,100,228,35,122,71,123,10,122,190,67,44,134,125,132,40,144,3,10,98,110,246,29,48,132,129,68,93,89,86,90,64,39,9,174,249,72,125,60,241,204,199,24,19,145,99,197,239,117,248,51,27,73,24,227,52,41,169,203,58,177,201,136,152,80,69,22,122,117,164,92,71,209,66,36,106,195,23,171,92,101,30,248,232,74,109,220,69,32,205,97,15,63,140,138,140,171,62,34,185,24,24,132,40,143,138,200,48,150,55,15,94,96,66,12,203,210,146,180,114,194,25,106,213,35,47,167,97,154,75,98,247,66,110,185,206,141,177,59,12,80,34,34,38,48,41,75,42,244,168,14,59,222,69,148,132,68,72,33,18,154,35,72,192,32,52,60,230,163,129,235,202,213,95,120,193,68,167,44,68,49,248,81,155,69,190,83,18,195,12,36,36,199,138,210,68,174,130,9,97,12,74,18,195,208,73,215,200,120,168,255,104,101,144,36,104,83,99,183,212,104,54,147,176,48,69,135,41,83,150,86,179,32,138,64,228,36,29,201,89,205,24,129,9,70,168,32,57,187,40,90,209,138,56,15,103,204,131,104,184,138,74,196,22,161,136,13,125,76,36,102,3,9,76,46,72,50,145,144,71,101,17,161,142,85,236,166,44,216,40,195,116,164,35,99,103,122,146,63,22,138,97,149,110,52,155,42,249,35,3,74,166,168,49,204,81,74,7,23,49,143,232,37,131,112,47,17,28,88,4,194,40,73,204,67,11,183,194,138,18,229,113,21,206,9,35,24,180,177,218,46,22,153,29,193,124,12,49,231,140,23,81,92,180,34,151,108,207,87,194,240,75,155,120,99,177,154,204,7,72,225,172,22,144,50,97,172,200,76,234,117,102,99,227,42,111,192,78,195,24,235,32,95,217,2,129,200,85,29,233,225,141,56,22,107,140,32,213,51,180,100,40,104,65,193,8,198,46,64,197,14,206,5,45,18,133,64,74,6,1,249,69,152,56,43,91,36,123,232,70,46,58,196,206,193,6,62,59,73,31,72,177,170,15,101,172,255,64,97,49,68,37,236,212,248,58,178,150,228,6,39,35,12,118,52,135,9,170,84,101,30,186,73,213,36,155,20,21,169,236,234,54,242,24,104,48,160,18,12,94,236,98,17,236,96,77,45,2,228,206,195,148,52,18,248,148,168,59,41,149,148,41,97,194,144,88,225,13,108,48,86,45,144,238,227,107,57,249,73,182,86,98,206,146,184,4,13,98,168,204,103,187,229,186,198,200,168,79,236,185,218,68,106,25,24,21,80,161,35,116,81,138,230,132,234,166,124,220,149,115,237,16,198,46,118,81,11,133,46,130,16,69,237,36,182,40,24,195,219,245,34,46,43,138,9,161,202,230,161,174,8,3,19,201,18,198,215,62,90,217,106,101,102,24,40,171,223,164,82,137,134,207,118,87,82,55,120,157,177,226,120,44,38,249,106,24,117,67,10,115,108,26,219,3,45,104,84,194,224,28,59,228,17,180,190,238,194,22,126,85,68,59,88,35,201,97,161,44,40,140,32,159,165,74,251,208,207,18,37,166,142,109,110,23,37,65,140,141,141,145,186,223,172,135,253,64,178,74,53,162,65,71,255,221,117,9,89,87,122,178,75,177,236,59,216,217,194,173,172,19,17,21,108,129,181,42,115,10,168,182,120,87,248,6,99,30,122,236,237,34,4,75,136,191,90,13,36,39,49,202,200,254,19,134,76,64,198,191,7,129,73,217,168,148,81,187,117,17,99,95,163,110,233,170,53,210,83,118,72,12,56,74,9,134,125,32,134,178,182,20,70,218,185,23,138,170,66,15,244,124,165,196,48,160,194,114,6,9,39,92,73,101,29,236,8,42,59,218,33,75,222,46,194,175,90,72,230,90,82,199,194,237,108,193,118,193,162,212,98,108,201,18,4,75,133,55,12,70,95,146,213,167,15,172,238,227,108,152,122,97,74,192,68,147,40,131,246,187,56,112,105,164,173,151,63,76,68,79,24,95,65,202,69,128,56,207,128,50,15,190,6,221,237,130,56,39,83,82,131,1,183,114,178,160,92,92,50,16,196,220,64,18,152,184,49,127,20,83,96,144,80,73,137,118,19,198,232,118,34,104,203,246,72,31,6,76,103,76,104,50,108,208,166,161,209,221,218,143,120,83,167,17,72,177,133,132,250,24,255,6,70,52,7,21,244,240,139,129,136,195,138,60,248,186,14,169,204,178,147,76,180,8,156,63,100,145,23,109,1,71,167,105,155,131,210,102,78,73,60,140,37,51,234,139,115,121,65,140,248,160,144,215,156,57,114,61,78,74,41,149,164,4,54,254,198,4,119,209,112,6,28,152,243,187,50,88,169,178,29,186,28,114,225,241,93,86,35,149,22,6,105,21,172,140,168,197,10,221,197,60,192,61,75,91,252,201,41,47,230,69,51,215,214,157,146,56,40,214,146,200,196,103,37,241,29,181,80,39,25,93,220,73,160,239,221,107,20,22,4,180,35,28,54,77,140,157,137,52,112,23,180,170,84,209,120,227,168,33,236,76,132,96,156,178,146,48,198,108,157,162,109,81,24,235,208,173,34,106,209,46,94,192,164,16,97,184,90,225,62,212,193,184,168,13,207,118,222,146,164,80,182,133,207,146,164,37,183,26,6,38,136,49,45,211,205,156,90,155,89,6,232,0,252,100,208,234,40,37,147,200,68,202,5,94,246,13,155,204,165,67,7,64,203,250,41,29,220,172,75,18,70,140,147,243,255,20,199,14,191,18,136,95,81,89,68,33,146,230,113,78,91,228,81,173,54,25,229,50,177,231,42,30,6,34,47,97,73,63,29,68,202,140,185,29,171,60,81,70,114,45,204,93,73,76,2,19,121,199,196,36,208,144,9,208,250,124,49,39,131,244,120,13,112,150,89,253,165,240,3,227,203,85,174,130,140,197,19,213,28,185,101,68,129,0,21,122,19,157,55,18,223,49,109,171,199,51,12,52,20,133,63,56,24,143,75,80,54,25,170,192,93,148,110,223,7,79,234,147,242,218,223,60,246,121,207,59,119,123,254,115,74,158,44,142,113,52,203,148,146,161,15,52,231,214,121,33,154,255,242,132,161,160,117,244,85,176,125,205,146,48,80,132,152,98,132,171,163,146,45,136,4,233,1,131,86,234,60,166,97,171,254,168,159,178,75,167,69,96,4,97,80,6,142,241,62,240,27,180,155,168,7,101,248,57,214,75,131,215,67,57,188,235,185,148,43,193,110,185,189,160,64,184,97,169,25,205,17,154,168,112,49,6,122,19,225,219,63,131,242,191,90,80,4,167,0,20,82,251,8,42,255,201,146,15,233,143,253,32,9,98,144,132,195,24,45,25,0,45,185,232,16,0,155,4,156,216,42,13,36,35,14,140,6,33,17,56,217,251,55,154,120,189,148,227,185,207,34,191,247,83,182,195,216,189,18,147,136,171,8,154,131,82,158,121,24,145,218,224,156,173,208,45,193,90,166,2,89,150,144,187,186,1,9,185,137,176,136,198,112,163,24,144,4,123,26,45,85,250,172,135,138,166,136,96,48,32,249,62,40,36,37,104,232,49,99,27,182,214,67,191,217,147,61,218,123,3,115,66,65,177,147,180,86,153,145,171,184,4,169,80,156,174,216,68,197,49,40,248,218,5,198,171,5,55,196,136,66,216,193,41,146,136,44,161,7,136,192,46,146,208,7,180,233,172,148,232,172,54,122,15,143,130,194,202,66,31,66,91,134,205,67,3,99,139,50,52,192,59,148,123,3,73,232,185,52,248,57,161,19,175,195,48,11,229,72,192,231,9,6,198,113,147,174,120,147,253,43,42,221,178,175,222,26,192,181,88,13,181,64,138,231,10,131,85,36,137,240,226,49,98,200,51,215,217,130,214,255,131,166,248,81,143,211,209,152,233,186,197,81,10,169,94,132,141,69,116,131,73,168,199,55,176,71,217,83,191,110,121,29,150,74,17,125,58,141,121,202,135,118,8,134,55,193,43,99,194,141,253,107,7,206,225,173,24,19,16,166,160,146,228,251,136,164,233,164,7,116,50,49,160,7,37,137,33,217,227,22,181,113,137,143,66,148,119,212,170,249,200,12,34,28,70,127,3,70,76,72,3,94,156,132,55,64,131,49,240,57,131,88,12,100,59,171,24,56,0,186,128,129,51,208,162,121,192,4,249,154,65,55,65,179,80,132,47,61,170,5,73,112,67,70,210,156,29,148,191,199,24,134,241,136,33,16,137,136,23,146,20,136,56,39,115,186,42,105,17,73,144,170,143,205,80,134,76,176,66,159,147,4,55,240,202,94,244,162,94,52,75,20,116,167,131,16,8,0,96,148,49,156,135,96,184,13,99,146,135,133,204,45,55,19,42,220,146,49,153,42,142,150,209,156,228,203,14,78,249,60,37,164,148,74,203,132,162,208,174,87,99,4,237,170,162,139,153,66,249,200,202,7,235,26,94,255,156,71,96,68,131,128,51,203,158,131,73,49,72,3,100,60,153,21,48,139,137,123,158,187,66,72,54,180,70,133,98,7,2,145,37,201,19,16,205,137,169,0,60,144,192,156,48,148,3,199,168,164,148,97,112,61,152,80,155,73,168,150,182,123,76,92,220,202,104,208,199,70,163,76,43,52,54,124,4,198,195,176,158,239,26,142,179,56,60,172,32,190,131,172,70,221,90,164,90,216,175,193,154,200,171,115,182,94,65,76,39,179,170,100,72,37,74,121,181,183,169,176,144,210,137,222,20,52,32,193,170,178,188,57,238,178,199,148,236,57,159,59,0,9,33,139,179,130,129,21,32,11,225,16,34,230,97,7,226,163,198,220,170,47,191,218,5,48,88,36,29,100,205,149,16,16,106,75,154,97,176,136,85,178,42,222,216,174,127,131,13,152,64,3,122,200,9,244,132,6,243,132,76,41,164,135,52,192,17,214,27,182,150,4,29,42,3,47,24,32,139,248,116,41,225,64,8,129,116,78,121,136,175,103,212,45,55,179,5,153,10,185,164,92,137,150,113,10,57,161,18,84,139,77,48,255,89,22,0,19,173,216,251,36,223,153,4,32,225,9,202,194,80,234,234,9,82,82,6,245,172,66,154,40,184,30,56,70,151,2,0,175,19,139,188,33,145,187,98,209,220,34,170,23,245,171,90,112,193,73,90,139,137,116,193,63,49,156,9,139,8,110,98,52,29,161,20,175,172,146,8,21,63,174,60,82,94,43,82,68,201,72,159,243,80,63,228,174,130,91,12,251,148,144,153,84,146,42,17,26,100,96,7,124,40,42,94,0,172,221,34,53,139,8,57,45,104,170,40,186,209,69,178,29,51,229,40,52,5,29,238,218,60,136,25,6,158,176,197,56,229,53,172,250,154,104,136,178,74,125,157,60,5,45,28,248,137,27,48,81,24,56,128,54,154,145,101,17,62,23,67,179,52,67,212,68,213,75,73,74,155,1,25,6,92,11,76,48,33,20,49,16,134,69,3,147,219,140,132,94,208,212,10,229,84,183,59,50,122,168,135,45,225,174,146,248,9,129,147,148,159,240,18,19,101,17,133,248,162,101,169,13,22,59,205,54,227,45,234,236,160,208,211,130,73,137,146,12,145,56,169,255,248,164,205,162,24,66,153,71,207,98,196,171,146,174,99,117,187,243,169,183,125,216,18,117,242,72,83,197,1,84,5,138,3,16,136,155,164,19,21,187,18,188,98,7,69,8,154,163,92,164,152,2,137,149,91,11,144,80,137,40,105,135,92,229,5,148,185,29,237,162,61,69,75,201,97,224,64,20,202,42,120,189,55,13,181,143,202,152,73,31,0,47,52,200,215,160,136,79,85,253,24,224,136,11,182,218,34,172,8,74,193,114,195,163,202,14,182,128,137,153,101,18,1,156,10,237,170,48,126,187,29,70,203,4,122,227,154,245,225,88,40,156,172,155,80,134,207,226,143,207,42,56,164,181,73,24,8,0,47,169,86,141,152,17,122,50,34,169,216,5,69,80,4,55,147,49,39,153,217,231,35,9,155,189,209,92,229,150,34,84,189,148,96,86,208,202,4,101,104,204,55,29,90,13,220,202,144,154,132,165,189,1,146,157,178,124,141,79,178,104,90,168,5,10,27,216,195,21,235,28,173,109,188,108,100,4,70,184,1,1,97,154,223,249,36,187,249,162,202,224,172,159,243,34,216,255,200,132,205,160,208,209,105,219,91,92,31,244,185,208,100,64,3,73,33,43,146,61,8,19,229,155,24,32,18,188,29,137,79,242,11,18,33,42,54,236,21,229,59,27,241,128,23,108,202,85,216,128,35,131,251,192,40,163,183,141,169,143,141,173,92,183,35,201,248,56,148,205,196,215,48,240,1,72,155,86,62,21,221,62,37,22,48,80,177,162,249,16,206,233,43,69,48,197,2,5,23,183,177,136,5,129,136,179,90,27,126,83,57,188,227,202,222,221,221,172,252,38,202,58,90,124,53,184,18,221,136,248,252,163,227,21,8,87,179,29,4,105,7,188,36,4,48,32,132,223,153,164,99,249,55,195,241,24,235,43,182,238,218,145,93,179,80,221,253,222,91,12,52,30,33,52,218,155,50,240,186,1,224,64,85,21,48,81,126,37,12,95,42,9,14,33,151,230,93,93,208,83,157,27,253,36,76,40,141,206,250,64,208,98,176,220,229,154,0,198,208,35,195,137,41,140,134,97,240,1,215,169,73,133,64,78,3,32,22,178,48,128,152,56,11,141,224,143,51,120,223,78,34,208,88,255,171,181,73,48,28,203,233,72,49,200,59,30,209,216,18,18,225,56,69,79,66,11,73,77,144,201,195,136,79,208,61,222,141,16,3,23,230,8,109,57,199,121,192,173,169,123,72,200,89,142,10,44,136,11,202,176,152,32,96,143,5,224,34,238,205,211,171,174,105,201,132,220,187,201,105,61,222,24,8,128,179,250,35,188,125,36,15,129,216,133,109,165,115,180,157,144,137,86,33,174,174,183,37,99,120,205,137,35,227,192,104,161,61,85,101,96,160,32,139,156,81,203,150,114,90,24,152,96,192,34,144,27,147,139,118,97,69,134,93,187,106,241,84,184,251,227,202,5,97,8,35,100,43,162,214,3,216,158,54,30,186,131,104,23,53,195,97,131,233,5,57,57,199,180,237,137,220,213,216,11,221,228,239,197,220,203,253,77,33,161,61,101,76,29,188,141,35,190,153,17,198,57,205,149,208,195,97,32,6,32,97,159,14,164,15,249,24,227,89,110,91,239,179,229,30,177,137,81,34,6,175,52,198,239,178,1,228,116,100,97,72,134,118,160,135,209,219,181,41,108,187,155,168,55,249,113,16,29,102,254,227,160,5,225,67,241,212,16,142,230,245,33,29,69,49,161,243,89,6,56,157,44,119,52,103,115,14,90,182,141,101,91,142,30,211,233,192,211,91,31,121,85,178,90,190,231,130,166,46,105,185,92,113,154,15,14,228,24,66,123,87,128,110,59,112,50,232,137,134,204,128,78,102,174,106,176,162,37,180,34,53,33,138,246,104,13,244,145,64,86,148,248,8,63,157,40,231,143,70,233,148,86,233,149,102,233,123,11,8,0,59";
				var len = newByteArray.Split(',').Length;
                var arrBytes = new byte[len];

                var c = 0;
                foreach (var i in newByteArray.Split(','))
                {
                    arrBytes[c] = byte.Parse(i);
                    c++;
                }

                return Image.FromStream(WoW.IO.FromBytes(arrBytes));
            }
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
        private static bool Volley
        {
            get
            {
                var Volley = ConfigFile.ReadValue("HunterMarksman", "Volley").Trim();

                return Volley != "" && Convert.ToBoolean(Volley);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "Volley", value.ToString()); }
        }		
        private static bool PiercingShot
        {
            get
            {
                var PiercingShot = ConfigFile.ReadValue("HunterMarksman", "PiercingShot").Trim();

                return PiercingShot != "" && Convert.ToBoolean(PiercingShot);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "PiercingShot", value.ToString()); }
        }	
        private static bool Trickshot
        {
            get
            {
                var Trickshot = ConfigFile.ReadValue("HunterMarksman", "Trickshot").Trim();

                return Trickshot != "" && Convert.ToBoolean(Trickshot);
            }
            set { ConfigFile.WriteValue("HunterMarksman", "Trickshot", value.ToString()); }
        }			
    }
}



/*
[AddonDetails.db]
AddonAuthor=Vectarius
AddonName=myspellpriority
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,194386,Volley,D3
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
Aura,194386,Volley
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