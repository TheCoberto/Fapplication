using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Fapplication
{
    public partial class Fapplication : Form
    {
        private Stopwatch stopwatch = new Stopwatch();
        private bool isRunning;
        private string elapsedTimeFilePath = "elapsedTime.txt";

        public Fapplication()
        {
            InitializeComponent();
            string timeString = File.ReadAllText(elapsedTimeFilePath);
            TimeSpan time = TimeSpan.Parse(timeString);
            ElapsedTime.Text = time.ToString();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                stopwatch.Start();
                Timer.Start();
                isRunning = true;
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                stopwatch.Stop();
                Timer.Stop();
                isRunning = false;
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
            ElapsedTime.Text = "00:00:00";
            SaveElapsedTime();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string timeString = File.ReadAllText(elapsedTimeFilePath);
            TimeSpan time = TimeSpan.Parse(timeString);
            time += stopwatch.Elapsed;
            ElapsedTime.Text = time.ToString(@"hh\:mm\:ss");
        }

        private void Fapplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveElapsedTime();
        }

        private void SaveElapsedTime()
        {
            string timeString = File.ReadAllText(elapsedTimeFilePath);
            TimeSpan time = TimeSpan.Parse(timeString);
            TimeSpan totalTime = time + stopwatch.Elapsed;

            try
            {
                if (stopwatch.Elapsed != TimeSpan.Zero)
                {
                    File.WriteAllText(elapsedTimeFilePath, totalTime.ToString());
                }
                else
                {
                    File.WriteAllText(elapsedTimeFilePath, TimeSpan.Zero.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save elapsed time: " + ex.Message);
            }
        }
    }
}