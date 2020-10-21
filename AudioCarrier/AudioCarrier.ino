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
    timeNow = TCNT1;
    TCNT1 = 0; //reset timer register
  }

void setup ()
  {
    Serial.begin (115200);
    Serial.println ("Started.");
    ADCSRB = 0;           // (Disable) ACME: Analog Comparator Multiplexer Enable
    ACSR =  bit (ACI)     // (Clear) Analog Comparator Interrupt Flag
          | bit (ACIE)    // Analog Comparator Interrupt Enable
          | bit (ACIS1);  // ACIS1, ACIS0: Analog Comparator Interrupt Mode Select (trigger on falling edge)

    TCCR1A = 0;
    TCCR1B |= (0 << CS12) | (1 << CS11) | (0 >> CS10); //Activate timer and set prescaler to 8
    TCNT1 = 0; //Reset timer register
   }  // end of setup

void loop ()
  {
    //Calculate frequency by using the period duration
    //determined through the timestamps captured in
    //ISR method.
    //delta = timeNow - timeBefore;
    audioFrequency = 1000000.0 / (float(timeNow) * 4.0);

    if (millis() % 1000 == 0)
    {
      Serial.print (audioFrequency, 5);
      Serial.print (" Hz, ");
      Serial.print (timeNow);
      Serial.println (" TCNT0");
    }

    //Do not process the same frequency multiple times
    if (audioFrequency != audioFrequencyBefore)
    {
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
