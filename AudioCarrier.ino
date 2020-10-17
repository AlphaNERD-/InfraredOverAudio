const int setFrequencySound = 1000;
const int receiveDataSound = 2000;
const int receiveDataEndSound = 3000;

const int readyMode = 0;
const int setFrequencyMode = 1;
const int receiveDataMode = 2;
const int sendDataMode = 3;

volatile unsigned long timeBefore;
volatile unsigned long timeNow;
volatile unsigned long delta;
volatile float audioFrequencybefore;
volatile float audioFrequency;

volatile float irFrequency;
volatile int[100] irData;
volatile int irDataIndex;

volatile int mode = 0;

ISR (ANALOG_COMP_vect)
  {
    timeBefore = timeNow;
    
    timeNow = micros();
  }

void setup ()
  {
  Serial.begin (115200);
  Serial.println ("Started.");
  ADCSRB = 0;           // (Disable) ACME: Analog Comparator Multiplexer Enable
  ACSR =  bit (ACI)     // (Clear) Analog Comparator Interrupt Flag
        | bit (ACIE)    // Analog Comparator Interrupt Enable
        | bit (ACIS1);  // ACIS1, ACIS0: Analog Comparator Interrupt Mode Select (trigger on falling edge)
   }  // end of setup

void loop ()
  {
    //Calculate frequency by using the period duration
    //determined through the timestamps captured in
    //ISR method.
    delta = timeNow - timeBefore;
    freq = 1.0 / ((float)delta / 1000000.0);

    //Do not process the same frequency multiple times
    if (audioFrequency != audioFrequencyBefore)
    {
      Serial.print (audioFrequency, 10);
      Serial.println (" Hz.");

      switch (mode)
      {
        case readyMode:
          if (audioFrequency == setFrequencySound)
          {
            mode = 1;
          }
          else if (audioFrequency == receiveDataSound)
          {
            mode = 2;
          }
          break;
        case setFrequencyMode:
          irFrequency = audioFrequency + 25000;

          mode = readyMode;
          break;
        case receiveDataMode:
          if (audioFrequency == receiveDataEndSound)
          {
            mode = readyMode;
            irDataIndex = 0;
          }
          else
          {
            irData[irDataIndex] = audioFrequency;

            irDataIndex++;
          }
          break;
        case sendDataMode:
          break;
      }
    }

    audioFrequencyBefore = audioFrequency;
    
  }  // end of loop
