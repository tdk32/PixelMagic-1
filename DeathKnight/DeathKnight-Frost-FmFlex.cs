// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class FrostFMFlex : CombatRoutine
    {
        public override string Name => "Frost DK";

        public override string Class => "Deathknight";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            WoW.Speak("Welcome to F m Flex Frost");
            Log.Write("Welcome to the Frost DK Machine Gun Rotation by Fmflex", Color.Green);
        }

        public override void Stop()
        {
        }

        private static bool CanCastInRange(string spell)
        {
            return WoW.CanCast(spell, true, true, true);
        }

        private static bool IsMelee()
        {
            return WoW.CanCast("Frost Strike", false, false, true, false, false);
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (CanCastInRange("Frost Strike") && IsMelee() && (!WoW.PlayerHasBuff("Icy Talons") || WoW.PlayerBuffTimeRemaining("Icy Talons") <= 2) && WoW.RunicPower >= 25)
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                    if (WoW.CanCast("Howling Blast", true, false, true) && !WoW.IsSpellOnCooldown("Howling Blast") && !WoW.TargetHasDebuff("Frost Fever") && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Howling Blast");
                        return;
                    }
                    if (WoW.CanCast("Frost Strike") && IsMelee() && WoW.RunicPower >= 80)
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                    if (WoW.CanCast("Howling Blast") && WoW.PlayerHasBuff("Rime") && WoW.CurrentRunes <= 1)
                    {
                        WoW.CastSpell("Howling Blast");
                        return;
                    }
                    if (WoW.CanCast("Frostscythe") && IsMelee() && WoW.TargetHasDebuff("Killing Machine") && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Frostscythe");
                        return;
                    }
                    if (WoW.CanCast("Obliterate") && IsMelee() && WoW.CurrentRunes >= 2)
                    {
                        WoW.CastSpell("Obliterate");
                        return;
                    }
                    if (WoW.CanCast("Glacial Advance") && IsMelee() && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Glacial Advance");
                        return;
                    }
                    if (WoW.CanCast("Remorseless Winter") && IsMelee() && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Remorseless Winter");
                        return;
                    }
                    if (WoW.CanCast("Frostscythe") && IsMelee() && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Frostscythe");
                        return;
                    }

                    if (WoW.CanCast("Frost Strike") && IsMelee() && WoW.PlayerBuffStacks("Icy Talons") < 3 && WoW.RunicPower >= 25)
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (CanCastInRange("Frost Strike") && IsMelee() && (!WoW.PlayerHasBuff("Icy Talons") || WoW.PlayerBuffTimeRemaining("Icy Talons") <= 2) && WoW.RunicPower >= 25)
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                    if (WoW.CanCast("Howling Blast", true, false, true) && !WoW.IsSpellOnCooldown("Howling Blast") && !WoW.TargetHasDebuff("Frost Fever") && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Howling Blast");
                        return;
                    }
                    if (WoW.CanCast("Frost Strike") && IsMelee() && WoW.RunicPower >= 80)
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                    if (WoW.CanCast("Frostscythe") && IsMelee() && WoW.TargetHasDebuff("Killing Machine") && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Frostscythe");
                        return;
                    }
                    if (WoW.CanCast("Glacial Advance") && IsMelee() && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Glacial Advance");
                        return;
                    }
                    if (WoW.CanCast("Remorseless Winter") && IsMelee() && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Remorseless Winter");
                        return;
                    }
                    if (WoW.CanCast("Frostscythe") && IsMelee() && WoW.CurrentRunes >= 1)
                    {
                        WoW.CastSpell("Frostscythe");
                        return;
                    }

                    if (WoW.CanCast("Frost Strike") && IsMelee() && WoW.PlayerBuffStacks("Icy Talons") < 3 && WoW.RunicPower >= 25)
                    {
                        WoW.CastSpell("Frost Strike");
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
AddonAuthor=FmFlex
AddonName=PixelMagic
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,49143,Frost Strike,D1
Spell,207230,Frostscythe,D2
Spell,49184,Howling Blast,D3
Spell,49020,Obliterate,D4
Spell,196770,Remorseless Winter,D5
Spell,194913,Glacial Advance,D6
Aura,94879,Icy Talons
Aura,55095,Frost Fever
Aura,59057,Rime
Aura,51128,Killing Machine
Aura,207256,Obliteration
*/