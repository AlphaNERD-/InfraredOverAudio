using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace IRSignalGenerator
{
    public partial class frmIRSignalGenerator : Form
    {
        List<SignalGenerator> generators;
        MixingSampleProvider provider;
        SignalGenerator genRow;
        SignalGenerator genCol;

        WaveOutEvent wo;
        double[] frequencies;
        int currentFrequency = 0;

        Timer switchSignalTimer = null;

        readonly int[] DTMF_FREQ = {697, 770, 852, 941, 1209, 1336, 1477, 1633};

        public frmIRSignalGenerator()
        {
            InitializeComponent();

            switchSignalTimer = new Timer();
            switchSignalTimer.Tick += SwitchSignalTimer_Tick;

            generators = new List<SignalGenerator>();

            genRow = new SignalGenerator();
            genRow.Type = SignalGeneratorType.Sin;
            genRow.Gain = 1.0;
            generators.Add(genRow);

            genCol = new SignalGenerator();
            genCol.Type = SignalGeneratorType.Sin;
            genCol.Gain = 1.0;
            generators.Add(genCol);

            provider = new MixingSampleProvider(generators);

            wo = new WaveOutEvent();
            wo.Init(provider);
        }

        private void SwitchSignalTimer_Tick(object sender, EventArgs e)
        {
            switchSignalTimer.Enabled = false;
            wo.Stop();

            System.Threading.Thread.Sleep((int)nudPauseLength.Value);

            UpdateSignalGenerators();

            switchSignalTimer.Enabled = true;
            wo.Play();
        }

        private void btnPlaySignal_Click(object sender, EventArgs e)
        {
            UpdateSignalGenerators();

            if (wo.PlaybackState == PlaybackState.Playing)
            {
                switchSignalTimer.Enabled = false;
                wo.Stop();
                btnPlaySignal.Text = "Play Signal";
                lblInfo.Text = "Waiting...";
            }
            else
            {
                //frequencies = PrepareIRData();
                //gen.Frequency = frequencies[currentFrequency];
                switchSignalTimer.Interval = (int)nudBitLength.Value;
                switchSignalTimer.Enabled = true;
                wo.Play();
                btnPlaySignal.Text = "Stop Signal";
                UpdateInfoLabel();
            }
        }

        private void UpdateSignalGenerators()
        {
            int rowValue = (int)nudRow.Value - 1;
            int colValue = (int)nudCol.Value + 3;

            genRow.Frequency = DTMF_FREQ[rowValue];
            genCol.Frequency = DTMF_FREQ[colValue];

            lblInfo.Text = "Sending " + rowValue;
        }

        private double[] PrepareIRData()
        {
            currentFrequency = 0;

            double[] result = null;
            string[] entries = tbxIRdata.Text.Split(' ');
            int index = 0;

            //We not only need the actual IR data but also
            //some signals to tell the Arduino what frequency
            //to pulse the LED with. And we need to tell the
            //Arduino when we send IR data and when we stop.
            result = new double[entries.Length + 4];

            //Set IR frequency signal
            result[index] = 1000d;
            index++;

            //Signal the IR frequency
            result[index] = (float)nudIRFrequency.Value - 25000;
            index++;

            //Tell Arduino listen to incoming data
            result[index] = 2000d;
            index++;

            foreach (string entry in entries)
            {
                string tempEntry = entry.Trim(' ', '+', '-');
                //tempEntry = tempEntry.Remove(0, 1);

                result[index] = Convert.ToDouble(tempEntry);
                index++;
            }

            //Tell Arduino to stop listening to incoming data
            //And of course send the data
            result[index] = 3000d;
            index++;

            return result;
        }

        private void UpdateInfoLabel()
        {
            //lblInfo.Text = "(" + (currentFrequency + 1) + "/" + frequencies.Length + "), " + frequencies[currentFrequency] + " Hz";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentFrequency < frequencies.Length - 1)
            {
                currentFrequency++;
                UpdateInfoLabel();
                //gen.Frequency = frequencies[currentFrequency];
            }
        }

        private void btnPrv_Click(object sender, EventArgs e)
        {
            if (currentFrequency > 0)
            {
                currentFrequency--;
                UpdateInfoLabel();
                //gen.Frequency = frequencies[currentFrequency];
            }
        }
    }
}
