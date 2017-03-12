//-TWonderchilds Shadow Priest
//-ToDo:
//          - AoE Rotation
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using PixelMagic.GUI;
using PixelMagic.Helpers;
using System;

namespace PixelMagic.Rotation
{
    public class TWonderchildSP2 : CombatRoutine
    {
        //-Constants----------------//
        // Spells
        private const string SHADOWFORM = "Shadowform";
        private const string VAMPIRIC_TOUCH = "Vampiric Touch";
        private const string VAMPIRIC_EMBRACE = "Vampiric Embrace";
        private const string SHADOW_WORD_PAIN = "Shadow Word: Pain";
        private const string SHADOW_WORD_DEATH = "Shadow Word: Death";
        private const string MIND_BLAST = "Mind Blast";
        private const string MIND_FLAY = "Mind Flay";
        private const string VOID_ERUPTION = "Void Eruption";
        private const string VOID_TORRENT = "Void Torrent";
        private const string VOID_BOLT = "Void Bolt";
        private const string POWER_WORD_SHIELD = "Power Word: Shield";
        private const string SHADOW_MEND = "Shadow Mend";
        private const string POWER_INFUSION = "Power Infusion";
        private const string SHADOW_FIEND = "Shadowfiend";
        private const string SILENCE = "Silence";

        // Buffs/Auras
        private const string SHADOWFORM_AURA = "Shadowform";
        private const string POWER_INFUSION_AURA = "Power Infusion";
        private const string VOIDFORM_AURA = "Voidform";
        //--------------------------//
        //-Form-Stuff---------------//
        // Cooldowns
        private CheckBox UseCDBox;

        private CheckBox PIBloodlustBox;
        private CheckBox PIStacksBox;
        private CheckBox PIWaitBox;
        private TextBox PIWaitText;
        private CheckBox SFWaitBox;
        private TextBox SFWaitText;
        private CheckBox SFPIBox;
        private CheckBox VoidTorrentBox;
        private TextBox SWDText;
        private TextBox PWSText;
        private TextBox VEText;
        private CheckBox SilenceBox;
        private RadioButton VoidTorrentRadio1;
        private RadioButton VoidTorrentRadio2;
        //--------------------------//

        private readonly Stopwatch interruptwatch = new Stopwatch();

        public override string Name => "TWonderchild's Shadow Priest";

        public override string Class => "Priest";


        public override Form SettingsForm
        {
            get;
            set;
        }
        //--------------------------//
        //-Init---------------------//
        public override void Initialize()
        {
            Log.DrawHorizontalLine();
            Log.WritePixelMagic("Welcome to TWonderchild's Shadow Priest", Color.Black);
            Log.DrawHorizontalLine();
            Log.WritePixelMagic("Please use the following Talents: http://eu.battle.net/wow/en/tool/talent-calculator#Xba!0100000", Color.Black);
            Log.WritePixelMagic("Surrender to Madness is not supported.", Color.Black);
            Log.WritePixelMagic("Check the ToDo-List before reporting bugs/requesting features", Color.Black);
            Log.WritePixelMagic("Cooldown Hotkey: F7", Color.Black);

            SettingsForm = new Form { Text = "Shadow Priest Settings", StartPosition = FormStartPosition.CenterScreen, Height = 340, Width = 360 };
            var labelCooldowns = new Label { Text = "Cooldown Usage", Size = new Size(180, 20), Left = 8, Top = 10 };
            SettingsForm.Controls.Add(labelCooldowns);
            PIBloodlustBox = new CheckBox { Checked = true, TabIndex = 1, Size = new Size(380, 20), Left = 25, Top = 30, Text = "Use Power Infusion with Bloodlust (Ignores other Conditions)" };
            SettingsForm.Controls.Add(PIBloodlustBox);
            PIStacksBox = new CheckBox { Checked = true, TabIndex = 1, Size = new Size(380, 20), Left = 25, Top = 50, Text = "Use Power Infusion at 20 Voidform Stacks" };
            SettingsForm.Controls.Add(PIStacksBox);
            PIWaitBox = new CheckBox { Checked = true, TabIndex = 1, Size = new Size(275, 20), Left = 25, Top = 70, Text = "Use PI and ignore Stacks if Shadow Fiend CD > " };
            SettingsForm.Controls.Add(PIWaitBox);
            PIWaitText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 70, Text = "20"};
            SettingsForm.Controls.Add(PIWaitText);
            SFPIBox = new CheckBox { Checked = true, TabIndex = 1, Size = new Size(380, 20), Left = 25, Top = 90, Text = "Use Shadow Fiend at 12 Sec Bloodlust/Power Infusion"};
            SettingsForm.Controls.Add(SFPIBox);
            SFWaitBox = new CheckBox { Checked = true, TabIndex = 1, Size = new Size(275, 20), Left = 25, Top = 110, Text = "Use Shadow Fiend if Power Infusion CD > " };
            SettingsForm.Controls.Add(SFWaitBox);
            SFWaitText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 110, Text = "20" };
            SettingsForm.Controls.Add(SFWaitText);
            VoidTorrentBox = new CheckBox { Checked = true, TabIndex = 1, Size = new Size(275, 20), Left = 25, Top = 130, Text = "Use Void Torrent" };
            SettingsForm.Controls.Add(VoidTorrentBox);
            var labelRotation = new Label { Text = "Rotation", Size = new Size(180, 20), Left = 8, Top = 150 };
            SettingsForm.Controls.Add(labelRotation);
            var labelSWD = new Label { Text = "Use 2nd SWD Stack if Insanity drops below ", Size = new Size(225, 20), Left = 8, Top = 170 };
            SettingsForm.Controls.Add(labelSWD);
            SWDText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 170, Text = "50" };
            SettingsForm.Controls.Add(SWDText);
            var labelOthers = new Label { Text = "Other spells", Size = new Size(180, 20), Left = 8, Top = 190 };
            SettingsForm.Controls.Add(labelOthers);
            var labelPWS = new Label { Text = "Use PWS at X% Health ", Size = new Size(225, 20), Left = 8, Top = 210 };
            SettingsForm.Controls.Add(labelPWS);
            PWSText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 210, Text = "40" };
            SettingsForm.Controls.Add(PWSText);
            var labelVE = new Label { Text = "Use Vampiric Embrace at X% Health ", Size = new Size(225, 20), Left = 8, Top = 230 };
            SettingsForm.Controls.Add(labelVE);
            VEText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 230, Text = "30" };
            SettingsForm.Controls.Add(VEText);
            SilenceBox = new CheckBox { Checked = true, TabIndex = 1, Size = new Size(380, 20), Left = 10, Top = 250, Text = "Auto Silence" };
            SettingsForm.Controls.Add(SilenceBox);
            VoidTorrentRadio1 = new RadioButton { TabIndex = 1, Size = new Size(200, 20), Left=10, Top=270, Text= "Use VT as CD",};
            SettingsForm.Controls.Add(VoidTorrentRadio1);
            VoidTorrentRadio2 = new RadioButton { Checked = true, TabIndex = 1, Size = new Size(200, 20), Left = 210, Top = 270, Text = "Use VT in Rota" };
            SettingsForm.Controls.Add(VoidTorrentRadio2);
        }
        //--------------------------//
        public override void Stop()
        {
            if (DetectKeyPress.GetKeyState(0x76) < 0)
                UseCooldowns = !UseCooldowns;
        }
        //-Pulse--------------------//
        public override void Pulse()
        {
            if (DetectKeyPress.GetKeyState(0x76) < 0)
                UseCooldowns = !UseCooldowns;
            if (WoW.IsInCombat)
                interruptwatch.Start();
            else
                return;

            if (combatRoutine.Type != RotationType.SingleTarget && combatRoutine.Type != RotationType.AOE) return;
            if (!WoW.HasTarget || !WoW.TargetIsEnemy) return;

            if (WoW.HealthPercent < int.Parse(PWSText.Text) && !WoW.PlayerHasBuff(POWER_WORD_SHIELD) && !WoW.IsSpellOnCooldown(POWER_WORD_SHIELD))
            {
                Log.Write("Health below " + PWSText.Text + "% - Using PWS now", Color.Red);
                SpellCast(POWER_WORD_SHIELD);
                return;
            }
            if (WoW.HealthPercent < int.Parse(VEText.Text) && !WoW.IsSpellOnCooldown(VAMPIRIC_EMBRACE))
            {
                Log.Write("Health below " + VEText.Text + "% - Using Vampiric Embrace now", Color.Red);
                SpellCast(VAMPIRIC_EMBRACE);
                return;
            }
            if (SilenceBox.Checked && WoW.TargetIsCastingAndSpellIsInterruptible && interruptwatch.ElapsedMilliseconds > 900)
            {
                if (!WoW.IsSpellOnCooldown(SILENCE))
                {
                    SpellCast(SILENCE);
                    interruptwatch.Reset();
                    interruptwatch.Start();
                    return;
                }
            }

            if (!(WoW.PlayerHasBuff(SHADOWFORM_AURA) || WoW.PlayerHasBuff(VOIDFORM_AURA)))
            {
                SpellCast(SHADOWFORM);
            }
            CooldownUsage();
            SingleTargetRotation();
        }

        private void SingleTargetRotation()
        {
            if (!WoW.PlayerHasBuff(VOIDFORM_AURA) && WoW.Insanity >= 65 && !WoW.IsMoving && DotsUp())
                SpellCast(VOID_ERUPTION);

            if(WoW.PlayerHasBuff(VOIDFORM_AURA) && DotsUp())
            {
                if(!WoW.IsMoving && VoidTorrentRadio2.Checked)
                    SpellCast(VOID_TORRENT);
                SpellCast(VOID_BOLT);
            }

            if (!WoW.IsMoving && DotsUp())
                SpellCast(MIND_BLAST);

            if(WoW.PlayerSpellCharges(SHADOW_WORD_DEATH) == 2 && WoW.Insanity <= 80 && WoW.TargetHealthPercent <= 20 && DotsUp())
                SpellCast(SHADOW_WORD_DEATH);
            if (WoW.PlayerSpellCharges(SHADOW_WORD_DEATH) == 1 && WoW.Insanity <= int.Parse(SWDText.Text) && WoW.TargetHealthPercent <= 20 && DotsUp())
                SpellCast(SHADOW_WORD_DEATH);

            if ((!WoW.TargetHasDebuff(VAMPIRIC_TOUCH) || WoW.TargetDebuffTimeRemaining(VAMPIRIC_TOUCH) <= 4) && !WoW.IsMoving && WoW.LastSpell!= VAMPIRIC_TOUCH) //Messy workaround to fix the double VT-Cast, since addon/BLizz API is returning weird values
                SpellCast(VAMPIRIC_TOUCH);
                
            if ((!WoW.TargetHasDebuff(SHADOW_WORD_PAIN) || WoW.TargetDebuffTimeRemaining(SHADOW_WORD_PAIN) <= 3))
                SpellCast(SHADOW_WORD_PAIN);
            
            if(WoW.TargetHasDebuff(SHADOW_WORD_PAIN) && WoW.TargetHasDebuff(VAMPIRIC_TOUCH) && !WoW.IsMoving && WoW.IsSpellOnCooldown(MIND_BLAST))
                SpellCast(MIND_FLAY);
        }

        private void SpellCast(string spellName)
        {
            if (WoW.PlayerIsCasting || (WoW.LastSpell == VOID_TORRENT && WoW.PlayerIsChanneling))
                return;
            if (spellName == MIND_FLAY && WoW.LastSpell == MIND_FLAY && WoW.PlayerIsChanneling)
                return;
            if (WoW.CanCast(spellName) && WoW.IsSpellInRange(SHADOW_WORD_PAIN))
            {
                WoW.CastSpell(spellName);
                if (WoW.IsSpellOnGCD(spellName))
                    Thread.Sleep(WoW.SpellCooldownTimeRemaining(spellName));
            }
        }

        private void CooldownUsage()
        {
            if (!UseCooldowns)
                return;
            if (PIBloodlustBox.Checked && CheckBloodlust() >= 20)
                SpellCast(POWER_INFUSION);
            if (PIStacksBox.Checked && WoW.PlayerHasBuff(VOIDFORM_AURA) && WoW.PlayerBuffStacks(VOIDFORM_AURA) >= 20)
                SpellCast(POWER_INFUSION);
            if (PIWaitBox.Checked && WoW.IsSpellOnCooldown(SHADOW_FIEND) && WoW.SpellCooldownTimeRemaining(SHADOW_FIEND) > int.Parse(PIWaitText.Text))
                SpellCast(POWER_INFUSION);
            if (SFPIBox.Checked && (WoW.PlayerBuffTimeRemaining(POWER_INFUSION_AURA) == 12 || CheckBloodlust() == 12))
                SpellCast(SHADOW_FIEND);
            if (SFWaitBox.Checked && WoW.IsSpellOnCooldown(POWER_INFUSION) && WoW.SpellCooldownTimeRemaining(POWER_INFUSION) > int.Parse(SFWaitText.Text))
                SpellCast(SHADOW_FIEND);
            if (!WoW.IsMoving && VoidTorrentRadio1.Checked && DotsUp() && VoidTorrentBox.Checked)
                SpellCast(VOID_TORRENT);
        }

        private int CheckBloodlust()
        {
            if (WoW.PlayerHasBuff("Bloodlust"))
                return WoW.PlayerBuffTimeRemaining("Bloodlust");
            if (WoW.PlayerHasBuff("Heroism"))
                return WoW.PlayerBuffTimeRemaining("Heroism");
            if (WoW.PlayerHasBuff("Time Warp"))
                return WoW.PlayerBuffTimeRemaining("Time Warp");
            if (WoW.PlayerHasBuff("Ancient Hysteria"))
                return WoW.PlayerBuffTimeRemaining("Ancient Hysteria");
            if (WoW.PlayerHasBuff("Netherwinds"))
                return WoW.PlayerBuffTimeRemaining("Netherwinds");
            if (WoW.PlayerHasBuff("Drums of Fury"))
                return WoW.PlayerBuffTimeRemaining("Drums of Fury");
            return 0;
        }

        private bool DotsUp()
        {
            if (WoW.TargetHasDebuff(SHADOW_WORD_PAIN) && WoW.TargetHasDebuff(VAMPIRIC_TOUCH))
                return true;
            else
                return false;
        }
    }
}
/*
[AddonDetails.db]
AddonAuthor=TWonderchild
AddonName=PostaI
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,232698,Shadowform,NumPad9
Spell,34914,Vampiric Touch,D1
Spell,15286,Vampiric Embrace,Numpad6
Spell,589,Shadow Word: Pain,D2
Spell,32379,Shadow Word: Death,X
Spell,8092,Mind Blast,D3
Spell,15407,Mind Flay,D4
Spell,228260,Void Eruption,Q
Spell,205065,Void Torrent,NumPad1
Spell,205448,Void Bolt,Q
Spell,17,Power Word: Shield,D5
Spell,186263,Shadow Mend,D0
Spell,10060,Power Infusion,NumPad2
Spell,34433,Shadowfiend,NumPad3
Spell,15487,Silence,E
Spell,47585,Dispersion,NumPad5
Aura,232698,Shadowform
Aura,194249,Voidform
Aura,34914,Vampiric Touch
Aura,589,Shadow Word: Pain
Aura,17,Power Word: Shield
Aura,10060,Power Infusion
Aura,197937,Lingering Insanity
Aura,2825,Bloodlust
Aura,32182,Heroism
Aura,80353,Time Warp
Aura,90355,Ancient Hysteria
Aura,160452,Netherwinds
Aura,178207,Drums of Fury
*/