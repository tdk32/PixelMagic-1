// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

/*
Survival by Vectarius
rev1 - 7.1.5 Rotation

to-do: 
- improve dps?
- add opener
- add all spec variations
- improve Mongoose Fury stacking
*/


using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class HunterSV : CombatRoutine
    {
		public override string Name { get { return "SV Hunter by Vectarius - I am back!"; } }

        public override string Class
        {
            get { return "Hunter"; }
        }

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to SV Hunter by Vectarius", Color.Green);
            Log.Write("Mythic+ optimized - 1/1/2/1/2/1/1", Color.Green);			
        }

        public override void Stop()
        {
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if ((WoW.HasTarget) && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                   /* 
				   if (WoW.CanCast("Raptor Strike") && !WoW.PlayerHasBuff("MOK"))
                    {
                        WoW.CastSpell("Raptor Strike");
			Log.Write("KEIN MOK!", Color.Green);
                        return;
                    }
                    if (WoW.CanCast("Raptor Strike") && WoW.PlayerHasBuff("MOK") && WoW.PlayerBuffStacks("MOK") <= 4 && WoW.PlayerBuffTimeRemaining("MOK") < 2)
                    {
                        WoW.CastSpell("Raptor Strike");
            Log.Write("weniger als 4x MOK! keine zeit", Color.Green);						
                        return;
                    }
					*/		
                    if (WoW.CanCast("Fury of Eagle") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.PlayerBuffStacks("Mongoose Fury") >= 6 && WoW.PlayerBuffTimeRemaining("Mongoose Fury") < 2 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Fury of Eagle");
                        return;
                    }
					
                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.PlayerBuffStacks("Mongoose Fury") >= 6 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }	
                    if (WoW.CanCast("Flanking Strike") && WoW.Focus >= 50 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Flanking Strike");
                        return;
                    }
                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.IsSpellInRange("Mongoose Bite") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }				
				
                    if (WoW.CanCast("Murder of Crows") && WoW.Focus >= 30 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }		
                    if (WoW.CanCast("Caltrops") && WoW.TargetHasDebuff("Caltrops") && WoW.TargetDebuffTimeRemaining("Caltrops") < 2 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Caltrops");
                        return;
                    }	
                    if (WoW.CanCast("Caltrops") && !WoW.TargetHasDebuff("Caltrops") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Caltrops");
                        return;
                    }	
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && WoW.TargetHasDebuff("Lacerate") && WoW.TargetDebuffTimeRemaining("Lacerate") < 2 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }	
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && !WoW.TargetHasDebuff("Lacerate") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }						
                    if (WoW.CanCast("Explosive Trap") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Explosive Trap");
                        return;
                    }
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && !WoW.TargetHasDebuff("Lacerate") && (WoW.Focus >= 35) && WoW.IsSpellInRange("Lacerate") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }

                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerSpellCharges ("Mongoose Bite") >= 3 && WoW.TargetHasDebuff("Lacerate") && WoW.TargetDebuffTimeRemaining("Lacerate") > 8 && !WoW.PlayerHasBuff("Mongoose Fury") && WoW.IsSpellInRange("Mongoose Bite") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if ((WoW.HasTarget) && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                   /* 
				   if (WoW.CanCast("Raptor Strike") && !WoW.PlayerHasBuff("MOK"))
                    {
                        WoW.CastSpell("Raptor Strike");
			Log.Write("KEIN MOK!", Color.Green);
                        return;
                    }
                    if (WoW.CanCast("Raptor Strike") && WoW.PlayerHasBuff("MOK") && WoW.PlayerBuffStacks("MOK") <= 4 && WoW.PlayerBuffTimeRemaining("MOK") < 2)
                    {
                        WoW.CastSpell("Raptor Strike");
            Log.Write("weniger als 4x MOK! keine zeit", Color.Green);						
                        return;
                    }
					*/		
					if (WoW.CanCast("Fury of Eagle") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.PlayerBuffStacks("Mongoose Fury") >= 4 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Fury of Eagle");
                        return;
                    }
					
                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.PlayerBuffStacks("Mongoose Fury") >= 6 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }	
                    if (WoW.CanCast("Flanking Strike") && WoW.Focus >= 50 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Flanking Strike");
                        return;
                    }
                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.IsSpellInRange("Mongoose Bite") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }				
				
                    if (WoW.CanCast("Murder of Crows") && WoW.Focus >= 30 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }		
                    if (WoW.CanCast("Caltrops") && WoW.TargetHasDebuff("Caltrops") && WoW.TargetDebuffTimeRemaining("Caltrops") < 2 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Caltrops");
                        return;
                    }	
                    if (WoW.CanCast("Caltrops") && !WoW.TargetHasDebuff("Caltrops") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Caltrops");
                        return;
                    }	
                    if (WoW.CanCast("Butchery") && WoW.Focus >= 40 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Butchery");
                        return;
                    }						
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && WoW.TargetHasDebuff("Lacerate") && WoW.TargetDebuffTimeRemaining("Lacerate") < 2 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }	
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && !WoW.TargetHasDebuff("Lacerate") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }						
                    if (WoW.CanCast("Explosive Trap") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Explosive Trap");
                        return;
                    }
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && !WoW.TargetHasDebuff("Lacerate") && (WoW.Focus >= 35) && WoW.IsSpellInRange("Lacerate") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }

                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerSpellCharges ("Mongoose Bite") >= 3 && WoW.TargetHasDebuff("Lacerate") && WoW.TargetDebuffTimeRemaining("Lacerate") > 8 && !WoW.PlayerHasBuff("Mongoose Fury") && WoW.IsSpellInRange("Mongoose Bite") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                if ((WoW.HasTarget) && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                   /* 
				   if (WoW.CanCast("Raptor Strike") && !WoW.PlayerHasBuff("MOK"))
                    {
                        WoW.CastSpell("Raptor Strike");
			Log.Write("KEIN MOK!", Color.Green);
                        return;
                    }
                    if (WoW.CanCast("Raptor Strike") && WoW.PlayerHasBuff("MOK") && WoW.PlayerBuffStacks("MOK") <= 4 && WoW.PlayerBuffTimeRemaining("MOK") < 2)
                    {
                        WoW.CastSpell("Raptor Strike");
            Log.Write("weniger als 4x MOK! keine zeit", Color.Green);						
                        return;
                    }
					*/		
					if (WoW.CanCast("Fury of Eagle") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.PlayerBuffStacks("Mongoose Fury") >= 4 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Fury of Eagle");
                        return;
                    }
					
                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.PlayerBuffStacks("Mongoose Fury") >= 6 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }	
                    if (WoW.CanCast("Flanking Strike") && WoW.Focus >= 50 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Flanking Strike");
                        return;
                    }
                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerHasBuff("Mongoose Fury") && WoW.IsSpellInRange("Mongoose Bite") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }				
				
                    if (WoW.CanCast("Murder of Crows") && WoW.Focus >= 30 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }		
                    if (WoW.CanCast("Caltrops") && WoW.TargetHasDebuff("Caltrops") && WoW.TargetDebuffTimeRemaining("Caltrops") < 2 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Caltrops");
                        return;
                    }	
                    if (WoW.CanCast("Caltrops") && !WoW.TargetHasDebuff("Caltrops") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Caltrops");
                        return;
                    }							
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && WoW.TargetHasDebuff("Lacerate") && WoW.TargetDebuffTimeRemaining("Lacerate") < 2 && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }	
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && !WoW.TargetHasDebuff("Lacerate") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }						
                    if (WoW.CanCast("Explosive Trap") && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Explosive Trap");
                        return;
                    }
                    if (WoW.CanCast("Lacerate") && WoW.Focus >= 35 && !WoW.TargetHasDebuff("Lacerate") && (WoW.Focus >= 35) && WoW.IsSpellInRange("Lacerate") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Lacerate");
                        return;
                    }

                    if (WoW.CanCast("Mongoose Bite") && WoW.PlayerSpellCharges ("Mongoose Bite") >= 3 && WoW.TargetHasDebuff("Lacerate") && WoW.TargetDebuffTimeRemaining("Lacerate") > 8 && !WoW.PlayerHasBuff("Mongoose Fury") && WoW.IsSpellInRange("Mongoose Bite") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsSpellInRange("Raptor Strike") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Mongoose Bite");
                        return;
                    }
				}	
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
Spell,190928,Mongoose Bite,D1
Spell,202800,Flanking Strike,D2
Spell,185855,Lacerate,D3
Spell,186270,Raptor Strike,D4
Spell,194277,Caltrops,D5
Spell,191433,Explosive Trap,D6
Spell,194855,Dragonsfire Grenade,D7
Spell,200163,Throwing Axes,D8
Spell,203415,Fury of Eagle,D9
Spell,206505,Murder of Crows,D0
Spell,212436,Butchery,C
Aura,190931,Mongoose Fury
Aura,185855,Lacerate
Aura,186289,Aspect of the Eagle
Aura,194277,Caltrops
Aura,201082,MOK
*/