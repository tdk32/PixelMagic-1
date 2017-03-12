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
        private readonly Stopwatch BeastCleave = new Stopwatch();

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
            Log.Write("Use Scroll Lock key to toggle ST/AOE/CLEAVE auto detection", Color.Blue);
            Log.Write("If Scroll Lock LED is ON ST/AOE/CLEAVE auto detection is ENABLED", Color.Blue);
            Log.Write("If Scroll Lock LED is OFF ST/AOE/CLEAVE auto detection is DISABLED use the manual mode to select ST/AOE/CLEAVE (Default: ALT+S, ALT+A)", Color.Blue);
        }

        public override void Stop()
        {
        }

        public override void Pulse()        // Updated for Legion (tested and working for single target)
        {
            for (int i = 1; i < 3; i++)
            {
                Log.Write("Buff Time :" + PlayerBuffTimeRemaining("Bestial Wrath") + "Has buff :" + PlayerHasBuff("Bestial Wrath"));

                AddonCreationPulse();
                PlayerStats();
                AoEStuff();
                if (WoW.IsInCombat && !IsMounted)
                {
                    SelectRotation();
                }

                //Healthstone - Potion
                if ((WoW.CanCast("Healthstone") || WoW.CanCast("Potion"))
                    && (WoW.ItemCount("Healthstone") >= 1 || WoW.ItemCount("Potion") >= 1)
                    && (!WoW.ItemOnCooldown("Healthstone") || !WoW.ItemOnCooldown("Potion"))
                    && !PlayerHasBuff("Feign Death")
                    && WoW.HealthPercent <= 30
                    && !IsMounted
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
                    && !IsMounted
                    && !PlayerHasBuff("Feign Death")
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
                    && !IsMounted
                    && !PlayerHasBuff("Feign Death")
                    && WoW.HealthPercent != 0)
                {
                    WoW.CastSpell("Call Pet");
                    return;
                }

                //Revive Pet
                if ((!WoW.HasPet || WoW.PetHealthPercent < 1)
                    && !IsMounted
                    && !PlayerHasBuff("Feign Death")
                    && WoW.HealthPercent != 0)
                {
                    WoW.CastSpell("Heart of the Phoenix");
                    WoW.CastSpell("Revive Pet");
                    return;
                }

                //Voley
                if (WoW.CanCast("Voley")
                    && !PlayerHasBuff("Feign Death")
                    && !PlayerHasBuff("Voley")
                    && WoW.HealthPercent != 0
                    && CharInfo.T6 == 3)
                {
                    WoW.CastSpell("Voley");
                    return;
                }

                //Intimidation //Binding Shot
                if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_KEY_Z) < 0
                    && ((WoW.CanCast("Intimidation") && CharInfo.T5 == 3) || (WoW.CanCast("Binding Shot") && CharInfo.T5 == 1)))
                {
                    WoW.CastSpell("Binding Shot");
                    WoW.CastSpell("Intimidation");
                    return;
                }

                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !IsMounted && !WoW.PlayerIsChanneling && !PlayerHasBuff("Feign Death") && WoW.HealthPercent != 0)
                {
                    //Stampede
                    if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_LSHIFT) < 0
                        && WoW.CanCast("Stampede")
                        && CharInfo.T7 == 1)
                    {
                        WoW.CastSpell("Stampede");
                        return;
                    }

                    //Cooldowns
                    if (UseCooldowns)
                    {
                        //Aspect of the Wild
                        if (WoW.CanCast("Aspect of the Wild")
                            && !PlayerHasBuff("Aspect of the Turtle")
                            && PlayerHasBuff("Bestial Wrath")
                            && PlayerBuffTimeRemaining("Bestial Wrath") >= 1200)
                        {
                            WoW.CastSpell("Aspect of the Wild");
                        }
                    }

                    //Legendary Trinket
                    if (WoW.CanCast("Kil'jaeden's Burning Wish")
                        && (((SpellCooldownTimeRemaining("Bestial Wrath") > 1500 && SpellCooldownTimeRemaining("Dire Beast") > 500)) || PlayerHasBuff("Bestial Wrath"))
                        && !WoW.ItemOnCooldown("Kil'jaeden's Burning Wish")
                        && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Kil'jaeden's Burning Wish");
                    }

                    //SINGLE TARGET

                    //Bestial Wrath
                    if ((combatRoutine.Type == RotationType.SingleTarget || combatRoutine.Type == RotationType.SingleTargetCleave)
                        && WoW.CanCast("Bestial Wrath")
                        && !PlayerHasBuff("Aspect of the Turtle")
                        && WoW.IsSpellInRange("Cobra Shot")
                        && (WoW.Focus >= 105 || (WoW.Focus >= 90 && WoW.CanCast("Aspect of the Wild"))))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        WoW.CastSpell("Kill Command");
                    }

                    //A Murder of Crows
                    if (combatRoutine.Type == RotationType.SingleTarget
                        && SpellCooldownTimeRemaining("Bestial Wrath") > 2300
                        && WoW.CanCast("A Murder of Crows")
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T6 == 1
                        && WoW.Focus >= 30)
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }

                    //Kill Command
                    if (combatRoutine.Type == RotationType.SingleTarget
                        && (SpellCooldownTimeRemaining("Kill Command") <= 100 || WoW.CanCast("Kill Command"))
                        && WoW.Focus >= 100)
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }

                    // Dire beast
                    if (combatRoutine.Type == RotationType.SingleTarget
                        && WoW.CanCast("Dire Beast")
                        && SpellCooldownTimeRemaining("Bestial Wrath") > 300
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 != 2)
                    {
                        WoW.CastSpell("Dire Beast");
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }

                    //Dire Frenzy
                    if (combatRoutine.Type == RotationType.SingleTarget
                        && WoW.CanCast("Dire Frenzy")
                        && SpellCooldownTimeRemaining("Bestial Wrath") > 600
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 == 2)
                    {
                        WoW.CastSpell("Titan's Thunder");
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }

                    //Kill Command
                    if (combatRoutine.Type == RotationType.SingleTarget
                        && (SpellCooldownTimeRemaining("Kill Command") <= 100 || WoW.CanCast("Kill Command"))
                        && WoW.Focus >= 30)
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }

                    //Chimaera Shot
                    if (combatRoutine.Type == RotationType.SingleTarget
                        && WoW.CanCast("Chimaera Shot")
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 == 3
                        && WoW.Focus < 80)
                    {
                        WoW.CastSpell("Chimaera Shot");
                        return;
                    }

                    //Cobra Shot
                    if (combatRoutine.Type == RotationType.SingleTarget
                        && ((WoW.Focus >= 100) || (PlayerHasBuff("Bestial Wrath") && WoW.Focus >= 40))
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
                        && !PlayerHasBuff("Aspect of the Turtle"))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }

                    // Dire beast
                    if (combatRoutine.Type == RotationType.AOE
                        && WoW.CanCast("Dire Beast")
                        && SpellCooldownTimeRemaining("Bestial Wrath") > 300
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 != 2)
                    {
                        WoW.CastSpell("Dire Beast");
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }

                    //Dire Frenzy
                    if (combatRoutine.Type == RotationType.AOE
                        && WoW.CanCast("Dire Frenzy")
                        && SpellCooldownTimeRemaining("Bestial Wrath") > 600
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 == 2)
                    {
                        WoW.CastSpell("Dire Frenzy");
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }

                    //Barrage
                    if (combatRoutine.Type == RotationType.AOE
                        && WoW.CanCast("Barrage")
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T6 == 2
                        && (WoW.Focus >= 60))
                    {
                        WoW.CastSpell("Barrage");
                        return;
                    }

                    //Multishot
                    if (combatRoutine.Type == RotationType.AOE
                        && WoW.CanCast("Multi-Shot")
                        && WoW.IsSpellInRange("Multi-Shot")
                        && WoW.Focus >= 40)
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }

                    //Chimaera Shot
                    if (combatRoutine.Type == RotationType.AOE
                        && WoW.CanCast("Chimaera Shot")
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 == 3
                        && WoW.Focus < 80)
                    {
                        WoW.CastSpell("Chimaera Shot");
                        return;
                    }

                    //CLEAVE

                    //A Murder of Crows
                    if (combatRoutine.Type == RotationType.SingleTarget
                        && SpellCooldownTimeRemaining("Bestial Wrath") > 2300
                        && WoW.CanCast("A Murder of Crows")
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T6 == 1
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
                        && SpellCooldownTimeRemaining("Bestial Wrath") > 300
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 != 2)
                    {
                        WoW.CastSpell("Dire Beast");
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }

                    //Dire Frenzy
                    if (combatRoutine.Type == RotationType.SingleTargetCleave
                        && WoW.CanCast("Dire Frenzy")
                        && SpellCooldownTimeRemaining("Bestial Wrath") > 600
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 == 2)
                    {
                        WoW.CastSpell("Dire Frenzy");
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }

                    //Barrage
                    if (combatRoutine.Type == RotationType.SingleTargetCleave
                        && WoW.CanCast("Barrage")
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T6 == 2
                        && (WoW.Focus >= 60))
                    {
                        WoW.CastSpell("Barrage");
                        return;
                    }

                    //Kill Command
                    if (combatRoutine.Type == RotationType.SingleTargetCleave
                        && WoW.CanCast("Kill Command")
                        //&& (SpellCooldownTimeRemaining("Bestial Wrath") - SpellCooldownTimeRemaining("Kill Command") > 2 || (SpellCooldownTimeRemaining("Bestial Wrath") >= 23 && SpellCooldownTimeRemaining("Dire Beast") - SpellCooldownTimeRemaining("Kill Command") > 2))
                        && WoW.Focus >= 30)
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }

                    //Chimaera Shot
                    if (combatRoutine.Type == RotationType.SingleTargetCleave
                        && WoW.CanCast("Chimaera Shot")
                        && WoW.IsSpellInRange("Cobra Shot")
                        && CharInfo.T2 == 3
                        && WoW.Focus < 80)
                    {
                        WoW.CastSpell("Chimaera Shot");
                        return;
                    }

                    //Cobra Shot
                    if (combatRoutine.Type == RotationType.SingleTargetCleave
                        && ((WoW.Focus >= 90) || (PlayerHasBuff("Bestial Wrath") && (WoW.Focus >= 40)))
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
                        && !PlayerHasBuff("Feign Death"))
                    {
                        WoW.CastSpell("Mend Pet");
                        return;
                    }
                }
                //Thread.Sleep(25);
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
            pixelColor = WoW.GetBlockColor(1, 24);
            CharInfo.T1 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) * 100 / 255));
            CharInfo.T2 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
            CharInfo.T3 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B) * 100 / 255));
            pixelColor = WoW.GetBlockColor(2, 24);
            CharInfo.T4 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) * 100 / 255));
            CharInfo.T5 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
            CharInfo.T6 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B) * 100 / 255));
            pixelColor = WoW.GetBlockColor(3, 24);
            CharInfo.T7 = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) * 100 / 255));
            spec = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
            race = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.B) * 100 / 255));
            pixelColor = WoW.GetBlockColor(4, 24);
            CharInfo.Mana = (Convert.ToSingle(pixelColor.B) * 100 / 255);
            postive = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) / 255));
            if ((Convert.ToDouble(pixelColor.G) == 255))
                hastePct = 0f;
            else
                            if (postive == 1)
                hastePct = (Convert.ToSingle(pixelColor.G) * 100f / 255f);
            else
                hastePct = (Convert.ToSingle(pixelColor.G) * 100f / 255f) * (-1);
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
            pixelColor = WoW.GetBlockColor(11, 23);
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
                if(Control.IsKeyLocked(Keys.Scroll))
                {
                    if (npcCount >= 4 && !WoW.TargetIsPlayer)
                        combatRoutine.ChangeType(RotationType.AOE);
                    if ((npcCount == 2 || npcCount == 3) && !WoW.TargetIsPlayer)
                        combatRoutine.ChangeType(RotationType.SingleTargetCleave);
                    if ((npcCount <= 1))
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
                Thread.Sleep(350);
                WoW.Reload();
            }            
        }
        private void AddonEdit()
        {
            try
            {
                string addonlua = File.ReadAllText("" + WoW.AddonPath + "\\" + AddonName + "\\" + AddonName + ".lua");
                int start = addonlua.IndexOf("local function updateSpellCooldowns(self, event)");
                int end = addonlua.IndexOf("local lastItemCooldownState = {");
                addonlua = addonlua.Remove(start, end - start);
                start = addonlua.IndexOf("local function PlayerNotMove()");
                end = addonlua.IndexOf("local function AutoAtacking()");
                addonlua = addonlua.Remove(start, end - start);
                addonlua = addonlua.Insert(start, mounted);
                Log.Write("Addon Editing in progress");
                start = addonlua.IndexOf("local function updateMyBuffs(self, event)");
                end = addonlua.IndexOf("local function updateTargetDebuffs(self, event)");
                addonlua = addonlua.Remove(start, end - start);

                start = addonlua.IndexOf("local Party1Buffs = { }");
                end = addonlua.IndexOf("local function updateRaidHealth(self, event)");
                addonlua = addonlua.Remove(start, end - start);
                addonlua = addonlua.Insert(start, partyvarable);
                start = addonlua.IndexOf("local function updateParty1Buffs()");
                end = addonlua.IndexOf("local function updateRaidSize(self, event)");
                addonlua = addonlua.Remove(start, end - start);
                start = addonlua.IndexOf("raidHealthFrame[i]:SetScript(\"OnUpdate\", updateRaidHealth)");
                end = addonlua.IndexOf("local spellOverlayedFrames = {}");
                addonlua = addonlua.Remove(start, end - start);
                addonlua = addonlua.Insert(start, partybuff);
                start = addonlua.IndexOf("local function PlayerNotMove()");
                end = addonlua.IndexOf("local function AutoAtacking()");
                addonlua = addonlua.Remove(start, end - start);
                addonlua = addonlua.Insert(start, mounted);
                start = addonlua.IndexOf("local function updatePlayerIsCasting(self, event)");
                end = addonlua.IndexOf("local lastTargetCastID = 0");
                addonlua = addonlua.Remove(start, end - start);
                addonlua = addonlua.Insert(start, CastUpdate);

                addonlua = addonlua.Replace("local name, rank, icon, count, debuffType, duration, expirationTime, unitCaster, isStealable, shouldConsolidate, spellId", "local name, _, _, count, debuffType, duration, expirationTime, _, _, _, spellId");
                addonlua = addonlua.Replace("local name, rank, icon, count, dispelType, duration, expires, caster, isStealable, nameplateShowPersonal, spellID, canApplyAura, isBossDebuff, _, nameplateShowAll, timeMod, value1, value2, value3", "local name, _, _, count, _, duration, expires, _, _, _, spellID, _, _, _, _, _, _, _, _");
                addonlua = addonlua.Replace("local name, _, _, count, debuffType, duration, expirationTime, _, _, _, spellId, canApplyAura, isBossDebuff, value1, value2, value3", "local name, _, _, count, _, duration, expirationTime, _, _, _, spellId, _, _, _, _, _");
                addonlua = addonlua.Replace("if name == \"Wild Imps\"", "if (name == \"Wild Imps\" or name == \"Spirit Wolf\" or name == \"Totem Mastery\")");
                addonlua = addonlua.Replace("and(startTime + duration - GetTime() > 1.6) ", "and(startTime + duration - GetTime() > (1.5 / (1 + (GetHaste() / 100) ))) ");
                addonlua = addonlua.Replace("end" + Environment.NewLine + Environment.NewLine + "local function InitializeTwo()", Environment.NewLine);
                addonlua = addonlua.Replace("	--print (\"Initialising Spell Charges Frames\")", "end" + Environment.NewLine + "local function InitializeTwo()" + Environment.NewLine + "	--print (\"Initialising Spell Charges Frames\")" + Environment.NewLine);
                addonlua = addonlua.Replace("IsSpellInRange(name, \"target\")", "LibStub(\"SpellRange-1.0\").IsSpellInRange(name, \"target\")");
                // addonlua = addonlua.Replace("if (guid ~= lastTargetGUID) then", "");
                //ddonlua = addonlua.Replace("lastTargetGUID = guid" + Environment.NewLine + "	end", "print(\"target selected\")");
                addonlua = addonlua.Replace("local function InitializeOne()", Environment.NewLine + CustomLua + Environment.NewLine + "local function InitializeOne()");
                addonlua = addonlua.Replace("InitializeOne()" + Environment.NewLine + "            InitializeTwo()", "InitializeOne()" + Environment.NewLine + "            InitializeTwo()" + Environment.NewLine + "            InitializeFour()");
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


        public static bool IsMoving
        {
            get
            {
                var c = WoW.GetBlockColor(1, 7);
                return (c.R == Color.Red.R) && (c.B == Color.Blue.B);
            }
        }
        public static bool IsMounted
        {
            get
            {
                var c = WoW.GetBlockColor(1, 7);
                return (c.G == Color.Green.G && (c.B == Color.Blue.B));
            }
        }
        public static int SpellCooldownTimeRemaining(int spellNoInArrayOfSpells)
        {
            var c = WoW.GetBlockColor(spellNoInArrayOfSpells, 2);

            try
            {
                Log.WriteDirectlyToLogFile($"Green = {c.G} Blue = {c.B}");
                if (c.G == 255)
                    return 0;
                return Convert.ToInt32(Math.Round(Convert.ToSingle(c.G) * 10000 / 255)) + Convert.ToInt32(Math.Round(Convert.ToSingle(c.B) * 100 / 255));

            }
            catch (Exception ex)
            {
                Log.Write($"Red = {c.R} Green = {c.G}");
                Log.Write(ex.Message, Color.Red);
            }

            return 0;
        }
        public static int SpellCooldownTimeRemaining(string spellBookSpellName)
        {
            foreach (var spell in SpellBook.Spells)
            {
                if (spell.SpellName == spellBookSpellName)
                    return SpellCooldownTimeRemaining(spell.InternalSpellNo);
            }
            Log.Write($"[IsSpellOnCooldown] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
            return 0;
        }
        private static int PlayerBuffStacks(int auraNoInArrayOfAuras)
        {
            var c = WoW.GetBlockColor(auraNoInArrayOfAuras, 8);

            try
            {
                Log.WriteDirectlyToLogFile($"Green = {c.G}");
                if (c.R == 255)
                    return 0;

                // ReSharper disable once PossibleNullReferenceException
                return Convert.ToInt32(Math.Round(Convert.ToSingle(c.R) * 100 / 255));
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find buff stacks for color G = " + c.R, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }
        private static int PlayerBuffStacks(string auraName)
        {
            foreach (var aura in SpellBook.Auras)
            {
                if (aura.AuraName == auraName)
                    return PlayerBuffStacks(aura.InternalAuraNo);
            }
            Log.Write($"[PlayerBuffTimeRemaining] Unable to find buff with name '{auraName}' in Spell Book");
            return -1;
        }
        public static int PlayerBuffTimeRemaining(int auraNoInArrayOfAuras)
        {
            var c = WoW.GetBlockColor(auraNoInArrayOfAuras, 8);
            if (PlayerHasBuff(auraNoInArrayOfAuras) == false)
                return 0;
            try
            {
                // ReSharper disable once PossibleNullReferenceException
               Log.WriteDirectlyToLogFile($"Green = {c.G} Blue = {c.B}");
               return Convert.ToInt32(Math.Round(Convert.ToSingle(c.G) * 10000 / 255)) + Convert.ToInt32(Math.Round(Convert.ToSingle(c.B) * 100 / 255));
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find buff stacks for color G = " + c.B, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }
        public static int PlayerBuffTimeRemaining(string buffName)
        {
            foreach (var aura in SpellBook.Auras)
            {
                if (aura.AuraName == buffName)
                    return PlayerBuffTimeRemaining(aura.InternalAuraNo);
            }
            Log.Write($"[PlayerBuffTimeRemaining] Unable to find buff with name '{buffName}' in Spell Book");
            return -1;

        }
        public static bool PlayerHasBuff(int auraNoInArrayOfAuras)
        {
            var c = WoW.GetBlockColor(auraNoInArrayOfAuras, 8);
            return (c.R != 255);
        }

        public static bool PlayerHasBuff(string buffName)
        {
            foreach (var aura in SpellBook.Auras)
            {
                if (aura.AuraName == buffName)
                    return PlayerHasBuff(aura.InternalAuraNo);
            }
            Log.Write($"[PlayerHasBuff] Unable to find buff with name '{buffName}' in Spell Book");
            return false;
            
        }
        public static int TargetDebuffTimeRemaining(int auraNoInArrayOfAuras)
        {
            var c = WoW.GetBlockColor(auraNoInArrayOfAuras, 4);

            try
            {
                Log.WriteDirectlyToLogFile($"Green = {c.G}");
                if (c.G == 255)
                    return 0;
                return Convert.ToInt32(Math.Round(Convert.ToSingle(c.G) * 10000 / 255)) + Convert.ToInt32(Math.Round(Convert.ToSingle(c.B) * 100 / 255));
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find debuff target stacks for color G = " + c.B, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }
            return 0;
        }

        public static int TargetDebuffTimeRemaining(string debuffName)
        {
            foreach (var aura in SpellBook.Auras)
            {
                if (aura.AuraName == debuffName)
                    return TargetDebuffStacks(aura.InternalAuraNo);
            }
            Log.Write($"[TargetDebuffTimeRemaining] Unable to find buff with name '{debuffName}' in Spell Book");
            return -1;
        }
        public static int TargetDebuffStacks(int auraNoInArrayOfAuras)
        {
            var c = WoW.GetBlockColor(auraNoInArrayOfAuras, 4);

            try
            {
                Log.WriteDirectlyToLogFile($"Green = {c.G}");
                if (c.R == 255)
                    return 0;

                // ReSharper disable once PossibleNullReferenceException
                return Convert.ToInt32(Math.Round(Convert.ToSingle(c.R) * 100 / 255));
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find debuff stacks for color G = " + c.G, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        public static int TargetDebuffStacks(string debuffName)
        {
            foreach (var aura in SpellBook.Auras)
            {
                if (aura.AuraName == debuffName)
                    return TargetDebuffStacks(aura.InternalAuraNo);
            }
            Log.Write($"[TargetDebuffTimeRemaining] Unable to find buff with name '{debuffName}' in Spell Book");
            return -1;
        }


        public override Form SettingsForm { get; set; }

        /// <summary>
        /// Lua strings for varies functions above
        /// </summary>
        private const string CastUpdate = @"
        local function updatePlayerIsCasting(self, event)
		spell, _, _, _, startTime, endTime, _, castID, _ = UnitCastingInfo(""player"")
		name, _, _, _, _, _, _, _ = UnitChannelInfo(""player"")

				
			if castID ~= nil then
		
				if GetTime() + timeDiff <= endTime/1000  then
					--print(""Cast time :"", timeDiff, ""Time "", GetTime())
					--print(""Cast time :"", endTime/1000, ""Time "", GetTime()+ timeDiff)
			
					playerIsCastingFrame.t:SetColorTexture(1, 0, 0, alphaColor)
				else
					--print(""OffCast time :"", endTime/1000 - timeDiff, ""Time "", GetTime())
					playerIsCastingFrame.t:SetColorTexture(1, 1, 1, alphaColor)

                end
            end
		
			if castID == nil then

                playerIsCastingFrame.t:SetColorTexture(1, 1, 1, alphaColor)

            end	
		

		if name ~= nil then
			if text ~= lastChanneling then

            playerIsCastingFrame.t:SetColorTexture(0, 1, 0, alphaColor)
			--   print(text)

            lastChanneling = text
            end

		else
			if text ~= lastChanneling then

            playerIsCastingFrame.t:SetColorTexture(1, 1, 1, alphaColor)

            lastChanneling = text
            end


        end
    end
";
        private const string partyvarable = @"local sentTime =0 
local timeDiff =0
local PartyBuffs = {}
do
for i = 1, 4 do
PartyBuffs[i] ={Buffs = {} ,LastStateBuff = {} ,debuffs = {},LaststateDebuff = {} }
end
end
";
        private const string partybuff = @"	raidHealthFrame[i]:SetScript(""OnUpdate"", updateRaidHealth)
	end
	
	for z =1, 4  do
			i=0
		for _, buffId in pairs(buffs) do

			 PartyBuffs[z].Buffs[buffId] = CreateFrame(""frame"","""", parent)
			 PartyBuffs[z].Buffs[buffId]:SetSize(size, size)
	         PartyBuffs[z].Buffs[buffId]:SetPoint(""TOPLEFT"", i * size, -size * 11+z)                            -- column 13 [Target Buffs]
			 PartyBuffs[z].Buffs[buffId].t = PartyBuffs[z].Buffs[buffId]:CreateTexture()
	         PartyBuffs[z].Buffs[buffId].t:SetColorTexture(1, 1, 1, alphaColor)
		     PartyBuffs[z].Buffs[buffId].t:SetAllPoints(  PartyBuffs[z].Buffs[buffId])
			PartyBuffs[z].Buffs[buffId]:Show()
			PartyBuffs[z].Buffs[buffId]:SetScript(""OnUpdate"", updatePartyBuffs)
        i=i+1
		end
	end
    i=0
	for _, debuffId in pairs(debuffs) do
		for z=1, 4 do 
			PartyBuffs[z].debuffs[debuffId] = CreateFrame(""frame"","""", parent)
			PartyBuffs[z].debuffs[debuffId]:SetSize(size, size)
			PartyBuffs[z].debuffs[debuffId]:SetPoint(""TOPLEFT"", i * size, -size * 15+z)         -- row 4, column 1+ [Spell In Range]
			PartyBuffs[z].debuffs[debuffId].t = PartyBuffs[z].debuffs[debuffId]:CreateTexture()        
			PartyBuffs[z].debuffs[debuffId].t:SetColorTexture(1, 1, 1, alphaColor)
			PartyBuffs[z].debuffs[debuffId].t:SetAllPoints(PartyBuffs[z].debuffs[debuffId])
			PartyBuffs[z].debuffs[debuffId]:Show()		               
			PartyBuffs[z].debuffs[debuffId]:SetScript(""OnUpdate"", updatePartyDebuffs)
		end
        i=i+1
	end
end
";
        private const string mounted = @"local function PlayerNotMove()
	mountedplayer = 0
	moveTime = 1
	if IsMounted() then
		mountedplayer = .5
	end
	if GetUnitSpeed(""Player"") == 0 then
		moveTime = 0
	end
        PlayerMovingFrame.t:SetColorTexture(moveTime, mountedplayer, 1, alphaColor)
end
";
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
    [""Worgen""]= 0.13,
}
local Spec = {
	[250] = 0.01,
	[251] = 0.02,
	[252] =0.03,
	[577] = 0.04,
	[581] =0.05,
	[102] = 0.06,
	[103] =0.07,
	[104] =0.08,
	[105] =0.09,
	[253] = 0.10,
	[254] =0.11,
	[255] = 0.12,
	[62] =0.13,
	[63] = 0.14,
	[64] =0.15,
	[268] = 0.16,
	[269] =0.17,
	[270] = 0.18,
	[65] = 0.19,
	[66] = 0.20,
	[70] =0.21,
	[256] = 0.22,
	[257]=.23,
	[257] =0.24,
	[259] =0.25,
	[260] =0.26,
	[261] = 0.27,
	[262] = 0.28,
    [263] = 0.29,
	[264] = 0.30,
	[265] = 0.31,
	[71] = 0.32,
	[72] = 0.33,
	[73] = 0.34,
	[266] = 0.35,
    [267] = 0.36,
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
frame:RegisterEvent(""ACTIONBAR_UPDATE_STATE"")
frame:RegisterUnitEvent(""UNIT_SPELLCAST_START"",""player"")
frame:RegisterEvent(""CURRENT_SPELL_CAST_CHANGED"")
local function updateMyBuffs(self, event)
    
	for _, auraId in pairs(buffs) do
        local buff = ""UnitBuff"";
		local auraName = GetSpellInfo(auraId)
		
		if auraName == nil then
			if (lastBuffState[auraId] ~= ""BuffOff"") then
                buffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                buffFrames[auraId].t:SetAllPoints(false)
                lastBuffState[auraId] = ""BuffOff""
                --print(""["" .. buff.. ""] "" .. auraName.. "" Off"")
            end
			return
		end

        local name, _, _, count, debuffType, duration, expirationTime, _, _, _, spellId = UnitBuff(""player"", auraName)
		
		if (name == auraName) then -- We have Aura up and Aura ID is matching our list
                local getTime = GetTime()
                local remainingTime = 0
                if(expirationTime ~=0) then
                     remainingTime = expirationTime - getTime + 0.5
                end
                remainingTime = string.format(""%00.2f"", tostring(remainingTime))
			if (lastBuffState[auraId] ~= ""BuffOn"" .. count..remainingTime) then
              local red = count/100;
			    local green = tonumber(strsub(tostring(remainingTime), 1, 2)) / 100
                local blue = tonumber(strsub(tostring(remainingTime), -2, -1)) / 100
                --print(""expirationTime:""..expirationTime.."" remainingTime:"" .. remainingTime.. "" blue:"" .. blue.. "" strbluecount:"" ..  strbluecount)
                buffFrames[auraId].t:SetColorTexture(red, green, blue, alphaColor)

                buffFrames[auraId].t:SetAllPoints(false)
                --print(""["" .. buff.. ""] "" .. auraName.. "" "" .. count.. "" Green: "" .. green)
                lastBuffState[auraId] = ""BuffOn"" .. count
            end
        else
            if (lastBuffState[auraId] ~= ""BuffOff"") then
                buffFrames[auraId].t:SetColorTexture(1, 1, 1, alphaColor)
                buffFrames[auraId].t:SetAllPoints(false)
                lastBuffState[auraId] = ""BuffOff""
                --print(""["" .. buff.. ""] "" .. auraName.. "" Off"")
            end
        end
    end
end

local function updateFlag(self, event)
	if event == ""PLAYER_CONTROL_GAINED"" then
		flagFrame.t:SetColorTexture(0,0,0,alphaColor)
	end
	if event == ""PLAYER_CONTROL_LOST"" then
		flagFrame.t:SetColorTexture(1,0,0,alphaColor)
	end
end
local function updateSpellCooldowns(self, event) 
    for _, spellId in pairs(cooldowns) do
		-- start is the value of GetTime() at the point the spell began cooling down
		-- duration is the total duration of the cooldown, NOT the remaining
		local start, duration, _ = GetSpellCooldown(spellId)
        --print("" "" .. spellId .. "" is currently active, use it and wait "" .. duration .. "" seconds for the next one."")
        local cooldownLeft = (start + duration - GetTime())
        local remainingTime = timeDiff > select(4, GetNetStats())/ 1000  and cooldownLeft - select(4, GetNetStats())/ 1000  or cooldownLeft - timeDiff
        if remainingTime < 0 then
			 remainingTime = 0
		 end
        
		if remainingTime ~= 0 then -- the spell is not ready to be cast
            	--print(""Spell with Id = "" .. spellId .. "" is on CD"")
                --print("" "" ..spellId.. "" remaining time: "" ..math.floor(remainingTime)..  "" "")
				remainingTime = string.format(""%00.3f"",tostring(remainingTime) )
				local green = tonumber(strsub(tostring(remainingTime), 1, 2))/100
				local blue = tonumber(strsub( tostring(remainingTime), -3,-2))/100
				cooldownframes[spellId].t:SetColorTexture(1, green, blue, alphaColor)				
				cooldownframes[spellId].t:SetAllPoints(false)
		else
				cooldownframes[spellId].t:SetColorTexture(0, 1, 1, alphaColor)
				cooldownframes[spellId].t:SetAllPoints(false)
		end						
	end
end
local function HasteInfoUpdate()
	local ratingBonus = math.floor(GetHaste())
	if lasthaste == ratingBonus then return end
	lastehaste = ratingBonus
    if ratingBonus == math.abs(ratingBonus) then
		hasteInfo[2] = 1
	else
        hasteInfo[2] = 0
    end
	hasteInfo[1] = tonumber(""0.0"".. math.abs(ratingBonus))
	if (math.abs(ratingBonus) >= 10) then
		hasteInfo[1] = tonumber(""0."".. math.abs(ratingBonus))
	end

end
local function UpdateMana()
		charUnit[1] = UnitPower(""player"",0) / UnitPowerMax(""player"",0)
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
    local specialsz =select(1, GetSpecializationInfo(GetSpecialization()))
	charUnit[2] = Race[UnitRace(""player"")]
	charUnit[3] = Spec[specialsz]
 
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
local function InitializeFour()
	for i = 1, 4 do 
		raidBuff[i] = 1
		raidBufftime[i] =1
	end
		--print (""Initialising raid Health Frames"")
	for i = 1, 20 do	
		raidHealthFrame[i] = CreateFrame(""frame"", """", parent)
		raidHealthFrame[i]:SetSize(size, size)
		raidHealthFrame[i]:SetPoint(""TOPLEFT"", size*(i-1), -size *21 )   --  row 1-20,  column 19
		raidHealthFrame[i].t = raidHealthFrame[i]:CreateTexture()        
		raidHealthFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		raidHealthFrame[i].t:SetAllPoints(raidHealthFrame[i])
		raidHealthFrame[i]:Show()
	end
	for i = 21, 30 do		
		raidHealthFrame[i] = CreateFrame(""frame"", """", parent)
		raidHealthFrame[i]:SetSize(size, size)
		raidHealthFrame[i]:SetPoint(""TOPLEFT"", size*(i-21), -size *22 )   --  row 1-10,  column 20
		raidHealthFrame[i].t = raidHealthFrame[i]:CreateTexture()        
		raidHealthFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		raidHealthFrame[i].t:SetAllPoints(raidHealthFrame[i])
		raidHealthFrame[i]:Show()
	end
		raidSizeFrame = CreateFrame(""frame"", """", parent)
		raidSizeFrame:SetSize(size, size)
		raidSizeFrame:SetPoint(""TOPLEFT"", size*(10), -size *22 )   --  row 11,  column 20
		raidSizeFrame.t = raidSizeFrame:CreateTexture()        
		raidSizeFrame.t:SetColorTexture(1, 1, 1, alphaColor)
		raidSizeFrame.t:SetAllPoints(raidSizeFrame)
		raidSizeFrame:Show()
		raidSizeFrame:SetScript(""OnUpdate"",NameplateFrameUPDATE)
		
	for i = 1, 4 do		
		RaidBuffFrame[i] = CreateFrame(""frame"", """", parent)
		RaidBuffFrame[i]:SetSize(size, size)
		RaidBuffFrame[i]:SetPoint(""TOPLEFT"", size*(10 + i), -size *22 )   --  row 12-15,  column 20
		RaidBuffFrame[i].t = RaidBuffFrame[i]:CreateTexture()        
		RaidBuffFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		RaidBuffFrame[i].t:SetAllPoints(RaidBuffFrame[i])
		RaidBuffFrame[i]:Show()
		
	end
	for i = 1, 5 do
		PlayerStatFrame[i] = CreateFrame(""frame"", """", parent)
		PlayerStatFrame[i]:SetSize(size, size)
		PlayerStatFrame[i]:SetPoint(""TOPLEFT"", size*(i-1), -size *23 )   --  row 1-4,  column 21
		PlayerStatFrame[i].t =PlayerStatFrame[i]:CreateTexture()        
		PlayerStatFrame[i].t:SetColorTexture(1, 1, 1, alphaColor)
		PlayerStatFrame[i].t:SetAllPoints(PlayerStatFrame[i])
		PlayerStatFrame[i]:Show()
	end
		timerDBMFrames = CreateFrame(""frame"", """", parent)
		timerDBMFrames:SetSize(size, size);
		timerDBMFrames:SetPoint(""TOPLEFT"", size * 5, -(size * 23))           -- column 6 row 21
		timerDBMFrames.t = timerDBMFrames:CreateTexture()        
		timerDBMFrames.t:SetColorTexture(0, 0, 0, alphaColor)
		timerDBMFrames.t:SetAllPoints(timerDBMFrames)
		timerDBMFrames:Show()	


		totemsFrame = CreateFrame(""frame"", """", parent)
		totemsFrame:SetSize(size, size);
		totemsFrame:SetPoint(""TOPLEFT"", size * 6, -(size * 23))           -- column 7 row 21
		totemsFrame.t = totemsFrame:CreateTexture()        
		totemsFrame.t:SetColorTexture(0, 0, 0, alphaColor)
		totemsFrame.t:SetAllPoints(totemsFrame)
		totemsFrame:Show()

		targetInfoFrame = CreateFrame(""frame"", """", parent)
		targetInfoFrame:SetSize(size, size);
		targetInfoFrame:SetPoint(""TOPLEFT"", size * 7, -(size * 23))           -- column 8 row 21
		targetInfoFrame.t = targetInfoFrame:CreateTexture()        
		targetInfoFrame.t:SetColorTexture(0, 0, 0, alphaColor)
		targetInfoFrame.t:SetAllPoints(targetInfoFrame)
		targetInfoFrame:Show()

		setBonusFrame = CreateFrame(""frame"", """", parent)
		setBonusFrame:SetSize(size, size);
		setBonusFrame:SetPoint(""TOPLEFT"", size * 8, -(size * 23))           -- column 9 row 21
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

    if event == ""UNIT_SPELLCAST_START"" then
		    timeDiff = GetTime() - sendTime
    end

	if event == ""CURRENT_SPELL_CAST_CHANGED"" then
        sendTime = GetTime()
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
        HasteInfoUpdate()
		Talents()
		TurnOnPlates()
		updateSetBonus()
	end
	if event == ""PLAYER_REGEN_DISABLED""then 
        HasteInfoUpdate()
		ClearNamePlates()
		CharRaceUpdate()
		Talents()
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
    if event == ""ACTIONBAR_UPDATE_STATE"" then
        updateSpellCharges()
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
