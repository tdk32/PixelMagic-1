// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class RetributionPaladin : CombatRoutine
    {
        private static readonly string AddonName = ConfigFile.ReadValue("PixelMagic", "AddonName");
        private static readonly string AddonEmbedName = "BossLib.xml"; // Initialization variables		
        private static readonly string LuaAddon = "Shaman.lua";
        private readonly Stopwatch BeastCleave = new Stopwatch();

        private bool AddonEdited;
        private bool AddonEmbeded;
        private char_data CharInfo;
        private double hastePct;
        private bool Nameplates;
        private int npcCount, players;
        private Color pixelColor = Color.FromArgb(0);

        public string[] Race = {"None", "Human", "Dwarf", "NightElf", "Gnome", "Dreanei", "Pandaren", "Orc", "Undead", "Tauren", "Troll", "BloodElf", "Goblin", "none"};
        private bool RangeLib;

        public string[] Spec =
        {
            "None", "Blood", "Frost", "Unholy", "Havoc", "Vengeance", "Balance", "Feral", "Guardian", "Restoration", "Beast Mastery", "Marksmanship", "Survival", "Arcane",
            "Fire", "Frost", "Brewmaster", "Mistweaver", "Windwalker", "Holy", "Protection", "Retribution", "Discipline", "HolyPriest", "Shadow", "Assassination", "Outlaw", "Subtlety",
            "Elemental", "Enhancement", "RestorationShaman", "Affliction", "Arms", "Fury", "Protection", "none"
        };

        public override string Name
        {
            get { return "Retribution Paladin"; }
        }

        public override string Class
        {
            get { return "Paladin"; }
        }

        public static string CustomLua
        {
            get
            {
                var customLua = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + LuaAddon);
                return customLua;
            }
        }

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Retribution Paladin", Color.Green);
            Log.Write("IN ORDER FOR THIS ROTATION TO WORK YOU WILL NEED TO DOWNLOAD AND INSTALL THE ADDON.", Color.Red);
            Log.Write("https://github.com/winifix/PixelMagicCR/tree/master/Hunter", Color.Blue);
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

            //Healthstone - Potion
            if ((WoW.CanCast("Healthstone") || WoW.CanCast("Potion")) && (WoW.ItemCount("Healthstone") >= 1 || WoW.ItemCount("Potion") >= 1) &&
                (!WoW.ItemOnCooldown("Healthstone") || !WoW.ItemOnCooldown("Potion")) && WoW.HealthPercent <= 30 && !WoW.PlayerHasBuff("Mount"))
            {
                WoW.CastSpell("Healthstone");
                WoW.CastSpell("Potion");
                return;
            }

            //Shield of Vengeance
            if (WoW.CanCast("Shield of Vengeance") && WoW.HealthPercent <= 40 && !WoW.PlayerHasBuff("Mount"))
            {
                WoW.CastSpell("Shield of Vengeance");
                return;
            }

            //Lay on Hands
            if (WoW.CanCast("Lay on Hands") && WoW.HealthPercent <= 20 && !WoW.PlayerHasBuff("Mount"))
            {
                WoW.CastSpell("Lay on Hands");
                return;
            }

            //Divine Steed
            if (DetectKeyPress.GetKeyState(DetectKeyPress.Num4) < 0 && WoW.CanCast("Divine Steed") && !WoW.PlayerHasBuff("Divine Steed"))
            {
                WoW.CastSpell("Divine Steed");
                return;
            }

            if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerHasBuff("Mount") && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && WoW.HealthPercent != 0)
            {
                //Crusade
                if (WoW.CanCast("Crusade") && CharInfo.T7 == 2 && WoW.CurrentHolyPower >= 3 && WoW.IsSpellInRange("Templar Verdict") &&
                    (WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Time Warp") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums of War")))
                {
                    WoW.CastSpell("Crusade");
                    return;
                }

                //Avenging Wrath
                if (WoW.CanCast("Avenging Wrath") && WoW.CurrentHolyPower >= 3 && WoW.IsSpellInRange("Templar Verdict") &&
                    (WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Time Warp") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums of War")))
                {
                    WoW.CastSpell("Avenging Wrath");
                    return;
                }

                //Hammer of Justice
                if (DetectKeyPress.GetKeyState(DetectKeyPress.NumpadADD) < 0 && WoW.CanCast("Hammer of Justice"))
                {
                    WoW.CastSpell("Hammer of Justice");
                    return;
                }

                //Holy Wrath
                if (WoW.CanCast("Holy Wrath") && CharInfo.T7 == 3 && WoW.HealthPercent <= 40 && WoW.IsSpellInRange("Templar Verdict"))
                {
                    WoW.CastSpell("Holy Wrath");
                    return;
                }

                //Single Target Rotation

                //Execution Sentence
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CanCast("Execution Sentence") && WoW.TargetHasDebuff("Judgement") &&
                    WoW.TargetDebuffTimeRemaining("Judgement") >= 6.5 && CharInfo.T1 == 2)
                {
                    WoW.CastSpell("Execution Sentence");
                    return;
                }

                //Justicar's Vengeance
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CanCast("Justicars Vengeance") && WoW.PlayerHasBuff("Divine Purpose") && WoW.IsSpellInRange("Templar Verdict") &&
                    CharInfo.T5 == 1)
                {
                    WoW.CastSpell("Justicars Vengeance");
                    return;
                }

                //Templar's Verdict
                if (combatRoutine.Type == RotationType.SingleTarget &&
                    (WoW.CurrentHolyPower >= 3 || WoW.PlayerHasBuff("Divine Purpose") || (WoW.CurrentHolyPower >= 2 && WoW.PlayerHasBuff("The Fires of Justice"))) &&
                    WoW.CanCast("Templar Verdict") && WoW.IsSpellInRange("Templar Verdict") && WoW.TargetHasDebuff("Judgement") && WoW.TargetDebuffTimeRemaining("Judgement") >= 0.5)
                {
                    WoW.CastSpell("Templar Verdict");
                    return;
                }

                //Judgement
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CanCast("Judgement") && WoW.CurrentHolyPower >= 3)
                {
                    WoW.CastSpell("Judgement");
                    return;
                }

                //Wake of Ashes
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CurrentHolyPower == 0 && WoW.CanCast("Wake of Ashes") && WoW.IsSpellInRange("Templar Verdict"))
                {
                    WoW.CastSpell("Wake of Ashes");
                    return;
                }

                //Blade of Justice
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CanCast("Blade of Justice") && WoW.CurrentHolyPower <= 3 && CharInfo.T4 != 3)
                {
                    WoW.CastSpell("Blade of Justice");
                    return;
                }

                //Divine Hammer
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CanCast("Divine Hammer") && WoW.CurrentHolyPower <= 3 && CharInfo.T4 == 3)
                {
                    WoW.CastSpell("Divine Hammer");
                    return;
                }

                //Crusader Strike
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CurrentHolyPower < 5 && WoW.PlayerSpellCharges("Crusader Strike") >= 1 && WoW.CanCast("Crusader Strike") &&
                    CharInfo.T2 != 2)
                {
                    WoW.CastSpell("Crusader Strike");
                    return;
                }

                //Zeal
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CurrentHolyPower < 5 && WoW.PlayerSpellCharges("Zeal") >= 1 && WoW.CanCast("Zeal") && CharInfo.T2 == 2)
                {
                    WoW.CastSpell("Zeal");
                    return;
                }

                //Consecration
                if (combatRoutine.Type == RotationType.SingleTarget && WoW.CanCast("Consecration") && WoW.IsSpellInRange("Templar Verdict") && CharInfo.T1 == 3)
                {
                    WoW.CastSpell("Consecration");
                    return;
                }

                //AoE Rotation = 3+ Targets

                //Divine Storm
                if (combatRoutine.Type == RotationType.AOE &&
                    (WoW.CurrentHolyPower >= 3 || WoW.PlayerHasBuff("Divine Purpose") || (WoW.CurrentHolyPower >= 2 && WoW.PlayerHasBuff("The Fires of Justice"))) &&
                    WoW.CanCast("Divine Storm") && WoW.IsSpellInRange("Templar Verdict"))
                {
                    WoW.CastSpell("Divine Storm");
                    return;
                }

                //Judgement
                if (combatRoutine.Type == RotationType.AOE && WoW.CanCast("Judgement"))
                {
                    WoW.CastSpell("Judgement");
                    return;
                }

                //Wake of Ashes
                if (combatRoutine.Type == RotationType.AOE && WoW.CurrentHolyPower == 0 && WoW.CanCast("Wake of Ashes") && WoW.IsSpellInRange("Templar Verdict"))
                {
                    WoW.CastSpell("Wake of Ashes");
                    return;
                }

                //Consecration
                if (combatRoutine.Type == RotationType.AOE && WoW.CanCast("Consecration") && WoW.IsSpellInRange("Templar Verdict") && CharInfo.T1 == 3)
                {
                    WoW.CastSpell("Consecration");
                    return;
                }

                //Blade of Justice
                if (combatRoutine.Type == RotationType.AOE && WoW.CanCast("Blade of Justice") && WoW.CurrentHolyPower <= 3 && CharInfo.T4 != 3)
                {
                    WoW.CastSpell("Blade of Justice");
                    return;
                }

                //Divine Hammer
                if (combatRoutine.Type == RotationType.AOE && WoW.CanCast("Divine Hammer") && WoW.CurrentHolyPower <= 3 && CharInfo.T4 == 3)
                {
                    WoW.CastSpell("Divine Hammer");
                    return;
                }

                //Crusader Strike
                if (combatRoutine.Type == RotationType.AOE && WoW.CurrentHolyPower < 5 && WoW.PlayerSpellCharges("Crusader Strike") >= 1 && WoW.CanCast("Crusader Strike") && CharInfo.T2 != 2)
                {
                    WoW.CastSpell("Crusader Strike");
                    return;
                }

                //Zeal
                if (combatRoutine.Type == RotationType.AOE && WoW.CurrentHolyPower < 5 && WoW.PlayerSpellCharges("Zeal") >= 1 && WoW.CanCast("Zeal") && CharInfo.T2 == 2)
                {
                    WoW.CastSpell("Zeal");
                    return;
                }


                if (combatRoutine.Type == RotationType.SingleTargetCleave) //Cleave rotation = 2 targets
                {
                    // Do Single Target Cleave stuff here if applicable else ignore this one
                }
            }
        }

        private void PlayerStats()
        {
            // Playerstats start at row 1,  column 21
            // t1 t2 t3
            // t4 t5 t7
            // t7 +-haste hastePCT
            // Spec, Mana, Race
            var postive = 0;
            if (Convert.ToDouble(pixelColor.R) == 255)
                hastePct = 0f;
            else
                hastePct = Convert.ToSingle(pixelColor.R)*100f/255f;
            int spec, race;
            pixelColor = WoW.GetBlockColor(1, 21);
            CharInfo.T1 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R)*100/255));
            CharInfo.T2 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G)*100/255));
            CharInfo.T3 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B)*100/255));
            pixelColor = WoW.GetBlockColor(2, 21);
            CharInfo.T4 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R)*100/255));
            CharInfo.T5 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G)*100/255));
            CharInfo.T6 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B)*100/255));
            pixelColor = WoW.GetBlockColor(3, 21);
            CharInfo.T7 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R)*100/255));
            spec = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G)*100/255));
            race = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B)*100/255));
            pixelColor = WoW.GetBlockColor(4, 21);
            CharInfo.Mana = Convert.ToSingle(pixelColor.B)*100/255;
            postive = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G)/255));
            if (Convert.ToDouble(pixelColor.B) == 255)
                hastePct = 0f;
            else if (postive == 1)
                hastePct = Convert.ToSingle(pixelColor.R)*100f/255f*-1;
            else
                hastePct = Convert.ToSingle(pixelColor.G)*100f/255f;
            if (race > 13)
                race = 0;
            if (spec > 34)
                spec = 0;
            //Log.Write ("Char Race :" + race + " Spec : " + spec);
            CharInfo.Race = Race[race];
            CharInfo.Spec = Spec[spec];
            Log.Write(" T1 " + CharInfo.T1 + " T2 " + CharInfo.T2 + " T3 " + CharInfo.T3 + " T4 " + CharInfo.T4 + " T5 " + CharInfo.T5 + " T6 " + CharInfo.T6 + " T7 " + CharInfo.T7);
            //Log.Write("Char Haste " + hastePct + " Mana :" + CharInfo.Mana + " Race : " +CharInfo.Race + " Spec : "  +CharInfo.Spec ) ;
        }

        private void AoEStuff()
        {
            var pixelColor = Color.FromArgb(0);
            pixelColor = WoW.GetBlockColor(11, 20);
            npcCount = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G)*100/255));
            if (Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B)/255)) == 1)
                Nameplates = true;
            else
                Nameplates = false;
        }

        private void SelectRotation()
        {
            if (Nameplates)
            {
                if (npcCount >= 3 && !WoW.TargetIsPlayer)
                    combatRoutine.ChangeType(RotationType.AOE);
                /* if ((npcCount == 0 || npcCount == 3) && !WoW.TargetIsPlayer)
                    combatRoutine.ChangeType(RotationType.SingleTargetCleave);*/
                if (npcCount <= 2)
                    combatRoutine.ChangeType(RotationType.SingleTarget);
            }
        }

        public void RangeLibCopy()
        {
            try
            {
                var fileName = "text.txt";
                var sourcePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory + "LibSpellRange-1.0\\");
                var sourcePathSub = string.Concat(AppDomain.CurrentDomain.BaseDirectory + "LibSpellRange-1.0\\lib\\LibStub\\");
                var targetPath = string.Concat("" + WoW.AddonPath + "\\" + AddonName + "\\lib\\LibSpellRange-1.0\\");
                var targetPathSub = string.Concat("" + WoW.AddonPath + "\\" + AddonName + "\\lib\\LibSpellRange-1.0\\lib\\LibStub\\");
                var destFile = "text.txt";

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
                if (!Directory.Exists(sourcePath))
                    Log.Write("Dirctory doesn't exist:" + sourcePath);
                if (!Directory.Exists(sourcePathSub))
                    Log.Write("Dirctory doesn't exist:" + sourcePathSub);
                if (Directory.Exists(sourcePath))
                {
                    var files = Directory.GetFiles(sourcePath);
                    foreach (var s in files)
                    {
                        Log.Write("Generating file" + s);
                        fileName = Path.GetFileName(s);
                        destFile = Path.Combine(targetPath, fileName);
                        File.Copy(s, destFile, true);
                    }
                }
                if (Directory.Exists(sourcePathSub))
                {
                    var files = Directory.GetFiles(sourcePathSub);

                    foreach (var s in files)
                    {
                        Log.Write("Generating Sub file" + s);
                        fileName = Path.GetFileName(s);
                        destFile = Path.Combine(targetPathSub, fileName);
                        File.Copy(s, destFile, true);
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
                    var addonlua =
                        " < Ui xmlns = \"http://www.blizzard.com/wow/ui/\" xmlns: xsi = \"http://www.w3.org/2001/XMLSchema-instance \" xsi: schemaLocation = \"http://www.blizzard.com/wow/ui/ ..\\FrameXML\\UI.xsd\" >" +
                        Environment.NewLine + "< Script file = \"lib\\LibSpellRange-1.0\\LibSpellRange-1.0.lua\" />" + Environment.NewLine + "</ Ui >" + Environment.NewLine;
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
                WoW.SendMacro("/reload");
            }
            Thread.Sleep(350);
        }

        private void AddonEdit()
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + LuaAddon))
                {
                    var addonlua = File.ReadAllText("" + WoW.AddonPath + "\\" + AddonName + "\\" + AddonName + ".lua");
                    Log.Write("Addon Editing in progress");
                    addonlua = addonlua.Replace("if name == \"Wild Imps\"", "if (name == \"Wild Imps\" or name == \"Spirit Wolf\" or name == \"Totem Mastery\")");
                    addonlua = addonlua.Replace("and(startTime + duration - GetTime() > 1.6) ", "and(startTime + duration - GetTime() > (1.5 / (1 + (GetHaste() / 100) ))) ");
                    addonlua = addonlua.Replace("end" + Environment.NewLine + Environment.NewLine + "local function InitializeTwo()", Environment.NewLine);
                    addonlua = addonlua.Replace("	--print (\"Initialising Spell Charges Frames\")",
                        "end" + Environment.NewLine + "local function InitializeTwo()" + Environment.NewLine + "	--print (\"Initialising Spell Charges Frames\")" + Environment.NewLine);
                    addonlua = addonlua.Replace("IsSpellInRange(name, \"target\")", "LibStub(\"SpellRange-1.0\").IsSpellInRange(name, \"target\")");
                    addonlua = addonlua.Replace("local function InitializeOne()", Environment.NewLine + CustomLua + Environment.NewLine + "local function InitializeOne()");
                    addonlua = addonlua.Replace("InitializeOne()" + Environment.NewLine + "            InitializeTwo()",
                        "InitializeOne()" + Environment.NewLine + "            InitializeTwo()" + Environment.NewLine + "            InitializeThree()");
                    addonlua = addonlua.Replace("healthFrame:SetScript(\"OnUpdate\", updateHealth)", "");
                    addonlua = addonlua.Replace("powerFrame: SetScript(\"OnUpdate\", updatePower)", "");
                    addonlua = addonlua.Replace("	targetHealthFrame: SetScript(\"OnUpdate\", updateTargetHealth) ", "");
                    addonlua = addonlua.Replace("unitCombatFrame: SetScript(\"OnUpdate\", updateCombat)", "");
                    addonlua = addonlua.Replace("unitPowerFrame:SetScript(\"OnUpdate\", updateUnitPower)", "");
                    addonlua = addonlua.Replace("isTargetFriendlyFrame: SetScript(\"OnUpdate\", updateIsFriendly)", "");
                    addonlua = addonlua.Replace("playerIsCastingFrame: SetScript(\"OnUpdate\", updatePlayerIsCasting)", "");
                    addonlua = addonlua.Replace("hasTargetFrame: SetScript(\"OnUpdate\", hasTarget)", "");
                    addonlua = addonlua.Replace("targetIsCastingFrame: SetScript(\"OnUpdate\", updateTargetIsCasting)", "");
                    addonlua = addonlua.Replace("hasteFrame: SetScript(\"OnUpdate\", updateHaste)", "");
                    addonlua = addonlua.Replace("unitIsVisibleFrame: SetScript(\"OnUpdate\", updateUnitIsVisible)", "");
                    addonlua = addonlua.Replace("petHealthFrame: SetScript(\"OnUpdate\", updatePetHealth)", "");
                    addonlua = addonlua.Replace("wildPetsFrame: SetScript(\"OnUpdate\", updateWildPetsFrame)", "");
                    addonlua = addonlua.Replace("cooldownframes[spellId]:SetScript(\"OnUpdate\", updateSpellCooldowns)", "");
                    addonlua = addonlua.Replace("spellInRangeFrames[spellId]:SetScript(\"OnUpdate\", updateSpellInRangeFrames)", "");
                    addonlua = addonlua.Replace("targetDebuffFrames[debuffId]:SetScript(\"OnUpdate\", updateTargetDebuffs)", "");
                    addonlua = addonlua.Replace("updateSpellChargesFrame[spellId]:SetScript(\"OnUpdate\", updateSpellCharges)", "");
                    addonlua = addonlua.Replace("TargetBuffs[buffId]:SetScript(\"OnUpdate\", updateTargetBuffs)", "");
                    addonlua = addonlua.Replace("PlayerMovingFrame: SetScript(\"OnUpdate\", PlayerNotMove)", "");
                    addonlua = addonlua.Replace("AutoAtackingFrame: SetScript(\"OnUpdate\", AutoAtacking)", "");
                    addonlua = addonlua.Replace("targetIsPlayerFrame: SetScript(\"OnUpdate\", updateIsPlayer)", "");
                    addonlua = addonlua.Replace("flagFrame: SetScript(\"OnUpdate\", updateFlag)", "");
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
                else
                    Log.Write("Adddon failed to load, Pleaes check " + LuaAddon + " Is in PM.EXE directory,Rotation will not function with out it");
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Log.Write("Editing Addon Complete");
        }


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

        public class DetectKeyPress
        {
            public static int Num1 = 0x31;
            public static int Num2 = 0x32;
            public static int Num3 = 0x33;
            public static int Num4 = 0x34;
            public static int Num5 = 0x35;
            public static int Num6 = 0x36;
            public static int Numpad0 = 0x60;
            public static int Numpad1 = 0x61;
            public static int Numpad2 = 0x62;
            public static int Numpad3 = 0x63;
            public static int Numpad4 = 0x64;
            public static int Numpad5 = 0x65;
            public static int Numpad6 = 0x66;
            public static int Numpad7 = 0x67;
            public static int Numpad8 = 0x68;
            public static int Numpad9 = 0x69;
            public static int NumpadDot = 0x6E;
            public static int NumpadADD = 0x6B;

            public static int Shift = 0x10;
            public static int Ctrl = 0x11;
            public static int Alt = 0x12;

            public static int Z = 0x5A;
            public static int X = 0x58;
            public static int C = 0x43;
            public static int V = 0x56;
            public static int Slash = 0xDC;

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern short GetKeyState(int virtualKeyCode);
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Sorcerer
AddonName=Quartz
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,217020,Zeal,NumPad1
Spell,215661,Justicars Vengeance,D8
Spell,184662,Shield of Vengeance,NumPad0
Spell,853,Hammer of Justice,OemMinus
Spell,213757,Execution Sentence,D9
Spell,633,Lay on Hands,D6
Spell,205273,Wake of Ashes,NumPad5
Spell,53385,Divine Storm,NumPad6
Spell,184575,Blade of Justice,NumPad4
Spell,198034,Divine Hammer,NumPad4
Spell,35395,Crusader Strike,NumPad1
Spell,85256,Templar Verdict,NumPad2
Spell,20271,Judgement,NumPad3
Spell,224668,Crusade,Subtract
Spell,31884,Avenging Wrath,Subtract
Spell,19750,Flash of Light,D1
Spell,210220,Holy Wrath,D8
Spell,205228,Consecration,D8
Spell,5512,Healthstone,D1
Spell,127834,Potion,D1
Spell,190784,Divine Steed,D8
Aura,20271,Judgement
Aura,223819,Divine Purpose
Aura,209785,The Fires of Justice
Aura,2825,Bloodlust
Aura,80353,Time Warp
Aura,160452,Netherwinds
Aura,230935,Drums of War
Aura,127271,Mount
Aura,25771,Forbearance
Aura,190784,Divine Steed
Item,5512,Healthstone
Item,127834,Potion
*/