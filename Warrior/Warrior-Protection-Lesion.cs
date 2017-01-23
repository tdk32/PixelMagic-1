// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using PixelMagic.Helpers;
using PixelMagic.GUI;

namespace PixelMagic.Rotation
{
    public class ProtectionLesion : CombatRoutine
    {
        public override string Name => "Protection Warrior";

        public override string Class => "Warrior";

        private readonly Stopwatch swingwatch = new Stopwatch();

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Protection Warrior", Color.Green);
            Log.Write("Suggested build: 1213112", Color.Green);
            Log.Write("2.0", Color.Green);
            Log.Write("Last Edited by Lesion 21/01/17 - PM API changes", Color.Green);
            WoW.Speak("Welcome to PixelMagic Protection by Lesion");
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {

            if (WoW.IsInCombat && WoW.IsSpellInRange("Shield Slam")) {
                swingwatch.Start();
            }

            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Shield Slam")) {

                    //SB
                    if (WoW.CanCast("Shield Block") && WoW.HealthPercent <= 99 && WoW.Rage >= 15 && !WoW.IsSpellOnCooldown("Shield Block") && !WoW.PlayerHasBuff("Shield Block")) {
                        WoW.CastSpell("Shield Block");
                        return;
                    }

                    if (WoW.CanCast("Shield Block") && WoW.Rage >= 15 && WoW.PlayerBuffTimeRemaining("Shield Block") <= 1) {
                        WoW.CastSpell("Shield Block");
                        return;
                    }


                    // IP Control
                    if (WoW.CanCast("Ignore Pain") && (WoW.Rage >= 50) && !WoW.PlayerHasBuff("Vengeance Ignore Pain")) {
                        WoW.CastSpell("Ignore Pain");
                        return;
                    }
                    if (WoW.CanCast("Ignore Pain") && (WoW.Rage >= 13) && WoW.PlayerHasBuff("Vengeance Ignore Pain")) {
                        WoW.CastSpell("Ignore Pain");
                    }

                    // Revenge Control
                    if (WoW.CanCast("Revenge") && WoW.Rage >= 45 && !WoW.PlayerHasBuff("Vengeance Revenge")) {
                        WoW.CastSpell("Revenge");
                    }
                    if (WoW.CanCast("Revenge") && WoW.IsSpellOverlayed("Revenge") && WoW.PlayerHasBuff("Vengeance Revenge")) {
                        WoW.CastSpell("Revenge");
                    }
                    if (WoW.CanCast("Revenge") && WoW.Rage >= 34 && WoW.PlayerHasBuff("Vengeance Revenge") && !WoW.IsSpellOnCooldown("Revenge")) {
                        WoW.CastSpell("Revenge");
                    }

                    //Rotational shiz
                    //It will wait 2.7 seconds for a devastate too proc shield slam. (bastardized swing timer)
                    if (!WoW.IsSpellOnCooldown("Shield Slam") && swingwatch.ElapsedMilliseconds > 2700) {
                        if (!WoW.IsSpellOverlayed("Shield Slam")) {
                            WoW.CastSpell("Shield Slam");
                            swingwatch.Reset();
                            swingwatch.Start();
                            return;

                        }
                    }
                    //will cast SS when proc's
                    if (WoW.CanCast("Shield Slam") && WoW.IsSpellOverlayed("Shield Slam")) {
                        WoW.CastSpell("Shield Slam");
                        swingwatch.Reset();
                        swingwatch.Start();
                        return;
                    }

                    //if (WoW.CanCast("Devastate") && WoW.IsSpellOnCooldown("Revenge"))
                    //{
                    //   WoW.CastSpell("Devastate");
                    //    return;
                    //}
                    if (WoW.CanCast("Thunder Clap") && !WoW.IsSpellOnCooldown("Thunder Clap")) {
                        WoW.CastSpell("Thunder Clap");
                        return;
                    }

                    //if (WoW.CanCast("Impending Victory") && WoW.Rage >= 10 && !WoW.IsSpellOnCooldown("Impending Victory") && WoW.HealthPercent <= 80)
                    //{
                    //	WoW.CastSpell("Impending Victory");
                    //}



                }
                //Artifact / Shockwave Combo.
                if (WoW.CanCast("Neltharion's Fury") && WoW.TargetHasDebuff("ShockWavestun")) {
                    WoW.CastSpell("Neltharion's Fury");
                    return;
                }

            }
            if (combatRoutine.Type == RotationType.AOE) {
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
Aura,2565,Shield Block
Aura,132168,ShockWavestun
Aura,122510,Ultimatum
Aura,202573,Vengeance Revenge
Aura,202574,Vengeance Ignore Pain
Aura,190456,Ignore Pain
Aura,203576,Dragon Scales
*/
