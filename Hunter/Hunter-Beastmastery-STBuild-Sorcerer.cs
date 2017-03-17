// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using PixelMagic.Helpers;
namespace PixelMagic.Rotation
{
    public class HunterBeastmastery : CombatRoutine
    {
        public override Form SettingsForm { get; set; }

        public override string Name
        {
            get
            {
                return "Hunter BM";
            }
        }

        public override string Class
        {
            get
            {
                return "Hunter";
            }
        }

        public override void Initialize()
        {
            Log.Write("Welcome to Beastmastery Hunter", Color.Green);
            Log.Write("Optimized for talent build 2112112", Color.Blue);
            Log.Write("May work with other builds but no guarantee", Color.Blue);
            Log.Write("Manual switch between ST/AoE/Cleave", Color.Blue);
        }

        public override void Stop()
        {
        }

        public override void Pulse()        // Updated for Legion (tested and working for single target)
        {            

            //Healthstone - Potion
            if ((WoW.CanCast("Healthstone") || WoW.CanCast("Potion"))
                && (WoW.ItemCount("Healthstone") >= 1 || WoW.ItemCount("Potion") >= 1)
                && (!WoW.ItemOnCooldown("Healthstone") || !WoW.ItemOnCooldown("Potion"))
                && !WoW.PlayerHasBuff("Feign Death")
                && WoW.HealthPercent <= 30
                && !WoW.PlayerHasBuff("Mount")
                && WoW.HealthPercent != 0)
            {
                Thread.Sleep(500);
                WoW.CastSpell("Healthstone");
                WoW.CastSpell("Potion");
                return;
            }

            //Exhilaration
            if (WoW.CanCast("Exhilaration")
                && WoW.HealthPercent <= 20
                && !WoW.PlayerHasBuff("Mount")
                && !WoW.PlayerHasBuff("Feign Death")
                && WoW.HealthPercent != 0)
            {
                WoW.CastSpell("Exhilaration");
                return;
            }

            //Counter Shot
            if (WoW.CanCast("Counter Shot")
                && WoW.TargetIsCastingAndSpellIsInterruptible
                && !WoW.IsSpellOnCooldown("Counter Shot")
                && WoW.TargetIsCasting
                && WoW.IsSpellInRange("Counter Shot")
                && WoW.TargetPercentCast > 80)
            {
                WoW.CastSpell("Counter Shot");
                return;
            }

            //Call pet
            if (!WoW.HasPet
                && !WoW.PlayerHasBuff("Mount")
                && !WoW.PlayerHasBuff("Feign Death")
                && WoW.HealthPercent != 0)
            {
                WoW.CastSpell("Call Pet");
                return;
            }

            //Revive Pet
            if ((!WoW.HasPet || WoW.PetHealthPercent < 1)
                && !WoW.PlayerHasBuff("Mount")
                && !WoW.PlayerHasBuff("Feign Death")
                && WoW.HealthPercent != 0)
            {
                WoW.CastSpell("Heart of the Phoenix");
                WoW.CastSpell("Revive Pet");
                return;
            }   
            
            //Intimidation //Binding Shot
            if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_KEY_Z) < 0
                && (WoW.CanCast("Intimidation") || WoW.CanCast("Binding Shot")))
            {
                WoW.CastSpell("Binding Shot");
                WoW.CastSpell("Intimidation");
                return;
            }

            /*/Voley
            if (WoW.CanCast("Voley")
                && !WoW.PlayerHasBuff("Feign Death")
                && !WoW.PlayerHasBuff("Voley")
                && WoW.HealthPercent != 0)
            {
                WoW.CastSpell("Voley");
                return;
            }*/

            if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerHasBuff("Mount") && !WoW.PlayerIsChanneling && !WoW.PlayerHasBuff("Feign Death") && WoW.HealthPercent != 0)
            {
                //Stampede
                if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_LSHIFT) < 0
                    && WoW.CanCast("Stampede"))
                {
                    WoW.CastSpell("Stampede");
                    return;
                }

                //Cooldowns
                if (UseCooldowns)
                {
                    //Aspect of the Wild
                    if (WoW.CanCast("Aspect of the Wild")
                        && !WoW.PlayerHasBuff("Aspect of the Turtle")
                        && WoW.PlayerHasBuff("Bestial Wrath")
                        && WoW.PlayerBuffTimeRemaining("Bestial Wrath") >= 12)
                    {
                        WoW.CastSpell("Aspect of the Wild");
                    }
                }

                //Legendary Trinket
                if (WoW.CanCast("Kil'jaeden's Burning Wish")
                    && (((WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 15 && WoW.SpellCooldownTimeRemaining("Dire Beast") > 5)) || WoW.PlayerHasBuff("Bestial Wrath"))
                    && !WoW.ItemOnCooldown("Kil'jaeden's Burning Wish")
                    && WoW.IsSpellInRange("Cobra Shot"))
                {
                    WoW.CastSpell("Kil'jaeden's Burning Wish");
                }

                //SINGLE TARGET

                //Bestial Wrath
                if ((combatRoutine.Type == RotationType.SingleTarget || combatRoutine.Type == RotationType.SingleTargetCleave)
                    && WoW.CanCast("Bestial Wrath")
                    && !WoW.PlayerHasBuff("Aspect of the Turtle")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && (WoW.Focus >= 100 || (WoW.Focus >= 90 && WoW.CanCast("Aspect of the Wild"))))
                {
                    WoW.CastSpell("Bestial Wrath");
                    WoW.CastSpell("Kill Command");
                }

                //A Murder of Crows
                if (combatRoutine.Type == RotationType.SingleTarget
                   // && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 23
                    && WoW.CanCast("A Murder of Crows")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && WoW.Focus >= 30)
                {
                    WoW.CastSpell("A Murder of Crows");
                    return;
                }

                //Kill Command
                if (combatRoutine.Type == RotationType.SingleTarget
                    && (WoW.SpellCooldownTimeRemaining("Kill Command") <= 1 || WoW.CanCast("Kill Command"))
                    && WoW.Focus >= 100)
                {
                    WoW.CastSpell("Kill Command");
                    return;
                }

                // Dire beast
                if (combatRoutine.Type == RotationType.SingleTarget
                    && WoW.CanCast("Dire Beast")
                    && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 3
                    && WoW.IsSpellInRange("Cobra Shot"))
                {
                    WoW.CastSpell("Dire Beast");
                    WoW.CastSpell("Titan's Thunder");
                    return;
                }

                /*/Dire Frenzy
                if (combatRoutine.Type == RotationType.SingleTarget
                    && WoW.CanCast("Dire Frenzy")
                    && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 6
                    && WoW.IsSpellInRange("Cobra Shot"))
                {
                    WoW.CastSpell("Titan's Thunder");
                    WoW.CastSpell("Dire Frenzy");
                    return;
                }*/

                //Kill Command
                if (combatRoutine.Type == RotationType.SingleTarget
                    && (WoW.SpellCooldownTimeRemaining("Kill Command") <= 1 || WoW.CanCast("Kill Command"))
                    && WoW.Focus >= 30)
                {
                    WoW.CastSpell("Kill Command");
                    return;
                }

                /*/Chimaera Shot
                if (combatRoutine.Type == RotationType.SingleTarget
                    && WoW.CanCast("Chimaera Shot")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && WoW.Focus < 80)
                {
                    WoW.CastSpell("Chimaera Shot");
                    return;
                }*/

                //Cobra Shot
                if (combatRoutine.Type == RotationType.SingleTarget
                    && ((WoW.Focus >= 90) || (WoW.PlayerHasBuff("Bestial Wrath") && WoW.Focus >= 40))
                    && WoW.IsSpellInRange("Cobra Shot")
                    && WoW.CanCast("Cobra Shot")
                    && !WoW.CanCast("Bestial Wrath"))
                {
                    WoW.CastSpell("Cobra Shot");
                    return;
                }

                //AOE

                //Bestial Wrath
                if (combatRoutine.Type == RotationType.AOE
                    && WoW.CanCast("Bestial Wrath")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && !WoW.PlayerHasBuff("Aspect of the Turtle"))
                {
                    WoW.CastSpell("Bestial Wrath");
                    return;
                }

                // Dire beast
                if (combatRoutine.Type == RotationType.AOE
                    && WoW.CanCast("Dire Beast")
                    && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 3
                    && WoW.IsSpellInRange("Cobra Shot"))
                {
                    WoW.CastSpell("Dire Beast");
                    WoW.CastSpell("Titan's Thunder");
                    return;
                }

                /*/Dire Frenzy
                if (combatRoutine.Type == RotationType.AOE
                    && WoW.CanCast("Dire Frenzy")
                    && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 6
                    && WoW.IsSpellInRange("Cobra Shot"))
                {
                    WoW.CastSpell("Dire Frenzy");
                    WoW.CastSpell("Titan's Thunder");
                    return;
                }

                //Barrage
                if (combatRoutine.Type == RotationType.AOE
                    && WoW.CanCast("Barrage")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && (WoW.Focus >= 60))
                {
                    WoW.CastSpell("Barrage");
                    return;
                }*/

                //Multishot
                if (combatRoutine.Type == RotationType.AOE
                    && WoW.CanCast("Multi-Shot")
                    && WoW.IsSpellInRange("Multi-Shot")
                    && WoW.Focus >= 40)
                {
                    WoW.CastSpell("Multi-Shot");
                    return;
                }

                /*/Chimaera Shot
                if (combatRoutine.Type == RotationType.AOE
                    && WoW.CanCast("Chimaera Shot")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && WoW.Focus < 80)
                {
                    WoW.CastSpell("Chimaera Shot");
                    return;
                }*/

                //CLEAVE

                //A Murder of Crows
                if (combatRoutine.Type == RotationType.SingleTarget
                    && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 23
                    && WoW.CanCast("A Murder of Crows")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && WoW.Focus >= 30)
                {
                    WoW.CastSpell("A Murder of Crows");
                    return;
                }

                //Multishot - Beast Cleave uptime
                if (combatRoutine.Type == RotationType.SingleTargetCleave
                    && WoW.CanCast("Multi-Shot")
                    && (!WoW.PetHasBuff("BeastCleave") || WoW.PetBuffTimeRemaining("BeastCleave") < 0.5)
                    && WoW.IsSpellInRange("Multi-Shot")
                    && !WoW.CanCast("Bestial Wrath")
                    && WoW.Focus >= 40)
                {
                    WoW.CastSpell("Multi-Shot");
                    return;
                }

                // Dire beast
                if (combatRoutine.Type == RotationType.SingleTargetCleave
                    && WoW.CanCast("Dire Beast")
                    && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 3
                    && WoW.IsSpellInRange("Cobra Shot"))
                {
                    WoW.CastSpell("Dire Beast");
                    WoW.CastSpell("Titan's Thunder");
                    return;
                }

                /*/Dire Frenzy
                if (combatRoutine.Type == RotationType.SingleTargetCleave
                    && WoW.CanCast("Dire Frenzy")
                    && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 6
                    && WoW.IsSpellInRange("Cobra Shot"))
                {
                    WoW.CastSpell("Dire Frenzy");
                    WoW.CastSpell("Titan's Thunder");
                    return;
                }

                //Barrage
                if (combatRoutine.Type == RotationType.SingleTargetCleave
                    && WoW.CanCast("Barrage")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && (WoW.Focus >= 60))
                {
                    WoW.CastSpell("Barrage");
                    return;
                }*/

                //Kill Command
                if (combatRoutine.Type == RotationType.SingleTargetCleave
                    && WoW.CanCast("Kill Command")
                    //&& (WoW.SpellCooldownTimeRemaining("Bestial Wrath") - WoW.SpellCooldownTimeRemaining("Kill Command") > 2 || (WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 23 && WoW.SpellCooldownTimeRemaining("Dire Beast") - WoW.SpellCooldownTimeRemaining("Kill Command") > 2))
                    && WoW.Focus >= 30)
                {
                    WoW.CastSpell("Kill Command");
                    return;
                }

                /*/Chimaera Shot
                if (combatRoutine.Type == RotationType.SingleTargetCleave
                    && WoW.CanCast("Chimaera Shot")
                    && WoW.IsSpellInRange("Cobra Shot")
                    && WoW.Focus < 80)
                {
                    WoW.CastSpell("Chimaera Shot");
                    return;
                }*/

                //Cobra Shot
                if (combatRoutine.Type == RotationType.SingleTargetCleave
                    && ((WoW.Focus >= 90) || (WoW.PlayerHasBuff("Bestial Wrath") && (WoW.Focus >= 40)))
                    && WoW.IsSpellInRange("Cobra Shot")
                    && WoW.CanCast("Cobra Shot")
                    && !WoW.CanCast("Bestial Wrath"))
                {
                    WoW.CastSpell("Cobra Shot");
                    return;
                }

                //Mend Pet
                if (WoW.HasPet
                    && WoW.CanCast("Mend Pet")
                    && WoW.PetHealthPercent <= 70
                    && !WoW.PlayerHasBuff("Feign Death"))
                {
                    WoW.CastSpell("Mend Pet");
                    return;
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Sorcerer
AddonName=Quartz
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,136,Mend Pet,D8
Spell,982,Revive Pet,D8
Spell,883,Call Pet,D7
Spell,5512,Healthstone,D1
Spell,127834,Potion,D1
Spell,19577,Intimidation,D9
Spell,109248,Binding Shot,D9
Spell,193455,Cobra Shot,NumPad2
Spell,109304,Exhilaration,NumPad7
Spell,120679,Dire Beast,NumPad1
Spell,217200,Dire Frenzy,NumPad1
Spell,34026,Kill Command,NumPad3
Spell,131894,A Murder of Crows,NumPad4
Spell,120360,Barrage,NumPad4
Spell,194386,Voley,NumPad4
Spell,2643,Multi-Shot,NumPad5
Spell,207068,Titan's Thunder,Add
Spell,19574,Bestial Wrath,NumPad9
Spell,55709,Heart of the Phoenix,D8
Spell,144259,Kil'jaeden's Burning Wish,D2
Spell,193530,Aspect of the Wild,Divide
Spell,53209,Chimaera Shot,D0
Spell,201430,Stampede,NumPad6
Spell,147362,Counter Shot,Decimal
Aura,120694,Dire Beast
Aura,5384,Feign Death
Aura,19574,Bestial Wrath
Aura,127271,Mount
Aura,186265,Aspect of the Turtle
Aura,118455,BeastCleave
Aura,194386,Voley
Item,5512,Healthstone
Item,127834,Potion
Item,144259,Kil'jaeden's Burning Wish
*/
