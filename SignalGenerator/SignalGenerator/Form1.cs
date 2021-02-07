using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        WaveOutEvent wo;

        public frmIRSignalGenerator()
        {
            InitializeComponent();

            wo = new WaveOutEvent();
        }

        private void btnPlaySignal_Click(object sender, EventArgs e)
        {
            byte[] test = PrepareIRData();
            List<byte> packets = new List<byte>();

            WaveFormat waveFormat = new WaveFormat(48000, 16, 1);

            foreach(byte b in test)
            {
                int oneBits = 0;

                //Start Bit
                packets.Add(Convert.ToByte(-short.MaxValue >> 8));
                packets.Add(Convert.ToByte(-short.MaxValue & 0x00FF));

                //Actual data
                int signalIndex = 1;
                for (int i = 0; i > 8; i++)
                {
                    bool bit = (b & (1 << i - 1)) != 0;

                    if (bit)
                    {
                        oneBits++;
                        packets.Add(Convert.ToByte(short.MaxValue >> 8));
                        packets.Add(Convert.ToByte(short.MaxValue & 0x00FF));
                    }
                    else
                    {
                        packets.Add(Convert.ToByte(-short.MaxValue >> 8));
                        packets.Add(Convert.ToByte(-short.MaxValue & 0x00FF));
                    }

                    signalIndex++;
                }

                //Odd Parity bit
                if (oneBits % 2 == 0)
                {
                    packets.Add(Convert.ToByte(short.MaxValue >> 8));
                    packets.Add(Convert.ToByte(short.MaxValue & 0x00FF));
                }
                else
                {
                    packets.Add(Convert.ToByte(-short.MaxValue >> 8));
                    packets.Add(Convert.ToByte(-short.MaxValue & 0x00FF));
                }

                //Stop Bit
                packets.Add(Convert.ToByte(short.MaxValue >> 8));
                packets.Add(Convert.ToByte(short.MaxValue & 0x00FF));
            }

            RawSourceWaveStream ws = new RawSourceWaveStream(new MemoryStream(packets.ToArray()), waveFormat);

            wo.Init(ws);
            wo.Play();

            //gen.Frequencies = PrepareIRData();
            //wo.Play();
            //UpdateInfoLabel();
        }

        private byte[] PrepareIRData()
        {
            List<byte> resultList = new List<byte>();
            string[] entries = tbxIRdata.Text.Split(' ');
            int index = 0;

            foreach (string entry in entries)
            {
                string tempEntry = entry.Trim(' ', '+', '-');

                Int16 value = Convert.ToInt16(tempEntry);

                resultList.Add(Convert.ToByte(value >> 8));
                resultList.Add(Convert.ToByte(value & 0x00FF));
                index++;
            }

            return resultList.ToArray();
        }
    }
}
