// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class WarlockAffliction : CombatRoutine
    {
        public override string Name
        {
            get { return "Affliction Warlock"; }
        }

        public override string Class
        {
            get { return "Warlock"; }
        }

        public override void Stop()
        {
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Mount"))
                {
                    if ((!WoW.TargetHasDebuff("Agony") || WoW.TargetDebuffTimeRemaining("Agony") <= 5.4) && (!WoW.PlayerIsChanneling || WoW.TargetDebuffTimeRemaining("Agony") <= 1.5) &&
                        WoW.CanCast("Agony") && WoW.IsSpellInRange("Agony"))
                    {
                        WoW.CastSpell("Agony");
                        return;
                    }

                    if (WoW.CurrentSoulShards >= 3 || WoW.WasLastCasted("Unstable Affliction") && !WoW.IsMoving && WoW.CanCast("Unstable Affliction") && WoW.IsSpellInRange("Agony"))
                    {
                        WoW.CastSpell("Unstable Affliction");
                        Thread.Sleep(200);
                        return;
                    }

                    if (WoW.CanCast("Life Tap") && !WoW.PlayerIsChanneling && ManaTap && !WoW.PlayerHasBuff("Empowered Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if ((!WoW.TargetHasDebuff("Corruption") || WoW.TargetDebuffTimeRemaining("Corruption") <= 4.2) &&
                        (!WoW.PlayerIsChanneling || WoW.TargetDebuffTimeRemaining("Corruption") <= 1) && WoW.CanCast("Corruption") && WoW.IsSpellInRange("Agony"))
                    {
                        WoW.CastSpell("Corruption");
                        return;
                    }

                    if ((!WoW.TargetHasDebuff("Siphon Life") || WoW.TargetDebuffTimeRemaining("Siphon Life") <= 4.2) &&
                        (!WoW.PlayerIsChanneling || WoW.TargetDebuffTimeRemaining("Siphon Life") <= 1) && SiphonLife && WoW.CanCast("Siphon Life") && WoW.IsSpellInRange("Agony"))
                    {
                        WoW.CastSpell("Siphon Life");
                        return;
                    }

                    /*if (WoW.TargetHasDebuff("Unstable Affliction1") && WoW.TargetHasDebuff("Unstable Affliction2")
                        || (WoW.TargetHasDebuff("Unstable Affliction1") && WoW.TargetHasDebuff("Unstable Affliction3"))
                        || (WoW.TargetHasDebuff("Unstable Affliction1") && WoW.TargetHasDebuff("Unstable Affliction4"))
                        || (WoW.TargetHasDebuff("Unstable Affliction1") && WoW.TargetHasDebuff("Unstable Affliction5"))
                        || (WoW.TargetHasDebuff("Unstable Affliction2") && WoW.TargetHasDebuff("Unstable Affliction3"))
                        || (WoW.TargetHasDebuff("Unstable Affliction2") && WoW.TargetHasDebuff("Unstable Affliction4"))
                        || (WoW.TargetHasDebuff("Unstable Affliction2") && WoW.TargetHasDebuff("Unstable Affliction5"))
                        || (WoW.TargetHasDebuff("Unstable Affliction3") && WoW.TargetHasDebuff("Unstable Affliction4"))
                        || (WoW.TargetHasDebuff("Unstable Affliction3") && WoW.TargetHasDebuff("Unstable Affliction5"))
                        || (WoW.TargetHasDebuff("Unstable Affliction4") && WoW.TargetHasDebuff("Unstable Affliction5"))
                        && !WoW.PlayerIsCasting
                        && WoW.CanCast("Reap Souls")
                        && !WoW.PlayerHasBuff("Deadwind Harvester")
                        && WoW.PlayerHasBuff("Tormented Souls"))
                    {
                        WoW.CastSpell("Reap Souls");
                        return;
                    }*/


                    if (WoW.CanCast("Reap Souls") && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Deadwind Harvester") && WoW.PlayerHasBuff("Tormented Souls"))
                    {
                        WoW.CastSpell("Reap Souls");
                        return;
                    }

                    if (WoW.CanCast("Felhunter") && GrimoireofService && !WoW.IsSpellOnCooldown("Felhunter") && WoW.IsSpellInRange("Agony") && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("Felhunter");
                        return;
                    }

                    if (WoW.CanCast("Unstable Affliction") && Contagion && !WoW.IsMoving && WoW.IsSpellInRange("Unstable Affliction") && !WoW.PlayerIsChanneling && WoW.CurrentSoulShards >= 1 &&
                        (!WoW.TargetHasDebuff("Unstable Affliction1") || !WoW.TargetHasDebuff("Unstable Affliction2") || !WoW.TargetHasDebuff("Unstable Affliction3") ||
                         !WoW.TargetHasDebuff("Unstable Affliction4") || !WoW.TargetHasDebuff("Unstable Affliction5") || (WoW.TargetDebuffTimeRemaining("Unstable Affliction1") <= 1.5) ||
                         (WoW.TargetDebuffTimeRemaining("Unstable Affliction2") <= 1.5) || (WoW.TargetDebuffTimeRemaining("Unstable Affliction3") <= 1.5) ||
                         (WoW.TargetDebuffTimeRemaining("Unstable Affliction4") <= 1.5) || (WoW.TargetDebuffTimeRemaining("Unstable Affliction5") <= 1.5)))
                    {
                        WoW.CastSpell("Unstable Affliction");
                        Thread.Sleep(200);
                        return;
                    }

                    if (WoW.IsInCombat && WoW.Mana < 70 && WoW.HealthPercent > 70 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.CanCast("Haunt") && Haunt && !WoW.IsSpellOnCooldown("Haunt") && WoW.IsSpellInRange("Agony") && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Haunt");
                        return;
                    }
                    if (WoW.CanCast("Drain Soul") && WoW.IsSpellInRange("Agony") && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Drain Soul");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Mount")) // Do AOE stuff here
                {
                    if (WoW.CanCast("Agony") && WoW.IsSpellInRange("Agony") && WoW.TargetHasDebuff("Seed of Corruption") &&
                        (!WoW.TargetHasDebuff("Agony") || (WoW.TargetDebuffTimeRemaining("Agony") <= 5.4)))
                    {
                        WoW.CastSpell("Agony");
                        return;
                    }

                    if (WoW.CanCast("Corruption") && WoW.IsSpellInRange("Agony") && WoW.TargetHasDebuff("Seed of Corruption") &&
                        (!WoW.TargetHasDebuff("Corruption") || (WoW.TargetDebuffTimeRemaining("Corruption") <= 4.2)))
                    {
                        WoW.CastSpell("Corruption");
                        return;
                    }

                    if (WoW.CanCast("Siphon Life") && SiphonLife && WoW.IsSpellInRange("Agony") && WoW.TargetHasDebuff("Seed of Corruption") &&
                        (!WoW.TargetHasDebuff("Siphon Life") || (WoW.TargetDebuffTimeRemaining("Siphon Life") <= 4.2)))
                    {
                        WoW.CastSpell("Siphon Life");
                        return;
                    }

                    if (WoW.CanCast("Seed of Corruption") && WoW.IsSpellInRange("Agony") && !WoW.TargetHasDebuff("Seed of Corruption") && !WoW.IsMoving && WoW.CurrentSoulShards >= 1)
                    {
                        WoW.CastSpell("Seed of Corruption");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                // Do Single Target Cleave stuff here if applicable else ignore this one
            }
        }

        #region Talents

        private CheckBox HauntBox;
        private CheckBox WritheinAgonyBox;
        private CheckBox DrainSoulBox;
        private CheckBox ContagionBox;
        private CheckBox AbsoluteCorruptionBox;
        private CheckBox ManaTapBox;
        private CheckBox DemonicCircleBox;
        private CheckBox MortalCoilBox;
        private CheckBox HowlofTerrorBox;
        private CheckBox SiphonLifeBox;
        private CheckBox SowtheSeedsBox;
        private CheckBox SoulHarvestBox;
        private CheckBox DemonSkinBox;
        private CheckBox BurningRushBox;
        private CheckBox DarkPactBox;
        private CheckBox GrimoireofSupremacyBox;
        private CheckBox GrimoireofServiceBox;
        private CheckBox GrimoireofSacrificeBox;
        private CheckBox SoulEffigyBox;
        private CheckBox PhantomSingularityBox;
        private CheckBox SoulConduitBox;

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Affliction Warlock", Color.Purple);
            Log.Write("READ ME:", Color.Red);
            Log.Write("Please select your talents from the Rotation settings menu", Color.Red);
            Log.Write("Some talents won't impact your rotation, they are there for future use if necesary", Color.Red);
            Log.Write("The script is not retard proof so please don't select more than 1 talent on each row", Color.Red);
            Log.Write("Absolute Corruption NOT SUPPORTED YET due to addon entering infinite error mode for unlimited time debuff", Color.Red);
            Log.Write("Soul Effigy and Phantom Singularity not sypported at this time", Color.Red);
            Log.Write("The rotation is based on Icy Veins data", Color.Red);
            WoW.Speak("Welcome to PixelMagic Affliction Warlock");

            SettingsForm = new Form {Text = "Talents", StartPosition = FormStartPosition.CenterScreen, Width = 555, Height = 350, ShowIcon = true};

            var lblHauntText = new Label //Haunt LABEL
            {Text = "Haunt", Size = new Size(150, 15), Left = 30, Top = 30};
            SettingsForm.Controls.Add(lblHauntText); //Haunt TEXT

            HauntBox = new CheckBox {Checked = Haunt, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 30};
            SettingsForm.Controls.Add(HauntBox); //Haunt BOX

            var lblWritheinAgonyText = new Label //Writhe in Agony LABEL
            {Text = "Writhe in Agony", Size = new Size(150, 15), Left = 210, Top = 30};
            SettingsForm.Controls.Add(lblWritheinAgonyText); //Writhe in Agony TEXT

            WritheinAgonyBox = new CheckBox {Checked = WritheinAgony, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 30};
            SettingsForm.Controls.Add(WritheinAgonyBox); //Writhe in Agony BOX		

            var lblDrainSoulText = new Label // Drain Soul LABEL
            {Text = "Drain Soul", Size = new Size(150, 15), Left = 390, Top = 30};
            SettingsForm.Controls.Add(lblDrainSoulText); //Drain Soul TEXT

            DrainSoulBox = new CheckBox {Checked = DrainSoul, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 30};
            SettingsForm.Controls.Add(DrainSoulBox); // Drain Soul BOX

            var lblContagionText = new Label // Contagion LABEL
            {Text = "Contagion", Size = new Size(150, 15), Left = 30, Top = 60};
            SettingsForm.Controls.Add(lblContagionText); //Contagion TEXT

            ContagionBox = new CheckBox {Checked = Contagion, TabIndex = 8, Size = new Size(15, 15), Left = 15, Top = 60};
            SettingsForm.Controls.Add(ContagionBox); //Contagion Box

            var lblAbsoluteCorruptionText = new Label //Absolute Corruption label
            {Text = "Absolute Corruption", Size = new Size(150, 15), Left = 210, Top = 60};
            SettingsForm.Controls.Add(lblAbsoluteCorruptionText); //Absolute Corruption text

            AbsoluteCorruptionBox = new CheckBox {Checked = AbsoluteCorruption, TabIndex = 10, Size = new Size(15, 15), Left = 195, Top = 60};
            SettingsForm.Controls.Add(AbsoluteCorruptionBox); //Absolute Corruption box

            var lblManaTapText = new Label //Mana Tap label
            {Text = "Mana Tap", Size = new Size(150, 15), Left = 390, Top = 60};
            SettingsForm.Controls.Add(lblManaTapText); //Mana Tap text

            ManaTapBox = new CheckBox {Checked = ManaTap, TabIndex = 12, Size = new Size(15, 15), Left = 375, Top = 60};
            SettingsForm.Controls.Add(ManaTapBox); //Mana Tap box	

            var lblDemonicCircleText = new Label //Demonic Circle label
            {Text = "Demonic Circle", Size = new Size(150, 15), Left = 30, Top = 90};
            SettingsForm.Controls.Add(lblDemonicCircleText); //Demonic Circle text

            DemonicCircleBox = new CheckBox {Checked = DemonicCircle, TabIndex = 12, Size = new Size(15, 15), Left = 15, Top = 90};
            SettingsForm.Controls.Add(DemonicCircleBox); //Demonic Circle box			

            var lblMortalCoilText = new Label //Mortal Coil LABEL
            {Text = "Mortal Coil", Size = new Size(150, 15), Left = 210, Top = 90};
            SettingsForm.Controls.Add(lblMortalCoilText); //Mortal Coil TEXT

            MortalCoilBox = new CheckBox {Checked = MortalCoil, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 90};
            SettingsForm.Controls.Add(MortalCoilBox); //Mortal Coil BOX		

            var lblHowlofTerrorText = new Label // Howl of Terror LABEL
            {Text = "Howl of Terror", Size = new Size(150, 15), Left = 390, Top = 90};
            SettingsForm.Controls.Add(lblHowlofTerrorText); //Howl of Terror TEXT

            HowlofTerrorBox = new CheckBox {Checked = HowlofTerror, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 90};
            SettingsForm.Controls.Add(HowlofTerrorBox); // Howl of Terror BOX

            var lblSiphonLifeText = new Label //Siphon Life LABEL
            {Text = "Siphon Life", Size = new Size(150, 15), Left = 30, Top = 120};
            SettingsForm.Controls.Add(lblSiphonLifeText); //Siphon Life TEXT

            SiphonLifeBox = new CheckBox {Checked = SiphonLife, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 120};
            SettingsForm.Controls.Add(SiphonLifeBox); //Siphon Life BOX

            var lblSowtheSeedsText = new Label //Sow the Seeds LABEL
            {Text = "Sow the Seeds", Size = new Size(150, 15), Left = 210, Top = 120};
            SettingsForm.Controls.Add(lblSowtheSeedsText); //Sow the Seeds TEXT

            SowtheSeedsBox = new CheckBox {Checked = SowtheSeeds, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 120};
            SettingsForm.Controls.Add(SowtheSeedsBox); //Sow the Seeds BOX		

            var lblSoulHarvestText = new Label // Soul Harvest LABEL
            {Text = "Soul Harvest", Size = new Size(150, 15), Left = 390, Top = 120};
            SettingsForm.Controls.Add(lblSoulHarvestText); //Soul Harvest TEXT

            SoulHarvestBox = new CheckBox {Checked = SoulHarvest, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 120};
            SettingsForm.Controls.Add(SoulHarvestBox); // Soul Harvest BOX

            var lblDemonSkinText = new Label //Demon Skin LABEL
            {Text = "Demon Skin", Size = new Size(150, 15), Left = 30, Top = 150};
            SettingsForm.Controls.Add(lblDemonSkinText); //Demon Skin TEXT

            DemonSkinBox = new CheckBox {Checked = DemonSkin, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 150};
            SettingsForm.Controls.Add(DemonSkinBox); //Demon Skin BOX

            var lblBurningRushText = new Label //Burning Rush LABEL
            {Text = "Burning Rush", Size = new Size(150, 15), Left = 210, Top = 150};
            SettingsForm.Controls.Add(lblBurningRushText); //Burning Rush TEXT

            BurningRushBox = new CheckBox {Checked = BurningRush, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 150};
            SettingsForm.Controls.Add(BurningRushBox); //Burning Rush BOX		

            var lblDarkPactText = new Label //Dark Pact LABEL
            {Text = "DarkPact", Size = new Size(150, 15), Left = 390, Top = 150};
            SettingsForm.Controls.Add(lblDarkPactText); //Dark Pact TEXT

            DarkPactBox = new CheckBox {Checked = DarkPact, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 150};
            SettingsForm.Controls.Add(DarkPactBox); //Dark Pact BOX

            var lblGrimoireofSupremacyText = new Label //Grimoire of Supremacy LABEL
            {Text = "Grimoire of Supremacy", Size = new Size(150, 15), Left = 30, Top = 180};
            SettingsForm.Controls.Add(lblGrimoireofSupremacyText); //Grimoire of Supremacy TEXT

            GrimoireofSupremacyBox = new CheckBox {Checked = GrimoireofSupremacy, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 180};
            SettingsForm.Controls.Add(GrimoireofSupremacyBox); //Grimoire of Supremacy BOX

            var lblGrimoireofServiceText = new Label //Grimoire of Service LABEL
            {Text = "Grimoire of Service", Size = new Size(150, 15), Left = 210, Top = 180};
            SettingsForm.Controls.Add(lblGrimoireofServiceText); //Grimoire of Service TEXT

            GrimoireofServiceBox = new CheckBox {Checked = GrimoireofService, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 180};
            SettingsForm.Controls.Add(GrimoireofServiceBox); //Grimoire of Service BOX		

            var lblGrimoireofSacrificeText = new Label //Grimoire of Sacrifice LABEL
            {Text = "Grimoire of Sacrifice", Size = new Size(150, 15), Left = 390, Top = 180};
            SettingsForm.Controls.Add(lblGrimoireofSacrificeText); //Grimoire of Sacrifice TEXT

            GrimoireofSacrificeBox = new CheckBox {Checked = GrimoireofSacrifice, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 180};
            SettingsForm.Controls.Add(GrimoireofSacrificeBox); // Grimoire of Sacrifice BOX

            var lblSoulEffigyText = new Label //Soul Effigy LABEL
            {Text = "Soul Effigy", Size = new Size(150, 15), Left = 30, Top = 210};
            SettingsForm.Controls.Add(lblSoulEffigyText); //Soul Effigy TEXT

            SoulEffigyBox = new CheckBox {Checked = SoulEffigy, TabIndex = 2, Size = new Size(15, 15), Left = 15, Top = 210};
            SettingsForm.Controls.Add(SoulEffigyBox); //Soul Effigy BOX

            var lblPhantomSingularityText = new Label //Phantom Singularity LABEL
            {Text = "Phantom Singularity", Size = new Size(150, 15), Left = 210, Top = 210};
            SettingsForm.Controls.Add(lblPhantomSingularityText); //Phantom Singularity TEXT

            PhantomSingularityBox = new CheckBox {Checked = PhantomSingularity, TabIndex = 4, Size = new Size(15, 15), Left = 195, Top = 210};
            SettingsForm.Controls.Add(PhantomSingularityBox); //Phantom Singularity BOX		

            var lblSoulConduitText = new Label //Soul Conduit LABEL
            {Text = "Soul Conduit", Size = new Size(150, 15), Left = 390, Top = 210};
            SettingsForm.Controls.Add(lblSoulConduitText); //Soul Conduit TEXT

            SoulConduitBox = new CheckBox {Checked = SoulConduit, TabIndex = 6, Size = new Size(15, 15), Left = 375, Top = 210};
            SettingsForm.Controls.Add(SoulConduitBox); //Soul Conduit BOX

            var cmdSave = new Button //Save Button
            {Text = "Save", Width = 40, Height = 20, Left = 445, Top = 270, Size = new Size(80, 30)};

            HauntBox.Checked = Haunt;
            WritheinAgonyBox.Checked = WritheinAgony;
            DrainSoulBox.Checked = DrainSoul;
            ContagionBox.Checked = Contagion;
            AbsoluteCorruptionBox.Checked = AbsoluteCorruption;
            ManaTapBox.Checked = ManaTap;
            DemonicCircleBox.Checked = DemonicCircle;
            MortalCoilBox.Checked = MortalCoil;
            HowlofTerrorBox.Checked = HowlofTerror;
            SiphonLifeBox.Checked = SiphonLife;
            SowtheSeedsBox.Checked = SowtheSeeds;
            SoulHarvestBox.Checked = SoulHarvest;
            DemonSkinBox.Checked = DemonSkin;
            BurningRushBox.Checked = BurningRush;
            DarkPactBox.Checked = DarkPact;
            GrimoireofSupremacyBox.Checked = GrimoireofSupremacy;
            GrimoireofServiceBox.Checked = GrimoireofService;
            GrimoireofSacrificeBox.Checked = GrimoireofSacrifice;
            SoulEffigyBox.Checked = SoulEffigy;
            PhantomSingularityBox.Checked = PhantomSingularity;
            SoulConduitBox.Checked = SoulConduit;

            cmdSave.Click += CmdSave_Click;
            HauntBox.CheckedChanged += Haunt_Click;
            WritheinAgonyBox.CheckedChanged += WritheinAgony_Click;
            DrainSoulBox.CheckedChanged += DrainSoul_Click;
            ContagionBox.CheckedChanged += Contagion_Click;
            AbsoluteCorruptionBox.CheckedChanged += AbsoluteCorruption_Click;
            ManaTapBox.CheckedChanged += ManaTap_Click;
            DemonicCircleBox.CheckedChanged += DemonicCircle_Click;
            MortalCoilBox.CheckedChanged += MortalCoil_Click;
            HowlofTerrorBox.CheckedChanged += HowlofTerror_Click;
            SiphonLifeBox.CheckedChanged += SiphonLife_Click;
            SowtheSeedsBox.CheckedChanged += SowtheSeeds_Click;
            SoulHarvestBox.CheckedChanged += SoulHarvest_Click;
            DemonSkinBox.CheckedChanged += DemonSkin_Click;
            BurningRushBox.CheckedChanged += BurningRush_Click;
            DarkPactBox.CheckedChanged += DarkPact_Click;
            GrimoireofSupremacyBox.CheckedChanged += GrimoireofSupremacy_Click;
            GrimoireofServiceBox.CheckedChanged += GrimoireofService_Click;
            GrimoireofSacrificeBox.CheckedChanged += GrimoireofSacrifice_Click;
            SoulEffigyBox.CheckedChanged += SoulEffigy_Click;
            PhantomSingularityBox.CheckedChanged += PhantomSingularity_Click;
            SoulConduitBox.CheckedChanged += SoulConduit_Click;

            SettingsForm.Controls.Add(cmdSave);
            lblHauntText.BringToFront();
            lblWritheinAgonyText.BringToFront();
            lblDrainSoulText.BringToFront();
            lblContagionText.BringToFront();
            lblAbsoluteCorruptionText.BringToFront();
            lblManaTapText.BringToFront();
            lblDemonicCircleText.BringToFront();
            lblMortalCoilText.BringToFront();
            lblHowlofTerrorText.BringToFront();
            lblSiphonLifeText.BringToFront();
            lblSowtheSeedsText.BringToFront();
            lblSoulHarvestText.BringToFront();
            lblDemonSkinText.BringToFront();
            lblBurningRushText.BringToFront();
            lblDarkPactText.BringToFront();
            lblGrimoireofSupremacyText.BringToFront();
            lblGrimoireofServiceText.BringToFront();
            lblGrimoireofSacrificeText.BringToFront();
            lblSoulEffigyText.BringToFront();
            lblPhantomSingularityText.BringToFront();
            lblSoulConduitText.BringToFront();

            Log.Write("Haunt = " + Haunt + "                                                  " + "Writhe in Agony = " + WritheinAgony + "                                                   " +
                      "Drain Soul = " + DrainSoul);
            Log.Write("Contagion = " + Contagion + "                                           " + "Absolute Corruption = " + AbsoluteCorruption + "                                           " +
                      "Mana Tap = " + ManaTap);
            Log.Write("Demonic Circle = " + DemonicCircle + "                                  " + "Mortal Coil = " + MortalCoil +
                      "                                                            " + "Howl of Terror = " + HowlofTerror);
            Log.Write("Siphon Life = " + SiphonLife + "                                          " + "Sow the Seeds = " + SowtheSeeds + "                                                      " +
                      "Soul Harvest = " + SoulHarvest);
            Log.Write("Demon Skin = " + DemonSkin + "                                        " + "Burning Rush = " + BurningRush + "                                                        " +
                      "Dark Pact = " + DarkPact);
            Log.Write("Grimoire of Supremacy = " + GrimoireofSupremacy + "                     " + "Grimoire of Service = " + GrimoireofService +
                      "                                              " + "Grimoire of Sacrifice = " + GrimoireofSacrifice);
            Log.Write("Soul Effigy = " + SoulEffigy + "                                          " + "PhantomSingularity = " + PhantomSingularity +
                      "                                             " + "Soul Conduit = " + SoulConduit);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Haunt = HauntBox.Checked;
            WritheinAgony = WritheinAgonyBox.Checked;
            DrainSoul = DrainSoulBox.Checked;
            Contagion = ContagionBox.Checked;
            AbsoluteCorruption = AbsoluteCorruptionBox.Checked;
            ManaTap = ManaTapBox.Checked;
            DemonicCircle = DemonicCircleBox.Checked;
            MortalCoil = MortalCoilBox.Checked;
            HowlofTerror = HowlofTerrorBox.Checked;
            SiphonLife = SiphonLifeBox.Checked;
            SowtheSeeds = SowtheSeedsBox.Checked;
            SoulHarvest = SoulHarvestBox.Checked;
            DemonSkin = DemonSkinBox.Checked;
            BurningRush = BurningRushBox.Checked;
            DarkPact = DarkPactBox.Checked;
            GrimoireofSupremacy = GrimoireofSupremacyBox.Checked;
            GrimoireofService = GrimoireofServiceBox.Checked;
            GrimoireofSacrifice = GrimoireofSacrificeBox.Checked;
            SoulEffigy = SoulEffigyBox.Checked;
            PhantomSingularity = PhantomSingularityBox.Checked;
            SoulConduit = SoulConduitBox.Checked;
            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void Haunt_Click(object sender, EventArgs e)
        {
            Haunt = HauntBox.Checked;
        }

        private void WritheinAgony_Click(object sender, EventArgs e)
        {
            WritheinAgony = WritheinAgonyBox.Checked;
        }

        private void DrainSoul_Click(object sender, EventArgs e)
        {
            DrainSoul = DrainSoulBox.Checked;
        }

        private void Contagion_Click(object sender, EventArgs e)
        {
            Contagion = ContagionBox.Checked;
        }

        private void AbsoluteCorruption_Click(object sender, EventArgs e)
        {
            AbsoluteCorruption = AbsoluteCorruptionBox.Checked;
        }

        private void ManaTap_Click(object sender, EventArgs e)
        {
            ManaTap = ManaTapBox.Checked;
        }

        private void DemonicCircle_Click(object sender, EventArgs e)
        {
            DemonicCircle = DemonicCircleBox.Checked;
        }

        private void MortalCoil_Click(object sender, EventArgs e)
        {
            MortalCoil = MortalCoilBox.Checked;
        }

        private void HowlofTerror_Click(object sender, EventArgs e)
        {
            HowlofTerror = HowlofTerrorBox.Checked;
        }

        private void SiphonLife_Click(object sender, EventArgs e)
        {
            SiphonLife = SiphonLifeBox.Checked;
        }

        private void SowtheSeeds_Click(object sender, EventArgs e)
        {
            SowtheSeeds = SowtheSeedsBox.Checked;
        }

        private void SoulHarvest_Click(object sender, EventArgs e)
        {
            SoulHarvest = SoulHarvestBox.Checked;
        }

        private void DemonSkin_Click(object sender, EventArgs e)
        {
            DemonSkin = DemonSkinBox.Checked;
        }

        private void BurningRush_Click(object sender, EventArgs e)
        {
            BurningRush = BurningRushBox.Checked;
        }

        private void DarkPact_Click(object sender, EventArgs e)
        {
            DarkPact = DarkPactBox.Checked;
        }

        private void GrimoireofSupremacy_Click(object sender, EventArgs e)
        {
            GrimoireofSupremacy = GrimoireofSupremacyBox.Checked;
        }

        private void GrimoireofService_Click(object sender, EventArgs e)
        {
            GrimoireofService = GrimoireofServiceBox.Checked;
        }

        private void GrimoireofSacrifice_Click(object sender, EventArgs e)
        {
            GrimoireofSacrifice = GrimoireofSacrificeBox.Checked;
        }

        private void SoulEffigy_Click(object sender, EventArgs e)
        {
            SoulEffigy = SoulEffigyBox.Checked;
        }

        private void PhantomSingularity_Click(object sender, EventArgs e)
        {
            PhantomSingularity = PhantomSingularityBox.Checked;
        }

        private void SoulConduit_Click(object sender, EventArgs e)
        {
            SoulConduit = SoulConduitBox.Checked;
        }

        public static bool Haunt
        {
            get
            {
                var Haunt = ConfigFile.ReadValue("WarlockAffliction", "Haunt").Trim();

                return Haunt != "" && Convert.ToBoolean(Haunt);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Haunt", value.ToString()); }
        }

        private static bool WritheinAgony
        {
            get
            {
                var WritheinAgony = ConfigFile.ReadValue("WarlockAffliction", "Writhe in Agony").Trim();

                return WritheinAgony != "" && Convert.ToBoolean(WritheinAgony);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Writhe in Agony", value.ToString()); }
        }

        private static bool DrainSoul
        {
            get
            {
                var DrainSoul = ConfigFile.ReadValue("WarlockAffliction", "Drain Soul").Trim();

                return DrainSoul != "" && Convert.ToBoolean(DrainSoul);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Drain Soul", value.ToString()); }
        }

        private static bool Contagion
        {
            get
            {
                var Contagion = ConfigFile.ReadValue("WarlockAffliction", "Contagion").Trim();

                return Contagion != "" && Convert.ToBoolean(Contagion);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Contagion", value.ToString()); }
        }

        private static bool AbsoluteCorruption
        {
            get
            {
                var AbsoluteCorruption = ConfigFile.ReadValue("WarlockAffliction", "Absolute Corruption").Trim();

                return AbsoluteCorruption != "" && Convert.ToBoolean(AbsoluteCorruption);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Absolute Corruption", value.ToString()); }
        }

        private static bool ManaTap
        {
            get
            {
                var ManaTap = ConfigFile.ReadValue("WarlockAffliction", "Mana Tap").Trim();

                return ManaTap != "" && Convert.ToBoolean(ManaTap);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Mana Tap", value.ToString()); }
        }

        private static bool DemonicCircle
        {
            get
            {
                var DemonicCircle = ConfigFile.ReadValue("WarlockAffliction", "Demonic Circle").Trim();

                return DemonicCircle != "" && Convert.ToBoolean(DemonicCircle);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Demonic Circle", value.ToString()); }
        }

        private static bool MortalCoil
        {
            get
            {
                var MortalCoil = ConfigFile.ReadValue("WarlockAffliction", "Mortal Coil").Trim();

                return MortalCoil != "" && Convert.ToBoolean(MortalCoil);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Mortal Coil", value.ToString()); }
        }

        private static bool HowlofTerror
        {
            get
            {
                var HowlofTerror = ConfigFile.ReadValue("WarlockAffliction", "Howl of Terror").Trim();

                return HowlofTerror != "" && Convert.ToBoolean(HowlofTerror);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Howl of Terror", value.ToString()); }
        }

        private static bool SiphonLife
        {
            get
            {
                var SiphonLife = ConfigFile.ReadValue("WarlockAffliction", "Siphon Life").Trim();

                return SiphonLife != "" && Convert.ToBoolean(SiphonLife);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Siphon Life", value.ToString()); }
        }

        private static bool SowtheSeeds
        {
            get
            {
                var SowtheSeeds = ConfigFile.ReadValue("WarlockAffliction", "Sow the Seeds").Trim();

                return SowtheSeeds != "" && Convert.ToBoolean(SowtheSeeds);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Sow the Seeds", value.ToString()); }
        }

        private static bool SoulHarvest
        {
            get
            {
                var SoulHarvest = ConfigFile.ReadValue("WarlockAffliction", "Soul Harvest").Trim();

                return SoulHarvest != "" && Convert.ToBoolean(SoulHarvest);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Soul Harvest", value.ToString()); }
        }

        private static bool DemonSkin
        {
            get
            {
                var DemonSkin = ConfigFile.ReadValue("WarlockAffliction", "Demon Skin").Trim();

                return DemonSkin != "" && Convert.ToBoolean(DemonSkin);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Demon Skin", value.ToString()); }
        }

        private static bool BurningRush
        {
            get
            {
                var BurningRush = ConfigFile.ReadValue("WarlockAffliction", "Burning Rush").Trim();

                return BurningRush != "" && Convert.ToBoolean(BurningRush);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Burning Rush", value.ToString()); }
        }

        private static bool DarkPact
        {
            get
            {
                var DarkPact = ConfigFile.ReadValue("WarlockAffliction", "Dark Pact").Trim();

                return DarkPact != "" && Convert.ToBoolean(DarkPact);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Dark Pact", value.ToString()); }
        }

        private static bool GrimoireofSupremacy
        {
            get
            {
                var GrimoireofSupremacy = ConfigFile.ReadValue("WarlockAffliction", "Grimoire of Supremacy").Trim();

                return GrimoireofSupremacy != "" && Convert.ToBoolean(GrimoireofSupremacy);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Grimoire of Supremacy", value.ToString()); }
        }

        private static bool GrimoireofService
        {
            get
            {
                var GrimoireofService = ConfigFile.ReadValue("WarlockAffliction", "Grimoire of Service").Trim();

                return GrimoireofService != "" && Convert.ToBoolean(GrimoireofService);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Grimoire of Service", value.ToString()); }
        }

        private static bool GrimoireofSacrifice
        {
            get
            {
                var GrimoireofSacrifice = ConfigFile.ReadValue("WarlockAffliction", "Grimoire of Sacrifice").Trim();

                return GrimoireofSacrifice != "" && Convert.ToBoolean(GrimoireofSacrifice);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Grimoire of Sacrifice", value.ToString()); }
        }

        private static bool SoulEffigy
        {
            get
            {
                var SoulEffigy = ConfigFile.ReadValue("WarlockAffliction", "Soul Effigy").Trim();

                return SoulEffigy != "" && Convert.ToBoolean(SoulEffigy);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Soul Effigy", value.ToString()); }
        }

        private static bool PhantomSingularity
        {
            get
            {
                var PhantomSingularity = ConfigFile.ReadValue("WarlockAffliction", "Phantom Singularity").Trim();

                return PhantomSingularity != "" && Convert.ToBoolean(PhantomSingularity);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Phantom Singularity", value.ToString()); }
        }

        private static bool SoulConduit
        {
            get
            {
                var SoulConduit = ConfigFile.ReadValue("WarlockAffliction", "Soul Conduit").Trim();

                return SoulConduit != "" && Convert.ToBoolean(SoulConduit);
            }
            set { ConfigFile.WriteValue("WarlockAffliction", "Soul Conduit", value.ToString()); }
        }
    }

    #endregion
}

/*
[AddonDetails.db]
AddonAuthor=Sorcerer
AddonName=PixelMagic
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,980,Agony,NumPad1
Spell,63106,Siphon Life,NumPad2
Spell,172,Corruption,NumPad3
Spell,30108,Unstable Affliction,NumPad4
Spell,216698,Reap Souls,NumPad5
Spell,1454,Life Tap,NumPad7
Spell,48181,Haunt,NumPad8
Spell,198590,Drain Soul,Add
Spell,27243,Seed of Corruption,NumPad0
Spell,111897,Felhunter,NumPad9
Aura,980,Agony
Aura,27243,Seed of Corruption
Aura,146739,Corruption
Aura,63106,Siphon Life
Aura,233490,Unstable Affliction1
Aura,233496,Unstable Affliction2
Aura,233497,Unstable Affliction3
Aura,233498,Unstable Affliction4
Aura,233499,Unstable Affliction5
Aura,216708,Deadwind Harvester
Aura,216695,Tormented Souls
Aura,235156,Empowered Life Tap
Aura,127271,Mount
*/