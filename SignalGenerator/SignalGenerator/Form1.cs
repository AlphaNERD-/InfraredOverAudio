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
        //IRSignalGenerator gen;
        SignalGenerator gen;
        WaveOutEvent wo;
        double[] frequencies;
        int currentFrequency = 0;

        Timer switchSignalTimer = null;
        bool bit = true;

        const int HIGH_FREQ = 1000;
        const int LOW_FREQ = 400;

        public frmIRSignalGenerator()
        {
            InitializeComponent();

            /*gen = new IRSignalGenerator()
            {
                Gain = 0.2,
            };*/

            switchSignalTimer = new Timer();
            switchSignalTimer.Tick += SwitchSignalTimer_Tick;

            gen = new SignalGenerator();
            gen.Type = SignalGeneratorType.Sin;
            //gen.Gain = 0.2f;

            wo = new WaveOutEvent();
            wo.Init(gen);
        }

        private void SwitchSignalTimer_Tick(object sender, EventArgs e)
        {
            switchSignalTimer.Enabled = false;
            wo.Stop();

            System.Threading.Thread.Sleep((int)nudPauseLength.Value);

            if (bit)
            {
                gen.Frequency = HIGH_FREQ;
                lblInfo.Text = "Sende 1";
            }
            else
            {
                gen.Frequency = LOW_FREQ;
                lblInfo.Text = "Sende 0";
            }

            bit = !bit;

            switchSignalTimer.Enabled = true;
            wo.Play();
        }

        private void btnPlaySignal_Click(object sender, EventArgs e)
        {
            if (bit)
            {
                gen.Frequency = HIGH_FREQ;
                lblInfo.Text = "Sende 1";

            }
            else
            {
                gen.Frequency = LOW_FREQ;
                lblInfo.Text = "Sende 0";
            }

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

    /// <summary>
    /// This class is derived from the SignalGenerator class of the NAudio project.
    /// </summary>
    public class IRSignalGenerator : ISampleProvider
    {
        // Wave format
        private readonly WaveFormat waveFormat;

        // Generator variable
        private int nSample;

        /// <summary>
        /// Initializes a new instance for the Generator
        /// </summary>
        /// <param name="sampleRate">Desired sample rate</param>
        /// <param name="channel">Number of channels</param>
        public IRSignalGenerator()
        {
            waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);

            // Default
            Gain = 1;
        }

        /// <summary>
        /// The waveformat of this WaveProvider (same as the source)
        /// </summary>
        public WaveFormat WaveFormat => waveFormat;

        /// <summary>
        /// Array of frequencies for the Generator. (20.0 - 20000.0 Hz)
        /// </summary>
        public double[] Frequencies { get; set; }

        public int currentFrequency;

        /// <summary>
        /// Gain for the Generator. (0.0 to 1.0)
        /// </summary>
        public double Gain { get; set; }

        /// <summary>
        /// Reads from this provider.
        /// </summary>
        public int Read(float[] buffer, int offset, int count)
        {
            int outIndex = offset;

            // Generator current value
            double multiple;
            double sampleValue;
            double sampleSaw;

            // Complete Buffer
            for (int sampleCount = 0; sampleCount < count; sampleCount++)
            {
                multiple = 2 * Frequencies[currentFrequency] / waveFormat.SampleRate;
                sampleSaw = ((nSample * multiple) % 2) - 1;
                sampleValue = sampleSaw > 0 ? Gain : -Gain;

                nSample++;

                /*if (sampleCount % 1000 == 0)
                {
                    currentFrequency++;

                    if (currentFrequency == Frequencies.Length)
                        currentFrequency = 0;
                }*/

                buffer[outIndex++] = (float)sampleValue;
            }
            return count;
        }
    }
}
