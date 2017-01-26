// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

// Balance Druid rotation by Scotishdwarf and Daniel
// Known Bugs :
// Addon Lua Errors, probably not because of rotation.
// Changelog :
// Version 2.1
// - Temporarily fix to problems with Empowerements (using Thread.Sleep)
// Version 2.0
// - Coded in Soul of the Forest  (AoE), Shooting Stars, Stellar Drift
// - Legendary : Kil'jaeden's Burning Wish
// Version 1.9
// - Small improvements on movement, empowerements and Starsurge.
// Version 1.8
// - Fulmination Charge support, will try use Starsurge's at 8+ stacks.
// Version 1.7
// - Improved usage for Incarnation (will cast it in heroism)
// - Blessing of the Ancients support
// Version 1.6
// - Added support for Talents : Renewal, Restoration Affinnity (Swiftmeld, Rejuvenation).
// - Restoration Affinity : You need only leave Moonkin form at under 50% health and it will use Swiftmeld and Rejuvenation and go back to Moonkin.
// - Renewal : Will cast it at under 30% health
// - Heroism/Timewarp : Will use Incarnation if available.
// Version 1.5
// - Improved Solar Wrath and Lunar Strike usage under Empowerements.
// Version 1.4
// - Fixed to work with latest donator build.
// - Added starfall use to aoe.
// Version 1.3
// - Improved Pull
// Version 1.2
// - Improved AoE
// Version 1.1
// - Better handling of everything
// - Added opener
// - More configuration options
// Version 1.0
// Added : 
// - AoE, manual use of Starfall required. 
// - Better handling of Solar Wrath, Lunar Strike, Starsurge etc..
// - Basic cooldown usage on bosses.
// Version 0.2
// Completly reworked how rotation works
// Version 0.1
// Start building the rotation
using PixelMagic.Helpers;
using System.Diagnostics;
using System.Drawing;
using System;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class BalanceDruid : CombatRoutine
    {
        private readonly Stopwatch pullwatch = new Stopwatch();
        private CheckBox NaturesBalanceBox;
		private CheckBox IncarnationBox;
		private CheckBox AstralCommunionBox;
        private CheckBox StellarFlareBox;
        private CheckBox HealingLowHPBox;
        private CheckBox RenewalBox;
        private CheckBox BlessingOfAncientsBox;
        private CheckBox SouloftheForestBox;
        private CheckBox StellarDriftBox;
        private CheckBox KBWBox;

        public override string Name
        {
            get
            {
                return "Balance Druid by Scotishdwarf";
            }
        }

        public override string Class
        {
            get
            {
                return "Druid";
            }
        }

        private static bool NaturesBalance
        {
            get
            {
                var naturesBalance = ConfigFile.ReadValue("BalanceDruid", "NaturesBalance").Trim();

                return naturesBalance != "" && Convert.ToBoolean(naturesBalance);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "NaturesBalance", value.ToString()); }
        }
		private static bool Incarnation
        {
            get
            {
                var incarnation = ConfigFile.ReadValue("BalanceDruid", "Incarnation").Trim();

                return incarnation != "" && Convert.ToBoolean(incarnation);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "Incarnation", value.ToString()); }
        }
        private static bool SouloftheForest
        {
            get
            {
                var souloftheForest = ConfigFile.ReadValue("BalanceDruid", "SouloftheForest").Trim();

                return souloftheForest != "" && Convert.ToBoolean(souloftheForest);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "SouloftheForest", value.ToString()); }
        }
        private static bool AstralCommunion
        {
            get
            {
                var astralCommunion = ConfigFile.ReadValue("BalanceDruid", "AstralCommunion").Trim();

                return astralCommunion != "" && Convert.ToBoolean(astralCommunion);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "AstralCommunion", value.ToString()); }
        }
        private static bool StellarFlare
        {
            get
            {
                var stellarFlare = ConfigFile.ReadValue("BalanceDruid", "StellarFlare").Trim();

                return stellarFlare != "" && Convert.ToBoolean(stellarFlare);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "StellarFlare", value.ToString()); }
        }
        private static bool HealingLowHP
        {
            get
            {
                var healingLowHP = ConfigFile.ReadValue("BalanceDruid", "HealingLowHP").Trim();

                return healingLowHP != "" && Convert.ToBoolean(healingLowHP);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "HealingLowHP", value.ToString()); }
        }
        private static bool Renewal
        {
            get
            {
                var renewal = ConfigFile.ReadValue("BalanceDruid", "Renewal").Trim();

                return renewal != "" && Convert.ToBoolean(renewal);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "Renewal", value.ToString()); }
        }
        private static bool BlessingOfAncients
        {
            get
            {
                var blessingOfAncients = ConfigFile.ReadValue("BalanceDruid", "BlessingOfAncients").Trim();

                return blessingOfAncients != "" && Convert.ToBoolean(blessingOfAncients);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "BlessingOfAncients", value.ToString()); }
        }
        private static bool StellarDrift
        {
            get
            {
                var stellarDrift = ConfigFile.ReadValue("BalanceDruid", "StellarDrift").Trim();

                return stellarDrift != "" && Convert.ToBoolean(stellarDrift);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "StellarDrift", value.ToString()); }
        }
        private static bool KBW
        {
            get
            {
                var kBW = ConfigFile.ReadValue("BalanceDruid", "KBW").Trim();

                return kBW != "" && Convert.ToBoolean(kBW);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "KBW", value.ToString()); }
        }

        public override void Initialize()
        {
            MessageBox.Show("Welcome to Balance Druid by Scotishdwarf 2.0.\n\nMy talent build : 3,1,3,1,3,3,3.\n\nNoteworthy things :\n- If using Stellar Drift and SotF, in single target use it manually, AoE will use automatically.\n- Starsurge used at 80 AP, pooling it high to minimize dps loss while moving, you can force cast it by moving.\n- On AOE, manual Starfall usage required, you can make cast at cursor macro for this.\n\nRecommended to use addon that hides Lua Errors for now.\n\nPress OK to continue loading rotation.");
            Log.Write("Welcome to Balance rotation", Color.Green);

            // TALENT CONFIG
            SettingsForm = new Form { Text = "Settings", StartPosition = FormStartPosition.CenterScreen, Width = 600, Height = 200, ShowIcon = false };

            var lblNaturesBalanceText = new Label { Text = "Talent : NaturesBalance", Size = new Size(200, 13), Left = 12, Top = 14 };
            SettingsForm.Controls.Add(lblNaturesBalanceText);

            NaturesBalanceBox = new CheckBox { Checked = NaturesBalance, TabIndex = 2, Size = new Size(15, 14), Left = 220, Top = 14 };
            SettingsForm.Controls.Add(NaturesBalanceBox);

            var lblIncarnationText = new Label {Text = "Talent : Incarnation", Size = new Size(200, 13), Left = 12, Top = 29};
            SettingsForm.Controls.Add(lblIncarnationText);

            IncarnationBox = new CheckBox {Checked = Incarnation, TabIndex = 4, Size = new Size(15, 14), Left = 220, Top = 29};
            SettingsForm.Controls.Add(IncarnationBox);
			
			var lblAstralCommunionText = new Label {Text = "Talent : Astral Communion", Size = new Size(200, 13), Left = 12, Top = 44};
            SettingsForm.Controls.Add(lblAstralCommunionText);

            AstralCommunionBox = new CheckBox {Checked = AstralCommunion, TabIndex = 6, Size = new Size(15, 14), Left = 220, Top = 44};
            SettingsForm.Controls.Add(AstralCommunionBox);

            var lblStellarFlareText = new Label { Text = "Talent : Stellar Flare", Size = new Size(200, 13), Left = 12, Top = 59 };
            SettingsForm.Controls.Add(lblStellarFlareText);

            StellarFlareBox = new CheckBox { Checked = StellarFlare, TabIndex = 6, Size = new Size(15, 14), Left = 220, Top = 59 };
            SettingsForm.Controls.Add(StellarFlareBox);

            var lblRenewalText = new Label { Text = "Talent : Renewal", Size = new Size(200, 13), Left = 12, Top = 74 };
            SettingsForm.Controls.Add(lblRenewalText);

            RenewalBox = new CheckBox { Checked = Renewal, TabIndex = 6, Size = new Size(15, 14), Left = 220, Top = 74 };
            SettingsForm.Controls.Add(RenewalBox);

            var lblHealingLowHPText = new Label { Text = "Talent : Resto Affinity, 30% HP", Size = new Size(200, 13), Left = 12, Top = 89 };
            SettingsForm.Controls.Add(lblHealingLowHPText);

            HealingLowHPBox = new CheckBox { Checked = HealingLowHP, TabIndex = 6, Size = new Size(15, 14), Left = 220, Top = 89 };
            SettingsForm.Controls.Add(HealingLowHPBox);

            var lblBlessingOfAncientsText = new Label { Text = "Talent : BlessingOfAncients", Size = new Size(200, 13), Left = 12, Top = 104 };
            SettingsForm.Controls.Add(lblBlessingOfAncientsText);

            BlessingOfAncientsBox = new CheckBox { Checked = BlessingOfAncients, TabIndex = 6, Size = new Size(15, 14), Left = 220, Top = 104 };
            SettingsForm.Controls.Add(BlessingOfAncientsBox);

            var lblSouloftheForestText = new Label { Text = "Talent : SouloftheForest", Size = new Size(200, 13), Left = 12, Top = 129 };
            SettingsForm.Controls.Add(lblSouloftheForestText);

            SouloftheForestBox = new CheckBox { Checked = SouloftheForest, TabIndex = 4, Size = new Size(15, 14), Left = 220, Top = 129 };
            SettingsForm.Controls.Add(SouloftheForestBox);

            var lblStellarDriftText = new Label { Text = "Talent : StellarDrift", Size = new Size(200, 13), Left = 12, Top = 144 };
            SettingsForm.Controls.Add(lblStellarDriftText);

            StellarDriftBox = new CheckBox { Checked = BlessingOfAncients, TabIndex = 6, Size = new Size(15, 14), Left = 220, Top = 144 };
            SettingsForm.Controls.Add(StellarDriftBox);

            var lblKBWText = new Label { Text = "Item : Kil'jaeden's Burning Wish", Size = new Size(200, 13), Left = 312, Top = 14 };
            SettingsForm.Controls.Add(lblKBWText);

            KBWBox = new CheckBox { Checked = KBW, TabIndex = 6, Size = new Size(15, 14), Left = 520, Top = 14 };
            SettingsForm.Controls.Add(KBWBox);

            var cmdSave = new Button { Text = "Save", Width = 65, Height = 25, Left = 462, Top = 118, Size = new Size(108, 31) };

            NaturesBalanceBox.Checked = NaturesBalance;
			IncarnationBox.Checked = Incarnation;
			AstralCommunionBox.Checked = AstralCommunion;
            StellarFlareBox.Checked = StellarFlare;
            HealingLowHPBox.Checked = HealingLowHP;
            RenewalBox.Checked = Renewal;
            BlessingOfAncientsBox.Checked = BlessingOfAncients;
            SouloftheForestBox.Checked = SouloftheForest;
            StellarDriftBox.Checked = StellarDrift;
            KBWBox.Checked = KBW;

            cmdSave.Click += CmdSave_Click;
            NaturesBalanceBox.CheckedChanged += NaturesBalance_Click;
			IncarnationBox.CheckedChanged += Incarnation_Click;
			AstralCommunionBox.CheckedChanged += AstralCommunion_Click;
            StellarFlareBox.CheckedChanged += StellarFlare_Click;
            HealingLowHPBox.CheckedChanged += HealingLowHP_Click;
            RenewalBox.CheckedChanged += Renewal_Click;
            BlessingOfAncientsBox.CheckedChanged += BlessingOfAncients_Click;
            SouloftheForestBox.CheckedChanged += SouloftheForest_Click;
            StellarDriftBox.CheckedChanged += StellarDrift_Click;
            KBWBox.CheckedChanged += KBW_Click;

            SettingsForm.Controls.Add(cmdSave);
            lblNaturesBalanceText.BringToFront();
			lblIncarnationText.BringToFront();
			lblAstralCommunionText.BringToFront();
            lblStellarFlareText.BringToFront();
            lblHealingLowHPText.BringToFront();
            lblRenewalText.BringToFront();
            lblBlessingOfAncientsText.BringToFront();
            lblSouloftheForestText.BringToFront();
            lblStellarDriftText.BringToFront();
            lblKBWText.BringToFront();

            Log.Write("Natures Balance = " + NaturesBalance);
			Log.Write("Incarnation = " + NaturesBalance);
			Log.Write("Astral Communion = " + NaturesBalance);
            Log.Write("Stellar Flare = " + StellarFlare);
            Log.Write("Healing under 30% HP = " + HealingLowHP);
            Log.Write("Renewal = " + Renewal);
            Log.Write("BlessingOfAncients = " + BlessingOfAncients);
            Log.Write("SouloftheForest = " + SouloftheForest);
            Log.Write("StellarDrift = " + StellarDrift);
            Log.Write("KBW = " + KBW);

        }

        // SET CLICK SAVE
        private void CmdSave_Click(object sender, EventArgs e)
        {
            NaturesBalance = NaturesBalanceBox.Checked;
			Incarnation = IncarnationBox.Checked;
			AstralCommunion = AstralCommunionBox.Checked;
            StellarFlare = StellarFlareBox.Checked;
            HealingLowHP = HealingLowHPBox.Checked;
            Renewal = RenewalBox.Checked;
            BlessingOfAncients = BlessingOfAncientsBox.Checked;
            SouloftheForest = SouloftheForestBox.Checked;
            StellarDrift = StellarDriftBox.Checked;
            KBW = KBWBox.Checked;
            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void NaturesBalance_Click(object sender, EventArgs e)
        {
            NaturesBalance = NaturesBalanceBox.Checked;
        }
		private void Incarnation_Click(object sender, EventArgs e)
        {
            Incarnation = IncarnationBox.Checked;
        }
		private void AstralCommunion_Click(object sender, EventArgs e)
        {
            AstralCommunion = AstralCommunionBox.Checked;
        }
        private void StellarFlare_Click(object sender, EventArgs e)
        {
            StellarFlare = StellarFlareBox.Checked;
        }
        private void HealingLowHP_Click(object sender, EventArgs e)
        {
            HealingLowHP = HealingLowHPBox.Checked;
        }
        private void BlessingOfAncients_Click(object sender, EventArgs e)
        {
            BlessingOfAncients = BlessingOfAncientsBox.Checked;
        }
        private void Renewal_Click(object sender, EventArgs e)
        {
            Renewal = RenewalBox.Checked;
        }
        private void SouloftheForest_Click(object sender, EventArgs e)
        {
            SouloftheForest = SouloftheForestBox.Checked;
        }
        private void StellarDrift_Click(object sender, EventArgs e)
        {
            StellarDrift = StellarDriftBox.Checked;
        }
        private void KBW_Click(object sender, EventArgs e)
        {
            KBW = KBWBox.Checked;
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
				// Pullwatch timer
				if (WoW.IsInCombat && !pullwatch.IsRunning)
				{
					pullwatch.Start();
					Log.Write("Starting Combat, Starting Pullwatch.", Color.Red);
                    Thread.Sleep(100);
                }
				// Pullwatch stop
				if (!WoW.IsInCombat && pullwatch.ElapsedMilliseconds > 1000)
				{
					pullwatch.Reset();
					Log.Write("Leaving Combat, Resetting Stopwatches.", Color.Red);
					
				}
                // Moonkin in Combat
                if (!WoW.PlayerIsCasting && WoW.IsInCombat && WoW.CanCast("Moonkin") && !WoW.PlayerHasBuff("Moonkin"))
                {
                    WoW.CastSpell("Moonkin");
                    Thread.Sleep(100);
                    return;
                }
                // If Blessing of the Ancients get it up.
                if (BlessingOfAncients && WoW.CanCast("BlessingOfAncients") && !WoW.PlayerHasBuff("BlessingOfElune") && !WoW.PlayerHasBuff("BlessingOfAnshe") && WoW.HealthPercent >= 10)
                {
                    WoW.CastSpell("BlessingOfAncients");
                    Thread.Sleep(100);
                    return;
                }
                if (BlessingOfAncients && WoW.CanCast("BlessingOfAncients") && WoW.PlayerHasBuff("BlessingOfAnshe") && WoW.HealthPercent >= 10)
                {
                    WoW.CastSpell("BlessingOfAncients");
                    Thread.Sleep(100);
                    return;
                }
                // Restoration Affinity
                if (HealingLowHP && !WoW.PlayerHasBuff("Moonkin") && WoW.HealthPercent >= 1)
                {
                    // If dont have rejuvenation buff
                    if (WoW.CanCast("Rejuvenation") && !WoW.PlayerHasBuff("Rejuvenation") && WoW.HealthPercent <= 50 && WoW.HealthPercent >= 1)
                    {
                        WoW.CastSpell("Rejuvenation");
                        return;
                    }
                    // If can cast swiftment
                    if (WoW.CanCast("Swiftmend") && WoW.HealthPercent <= 50 && !WoW.IsSpellOnCooldown("Swiftmend") && WoW.HealthPercent >= 1)
                    {
                        WoW.CastSpell("Swiftmend");
                        return;
                    }
                    // Return to moonkin pew pew
                    if (WoW.CanCast("Moonkin") && WoW.IsSpellOnCooldown("Swiftmend") && WoW.PlayerHasBuff("Rejuvenation"))
                    {
                        WoW.CastSpell("Moonkin");
                        return;
                    }
                }
                // Renewal if under 30% HP
                if (Renewal && WoW.CanCast("Renewal") && WoW.HealthPercent >= 1 && WoW.HealthPercent <= 30)
                {
                    WoW.CastSpell("Renewal");
                    return;
                }
				// Pull
				if (WoW.IsInCombat && pullwatch.ElapsedMilliseconds < 10000 && UseCooldowns)
                {
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
					// New Moon
                    if (WoW.IsSpellInRange("Moon") 
                        && WoW.CanCast("Moon"))
                    {
                        WoW.CastSpell("Moon");
                        return;
                    }
					// Moonfire not in target
                    if (WoW.IsSpellInRange("Moonfire")
                        && WoW.CanCast("Moonfire")
                        && !WoW.TargetHasDebuff("Moonfire"))
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    // Sunfire not in target
                    if (WoW.IsSpellInRange("Sunfire")
                        && WoW.CanCast("Sunfire")
                        && !WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
					// CelestialAlignment
					if (!Incarnation && WoW.CanCast("CelestialAlignment"))
                    {
                        WoW.CastSpell("CelestialAlignment");
                        return;
                    }
                    // Incarnation
                    if (Incarnation && WoW.CanCast("Incarnation"))
                    {
                        WoW.CastSpell("Incarnation");
                        return;
                    }
					// HalfMoon
                    if (WoW.CanCast("HalfMoon")
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("HalfMoon");
                        return;
                    }
                    // FullMoon
                    if (WoW.CanCast("FullMoon")
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("FullMoon");
                        return;
                    }
					// Under Celestial Alignment
					if (WoW.PlayerHasBuff("CelestialAlignment"))
						{
                        // KBW if in use
                        if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                        {
                            WoW.CastSpell("KBW");
                            return;
                        }
                        // Starsurge
                        if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40)
						{
							WoW.CastSpell("Starsurge");
							return;
						}
						// Solar Wrath at 3 solar empowerement
						if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerHasBuff("SolarEmp"))
						{
							WoW.CastSpell("SolarW");
                            Thread.Sleep(100);
                            return;
						}
						// Lunar Strike at 3 solar empowerement
						if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp"))
						{
							WoW.CastSpell("LStrike");
                            Thread.Sleep(100);
                            return;
						}
						// Solar Wrath if natures balance and betwean 5-2seconds left on sunfire
						if (NaturesBalance 
							&& WoW.IsSpellInRange("SolarW")
							&& WoW.CanCast("SolarW")
							&& WoW.TargetDebuffTimeRemaining("Sunfire") <= 5
							&& WoW.TargetDebuffTimeRemaining("Sunfire") >= 2)
						{
							WoW.CastSpell("SolarW");
                            Thread.Sleep(100);
                            return;
						}
						// Lunar Strike if natures abalnce and betwean 5-2seconds left on moonfire
						if (NaturesBalance 
							&& WoW.IsSpellInRange("LStrike")
							&& WoW.CanCast("LStrike")
							&& WoW.TargetDebuffTimeRemaining("Moonfire") <= 6
							&& WoW.TargetDebuffTimeRemaining("Moonfire") >= 3)
						{
							WoW.CastSpell("LStrike");
                            Thread.Sleep(100);
                            return;
						}
						// Stellar Flare if not in target
						if (WoW.IsSpellInRange("Stellar Flare")
							&& WoW.CanCast("Stellar Flare")
							&& WoW.CurrentAstralPower >= 15
							&& !WoW.TargetHasDebuff("Stellar Flare"))
						{
							WoW.CastSpell("Stellar Flare");
							return;
						}
						// Stellar Flare if under 5 remaining and at over 15 astral power
						if (StellarFlare && WoW.IsSpellInRange("Stellar Flare")
							&& WoW.CanCast("Stellar Flare")
							&& WoW.CurrentAstralPower >= 15
							&& WoW.TargetDebuffTimeRemaining("Stellar Flare") <= 5)
						{
							WoW.CastSpell("Stellar Flare");
							return;
						}
                        // New Moon
                        if (WoW.IsSpellInRange("Moon")
                            && WoW.CanCast("Moon")
                            && WoW.CurrentAstralPower <= 90
                            && WoW.TargetHasDebuff("Moonfire")
                            && WoW.TargetHasDebuff("Sunfire"))
                        {
                            WoW.CastSpell("Moon");
                            return;
                        }
                        // HalfMoon
                        if (WoW.IsSpellInRange("HalfMoon")
                            && WoW.CanCast("HalfMoon")
                            && WoW.CurrentAstralPower <= 80
                            && WoW.TargetHasDebuff("Moonfire")
                            && WoW.TargetHasDebuff("Sunfire"))
                        {
                            WoW.CastSpell("HalfMoon");
                            return;
                        }
                        // FullMoon
                        if (WoW.IsSpellInRange("FullMoon")
                            && WoW.CanCast("FullMoon")
                            && WoW.CurrentAstralPower <= 60
                            && WoW.TargetHasDebuff("Moonfire")
                            && WoW.TargetHasDebuff("Sunfire"))
                        {
                            WoW.CastSpell("FullMoon");
                            return;
                        }
                        // Cast SolarWrath when nothing else to do
                        if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW"))
						{
							WoW.CastSpell("SolarW");
							return;
						}
						return;
					}
                    // Under Incarnation
                    if (Incarnation && WoW.PlayerHasBuff("Incarnation"))
					{
                        // KBW if in use
                        if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                        {
                            WoW.CastSpell("KBW");
                            return;
                        }
                        // Starsurge
                        if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40)
						{
							WoW.CastSpell("Starsurge");
							return;
						}
						// Solar Wrath at 3 solar empowerement
						if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerHasBuff("SolarEmp"))
						{
							WoW.CastSpell("SolarW");
                            Thread.Sleep(100);
                            return;
						}
						// Lunar Strike at 3 solar empowerement
						if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp"))
						{
							WoW.CastSpell("LStrike");
                            Thread.Sleep(100);
                            return;
						}
						// Solar Wrath if natures balance and betwean 5-2seconds left on sunfire
						if (NaturesBalance 
							&& WoW.IsSpellInRange("SolarW")
							&& WoW.CanCast("SolarW")
							&& WoW.TargetDebuffTimeRemaining("Sunfire") <= 5
							&& WoW.TargetDebuffTimeRemaining("Sunfire") >= 2)
						{
							WoW.CastSpell("SolarW");
							return;
						}
						// Lunar Strike if natures abalnce and betwean 5-2seconds left on moonfire
						if (NaturesBalance 
							&& WoW.IsSpellInRange("LStrike")
							&& WoW.CanCast("LStrike")
							&& WoW.TargetDebuffTimeRemaining("Moonfire") <= 6
							&& WoW.TargetDebuffTimeRemaining("Moonfire") >= 3)
						{
							WoW.CastSpell("LStrike");
							return;
						}
						// Stellar Flare if under 7.2 remaining and at over 15 astral power
						if (StellarFlare && WoW.IsSpellInRange("Stellar Flare")
							&& WoW.CanCast("Stellar Flare")
							&& WoW.CurrentAstralPower >= 15
							&& !WoW.TargetHasDebuff("Stellar Flare"))
						{
							WoW.CastSpell("Stellar Flare");
							return;
						}
						// Stellar Flare if under 7.2 remaining and at over 15 astral power
						if (StellarFlare && WoW.IsSpellInRange("Stellar Flare")
							&& WoW.CanCast("Stellar Flare")
							&& WoW.CurrentAstralPower >= 15
							&& WoW.TargetDebuffTimeRemaining("Stellar Flare") <= 5)
						{
							WoW.CastSpell("Stellar Flare");
							return;
						}
                        // Cast LunarStrike if no SolarEmp and have LunarEmp
                        if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp") && !WoW.PlayerHasBuff("SolarEmp"))
                        {
                            WoW.CastSpell("LStrike");
                            Thread.Sleep(100);
                            return;
                        }
                        // New Moon
                        if (WoW.IsSpellInRange("Moon")
                            && WoW.CanCast("Moon")
                            && WoW.CurrentAstralPower <= 90
                            && WoW.TargetHasDebuff("Moonfire")
                            && WoW.TargetHasDebuff("Sunfire"))
                        {
                            WoW.CastSpell("Moon");
                            return;
                        }
                        // HalfMoon
                        if (WoW.IsSpellInRange("HalfMoon")
                            && WoW.CanCast("HalfMoon")
                            && WoW.CurrentAstralPower <= 80
                            && WoW.TargetHasDebuff("Moonfire")
                            && WoW.TargetHasDebuff("Sunfire"))
                        {
                            WoW.CastSpell("HalfMoon");
                            return;
                        }
                        // FullMoon
                        if (WoW.IsSpellInRange("FullMoon")
                            && WoW.CanCast("FullMoon")
                            && WoW.CurrentAstralPower <= 60
                            && WoW.TargetHasDebuff("Moonfire")
                            && WoW.TargetHasDebuff("Sunfire"))
                        {
                            WoW.CastSpell("FullMoon");
                            return;
                        }
                        // Cast SolarWrath when nothing else to do
                        if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW"))
						{
							WoW.CastSpell("SolarW");
                            Thread.Sleep(100);
                            return;
						}
                        return;
					}
				}
                // Cooldown rotation
                if (WoW.IsInCombat && WoW.HasTarget && UseCooldowns && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin"))
                {
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    // Incarnation
                    // TODO : Confugrable usage
                    if (Incarnation && WoW.CanCast("Incarnation")
                        && WoW.CurrentAstralPower >= 40)
                    {
                        WoW.CastSpell("Incarnation");
                        return;
                    }
                    // Incarnation if Timewarp 80353, Heroism 2825 
                    if (Incarnation && WoW.CanCast("Incarnation") 
                        && WoW.CurrentAstralPower >= 40 
                        && (WoW.PlayerHasBuff("Timewarp") || WoW.PlayerHasBuff("Heroism")))
                    {
                        WoW.CastSpell("Incarnation");
                        return;
                    }
                    // Celestial Alignment if Astral Power bigger than 40
                    // TODO : Configurable usage
                    if (!Incarnation && WoW.CanCast("CelestialAlignment")
                        && WoW.CurrentAstralPower >= 40)
                    {
                        WoW.CastSpell("CelestialAlignment");
                        return;
                    }
                    // Astral Communion if Astral Power smaller than 25
                    // TODO : Confugrable usage
                    if (AstralCommunion && WoW.CanCast("Astral Communion")
                        && WoW.CurrentAstralPower <= 25)
                    {
                        WoW.CastSpell("Astral Communion");
                        return;
                    }
                }
				// Under Celestial Alignment
				if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin") && !WoW.IsMoving && WoW.PlayerHasBuff("CelestialAlignment"))
				{
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    // Starsurge
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40)
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
					// Solar Wrath at 3 solar empowerement
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerHasBuff("SolarEmp"))
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike at 3 solar empowerement
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
					// Solar Wrath if natures balance and betwean 5-2seconds left on sunfire
                    if (NaturesBalance 
					    && WoW.IsSpellInRange("SolarW")
                        && WoW.CanCast("SolarW")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 5
                        && WoW.TargetDebuffTimeRemaining("Sunfire") >= 2)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike if natures abalnce and betwean 5-2seconds left on moonfire
                    if (NaturesBalance 
						&& WoW.IsSpellInRange("LStrike")
                        && WoW.CanCast("LStrike")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 6
                        && WoW.TargetDebuffTimeRemaining("Moonfire") >= 3)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Cast LunarStrike if no SolarEmp and have LunarEmp
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp") && !WoW.PlayerHasBuff("SolarEmp"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Stellar Flare if no stellar flare
                    if (StellarFlare && WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && WoW.CurrentAstralPower >= 15
                        && !WoW.TargetHasDebuff("Stellar Flare"))
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // New Moon
                    if (WoW.IsSpellInRange("Moon")
                        && WoW.CanCast("Moon")
                        && WoW.CurrentAstralPower <= 90
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if (WoW.CurrentAstralPower <= 90)
                        {
                            WoW.CastSpell("Moon");
                            return;
                        }
                        return;
                    }
                    // HalfMoon
                    if (WoW.IsSpellInRange("HalfMoon")
                        && WoW.CanCast("HalfMoon")
                        && WoW.CurrentAstralPower <= 80
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if (WoW.CurrentAstralPower <= 80)
                        {
                            WoW.CastSpell("HalfMoon");
                            return;
                        } 
                        return;
                    }
                    // FullMoon
                    if (WoW.IsSpellInRange("FullMoon")
                        && WoW.CanCast("FullMoon")
                        && WoW.CurrentAstralPower <= 60
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if (WoW.CurrentAstralPower <= 60)
                        {
                            WoW.CastSpell("FullMoon");
                            return;
                        }
                        return;
                    }
                    // Cast SolarWrath when nothing else to do
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW"))
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
				}
				// Under Incarnation
				if (Incarnation && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin") && !WoW.IsMoving && WoW.PlayerHasBuff("Incarnation"))
				{
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    // Starsurge
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40)
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
					// Solar Wrath at 3 solar empowerement
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerHasBuff("SolarEmp"))
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike at 3 solar empowerement
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
					// Solar Wrath if natures balance and betwean 5-2seconds left on sunfire
                    if (NaturesBalance 
					    && WoW.IsSpellInRange("SolarW")
                        && WoW.CanCast("SolarW")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 5
                        && WoW.TargetDebuffTimeRemaining("Sunfire") >= 2)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike if natures abalnce and betwean 5-2seconds left on moonfire
                    if (NaturesBalance 
						&& WoW.IsSpellInRange("LStrike")
                        && WoW.CanCast("LStrike")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 6
                        && WoW.TargetDebuffTimeRemaining("Moonfire") >= 3)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Cast LunarStrike if no SolarEmp and have LunarEmp
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp") && !WoW.PlayerHasBuff("SolarEmp"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Stellar Flare if no stellar flare
                    if (StellarFlare && WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && WoW.CurrentAstralPower >= 15
                        && !WoW.TargetHasDebuff("Stellar Flare"))
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // New Moon
                    if (WoW.IsSpellInRange("Moon")
                        && WoW.CanCast("Moon")
                        && WoW.CurrentAstralPower <= 90
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if(WoW.CurrentAstralPower <= 90)
                        {
                            WoW.CastSpell("Moon");
                            return;
                        }
                        return;
                    }
                    // HalfMoon
                    if (WoW.IsSpellInRange("HalfMoon")
                        && WoW.CanCast("HalfMoon")
                        && WoW.CurrentAstralPower <= 80
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if (WoW.CurrentAstralPower <= 80)
                        {
                            WoW.CastSpell("HalfMoon");
                            return;
                        }
                        return;
                    }
                    // FullMoon
                    if (WoW.IsSpellInRange("FullMoon")
                        && WoW.CanCast("FullMoon")
                        && WoW.CurrentAstralPower <= 60
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if (WoW.CurrentAstralPower <= 60)
                        {
                            WoW.CastSpell("FullMoon");
                            return;
                        }
                        return;
                    }
                    // Cast SolarWrath when nothing else to do
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW"))
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
				}
                // Trinket prog override
                if (WoW.CanCast("Starsurge") && WoW.IsSpellInRange("Starsurge") && WoW.CurrentAstralPower >= 40 && WoW.PlayerHasBuff("FulminationCharge") && WoW.PlayerBuffStacks("FulminationCharge") >= 8)
                {
                    WoW.CastSpell("Starsurge");
                    return;
                }
                if (WoW.CanCast("Moon") && WoW.IsSpellInRange("Moon") && WoW.PlayerHasBuff("FulminationCharge") && !WoW.CanCast("Starsurge"))
                {
                    WoW.CastSpell("HalfMoon");
                    return;
                }
                if (WoW.CanCast("HalfMoon") && WoW.IsSpellInRange("HalfMoon") && WoW.PlayerHasBuff("FulminationCharge") && !WoW.CanCast("Starsurge"))
                {
                    WoW.CastSpell("HalfMoon");
                    return;
                }
                if (WoW.CanCast("FullMoon") && WoW.IsSpellInRange("FullMoon") && WoW.PlayerHasBuff("FulminationCharge") && !WoW.CanCast("Starsurge"))
                {
                    WoW.CastSpell("FullMoon");
                    return;
                }
                // Main single target rotation
                if (WoW.IsInCombat && WoW.HasTarget&& WoW.TargetIsEnemy&&WoW.PlayerHasBuff("Moonkin")&&!WoW.IsMoving)
                {
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    // Owlkin Frenzy
                    if (WoW.PlayerHasBuff("OwlkinFrenzy"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Priority execute order
                    // New Moon
                    if (WoW.IsSpellInRange("Moon") 
                        && WoW.CanCast("Moon")
                        && WoW.PlayerSpellCharges("Moon") == 3
                        && WoW.TargetHasDebuff("Moonfire") 
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Moon");
                        return;
                    }
                    // HalfMoon
                    if (WoW.IsSpellInRange("HalfMoon")
                        && WoW.CanCast("HalfMoon")
                        && WoW.PlayerSpellCharges("HalfMoon") == 3
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("HalfMoon");
                        return;
                    }
                    // FullMoon
                    if (WoW.IsSpellInRange("FullMoon")
                        && WoW.CanCast("FullMoon")
                        && WoW.PlayerSpellCharges("FullMoon") == 3
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("FullMoon");
                        return;
                    }
                    // Moonfire if under 6.6 remaining (no Natures Balance)
                    if (!NaturesBalance
                        && WoW.IsSpellInRange("Moonfire")
                        && WoW.CanCast("Moonfire")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 6.6)
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    // Sunfire if under 5.4 remaining (no Natures Balance)
                    if (!NaturesBalance 
                        && WoW.IsSpellInRange("Sunfire")
                        && WoW.CanCast("Sunfire")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 5.4)
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    // Moonfire if under 3 remaining (Natures Balance)
                    if (NaturesBalance 
					    && WoW.IsSpellInRange("Moonfire")
                        && WoW.CanCast("Moonfire")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 3)
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    // Sunfire if under 3 remaining (Natures Balance)
                    if (NaturesBalance
					    && WoW.IsSpellInRange("Sunfire")
                        && WoW.CanCast("Sunfire")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 3)
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    // Stellar Flare if under 7.2 remaining and at over 15 astral power
                    if (WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && WoW.CurrentAstralPower >= 15
                        && WoW.TargetDebuffTimeRemaining("Stellar Flare") <= 7.2)
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // Solar Wrath at 3 solar empowerement
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerBuffStacks("SolarEmp") == 3)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike at 3 solar empowerement
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerBuffStacks("LunarEmp") == 3)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Moonfire if not on target
                    if (WoW.IsSpellInRange("Moonfire")
                        && WoW.CanCast("Moonfire")
                        && !WoW.TargetHasDebuff("Moonfire"))
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    // Sunfire if not on target
                    if (WoW.IsSpellInRange("Sunfire")
                        && WoW.CanCast("Sunfire")
                        && !WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    // Stellar Flare if not on target
                    if (WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && !WoW.TargetHasDebuff("Stellar Flare"))
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // Stellar Flare if not on target
                    if (WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && WoW.TargetDebuffTimeRemaining("Stellar Flare") < 5)
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // Secondary priority execute order
                    // New Moon
                    if (WoW.IsSpellInRange("Moon")
                        && WoW.CanCast("Moon")
                        && WoW.CurrentAstralPower <= 90
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if (WoW.CurrentAstralPower <= 90)
                        {
                            WoW.CastSpell("Moon");
                            return;
                        }
                        return;
                    }
                    // HalfMoon
                    if (WoW.IsSpellInRange("HalfMoon")
                        && WoW.CanCast("HalfMoon")
                        && WoW.CurrentAstralPower <= 80
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if (WoW.CurrentAstralPower <= 80)
                        {
                            WoW.CastSpell("HalfMoon");
                            return;
                        }
                        return;
                    }
                    // FullMoon
                    if (WoW.IsSpellInRange("FullMoon")
                        && WoW.CanCast("FullMoon")
                        && WoW.CurrentAstralPower <= 60
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        if (WoW.CurrentAstralPower <= 60)
                        {
                            WoW.CastSpell("FullMoon");
                            return;
                        }
                        return;
                    }
                    // Starsurge for Emerald Dreamcatcher
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.PlayerSpellCharges("Starsurge") <= 2 && WoW.CurrentAstralPower >= 40)
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
                    // Starsurge
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 80 && (!WoW.PlayerHasBuff("SolarEmp") || WoW.PlayerBuffStacks("SolarEmp") < 3) && (!WoW.PlayerHasBuff("LunarEmp") || WoW.PlayerBuffStacks("LunarEmp") < 3))
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
                    // Solar Wrath at solar empowerement
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerBuffStacks("SolarEmp") <= 1)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Solar Wrath if natures balance and betwean 5-2seconds left on sunfire
                    if (NaturesBalance 
					    && WoW.IsSpellInRange("SolarW")
                        && WoW.CanCast("SolarW")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 5
                        && WoW.TargetDebuffTimeRemaining("Sunfire") >= 2)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike if natures abalnce and betwean 5-2seconds left on moonfire
                    if (NaturesBalance 
						&& WoW.IsSpellInRange("LStrike")
                        && WoW.CanCast("LStrike")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 6
                        && WoW.TargetDebuffTimeRemaining("Moonfire") >= 3)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Cast LunarStrike if no SolarEmp and have LunarEmp
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp") && !WoW.PlayerHasBuff("SolarEmp"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Cast SolarWrath when nothing else to do
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW"))
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    return;

                }
                // Stellar Drift
                if (StellarDrift && WoW.IsMoving && WoW.PlayerHasBuff("StarfallP") && WoW.IsSpellInRange("LStrike"))
                {
                    if (WoW.CanCast("FullMoon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 3)
                    {
                        WoW.CastSpell("FullMoon");
                        return;
                    }
                    if (WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 2.5)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    if (WoW.CanCast("Moon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 1.2)
                    {
                        WoW.CastSpell("Moon");
                        return;
                    }
                    if (WoW.CanCast("HalfMoon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 2)
                    {
                        WoW.CastSpell("HalfMoon");
                        return;
                    }
                    if (WoW.CanCast("SolarW") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 2)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }

                }
                if (WoW.IsInCombat && WoW.HasTarget&& WoW.TargetIsEnemy&&WoW.PlayerHasBuff("Moonkin")&&WoW.IsMoving)
				{
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40 && (!WoW.PlayerHasBuff("SolarEmp") || WoW.PlayerBuffStacks("SolarEmp") < 3) && (!WoW.PlayerHasBuff("LunarEmp") || WoW.PlayerBuffStacks("LunarEmp") < 3))
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
                    if (WoW.IsSpellInRange("Moonfire") && WoW.CanCast("Moonfire") && !WoW.TargetHasDebuff("Moonfire"))
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    if (WoW.IsSpellInRange("Sunfire") && WoW.CanCast("Sunfire") && WoW.CurrentAstralPower <= 40)
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
				}
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (!WoW.PlayerIsCasting && WoW.IsInCombat && WoW.CanCast("Moonkin"))
                {
                    WoW.CastSpell("Moonkin");
                    return;
                }
				// Cooldown rotation
                if (WoW.IsInCombat && WoW.HasTarget && UseCooldowns && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin"))
                {
                    //// Kil'jaeden's Burning Wish
                    //if (KBW && !WoW.ItemOnCooldown("KBW"))

                    // Celestial Alignment if Astral Power bigger than 40
                    // TODO : Configurable usage
                    if (!Incarnation && WoW.CanCast("CelestialAlignment")
                        && WoW.CurrentAstralPower >= 40)
                    {
                        WoW.CastSpell("CelestialAlignment");
                        return;
                    }
                    // Astral Communion if Astral Power smaller than 25
                    // TODO : Confugrable usage
                    if (AstralCommunion && WoW.CanCast("Astral Communion")
                        && WoW.CurrentAstralPower <= 25)
                    {
                        WoW.CastSpell("Astral Communion");
                        return;
                    }
                    // Incarnation
                    // TODO : Confugrable usage
                    if (Incarnation && WoW.CanCast("Incarnation")
                        && WoW.CurrentAstralPower >= 40)
                    {
                        WoW.CastSpell("Incarnation");
                        return;
                    }
                }
                // Soul of the Forest Rotation
                if (SouloftheForest && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin") && WoW.IsSpellInRange("LStrike"))
                {
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    if (WoW.CanCast("Sunfire") && WoW.IsSpellInRange("Sunfire") && !WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    if (WoW.CanCast("Moonfire") && WoW.IsSpellInRange("Sunfire") && !WoW.TargetHasDebuff("Moonfire"))
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    if (StellarDrift && WoW.IsMoving && WoW.CanCast("FullMoon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 3)
                    {
                        WoW.CastSpell("FullMoon");
                        return;
                    }
                    if (StellarDrift && WoW.IsMoving && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 2.5)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    if (StellarDrift && WoW.IsMoving && WoW.CanCast("Moon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 1.2)
                    {
                        WoW.CastSpell("Moon");
                        return;
                    }
                    if (StellarDrift && WoW.IsMoving && WoW.CanCast("HalfMoon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 2)
                    {
                        WoW.CastSpell("HalfMoon");
                        return;
                    }
                    if (WoW.CanCast("Starfall") && WoW.CurrentAstralPower >= 40 && !WoW.TargetHasDebuff("StarfallT"))
                    {
                        WoW.CastSpell("Starfall");
                        return;
                    }
                    if (WoW.CanCast("Starfall") && WoW.CurrentAstralPower >= 70)
                    {
                        WoW.CastSpell("Starfall");
                        return;
                    }
                    if (WoW.IsMoving && WoW.CanCast("Sunfire"))
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    if (WoW.CanCast("Moon"))
                    {
                        WoW.CastSpell("Moon");
                        return;
                    }
                    if (WoW.CanCast("HalfMoon"))
                    {
                        WoW.CastSpell("HalfMoon");
                        return;
                    }
                    if (WoW.CanCast("FullMoon"))
                    {
                        WoW.CastSpell("FullMoon");
                        return;
                    }
                    if (WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40 && WoW.TargetHasDebuff("StarfallT") && (WoW.PlayerHasBuff("Incarnation") || WoW.PlayerHasBuff("CelestialAlignment")))
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
                    if (WoW.CanCast("LStrike") && WoW.PlayerHasBuff("StarfallP"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                }
                // Stellar Drift
                if (StellarDrift && WoW.IsMoving && WoW.PlayerHasBuff("StarfallP") && WoW.IsSpellInRange("LStrike"))
                {
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    if (WoW.CanCast("FullMoon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 3)
                    {
                        WoW.CastSpell("FullMoon");
                        return;
                    }
                    if (WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 2.5)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    if (WoW.CanCast("Moon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 1.2)
                    {
                        WoW.CastSpell("Moon");
                        return;
                    }
                    if (WoW.IsMoving && WoW.CanCast("HalfMoon") && WoW.PlayerHasBuff("StarfallP") && WoW.PlayerBuffTimeRemaining("StarfallP") >= 2)
                    {
                        WoW.CastSpell("HalfMoon");
                        return;
                    }
                }
				// Under Celestial Alignment
				if (!SouloftheForest && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin") && !WoW.IsMoving && WoW.PlayerHasBuff("CelestialAlignment"))
				{
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    // Stellar Drift
                    if (StellarDrift && WoW.CanCast("Starfall") && WoW.CurrentAstralPower >= 70)
                    {
                        WoW.CastSpell("Starfall");
                        return;
                    }
                    // Starsurge
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40 && WoW.TargetHasDebuff("StarfallT"))
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
					// Solar Wrath at 3 solar empowerement
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerHasBuff("SolarEmp"))
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike at 3 solar empowerement
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
					// Solar Wrath if natures balance and betwean 5-2seconds left on sunfire
                    if (NaturesBalance 
					    && WoW.IsSpellInRange("SolarW")
                        && WoW.CanCast("SolarW")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 5
                        && WoW.TargetDebuffTimeRemaining("Sunfire") >= 2)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike if natures abalnce and betwean 5-2seconds left on moonfire
                    if (NaturesBalance 
						&& WoW.IsSpellInRange("LStrike")
                        && WoW.CanCast("LStrike")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 6
                        && WoW.TargetDebuffTimeRemaining("Moonfire") >= 3)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Stellar Flare if no stellar flare
                    if (StellarFlare && WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && WoW.CurrentAstralPower >= 15
                        && !WoW.TargetHasDebuff("Stellar Flare"))
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // Cast SolarWrath when nothing else to do
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
				}
				// Under Incarnation
				if (!SouloftheForest && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin") && !WoW.IsMoving && WoW.PlayerHasBuff("Incarnation"))
				{
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    // Stellar Drift
                    if (StellarDrift && WoW.CanCast("Starfall") && WoW.CurrentAstralPower >= 70)
                    {
                        WoW.CastSpell("Starfall");
                        return;
                    }
                    // Starsurge
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40 && WoW.TargetHasDebuff("StarfallT"))
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
                    // Lunar Strike at 3 solar empowerement
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerHasBuff("LunarEmp"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
					// Solar Wrath at 3 solar empowerement
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerHasBuff("SolarEmp"))
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
					// Solar Wrath if natures balance and betwean 5-2seconds left on sunfire
                    if (NaturesBalance 
					    && WoW.IsSpellInRange("SolarW")
                        && WoW.CanCast("SolarW")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 5
                        && WoW.TargetDebuffTimeRemaining("Sunfire") >= 2)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike if natures abalnce and betwean 5-2seconds left on moonfire
                    if (NaturesBalance 
						&& WoW.IsSpellInRange("LStrike")
                        && WoW.CanCast("LStrike")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 6
                        && WoW.TargetDebuffTimeRemaining("Moonfire") >= 3)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Stellar Flare if no stellar flare
                    if (StellarFlare && WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && WoW.CurrentAstralPower >= 15
                        && !WoW.TargetHasDebuff("Stellar Flare"))
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // Cast SolarWrath when nothing else to do
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
				}
                if (!SouloftheForest && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin") && !WoW.IsMoving)
                {
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    // Moonfire if not on target
                    if (WoW.IsSpellInRange("Moonfire")
                        && WoW.CanCast("Moonfire")
                        && !WoW.TargetHasDebuff("Moonfire"))
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    // Sunfire if not on target
                    if (WoW.IsSpellInRange("Sunfire")
                        && WoW.CanCast("Sunfire")
                        && !WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    // Priority execute order
                    // New Moon
                    if (WoW.IsSpellInRange("Moon")
                        && WoW.CanCast("Moon")
                        && WoW.PlayerSpellCharges("Moon") == 3
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Moon");
                        return;
                    }
                    // HalfMoon
                    if (WoW.IsSpellInRange("HalfMoon")
                        && WoW.CanCast("HalfMoon")
                        && WoW.PlayerSpellCharges("HalfMoon") == 3
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("HalfMoon");
                        return;
                    }
                    // FullMoon
                    if (WoW.IsSpellInRange("FullMoon")
                        && WoW.CanCast("FullMoon")
                        && WoW.PlayerSpellCharges("FullMoon") == 3
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("FullMoon");
                        return;
                    }
                    // Starfall
                    if (WoW.CanCast("Starfall") && WoW.CurrentAstralPower >= 60)
                    {
                        WoW.CastSpell("Starfall");
                        return;
                    }
                    // Moonfire if under 6.6 remaining and Natures Balance checked
                    if (!NaturesBalance
                        && WoW.IsSpellInRange("Moonfire")
                        && WoW.CanCast("Moonfire")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 6.6)
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    // Sunfire if under 5.4 remaining and Natures Balance checked
                    if (!NaturesBalance
                        && WoW.IsSpellInRange("Sunfire")
                        && WoW.CanCast("Sunfire")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 5.4)
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    // Moonfire if under 3 remaining (no natures balance)
                    if (NaturesBalance 
						&& WoW.IsSpellInRange("Moonfire")
                        && WoW.CanCast("Moonfire")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 3)
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    // Sunfire if under 3 remaining (no natures balance)
                    if (NaturesBalance 
						&& WoW.IsSpellInRange("Sunfire")
                        && WoW.CanCast("Sunfire")
                        && WoW.TargetDebuffTimeRemaining("Sunfire") <= 3)
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    // Stellar Flare if under 7.2 remaining and at over 15 astral power
                    if (WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && WoW.CurrentAstralPower >= 15
                        && WoW.TargetDebuffTimeRemaining("Stellar Flare") <= 7.2)
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // Lunar Strike at 3 solar empowerement
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerBuffStacks("LunarEmp") == 3)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Solar Wrath at 3 solar empowerement
                    if (WoW.IsSpellInRange("SolarW") && WoW.CanCast("SolarW") && WoW.PlayerBuffStacks("SolarEmp") == 3)
                    {
                        WoW.CastSpell("SolarW");
                        Thread.Sleep(100);
                        return;
                    }
                    // Moonfire if not on target
                    if (WoW.IsSpellInRange("Moonfire")
                        && WoW.CanCast("Moonfire")
                        && !WoW.TargetHasDebuff("Moonfire"))
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    // Sunfire if not on target
                    if (WoW.IsSpellInRange("Sunfire")
                        && WoW.CanCast("Sunfire")
                        && !WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                    // Stellar Flare if not on target
                    if (WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && !WoW.TargetHasDebuff("Stellar Flare"))
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // Stellar Flare if not on target
                    if (WoW.IsSpellInRange("Stellar Flare")
                        && WoW.CanCast("Stellar Flare")
                        && WoW.TargetDebuffTimeRemaining("Stellar Flare") < 5)
                    {
                        WoW.CastSpell("Stellar Flare");
                        return;
                    }
                    // Secondary priority execute order
                    // New Moon
                    if (WoW.IsSpellInRange("Moon")
                        && WoW.CanCast("Moon")
                        && WoW.CurrentAstralPower <= 90
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Moon");
                        return;
                    }
                    // HalfMoon
                    if (WoW.IsSpellInRange("HalfMoon")
                        && WoW.CanCast("HalfMoon")
                        && WoW.CurrentAstralPower <= 80
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("HalfMoon");
                        return;
                    }
                    // FullMoon
                    if (WoW.IsSpellInRange("FullMoon")
                        && WoW.CanCast("FullMoon")
                        && WoW.CurrentAstralPower <= 60
                        && WoW.TargetHasDebuff("Moonfire")
                        && WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("FullMoon");
                        return;
                    }
                    // Starsurge to prevent capping AP
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 90)
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
                    // Lunar Strike at lunar empowerement
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike") && WoW.PlayerBuffStacks("LunarEmp") <= 1)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Lunar Strike if natures abalnce and betwean 5-2seconds left on moonfire
                    if (NaturesBalance 
						&& WoW.IsSpellInRange("LStrike")
                        && WoW.CanCast("LStrike")
                        && WoW.TargetDebuffTimeRemaining("Moonfire") <= 6
                        && WoW.TargetDebuffTimeRemaining("Moonfire") >= 3)
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }
                    // Cast Lunar Strike when nothing else to do
                    if (WoW.IsSpellInRange("LStrike") && WoW.CanCast("LStrike"))
                    {
                        WoW.CastSpell("LStrike");
                        Thread.Sleep(100);
                        return;
                    }

                }
                if (!SouloftheForest && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerHasBuff("Moonkin") && WoW.IsMoving)
                {
                    // KBW if in use
                    if (KBW && !WoW.ItemOnCooldown("KBW") && WoW.IsSpellInRange("LStrike"))
                    {
                        WoW.CastSpell("KBW");
                        return;
                    }
                    if (WoW.IsSpellInRange("Moonfire") && WoW.CanCast("Moonfire") && WoW.CurrentAstralPower <= 40)
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    if (WoW.IsSpellInRange("Starsurge") && WoW.CanCast("Starsurge") && WoW.CurrentAstralPower >= 40)
                    {
                        WoW.CastSpell("Starsurge");
                        return;
                    }
                    if (WoW.IsSpellInRange("Sunfire") && WoW.CanCast("Sunfire") && !WoW.TargetHasDebuff("Sunfire"))
                    {
                        WoW.CastSpell("Sunfire");
                        return;
                    }
                }
            }
        }
        
        public override Form SettingsForm { get; set; }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Daniel and Scotishdwarf
AddonName=HideOrderHallBar
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,8921,Moonfire,D4
Spell,93402,Sunfire,F8
Spell,202767,Moon,G
Spell,78674,Starsurge,D3
Spell,191034,Starfall,D8
Spell,194153,LStrike,D2
Spell,190984,SolarW,D1
Spell,202771,FullMoon,G
Spell,202768,HalfMoon,G
Spell,202347,Stellar Flare,F9
Spell,194223,CelestialAlignment,Z
Spell,202359,Astral Communion,F10
Spell,202430,NaturesBalance,E
Spell,102560,Incarnation,Z
Spell,18562,Swiftmend,D4
Spell,774,Rejuvenation,D1
Spell,24858,Moonkin,F11
Spell,108238,Renewal,F7
Spell,202360,BlessingOfAncients,F10
Spell,235991,KBW,T
Aura,164547,LunarEmp
Aura,164545,SolarEmp
Aura,164812,Moonfire
Aura,93402,Sunfire
Aura,24858,Moonkin
Aura,202347,Stellar Flare
Aura,194223,CelestialAlignment
Aura,102560,Incarnation
Aura,80353,Timewarp
Aura,2825,Heroism
Aura,774,Rejuvenation
Aura,215632,FulminationCharge
Aura,202737,BlessingOfElune
Aura,202739,BlessingOfAnshe
Aura,157228,OwlkinFrenzy
Aura,191034,StarfallP
Aura,197637,StarfallT
Item,144259,KBW
*/
