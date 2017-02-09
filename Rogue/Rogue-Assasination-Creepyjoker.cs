// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class Assassination : CombatRoutine
    {
        public override string Name
        {
            get { return "Rogue-Assassination"; }
        }

        public override string Class
        {
            get { return "Rogue"; }
        }

        public override Form SettingsForm { get; set; }


        public override void Initialize()
        {
            Log.Write("Welcome to Rogue Assasination by Creepyjoker.", Color.Green);
			Log.Write("The following rotation is still in beta version.", Color.Red);
            Log.Write("Suggested build: Master Poisoner - Vigor - AP");
        }

        public override void Stop()
        {
        }


        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
				
				if ( WoW.HasTarget && WoW.IsSpellInRange("Rupture"))
					
				
				{
					if ( WoW.HasBossTarget &&
                        WoW.CanCast("Kingsbane") &&
                        !WoW.IsSpellOnCooldown("Kingsbane") &&
                            (
                            WoW.SpellCooldownTimeRemaining("Vendetta") >= 10 ||
                            WoW.TargetHasDebuff("Vendetta")
                            ))
                         {
                        WoW.CastSpell("Kingsbane");
                        return;
                    }
					if (WoW.CanCast("Garrote") && WoW.Energy >= 45 && !WoW.TargetHasDebuff("Garrote") && !WoW.IsSpellOnCooldown("Garrote") && WoW.CurrentComboPoints <= 4)
					{
						WoW.CastSpell("Garrote");
						return;
					}
					if (WoW.CanCast("Fan Of Knives") && WoW.Energy >= 35 && WoW.PlayerHasBuff("FoK") && WoW.PlayerBuffStacks("FoK") == 30 && WoW.CurrentComboPoints <= 4)
					{
						WoW.CastSpell("Fan Of Knives");
						return;	
					}
					if (WoW.CanCast("Garrote") && WoW.Energy >= 45 && WoW.TargetHasDebuff("Garrote") && WoW.TargetDebuffTimeRemaining("Garrote") <= 4 && WoW.CurrentComboPoints <= 4)
					{
						WoW.CastSpell("Garrote");
						return;
					}
					if (WoW.CurrentComboPoints == 4 && WoW.Energy >= 25 && WoW.CanCast("Rupture") && !WoW.TargetHasDebuff("Rupture"))
					{
						WoW.CastSpell("Rupture");
						return;
					}
					if (WoW.CurrentComboPoints == 4 && WoW.Energy >= 25 && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") <= 6)
					{
						WoW.CastSpell("Rupture");
						return;
					}
					if (WoW.CurrentComboPoints == 5 && WoW.Energy >= 25 && WoW.CanCast("Rupture") && !WoW.TargetHasDebuff("Rupture"))
					{
						WoW.CastSpell("Rupture");
						return;
					}
					if (WoW.CurrentComboPoints == 5 && WoW.Energy >= 25 && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") <= 6)
					{
						WoW.CastSpell("Rupture");
						return;
					}
					if (WoW.Energy >= 35 && WoW.CurrentComboPoints == 4 && WoW.CanCast("Envenom") && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") > 6)
					{
						WoW.CastSpell("Envenom");
						return;
					}
					if (WoW.Energy >= 35 && WoW.CurrentComboPoints == 5 && WoW.CanCast("Envenom") && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") > 6)
					{
						WoW.CastSpell("Envenom");
						return;
					}
					if (WoW.CanCast("Mutilate") && WoW.Energy >= 55 && WoW.CurrentComboPoints <= 3 )
					{
						WoW.CastSpell("Mutilate");
						return;
					}
					
					if (WoW.HasBossTarget && !WoW.IsSpellOnCooldown("Kingsbane") && WoW.CanCast("Kingsbane") && WoW.CurrentComboPoints <= 4 && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") <= 10 && WoW.Energy >= 35)
					{
						WoW.CastSpell("Kingsbane");
						return;
					}
					if (WoW.HasBossTarget && !WoW.IsSpellOnCooldown("Kingsbane") && WoW.CanCast("Kingsbane") && WoW.CurrentComboPoints <= 4 && WoW.TargetHasDebuff("Garrote") && WoW.TargetDebuffTimeRemaining("Garrote") <= 10 && WoW.Energy >= 35)
					{
						WoW.CastSpell("Kingsbane");
						return;
					}
					if (WoW.HasBossTarget && !WoW.IsSpellOnCooldown("Kingsbane") && WoW.CanCast("Kingsbane") && WoW.CurrentComboPoints <= 4 && !WoW.TargetHasDebuff("Rupture") && WoW.Energy >= 35 )
					{
						WoW.CastSpell("Kingsbane");
						return;
					}
                         
 					if (
                       WoW.HasBossTarget && WoW.CanCast("Vendetta") &&
                        !WoW.IsSpellOnCooldown("Vendetta") &&
                        WoW.Energy <= 50
                        ) {
                        WoW.CastSpell("Vendetta");
                        return;
                    }
					/*if (WoW.HasBossTarget && WoW.CanCast("Vanish") && !WoW.IsSpellOnCooldown("Vanish") && WoW.CurrentComboPoints == 5 && WoW.Energy >= 35 && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") <= 6)
					{
						WoW.CastSpell("Vanish");
						return;
					}*/
						
					
					
					
					
				}
                
                }
            

            if (combatRoutine.Type == RotationType.AOE || combatRoutine.Type == RotationType.SingleTargetCleave) // Do AoE Target Stuff here
            {
                if ((WoW.HasTarget || WoW.IsBoss) && WoW.TargetIsEnemy && WoW.IsInCombat) {
                   {
					
					if (WoW.CanCast("Garrote") && WoW.Energy >= 45 && !WoW.TargetHasDebuff("Garrote") && !WoW.IsSpellOnCooldown("Garrote") && WoW.CurrentComboPoints <= 4)
					{
						WoW.CastSpell("Garrote");
						return;
					}
					if (WoW.CanCast("Garrote") && WoW.Energy >= 45 && WoW.TargetHasDebuff("Garrote") && WoW.TargetDebuffTimeRemaining("Garrote") <= 3 && WoW.CurrentComboPoints <= 4)
					{
						WoW.CastSpell("Garrote");
						return;
					}
					if (WoW.CurrentComboPoints == 4 && WoW.Energy >= 25 && WoW.CanCast("Rupture") && !WoW.TargetHasDebuff("Rupture"))
					{
						WoW.CastSpell("Rupture");
						return;
					}
					if (WoW.CurrentComboPoints == 4 && WoW.Energy >= 25 && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") <= 6)
					{
						WoW.CastSpell("Rupture");
						return;
					}
					if (WoW.CurrentComboPoints == 5 && WoW.Energy >= 25 && WoW.CanCast("Rupture") && !WoW.TargetHasDebuff("Rupture"))
					{
						WoW.CastSpell("Rupture");
						return;
					}
					if (WoW.CurrentComboPoints == 5 && WoW.Energy >= 25 && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") <= 6)
					{
						WoW.CastSpell("Rupture");
						return;
					}
					if (WoW.Energy >= 35 && WoW.CurrentComboPoints == 4 && WoW.CanCast("Envenom") && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") > 6)
					{
						WoW.CastSpell("Envenom");
						return;
					}
					if (WoW.Energy >= 35 && WoW.CurrentComboPoints == 5 && WoW.CanCast("Envenom") && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") > 6)
					{
						WoW.CastSpell("Envenom");
						return;
					}
					if (WoW.CanCast("Fan Of Knives") && WoW.Energy >= 35 && WoW.CurrentComboPoints <= 3 )
					{
						WoW.CastSpell("Fan Of Knives");
						return;
					}
					if ( WoW.HasBossTarget &&
                        WoW.CanCast("Kingsbane") &&
                        !WoW.IsSpellOnCooldown("Kingsbane") &&
                            (
                            WoW.SpellCooldownTimeRemaining("Vendetta") >= 10 ||
                            WoW.TargetHasDebuff("Vendetta")
                            ))
                         {
                        WoW.CastSpell("Kingsbane");
                        return;
                    }
					if (WoW.HasBossTarget && !WoW.IsSpellOnCooldown("Kingsbane") && WoW.CanCast("Kingsbane") && WoW.CurrentComboPoints <= 4 && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") <= 10 && WoW.Energy >= 35)
					{
						WoW.CastSpell("Kingsbane");
						return;
					}
					if (WoW.HasBossTarget && !WoW.IsSpellOnCooldown("Kingsbane") && WoW.CanCast("Kingsbane") && WoW.CurrentComboPoints <= 4 && WoW.TargetHasDebuff("Garrote") && WoW.TargetDebuffTimeRemaining("Garrote") <= 10 && WoW.Energy >= 35)
					{
						WoW.CastSpell("Kingsbane");
						return;
					}
					if (WoW.HasBossTarget && !WoW.IsSpellOnCooldown("Kingsbane") && WoW.CanCast("Kingsbane") && WoW.CurrentComboPoints <= 4 && !WoW.TargetHasDebuff("Rupture") && WoW.Energy >= 35 )
					{
						WoW.CastSpell("Kingsbane");
						return;
					}
                         
 					if (
                       WoW.HasBossTarget && WoW.CanCast("Vendetta") &&
                        !WoW.IsSpellOnCooldown("Vendetta") &&
                        WoW.Energy <= 50
                        ) {
                        WoW.CastSpell("Vendetta");
                        return;
                    }
					if (WoW.HasBossTarget && WoW.CanCast("Vanish") && !WoW.IsSpellOnCooldown("Vanish") && WoW.CurrentComboPoints == 5 && WoW.Energy >= 35 && WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") <= 6 && WoW.TargetDebuffTimeRemaining("Garrote") >= 6)
					{
						WoW.CastSpell("Vanish");
						return;
					}
            }
        }
    }
}
	}
}
/*
[AddonDetails.db]
AddonAuthor=Vectarius, Dubsys
AddonName=Sucstobeyou
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,1943,Rupture,D2
Spell,79140,Vendetta,D5
Spell,1856,Vanish,V
Spell,1329,Mutilate,D1
Spell,703,Garrote,R
Spell,192759,Kingsbane,D3
Spell,32645,Envenom,Q
Spell,51723,Fan Of Knives,X
Aura,1943,Rupture
Aura,1784,Stealth
Aura,703,Garrote
Aura,208693,FoK
Aura,32645,Envenom
Aura,200802,Agonizing Poison
Aura,193641,Elaborate Planning
Aura,79140,Vendetta
*/
