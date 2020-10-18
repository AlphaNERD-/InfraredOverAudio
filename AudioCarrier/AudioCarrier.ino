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
volatile float audioFrequencyBefore;
volatile float audioFrequency;

volatile float irFrequency;
volatile int irData[100];
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
    audioFrequency = 1.0 / ((float)delta / 1000000.0);

    //Do not process the same frequency multiple times
    if (audioFrequency != audioFrequencyBefore)
    {
      if (millis() % 1000 == 0)
      {
        Serial.print (audioFrequency, 10);
        Serial.print (" Hz, ");
        Serial.print (delta);
        Serial.println (" microsecs");
      }

      switch (mode)
      {
        case readyMode:
          if (audioFrequency == setFrequencySound)
          {
            mode = 1;

            Serial.println ("Start setting IR frequency.");
          }
          else if (audioFrequency == receiveDataSound)
          {
            mode = 2;

            Serial.println ("Start receiving IR data.");
          }
          break;
        case setFrequencyMode:
          irFrequency = audioFrequency + 25000;

          Serial.print ("IR frequency set to ");
          Serial.print (irFrequency, 10);
          Serial.println (" Hz.");
          
          mode = readyMode;
          break;
        case receiveDataMode:
          if (audioFrequency == receiveDataEndSound)
          {
            Serial.println ("Stop receiving IR data.");
            
            mode = readyMode;
            irDataIndex = 0;
          }
          else
          {
            irData[irDataIndex] = audioFrequency;

            irDataIndex++;

            Serial.print ("IR pulse received: ");
            Serial.print (irData[irDataIndex]);
            Serial.println (" microsecs.");
          }
          break;
        case sendDataMode:
          Serial.println ("Sending IR data...");
          break;
      }
    }

    audioFrequencyBefore = audioFrequency;
    
  }  // end of loop
