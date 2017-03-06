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
    public class WarlockDemonology : CombatRoutine
    {  
        public override string Name
        {
            get { return "Demonology Warlock"; }
        }

        public override string Class
        {
            get { return "Warlock"; }
        }

        public override void Initialize()
        {
            Log.Write("Welcome to the Demonology Warlock rotation", Color.Purple);            
            Log.Write("Use Scroll Lock key to toggle ST/AOE/CLEAVE auto detection", Color.Blue);
            Log.Write("If Scroll Lock LED is ON ST/AOE/CLEAVE auto detection is ENABLED", Color.Blue);
            Log.Write("If Scroll Lock LED is OFF ST/AOE/CLEAVE auto detection is DISABLED use the manual mode to select ST/AOE/CLEAVE (Default: ALT+S, ALT+A)", Color.Blue);
        }

        public override void Stop()
        {
        }				
		
        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            AddonCreationPulse();
            PlayerStats();
            AoEStuff();
            if (WoW.IsInCombat && !WoW.PlayerHasBuff("Mount"))
            {
                SelectRotation();
            }

            //Dark Pact
            if (WoW.CanCast("Dark Pact")
                && CharInfo.T5 == 3
                && WoW.HealthPercent <= 30
                && !WoW.PlayerHasBuff("Mount"))
            {
                WoW.CastSpell("Dark Pact");
                return;
            }           

            //Shadowfury
            if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_LMENU) < 0
                && CharInfo.T3 == 3
                && !WoW.IsMoving
                && WoW.CanCast("Shadowfury"))
            {
                WoW.CastSpell("Shadowfury");
                return;
            }

            if (UseCooldowns)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsChanneling && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Mount"))
                { 
                    //Doomguard
                    if (WoW.CanCast("Doomguard")
                        && (CharInfo.T6 == 0 || CharInfo.T6 == 2 || CharInfo.T6 == 3)
                        && WoW.CurrentSoulShards >= 1
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Doomguard");
                        return;
                    }

                    //Grimoire of Service
                    if (WoW.CanCast("Grimoire: Felguard")
                        && CharInfo.T6 == 2
                        && WoW.CurrentSoulShards >= 1
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Grimoire: Felguard");
                        return;
                    }

                    //Soul Harvest
                    if (WoW.CanCast("Soul Harvest")
                        && CharInfo.T4 == 3
                        && !WoW.IsMoving
                        && WoW.IsSpellInRange("Doom")
                        && (WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Time Warp") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums of War") || WoW.PlayerHasBuff("Heroism")))
                    {
                        WoW.CastSpell("Soul Harvest");
                        return;
                    }
                }
            }

            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsChanneling && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Mount"))
                {
                    if ((!WoW.TargetHasDebuff("Doom") || WoW.TargetDebuffTimeRemaining("Doom") <= 1.5)                        
                        && WoW.CanCast("Doom")
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Doom");
                        return;
                    }

                    if (WoW.CanCast("Darkglare")
                        && CharInfo.T7 == 1
                        && WoW.CurrentSoulShards >= 1
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Darkglare");
                        return;
                    }

                    if (WoW.CanCast("Call Dreadstalkers")
                        && (WoW.CurrentSoulShards >= 2 || WoW.TargetHasDebuff("Demonic Calling"))
                        && WoW.IsSpellInRange("Doom")
                        && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Call Dreadstalkers");
                        return;
                    }
                   
                    if (WoW.CanCast("Hand of Guldan")
                        && WoW.CurrentSoulShards >= 4
                        && WoW.IsSpellInRange("Doom")
                        && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Hand of Guldan");
                        return;
                    }

                    if (WoW.CanCast("Demonic Empowerment")
                        && !WoW.IsMoving
                        && !WoW.WasLastCasted("Demonic Empowerment")
                        && (!WoW.PetHasBuff("Demonic Empowerment") || WoW.PetBuffTimeRemaining("Demonic Empowerment") <= 1.5
                        || WoW.WasLastCasted("Call Dreadstalkers") || WoW.WasLastCasted("Grimoire: Felguard") || WoW.WasLastCasted("Doomguard") || WoW.WasLastCasted("Hand of Guldan")))
                    {
                        WoW.CastSpell("Demonic Empowerment");
                        Thread.Sleep(1000);
                        return;
                    }

                    if (WoW.CanCast("Talkiels Consumption")
                        && WoW.PetHasBuff("Demonic Empowerment")
                        && WoW.PetBuffTimeRemaining("Demonic Empowerment") >= 2
                        && WoW.DreadstalkersCount >= 1
                        && WoW.IsSpellInRange("Doom")
                        && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Talkiels Consumption");
                        return;
                    }

                    if (WoW.CanCast("Felstorm")
                        && WoW.PetHasBuff("Demonic Empowerment") 
                        && WoW.PetBuffTimeRemaining("Demonic Empowerment") >= 6
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Felstorm");
                        return;
                    }

                    if (WoW.CanCast("Shadowflame")
                        && CharInfo.T1 == 2
                        && !WoW.TargetHasDebuff("Shadowflame")
                        && WoW.CanCast("Shadowflame")
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    if (WoW.CanCast("Life Tap")
                        && WoW.Mana < 60
                        && WoW.HealthPercent > 50)
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.CanCast("Demonwrath")
                        && WoW.Mana > 60
                        && WoW.IsMoving)
                    {
                        WoW.CastSpell("Demonwrath");
                        return;
                    }

                    if ((WoW.CanCast("Shadow Bolt") || WoW.CanCast("Demonbolt"))                        
                        && WoW.IsSpellInRange("Doom")
                        && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Shadow Bolt");
                        WoW.CastSpell("Demonbolt");
                        return;
                    }                  
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsChanneling && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Mount"))
                {
                    if (WoW.CanCast("Hand of Guldan")
                        && WoW.CurrentSoulShards >= 4
                        && WoW.IsSpellInRange("Doom")
                        && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Hand of Guldan");
                        return;
                    }

                    if (WoW.CanCast("Implosion")
                        && CharInfo.T2 == 3
                        && WoW.WildImpsCount >= 1
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Implosion");
                        return;
                    }

                    if (WoW.CanCast("Darkglare")
                        && CharInfo.T7 == 1
                        && WoW.CurrentSoulShards >= 1
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Darkglare");
                        return;
                    }

                    if (WoW.CanCast("Demonic Empowerment")
                       && WoW.CanCast("Felstorm")
                       && !WoW.IsMoving
                       && !WoW.WasLastCasted("Demonic Empowerment")
                       && (!WoW.PetHasBuff("Demonic Empowerment") || WoW.PetBuffTimeRemaining("Demonic Empowerment") <= 6))
                    {
                        WoW.CastSpell("Demonic Empowerment");
                        Thread.Sleep(2000);
                        return;
                    }

                    if (WoW.CanCast("Felstorm")
                        && WoW.PetHasBuff("Demonic Empowerment")
                        && WoW.PetBuffTimeRemaining("Demonic Empowerment") >= 6
                        && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Felstorm");
                        return;
                    }                    

                    if (WoW.CanCast("Life Tap")
                        && WoW.Mana < 60
                        && WoW.HealthPercent > 50)
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.CanCast("Demonwrath")
                        && WoW.Mana > 60)
                    {
                        WoW.CastSpell("Demonwrath");
                        return;
                    }

                }
            }

            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                // Do Single Target Cleave stuff here if applicable else ignore this one
            }            
        }

        #region Talents functions
        public struct char_data
        {
            public int T1;
            public int T2;
            public int T3;
            public int T4;
            public int T5;
            public int T6;
            public int T7;
            public float Mana;
            public string Spec;
            public string Race;

            private char_data(int p1, int p2, int p3, int p4, int p5, int p6, int p7, float mana, string spec, string race)
            {
                T1 = p1;
                T2 = p2;
                T3 = p3;
                T4 = p4;
                T5 = p5;
                T6 = p6;
                T7 = p7;
                Mana = mana;
                Spec = spec;
                Race = race;
            }
        }

        public string[] Race = new string[] { "None", "Human", "Dwarf", "NightElf", "Gnome", "Dreanei", "Pandaren", "Orc", "Undead", "Tauren", "Troll", "BloodElf", "Goblin", "Worgen", "none" };
        public string[] Spec = new string[] { "None", "Blood", "Frost", "Unholy", "Havoc", "Vengeance", "Balance", "Feral", "Guardian", "Restoration", "Beast Mastery", "Marksmanship", "Survival", "Arcane", "Fire", "Frost", "Brewmaster", "Mistweaver", "Windwalker", "Holy", "Protection", "Retribution", "Discipline", "HolyPriest", "Shadow", "Assassination", "Outlaw", "Subtlety", "Elemental", "Enhancement", "RestorationShaman", "Affliction", "Arms", "Fury", "Protection", "none" };
        private int npcCount, players;
        private bool Nameplates = false;
        private Color pixelColor = Color.FromArgb(0);
        private double hastePct;
        private char_data CharInfo = new char_data();
        private bool AddonEmbeded = false;
        private bool RangeLib = false;

        private void PlayerStats()
        {
            // Playerstats start at row 1,  column 21
            // t1 t2 t3
            // t4 t5 t7
            // t7 +-haste hastePCT
            // Spec, Mana, Race
            int postive = 0;
            if ((Convert.ToDouble(pixelColor.R) == 255))
                hastePct = 0f;
            else
                hastePct = (Convert.ToSingle(pixelColor.R) * 100f / 255f);
            int spec, race;
            pixelColor = WoW.GetBlockColor(1, 21);
            CharInfo.T1 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) * 100 / 255));
            CharInfo.T2 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
            CharInfo.T3 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B) * 100 / 255));
            pixelColor = WoW.GetBlockColor(2, 21);
            CharInfo.T4 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) * 100 / 255));
            CharInfo.T5 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
            CharInfo.T6 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B) * 100 / 255));
            pixelColor = WoW.GetBlockColor(3, 21);
            CharInfo.T7 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) * 100 / 255));
            spec = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
            race = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B) * 100 / 255));
            pixelColor = WoW.GetBlockColor(4, 21);
            CharInfo.Mana = (Convert.ToSingle(pixelColor.B) * 100 / 255);
            postive = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) / 255));
            if ((Convert.ToDouble(pixelColor.B) == 255))
                hastePct = 0f;
            else
                if (postive == 1)
                    hastePct = (Convert.ToSingle(pixelColor.R) * 100f / 255f) * (-1);
                else
                    hastePct = (Convert.ToSingle(pixelColor.G) * 100f / 255f);
            if (race > 13)
                race = 0;
            if (spec > 34)
                spec = 0;
            //Log.Write ("Char Race :" + race + " Spec : " + spec);
            CharInfo.Race = Race[race];
            CharInfo.Spec = Spec[spec];
            //Log.Write(" T1 " + CharInfo.T1 + " T2 " + CharInfo.T2 + " T3 " + CharInfo.T3 + " T4 " + CharInfo.T4 + " T5 " + CharInfo.T5 + " T6 " + CharInfo.T6 + " T7 " + CharInfo.T7);
            //Log.Write("Char Haste " + hastePct + " Mana :" + CharInfo.Mana + " Race : " +CharInfo.Race + " Spec : "  +CharInfo.Spec ) ;

        }

        private void AoEStuff()
        {
            Color pixelColor = Color.FromArgb(0);
            pixelColor = WoW.GetBlockColor(11, 20);
            npcCount = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
            if (Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B) / 255)) == 1)
                Nameplates = true;
            else
                Nameplates = false;
        }

        private void SelectRotation()
        {
            if (Nameplates)
            {
                if (Control.IsKeyLocked(Keys.Scroll))
                {
                    if (npcCount >= 3 && !WoW.TargetIsPlayer)
                        combatRoutine.ChangeType(RotationType.AOE);
                    /* if ((npcCount == 2 || npcCount == 3) && !WoW.TargetIsPlayer)
                         combatRoutine.ChangeType(RotationType.SingleTargetCleave);
                     if ((npcCount <= 2))*/
                    combatRoutine.ChangeType(RotationType.SingleTarget);
                }
            }
        }


        private static string AddonName = ConfigFile.ReadValue("PixelMagic", "AddonName");
        private static string AddonEmbedName = "BossLib.xml";// Initialization variables		
        private bool AddonEdited = false;
        public void RangeLibCopy()
        {
            try
            {
                string targetPath = string.Concat("" + WoW.AddonPath + "\\" + AddonName + "\\lib\\LibSpellRange-1.0\\");
                string targetPathSub = string.Concat("" + WoW.AddonPath + "\\" + AddonName + "\\lib\\LibSpellRange-1.0\\lib\\LibStub\\");

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                    Log.Write("Base target:" + targetPath);
                }
                if (!Directory.Exists(targetPathSub))
                {
                    Log.Write("Sub target:" + targetPathSub);
                    Directory.CreateDirectory(targetPathSub);
                }

                if (Directory.Exists(targetPath))
                {
                    if (!File.Exists(Path.Combine(targetPath, LibSpellToc)))
                    {

                        File.WriteAllText(Path.Combine(targetPath, LibSpellToc), LibSpellTocContent);
                    }
                    if (!File.Exists(Path.Combine(targetPath, LibSpellLua)))
                    {
                        File.WriteAllText(Path.Combine(targetPath, LibSpellLua), LibSpellLuaContent);
                    }
                    if (!File.Exists(Path.Combine(targetPath, LibXml)))
                    {
                        File.WriteAllText(Path.Combine(targetPath, LibXml), LibXmlContent);
                    }
                }

                if (Directory.Exists(targetPathSub))
                {
                    if (!File.Exists(Path.Combine(targetPathSub, LibStubLua)))
                    {
                        File.WriteAllText(Path.Combine(targetPathSub, LibStubLua), LibStubLuaContent);
                    }
                    if (!File.Exists(Path.Combine(targetPathSub, LibStubToc)))
                    {
                        File.WriteAllText(Path.Combine(targetPathSub, LibStubToc), LibStubTocContent);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Thread.Sleep(2000);
            RangeLib = true;

        }

        private void AddonEmbedEdit()
        {
            try
            {

                Log.Write("Addon emedding Editing in progress");
                if (!File.Exists(Path.Combine("" + WoW.AddonPath + "\\" + AddonName + "\\", AddonEmbedName)))
                {
                    string addonlua = " < Ui xmlns = \"http://www.blizzard.com/wow/ui/\" xmlns: xsi = \"http://www.w3.org/2001/XMLSchema-instance \" xsi: schemaLocation = \"http://www.blizzard.com/wow/ui/ ..\\FrameXML\\UI.xsd\" >" + Environment.NewLine
                + "< Script file = \"lib\\LibSpellRange-1.0\\LibSpellRange-1.0.lua\" />" + Environment.NewLine
                + "</ Ui >" + Environment.NewLine;
                    File.WriteAllText("" + WoW.AddonPath + "\\" + AddonName + "\\" + AddonEmbedName, addonlua);
                    Log.Write("Addon Embedding complete");

                }
                AddonEmbeded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void AddonCreationPulse()
        {

            // Editing the addon
            if (AddonEdited == false)
            {
                Log.Write("Editing Addon");
                AddonEdit();
                Log.Write("Editing Addon Complete");
                Thread.Sleep(2000);

            }
            if (AddonEmbeded == false)
            {
                Log.Write("embedingin rangecheck");
                AddonEmbedEdit();
                Log.Write("embedingin RangeCheck Complete");
                Thread.Sleep(2000);
            }
            if (RangeLib == false)
            {
                RangeLibCopy();
                WoW.Reload();
            }
            Thread.Sleep(350);
        }
        private void AddonEdit()
        {
            try
            {
                string addonlua = File.ReadAllText("" + WoW.AddonPath + "\\" + AddonName + "\\" + AddonName + ".lua");
                Log.Write("Addon Editing in progress");
                addonlua = addonlua.Replace("if name == \"Wild Imps\"", "if (name == \"Wild Imps\" or name == \"Spirit Wolf\" or name == \"Totem Mastery\")");
                addonlua = addonlua.Replace("and(startTime + duration - GetTime() > 1.6) ", "and(startTime + duration - GetTime() > (1.5 / (1 + (GetHaste() / 100) ))) ");
                addonlua = addonlua.Replace("end" + Environment.NewLine + Environment.NewLine + "local function InitializeTwo()", Environment.NewLine);
                addonlua = addonlua.Replace("	--print (\"Initialising Spell Charges Frames\")", "end" + Environment.NewLine + "local function InitializeTwo()" + Environment.NewLine + "	--print (\"Initialising Spell Charges Frames\")" + Environment.NewLine);
                addonlua = addonlua.Replace("IsSpellInRange(name, \"target\")", "LibStub(\"SpellRange-1.0\").IsSpellInRange(name, \"target\")");
                // addonlua = addonlua.Replace("if (guid ~= lastTargetGUID) then", "");
                //ddonlua = addonlua.Replace("lastTargetGUID = guid" + Environment.NewLine + "	end", "print(\"target selected\")");
                addonlua = addonlua.Replace("local function InitializeOne()", Environment.NewLine + CustomLua + Environment.NewLine + "local function InitializeOne()");
                addonlua = addonlua.Replace("InitializeOne()" + Environment.NewLine + "            InitializeTwo()", "InitializeOne()" + Environment.NewLine + "            InitializeTwo()" + Environment.NewLine + "            InitializeThree()");
                addonlua = addonlua.Replace("healthFrame:SetScript(\"OnUpdate\", updateHealth)", "");
                addonlua = addonlua.Replace("powerFrame:SetScript(\"OnUpdate\", updatePower)", "");
                addonlua = addonlua.Replace("targetHealthFrame:SetScript(\"OnUpdate\", updateTargetHealth)", "");
                addonlua = addonlua.Replace("unitCombatFrame:SetScript(\"OnUpdate\", updateCombat)", "");
                addonlua = addonlua.Replace("unitPowerFrame:SetScript(\"OnUpdate\", updateUnitPower)", "");
                addonlua = addonlua.Replace("isTargetFriendlyFrame:SetScript(\"OnUpdate\", updateIsFriendly)", "");
                addonlua = addonlua.Replace("playerIsCastingFrame:SetScript(\"OnUpdate\", updatePlayerIsCasting)", "");
                addonlua = addonlua.Replace("hasTargetFrame: SetScript(\"OnUpdate\", hasTarget)", "");
                addonlua = addonlua.Replace("targetIsCastingFrame:SetScript(\"OnUpdate\", updateTargetIsCasting)", "");
                addonlua = addonlua.Replace("hasteFrame:SetScript(\"OnUpdate\", updateHaste)", "");
                addonlua = addonlua.Replace("unitIsVisibleFrame:SetScript(\"OnUpdate\", updateUnitIsVisible)", "");
                addonlua = addonlua.Replace("unitPetFrame:SetScript(\"OnUpdate\", updateUnitPet)", "");
                addonlua = addonlua.Replace("petHealthFrame:SetScript(\"OnUpdate\", updatePetHealth)", "");
                addonlua = addonlua.Replace("wildPetsFrame:SetScript(\"OnUpdate\", updateWildPetsFrame)", "");
                addonlua = addonlua.Replace("petBuffFrames[buffId]:SetScript(\"OnUpdate\", updateMyPetBuffs)", "");
                addonlua = addonlua.Replace("cooldownframes[spellId]:SetScript(\"OnUpdate\", updateSpellCooldowns)", "");
                addonlua = addonlua.Replace("spellInRangeFrames[spellId]:SetScript(\"OnUpdate\", updateSpellInRangeFrames)", "");
                addonlua = addonlua.Replace("targetDebuffFrames[debuffId]:SetScript(\"OnUpdate\", updateTargetDebuffs)", "");
                addonlua = addonlua.Replace("updateSpellChargesFrame[spellId]:SetScript(\"OnUpdate\", updateSpellCharges)", "");
                addonlua = addonlua.Replace("TargetBuffs[buffId]:SetScript(\"OnUpdate\", updateTargetBuffs)", "");
                addonlua = addonlua.Replace("PlayerMovingFrame:SetScript(\"OnUpdate\", PlayerNotMove)", "");
                addonlua = addonlua.Replace("AutoAtackingFrame:SetScript(\"OnUpdate\", AutoAtacking)", "");
                addonlua = addonlua.Replace("targetIsPlayerFrame:SetScript(\"OnUpdate\", updateIsPlayer)", "");
                addonlua = addonlua.Replace("flagFrame:SetScript(\"OnUpdate\", updateFlag)", "");
                addonlua = addonlua.Replace("targetLastSpellFrame[i]:SetScript(\"OnUpdate\", updateTargetCurrentSpell)", "");
                addonlua = addonlua.Replace("targetArena1Frame[i]:SetScript(\"OnUpdate\", updateArena1Spell)", "");
                addonlua = addonlua.Replace("targetArena2Frame[i]:SetScript(\"OnUpdate\", updateArena2Spell)", "");
                addonlua = addonlua.Replace("targetArena3Frame[i]:SetScript(\"OnUpdate\", updateArena3Spell)", "");
                addonlua = addonlua.Replace("buffFrames[buffId]:SetScript(\"OnUpdate\", updateMyBuffs)", "");
                addonlua = addonlua.Replace("itemframes[itemId]:SetScript(\"OnUpdate\", updateItemCooldowns)", "");
                addonlua = addonlua.Replace("spellOverlayedFrames[spellId]:SetScript(\"OnUpdate\", updateIsSpellOverlayedFrames)", "");
                addonlua = addonlua.Replace("playerDebuffFrames[debuffId]:SetScript(\"OnUpdate\", updatePlayerDebuffs)", "");
                File.WriteAllText("" + WoW.AddonPath + "\\" + AddonName + "\\" + AddonName + ".lua", addonlua);
                AddonEdited = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Log.Write("Editing Addon Complete");
        }

        public override Form SettingsForm { get; set; }

        private const string CustomLua = @"local Healingbuffs =  ""Riptide""
local Race = {
	[""Human""] = 0.01,
	[""Dwarf""]= 0.02,
	[""nightelf""]= 0.03,
	[""Gnome""]= 0.04,
	[""Draenei""]= 0.05,
	[""Pandaren""]= 0.06,
	[""Orc""]= 0.07,
	[""Undead""]= 0.08,
	[""Tauren""]= 0.09,
	[""Troll""]= 0.10,
	[""Blood Elf""]= 0.11,
	[""Goblin""]= 0.12,
    [""Worgen""] = 0.13,
}
local Spec = {
	[""Blood""] = 0.01,
	[""Frost""] = 0.02,
	[""Unholy""] =0.03,
	[""Havoc""] = 0.04,
	[""Vengeance""] =0.05,
	[""Balance""] = 0.06,
	[""Feral""] =0.07,
	[""Guardian""] =0.08,
	[""Restoration""] =0.09,
	[""Beast Mastery""] = 0.10,
	[""Marksmanship""] =0.11,
	[""Survival""] = 0.12,
	[""Arcane""] =0.13,
	[""Fire""] = 0.14,
	[""Frost""] =0.15,
	[""Brewmaster""] = 0.16,
	[""Mistweaver""] =0.17,
	[""Windwalker""] = 0.18,
	[""Holy""] = 0.19,
	[""Protection""] = 0.20,
	[""Retribution""] =0.21,
	[""Discipline""] = 0.22,
	[""HolyPriest""]=.23,
	[""Shadow""] =0.24,
	[""Assassination""] =0.25,
	[""Outlaw""] =0.26,
	[""Subtlety""] = 0.27,
	[""Elemental""] = 0.28,
	[""Enhancement""] = 0.29,
	[""RestorationShaman""] = 0.30,
	[""Affliction""] = 0.31,
	[""Arms""] = 0.32,
	[""Fury""] = 0.33,
	[""Protection""] = 0.34,
	[""Demonology""] = 0.35,
    [""Destruction""] = 0.36,
}

local activeUnitPlates = {}
local RaidBuffFrame = {}
local raidSizeFrame = nil
local PlayerStatFrame = {}
local targetInfoFrame = nil
local timerDBMFrames = {}
local PlatesOn = 0
local party_units = {}
local raid_units = {}
local raidheal_cache = {}
local raidHealthFrame = {}
local RaidRole = {}
local RaidRange = {}
local lasthp = {}
local castPCT = 0
local charUnit = {}
local Tier = {}
local lasthaste = 0
local hasteInfo = {}
local raidBuff = {}
local raidBufftime = {}
local partySize = 0
local setBonusFrame = nil

local frame = CreateFrame(""frame"", """", parent)
frame:RegisterEvent(""NAME_PLATE_UNIT_ADDED"")
frame:RegisterEvent(""UNIT_HEALTH_FREQUENT"")
frame:RegisterEvent(""RAID_ROSTER_UPDATE"")
frame:RegisterEvent(""GROUP_ROSTER_UPDATE"")
frame:RegisterUnitEvent(""UNIT_SPELL_HASTE"",""player"")
frame:RegisterUnitEvent(""UNIT_POWER"",""player"")
frame:RegisterEvent(""PLAYER_REGEN_DISABLED"")
frame:RegisterEvent(""PLAYER_REGEN_ENABLED"")
frame:RegisterEvent(""PLAYER_ENTERING_WORLD"")
frame:RegisterEvent(""CHAT_MSG_ADDON"")
frame:RegisterUnitEvent(""UNIT_HEALTH"",""player"")
frame:RegisterEvent(""PLAYER_EQUIPMENT_CHANGED"")
frame:RegisterEvent(""PLAYER_TARGET_CHANGED"")
frame:RegisterEvent(""PLAYER_ENTER_COMBAT"")
frame:RegisterEvent(""PLAYER_LEAVE_COMBAT"")
frame:RegisterEvent(""PLAYER_CONTROL_LOST"")
frame:RegisterEvent(""PLAYER_CONTROL_GAINED"")

local function updateFlag(self, event)
	if event == ""PLAYER_CONTROL_GAINED"" then
		flagFrame.t:SetColorTexture(0,0,0,alphaColor)
	end
	if event == ""PLAYER_CONTROL_LOST"" then
		flagFrame.t:SetColorTexture(1,0,0,alphaColor)
	end
end

local function HasteInfoUpdate()
	local ratingBonus = math.floor(GetHaste())
	if lasthaste == ratingBonus then return end
	lastehaste = ratingBonus
	hasteInfo[2] = 0
	if (ratingBonus * -1) > ratingBonus then
		HasteInfo[2] = 1
	end
	hasteInfo[1] = tonumber(""0.0"".. math.abs(ratingBonus))
	if (math.abs(ratingBonus) >= 10) then
		hasteInfo[1] = tonumber(""0."".. math.abs(ratingBonus))
	end

end
local function UpdateMana()
		charUnit[1] = UnitPower(""player"",0) / UnitPowerMax(""player"",0)
		HasteInfoUpdate()
		PlayerStatFrame[4].t:SetColorTexture(hasteInfo[2], hasteInfo[1],charUnit[1], alphaColor)
end


local function Talents()
		AllSeven = false
		for i = 1, GetMaxTalentTier() do 
		talenetSelected = false
			for z = 1, 3 do 
				local  selected = select(4, GetTalentInfo(i, z,1))
				if(selected == true) then
					Tier[i] = z/100
					talenetSelected = true
				end
				if talenetSelected == false and z == 3 then
					Tier[i]=0
				end
			end
			if i == 7 then
				AllSeven = true 
			end
		end
	if not AllSeven then
		for i = GetMaxTalentTier(), 7 do
			Tier[i] = 0	
		end
	end
	PlayerStatFrame[1].t:SetColorTexture(Tier[1], Tier[2],Tier[3], alphaColor)
	PlayerStatFrame[2].t:SetColorTexture(Tier[4],Tier[5],Tier[6], alphaColor)
	PlayerStatFrame[3].t:SetColorTexture(Tier[7],charUnit[3],charUnit[2], alphaColor)
end

local function CharRaceUpdate()
	charUnit[2] = Race[UnitRace(""player"")]
	charUnit[3] = Spec[select(2, GetSpecializationInfo(GetSpecialization()))]
	
end


local function UnitIsPartyUnit(unit)
	--print(""checking :"", unit)
	for _, v in next, party_units do
		if unit == v then return true end
	end
end

local function UnitIsRaidUnit(unit)
	for _, v in next, raid_units do
		if unit == v then return true end
	end
end

local function HealthChangedEvent(unit)
	local h = UnitHealth(unit)
	if h==lasthp[unit] then return end
	lasthp[unit]=h
	local m = UnitHealthMax(unit);
	h = (h / m)
	raidheal_cache[unit] = h
end

local function RangeCheck(unit)
	if LibStub(""SpellRange-1.0"").IsSpellInRange(""Healing Wave"", unit) == 1 then
		RaidRange[unit] = 1;
	else
		RaidRange[unit] = .5;
	end
end

local function RaidRoleCheck(unit)
	if UnitGroupRolesAssigned(unit) == ""TANK"" then
		RaidRole[unit] = 1;
	elseif UnitGroupRolesAssigned(unit) == ""HEALER"" then
		RaidRole[unit] = .5;
	else
		RaidRole[unit] = 0;
	end
end

local function UpdateRaidIndicators(unit)
	if UnitIsRaidUnit(unit) or UnitIsPartyUnit(unit) then
		if UnitInParty (""player"") and not UnitInRaid (""player"") then
		--print(unit,""needs heals"")
			for i, key in pairs(party_units) do
				if key == unit then
					HealthChangedEvent(unit)
					RangeCheck(unit)
					RaidRoleCheck(unit)
					--print(unit, ""is at :"", raidheal_cache[unit])
					raidHealthFrame[i].t:SetColorTexture(raidheal_cache[unit], RaidRange[unit], RaidRole[unit], alphaColor)
				end	
			end
		end
		if UnitInRaid (""player"") then 
			for i, key in pairs(raid_units) do
				if key == unit then
					HealthChangedEvent(unit)
					RangeCheck(unit)
					RaidRoleCheck(unit)
				
					--print(unit, ""is at :"", raidheal_cache[unit], "" and At : "", i)
					raidHealthFrame[i].t:SetColorTexture(raidheal_cache[unit], RaidRange[unit], RaidRole[unit], alphaColor)
				end	
			end
		end
		if not UnitInRaid (""player"") and not UnitInParty (""player"") then
			for i = 1, 30 do
				raidHealthFrame[i].t:SetColorTexture(1, 0, 0, alphaColor)
			end
		end
	end
end

local function updateTotemsFrame()
	Totemsframe = 0
	TotemDuration = 0 
	for i = 1, 4 do
		haveTotem, name, startTime, duration, icon = GetTotemInfo(i)
		local Quesatime =  startTime + duration
		if haveTotem then
			if (name == ""Spirit Wolf"" or name == ""Totem Mastery"")
			and(startTime + duration - GetTime() > 1.5 ) then
				Totemsframe = 1;
				TotemDuration = startTime + duration - GetTime()
			end
		end
	end
	totemsFrame.t:SetColorTexture(Totemsframe, TotemDuration,0, alphaColor)
	
end


local function UpdateRaidBuffIndicators(unit)
		if select(7, UnitBuff(unit, Healingbuffs, ""player"")) == nil  then return end
		UpdateRaidBuffslot(unit,expires)
end

local function UpdateRaidBuffslot(unit,expires)
	for i = 1, 4 do 
		if raidBuff[i] == 0 then
			local slot = string.match (unit, ""%d+"")
			UpdateBuffTime(unit,expires,i)
			if i >= 10 then
				raidBuff[i] = tonumber(""0."" .. slot)
			else 
				raidBuff[i] = tonumber(""0.0"" .. slot)
			end
		end
	end
end

local function UpdateBuffTime(unit,expires,location)
	local remainingTime = math.floor(expires -  GetTime() + 0.5)
	if(remainingTime >= 10) then
		raidBufftime[i] = tonumber(""0.""..remainingTime);
	else
		raidBufftime[i] = tonumber(""0.0""..remainingTime);
	end
end

local function updateRaidBuff(self, event)
	if not UnitInRaid (""player"") and  UnitInParty (""player"") then
		for key, _ in pairs(party_units) do
			UpdateRaidBuffIndicators(key)
		end
	end	
	if UnitInRaid (""player"") then
		for key, _ in pairs(raid_units) do
			UpdateRaidBuffIndicators(key)
		end
	end
	for i=1, 4 do 
		if raidBuff[i] ~= nil then
			RaidBuffFrame[i].t:SetColorTexture(raidBuff[i], 1, raidBufftime[i], alphaColor)
			RaidBuffFrame[i].t:SetAllPoints(false)
		else
			RaidBuffFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
			RaidBuffFrame[i].t:SetAllPoints(false)
			raidBuff[i] = nil
			raidBufftime[i] = nil
		end
	end
end
local function TurnOnPlates()
	if GetCVar(""nameplateShowEnemies"") == ""1"" then
		PlatesOn = 1
	else
		PlatesOn = 0
	end
end
local enemiesPlate = 0
local function activeEnemies()
    enemiesPlate = 0
    for k, v in pairs(activeUnitPlates) do
        if v ~= nil then
            if UnitCanAttack(""player"", k) 
            and (LibStub(""SpellRange-1.0"").IsSpellInRange(""Death Strike"", k) == 1 and charUnit[3] == .01 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Frost Strike"", k) == 1 and charUnit[3] == .02 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Festering Strike"", k) == 1 and charUnit[3] == .03 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Demon's Bite"", k) == 1 and charUnit[3] == .04 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Shear"", k) == 1 and charUnit[3] == .05 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Solar Wrath"", k) == 1 and charUnit[3] == .06 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Shred"", k) == 1 and charUnit[3] == .07 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Mangle"", k) == 1 and charUnit[3] == .08 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Solar Wrath"", k) == 1 and charUnit[3] == .09 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Counter Shot"", k) == 1 and charUnit[3] == .10 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Counter Shot"", k) == 1 and charUnit[3] == .11 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Mongoose Bite"", k) == 1 and charUnit[3] == .12 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Arcane Blast"", k) == 1 and charUnit[3] == .13 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Fireball"", k) == 1 and charUnit[3] == .14 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Frostbolt"", k) == 1 and charUnit[3] == .15 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Blackout Strike"", k) == 1 and charUnit[3] == .16 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Rising Sun Kick"", k) == 1 and charUnit[3] == .17 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Rising Sun Kick"", k) == 1 and charUnit[3] == .18 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Crusader Strike"", k) == 1 and charUnit[3] == .19 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Shield of the Righteous"", k) == 1 and charUnit[3] == .20 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Crusader Strike"", k) == 1 and charUnit[3] == .21 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Penance"", k) == 1 and charUnit[3] == .22 or
			
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Smite"", k) == 1 and charUnit[3] == .23 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Mind Blast"", k) == 1 and charUnit[3] == .24 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Mutilate"", k) == 1 and charUnit[3] == .25 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Saber Slash"", k) == 1 and charUnit[3] == .26 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Backstab"", k) == 1 and charUnit[3] == .27 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Lightning Bolt"", k) == 1 and charUnit[3] == .28 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Rockbiter"", k) == 1 and charUnit[3] == .29 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Lightning Bolt"", k) == 1 and charUnit[3] == .30 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Agony"", k) == 1 and charUnit[3] == .31 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Mortal Strike"", k) == 1 and charUnit[3] == .32 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Bloodthirst"", k) == 1 and charUnit[3] == .33 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Devastate"", k) == 1 and charUnit[3] == .34 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Doom"", k) == 1 and charUnit[3] == .35 or
			LibStub(""SpellRange-1.0"").IsSpellInRange(""Incinerate"", k) == 1 and charUnit[3] == .36) and
            UnitIsDead(k) == false and UnitAffectingCombat(k) then
                enemiesPlate = enemiesPlate + 1
            --print(""what is k:"",k,""Is "",v,"" In range "",LibStub(""SpellRange-1.0"").IsSpellInRange(""Mongoose Bite"", v),"" Is unit dead :"",UnitIsDead(v))
        
            end
        end
    end
    enemiesPlate = enemiesPlate/100
    
end
local function NameplateFrameUPDATE()
	activeEnemies()
	TurnOnPlates()
	--TurnOnPlates()
	--print(""Npc counT"",enemiesPlate)
	raidSizeFrame.t:SetColorTexture(partySize, enemiesPlate, PlatesOn, alphaColor)
end

local function AddNameplate(unitID)
	local GUID = false
	for k,v in pairs(activeUnitPlates)do
		 activeUnitPlates[k] = nil
	end	
	for i=1, 30 do
		if	UnitGUID (""nameplate""..i) ~= nil and not UnitIsPlayer(""nameplate"".. i) then
			for k, v in pairs(activeUnitPlates) do
			--print(""Nameplate :"",""nameplate""..i,""GUID"",UnitGUID (""nameplate""..i)  )
				if v == UnitGUID (""nameplate""..i) then
					GUID = true
				end
			end
			if GUID == false then
				--print(""SET Nameplate :"",""nameplate""..i,""GUID"",UnitGUID (""nameplate""..i)  )
				activeUnitPlates[""nameplate""..i] = UnitGUID (""nameplate""..i)
				GUID = false
			end
		end
		GUID = false
	end
end
local function ClearNamePlates()
	for k,v in pairs(activeUnitPlates)do
		if  k ~=UnitGUID(""target"") then 
			activeUnitPlates[k] = nil
		end 
	end	
end


local function register_unit(tbl, unit)
		table.insert(tbl, unit)
end
do
	for i = 1, 5 do
		register_unit(party_units, (""party%d""):format(i))
	end
	for	i = 1,30 do
		register_unit(raid_units, (""raid%d""):format(i))
	end
end
local invSlots = {
1,3,5,7,10,15
}
local setItems = {
	--shaman
	138343, 138348, 138372, 138346, 138341, 138345
}
local function updateSetBonus()
	count = 0
	for _, v in pairs(invSlots) do
		itemID = GetInventoryItemID(""player"", v);
		for _,z in pairs(setItems) do
			if(itemID == z)then
			--print(v, "" "", z)
				count= count +1
			end
		end

	end
	setBonusFrame.t:SetColorTexture(count/100, 0, 0, alphaColor)
end

local function UdateRaidSizeFrame(self, event)
	partySize = GetNumGroupMembers() ;
	TurnOnPlates()
	partySize = partySize /100
	--print(""Party Size: "",partySize)
	if partySize > .30 then
		partySize = .30
	end
	--print(""Partyupdate :"",partySize)
	--print(""Name plates :"", PlatesOn)
	raidSizeFrame.t:SetColorTexture(partySize, enemiesPlate, PlatesOn, alphaColor)
end
local DBMTIMER = {}

local function updateDBMFrames(elapsed)
	if(pullvalue and tickerdbm >= 0) then
		tickerdbm = tickerdbm - elapsed
		timerDBMFrames.t:SetColorTexture( 1,tickerdbm/10,0, alphaColor)
	else
		timerDBMFrames.t:SetColorTexture( 0,0,0, alphaColor)
	end
end
local timer 
local function DBMPull(prefix,msg,sender)
	_, _,_,_,_, _,_, instanceMapID, _ = GetInstanceInfo()
	if prefix == ""D4"" and select(1,strsplit(""\t"", msg)) == ""PT""
    and (UnitInRaid(Ambiguate(sender, ""short"")) or UnitInParty ( Ambiguate (sender, ""short"") ) )
   	and tonumber ( select(3, strsplit(""\t"", msg) ) ) == instanceMapID  then
		local time = select(2,strsplit(""\t"", msg) )
		time =  tonumber(time)
		if time ~= 0 then
			print(""DBM pull timer"")
			tickerdbm = time
			pullvalue = true
		end
		if time == 0 then
			tickerdbm = time
			pullvalue = false
		end

	end
end
local function updatetargetInfoFrame()
	targetexist = 0
	rangetargetexist = 0
	if UnitExists(""target"") then
		if	LibStub(""SpellRange-1.0"").IsSpellInRange(""Rockbiter"", ""target"") == 1 and charUnit[3] == .29  then
			targetexist = 1
		end
		if LibStub(""SpellRange-1.0"").IsSpellInRange(""Lightning Bolt"", ""target"") == 1 and charUnit[3] == .28 then
			targetexist = 1
		end
	end
	if UnitExists(""mouseover"") and UnitAffectingCombat(""mouseover"") and LibStub(""SpellRange-1.0"").IsSpellInRange(""Lightning Bolt"", ""mouseover"") == 1 then
			rangetargetexist = 1
	end
	
	--print(""Info "", targetexist, "" "", rangetargetexist )
	targetInfoFrame.t:SetColorTexture(targetexist,rangetargetexist, 0 ,alphaColor)
end
local function InitializeThree()
	for i = 1, 4 do 
		raidBuff[i] = 1
		raidBufftime[i] =1
	end
		--print (""Initialising raid Health Frames"")
	for i = 1, 20 do	
		raidHealthFrame[i] = CreateFrame(""frame"", """", parent)
		raidHealthFrame[i]:SetSize(size, size)
		raidHealthFrame[i]:SetPoint(""TOPLEFT"", size*(i-1), -size *18 )   --  row 1-20,  column 19
		raidHealthFrame[i].t = raidHealthFrame[i]:CreateTexture()        
		raidHealthFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		raidHealthFrame[i].t:SetAllPoints(raidHealthFrame[i])
		raidHealthFrame[i]:Show()
	end
	for i = 21, 30 do		
		raidHealthFrame[i] = CreateFrame(""frame"", """", parent)
		raidHealthFrame[i]:SetSize(size, size)
		raidHealthFrame[i]:SetPoint(""TOPLEFT"", size*(i-21), -size *19 )   --  row 1-10,  column 20
		raidHealthFrame[i].t = raidHealthFrame[i]:CreateTexture()        
		raidHealthFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		raidHealthFrame[i].t:SetAllPoints(raidHealthFrame[i])
		raidHealthFrame[i]:Show()
	end
		raidSizeFrame = CreateFrame(""frame"", """", parent)
		raidSizeFrame:SetSize(size, size)
		raidSizeFrame:SetPoint(""TOPLEFT"", size*(10), -size *19 )   --  row 11,  column 20
		raidSizeFrame.t = raidSizeFrame:CreateTexture()        
		raidSizeFrame.t:SetColorTexture(1, 1, 1, alphaColor)
		raidSizeFrame.t:SetAllPoints(raidSizeFrame)
		raidSizeFrame:Show()
		raidSizeFrame:SetScript(""OnUpdate"",NameplateFrameUPDATE)
		
	for i = 1, 4 do		
		RaidBuffFrame[i] = CreateFrame(""frame"", """", parent)
		RaidBuffFrame[i]:SetSize(size, size)
		RaidBuffFrame[i]:SetPoint(""TOPLEFT"", size*(10 + i), -size *19 )   --  row 12-15,  column 20
		RaidBuffFrame[i].t = RaidBuffFrame[i]:CreateTexture()        
		RaidBuffFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		RaidBuffFrame[i].t:SetAllPoints(RaidBuffFrame[i])
		RaidBuffFrame[i]:Show()
		
	end
	for i = 1, 5 do
		PlayerStatFrame[i] = CreateFrame(""frame"", """", parent)
		PlayerStatFrame[i]:SetSize(size, size)
		PlayerStatFrame[i]:SetPoint(""TOPLEFT"", size*(i-1), -size *20 )   --  row 1-4,  column 21
		PlayerStatFrame[i].t =PlayerStatFrame[i]:CreateTexture()        
		PlayerStatFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		PlayerStatFrame[i].t:SetAllPoints(PlayerStatFrame[i])
		PlayerStatFrame[i]:Show()
	end
		timerDBMFrames = CreateFrame(""frame"", """", parent)
		timerDBMFrames:SetSize(size, size);
		timerDBMFrames:SetPoint(""TOPLEFT"", size * 5, -(size * 20))           -- column 6 row 21
		timerDBMFrames.t = timerDBMFrames:CreateTexture()        
		timerDBMFrames.t:SetColorTexture(0, 0, 0, alphaColor)
		timerDBMFrames.t:SetAllPoints(timerDBMFrames)
		timerDBMFrames:Show()	


		totemsFrame = CreateFrame(""frame"", """", parent)
		totemsFrame:SetSize(size, size);
		totemsFrame:SetPoint(""TOPLEFT"", size * 6, -(size * 20))           -- column 7 row 21
		totemsFrame.t = totemsFrame:CreateTexture()        
		totemsFrame.t:SetColorTexture(0, 0, 0, alphaColor)
		totemsFrame.t:SetAllPoints(totemsFrame)
		totemsFrame:Show()

		targetInfoFrame = CreateFrame(""frame"", """", parent)
		targetInfoFrame:SetSize(size, size);
		targetInfoFrame:SetPoint(""TOPLEFT"", size * 7, -(size * 20))           -- column 8 row 21
		targetInfoFrame.t = targetInfoFrame:CreateTexture()        
		targetInfoFrame.t:SetColorTexture(0, 0, 0, alphaColor)
		targetInfoFrame.t:SetAllPoints(targetInfoFrame)
		targetInfoFrame:Show()

		setBonusFrame = CreateFrame(""frame"", """", parent)
		setBonusFrame:SetSize(size, size);
		setBonusFrame:SetPoint(""TOPLEFT"", size * 8, -(size * 20))           -- column 9 row 21
		setBonusFrame.t = setBonusFrame:CreateTexture()        
		setBonusFrame.t:SetColorTexture(0, 0, 0, alphaColor)
		setBonusFrame.t:SetAllPoints(setBonusFrame)
		setBonusFrame:Show()
end

local function HealinEventHandler(self,event, ...)
    if event == ""NAME_PLATE_UNIT_ADDED"" then
		    AddNameplate(select(1,...))
			activeEnemies()
    end
	if event == ""UNIT_HEALTH_FREQUENT"" then
	
		if (select(1,...) ~= ""player"") then
			UpdateRaidIndicators(select(1,...))
		end
	end

	if event == ""RAID_ROSTER_UPDATE"" or event == ""GROUP_ROSTER_UPDATE"" then
	   UdateRaidSizeFrame()
    end
	if event == ""UNIT_SPELL_HASTE"" then
		HasteInfoUpdate()
		updateHaste()
	end
	if event == ""UNIT_POWER"" then
		UpdateMana()
		updatePower()
		updateUnitPower()
	end
	if event == ""PLAYER_ENTERING_WORLD""then
		CharRaceUpdate()
		Talents()
		TurnOnPlates()
		updateSetBonus()
	end
	if event == ""PLAYER_REGEN_DISABLED""then 
		ClearNamePlates()
		CharRaceUpdate()
		Talents()
        updatePower()
        updateUnitPower()
		UpdateMana()
		updateCombat()
		updateSetBonus()
	end
	if event == ""PLAYER_REGEN_ENABLED""then 
		ClearNamePlates()
		activeEnemies()
		updateCombat()
	end
	if event == ""PLAYER_EQUIPMENT_CHANGED"" then
		updateSetBonus()
	end
	if event == ""CHAT_MSG_ADDON""then 
		--select(1,...) --prefix
		--select(2,...) --msg
		--select(4,...) sender
		DBMPull(select(1,...),select(2,...),select(4,...))
	end

	if event == ""UNIT_HEALTH"" then
		updateHealth()
		updateTargetHealth()
	end
	if event == ""PLAYER_TARGET_CHANGED"" then
		updateHealth()
		updateTargetHealth()
		updateIsFriendly()
		AutoAtacking()
		updateUnitIsVisible()
		updateIsPlayer()
		updateCombat()
		hasTarget()
		updatetargetInfoFrame()
	end
	if event == ""PLAYER_ENTER_COMBAT"" or event == ""PLAYER_LEAVE_COMBAT"" then
		updateIsFriendly()
		AutoAtacking()
		updateHealth()
		updateTargetHealth()
		updateUnitIsVisible()
		updateIsPlayer()
		updateCombat()
		hasTarget()
		updatetargetInfoFrame()
	end

end

local GlobalTimer = 0
local function onUpDateFunction(self,elapsed)
		if GlobalTimer <= 0 then
			GlobalTimer = .10
		else
			GlobalTimer = GlobalTimer - elapsed
		end
		
		if GlobalTimer <= .01 then
			updateRaidBuff()
			
			updateDBMFrames(elapsed)
			updatePlayerIsCasting()
			updateTargetIsCasting()
						
		    if (classIndex == 6 or                                  -- DeathKnight   
				classIndex == 3 or                                  -- Hunter
				classIndex == 9 or                                  -- Warlock
				classIndex == 8 or                                  -- Mage
				classIndex == 7)                                    -- Shaman (Enh. Needs it for Wolves)
				then
					updateUnitPet()
					updatePetHealth()
					updateWildPetsFrame()
					updateMyPetBuffs()
					updateTotemsFrame()
			end
			updateSpellCooldowns()
			updateSpellInRangeFrames()
			updateTargetDebuffs()
			updateSpellCharges()
			updateTargetBuffs()
			PlayerNotMove()
			updateTargetCurrentSpell()
			updateArena1Spell()
			updateArena2Spell()
			updateArena3Spell()
			updateMyBuffs()
			updateItemCooldowns()
			updatePlayerDebuffs()
			updateIsSpellOverlayedFrames()

			 GlobalTimer= .10
		end
end	
frame:SetScript(""OnEvent"",HealinEventHandler)
frame:SetScript(""OnUpdate"",onUpDateFunction)";
        private const string LibSpellToc = "LibSpellRange-1.0.toc";
        private const string LibSpellTocContent = @"## Interface: 70100
## X-CompatibleWith: 50400
## Title: Lib: SpellRange-1.0
## Notes: Provides enhanced spell range checking functionality
## Author: Cybeloras of Aerie Peak
## X-Category: Library
## X-Curse-Packaged-Version: 1.0.011
## X-Curse-Project-Name: LibSpellRange-1.0
## X-Curse-Project-ID: libspellrange-1-0
## X-Curse-Repository-ID: wow/libspellrange-1-0/mainline

libs\LibStub\LibStub.lua

lib.xml";
        private const string LibSpellLua = "LibSpellRange-1.0.lua";
        private const string LibSpellLuaContent = @"--- = Background =
-- Blizzard's IsSpellInRange API has always been very limited - you either must have the name of the spell, or its spell book ID. Checking directly by spellID is simply not possible.
-- Now, in Mists of Pandaria, Blizzard changed the way that many talents and specialization spells work - instead of giving you a new spell when leaned, they replace existing spells. These replacement spells do not work with Blizzard's IsSpellInRange function whatsoever; this limitation is what prompted the creation of this lib.
-- = Usage = 
-- **LibSpellRange-1.0** exposes an enhanced version of IsSpellInRange that:
-- * Allows ranged checking based on both spell name and spellID.
-- * Works correctly with replacement spells that will not work using Blizzard's IsSpellInRange method alone.
--
-- @class file
-- @name LibSpellRange-1.0.lua

local major = ""SpellRange-1.0""
local minor = 11

assert(LibStub, format(""%s requires LibStub."", major))

local Lib = LibStub:NewLibrary(major, minor)
if not Lib then return end

local tonumber = _G.tonumber
local strlower = _G.strlower
local wipe = _G.wipe
local type = _G.type

local GetSpellTabInfo = _G.GetSpellTabInfo
local GetNumSpellTabs = _G.GetNumSpellTabs
local GetSpellBookItemInfo = _G.GetSpellBookItemInfo
local GetSpellBookItemName = _G.GetSpellBookItemName
local GetSpellLink = _G.GetSpellLink
local GetSpellInfo = _G.GetSpellInfo

local IsSpellInRange = _G.IsSpellInRange
local SpellHasRange = _G.SpellHasRange

-- isNumber is basically a tonumber cache for maximum efficiency
Lib.isNumber = Lib.isNumber or setmetatable({}, {
	__mode = ""kv"",
	__index = function(t, i)
		local o = tonumber(i) or false
		t[i] = o
		return o
end})
local isNumber = Lib.isNumber

-- strlower cache for maximum efficiency
Lib.strlowerCache = Lib.strlowerCache or setmetatable(
{}, {
	__index = function(t, i)
		if not i then return end
		local o
		if type(i) == ""number"" then
			o = i
		else
			o = strlower(i)
		end
		t[i] = o
		return o
	end,
}) local strlowerCache = Lib.strlowerCache

-- Matches lowercase player spell names to their spellBookID
Lib.spellsByName_spell = Lib.spellsByName_spell or {}
local spellsByName_spell = Lib.spellsByName_spell

-- Matches player spellIDs to their spellBookID
Lib.spellsByID_spell = Lib.spellsByID_spell or {}
local spellsByID_spell = Lib.spellsByID_spell

-- Matches lowercase pet spell names to their spellBookID
Lib.spellsByName_pet = Lib.spellsByName_pet or {}
local spellsByName_pet = Lib.spellsByName_pet

-- Matches pet spellIDs to their spellBookID
Lib.spellsByID_pet = Lib.spellsByID_pet or {}
local spellsByID_pet = Lib.spellsByID_pet

-- Updates spellsByName and spellsByID
local function UpdateBook(bookType)
	local _, _, offs, numspells = GetSpellTabInfo(3)
	local max = offs -- The offset of the next tab is the max ID of the previous tab.
	if numspells == 0 then
		-- New characters pre level 10 only have 2 tabs.
		local _, _, offs, numspells = GetSpellTabInfo(2)
		max = offs + numspells 
	end

	local spellsByName = Lib[""spellsByName_"" .. bookType]
	local spellsByID = Lib[""spellsByID_"" .. bookType]
	
	wipe(spellsByName)
	wipe(spellsByID)
	
	for spellBookID = 1, max do
		local type, baseSpellID = GetSpellBookItemInfo(spellBookID, bookType)
		
		if type == ""SPELL"" then
			local currentSpellName = GetSpellBookItemName(spellBookID, bookType)
			local link = GetSpellLink(currentSpellName)
			local currentSpellID = tonumber(link and link:gsub(""|"", ""||""):match(""spell:(%d+)""))
			
			local baseSpellName = GetSpellInfo(baseSpellID)
			
			if currentSpellName then
				spellsByName[strlower(currentSpellName)] = spellBookID
			end
			if baseSpellName then
				spellsByName[strlower(baseSpellName)] = spellBookID
			end
			
			if currentSpellID then
				spellsByID[currentSpellID] = spellBookID
			end
			if baseSpellID then
				spellsByID[baseSpellID] = spellBookID
			end
		end
	end
end

-- Handles updating spellsByName and spellsByID
if not Lib.updaterFrame then
	Lib.updaterFrame = CreateFrame(""Frame"")
end
Lib.updaterFrame:UnregisterAllEvents()
Lib.updaterFrame:RegisterEvent(""SPELLS_CHANGED"")

local function UpdateSpells()
	UpdateBook(""spell"")
	UpdateBook(""pet"")
end

Lib.updaterFrame:SetScript(""OnEvent"", UpdateSpells)
UpdateSpells()

--- Improved spell range checking function.
-- @name SpellRange.IsSpellInRange
-- @paramsig spell, unit
-- @param spell Name or spellID of a spell that you wish to check the range of. The spell must be a spell that you have in your spellbook or your pet's spellbook.
-- @param unit UnitID of the spell that you wish to check the range on.
-- @return Exact same returns as http://wowprogramming.com/docs/api/IsSpellInRange
-- @usage
-- -- Check spell range by spell name on unit ""target""
-- local SpellRange = LibStub(""SpellRange-1.0"")
-- local inRange = SpellRange.IsSpellInRange(""Stormstrike"", ""target"")
--
-- -- Check spell range by spellID on unit ""mouseover""
-- local SpellRange = LibStub(""SpellRange-1.0"")
-- local inRange = SpellRange.IsSpellInRange(17364, ""mouseover"")
function Lib.IsSpellInRange(spellInput, unit)
	if isNumber[spellInput] then
		local spell = spellsByID_spell[spellInput]
		if spell then
			return IsSpellInRange(spell, ""spell"", unit)
		else
			local spell = spellsByID_pet[spellInput]
			if spell then
				return IsSpellInRange(spell, ""pet"", unit)
			end
		end
	else
		local spellInput = strlowerCache[spellInput]
		
		local spell = spellsByName_spell[spellInput]
		if spell then
			return IsSpellInRange(spell, ""spell"", unit)
		else
			local spell = spellsByName_pet[spellInput]
			if spell then
				return IsSpellInRange(spell, ""pet"", unit)
			end
		end
		
		return IsSpellInRange(spellInput, unit)
	end
	
end


--- Improved SpellHasRange.
-- @name SpellRange.SpellHasRange
-- @paramsig spell
-- @param spell Name or spellID of a spell that you wish to check for a range. The spell must be a spell that you have in your spellbook or your pet's spellbook.
-- @return Exact same returns as http://wowprogramming.com/docs/api/SpellHasRange
-- @usage
-- -- Check if a spell has a range by spell name
-- local SpellRange = LibStub(""SpellRange-1.0"")
-- local hasRange = SpellRange.SpellHasRange(""Stormstrike"")
--
-- -- Check if a spell has a range by spellID
-- local SpellRange = LibStub(""SpellRange-1.0"")
-- local hasRange = SpellRange.SpellHasRange(17364)
function Lib.SpellHasRange(spellInput)
	if isNumber[spellInput] then
		local spell = spellsByID_spell[spellInput]
		if spell then
			return SpellHasRange(spell, ""spell"")
		else
			local spell = spellsByID_pet[spellInput]
			if spell then
				return SpellHasRange(spell, ""pet"")
			end
		end
	else
		local spellInput = strlowerCache[spellInput]
		
		local spell = spellsByName_spell[spellInput]
		if spell then
			return SpellHasRange(spell, ""spell"")
		else
			local spell = spellsByName_pet[spellInput]
			if spell then
				return SpellHasRange(spell, ""pet"")
			end
		end
		
		return SpellHasRange(spellInput)
	end
	
end";
        private const string LibXml = "lib.xml";
        private const string LibXmlContent = @"<Ui>
	<Script file=""LibSpellRange-1.0.lua""/>
</Ui>";
        private const string LibStubLua = "LibStub.lua";
        private const string LibStubLuaContent = @"-- $Id: LibStub.lua 103 2014-10-16 03:02:50Z mikk $
-- LibStub is a simple versioning stub meant for use in Libraries.  http://www.wowace.com/addons/libstub/ for more info
-- LibStub is hereby placed in the Public Domain
-- Credits: Kaelten, Cladhaire, ckknight, Mikk, Ammo, Nevcairiel, joshborke
local LIBSTUB_MAJOR, LIBSTUB_MINOR = ""LibStub"", 2  -- NEVER MAKE THIS AN SVN REVISION! IT NEEDS TO BE USABLE IN ALL REPOS!
local LibStub = _G[LIBSTUB_MAJOR]

-- Check to see is this version of the stub is obsolete
if not LibStub or LibStub.minor < LIBSTUB_MINOR then
	LibStub = LibStub or {libs = {}, minors = {} }
	_G[LIBSTUB_MAJOR] = LibStub
	LibStub.minor = LIBSTUB_MINOR
	
	-- LibStub:NewLibrary(major, minor)
	-- major (string) - the major version of the library
	-- minor (string or number ) - the minor version of the library
	-- 
	-- returns nil if a newer or same version of the lib is already present
	-- returns empty library object or old library object if upgrade is needed
	function LibStub:NewLibrary(major, minor)
		assert(type(major) == ""string"", ""Bad argument #2 to `NewLibrary' (string expected)"")
		minor = assert(tonumber(strmatch(minor, ""%d+"")), ""Minor version must either be a number or contain a number."")
		
		local oldminor = self.minors[major]
		if oldminor and oldminor >= minor then return nil end
		self.minors[major], self.libs[major] = minor, self.libs[major] or {}
		return self.libs[major], oldminor
	end
	
	-- LibStub:GetLibrary(major, [silent])
	-- major (string) - the major version of the library
	-- silent (boolean) - if true, library is optional, silently return nil if its not found
	--
	-- throws an error if the library can not be found (except silent is set)
	-- returns the library object if found
	function LibStub:GetLibrary(major, silent)
		if not self.libs[major] and not silent then
			error((""Cannot find a library instance of %q.""):format(tostring(major)), 2)
		end
		return self.libs[major], self.minors[major]
	end
	
	-- LibStub:IterateLibraries()
	-- 
	-- Returns an iterator for the currently registered libraries
	function LibStub:IterateLibraries() 
		return pairs(self.libs) 
	end
	
	setmetatable(LibStub, { __call = LibStub.GetLibrary })
end";
        private const string LibStubToc = "LibStub.toc";
        private const string LibStubTocContent = @"## Interface: 70000
## Title: Lib: LibStub
## Notes: Universal Library Stub
## Credits: Kaelten, Cladhaire, ckknight, Mikk, Ammo, Nevcairiel
## X-Website: http://www.wowace.com/addons/libstub/
## X-Category: Library
## X-License: Public Domain
## X-Curse-Packaged-Version: r105
## X-Curse-Project-Name: LibStub
## X-Curse-Project-ID: libstub
## X-Curse-Repository-ID: wow/libstub/mainline

LibStub.lua";
    }
}
        #endregion
/*
[AddonDetails.db]
AddonAuthor=Sorcerer
AddonName=Quartz
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,686,Shadow Bolt,NumPad1
Spell,157695,Demonbolt,NumPad1
Spell,104316,Call Dreadstalkers,NumPad2
Spell,105174,Hand of Guldan,NumPad3
Spell,193396,Demonic Empowerment,NumPad4
Spell,603,Doom,NumPad5
Spell,193440,Demonwrath,NumPad6
Spell,1454,Life Tap,NumPad7
Spell,205180,Darkglare,NumPad8
Spell,111897,Grimoire: Felguard,NumPad9
Spell,211714,Talkiels Consumption,Add
Spell,205181,Shadowflame,NumPad0
Spell,18540,Doomguard,Decimal
Spell,119914,Felstorm,D4
Spell,196098,Soul Harvest,D0
Spell,196277,Implosion,D7
Spell,30283,Shadowfury,D3
Spell,108416,Dark Pact,Multiply
Aura,2825,Bloodlust
Aura,32182,Heroism
Aura,80353,Time Warp
Aura,160452,Netherwinds
Aura,230935,Drums of War
Aura,603,Doom
Aura,193396,Demonic Empowerment
Aura,205146,Demonic Calling
Aura,205181,Shadowflame
Aura,127271,Mount
*/
