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
        SignalGenerator gen0;
        SignalGenerator gen1;
        SignalGenerator gen2;
        SignalGenerator gen4;
        SignalGenerator gen8;
        SignalGenerator gen16;
        SignalGenerator gen32;
        SignalGenerator gen64;
        SignalGenerator gen128;

        WaveOutEvent wo;
        double[] frequencies;
        int currentFrequency = 0;

        Timer switchSignalTimer = null;

        const int HIGH_FREQ = 1000;
        const int LOW_FREQ = 400;

        public frmIRSignalGenerator()
        {
            InitializeComponent();

            switchSignalTimer = new Timer();
            switchSignalTimer.Tick += SwitchSignalTimer_Tick;

            generators = new List<SignalGenerator>();

            gen0 = new SignalGenerator();
            gen0.Type = SignalGeneratorType.Sin;
            gen0.Frequency = 400;
            generators.Add(gen0);

            gen1 = new SignalGenerator();
            gen1.Type = SignalGeneratorType.Sin;
            gen1.Frequency = 1000;
            generators.Add(gen1);

            gen2 = new SignalGenerator();
            gen2.Type = SignalGeneratorType.Sin;
            gen2.Frequency = 1600;
            generators.Add(gen2);

            gen4 = new SignalGenerator();
            gen4.Type = SignalGeneratorType.Sin;
            gen4.Frequency = 2200;
            generators.Add(gen4);

            gen8 = new SignalGenerator();
            gen8.Type = SignalGeneratorType.Sin;
            gen8.Frequency = 2800;
            generators.Add(gen8);

            gen16 = new SignalGenerator();
            gen16.Type = SignalGeneratorType.Sin;
            gen16.Frequency = 3400;
            generators.Add(gen16);

            gen32 = new SignalGenerator();
            gen32.Type = SignalGeneratorType.Sin;
            gen32.Frequency = 4000;
            generators.Add(gen32);

            gen64 = new SignalGenerator();
            gen64.Type = SignalGeneratorType.Sin;
            gen64.Frequency = 4600;
            generators.Add(gen64);

            gen128 = new SignalGenerator();
            gen128.Type = SignalGeneratorType.Sin;
            gen128.Frequency = 5200;
            generators.Add(gen128);

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
            int byteValue = (int)nudSendByte.Value;

            if (byteValue == 0)
            {
                gen0.Gain = 1.0;
                gen1.Gain = 0.0;
                gen2.Gain = 0.0;
                gen4.Gain = 0.0;
                gen8.Gain = 0.0;
                gen16.Gain = 0.0;
                gen32.Gain = 0.0;
                gen64.Gain = 0.0;
                gen128.Gain = 0.0;
            }
            else
            {
                gen0.Gain = 0.0;

                if ((byteValue & (1 << 0)) != 0)
                    gen1.Gain = 1.0;

                if ((byteValue & (1 << 1)) != 0)
                    gen2.Gain = 1.0;

                if ((byteValue & (1 << 2)) != 0)
                    gen4.Gain = 1.0;

                if ((byteValue & (1 << 3)) != 0)
                    gen8.Gain = 1.0;

                if ((byteValue & (1 << 4)) != 0)
                    gen16.Gain = 1.0;

                if ((byteValue & (1 << 5)) != 0)
                    gen32.Gain = 1.0;

                if ((byteValue & (1 << 6)) != 0)
                    gen64.Gain = 1.0;

                if ((byteValue & (1 << 7)) != 0)
                    gen128.Gain = 1.0;
            }

            lblInfo.Text = "Sending " + byteValue;
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
