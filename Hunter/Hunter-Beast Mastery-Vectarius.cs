// winifix@gmail.com
// ReSharper disable UnusedMember.Global

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class BMHunterVectarius : CombatRoutine
    {
		//Pet Control	
		private CheckBox HealPetBox;
		// Items
		private CheckBox KilJaedenBox;			
		// Races
		private CheckBox BloodElfBox;
		private CheckBox OrcBox;		
		// DEF cds
		private CheckBox ExhilarationBox;
		private CheckBox FeignDeathBox;
		private CheckBox AspectoftheTurtleBox;	

		private CheckBox CounterShotBox;		
		
		//dps cds
		private CheckBox AspectoftheWildBox;
		
		

				        private static bool KilJaeden
        {
            get
            {
                var KilJaeden = ConfigFile.ReadValue("HunterBeastmastery", "KilJaeden").Trim();

                return KilJaeden != "" && Convert.ToBoolean(KilJaeden);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "KilJaeden", value.ToString()); }
        }	
		
        private static bool HealPet
        {
            get
            {
                var HealPet = ConfigFile.ReadValue("HunterBeastmastery", "HealPet").Trim();

                return HealPet != "" && Convert.ToBoolean(HealPet);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "HealPet", value.ToString()); }
        }
		
		

		
        private static bool CounterShot
        {
            get
            {
                var CounterShot = ConfigFile.ReadValue("HunterBeastmastery", "CounterShot").Trim();

                return CounterShot != "" && Convert.ToBoolean(CounterShot);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "CounterShot", value.ToString()); }
        }	
		
        private static bool Exhilaration
        {
            get
            {
                var Exhilaration = ConfigFile.ReadValue("HunterBeastmastery", "Exhilaration").Trim();

                return Exhilaration != "" && Convert.ToBoolean(Exhilaration);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "Exhilaration", value.ToString()); }
        }	
		
        private static bool FeignDeath
        {
            get
            {
                var FeignDeath = ConfigFile.ReadValue("HunterBeastmastery", "FeignDeath").Trim();

                return FeignDeath != "" && Convert.ToBoolean(FeignDeath);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "FeignDeath", value.ToString()); }
        }	

        private static bool AspectoftheTurtle
        {
            get
            {
                var AspectoftheTurtle = ConfigFile.ReadValue("HunterBeastmastery", "AspectoftheTurtle").Trim();

                return AspectoftheTurtle != "" && Convert.ToBoolean(AspectoftheTurtle);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "AspectoftheTurtle", value.ToString()); }
        }				
		
        private static bool AspectoftheWild
        {
            get
            {
                var AspectoftheWild = ConfigFile.ReadValue("HunterBeastmastery", "AspectoftheWild").Trim();

                return AspectoftheWild != "" && Convert.ToBoolean(AspectoftheWild);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "AspectoftheWild", value.ToString()); }
        }		
		

		

        
      public override string Name
        {
            get { return "Hunter Beast Mastery"; }
        }

        
		 public override string Class
        {
            get { return "Hunter"; }
        }

        public override Form SettingsForm { get; set; }
		
		
        public override void Initialize()
        {
            
            SettingsForm = new Form
            {
                Text = "Beast Mastery Hunter",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 1000,
                Height = 650,
                ShowIcon = false
            };

			
			var lblTitle = new Label
            {
                Text =
                    "BM Hunter by Vectarius",
                Size = new Size(270, 14),
                Left = 61,
                Top = 1
	       };
			lblTitle.ForeColor = Color.Black;
			Font myFont = new Font(lblTitle.Font,FontStyle.Bold|FontStyle.Underline);
			lblTitle.Font = myFont;
            SettingsForm.Controls.Add(lblTitle);
			
			

			

			
						var lblTextBox3 = new Label
            {
                Text =
                    "Cooldowns",
                Size = new Size(200, 17),
                Left = 70,
                Top = 250
            };
			lblTextBox3.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox3);

			 
			var lblAspectoftheWildBox = new Label
            {
                Text =
                    "Aspect of the Wild",
                Size = new Size(270, 15),
                Left = 100,
                Top = 275
            };
			
			lblAspectoftheWildBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblAspectoftheWildBox);			
           
			var lblCounterShotBox = new Label
            {
                Text =
                    "Counter Shot",
                Size = new Size(270, 15),
                Left = 100,
                Top = 300
            };
			
			lblCounterShotBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblCounterShotBox);	

			var lblExhilarationBox = new Label
            {
                Text =
                    "Exhilaration",
                Size = new Size(270, 15),
                Left = 100,
                Top = 325
            };
			
			lblExhilarationBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblExhilarationBox);	

			var lblAspectoftheTurtleBox = new Label
            {
                Text =
                    "Aspect of the Turtle",
                Size = new Size(270, 15),
                Left = 100,
                Top = 375
            };
			
			lblAspectoftheTurtleBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblAspectoftheTurtleBox);	

			var lblFeignDeathBox = new Label
            {
                Text =
                    "Feign Death",
                Size = new Size(270, 15),
                Left = 100,
                Top = 350
            };
			
			lblFeignDeathBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblFeignDeathBox);		



					
			 
			var lblTextBox5 = new Label
            {
                Text =
                    "Pet Control",
                Size = new Size(200, 17),
                Left = 70,
                Top = 475
            };
			lblTextBox5.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox5);			 

						var lblTextBox6 = new Label
            {
                Text =
                    "Items",
                Size = new Size(200, 17),
                Left = 70,
                Top = 525
            };
			lblTextBox6.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox6);
			 



	

			var lblHealPetBox = new Label
            {
                Text =
                    "Heal Pet",
                Size = new Size(270, 15),
                Left = 100,
                Top = 500
            };
			
			lblHealPetBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblHealPetBox);	

			var lblKilJaedenBox = new Label
            {
                Text =
                    "Kil'Jaeden's Burning Wish",
                Size = new Size(270, 15),
                Left = 100,
                Top = 550
            };
			
			lblKilJaedenBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblKilJaedenBox);			
		   
			var lblDiscordBox = new Label
            {
                Text =
                    "Please report any issues on #Hunter",
                Size = new Size(270, 250),
                Left = 560,
                Top = 500
            };
			lblDiscordBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblDiscordBox);
			
			
			
			
			var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 5, Top = 575, Size = new Size(120, 31)};
			
			var cmdReadme = new Button {Text = "Macros! Use Them", Width = 65, Height = 25, Left = 125, Top = 575, Size = new Size(120, 31)};
			
 

//items
            KilJaedenBox = new CheckBox {Checked = KilJaeden, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 550};		
            SettingsForm.Controls.Add(KilJaedenBox);
//pet control			
			HealPetBox = new CheckBox {Checked = HealPet, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 500};			
            SettingsForm.Controls.Add(HealPetBox);
			
			// Checkboxes
            CounterShotBox = new CheckBox {Checked = CounterShot, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 300};		
            SettingsForm.Controls.Add(CounterShotBox);
			ExhilarationBox = new CheckBox {Checked = Exhilaration, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 325};			
            SettingsForm.Controls.Add(ExhilarationBox);
			FeignDeathBox = new CheckBox {Checked = FeignDeath, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 350};
            SettingsForm.Controls.Add(FeignDeathBox);
			
			AspectoftheTurtleBox = new CheckBox {Checked = AspectoftheTurtle, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 375};			
			            SettingsForm.Controls.Add(AspectoftheTurtleBox);		
			//dps cooldowns
            AspectoftheWildBox = new CheckBox {Checked = AspectoftheWild, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 275};
            SettingsForm.Controls.Add(AspectoftheWildBox);			

			
			
			CounterShotBox.Checked = CounterShot;	
			ExhilarationBox.Checked = Exhilaration;	
			FeignDeathBox.Checked = FeignDeath;	
			AspectoftheTurtleBox.Checked = AspectoftheTurtle;	
			
			AspectoftheWildBox.Checked = AspectoftheWild;

			

		
			
			//cmdSave

			
            KilJaedenBox.CheckedChanged += KilJaeden_Click;    
            HealPetBox.CheckedChanged += HealPet_Click;				
			
            AspectoftheWildBox.CheckedChanged += AspectoftheWild_Click;    
            ExhilarationBox.CheckedChanged += Exhilaration_Click; 
            CounterShotBox.CheckedChanged += CounterShot_Click;
            FeignDeathBox.CheckedChanged += FeignDeath_Click;
            AspectoftheTurtleBox.CheckedChanged += AspectoftheTurtle_Click;	
			
			
			cmdSave.Click += CmdSave_Click;
			cmdReadme.Click += CmdReadme_Click;
 
			
			SettingsForm.Controls.Add(cmdSave);
			SettingsForm.Controls.Add(cmdReadme);
			lblDiscordBox.BringToFront();		
			lblTextBox5.BringToFront();		
			lblTextBox6.BringToFront();				
			lblTitle.BringToFront();


			
            KilJaedenBox.BringToFront();	
            HealPetBox.BringToFront();				
			
            AspectoftheWildBox.BringToFront();	
            CounterShotBox.BringToFront();	
            ExhilarationBox.BringToFront();
            FeignDeathBox.BringToFront();
            AspectoftheTurtleBox.BringToFront();				
			

			
			
		}
			
			private void CmdSave_Click(object sender, EventArgs e)
        {


            KilJaeden = KilJaedenBox.Checked;		
            HealPet = HealPetBox.Checked;				
			
            AspectoftheWild = AspectoftheWildBox.Checked;		
            CounterShot = CounterShotBox.Checked;	
            Exhilaration = ExhilarationBox.Checked;
            FeignDeath = FeignDeathBox.Checked;
            AspectoftheTurtle = AspectoftheTurtleBox.Checked;			
			

			
			
			
			
            MessageBox.Show("Settings saved.", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }
		private void CmdReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                " make sure you make macros of Kill Command and Dire Frenzy/Beast /petattack",
                "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
		
//items
		private void KilJaeden_Click(object sender, EventArgs e)
        {
            KilJaeden = KilJaedenBox.Checked;
        }			
			
//pet control			
		private void HealPet_Click(object sender, EventArgs e)
        {
            HealPet = HealPetBox.Checked;
        }	

			
		private void CounterShot_Click(object sender, EventArgs e)
        {
            CounterShot = CounterShotBox.Checked;
        }			
			
        private void Exhilaration_Click(object sender, EventArgs e)
        {
            Exhilaration = ExhilarationBox.Checked;
        }
        private void FeignDeath_Click(object sender, EventArgs e)
        {
            FeignDeath = FeignDeathBox.Checked;
        }
        private void AspectoftheTurtle_Click(object sender, EventArgs e)
        {
            AspectoftheTurtle = AspectoftheTurtleBox.Checked;
        }			
			//dpscooldown
        private void AspectoftheWild_Click(object sender, EventArgs e)
        {
            AspectoftheWild = AspectoftheWildBox.Checked;
        }			

		
		
        public override void Stop()
        {
			
			
        }

        public override void Pulse()
        {
			if (DetectKeyPress.GetKeyState(0x76) < 0)
            {
                UseCooldowns = !UseCooldowns;
            }	
			
			if (combatRoutine.Type == RotationType.SingleTarget || combatRoutine.Type == RotationType.SingleTargetCleave)  
            {
			if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {	
                  if (WoW.CanCast("FeignDeath") && WoW.HealthPercent < 30 && FeignDeath && !WoW.IsSpellOnCooldown("FeignDeath") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("FeignDeath");
                        return;
                    }
                    if (WoW.CanCast("Exhilaration") && WoW.HealthPercent < 40 && Exhilaration && !WoW.IsSpellOnCooldown("Exhil") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("Exhilaration");
                        return;
                    }	
					if (WoW.CanCast("AspectoftheTurtle") && WoW.HealthPercent < 20 && AspectoftheTurtle && !WoW.IsSpellOnCooldown("AspectoftheTurtle") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("AspectoftheTurtle");
                        return;
                    }
					if (WoW.CanCast("Ancient Healing Potion") && WoW.HealthPercent < 20 && !WoW.IsSpellOnCooldown("Ancient Healing Potion") && WoW.HealthPercent != 0)
						{
							WoW.CastSpell("Ancient Healing Potion");
							return;
						}
					if (WoW.CanCast("Silkweave Bandage") && WoW.HealthPercent < 40 && WoW.PlayerHasBuff("Turtle") && !WoW.IsMoving && !WoW.PlayerHasDebuff("Bandaged") && WoW.HealthPercent != 0)
						{
							WoW.CastSpell("Silkweave Bandage");
							return;
						}
					if (!WoW.HasPet && WoW.CanCast("Wolf"))
					{
						WoW.CastSpell("Wolf") ;
						return;
					}
					/*if (WoW.PetHealthPercent <= 0 && WoW.CanCast("Phoenix"))
					{
						WoW.CastSpell("Phoenix") ;
						return;
					}
*/					
					if (WoW.PetHealthPercent <= 90 && HealPet && !WoW.PetHasBuff("Heal Pet") && WoW.CanCast("Revive Pet") && !WoW.IsMoving)
					{
						WoW.CastSpell("Heal Pet") ;
						return;
					}
					if (WoW.PetHealthPercent <= 0 && WoW.IsSpellOnCooldown("Phoenix") && WoW.CanCast("Revive Pet") && !WoW.IsMoving)
					{
						WoW.CastSpell("Revive Pet") ;
						return;
					}			
					if (WoW.CanCast("A Murder of Crows") 
						&& WoW.Talent(6)==1 
						&& WoW.Focus >= 25 
						&& WoW.IsSpellInRange("A Murder of Crows"))
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
                    if (WoW.TargetIsCasting)
                    {
                        if (WoW.CanCast("Counter Shot") 
						&& WoW.TargetIsCastingAndSpellIsInterruptible 
						&& WoW.TargetPercentCast >= 60 
						&& !WoW.IsSpellOnCooldown("Counter Shot") 
						&& !WoW.PlayerIsChanneling 
						&& !WoW.WasLastCasted("Counter Shot"))
                        {
                            WoW.CastSpell("Counter Shot");						
                            return;
                        }	
					}	
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& WoW.Talent(6) == 3)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }
					/*if (WoW.CanCast("Arcane Torrent") 
						
						&& !WoW.IsSpellOnCooldown ("Arcane Torrent")
						&& WoW.Race == BloodElf
						&& WoW.Focus <= 85)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }	
					
					if (WoW.CanCast("Blood Fury") 
						
						&& !WoW.IsSpellOnCooldown ("Blood Fury")
						&& WoW.Race ("Orc"))
                    {
                        WoW.CastSpell("Blood Fury");
                        return;
                    }	
*/					
					if (WoW.CanCast("Kil'jaeden's Burning Wish") 
						&& KilJaeden 
						&& !WoW.ItemOnCooldown("Kil'jaeden's Burning Wish") 
						&& !WoW.IsSpellOnCooldown("Kil'jaeden's Burning Wish"))  
                    {
                        WoW.CastSpell("Kil'jaeden's Burning Wish");
                        return;
                    }					
					if (WoW.CanCast("Bestial Wrath") 
						&& WoW.Focus >= 80 
						&& WoW.IsSpellInRange("Cobra Shot")						
						&& !WoW.PlayerHasBuff("AspectoftheTurtle")
						&& WoW.Talent(2) == 2
						&& !WoW.IsSpellOnCooldown("Dire Frenzy")) 
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }	
					if (WoW.CanCast("Bestial Wrath") 
						&& WoW.Focus >= 115 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.IsSpellOnCooldown("Kill Command")						
						&& !WoW.PlayerHasBuff("AspectoftheTurtle"))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }						
					if (WoW.CanCast("Bestial Wrath") 
						&& WoW.Focus >= 90 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& !WoW.IsSpellOnCooldown("Kill Command")
						&& !WoW.PlayerHasBuff("AspectoftheTurtle")) 
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }					
					if (WoW.CanCast("Aspect of the Wild") 
						&& !WoW.IsSpellOnCooldown("Aspect of the Wild") 
						&& UseCooldowns 
						&& WoW.PlayerHasBuff("Bestial Wrath") ) 
                    {
                        WoW.CastSpell("Aspect of the Wild");
						return;
                    }	
					if (WoW.CanCast("Dire Beast") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& (WoW.Talent(2) == 1 || WoW.Talent(2) == 3)
						&& !WoW.IsSpellOnCooldown("Dire Beast") 
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 3)
                    {
                        WoW.CastSpell("Dire Beast");
						if (WoW.CanCast("Titan's Thunder") 
							&& !WoW.IsSpellOnCooldown("Titan's Thunder"))
						{
                        WoW.CastSpell("Titan's Thunder");
                        return;
						}
					}
					if (WoW.CanCast("Chimaera Shot") 
						&& WoW.Focus <90
						&& WoW.IsSpellOnCooldown("Dire Frenzy")
						&& WoW.IsSpellOnCooldown("Kill Command")						
						&& WoW.CanCast("Chimaera Shot")
						&& WoW.Talent(2) == 3)
						{
                        WoW.CastSpell("Chimaera Shot");
                        return;
						}						
					if (WoW.CanCast("Titan's Thunder") 
						&& !WoW.IsSpellOnCooldown("Titan's Thunder") 
						&& !WoW.IsSpellOnCooldown("Dire Frenzy")
						&& WoW.CanCast("Dire Frenzy")
						&& WoW.Talent(2) == 2)
						{
                        WoW.CastSpell("Titan's Thunder");
						WoW.CastSpell("Dire Frenzy");
                        return;
						}						

                    if (WoW.CanCast("Dire Frenzy") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.IsSpellOnCooldown("Bestial Wrath") 
						&& WoW.PlayerSpellCharges("Dire Frenzy") >=2
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 3
						&& WoW.Talent(2) == 2)
                    {
                        WoW.CastSpell("Dire Frenzy");
						return;
                    }					
                    if (WoW.CanCast("Dire Frenzy") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.IsSpellOnCooldown("Bestial Wrath")
						&& ((WoW.PetHasBuff("Dire Frenzy") && WoW.PetBuffTimeRemaining ("Dire Frenzy") <=20) || !WoW.PetHasBuff("Dire Frenzy"))
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 30
						&& WoW.Talent(2) == 2)
                    {
                        WoW.CastSpell("Dire Frenzy");
						return;
                    }	
                    if (WoW.CanCast("Kill Command") 
						&& WoW.Focus >= 30 
						&& WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
					if (WoW.CanCast("Cobra Shot") 
						&& (WoW.Focus >= 50) 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.PlayerHasBuff("Bestial Wrath") 
						&& WoW.IsSpellOnCooldown("Kill Command") 
						&& WoW.SpellCooldownTimeRemaining("Kill Command") > 14)
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }	
					if (WoW.CanCast("Cobra Shot") 
						&& (WoW.Focus >= 32) 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.PlayerHasBuff("Bestial Wrath") 
						&& WoW.PlayerBuffTimeRemaining("Bestial Wrath") <= 20
						&& WoW.IsSpellOnCooldown("Kill Command") 
						&& WoW.SpellCooldownTimeRemaining("Kill Command") > 14)
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }						
					if (WoW.CanCast("Cobra Shot") 
						&& (WoW.Focus >= 90) 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.IsSpellOnCooldown("Bestial Wrath") 
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 1.3)
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }

                }
            }

            if (combatRoutine.Type == RotationType.AOE)
            {
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                  if (WoW.CanCast("FeignDeath") && WoW.HealthPercent < 30 && FeignDeath && !WoW.IsSpellOnCooldown("FeignDeath") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("FeignDeath");
                        return;
                    }
                    if (WoW.CanCast("Exhilaration") && WoW.HealthPercent < 40 && Exhilaration && !WoW.IsSpellOnCooldown("Exhil") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("Exhilaration");
                        return;
                    }	
					if (WoW.CanCast("AspectoftheTurtle") && WoW.HealthPercent < 20 && AspectoftheTurtle && !WoW.IsSpellOnCooldown("AspectoftheTurtle") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("AspectoftheTurtle");
                        return;
                    }
					if (WoW.CanCast("Ancient Healing Potion") && WoW.HealthPercent < 20 && !WoW.IsSpellOnCooldown("Ancient Healing Potion") && WoW.HealthPercent != 0)
						{
							WoW.CastSpell("Ancient Healing Potion");
							return;
						}
					if (WoW.CanCast("Silkweave Bandage") && WoW.HealthPercent < 40 && WoW.PlayerHasBuff("Turtle") && !WoW.IsMoving && !WoW.PlayerHasDebuff("Bandaged") && WoW.HealthPercent != 0)
						{
							WoW.CastSpell("Silkweave Bandage");
							return;
						}
					if (!WoW.HasPet && WoW.CanCast("Wolf"))
					{
						WoW.CastSpell("Wolf") ;
						return;
					}
					/*if (WoW.PetHealthPercent <= 0 && WoW.CanCast("Phoenix"))
					{
						WoW.CastSpell("Phoenix") ;
						return;
					}
*/					
					if (WoW.PetHealthPercent <= 90 && HealPet && !WoW.PetHasBuff("Heal Pet") && WoW.CanCast("Revive Pet") && !WoW.IsMoving)
					{
						WoW.CastSpell("Heal Pet") ;
						return;
					}
					if (WoW.PetHealthPercent <= 0 && WoW.IsSpellOnCooldown("Phoenix") && WoW.CanCast("Revive Pet") && !WoW.IsMoving)
					{
						WoW.CastSpell("Revive Pet") ;
						return;
					}			
					if (WoW.CanCast("A Murder of Crows") 
						&& WoW.Talent(6) == 1 
						&& WoW.Focus >= 25 
						&& WoW.IsSpellInRange("A Murder of Crows"))
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
                    if (WoW.TargetIsCasting)
                    {
                        if (WoW.CanCast("Counter Shot") 
						&& WoW.TargetIsCastingAndSpellIsInterruptible 
						&& WoW.TargetPercentCast >= 60 
						&& !WoW.IsSpellOnCooldown("Counter Shot") 
						&& !WoW.PlayerIsChanneling 
						&& !WoW.WasLastCasted("Counter Shot"))
                        {
                            WoW.CastSpell("Counter Shot");						
                            return;
                        }	
					}	
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& WoW.Talent(6) == 3)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }
					/*if (WoW.CanCast("Arcane Torrent") 
						
						&& !WoW.IsSpellOnCooldown ("Arcane Torrent")
						&& WoW.Race == BloodElf
						&& WoW.Focus <= 85)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }	
					
					if (WoW.CanCast("Blood Fury") 
						
						&& !WoW.IsSpellOnCooldown ("Blood Fury")
						&& WoW.Race ("Orc"))
                    {
                        WoW.CastSpell("Blood Fury");
                        return;
                    }	
*/					
					if (WoW.CanCast("Kil'jaeden's Burning Wish") 
						&& KilJaeden 
						&& !WoW.ItemOnCooldown("Kil'jaeden's Burning Wish") 
						&& !WoW.IsSpellOnCooldown("Kil'jaeden's Burning Wish"))  
                    {
                        WoW.CastSpell("Kil'jaeden's Burning Wish");
                        return;
                    }					
                    if (WoW.CanCast("Barrage") 
						&& WoW.Talent(6) == 2 
						&& !WoW.IsSpellOnCooldown("Barrage") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.Focus >= 60)
                    {
                        WoW.CastSpell("Barrage");
                        return;
                    }	
					if (WoW.CanCast("Chimaera Shot") 
						&& WoW.Focus <90
						&& WoW.IsSpellOnCooldown("Dire Frenzy")
						&& WoW.IsSpellOnCooldown("Kill Command")						
						&& WoW.CanCast("Chimaera Shot")
						&& WoW.Talent(2) == 3)
						{
                        WoW.CastSpell("Chimaera Shot");
                        return;
						}						
				
					if (WoW.CanCast("Bestial Wrath") 
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& !WoW.PlayerHasBuff("AspectoftheTurtle") 
						&& !WoW.IsSpellOnCooldown("Bestial Wrath"))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }					
					if (WoW.CanCast("Aspect of the Wild") 
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14 
						&& !WoW.IsSpellOnCooldown("Aspect of the Wild") 
						&& UseCooldowns 
						&& WoW.PlayerHasBuff("Bestial Wrath")) 
                    {
                        WoW.CastSpell("Aspect of the Wild");

						return;
                    }		
					if (WoW.CanCast("Multi-Shot") && (WoW.Focus >= 40) && WoW.IsSpellInRange("Cobra Shot") && (DetectKeyPress.GetKeyState(0x5A) < 0)) 
					{
                            WoW.CastSpell("Multi-Shot");
                            return;
                    }						
					if (WoW.CanCast("Multi-Shot") 
						&& WoW.Focus >= 40
						&& !WoW.PetHasBuff("Beast Cleave") 
						&& WoW.IsSpellInRange("Multi-Shot"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
                    if (WoW.CanCast("Multi-Shot") 
						&& WoW.Focus >= 40 
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") <= 14 
						&& WoW.IsSpellInRange("Multi-Shot"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
					if (WoW.CanCast("Dire Beast") 
						&& WoW.IsSpellInRange("Dire Beast") 
						&& WoW.PetHasBuff("Beast Cleave") 
						&& (WoW.Talent(2) == 1 || WoW.Talent(2) == 3)
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14 
						&& !WoW.IsSpellOnCooldown("Dire Beast") 
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 30)
                    {
                        WoW.CastSpell("Dire Beast");
						if (WoW.CanCast("Titan's Thunder") 
							&& !WoW.IsSpellOnCooldown("Titan's Thunder"))
						{
                        WoW.CastSpell("Titan's Thunder");
                        return;
						}				
					}
					if (WoW.CanCast("Titan's Thunder") 
						&& !WoW.IsSpellOnCooldown("Titan's Thunder") 
						&& WoW.IsSpellOnCooldown("Dire Frenzy")
						&& WoW.SpellCooldownTimeRemaining("Dire Frenzy") <=14
						&& WoW.Talent(2) == 2)
						{
                        WoW.CastSpell("Titan's Thunder");
                        return;
						}						
                    if (WoW.CanCast("Dire Frenzy") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.IsSpellOnCooldown("Bestial Wrath") 
						&& WoW.PlayerSpellCharges("Dire Frenzy") >=2
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 30
						&& WoW.Talent(2) == 2)
                    {
                        WoW.CastSpell("Dire Frenzy");
						return;
                    }	
                    if (WoW.CanCast("Dire Frenzy") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.PetHasBuff("Dire Frenzy") 
						&& WoW.PetBuffTimeRemaining ("Dire Frenzy") <=30
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14 						
						&& WoW.Talent(2) == 2)
                    {
                        WoW.CastSpell("Dire Frenzy");
						return;
                    }					
                    if (WoW.CanCast("Dire Frenzy") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.IsSpellOnCooldown("Bestial Wrath")
						&& ((WoW.PetHasBuff("Dire Frenzy") && WoW.PetBuffTimeRemaining ("Dire Frenzy") <=2) || !WoW.PetHasBuff("Dire Frenzy"))
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 30
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14						
						&& WoW.Talent(2) == 2)
                    {
                        WoW.CastSpell("Dire Frenzy");
						return;
                    }
                    if (WoW.CanCast("Dire Frenzy") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.IsSpellOnCooldown("Bestial Wrath") 
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 60
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14
						&& WoW.Talent(2) == 2
						&& WoW.IsSpellOnCooldown("Titan's Thunder")
						&& WoW.SpellCooldownTimeRemaining("Titan's Thunder") >=14)
                    {
                        WoW.CastSpell("Dire Frenzy");
						return;
                    }					
									


					if (WoW.CanCast("Kill Command") 
						&& WoW.Focus >= 55 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& !WoW.IsSpellOnCooldown("Kill Command")
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14) 
					{
                        WoW.CastSpell("Kill Command");
                        return;
                    }
                    if (WoW.CanCast("Cobra Shot") 
						&& (WoW.Focus >= 65)
						&& WoW.Talent(7) == 2			
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14 
						&& WoW.PlayerHasBuff("Bestial Wrath")
						&& WoW.SpellCooldownTimeRemaining("Kill Command") >= 30)
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
					if (WoW.CanCast("Cobra Shot") 
						&& WoW.Focus >= 110 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") >= 14 
						&& WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 14)
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }						
				}
            }
 

		}
	}

}
	

/*
[AddonDetails.db]
AddonAuthor=Vectarius
AddonName=myspellpriority
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,83245,Wolf,F1
Spell,120679,Dire Beast,D1
Spell,217200,Dire Frenzy,D1
Spell,193455,Cobra Shot,D3
Spell,2643,Multi-Shot,D4
Spell,34026,Kill Command,D2
Spell,19574,Bestial Wrath,D8
Spell,141894,A Murder of Crows,D5
Spell,120360,Barrage,D6
Spell,147362,Counter Shot,D0
Spell,193530,Aspect of the Wild,D9
Spell,20572,Blood Fury,F9
Spell,207068,Titan's Thunder,D7
Spell,5116,Concussive,None
Spell,109304,Exhilaration,V
Spell,186265,AspectoftheTurtle,G
Spell,5384,FeignDeath,F2
Spell,127834,Ancient Healing Potion,F5
Spell,143940,Silkweave Bandage,None
Spell,55709,Phoenix,F6
Spell,5512,Healthstone,F7
Spell,982,Revive Pet,X
Spell,142117,Potion Power,F10
Spell,146,Heal Pet,X
Spell,144259,Kil'jaeden's Burning Wish,F4
Spell,194386,Volley,F
Spell,80483,Arcane Torrent,F3
Spell,53209,Chimaera Shot,none
Aura,217200,Dire Frenzy
Aura,186265,AspectoftheTurtle
Aura,146,Heal Pet
Aura,11196,Bandaged
Aura,234143,Temptation
Aura,2825,Bloodlust
Aura,80353,Time Warp
Aura,90355,Ancient Hysteria
Aura,160452,Netherwinds
Aura,146614,Drums
Aura,32182,Heroism
Aura,229206,Potion Power
Aura,19574,Bestial Wrath
Aura,118455,Beast Cleave
Aura,193530,Aspect of the Wild
Aura,194386,Volley
Item,144259,Kil'jaeden's Burning Wish

*/