// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class HunterBeastmastery : CombatRoutine
    {
        public override string Name => "Hunter BM";

        public override string Class => "Hunter";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Hunter BM", Color.Green);
            WoW.Speak("Welcome to PixelMagic Beastmastery");
        }

        public override void Stop()
        {
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    if (WoW.CanCast("A Murder of Crows") && WoW.IsSpellInRange("A Murder of Crows") && WoW.Focus >= 30)
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
                    if (WoW.CanCast("Titan's Thunder"))
                    {
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }
                    if (WoW.CanCast("Dire Beast") && WoW.IsSpellInRange("Dire Beast"))
                    {
                        WoW.CastSpell("Dire Beast");
                        return;
                    }
                    if (WoW.CanCast("Dire Frenzy") && WoW.IsSpellInRange("Dire Frenzy"))
                    {
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }
                    if (WoW.CanCast("Kill Command") && WoW.Focus >= 30 && WoW.IsSpellInRange("Kill Command"))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
                    if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 90) && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    if (WoW.CanCast("A Murder of Crows") && WoW.IsSpellInRange("A Murder of Crows") && WoW.Focus >= 30)
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
                    if (WoW.CanCast("Titan's Thunder"))
                    {
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }
                    if (WoW.CanCast("Dire Beast") && WoW.IsSpellInRange("Dire Beast"))
                    {
                        WoW.CastSpell("Dire Beast");
                        return;
                    }
                    if (WoW.CanCast("Dire Frenzy") && WoW.IsSpellInRange("Dire Frenzy"))
                    {
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }
                    if (WoW.CanCast("Kill Command") && WoW.Focus >= 30 && WoW.IsSpellInRange("Kill Command"))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
                    if (WoW.CanCast("Multi-Shot") && WoW.IsSpellInRange("Multi-Shot") && WoW.Focus >= 40) // We need a PetBuff command!
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }
                    if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 90) && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                // Do Single Target Cleave stuff here if applicable else ignore this one
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Vectarius
AddonName=PixelMagic
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,193455,Cobra Shot,D4
Spell,120679,Dire Beast,D6
Spell,217200,Dire Frenzy,D6
Spell,34026,Kill Command,D2
Spell,131894,A Murder of Crows,D3
Spell,2643,Multi-Shot,D5
Spell,207068,Titan's Thunder,D7
*/