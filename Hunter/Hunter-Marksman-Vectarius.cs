// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class HunterMarksman : CombatRoutine
    {
        public override string Name => "Hunter MM";

        public override string Class => "Hunter";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Hunter MM", Color.Green);
            Log.Write("Welcome to PixelMagic Marksman");
        }

        public override void Stop()
        {
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.CanCast("Barrage") && (WoW.Focus >= 60) && WoW.IsSpellInRange("Barrage"))
                    {
                        WoW.CastSpell("Barrage");
                        return;
                    }
                    if ((WoW.Focus >= 30) && WoW.CanCast("Marked Shot") && WoW.TargetHasDebuff("Hunters Mark") && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Windburst") &&
                        WoW.TargetHasDebuff("Vulnerable"))
                    {
                        WoW.CastSpell("Marked Shot");
                        return;
                    }
                    if (WoW.CanCast("Windburst") && (WoW.Focus >= 20) && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }
                    if (WoW.TargetHasDebuff("Vulnerable") && (WoW.Focus >= 50) && WoW.CanCast("AS") && WoW.IsSpellInRange("AS") && (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 2.1) &&
                        !WoW.PlayerIsChanneling && !WoW.IsMoving)
                    {
                        WoW.CastSpell("AS");
                        return;
                    }
                    if (WoW.TargetHasDebuff("Vulnerable") && WoW.Focus >= 50 && WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") && (WoW.TargetDebuffTimeRemaining("Vulnerable") >= 2.1) &&
                        !WoW.PlayerIsChanneling && WoW.PlayerHasBuff("Lock and Load"))
                    {
                        WoW.CastSpell("AS");
                        return;
                    }
                    if (WoW.CanCast("Sidewinders") && (WoW.PlayerSpellCharges("Sidewinders") >= 1) && (WoW.PlayerHasBuff("Marking Targets") || WoW.PlayerHasBuff("Trueshot")) &&
                        WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Sidewinders");
                        return;
                    }
                    if (WoW.CanCast("AS") && (WoW.Focus >= 80) && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling && !WoW.IsMoving)
                    {
                        WoW.CastSpell("AS");
                        return;
                    }
                    if (WoW.CanCast("AS") && (WoW.Focus >= 80) && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling && WoW.PlayerHasBuff("Lock and Load"))
                    {
                        WoW.CastSpell("AS");
                        return;
                    }
                    if (WoW.CanCast("Sidewinders") && (WoW.PlayerSpellCharges("Sidewinders") >= 2) && WoW.IsSpellInRange("Windburst") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Sidewinders");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE stuff here
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
WoWVersion=Legion-70100
[SpellBook.db]
Spell,204147,Windburst,D2
Spell,120360,Barrage,D3
Spell,19434,AS,D4
Spell,214579,Sidewinders,D5
Spell,185901,Marked Shot,D6
Aura,223138,Marking Targets
Aura,185987,Hunters Mark
Aura,194594,Lock and Load
Aura,187131,Vulnerable
Aura,193526,Trueshot
*/