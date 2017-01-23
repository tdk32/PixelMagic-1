// noaxeqtr@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody
// Reccomended Talent Build 1312112

using System;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class RetributionPaladin : CombatRoutine
    {
        private CheckBox BladeofWrathBox;
        private CheckBox BlindingLightBox;
        private CheckBox CavalierBox;
        private CheckBox ConsecrationBox;
        private CheckBox CrusadeBox;
        private CheckBox DivineHammerBox;
        private CheckBox DivineInterventionBox;
        private CheckBox DivinePurposeBox;
        private CheckBox ExecutionSentenceBox;
        private CheckBox EyeforanEyeBox;

        private CheckBox FinalVerdictBox;
        private CheckBox FistofJusticeBox;
        private CheckBox GreaterJudgmentBox;
        private CheckBox HolyWrathBox;
        private CheckBox JusticarsVengeanceBox;
        private CheckBox RepentanceBox;
        private CheckBox SealofLightBox;
        private CheckBox TheFireofJusticeBox;
        private CheckBox VirtuesBladeBox;
        private CheckBox WordofGloryBox;
        private CheckBox ZealBox;

        public override string Name
        {
            get { return "Retribution Paladin"; }
        }

        public override string Class
        {
            get { return "Paladin"; }
        }


        public override Form SettingsForm { get; set; }

        public static bool FinalVerdict
        {
            get
            {
                var FinalVerdict = ConfigFile.ReadValue("RetributionPaladin", "Final Verdict").Trim();

                return FinalVerdict != "" && Convert.ToBoolean(FinalVerdict);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Final Verdict", value.ToString()); }
        }

        private static bool ExecutionSentence
        {
            get
            {
                var ExecutionSentence = ConfigFile.ReadValue("RetributionPaladin", "Execution Sentence").Trim();

                return ExecutionSentence != "" && Convert.ToBoolean(ExecutionSentence);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Execution Sentence", value.ToString()); }
        }

        private static bool Consecration
        {
            get
            {
                var Consecration = ConfigFile.ReadValue("RetributionPaladin", "Consecration").Trim();

                return Consecration != "" && Convert.ToBoolean(Consecration);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Consecration", value.ToString()); }
        }

        private static bool TheFireofJustice
        {
            get
            {
                var TheFireofJustice = ConfigFile.ReadValue("RetributionPaladin", "The Fire of Justice").Trim();

                return TheFireofJustice != "" && Convert.ToBoolean(TheFireofJustice);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "The Fire of Justice", value.ToString()); }
        }

        private static bool Zeal
        {
            get
            {
                var Zeal = ConfigFile.ReadValue("RetributionPaladin", "Absolute Corruption").Trim();

                return Zeal != "" && Convert.ToBoolean(Zeal);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Absolute Corruption", value.ToString()); }
        }

        private static bool GreaterJudgment
        {
            get
            {
                var GreaterJudgment = ConfigFile.ReadValue("RetributionPaladin", "Mana Tap").Trim();

                return GreaterJudgment != "" && Convert.ToBoolean(GreaterJudgment);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Mana Tap", value.ToString()); }
        }

        private static bool FistofJustice
        {
            get
            {
                var FistofJustice = ConfigFile.ReadValue("RetributionPaladin", "Demonic Circle").Trim();

                return FistofJustice != "" && Convert.ToBoolean(FistofJustice);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Demonic Circle", value.ToString()); }
        }

        private static bool Repentance
        {
            get
            {
                var Repentance = ConfigFile.ReadValue("RetributionPaladin", "Mortal Coil").Trim();

                return Repentance != "" && Convert.ToBoolean(Repentance);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Mortal Coil", value.ToString()); }
        }

        private static bool BlindingLight
        {
            get
            {
                var BlindingLight = ConfigFile.ReadValue("RetributionPaladin", "Howl of Terror").Trim();

                return BlindingLight != "" && Convert.ToBoolean(BlindingLight);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Howl of Terror", value.ToString()); }
        }

        private static bool VirtuesBlade
        {
            get
            {
                var VirtuesBlade = ConfigFile.ReadValue("RetributionPaladin", "Siphon Life").Trim();

                return VirtuesBlade != "" && Convert.ToBoolean(VirtuesBlade);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Siphon Life", value.ToString()); }
        }

        private static bool BladeofWrath
        {
            get
            {
                var BladeofWrath = ConfigFile.ReadValue("RetributionPaladin", "Sow the Seeds").Trim();

                return BladeofWrath != "" && Convert.ToBoolean(BladeofWrath);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Sow the Seeds", value.ToString()); }
        }

        private static bool DivineHammer
        {
            get
            {
                var DivineHammer = ConfigFile.ReadValue("RetributionPaladin", "Soul Harvest").Trim();

                return DivineHammer != "" && Convert.ToBoolean(DivineHammer);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Soul Harvest", value.ToString()); }
        }

        private static bool JusticarsVengeance
        {
            get
            {
                var JusticarsVengeance = ConfigFile.ReadValue("RetributionPaladin", "Demon Skin").Trim();

                return JusticarsVengeance != "" && Convert.ToBoolean(JusticarsVengeance);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Demon Skin", value.ToString()); }
        }

        private static bool EyeforanEye
        {
            get
            {
                var EyeforanEye = ConfigFile.ReadValue("RetributionPaladin", "Burning Rush").Trim();

                return EyeforanEye != "" && Convert.ToBoolean(EyeforanEye);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Burning Rush", value.ToString()); }
        }

        private static bool WordofGlory
        {
            get
            {
                var WordofGlory = ConfigFile.ReadValue("RetributionPaladin", "Dark Pact").Trim();

                return WordofGlory != "" && Convert.ToBoolean(WordofGlory);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Dark Pact", value.ToString()); }
        }

        private static bool DivineIntervention
        {
            get
            {
                var DivineIntervention = ConfigFile.ReadValue("RetributionPaladin", "Grimoire of Supremacy").Trim();

                return DivineIntervention != "" && Convert.ToBoolean(DivineIntervention);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Grimoire of Supremacy", value.ToString()); }
        }

        private static bool Cavalier
        {
            get
            {
                var Cavalier = ConfigFile.ReadValue("RetributionPaladin", "Grimoire of Service").Trim();

                return Cavalier != "" && Convert.ToBoolean(Cavalier);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Grimoire of Service", value.ToString()); }
        }

        private static bool SealofLight
        {
            get
            {
                var SealofLight = ConfigFile.ReadValue("RetributionPaladin", "Grimoire of Sacrifice").Trim();

                return SealofLight != "" && Convert.ToBoolean(SealofLight);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Grimoire of Sacrifice", value.ToString()); }
        }

        private static bool DivinePurpose
        {
            get
            {
                var DivinePurpose = ConfigFile.ReadValue("RetributionPaladin", "Soul Effigy").Trim();

                return DivinePurpose != "" && Convert.ToBoolean(DivinePurpose);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Soul Effigy", value.ToString()); }
        }

        private static bool Crusade
        {
            get
            {
                var Crusade = ConfigFile.ReadValue("RetributionPaladin", "Phantom Singularity").Trim();

                return Crusade != "" && Convert.ToBoolean(Crusade);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Phantom Singularity", value.ToString()); }
        }

        private static bool HolyWrath
        {
            get
            {
                var HolyWrath = ConfigFile.ReadValue("RetributionPaladin", "Soul Conduit").Trim();

                return HolyWrath != "" && Convert.ToBoolean(HolyWrath);
            }
            set { ConfigFile.WriteValue("RetributionPaladin", "Soul Conduit", value.ToString()); }
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            //
            if (WoW.CanCast("Shield of Vengeance") && WoW.HealthPercent <= 40)
            {
                WoW.CastSpell("Shield of Vengeance");
                return;
            }

            //
            if (WoW.CanCast("Lay on Hands") && WoW.HealthPercent <= 20)
            {
                WoW.CastSpell("Lay on Hands");
                return;
            }

            if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Mount"))
            {
                //Crusade
                if (WoW.CanCast("Crusade") && Crusade && WoW.CurrentHolyPower >= 3 && WoW.IsSpellInRange("Templar Verdict") &&
                    (WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Time Warp") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums of War")))
                {
                    WoW.CastSpell("Crusade");
                    return;
                }

                //Avenging Wrath
                if (WoW.CanCast("Avenging Wrath") && WoW.CurrentHolyPower >= 3 && WoW.IsSpellInRange("Templar Verdict") &&
                    (WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Time Warp") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums of War")))
                {
                    WoW.CastSpell("Avenging Wrath");
                    return;
                }

                //Hammer of Justice
                if (Control.ModifierKeys == Keys.Shift && WoW.CanCast("Hammer of Justice"))
                {
                    WoW.CastSpell("Hammer of Justice");
                    return;
                }

                //Holy Wrath
                if (WoW.CanCast("Holy Wrath") && HolyWrath && WoW.HealthPercent <= 40 && WoW.IsSpellInRange("Templar Verdict"))
                {
                    WoW.CastSpell("Holy Wrath");
                    return;
                }

                //Single Target Rotation
                if (combatRoutine.Type == RotationType.SingleTarget)
                {
                    //Execution Sentence
                    if (WoW.CanCast("Execution Sentence") && ExecutionSentence && WoW.TargetHasDebuff("Judgement") && WoW.TargetDebuffTimeRemaining("Judgement") >= 6.5)
                    {
                        WoW.CastSpell("Execution Sentence");
                        return;
                    }

                    //Justicar's Vengeance
                    if (WoW.CanCast("Justicars Vengeance") && JusticarsVengeance && WoW.PlayerHasBuff("Divine Purpose") && WoW.IsSpellInRange("Templar Verdict"))
                    {
                        WoW.CastSpell("Justicars Vengeance");
                        return;
                    }

                    //Templar's Verdict
                    if ((WoW.CurrentHolyPower >= 3 || WoW.PlayerHasBuff("Divine Purpose") || (WoW.CurrentHolyPower >= 2 && WoW.PlayerHasBuff("The Fires of Justice"))) &&
                        WoW.CanCast("Templar Verdict") && WoW.IsSpellInRange("Templar Verdict") && WoW.TargetHasDebuff("Judgement") && WoW.TargetDebuffTimeRemaining("Judgement") >= 0.5)
                    {
                        WoW.CastSpell("Templar Verdict");
                        return;
                    }

                    //Judgement
                    if (WoW.CanCast("Judgement") && WoW.CurrentHolyPower >= 3)
                    {
                        WoW.CastSpell("Judgement");
                        return;
                    }

                    //Wake of Ashes
                    if (WoW.CurrentHolyPower == 0 && WoW.CanCast("Wake of Ashes") && WoW.IsSpellInRange("Templar Verdict"))
                    {
                        WoW.CastSpell("Wake of Ashes");
                        return;
                    }

                    //Blade of Justice
                    if (WoW.CanCast("Blade of Justice") && !DivineHammer && WoW.CurrentHolyPower <= 3)
                    {
                        WoW.CastSpell("Blade of Justice");
                        return;
                    }

                    //Divine Hammer
                    if (WoW.CanCast("Divine Hammer") && DivineHammer && WoW.CurrentHolyPower <= 3)
                    {
                        WoW.CastSpell("Divine Hammer");
                        return;
                    }

                    //Crusader Strike
                    if (WoW.CurrentHolyPower < 5 && !Zeal && WoW.PlayerSpellCharges("Crusader Strike") >= 1 && WoW.CanCast("Crusader Strike"))
                    {
                        WoW.CastSpell("Crusader Strike");
                        return;
                    }

                    //Zeal
                    if (WoW.CurrentHolyPower < 5 && Zeal && WoW.PlayerSpellCharges("Zeal") >= 1 && WoW.CanCast("Zeal"))
                    {
                        WoW.CastSpell("Zeal");
                        return;
                    }

                    //Consecration
                    if (WoW.CanCast("Consecration") && Consecration && WoW.IsSpellInRange("Templar Verdict"))
                    {
                        WoW.CastSpell("Consecration");
                        return;
                    }
                }

                //AoE Rotation = 3+ Targets
                if (combatRoutine.Type == RotationType.AOE)
                {
                    //Divine Storm
                    if ((WoW.CurrentHolyPower >= 3 || WoW.PlayerHasBuff("Divine Purpose") || (WoW.CurrentHolyPower >= 2 && WoW.PlayerHasBuff("The Fires of Justice"))) &&
                        WoW.CanCast("Divine Storm") && WoW.IsSpellInRange("Templar Verdict"))
                    {
                        WoW.CastSpell("Divine Storm");
                        return;
                    }

                    //Judgement
                    if (WoW.CanCast("Judgement"))
                    {
                        WoW.CastSpell("Judgement");
                        return;
                    }

                    //Wake of Ashes
                    if (WoW.CurrentHolyPower == 0 && WoW.CanCast("Wake of Ashes") && WoW.IsSpellInRange("Templar Verdict"))
                    {
                        WoW.CastSpell("Wake of Ashes");
                        return;
                    }

                    //Consecration
                    if (WoW.CanCast("Consecration") && Consecration && WoW.IsSpellInRange("Templar Verdict"))
                    {
                        WoW.CastSpell("Consecration");
                        return;
                    }

                    //Blade of Justice
                    if (WoW.CanCast("Blade of Justice") && !DivineHammer && WoW.CurrentHolyPower <= 3)
                    {
                        WoW.CastSpell("Blade of Justice");
                        return;
                    }

                    //Divine Hammer
                    if (WoW.CanCast("Divine Hammer") && DivineHammer && WoW.CurrentHolyPower <= 3)
                    {
                        WoW.CastSpell("Divine Hammer");
                        return;
                    }

                    //Crusader Strike
                    if (WoW.CurrentHolyPower < 5 && !Zeal && WoW.PlayerSpellCharges("Crusader Strike") >= 1 && WoW.CanCast("Crusader Strike"))
                    {
                        WoW.CastSpell("Crusader Strike");
                        return;
                    }

                    //Zeal
                    if (WoW.CurrentHolyPower < 5 && Zeal && WoW.PlayerSpellCharges("Zeal") >= 1 && WoW.CanCast("Zeal"))
                    {
                        WoW.CastSpell("Zeal");
                        return;
                    }
                }

                if (combatRoutine.Type == RotationType.SingleTargetCleave) //Cleave rotation = 2 targets
                {
                    // Do Single Target Cleave stuff here if applicable else ignore this one
                }
            }
        }

        public override void Initialize()
        {
            Log.Write("READ ME:", Color.Red);
            Log.Write("Select your talents from the Rotation settings menu. Some talents won't impact your rotation, they are there for future use.", Color.Red);
            Log.Write("Please don't select more than 1 talent on each row.", Color.Red);
            Log.Write("After selecting your talents reload the rotation before starting it.", Color.Red);
            Log.Write("Crusade/Avenging Wrath autocasts only if you have Bloodlust and 3 or more holy power otherwise it's manual", Color.Red);
            Log.Write("Press the Shift (try both Shift Keys) key to cast Hammer of Justice", Color.Red);
            Log.Write("Autocast Shield of Vengeance and Holy Wrath under 40% health and Lay on Hands under 20%", Color.Red);
            Log.Write("Justicar's Vengeance talent only works with Divine Purpose procs (Icy Veins)", Color.Red);
            Log.Write("Talents that are not implemented: Repentance, Blinding Light, Eye for an Eye, Word of Glory, Seal of Light", Color.Red);
            Log.Write("The rotation is based on Icy Veins and Noxic data and will not start if you are mounted on the Crimson Water Strider (for questing).", Color.Red);
            WoW.Speak("Welcome to PixelMagic Retribution Paladin Rotation");
            Log.Write("Selected Talents:", Color.Blue);
            Log.Write(
                "Final Verdict = " + FinalVerdict + "                                                  " + "Execution Sentence = " + ExecutionSentence +
                "                                                 " + "Consecration = " + Consecration, Color.Blue);
            Log.Write(
                "The Fire of Justice = " + TheFireofJustice + "                                         " + "Zeal = " + Zeal +
                "                                                                            " + "Greater Judgment = " + GreaterJudgment, Color.Blue);
            Log.Write(
                "Fistof Justice = " + FistofJustice + "                                                  " + "Repentance = " + Repentance +
                "                                                               " + "Blinding Light = " + BlindingLight, Color.Blue);
            Log.Write(
                "Virtues Blade = " + VirtuesBlade + "                                                 " + "Blade of Wrath = " + BladeofWrath +
                "                                                          " + "Divine Hammer = " + DivineHammer, Color.Blue);
            Log.Write(
                "Justicar's Vengeance = " + JusticarsVengeance + "                                     " + "Eye for an Eye = " + EyeforanEye +
                "                                                            " + "Word of Glory = " + WordofGlory, Color.Blue);
            Log.Write(
                "Divine Intervention = " + DivineIntervention + "                                      " + "Cavalier = " + Cavalier +
                "                                                                       " + "Seal of Light = " + SealofLight, Color.Blue);
            Log.Write(
                "Divine Purpose = " + DivinePurpose + "                                             " + "Crusade = " + Crusade +
                "                                                                       " + "Holy Wrath = " + HolyWrath, Color.Blue);

            SettingsForm = new Form {Text = "Talents", StartPosition = FormStartPosition.CenterScreen, Width = 555, Height = 350, ShowIcon = true};

            var lblFinalVerdictText = new Label {Text = "Final Verdict", Size = new Size(150, 15), Left = 30, Top = 30};
            SettingsForm.Controls.Add(lblFinalVerdictText);

            FinalVerdictBox = new CheckBox {Checked = FinalVerdict, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 30};
            SettingsForm.Controls.Add(FinalVerdictBox);

            var lblExecutionSentenceText = new Label {Text = "Execution Sentence", Size = new Size(150, 15), Left = 210, Top = 30};
            SettingsForm.Controls.Add(lblExecutionSentenceText);

            ExecutionSentenceBox = new CheckBox {Checked = ExecutionSentence, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 30};
            SettingsForm.Controls.Add(ExecutionSentenceBox);

            var lblConsecrationText = new Label {Text = "Consecration", Size = new Size(150, 15), Left = 390, Top = 30};
            SettingsForm.Controls.Add(lblConsecrationText);

            ConsecrationBox = new CheckBox {Checked = Consecration, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 30};
            SettingsForm.Controls.Add(ConsecrationBox);

            var lblTheFireofJusticeText = new Label {Text = "The Fire of Justice", Size = new Size(150, 15), Left = 30, Top = 60};
            SettingsForm.Controls.Add(lblTheFireofJusticeText);

            TheFireofJusticeBox = new CheckBox {Checked = TheFireofJustice, TabIndex = 8, Size = new Size(15, 15), Left = 15, Top = 60};
            SettingsForm.Controls.Add(TheFireofJusticeBox);

            var lblZealText = new Label {Text = "Zeal", Size = new Size(150, 15), Left = 210, Top = 60};
            SettingsForm.Controls.Add(lblZealText);

            ZealBox = new CheckBox {Checked = Zeal, TabIndex = 10, Size = new Size(15, 15), Left = 195, Top = 60};
            SettingsForm.Controls.Add(ZealBox);

            var lblGreaterJudgmentText = new Label {Text = "Greater Judgment", Size = new Size(150, 15), Left = 390, Top = 60};
            SettingsForm.Controls.Add(lblGreaterJudgmentText);

            GreaterJudgmentBox = new CheckBox {Checked = GreaterJudgment, TabIndex = 12, Size = new Size(15, 15), Left = 375, Top = 60};
            SettingsForm.Controls.Add(GreaterJudgmentBox);

            var lblFistofJusticeText = new Label {Text = "Fist of Justice", Size = new Size(150, 15), Left = 30, Top = 90};
            SettingsForm.Controls.Add(lblFistofJusticeText);

            FistofJusticeBox = new CheckBox {Checked = FistofJustice, TabIndex = 12, Size = new Size(15, 15), Left = 15, Top = 90};
            SettingsForm.Controls.Add(FistofJusticeBox);

            var lblRepentanceText = new Label {Text = "Repentance", Size = new Size(150, 15), Left = 210, Top = 90};
            SettingsForm.Controls.Add(lblRepentanceText);

            RepentanceBox = new CheckBox {Checked = Repentance, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 90};
            SettingsForm.Controls.Add(RepentanceBox);

            var lblBlindingLightText = new Label {Text = "Blinding Light", Size = new Size(150, 15), Left = 390, Top = 90};
            SettingsForm.Controls.Add(lblBlindingLightText);

            BlindingLightBox = new CheckBox {Checked = BlindingLight, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 90};
            SettingsForm.Controls.Add(BlindingLightBox);

            var lblVirtuesBladeText = new Label {Text = "Virtue's Blade", Size = new Size(150, 15), Left = 30, Top = 120};
            SettingsForm.Controls.Add(lblVirtuesBladeText);

            VirtuesBladeBox = new CheckBox {Checked = VirtuesBlade, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 120};
            SettingsForm.Controls.Add(VirtuesBladeBox);

            var lblBladeofWrathText = new Label {Text = "Blade of Wrath", Size = new Size(150, 15), Left = 210, Top = 120};
            SettingsForm.Controls.Add(lblBladeofWrathText);

            BladeofWrathBox = new CheckBox {Checked = BladeofWrath, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 120};
            SettingsForm.Controls.Add(BladeofWrathBox);

            var lblDivineHammerText = new Label {Text = "Divine Hammer", Size = new Size(150, 15), Left = 390, Top = 120};
            SettingsForm.Controls.Add(lblDivineHammerText);

            DivineHammerBox = new CheckBox {Checked = DivineHammer, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 120};
            SettingsForm.Controls.Add(DivineHammerBox);

            var lblJusticarsVengeanceText = new Label {Text = "Justicar's Vengeance", Size = new Size(150, 15), Left = 30, Top = 150};
            SettingsForm.Controls.Add(lblJusticarsVengeanceText);

            JusticarsVengeanceBox = new CheckBox {Checked = JusticarsVengeance, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 150};
            SettingsForm.Controls.Add(JusticarsVengeanceBox);

            var lblEyeforanEyeText = new Label {Text = "Eye for an Eye", Size = new Size(150, 15), Left = 210, Top = 150};
            SettingsForm.Controls.Add(lblEyeforanEyeText);

            EyeforanEyeBox = new CheckBox {Checked = EyeforanEye, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 150};
            SettingsForm.Controls.Add(EyeforanEyeBox);

            var lblWordofGloryText = new Label {Text = "Word of Glory", Size = new Size(150, 15), Left = 390, Top = 150};
            SettingsForm.Controls.Add(lblWordofGloryText);

            WordofGloryBox = new CheckBox {Checked = WordofGlory, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 150};
            SettingsForm.Controls.Add(WordofGloryBox);

            var lblDivineInterventionText = new Label {Text = "Divine Intervention", Size = new Size(150, 15), Left = 30, Top = 180};
            SettingsForm.Controls.Add(lblDivineInterventionText);

            DivineInterventionBox = new CheckBox {Checked = DivineIntervention, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 180};
            SettingsForm.Controls.Add(DivineInterventionBox);

            var lblCavalierText = new Label {Text = "Cavalier", Size = new Size(150, 15), Left = 210, Top = 180};
            SettingsForm.Controls.Add(lblCavalierText);

            CavalierBox = new CheckBox {Checked = Cavalier, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 180};
            SettingsForm.Controls.Add(CavalierBox);

            var lblSealofLightText = new Label {Text = "Seal of Light", Size = new Size(150, 15), Left = 390, Top = 180};
            SettingsForm.Controls.Add(lblSealofLightText);

            SealofLightBox = new CheckBox {Checked = SealofLight, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 180};
            SettingsForm.Controls.Add(SealofLightBox);

            var lblDivinePurposeText = new Label {Text = "Divine Purpose", Size = new Size(150, 15), Left = 30, Top = 210};
            SettingsForm.Controls.Add(lblDivinePurposeText);

            DivinePurposeBox = new CheckBox {Checked = DivinePurpose, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 210};
            SettingsForm.Controls.Add(DivinePurposeBox);

            var lblCrusadeText = new Label {Text = "Crusade", Size = new Size(150, 15), Left = 210, Top = 210};
            SettingsForm.Controls.Add(lblCrusadeText);

            CrusadeBox = new CheckBox {Checked = Crusade, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 210};
            SettingsForm.Controls.Add(CrusadeBox);

            var lblHolyWrathText = new Label {Text = "Holy Wrath", Size = new Size(150, 15), Left = 390, Top = 210};
            SettingsForm.Controls.Add(lblHolyWrathText);

            HolyWrathBox = new CheckBox {Checked = HolyWrath, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 210};
            SettingsForm.Controls.Add(HolyWrathBox);

            //Save Button
            var cmdSave = new Button {Text = "Save", Width = 40, Height = 20, Left = 445, Top = 270, Size = new Size(80, 30)};

            FinalVerdictBox.Checked = FinalVerdict;
            ExecutionSentenceBox.Checked = ExecutionSentence;
            ConsecrationBox.Checked = Consecration;
            TheFireofJusticeBox.Checked = TheFireofJustice;
            ZealBox.Checked = Zeal;
            GreaterJudgmentBox.Checked = GreaterJudgment;
            FistofJusticeBox.Checked = FistofJustice;
            RepentanceBox.Checked = Repentance;
            BlindingLightBox.Checked = BlindingLight;
            VirtuesBladeBox.Checked = VirtuesBlade;
            BladeofWrathBox.Checked = BladeofWrath;
            DivineHammerBox.Checked = DivineHammer;
            JusticarsVengeanceBox.Checked = JusticarsVengeance;
            EyeforanEyeBox.Checked = EyeforanEye;
            WordofGloryBox.Checked = WordofGlory;
            DivineInterventionBox.Checked = DivineIntervention;
            CavalierBox.Checked = Cavalier;
            SealofLightBox.Checked = SealofLight;
            DivinePurposeBox.Checked = DivinePurpose;
            CrusadeBox.Checked = Crusade;
            HolyWrathBox.Checked = HolyWrath;

            cmdSave.Click += CmdSave_Click;
            FinalVerdictBox.CheckedChanged += FinalVerdict_Click;
            ExecutionSentenceBox.CheckedChanged += ExecutionSentence_Click;
            ConsecrationBox.CheckedChanged += Consecration_Click;
            TheFireofJusticeBox.CheckedChanged += TheFireofJustice_Click;
            ZealBox.CheckedChanged += Zeal_Click;
            GreaterJudgmentBox.CheckedChanged += GreaterJudgment_Click;
            FistofJusticeBox.CheckedChanged += FistofJustice_Click;
            RepentanceBox.CheckedChanged += Repentance_Click;
            BlindingLightBox.CheckedChanged += BlindingLight_Click;
            VirtuesBladeBox.CheckedChanged += VirtuesBlade_Click;
            BladeofWrathBox.CheckedChanged += BladeofWrath_Click;
            DivineHammerBox.CheckedChanged += DivineHammer_Click;
            JusticarsVengeanceBox.CheckedChanged += JusticarsVengeance_Click;
            EyeforanEyeBox.CheckedChanged += EyeforanEye_Click;
            WordofGloryBox.CheckedChanged += WordofGlory_Click;
            DivineInterventionBox.CheckedChanged += DivineIntervention_Click;
            CavalierBox.CheckedChanged += Cavalier_Click;
            SealofLightBox.CheckedChanged += SealofLight_Click;
            DivinePurposeBox.CheckedChanged += DivinePurpose_Click;
            CrusadeBox.CheckedChanged += Crusade_Click;
            HolyWrathBox.CheckedChanged += HolyWrath_Click;

            SettingsForm.Controls.Add(cmdSave);
            lblFinalVerdictText.BringToFront();
            lblExecutionSentenceText.BringToFront();
            lblConsecrationText.BringToFront();
            lblTheFireofJusticeText.BringToFront();
            lblZealText.BringToFront();
            lblGreaterJudgmentText.BringToFront();
            lblFistofJusticeText.BringToFront();
            lblRepentanceText.BringToFront();
            lblBlindingLightText.BringToFront();
            lblVirtuesBladeText.BringToFront();
            lblBladeofWrathText.BringToFront();
            lblDivineHammerText.BringToFront();
            lblJusticarsVengeanceText.BringToFront();
            lblEyeforanEyeText.BringToFront();
            lblWordofGloryText.BringToFront();
            lblDivineInterventionText.BringToFront();
            lblCavalierText.BringToFront();
            lblSealofLightText.BringToFront();
            lblDivinePurposeText.BringToFront();
            lblCrusadeText.BringToFront();
            lblHolyWrathText.BringToFront();
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            FinalVerdict = FinalVerdictBox.Checked;
            ExecutionSentence = ExecutionSentenceBox.Checked;
            Consecration = ConsecrationBox.Checked;
            TheFireofJustice = TheFireofJusticeBox.Checked;
            Zeal = ZealBox.Checked;
            GreaterJudgment = GreaterJudgmentBox.Checked;
            FistofJustice = FistofJusticeBox.Checked;
            Repentance = RepentanceBox.Checked;
            BlindingLight = BlindingLightBox.Checked;
            VirtuesBlade = VirtuesBladeBox.Checked;
            BladeofWrath = BladeofWrathBox.Checked;
            DivineHammer = DivineHammerBox.Checked;
            JusticarsVengeance = JusticarsVengeanceBox.Checked;
            EyeforanEye = EyeforanEyeBox.Checked;
            WordofGlory = WordofGloryBox.Checked;
            DivineIntervention = DivineInterventionBox.Checked;
            Cavalier = CavalierBox.Checked;
            SealofLight = SealofLightBox.Checked;
            DivinePurpose = DivinePurposeBox.Checked;
            Crusade = CrusadeBox.Checked;
            HolyWrath = HolyWrathBox.Checked;
            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void FinalVerdict_Click(object sender, EventArgs e)
        {
            FinalVerdict = FinalVerdictBox.Checked;
        }

        private void ExecutionSentence_Click(object sender, EventArgs e)
        {
            ExecutionSentence = ExecutionSentenceBox.Checked;
        }

        private void Consecration_Click(object sender, EventArgs e)
        {
            Consecration = ConsecrationBox.Checked;
        }

        private void TheFireofJustice_Click(object sender, EventArgs e)
        {
            TheFireofJustice = TheFireofJusticeBox.Checked;
        }

        private void Zeal_Click(object sender, EventArgs e)
        {
            Zeal = ZealBox.Checked;
        }

        private void GreaterJudgment_Click(object sender, EventArgs e)
        {
            GreaterJudgment = GreaterJudgmentBox.Checked;
        }

        private void FistofJustice_Click(object sender, EventArgs e)
        {
            FistofJustice = FistofJusticeBox.Checked;
        }

        private void Repentance_Click(object sender, EventArgs e)
        {
            Repentance = RepentanceBox.Checked;
        }

        private void BlindingLight_Click(object sender, EventArgs e)
        {
            BlindingLight = BlindingLightBox.Checked;
        }

        private void VirtuesBlade_Click(object sender, EventArgs e)
        {
            VirtuesBlade = VirtuesBladeBox.Checked;
        }

        private void BladeofWrath_Click(object sender, EventArgs e)
        {
            BladeofWrath = BladeofWrathBox.Checked;
        }

        private void DivineHammer_Click(object sender, EventArgs e)
        {
            DivineHammer = DivineHammerBox.Checked;
        }

        private void JusticarsVengeance_Click(object sender, EventArgs e)
        {
            JusticarsVengeance = JusticarsVengeanceBox.Checked;
        }

        private void EyeforanEye_Click(object sender, EventArgs e)
        {
            EyeforanEye = EyeforanEyeBox.Checked;
        }

        private void WordofGlory_Click(object sender, EventArgs e)
        {
            WordofGlory = WordofGloryBox.Checked;
        }

        private void DivineIntervention_Click(object sender, EventArgs e)
        {
            DivineIntervention = DivineInterventionBox.Checked;
        }

        private void Cavalier_Click(object sender, EventArgs e)
        {
            Cavalier = CavalierBox.Checked;
        }

        private void SealofLight_Click(object sender, EventArgs e)
        {
            SealofLight = SealofLightBox.Checked;
        }

        private void DivinePurpose_Click(object sender, EventArgs e)
        {
            DivinePurpose = DivinePurposeBox.Checked;
        }

        private void Crusade_Click(object sender, EventArgs e)
        {
            Crusade = CrusadeBox.Checked;
        }

        private void HolyWrath_Click(object sender, EventArgs e)
        {
            HolyWrath = HolyWrathBox.Checked;
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Sorcerer
AddonName=PixelMagic
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,217020,Zeal,NumPad1
Spell,215661,Justicars Vengeance,D8
Spell,184662,Shield of Vengeance,NumPad0
Spell,853,Hammer of Justice,Add
Spell,213757,Execution Sentence,D9
Spell,633,Lay on Hands,D6
Spell,205273,Wake of Ashes,NumPad5
Spell,53385,Divine Storm,NumPad6
Spell,184575,Blade of Justice,NumPad4
Spell,198034,Divine Hammer,NumPad4
Spell,35395,Crusader Strike,NumPad1
Spell,85256,Templar Verdict,NumPad2
Spell,20271,Judgement,NumPad3
Spell,224668,Crusade,Subtract
Spell,31884,Avenging Wrath,Subtract
Spell,19750,Flash of Light,D1
Spell,210220,Holy Wrath,D8
Spell,205228,Consecration,D8
Aura,20271,Judgement
Aura,223819,Divine Purpose
Aura,209785,The Fires of Justice
Aura,2825,Bloodlust
Aura,80353,Time Warp
Aura,160452,Netherwinds
Aura,230935,Drums of War
Aura,127271,Mount
Aura,25771,Forbearance
*/