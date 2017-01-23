// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

/*

Guardian Rotation BETA version 0.2

Change log:

0.1 
Initial Release

0.1.1
API Updates - Better active mitigation and healing management/prioritization

0.1.2
Tab aggroing logic improved
Added chain pulling logic

0.1.3
Improved interrupt logic

0.1.4
Added custom health rate of change trigger and interrupt delay

0.1.5
Improved tab-aggroing logic, now will search for in-melee-range mobs every second to switch in an asychronous way
Improved sliders for Interrupt and Frenzied Regeneration
Annotated the code

0.1.6
Added bearcatting - seeing a 30% increase in DPS compared to bear single target rotation

0.1.7
Refined rotation
Improved Wild Charge logic

0.1.8
Added more healing flexibility and Frenzied Regen timer

0.2
Added buttons for CoolDown use and DPS burst

0.2.1
Fine tuned rotation

0.2.2
Issues - DPS Burst and Cooldowns should work for all talent option choices 

To do:

Add spellids to be interrupted and functionality to manually add them
Improve rotation logic to be tuned for more talent choices
Add custom DPS and CD keys
Port to 308
Get more stuff to do!


*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using PixelMagic.Helpers;
using Timer = System.Timers.Timer;

#pragma warning disable 414

namespace PixelMagic.Rotation
{
    public class Guardian : CombatRoutine
    {
        // Variables for HP timer
        private static int PreviousHP;
        private static int CurrentHP;

        // Variables for bearcatting
        private static int bearstep;
        private static int incarnatestep;
        private static bool OpenerDone;

        // How fast HP is changing
        private static float HPRateOfChange;

        // Booleans to check for running events
        private static bool firstrun = true;
        private static bool IsCompleted = true;
        private static bool interrupting;

        // HP Rate Of Change booleans
        private static bool HPROC1sAny;
        private static bool HPROC5sAny;
        private static bool HPROC10sAny;

        // HP Rate of Change Sums

        // HP Rate of Change Maxs
        private static int HPROC1sMax;
        private static int HPROC5sMax;
        private static int HPROC10sMax;

        // User selection booleans
        private static bool DPSBurst;
        private static bool CoolDowns;
        private readonly Stopwatch CoolDownTimer = new Stopwatch();

        // Selections stopwatch
        private readonly Stopwatch DPSTimer = new Stopwatch();

        // The Frenzied Regen stopwatch
        private readonly Stopwatch FrenziedTimer = new Stopwatch();

        //List of interruptible spell id's
        private readonly List<int> InterruptableSpells = new List<int>
        {
            2008,
            32546,
            121135,
            152108,
            155245,
            152118,
            33786,
            1064,
            116858,
            157695,
            111771,
            64843,
            605,
            689,
            103103,
            117014,
            339,
            124682,
            114163,
            5782,
            2061,
            19750,
            120517,
            102051,
            120517,
            2060,
            48181,
            2060,
            73920,
            8004,
            5185,
            77472,
            51514,
            82326,
            82327,
            85222,
            32375,
            115268,
            129197,
            118,
            61305,
            28272,
            61721,
            61025,
            61780,
            28271,
            596,
            33076,
            20484,
            8936,
            20066,
            2006,
            115178,
            50769,
            113724,
            6358,
            686,
            115175,
            116694,
            155361,
            48438,
            85673
        };

        private readonly Queue<float> Queue10s = new Queue<float>();

        // HP Rate Of Change Queues 
        private readonly Queue<float> Queue1s = new Queue<float>();
        private readonly Queue<float> Queue5s = new Queue<float>();
        private CheckBox BarkskinBox;
        private CheckBox BearcattingBox;
        private CheckBox BristlingFurBox;
        private CheckBox ChainPullingBox;
        private TrackBar HPROCTrackBar;
        private Label HPROCValue;
        private CheckBox IncapacitatingBox;
        private CheckBox IncarnationBox;
        private CheckBox IncarnationDPSBox;

        private TrackBar InterruptDelayTrackBar;
        private Label InterruptDelayValue;
        private CheckBox LunarBeamBox;
        private CheckBox ProwlOOCBox;

        // Variables for options				
        private CheckBox PulverizeBox;
        private CheckBox RageOfTheSleeperBox;
        private CheckBox RageOfTheSleeperCDBox;
        private CheckBox RageOfTheSleeperDPSBox;
        private CheckBox RageOfTheSleeperHealthBox;
        private CheckBox RendAndTearBox;
        private CheckBox SkullBashInterruptBox;
        private CheckBox SoulOfTheForestBox;
        private CheckBox SurvivalInstictsBox;
        private CheckBox TabAOEBox;
        private CheckBox TabMacroBox;

        // The HP timer
        private Timer timer;
        private CheckBox WildChargeAOEBox;
        private CheckBox WildChargeSingleBox;
        // Global methods for CombatRoutine

        public override string Name => "Guardian Rotation";

        public override string Class => "Druid";

        // End of global methods
        // Start of Initialize method
        // Building the settings dialog and printing advice in PM

        public override Form SettingsForm { get; set; }


        // Start of code that reads and saves to file, so that you have your previous settings when you reload the rotation  

        private static bool SurvivalInsticts
        {
            get
            {
                var survivalInsticts = ConfigFile.ReadValue("Guardian", "SurvivalInsticts").Trim();
                return survivalInsticts != "" && Convert.ToBoolean(survivalInsticts);
            }
            set { ConfigFile.WriteValue("Guardian", "SurvivalInsticts", value.ToString()); }
        }

        private static bool Pulverize
        {
            get
            {
                var pulverize = ConfigFile.ReadValue("Guardian", "Pulverize").Trim();
                return pulverize != "" && Convert.ToBoolean(pulverize);
            }
            set { ConfigFile.WriteValue("Guardian", "Pulverize", value.ToString()); }
        }

        private static bool WildChargeSingle
        {
            get
            {
                var wildChargeSingle = ConfigFile.ReadValue("Guardian", "WildChargeSingle").Trim();
                return wildChargeSingle != "" && Convert.ToBoolean(wildChargeSingle);
            }
            set { ConfigFile.WriteValue("Guardian", "WildChargeSingle", value.ToString()); }
        }

        private static bool Barkskin
        {
            get
            {
                var barkskin = ConfigFile.ReadValue("Guardian", "Barkskin").Trim();
                return barkskin != "" && Convert.ToBoolean(barkskin);
            }
            set { ConfigFile.WriteValue("Guardian", "Barkskin", value.ToString()); }
        }

        private static bool SkullBashInterrupt
        {
            get
            {
                var skullBashInterrupt = ConfigFile.ReadValue("Guardian", "SkullBashInterrupt").Trim();
                return skullBashInterrupt != "" && Convert.ToBoolean(skullBashInterrupt);
            }
            set { ConfigFile.WriteValue("Guardian", "SkullBashInterrupt", value.ToString()); }
        }

        private static bool WildChargeAOE
        {
            get
            {
                var wildChargeAoe = ConfigFile.ReadValue("Guardian", "WildChargeAOE").Trim();
                return wildChargeAoe != "" && Convert.ToBoolean(wildChargeAoe);
            }
            set { ConfigFile.WriteValue("Guardian", "WildChargeAOE", value.ToString()); }
        }

        private static bool RageOfTheSleeper
        {
            get
            {
                var rageOfTheSleeper = ConfigFile.ReadValue("Guardian", "RageOfTheSleeper").Trim();
                return rageOfTheSleeper != "" && Convert.ToBoolean(rageOfTheSleeper);
            }
            set { ConfigFile.WriteValue("Guardian", "RageOfTheSleeper", value.ToString()); }
        }

        private static bool SoulOfTheForest
        {
            get
            {
                var soulOfTheForest = ConfigFile.ReadValue("Guardian", "SoulOfTheForest").Trim();
                return soulOfTheForest != "" && Convert.ToBoolean(soulOfTheForest);
            }
            set { ConfigFile.WriteValue("Guardian", "SoulOfTheForest", value.ToString()); }
        }

        private static bool RendAndTear
        {
            get
            {
                var rendAndTear = ConfigFile.ReadValue("Guardian", "RendAndTear").Trim();
                return rendAndTear != "" && Convert.ToBoolean(rendAndTear);
            }
            set { ConfigFile.WriteValue("Guardian", "RendAndTear", value.ToString()); }
        }

        private static bool BristlingFur
        {
            get
            {
                var bristlingFur = ConfigFile.ReadValue("Guardian", "BristlingFur").Trim();
                return bristlingFur != "" && Convert.ToBoolean(bristlingFur);
            }
            set { ConfigFile.WriteValue("Guardian", "BristlingFur", value.ToString()); }
        }

        private static bool Incarnation
        {
            get
            {
                var incarnation = ConfigFile.ReadValue("Guardian", "Incarnation").Trim();
                return incarnation != "" && Convert.ToBoolean(incarnation);
            }
            set { ConfigFile.WriteValue("Guardian", "Incarnation", value.ToString()); }
        }

        private static bool TabAOE
        {
            get
            {
                var tabAoe = ConfigFile.ReadValue("Guardian", "TabAOE").Trim();
                return tabAoe != "" && Convert.ToBoolean(tabAoe);
            }
            set { ConfigFile.WriteValue("Guardian", "TabAOE", value.ToString()); }
        }

        private static bool Incapacitating
        {
            get
            {
                var incapacitating = ConfigFile.ReadValue("Guardian", "Incapacitating").Trim();
                return incapacitating != "" && Convert.ToBoolean(incapacitating);
            }
            set { ConfigFile.WriteValue("Guardian", "Incapacitating", value.ToString()); }
        }

        private static bool ChainPulling
        {
            get
            {
                var chainPulling = ConfigFile.ReadValue("Guardian", "ChainPulling").Trim();
                return chainPulling != "" && Convert.ToBoolean(chainPulling);
            }
            set { ConfigFile.WriteValue("Guardian", "ChainPulling", value.ToString()); }
        }

        private static bool Bearcatting
        {
            get
            {
                var bearcatting = ConfigFile.ReadValue("Guardian", "Bearcatting").Trim();
                return bearcatting != "" && Convert.ToBoolean(bearcatting);
            }
            set { ConfigFile.WriteValue("Guardian", "Bearcatting", value.ToString()); }
        }

        private static bool ProwlOOC
        {
            get
            {
                var prowlOoc = ConfigFile.ReadValue("Guardian", "ProwlOOC").Trim();
                return prowlOoc != "" && Convert.ToBoolean(prowlOoc);
            }
            set { ConfigFile.WriteValue("Guardian", "ProwlOOC", value.ToString()); }
        }

        private static bool LunarBeam
        {
            get
            {
                var lunarBeam = ConfigFile.ReadValue("Guardian", "LunarBeam").Trim();
                return lunarBeam != "" && Convert.ToBoolean(lunarBeam);
            }
            set { ConfigFile.WriteValue("Guardian", "LunarBeam", value.ToString()); }
        }

        private static bool TabMacro
        {
            get
            {
                var tabMacro = ConfigFile.ReadValue("Guardian", "TabMacro").Trim();
                return tabMacro != "" && Convert.ToBoolean(tabMacro);
            }
            set { ConfigFile.WriteValue("Guardian", "TabMacro", value.ToString()); }
        }

        private static bool RageOfTheSleeperDPS
        {
            get
            {
                var rageOfTheSleeperDps = ConfigFile.ReadValue("Guardian", "RageOfTheSleeperDPS").Trim();
                return rageOfTheSleeperDps != "" && Convert.ToBoolean(rageOfTheSleeperDps);
            }
            set { ConfigFile.WriteValue("Guardian", "RageOfTheSleeperDPS", value.ToString()); }
        }

        private static bool RageOfTheSleeperCD
        {
            get
            {
                var rageOfTheSleeperCd = ConfigFile.ReadValue("Guardian", "RageOfTheSleeperCD").Trim();
                return rageOfTheSleeperCd != "" && Convert.ToBoolean(rageOfTheSleeperCd);
            }
            set { ConfigFile.WriteValue("Guardian", "RageOfTheSleeperCD", value.ToString()); }
        }

        private static bool RageOfTheSleeperHealth
        {
            get
            {
                var rageOfTheSleeperHealth = ConfigFile.ReadValue("Guardian", "RageOfTheSleeperHealth").Trim();
                return rageOfTheSleeperHealth != "" && Convert.ToBoolean(rageOfTheSleeperHealth);
            }
            set { ConfigFile.WriteValue("Guardian", "RageOfTheSleeperHealth", value.ToString()); }
        }

        private static bool IncarnationDPS
        {
            get
            {
                var incarnationDps = ConfigFile.ReadValue("Guardian", "IncarnationDPS").Trim();
                return incarnationDps != "" && Convert.ToBoolean(incarnationDps);
            }
            set { ConfigFile.WriteValue("Guardian", "IncarnationDPS", value.ToString()); }
        }

        private static int HPROC
        {
            get
            {
                var hproc = ConfigFile.ReadValue("Guardian", "HPROC");
                try
                {
                    return Convert.ToInt32(hproc);
                }
                catch (FormatException)
                {
                    return 0;
                }
            }
            set { ConfigFile.WriteValue("Guardian", "HPROC", value.ToString()); }
        }

        private static int InterruptDelay
        {
            get
            {
                var interruptDelay = ConfigFile.ReadValue("Guardian", "InterruptDelay").Trim();
                try
                {
                    if (Convert.ToInt32(interruptDelay) > 50)
                    {
                        return Convert.ToInt32(interruptDelay);
                    }
                    return 50;
                }
                catch (FormatException)
                {
                    return 50;
                }
            }
            set { ConfigFile.WriteValue("Guardian", "InterruptDelay", value.ToString()); }
        }

        public override void Stop()
        {
        }

        public override void Initialize()
        {
            Log.Write("Guardian Rotation v0.2.2 by Inhade", Color.Green);
            Log.Write("Supports all talent choices. However, recommended talents are 2131321 and 2331321", Color.Green);
            Log.Write("Best talent and option choices:", Color.Black);
            Log.Write("Resto affinity and Galactic Guardian", Color.Black);
            Log.Write("Barkskin and Survival either manual or on CD key", Color.Black);
            Log.Write("All else is situational, but feel free to play around regardless", Color.Black);
            Log.Write("Note that CDs and utility abilities are best triggered by the user, as they tend to be reactive", Color.Green);
            Log.Write("Adjust how aggresive healing logic you want to be by changing HP Rate of change treshold in options", Color.Red);
            Log.Write("A lower threshold triggers Frenzied Regen more easily and hence more often", Color.Red);
            Log.Write("This also modulates how easily other CDs will be cast based on if you are taking damage or not", Color.Green);
            Log.Write("as well as the fail-safe to switch to bear if you are bearcatting", Color.Green);
            Log.Write("You can also adjust the delay before the rotation interupts current target", Color.Red);
            Log.Write("Right now, the rotation supports interrupting just for PvP spells", Color.Red);
            Log.Write("The rotation incorporates the function to automatically change targets in AOE", Color.Red);
            Log.Write("Tick TabAOE if you want to use it in AOE rotation", Color.Red);
            Log.Write("You are encouraged to create a /targetenemy macro and bind it to the TabMacro spell in PM Spellbook", Color.Red);
            Log.Write("AOE rotation focuses on picking up and holding aggro both in utility and rotation priorities", Color.Green);
            Log.Write("This causes a dps loss, so if you want to DPS efficiently in AOE choose the Cleave rotation", Color.Green);
            Log.Write("You can choose to bearcat if you have the feral affinity for a 30% dps increase", Color.Red);
            Log.Write("Always be careful not to be taking damage as it can be lethal if you are in cat form", Color.Green);
            Log.Write("This talent is highly situational, always go with Resto affinity if you are unsure", Color.Green);
            Log.Write("Use [ for defensive CDs and ] for DPS burst", Color.Red);
            Log.Write("If you chose to bearcat, DPS Burst will work as the trigger to start the bearcatting rotation", Color.Green);
            Log.Write("Each CD keypress will first use both your Survival Insticts, then Rage if you chose to, then Barkskin", Color.Green);
            WoW.Speak("Guardian Rotation by Inhade");

            SettingsForm = new Form {Text = "Guardian Druid by Inhade - Rotation Settings", StartPosition = FormStartPosition.CenterScreen, Width = 630, Height = 430, ShowIcon = false};

            var PulverizeText = new Label {Text = "Pulverize", Size = new Size(200, 13), Left = 12, Top = 14};
            SettingsForm.Controls.Add(PulverizeText);
            PulverizeBox = new CheckBox {Checked = Pulverize, TabIndex = 2, Size = new Size(15, 14), Left = 225, Top = 14};
            SettingsForm.Controls.Add(PulverizeBox);

            PulverizeBox.Checked = Pulverize;
            PulverizeBox.CheckedChanged += Pulverize_Click;
            PulverizeText.BringToFront();

            var WildChargeSingleText = new Label {Text = "Wild Charge for Single Target", Size = new Size(200, 13), Left = 12, Top = 29};
            SettingsForm.Controls.Add(WildChargeSingleText);
            WildChargeSingleBox = new CheckBox {Checked = WildChargeSingle, TabIndex = 4, Size = new Size(15, 14), Left = 225, Top = 29};
            SettingsForm.Controls.Add(WildChargeSingleBox);

            WildChargeSingleBox.Checked = WildChargeSingle;
            WildChargeSingleBox.CheckedChanged += WildChargeSingle_Click;
            WildChargeSingleText.BringToFront();

            var WildChargeAOEText = new Label {Text = "WildCharge for AOE", Size = new Size(200, 13), Left = 12, Top = 45};
            SettingsForm.Controls.Add(WildChargeAOEText);
            WildChargeAOEBox = new CheckBox {Checked = WildChargeAOE, TabIndex = 6, Size = new Size(15, 14), Left = 225, Top = 45};
            SettingsForm.Controls.Add(WildChargeAOEBox);

            WildChargeAOEBox.Checked = WildChargeAOE;
            WildChargeAOEBox.CheckedChanged += WildChargeAOE_Click;
            WildChargeAOEText.BringToFront();

            var SkullBashInterruptText = new Label {Text = "Skull Bash Interrupt", Size = new Size(200, 13), Left = 12, Top = 61};
            SettingsForm.Controls.Add(SkullBashInterruptText);
            SkullBashInterruptBox = new CheckBox {Checked = SkullBashInterrupt, TabIndex = 8, Size = new Size(15, 14), Left = 225, Top = 61};
            SettingsForm.Controls.Add(SkullBashInterruptBox);

            SkullBashInterruptBox.Checked = SkullBashInterrupt;
            SkullBashInterruptBox.CheckedChanged += SkullBashInterrupt_Click;
            SkullBashInterruptText.BringToFront();

            var SoulOfTheForestText = new Label {Text = "Soul of the Forest", Size = new Size(200, 13), Left = 12, Top = 78};
            SettingsForm.Controls.Add(SoulOfTheForestText);
            SoulOfTheForestBox = new CheckBox {Checked = SoulOfTheForest, TabIndex = 10, Size = new Size(15, 14), Left = 225, Top = 78};
            SettingsForm.Controls.Add(SoulOfTheForestBox);

            SoulOfTheForestBox.Checked = SoulOfTheForest;
            SoulOfTheForestBox.CheckedChanged += SoulOfTheForest_Click;
            SoulOfTheForestText.BringToFront();

            var SurvivalInstictsText = new Label {Text = "Survival Insticts", Size = new Size(200, 13), Left = 12, Top = 95};
            SettingsForm.Controls.Add(SurvivalInstictsText);
            SurvivalInstictsBox = new CheckBox {Checked = SurvivalInsticts, TabIndex = 12, Left = 225, Top = 95, Size = new Size(15, 14)};
            SettingsForm.Controls.Add(SurvivalInstictsBox);

            SurvivalInstictsBox.Checked = SurvivalInsticts;
            SurvivalInstictsBox.CheckedChanged += SurvivalInsticts_Click;
            SurvivalInstictsText.BringToFront();

            var BarkskinText = new Label {Text = "Barkskin", Size = new Size(200, 13), Left = 12, Top = 112};
            SettingsForm.Controls.Add(BarkskinText);
            BarkskinBox = new CheckBox {Checked = Barkskin, TabIndex = 14, Size = new Size(15, 14), Left = 225, Top = 112};
            SettingsForm.Controls.Add(BarkskinBox);

            BarkskinBox.Checked = Barkskin;
            BarkskinBox.CheckedChanged += Barkskin_Click;
            BarkskinText.BringToFront();

            var RageOfTheSleeperText = new Label {Text = "Use Rage of the Sleeper constantly", Size = new Size(200, 13), Left = 12, Top = 281};
            SettingsForm.Controls.Add(RageOfTheSleeperText);
            RageOfTheSleeperBox = new CheckBox {Checked = RageOfTheSleeper, Size = new Size(15, 14), TabIndex = 16, Left = 225, Top = 281};
            SettingsForm.Controls.Add(RageOfTheSleeperBox);

            RageOfTheSleeperBox.Checked = RageOfTheSleeper;
            RageOfTheSleeperBox.CheckedChanged += RageOfTheSleeper_Click;
            RageOfTheSleeperText.BringToFront();

            var RendAndTearText = new Label {Text = "Rend and Tear", Size = new Size(200, 13), Left = 12, Top = 146};
            SettingsForm.Controls.Add(RendAndTearText);
            RendAndTearBox = new CheckBox {Checked = RendAndTear, Size = new Size(15, 14), TabIndex = 20, Left = 225, Top = 146};
            SettingsForm.Controls.Add(RendAndTearBox);

            RendAndTearBox.Checked = RendAndTear;
            RendAndTearBox.CheckedChanged += RendAndTear_Click;
            RendAndTearText.BringToFront();

            var BristlingFurText = new Label {Text = "Bristling Fur", Size = new Size(200, 13), Left = 12, Top = 163};
            SettingsForm.Controls.Add(BristlingFurText);
            BristlingFurBox = new CheckBox {Checked = BristlingFur, Size = new Size(15, 14), TabIndex = 22, Left = 225, Top = 163};
            SettingsForm.Controls.Add(BristlingFurBox);

            BristlingFurBox.Checked = BristlingFur;
            BristlingFurBox.CheckedChanged += BristlingFur_Click;
            BristlingFurText.BringToFront();

            var IncarnationText = new Label {Text = "Use Incarnation constantly", Size = new Size(200, 13), Left = 12, Top = 180};
            SettingsForm.Controls.Add(IncarnationText);
            IncarnationBox = new CheckBox {Checked = Incarnation, Size = new Size(15, 14), TabIndex = 24, Left = 225, Top = 180};
            SettingsForm.Controls.Add(IncarnationBox);

            IncarnationBox.Checked = Incarnation;
            IncarnationBox.CheckedChanged += Incarnation_Click;
            IncarnationText.BringToFront();

            var TabAOEText = new Label {Text = "Tab in AOE", Size = new Size(200, 13), Left = 12, Top = 197};
            SettingsForm.Controls.Add(TabAOEText);
            TabAOEBox = new CheckBox {Checked = TabAOE, Size = new Size(15, 14), TabIndex = 26, Left = 225, Top = 197};
            SettingsForm.Controls.Add(TabAOEBox);

            TabAOEBox.Checked = TabAOE;
            TabAOEBox.CheckedChanged += TabAOE_Click;
            TabAOEText.BringToFront();

            var IncapacitatingText = new Label {Text = "Incapacitating Roar", Size = new Size(200, 13), Left = 12, Top = 214};
            SettingsForm.Controls.Add(IncapacitatingText);
            IncapacitatingBox = new CheckBox {Checked = Incapacitating, Size = new Size(15, 14), TabIndex = 28, Left = 225, Top = 214};
            SettingsForm.Controls.Add(IncapacitatingBox);

            IncapacitatingBox.Checked = Incapacitating;
            IncapacitatingBox.CheckedChanged += Incapacitating_Click;
            IncapacitatingText.BringToFront();

            var ChainPullingText = new Label {Text = "Chain Pulling", Size = new Size(200, 13), Left = 12, Top = 231};
            SettingsForm.Controls.Add(ChainPullingText);
            ChainPullingBox = new CheckBox {Checked = ChainPulling, Size = new Size(15, 14), TabIndex = 30, Left = 225, Top = 231};
            SettingsForm.Controls.Add(ChainPullingBox);

            ChainPullingBox.Checked = ChainPulling;
            ChainPullingBox.CheckedChanged += ChainPulling_Click;
            ChainPullingText.BringToFront();

            var TabMacroText = new Label {Text = "I have setup a /targetenemy macro", Size = new Size(180, 13), Left = 245, Top = 197};
            SettingsForm.Controls.Add(TabMacroText);
            TabMacroBox = new CheckBox {Checked = TabMacro, Size = new Size(15, 14), TabIndex = 32, Left = 425, Top = 197};
            SettingsForm.Controls.Add(TabMacroBox);

            TabMacroBox.Checked = TabMacro;
            TabMacroBox.CheckedChanged += TabMacro_Click;
            TabMacroText.BringToFront();

            var BearcattingText = new Label {Text = "Bearcatting", Size = new Size(200, 13), Left = 12, Top = 248};
            SettingsForm.Controls.Add(BearcattingText);
            BearcattingBox = new CheckBox {Checked = Bearcatting, Size = new Size(15, 14), TabIndex = 34, Left = 225, Top = 248};
            SettingsForm.Controls.Add(BearcattingBox);

            BearcattingBox.Checked = Bearcatting;
            BearcattingBox.CheckedChanged += Bearcatting_Click;
            BearcattingText.BringToFront();

            var ProwlOOCText = new Label {Text = "Prowl out of combat", Size = new Size(200, 13), Left = 12, Top = 265};
            SettingsForm.Controls.Add(ProwlOOCText);
            ProwlOOCBox = new CheckBox {Checked = ProwlOOC, Size = new Size(15, 14), TabIndex = 36, Left = 225, Top = 265};
            SettingsForm.Controls.Add(ProwlOOCBox);

            ProwlOOCBox.Checked = ProwlOOC;
            ProwlOOCBox.CheckedChanged += ProwlOOC_Click;
            ProwlOOCText.BringToFront();

            var LunarBeamText = new Label {Text = "Lunar Beam", Size = new Size(200, 13), Left = 12, Top = 129};
            SettingsForm.Controls.Add(LunarBeamText);
            LunarBeamBox = new CheckBox {Checked = LunarBeam, Size = new Size(15, 14), TabIndex = 38, Left = 225, Top = 129};
            SettingsForm.Controls.Add(LunarBeamBox);

            LunarBeamBox.Checked = LunarBeam;
            LunarBeamBox.CheckedChanged += LunarBeam_Click;
            LunarBeamText.BringToFront();

            var RageOfTheSleeperDPSText = new Label {Text = "Save Rage of the Sleeper for DPS Burst", Size = new Size(200, 13), Left = 12, Top = 297};
            SettingsForm.Controls.Add(RageOfTheSleeperDPSText);
            RageOfTheSleeperDPSBox = new CheckBox {Checked = RageOfTheSleeperDPS, Size = new Size(15, 14), TabIndex = 40, Left = 225, Top = 297};
            SettingsForm.Controls.Add(RageOfTheSleeperDPSBox);

            RageOfTheSleeperDPSBox.Checked = RageOfTheSleeperDPS;
            RageOfTheSleeperDPSBox.CheckedChanged += RageOfTheSleeperDPS_Click;
            RageOfTheSleeperDPSText.BringToFront();

            var RageOfTheSleeperCDText = new Label {Text = "Save Rage of the Sleeper for CD usage", Size = new Size(200, 13), Left = 12, Top = 314};
            SettingsForm.Controls.Add(RageOfTheSleeperCDText);
            RageOfTheSleeperCDBox = new CheckBox {Checked = RageOfTheSleeperCD, Size = new Size(15, 14), TabIndex = 42, Left = 225, Top = 314};
            SettingsForm.Controls.Add(RageOfTheSleeperCDBox);

            RageOfTheSleeperCDBox.Checked = RageOfTheSleeperCD;
            RageOfTheSleeperCDBox.CheckedChanged += RageOfTheSleeperCD_Click;
            RageOfTheSleeperCDText.BringToFront();

            var RageOfTheSleeperHealthText = new Label {Text = "Save Rage of the Sleeper for low HP", Size = new Size(200, 13), Left = 12, Top = 331};
            SettingsForm.Controls.Add(RageOfTheSleeperHealthText);
            RageOfTheSleeperHealthBox = new CheckBox {Checked = RageOfTheSleeperHealth, Size = new Size(15, 14), TabIndex = 44, Left = 225, Top = 331};
            SettingsForm.Controls.Add(RageOfTheSleeperHealthBox);

            RageOfTheSleeperHealthBox.Checked = RageOfTheSleeperHealth;
            RageOfTheSleeperHealthBox.CheckedChanged += RageOfTheSleeperHealth_Click;
            RageOfTheSleeperHealthText.BringToFront();

            var IncarnationDPSText = new Label {Text = "Save Incarnation for DPS Burst", Size = new Size(200, 13), Left = 12, Top = 348};
            SettingsForm.Controls.Add(IncarnationDPSText);
            IncarnationDPSBox = new CheckBox {Checked = IncarnationDPS, Size = new Size(15, 14), TabIndex = 46, Left = 225, Top = 348};
            SettingsForm.Controls.Add(IncarnationDPSBox);

            IncarnationDPSBox.Checked = IncarnationDPS;
            IncarnationDPSBox.CheckedChanged += IncarnationDPS_Click;
            IncarnationDPSText.BringToFront();

            var HPROCText = new Label {Text = "HP Rate of change threshold", Size = new Size(180, 13), Left = 320, Top = 14};
            SettingsForm.Controls.Add(HPROCText);

            HPROCTrackBar = new TrackBar();
            HPROCTrackBar.Location = new Point(320, 31);
            HPROCTrackBar.Size = new Size(224, 45);
            HPROCTrackBar.Scroll += HPROCTrackBar_Scroll;
            HPROCTrackBar.Maximum = 20;
            HPROCTrackBar.TickFrequency = 2;
            HPROCTrackBar.LargeChange = 2;
            HPROCTrackBar.SmallChange = 2;
            HPROCTrackBar.Value = HPROC;
            SettingsForm.Controls.Add(HPROCTrackBar);

            HPROCValue = new Label {Size = new Size(20, 45), Left = 550, Top = 31};
            HPROCValue.Text = "" + HPROC;
            SettingsForm.Controls.Add(HPROCValue);

            HPROC = HPROCTrackBar.Value;
            HPROCText.BringToFront();
            HPROCTrackBar.BringToFront();
            HPROCValue.BringToFront();

            var InterruptDelayText = new Label {Text = "Interrupt Delay (in ms)", Size = new Size(180, 13), Left = 320, Top = 80};
            SettingsForm.Controls.Add(InterruptDelayText);

            InterruptDelayTrackBar = new TrackBar();
            InterruptDelayTrackBar.Location = new Point(320, 97);
            InterruptDelayTrackBar.Size = new Size(224, 45);
            InterruptDelayTrackBar.Scroll += InterruptDelayTrackBar_Scroll;
            InterruptDelayTrackBar.Minimum = 50;
            InterruptDelayTrackBar.Maximum = 850;
            InterruptDelayTrackBar.TickFrequency = 50;
            InterruptDelayTrackBar.LargeChange = 50;
            InterruptDelayTrackBar.SmallChange = 25;
            InterruptDelayTrackBar.Value = InterruptDelay;
            SettingsForm.Controls.Add(InterruptDelayTrackBar);

            InterruptDelayValue = new Label {Size = new Size(30, 45), Left = 550, Top = 97};
            InterruptDelayValue.Text = "" + InterruptDelay;
            SettingsForm.Controls.Add(InterruptDelayValue);

            InterruptDelayText.BringToFront();
            InterruptDelayTrackBar.BringToFront();
            InterruptDelayValue.BringToFront();

            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 520, Top = 346, Size = new Size(65, 25)};

            cmdSave.Click += CmdSave_Click;
            SettingsForm.Controls.Add(cmdSave);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void InterruptDelayTrackBar_Scroll(object sender, EventArgs e)
        {
            InterruptDelay = InterruptDelayTrackBar.Value;
            InterruptDelayValue.Text = "" + InterruptDelayTrackBar.Value;
        }

        private void HPROCTrackBar_Scroll(object sender, EventArgs e)
        {
            HPROC = HPROCTrackBar.Value;
            HPROCValue.Text = "" + HPROCTrackBar.Value;
        }

        private void Pulverize_Click(object sender, EventArgs e)
        {
            Pulverize = PulverizeBox.Checked;
        }

        private void WildChargeSingle_Click(object sender, EventArgs e)
        {
            WildChargeSingle = WildChargeSingleBox.Checked;
        }

        private void WildChargeAOE_Click(object sender, EventArgs e)
        {
            WildChargeAOE = WildChargeAOEBox.Checked;
        }

        private void SkullBashInterrupt_Click(object sender, EventArgs e)
        {
            SkullBashInterrupt = SkullBashInterruptBox.Checked;
        }

        private void SoulOfTheForest_Click(object sender, EventArgs e)
        {
            SoulOfTheForest = SoulOfTheForestBox.Checked;
        }

        private void SurvivalInsticts_Click(object sender, EventArgs e)
        {
            SurvivalInsticts = SurvivalInstictsBox.Checked;
        }

        private void Barkskin_Click(object sender, EventArgs e)
        {
            Barkskin = BarkskinBox.Checked;
        }

        private void RageOfTheSleeper_Click(object sender, EventArgs e)
        {
            RageOfTheSleeper = RageOfTheSleeperBox.Checked;
        }

        private void BristlingFur_Click(object sender, EventArgs e)
        {
            BristlingFur = BristlingFurBox.Checked;
        }

        private void RendAndTear_Click(object sender, EventArgs e)
        {
            RendAndTear = RendAndTearBox.Checked;
        }

        private void Incarnation_Click(object sender, EventArgs e)
        {
            Incarnation = IncarnationBox.Checked;
        }

        private void TabAOE_Click(object sender, EventArgs e)
        {
            TabAOE = TabAOEBox.Checked;
        }

        private void Incapacitating_Click(object sender, EventArgs e)
        {
            Incapacitating = IncapacitatingBox.Checked;
        }

        private void ChainPulling_Click(object sender, EventArgs e)
        {
            ChainPulling = ChainPullingBox.Checked;
        }

        private void Bearcatting_Click(object sender, EventArgs e)
        {
            Bearcatting = BearcattingBox.Checked;
        }

        private void ProwlOOC_Click(object sender, EventArgs e)
        {
            ProwlOOC = ProwlOOCBox.Checked;
        }

        private void TabMacro_Click(object sender, EventArgs e)
        {
            TabMacro = TabMacroBox.Checked;
        }

        private void LunarBeam_Click(object sender, EventArgs e)
        {
            LunarBeam = LunarBeamBox.Checked;
        }

        private void RageOfTheSleeperDPS_Click(object sender, EventArgs e)
        {
            RageOfTheSleeperDPS = RageOfTheSleeperDPSBox.Checked;
        }

        private void RageOfTheSleeperCD_Click(object sender, EventArgs e)
        {
            RageOfTheSleeperCD = RageOfTheSleeperCDBox.Checked;
        }

        private void RageOfTheSleeperHealth_Click(object sender, EventArgs e)
        {
            RageOfTheSleeperHealth = RageOfTheSleeperHealthBox.Checked;
        }

        private void IncarnationDPS_Click(object sender, EventArgs e)
        {
            IncarnationDPS = IncarnationDPSBox.Checked;
        }

        // End of Initialize method
        // Making pulse asynchronous, to accomodate for async tab-aggroing and interrupting

        public override async void Pulse() => await AsyncPulse();


        // Checks for user selections

        private void keypress()
        {
            if (DetectKeyPress.GetKeyState(0xdd) < 0)
            {
                if (DPSTimer.ElapsedMilliseconds > 1000)
                {
                    DPSBurst = !DPSBurst;
                    Log.Write("DPS Burst " + (DPSBurst ? "Activated" : "Deactivated"), Color.Red);
                    WoW.Speak("D P S Burst " + (DPSBurst ? "On" : "Off"));
                    Thread.Sleep(50);
                    DPSTimer.Restart();
                }
            }

            if (DetectKeyPress.GetKeyState(0xdb) < 0)
            {
                CoolDowns = true;
                Log.Write("Cooldowns Activated", Color.Red);
                WoW.Speak("Cooldowns Activated");
                Thread.Sleep(50);
                CoolDownTimer.Restart();
            }
        }


        // Start of async task called by Pulse

        private async Task AsyncPulse()
        {
            // Call the keypress method asynchronously
            keypress();

            // Resets Cooldown use to off if 5 seconds have passed
            // That way it doesn't have to be manually turned off, it's a panic button

            if (CoolDownTimer.ElapsedMilliseconds > 2000)
            {
                CoolDowns = false;
            }

            // Calls the methods that gets the averages and spikes of damage taken

            CalculateHPROCLists();

            // Checks if it is the first time user started the rotation
            // and if so, starts the timer that calculates the rate of change in HP
            // and the stopwatch timer for user options

            if (firstrun)
            {
                DPSTimer.Start();
                InitTimer();
                firstrun = false;
            }

            // Checks if the asynchronous task is already counting down for an interrupt,
            // and if not goes there to see if an interrupt is needed

            if (interrupting == false)
            {
                await WaitForInterrupt();
            }

            // Checks if user chose to tab-aggro and a tab-aggro is due now
            // and if so, goes to the asynchronous task that switches to a target in melee range

            if (combatRoutine.Type == RotationType.AOE && TabAOE && IsInMeleeRange() && IsCompleted)
            {
                await ChangeTarget();
            }

            // Prowl when out of combat if selected

            if (!WoW.IsInCombat && ProwlOOC && WoW.CanCast("Prowl") && !WoW.PlayerHasBuff("Prowl"))
            {
                WoW.CastSpell("Prowl");
                return;
            }

            // Checks if there is enough HP loss for a Frenzied Regen to be worth activating
            // according to how low the user has set the threshold to activate it
            // and prioritizes it of over active mitigation if Guardian of Elune is up

            if (WoW.CanCast("Frenzied Regeneration") &&
                (HPROC1sAny ||
                 HPRateOfChange < -HPROC1sMax*0.8 && WoW.PlayerHasBuff("Guardian of Elune") && WoW.HealthPercent <= 85 && !WoW.PlayerHasBuff("Frenzied Regeneration") && WoW.HealthPercent <= 80) &&
                WoW.PlayerHasBuff("Bear Form") && (!FrenziedTimer.IsRunning || FrenziedTimer.ElapsedMilliseconds > 5000) && WoW.Rage >= 10)
            {
                WoW.CastSpell("Frenzied Regeneration");
                FrenziedTimer.Reset();
                FrenziedTimer.Start();
                return;
            }

            // Checks if Guardian of Elune is not up, is about to expire and go unused, or there is no incoming damage
            // and fires up active mitigation with a priority to always keep Ironfur up

            if (WoW.CanCast("Ironfur") && (WoW.Rage >= 45 || (WoW.PlayerHasBuff("Gory Fur") && WoW.Rage >= 25)) && (WoW.PlayerHasBuff("Mark of Ursol") || !WoW.PlayerHasBuff("Ironfur")) &&
                (!WoW.PlayerHasBuff("Guardian of Elune") || WoW.PlayerBuffTimeRemaining("Guardian of Elune") < 5 || HPROC5sMax > -HPROC*0.2) && WoW.PlayerHasBuff("Bear Form") && !DPSBurst)
            {
                WoW.CastSpell("Ironfur");
                return;
            }

            if (WoW.CanCast("Mark of Ursol") && (WoW.Rage >= 45 || (WoW.PlayerHasBuff("Gory Fur") && WoW.Rage >= 25)) && !WoW.PlayerHasBuff("Mark of Ursol") && WoW.PlayerHasBuff("Ironfur") &&
                (!WoW.PlayerHasBuff("Guardian of Elune") || WoW.PlayerBuffTimeRemaining("Guardian of Elune") < 5 || HPROC5sMax > -HPROC*0.2) && WoW.PlayerHasBuff("Bear Form") && !DPSBurst)
            {
                WoW.CastSpell("Mark of Ursol");
                return;
            }

            // Checks if user has selected to use CDs automatically and uses them if below a certain health percent

            if (WoW.CanCast("Barkskin") &&
                ((WoW.HealthPercent <= 50 && Barkskin) ||
                 CoolDowns && WoW.PlayerSpellCharges("Survival Instincts") == 0 && (WoW.IsSpellOnCooldown("Rage of the Sleeper") || !RageOfTheSleeperCD) &&
                 !WoW.PlayerHasBuff("Survival Instincts") && !WoW.PlayerHasBuff("Barkskin") && !WoW.PlayerHasBuff("Rage of the Sleeper")))
            {
                WoW.CastSpell("Barkskin");
                CoolDowns = false;
                return;
            }

            if (WoW.CanCast("Rage of the Sleeper") &&
                ((WoW.HealthPercent <= 35 && RageOfTheSleeperHealth) || (CoolDowns && WoW.PlayerSpellCharges("Survival Instincts") == 0 && RageOfTheSleeperCD)) &&
                !WoW.PlayerHasBuff("Survival Instincts") && !WoW.PlayerHasBuff("Barkskin") && !WoW.PlayerHasBuff("Rage of the Sleeper"))
            {
                WoW.CastSpell("Rage of the Sleeper");
                CoolDowns = false;
                return;
            }

            if (WoW.CanCast("Survival Instincts") && ((WoW.HealthPercent <= 20 && SurvivalInsticts) || CoolDowns) && !WoW.PlayerHasBuff("Survival Instincts") && !WoW.PlayerHasBuff("Barkskin") &&
                !WoW.PlayerHasBuff("Rage of the Sleeper"))
            {
                WoW.CastSpell("Survival Instincts");
                CoolDowns = false;
                return;
            }

            //Bearcatting rotation if bearcatting is selected and we are not taking damage

            if (combatRoutine.Type == RotationType.SingleTarget && Bearcatting && HPROC10sMax > -HPROC*0.25 && DPSBurst)
            {
                // Wild Charge if selected to, to the enemy's location

                if (WoW.HasTarget && WoW.TargetIsEnemy && WildChargeSingle && WoW.CanCast("Wild Charge", true, true, true, true) && WoW.PlayerHasBuff("Cat Form"))
                {
                    WoW.CastSpell("Wild Charge");
                    return;
                }

                if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    // Get into Cat Form if we are not already into it

                    if (!WoW.PlayerHasBuff("Cat Form") && OpenerDone == false)
                    {
                        WoW.CastSpell("Cat Form");
                    }

                    // Opener rotation

                    if (OpenerDone == false)
                    {
                        // Stop opener if we run out of energy

                        if (WoW.PlayerHasBuff("Cat Form") && WoW.Power < 20)
                        {
                            OpenerDone = true;
                            return;
                        }

                        // Cast Rake

                        if (WoW.CanCast("Rake") && !WoW.WasLastCasted("Rake") && WoW.PlayerHasBuff("Cat Form") && WoW.Power > 90)
                        {
                            WoW.CastSpell("Rake");
                            return;
                        }

                        // Cast Shred untill we run out of energy

                        if (WoW.CanCast("Shred"))
                        {
                            WoW.CastSpell("Shred");
                            return;
                        }
                    }

                    if (OpenerDone)
                    {
                        if (WoW.PlayerHasBuff("Bear Form"))
                        {
                            // If we used both Mangle and Thrash or Rip and/or Rake are about to fall off,
                            // switch to cat form

                            if ((bearstep >= 3 || (IncarnationDPS && incarnatestep >= 7)) && WoW.CanCast("Cat Form") &&
                                (!WoW.PlayerHasBuff("Rage of the Sleeper") && !WoW.PlayerHasBuff("Incarnation: Guardian of Ursoc") ||
                                 (WoW.TargetDebuffStacks("Thrash") == 3 && (!WoW.TargetHasDebuff("Rip") || WoW.TargetDebuffTimeRemaining("Rip") <= 3)) ||
                                 (WoW.TargetDebuffStacks("Thrash") == 3 && (!WoW.TargetHasDebuff("Rake") || WoW.TargetDebuffTimeRemaining("Rake") <= 3))))
                            {
                                WoW.CastSpell("Cat Form");
                                bearstep = 0;
                                Log.Write("Switching to cat", Color.Red);
                                return;
                            }

                            // Cast Rage of the Sleeper if selected to gain the 25% damage increase from the artifact trait

                            if (WoW.CanCast("Rage of the Sleeper") && !RageOfTheSleeperCD && !RageOfTheSleeperHealth && !WoW.PlayerHasBuff("Incarnation: Guardian of Ursoc"))
                            {
                                WoW.CastSpell("Rage of the Sleeper");
                                return;
                            }

                            // Cast Incarnation: Guardian of Ursoc if selected and available

                            if (WoW.CanCast("Incarnation: Guardian of Ursoc") && !WoW.PlayerHasBuff("Rage of the Sleeper"))
                            {
                                WoW.CastSpell("Incarnation: Guardian of Ursoc");
                                return;
                            }

                            // Always keep up Moonfire

                            if (WoW.IsSpellInRange("Moonfire") && WoW.CanCast("Moonfire") &&
                                ((WoW.TargetHasDebuff("Moonfire") && WoW.TargetDebuffTimeRemaining("Moonfire") <= 3) || !WoW.TargetHasDebuff("Moonfire")))
                            {
                                WoW.CastSpell("Moonfire");
                                return;
                            }

                            // Cast Maul if we just started as bear

                            if (WoW.CanCast("Maul") && bearstep == 0 && incarnatestep == 0)
                            {
                                WoW.CastSpell("Maul");
                                Thread.Sleep(50);
                                bearstep++;
                                incarnatestep++;
                                return;
                            }

                            // Cast Mangle and Thrash as long as we can cast them
                            // More times, when incarnating

                            if (WoW.CanCast("Mangle") && (bearstep == 1 || incarnatestep == 1 || incarnatestep == 3 || incarnatestep == 5))
                            {
                                WoW.CastSpell("Mangle");
                                Thread.Sleep(50);
                                bearstep++;
                                incarnatestep++;
                                return;
                            }

                            if (WoW.CanCast("Thrash") && (bearstep == 2 || incarnatestep == 2 || incarnatestep == 4 || incarnatestep == 6))
                            {
                                WoW.CastSpell("Thrash");
                                bearstep++;
                                incarnatestep++;
                                return;
                            }
                        }

                        if (WoW.PlayerHasBuff("Cat Form"))
                        {
                            // Switch to bear form if we don't have enough energy

                            if (WoW.Power < 20)
                            {
                                WoW.CastSpell("Bear Form");
                                Log.Write("Switching to bear", Color.Blue);
                                return;
                            }

                            // Use Rake if debuff is not there or is about to fall off

                            if (WoW.CurrentComboPoints < 5 && (!WoW.TargetHasDebuff("Rake") || WoW.TargetHasDebuff("Rake") && WoW.TargetDebuffTimeRemaining("Rake") < 3))
                            {
                                WoW.CastSpell("Rake");
                                Thread.Sleep(50);
                                return;
                            }

                            // Otherwise, use shred to build up combo points

                            if (WoW.CurrentComboPoints < 5)
                            {
                                WoW.CastSpell("Shred");
                                Thread.Sleep(50);
                                return;
                            }

                            // If we have 5 combo points and target is below 25% so that we can refresh our Rip
                            // cast Ferocious Bite

                            if (WoW.CurrentComboPoints == 5 && WoW.TargetHasDebuff("Rip") && WoW.TargetDebuffTimeRemaining("Rip") < 5 && WoW.TargetHealthPercent < 25)
                            {
                                WoW.CastSpell("Ferocious Bite");
                                Thread.Sleep(50);
                                return;
                            }

                            // Otherwise, cast Rip as a finisher

                            if (WoW.CurrentComboPoints == 5)
                            {
                                WoW.CastSpell("Rip");
                                Thread.Sleep(50);
                                return;
                            }
                        }
                    }
                }
            }

            // Proceeds to main rotation

            switch (combatRoutine.Type)
            {
                // Single target rotation

                case RotationType.SingleTarget:

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && (!Bearcatting || HPROC10sMax < -HPROC*0.25 || !DPSBurst))
                    {
                        // Keep Bear Form up

                        if (!WoW.PlayerHasBuff("Bear Form"))
                        {
                            WoW.CastSpell("Bear Form");
                        }

                        // Wild Charge if selected to, to the enemy's location

                        if (WoW.HasTarget && WoW.TargetIsEnemy && WildChargeSingle && WoW.CanCast("Wild Charge", true, true, true, true))
                        {
                            WoW.CastSpell("Wild Charge");
                            return;
                        }

                        // Always use Moonfire if there is a Galactic Guardian proc

                        if (WoW.IsSpellInRange("Moonfire") && WoW.CanCast("Moonfire") && WoW.PlayerHasBuff("Galactic Guardian"))
                        {
                            WoW.CastSpell("Moonfire");
                            return;
                        }

                        // Cast Incarnation of Ursoc if selected and available

                        if (WoW.CanCast("Incarnation: Guardian of Ursoc") && (Incarnation || (IncarnationDPS && DPSBurst)))
                        {
                            WoW.CastSpell("Incarnation: Guardian of Ursoc");
                            return;
                        }

                        // Cast Bristling Fur if selected, available, and we are taking at least some damage

                        if (WoW.CanCast("Bristling Fur") && BristlingFur && HPROC5sMax < -HPROC*0.4)
                        {
                            WoW.CastSpell("Bristling Fur");
                            return;
                        }

                        // Cast Lunar Beam if selected, available and we are low on health						

                        if (WoW.CanCast("Lunar Beam") && LunarBeam && WoW.HealthPercent <= 45)
                        {
                            WoW.CastSpell("Lunar Beam");
                            return;
                        }

                        // Cast Rage of the Sleeper if selected, available and we are taking at least some damage
                        // unless DPSBurst is enabled

                        if (WoW.CanCast("Rage of the Sleeper") &&
                            ((DPSBurst && RageOfTheSleeperDPS) || (!RageOfTheSleeperCD && !RageOfTheSleeperHealth && !RageOfTheSleeperDPS && RageOfTheSleeper && HPROC5sMax < -HPROC*0.4)))
                        {
                            WoW.CastSpell("Rage of the Sleeper");
                            return;
                        }

                        // Then use Mangle if it is off CD

                        if (WoW.IsSpellInRange("Mangle") && WoW.CanCast("Mangle") && (!RendAndTear || (WoW.TargetDebuffTimeRemaining("Thrash") > 3 && RendAndTear)))
                        {
                            WoW.CastSpell("Mangle");
                            return;
                        }

                        // Then use Thrash if it is off CD

                        if (WoW.CanCast("Thrash"))
                        {
                            WoW.CastSpell("Thrash");
                            return;
                        }

                        // If you have Pulverize and selected that the rotation will use it,
                        // cast it if target has 3 stacks of Thrash

                        if (WoW.CanCast("Pulverize") && Pulverize && (WoW.TargetDebuffStacks("Thrash") == 3))
                        {
                            WoW.CastSpell("Pulverize");
                            return;
                        }

                        // If target does not have Moonfire running on it, or is about to expire,
                        // refresh it

                        if (WoW.CanCast("Moonfire") && WoW.TargetHasDebuff("Moonfire") && WoW.TargetDebuffTimeRemaining("Moonfire") <= 3 ||
                            !WoW.TargetHasDebuff("Moonfire"))
                        {
                            WoW.CastSpell("Moonfire");
                            return;
                        }

                        // If you are not taking damage, the rotation will not use active mitigation 
                        // which are the rage spenders, and build up on rage.
                        // If so, use Maul to spend rage

                        if (WoW.CanCast("Maul") && WoW.Rage >= 80)
                        {
                            WoW.CastSpell("Maul");
                            return;
                        }

                        // Swipe as a filler if nothing else to do

                        if (WoW.CanCast("Swipe") && WoW.IsSpellOnCooldown("Mangle"))
                        {
                            WoW.CastSpell("Swipe");
                        }
                    }

                    break;

                // AOE rotation, mainly for picking up and keeping aggro

                case RotationType.AOE:

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy)
                    {
                        // Keep Bear Form up

                        if (!WoW.PlayerHasBuff("Bear Form"))
                        {
                            WoW.CastSpell("Bear Form");
                        }

                        //Taunt if target is not taunted and Growl is available

                        if (WoW.IsSpellInRange("Growl") && WoW.CanCast("Growl") && !WoW.TargetHasDebuff("Intimidated"))
                        {
                            WoW.CastSpell("Growl");
                        }

                        // Checks if we are not tab-aggroing
                        // and if not, wild charges to its position if we have the talent
                        // and the user chose that the rotation uses it

                        if (WoW.IsSpellInRange("Wild Charge") && WoW.CanCast("Wild Charge") && !TabAOE)
                        {
                            WoW.CastSpell("Wild Charge");
                            return;
                        }

                        // Casts Moonfire if target does not have the debuff, is about to expire or we have a Galactic Guardian proc

                        if (WoW.IsSpellInRange("Moonfire") && WoW.CanCast("Moonfire") &&
                            (!WoW.TargetHasDebuff("Moonfire") || WoW.PlayerHasBuff("Galactic Guardian") || (WoW.TargetHasDebuff("Moonfire") && WoW.TargetDebuffTimeRemaining("Moonfire") <= 3)))
                        {
                            WoW.CastSpell("Moonfire");
                            return;
                        }

                        // Casts Mighty Bash if we have the talent and the target is not a boss (and hence not CCable)

                        if (WoW.IsSpellInRange("Mighty Bash") && WoW.CanCast("Mighty Bash") && !UseCooldowns)
                        {
                            WoW.CastSpell("Mighty Bash");
                            return;
                        }

                        // Cast Incarnation of Ursoc if selected and available

                        if (WoW.CanCast("Incarnation: Guardian of Ursoc") && (Incarnation || (IncarnationDPS && DPSBurst)))
                        {
                            WoW.CastSpell("Incarnation: Guardian of Ursoc");
                            return;
                        }

                        // Cast Bristling Fur if selected, available, and we are taking at least some damage

                        if (WoW.CanCast("Bristling Fur") && BristlingFur && HPROC5sMax < -HPROC*0.4)
                        {
                            WoW.CastSpell("Bristling Fur");
                            return;
                        }

                        // Cast Lunar Beam if selected, available and we are low on health						

                        if (WoW.CanCast("Lunar Beam") && LunarBeam && WoW.HealthPercent <= 45)
                        {
                            WoW.CastSpell("Lunar Beam");
                            return;
                        }

                        // Cast Rage of the Sleeper if selected, available and we are taking at least some damage

                        if (WoW.CanCast("Rage of the Sleeper") &&
                            ((DPSBurst && RageOfTheSleeperDPS) || (!RageOfTheSleeperCD && !RageOfTheSleeperHealth && !RageOfTheSleeperDPS) || (RageOfTheSleeper && HPROC5sMax < -HPROC*0.4)))
                        {
                            WoW.CastSpell("Rage of the Sleeper");
                            return;
                        }

                        // Cast Thrash if it is off CD

                        if (WoW.CanCast("Thrash"))
                        {
                            WoW.CastSpell("Thrash");
                            return;
                        }

                        // Only cast Mangle if we have Incarnation up as it is single target otherwise.
                        // This is a dps loss, but prioritizes Swipe to pick up and keep aggro

                        if (WoW.IsSpellInRange("Mangle") && WoW.CanCast("Mangle") && Incarnation && WoW.PlayerHasBuff("Incarnation: Guardian of Ursoc"))
                        {
                            WoW.CastSpell("Mangle");
                            return;
                        }


                        // Uses Incapacitating Roar if available and selected.
                        // If we are chain pulling, it will use it at 30% to pull the next pack
                        // (hint: If we are chain pulling and Incapacitating Roar is casted, move on to the next pack!)

                        if (WoW.CanCast("Incapacitating Roar") && Incapacitating && ((ChainPulling && WoW.TargetHealthPercent <= 30) || !ChainPulling))
                        {
                            WoW.CastSpell("Incapacitating Roar");
                            return;
                        }

                        // Otherwise, use Swipe

                        if (WoW.CanCast("Swipe"))
                        {
                            WoW.CastSpell("Swipe");
                        }
                    }

                    break;

                case RotationType.SingleTargetCleave:

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy)
                    {
                        // Keep Bear Form up

                        if (!WoW.PlayerHasBuff("Bear Form"))
                        {
                            WoW.CastSpell("Bear Form");
                        }

                        // Always use Moonfire if there is a Galactic Guardian proc

                        if (WoW.IsSpellInRange("Moonfire") && WoW.CanCast("Moonfire") && WoW.PlayerHasBuff("Galactic Guardian"))
                        {
                            WoW.CastSpell("Moonfire");
                            return;
                        }

                        // Cast Incarnation of Ursoc if selected and available

                        if (WoW.CanCast("Incarnation: Guardian of Ursoc") && (Incarnation || (IncarnationDPS && DPSBurst)))
                        {
                            WoW.CastSpell("Incarnation: Guardian of Ursoc");
                            return;
                        }

                        // Cast Bristling Fur if selected, available, and we are taking at least some damage

                        if (WoW.CanCast("Bristling Fur") && BristlingFur && HPROC5sMax < -HPROC*0.4)
                        {
                            WoW.CastSpell("Bristling Fur");
                            return;
                        }

                        // Cast Lunar Beam if selected, available and we are low on health						

                        if (WoW.CanCast("Lunar Beam") && LunarBeam && WoW.HealthPercent <= 45)
                        {
                            WoW.CastSpell("Lunar Beam");
                            return;
                        }

                        // Cast Rage of the Sleeper if selected, available and we are taking at least some damage

                        if (WoW.CanCast("Rage of the Sleeper") &&
                            ((DPSBurst && RageOfTheSleeperDPS) || (!RageOfTheSleeperCD && !RageOfTheSleeperHealth && !RageOfTheSleeperDPS) || (RageOfTheSleeper && HPROC5sMax < -HPROC*0.4)))
                        {
                            WoW.CastSpell("Rage of the Sleeper");
                            return;
                        }

                        // Then use Mangle if it is off CD

                        if (WoW.IsSpellInRange("Mangle") && WoW.CanCast("Mangle"))
                        {
                            WoW.CastSpell("Mangle");
                            return;
                        }

                        // Then use Thrash if it is off CD

                        if (WoW.CanCast("Thrash", true, true, true))
                        {
                            WoW.CastSpell("Thrash");
                            return;
                        }

                        // Swipe as a filler if nothing else to do

                        if (WoW.CanCast("Swipe", true, true, true))
                        {
                            WoW.CastSpell("Swipe");
                        }

                        // Then keep up Moonfire on main target if not automatically refreshed

                        if (WoW.IsSpellInRange("Moonfire") && WoW.CanCast("Moonfire") && WoW.TargetHasDebuff("Moonfire") && WoW.TargetDebuffTimeRemaining("Moonfire") <= 3 ||
                            !WoW.TargetHasDebuff("Moonfire"))
                        {
                            WoW.CastSpell("Moonfire");
                        }
                    }
                    break;
            }
        }


        // Starts timer to calculate HP loss 	

        private void InitTimer()
        {
            timer = new Timer();
            timer.Enabled = true;
            timer.Elapsed += timer_Tick;
            timer.Interval = 200;
        }

        // Every tick (200ms) this timer sends the % of HP that was lost in a thread-safe way.
        // This is useful to calculate if a Frenzied Regen would be worth it,
        // based on the choice the user made.
        // In options you can set how aggressively to use Frenzied Regen,
        // with a lower HP % threshold meaning Frenzied Regen will be cast more often

        private void timer_Tick(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            Interlocked.Increment(ref CurrentHP);
            if (CurrentHP == 0)
            {
                CurrentHP = WoW.HealthPercent;
            }
            else
            {
                PreviousHP = CurrentHP;
                CurrentHP = WoW.HealthPercent;
                HPRateOfChange = CurrentHP - PreviousHP;
                Thread.Sleep(200);
            }
            timer.Enabled = true;
        }


        // Calculates HPROC averages and spikes

        private void CalculateHPROCLists()
        {
            // Populates the variables of the last second
            // Max is the maximum change in HP for the given time period
            // Any is a boolean that indicates whether we have taken more damage than the threshold
            // at any point in the last second
            // and Average is the averaged rate of change for the last second

            if (Queue1s.Count > 4)
            {
                Queue1s.Dequeue();
            }
            Queue1s.Enqueue(HPRateOfChange);

            foreach (var a in Queue1s)
            {
                if (a < -HPROC)
                {
                    HPROC1sMax = Convert.ToInt32(a);
                    HPROC1sAny = true;
                    break;
                }
                HPROC1sMax = 0;
                HPROC1sAny = false;
            }

            // Populates the variables of the last 5 seconds
            // simirarly to the previous set

            if (Queue5s.Count > 24)
            {
                Queue5s.Dequeue();
            }
            Queue5s.Enqueue(HPRateOfChange);

            foreach (var c in Queue5s)
            {
                if (c < -HPROC)
                {
                    HPROC5sMax = Convert.ToInt32(c);
                    HPROC5sAny = true;
                    break;
                }
                HPROC5sMax = 0;
                HPROC5sAny = false;
            }


            // Populates the variables of the last 10 seconds
            // simirarly to the previous set

            if (Queue10s.Count > 49)
            {
                Queue10s.Dequeue();
            }
            Queue10s.Enqueue(HPRateOfChange);


            foreach (var e in Queue10s)
            {
                if (e < -HPROC)
                {
                    HPROC10sMax = Convert.ToInt32(e);
                    HPROC10sAny = true;
                    break;
                }
                HPROC10sMax = 0;
                HPROC10sAny = false;
            }
        }

        // Logic for changing target in AOE if user has selected tab-aggroing
        // Will change target untill it finds one in range, every 1 s
        // in a thread-safe and asynchronous way
        // The user is provided in options which way they prefer to change target,
        // either by a /targetenemy macro (more efficient and safe)
        // or by PM sending the keystrokes themselves

        private static async Task ChangeTarget()
        {
            IsCompleted = false;
            {
                if (TabMacro)
                {
                    for (var a = 1; a < 4; a++)
                    {
                        WoW.CastSpell("TabMacro");
                        if (IsInMeleeRange())
                        {
                            await Task.Delay(1000);
                            IsCompleted = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (var a = 1; a < 4; a++)
                    {
                        WoW.SendMacro("/targetenemy");
                        if (IsInMeleeRange())
                        {
                            await Task.Delay(1000);
                            IsCompleted = true;
                            break;
                        }
                    }
                }
            }
            IsCompleted = true;
        }

        // Checks if the spell currently being cast by the target is in the list of spells to be interrupted
        // Currently the list contains only PVP spells

        private bool Interruptable()
        {
            return InterruptableSpells.Contains(WoW.TargetCastingSpellID);
        }

        // Asynchronous task that detects when a target begins to cast a spell,
        // returns to the loop and then resumes when the interrupt delay has passed

        private async Task WaitForInterrupt()
        {
            if (WoW.TargetIsCasting && SkullBashInterrupt && WoW.IsSpellInRange("Skull Bash") && Interruptable())
            {
                interrupting = true;
                await Task.Delay(InterruptDelay);
                WoW.CastSpell("Skull Bash");
                Log.Write("Interrupting!", Color.Red);
                interrupting = false;
            }
        }

        // End of config saving code		


        // Checks if current target is in melee range
        private static bool IsInMeleeRange()
        {
            return WoW.CanCast("Mangle", false, false, true, false, false);
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Inhade
AddonName=PixelMagic
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,77758,Thrash,D3
Spell,210037,Growl,D5
Spell,80313,Pulverize,X
Spell,33917,Mangle,D2
Spell,164812,Moonfire,D5
Spell,106785,Swipe,D9
Spell,192081,Ironfur,U
Spell,192083,Mark of Ursol,V
Spell,22842,Frenzied Regeneration,B
Spell,22812,Barkskin,Z
Spell,6807,Maul,F
Spell,0,TabMacro,Divide
Spell,106839,Skull Bash,S
Spell,102558,Incarnation: Guardian of Ursoc,D0
Spell,99,Incapacitating Roar,K
Spell,204066,Lunar Beam,F7
Spell,200851,Rage of the Sleeper,J
Spell,155835,Bristling Fur,W
Spell,102401,Wild Charge,D1
Spell,5211,Mighty Bash,Subtract
Spell,61336,Survival Instincts,A
Spell,5487,Bear Form,NumPad0
Spell,1822,Rake,F2
Spell,5221,Shred,F3
Spell,1079,Rip,Decimal
Spell,768,Cat Form,Add
Spell,5215,Prowl,Q
Spell,22568,Ferocious Bite,PageDown
Aura,22812,Barkskin
Aura,200851,Rage of the Sleeper
Aura,102558,Incarnation: Guardian of Ursoc
Aura,61336,Survival Instincts
Aura,201671,Gory Fur
Aura,155578,Guardian of Elune
Aura,192083,Mark of Ursol
Aura,192081,Ironfur
Aura,77758,Thrash
Aura,164812,Moonfire
Aura,206891,Intimidated
Aura,203964,Galactic Guardian
Aura,5487,Bear Form
Aura,768,Cat Form
Aura,5215,Prowl
Aura,1079,Rip
Aura,155722,Rake
*/
