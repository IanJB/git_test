using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OS;

namespace StopWatch
{
    public partial class Form1 : Form
    {
        private operatingSystem os;
        private const int OSwin32 = 1;
        private const int OSunix = 2;
        private static string OSseparator = "//";
        private const string SetUpString = "Settings";
        private const string WarmUpString = "Warm Up";
        private const string WorkString = "Work";
        private const string RelaxString = "Relax";
        private const string CoolDownString = "Cool Down";
        private const string AllDoneString = "All Done!";
        private const int WarmUp = 0;
        private const int IntervalWork = 1;
        private const int IntervalRelax = 2;
        private const int CoolDown = 3;
        private string filePath = "";
        private string iniFileName = "";
        private string iniText = "";
        private string WarmSound = "";
        private string WorkSound = "";
        private string RelaxSound = "";
        private string CoolSound = "";
        private string AllDoneSound = "";
        private int WarmUpTime = 300;
        private int WorkTime = 20;
        private int RelaxTime = 10;
        private int repetitionCount = 8;
        private int CoolDownTime = 300;
        private string TimeString = "";
        private int ActionState = WarmUp;
        private int secondCount = 0;
        private bool neverStarted = true;
        private bool started = false;
        private bool allDone = false;
        private System.Media.SoundPlayer wavPlayer = new System.Media.SoundPlayer();
        private string SVNrevision = "$Rev:$";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            os=new operatingSystem();
            OSlabel.Text = os.OSstring;
            if (os.OS == OSunix)
            {
                OSseparator="/";
            }
            filePath = Application.StartupPath.ToString();
            iniFileName = filePath + OSseparator + Application.ProductName.ToString() + ".ini";
            WarmSound = filePath + OSseparator + "gntlmn.wav";
            WorkSound = filePath + OSseparator + "work.wav";
            RelaxSound = filePath + OSseparator + "relax.wav";
            CoolSound = filePath + OSseparator + "cool.wav";
            AllDoneSound = filePath + OSseparator + "allDOne.wav";
            string[] splitLine = new string[] { "", "" };
            if (File.Exists(iniFileName))
            {
                iniText = File.ReadAllText(iniFileName);
                splitLine = iniText.Split(',');
            }
            if (splitLine.Length == 5)
            {
                WarmUpTime = int.Parse(splitLine[0]);
                WorkTime = int.Parse(splitLine[1]);
                RelaxTime = int.Parse(splitLine[2]);
                repetitionCount = int.Parse(splitLine[3]);
                CoolDownTime = int.Parse(splitLine[4]);
            }
            this.Text = Application.ProductName.ToString() + " " + SVNrevision;
            ActionLabel.Text = SetUpString;
            TimeLabel.Text = TimeString;
            RepetitionLabel.Text = "";
            this.BackColor = Color.Yellow;
            warmUpTimeValue.Value = WarmUpTime;
            WorkTimeValue.Value = WorkTime;
            RelaxTimeValue.Value = RelaxTime;
            RepetitionCountValue.Value = repetitionCount;
            CoolDownValue.Value = CoolDownTime;
            secondCount = WarmUpTime;
            StartButton.BackColor = Color.ForestGreen;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (neverStarted)
            {
                ActionLabel.Text = WarmUpString;
                started = true;
                SecondTimer.Interval = 1000;
                SecondTimer.Start();
                StartButton.Text = "Pause";
                StartButton.BackColor = Color.Red;
                WarmUplabel.Visible = false;
                warmUpTimeValue.Visible = false;
                WorkTimeLabel.Visible = false;
                WorkTimeValue.Visible = false;
                RelaxTimeLabel.Visible = false;
                RelaxTimeValue.Visible = false;
                RepetitionsLabel.Visible = false;
                RepetitionCountValue.Visible = false;
                CoolDownLabel.Visible = false;
                CoolDownValue.Visible = false;
                RepetitionLabel.Visible = true;
                TimeLabel.Visible = true;
                neverStarted = false;
                iniText = WarmUpTime.ToString() + "," +
                          WorkTime.ToString() + "," +
                          RelaxTime.ToString() + "," +
                          repetitionCount.ToString() + "," +
                          CoolDownTime.ToString();
                TextWriter tw = new StreamWriter(iniFileName);
                tw.WriteLine(iniText);
                tw.Close();
                this.BackColor = Color.Orange;
                wavPlayer.SoundLocation = WarmSound;
                wavPlayer.Play();
            }
            else if (allDone)
            {
                this.Close();
            }
            else if (started) // already running - pause
            {
                started = false;
                StartButton.Text = "Resume";
                StartButton.BackColor = Color.LightGreen;
                SecondTimer.Stop();
            }
            else // paused - resume
            {
                started = true;
                SecondTimer.Start();
                StartButton.Text = "Pause";
                StartButton.BackColor = Color.Red;                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (ActionState)
            {
                case WarmUp:
                    TimeString = secondCount.ToString();
                    if (secondCount <= 1)
                    {
                        secondCount = WorkTime + 1;
                        ActionState = IntervalWork;
                        wavPlayer.SoundLocation = WorkSound;
                        wavPlayer.Play();
                    }
                    break;

                case IntervalWork:
                    TimeString = secondCount.ToString();
                    RepetitionLabel.Text = repetitionCount.ToString();
                    ActionLabel.Text = WorkString;
                    this.BackColor = Color.Red;
                    if (secondCount <= 1)
                    {
                        repetitionCount--;
                        if (repetitionCount > 0)
                        {
                            secondCount = RelaxTime + 1;
                            ActionState = IntervalRelax;
                            wavPlayer.SoundLocation = RelaxSound;
                            wavPlayer.Play();
                        }
                        else
                        {
                            secondCount = CoolDownTime + 1;
                            ActionState = CoolDown;
                            wavPlayer.SoundLocation = CoolSound;
                            wavPlayer.Play();
                        }
                    }
                    break;

                case IntervalRelax:
                    TimeString = secondCount.ToString();
                    RepetitionLabel.Text = repetitionCount.ToString();
                    ActionLabel.Text = RelaxString;
                    this.BackColor = Color.Green;
                    if (secondCount <= 1)
                    {
                        ActionState = IntervalWork;
                        secondCount = WorkTime + 1;
                        wavPlayer.SoundLocation = WorkSound;
                        wavPlayer.Play();
                    }
                    break;

                case CoolDown:
                    TimeString = secondCount.ToString();
                    ActionLabel.Text = CoolDownString;
                    this.BackColor = Color.Blue;
                    RepetitionLabel.Text = "";
                    if (secondCount <= 0)
                    {
                        secondCount = CoolDownTime + 1;
                        ActionLabel.Text = AllDoneString;
                        SecondTimer.Stop();
                        StartButton.Text = "Exit";
                        allDone = true;
                        TimeString = "";
                        RepetitionLabel.Text = "";
                        wavPlayer.SoundLocation = AllDoneSound;
                        wavPlayer.Play();
                    }
                    break;
            }
            TimeLabel.Text = TimeString;
            secondCount--;
        }

        private void warmUpTimeValue_ValueChanged(object sender, EventArgs e)
        {
            WarmUpTime = (int)warmUpTimeValue.Value;
            secondCount = WarmUpTime;
        }

        private void WorkTimeValue_ValueChanged(object sender, EventArgs e)
        {
            WorkTime = (int)WorkTimeValue.Value;
        }

        private void RelaxTimeValue_ValueChanged(object sender, EventArgs e)
        {
            RelaxTime = (int)RelaxTimeValue.Value;
        }

        private void RepetitionCountValue_ValueChanged(object sender, EventArgs e)
        {
            repetitionCount = (int)RepetitionCountValue.Value;
        }

        private void CoolDownValue_ValueChanged(object sender, EventArgs e)
        {
            CoolDownTime = (int)CoolDownValue.Value;
        }
    }
}