using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PixelMagic.Helpers;
using System.Threading;

namespace PixelMagic.Rotation.DeathKnight.DK

{
    public class DK_Blood_FmFlex : CombatRoutine
    {
        public string gcdTime = "0.7";
        public bool AddonEdited = false;

        private int bonesStack;
        private readonly CheckBox checkIsTalentBloodDrinker = new CheckBox();
        private readonly CheckBox checkIsTalentBoneStorm = new CheckBox();
        private int currentRunes;
        private CheckBox isCDDefEnableBox;
        private bool isMelee;
        private bool renewBones;
        private int runicPower;


        public override string Name
        {
            get { return "Blood DK by FmFlex"; }
        }

        public override string Class
        {
            get { return "Deathknight"; }
        }

        public override Form SettingsForm { get; set; }

        public static bool isCDDefEnable
        {
            get
            {
                var isCDDefEnable = ConfigFile.ReadValue("DKBlood", "isCDDefEnable").Trim();

                if (isCDDefEnable != "")
                {
                    return Convert.ToBoolean(isCDDefEnable);
                }

                return true;
            }
            set { ConfigFile.WriteValue("DKBlood", "isCDDefEnable", value.ToString()); }
        }


        public static bool isTalentBoneStorm
        {
            get
            {
                var isTalentBoneStorm = ConfigFile.ReadValue("DKBlood", "isTalentBoneStorm").Trim();
                if (isTalentBoneStorm != "")
                {
                    return Convert.ToBoolean(isTalentBoneStorm);
                }
                return true;
            }
            set { ConfigFile.WriteValue("DKBlood", "isTalentBoneStorm", value.ToString()); }
        }

        public static bool isTalentBloodDrinker
        {
            get
            {
                var isTalentBloodDrinker = ConfigFile.ReadValue("DKBlood", "isTalentBloodDrinker").Trim();
                if (isTalentBloodDrinker != "")
                {
                    return Convert.ToBoolean(isTalentBloodDrinker);
                }
                return true;
            }
            set { ConfigFile.WriteValue("DKBlood", "isTalentBloodDrinker", value.ToString()); }
        }



        public override void Initialize()
        {
            Log.Write("Welcome to Blood DK V1.3 by FmFlex - fixed by smartie", Color.Green);
            SettingsForm = new Form
            {
                Text = "Settings",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 400,
                Height = 400,
                ShowIcon = false,
                FormBorderStyle = FormBorderStyle.FixedSingle
            };
            SettingsForm.MaximizeBox = false;
            SettingsForm.MinimizeBox = false;

            var labelIsCDDefEnable = new Label //12; 114 LEFT is first value, Top is second.
            {
                Text = "Automaticaly use defensive CD  :",
                Size = new Size(180, 20), //81; 13
                Left = 12,
                Top = 114
            };
            labelIsCDDefEnable.Font = new Font("Arial", 9.0f);
            labelIsCDDefEnable.BackColor = Color.Black;
            labelIsCDDefEnable.ForeColor = Color.White;
            SettingsForm.Controls.Add(labelIsCDDefEnable); //113; 114 

            isCDDefEnableBox = new CheckBox { Checked = isCDDefEnable, TabIndex = 2, Size = new Size(15, 14), Left = 200, Top = 114 };
            //isCDDefEnableBox.Appearance = Appearance.Button;
            SettingsForm.Controls.Add(isCDDefEnableBox);

            checkIsTalentBoneStorm.AutoSize = true;
            checkIsTalentBoneStorm.Location = new Point(12, 28);
            checkIsTalentBoneStorm.Name = "checkIsTalentBoneStorm";
            checkIsTalentBoneStorm.Size = new Size(100, 28);
            checkIsTalentBoneStorm.TabIndex = 9;
            checkIsTalentBoneStorm.Text = "Bones Storm";
            checkIsTalentBoneStorm.UseVisualStyleBackColor = true;
            checkIsTalentBoneStorm.Checked = isTalentBoneStorm;
            SettingsForm.Controls.Add(checkIsTalentBoneStorm);


            checkIsTalentBloodDrinker.AutoSize = true;
            checkIsTalentBloodDrinker.Location = new Point(12, 45);
            checkIsTalentBloodDrinker.Name = "checkIsTalentBloodDrinker";
            checkIsTalentBloodDrinker.Size = new Size(100, 28);
            checkIsTalentBloodDrinker.TabIndex = 9;
            checkIsTalentBloodDrinker.Text = "Blooddrinker";
            checkIsTalentBloodDrinker.UseVisualStyleBackColor = true;
            checkIsTalentBloodDrinker.Checked = isTalentBloodDrinker;

            SettingsForm.Controls.Add(checkIsTalentBloodDrinker);
            isCDDefEnableBox.CheckedChanged += isCDDefEnable_Click;
            labelIsCDDefEnable.BringToFront();
            checkIsTalentBoneStorm.CheckedChanged += checkIsTalentBoneStorm_Click;
            checkIsTalentBloodDrinker.CheckedChanged += checkIsTalentBloodDrinker_Click;
        }


        private void isCDDefEnable_Click(object sender, EventArgs e)
        {
            isCDDefEnable = isCDDefEnableBox.Checked;
        }

        private void checkIsTalentBoneStorm_Click(object sender, EventArgs e)
        {
            isTalentBoneStorm = checkIsTalentBoneStorm.Checked;
        }

        private void checkIsTalentBloodDrinker_Click(object sender, EventArgs e)
        {
            isTalentBloodDrinker = checkIsTalentBloodDrinker.Checked;
        }


        public override void Stop()
        {
        }
        public static bool CanCastInRange(string spell)
        {
            return WoW.CanCast(spell, false, true, true, false, false);
        }

        public static bool CanCastNoRange(string spell)
        {
            return !WoW.IsSpellOnCooldown(spell);
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            renewBones = !WoW.PlayerHasBuff("Bone Shield") || WoW.PlayerBuffTimeRemaining("Bone Shield") <= 500;
            isMelee = WoW.CanCast("Marrowrend", false, false, true, false, false);
            bonesStack = WoW.PlayerBuffStacks("Bone Shield");
            currentRunes = WoW.CurrentRunes;
            runicPower = WoW.RunicPower;
            if (combatRoutine.Type == RotationType.SingleTarget || combatRoutine.Type == RotationType.SingleTargetCleave) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && WoW.TargetIsVisible)
                {
                    if (isCDDefEnable)
                        useCDDef();
                    if ((renewBones || bonesStack < 3) && isMelee)
                    {
                        if (currentRunes >= 2)
                        {
                            WoW.CastSpell("Marrowrend");
                            return;
                        }
                    }
                    if (WoW.CanCast("Blood Boil", false, true, false, true, false) && isMelee && !WoW.TargetHasDebuff("Blood Plague"))
                    {
                        WoW.CastSpell("Blood Boil");
                        return;
                    }
                    if (CanCastNoRange("Consumption") && isMelee)
                    {
                        WoW.CastSpell("Consumption");
                        return;
                    }
                    if (isTalentBloodDrinker && CanCastInRange("BD") && !renewBones && currentRunes >= 1)
                    {
                        WoW.CastSpell("BD");
                        return;
                    }
                    if (isMelee && WoW.PlayerHasBuff("Crimson Scourge") && WoW.TargetHealthPercent >= 10)
                    {
                        WoW.CastSpell("DnD");
                        return;
                    }
                    if (isMelee && runicPower >= 45 && ((WoW.PlayerHasBuff("Ossuary") && (runicPower >= 85 || WoW.HealthPercent < 80)) || WoW.HealthPercent < 50))
                    {
                        WoW.CastSpell("Death Strike");
                        return;
                    }
                    if (isMelee && currentRunes >= 2 && bonesStack <= 6)
                    {
                        WoW.CastSpell("Marrowrend");
                        return;
                    }
                    if (WoW.SpellCooldownTimeRemaining("DnD") == 0 && isMelee && currentRunes >= 2 && WoW.TargetHealthPercent >= 10 && !renewBones && bonesStack > 6)
                    {
                        WoW.CastSpell("DnD");
                        return;
                    }
                    if (isMelee && currentRunes >= 2 && !renewBones && bonesStack > 6)
                    {
                        WoW.CastSpell("Heart Strike");
                        return;
                    }
                    if (WoW.CanCast("Blood Boil", false, true, false, true, false) && isMelee)
                    {
                        WoW.CastSpell("Blood Boil");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && WoW.TargetIsVisible)
                {
                    if (isCDDefEnable)
                        useCDDef();
                    if ((renewBones || bonesStack < 3) && isMelee)
                    {
                        if (currentRunes >= 2)
                        {
                            WoW.CastSpell("Marrowrend");
                            return;
                        }
                    }
                    if (WoW.CanCast("Blood Boil", true, true, false, true, true) && isMelee && !WoW.TargetHasDebuff("Blood Plague"))
                    {
                        WoW.CastSpell("Blood Boil");
                        return;
                    }
                    if (isTalentBloodDrinker && CanCastInRange("BD") && WoW.HealthPercent <= 60 && !renewBones && currentRunes >= 1)
                    {
                        WoW.CastSpell("BD");
                        return;
                    }
                    if (CanCastNoRange("Consumption") && isMelee)
                    {
                        WoW.CastSpell("Consumption");
                        return;
                    }
                    if (isMelee && WoW.PlayerHasBuff("Crimson Scourge") && WoW.TargetHealthPercent >= 10)
                    {
                        WoW.CastSpell("DnD");
                        return;
                    }
                    if (isTalentBoneStorm && WoW.SpellCooldownTimeRemaining("Bonestorm") == 0 && isMelee && runicPower >= 100)
                    {
                        WoW.CastSpell("Bonestorm");
                        return;
                    }
                    if (isTalentBoneStorm && isMelee && runicPower >= 45 &&
                        ((runicPower >= 85 && WoW.SpellCooldownTimeRemaining("Bonestorm") >= 300) || WoW.HealthPercent < 70 || WoW.HealthPercent < 50))
                    {
                        WoW.CastSpell("Death Strike");
                        return;
                    }
                    if (!isTalentBoneStorm && isMelee && runicPower >= 45 && (runicPower >= 85 || WoW.HealthPercent < 70))
                    {
                        WoW.CastSpell("Death Strike");
                        return;
                    }

                    if (WoW.SpellCooldownTimeRemaining("DnD") == 0 && isMelee && currentRunes >= 1 && WoW.TargetHealthPercent >= 10 && !renewBones && bonesStack > 2)
                    {
                        WoW.CastSpell("DnD");
                        return;
                    }
                    if (isMelee && currentRunes >= 1 && !renewBones && bonesStack > 2)
                    {
                        WoW.CastSpell("Heart Strike");
                        return;
                    }
                    if (WoW.CanCast("Blood Boil", false, true, false, true, false) && isMelee)
                    {
                        WoW.CastSpell("Blood Boil");
                        return;
                    }
                    if (isTalentBloodDrinker && CanCastInRange("BD") && !renewBones && currentRunes >= 1)
                    {
                        WoW.CastSpell("BD");
                    }
                }
            }
        }

        public void useCDDef()
        {
            if (CanCastNoRange("Anti-Magic Shell") && WoW.HealthPercent < 70 && !WoW.IsSpellOnCooldown("Anti-Magic Shell"))
            {
                WoW.CastSpell("Anti-Magic Shell");
                return;
            }
            if (CanCastNoRange("Icebound Fortitude") && WoW.HealthPercent < 40 && !WoW.IsSpellOnCooldown("Icebound Fortitude"))
            {
                WoW.CastSpell("Icebound Fortitude");
                return;
            }
            if (CanCastNoRange("Vampiric Blood") && WoW.HealthPercent < 50 && !WoW.IsSpellOnCooldown("Vampiric Blood"))
            {
                WoW.CastSpell("Vampiric Blood");
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Tyalieva
AddonName=tyahelper
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,195182,Marrowrend,D1
Spell,50842,Blood Boil,D2
Spell,49998,Death Strike,D3
Spell,206930,Heart Strike,D4
Spell,205223,Consumption,D9
Spell,48707,Anti-Magic Shell,D6
Spell,55233,Vampiric Blood,D7
Spell,48792,Icebound Fortitude,D8
Spell,49028,Dancing Rune Weapon,None
Spell,43265,DnD,D5
Spell,47528,Mind Freeze,F
Spell,194844,Bonestorm,F9
Spell,206931,BD,F3
Aura,195181,Bone Shield
Aura,55078,Blood Plague
Aura,81136,Crimson Scourge
Aura,219788,Ossuary
*/
