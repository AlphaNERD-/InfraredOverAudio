#include "Arduino.h"

#define NUM_SAMPLES 100
#define ADC_MIDPOINT 512
#define AUDIO_PIN 23

#define SAMPLE_FREQ 64516

  int status = 0;
  int prevStatus = 0;

  void setup() {
    Serial.begin(115200);
    pinMode(AUDIO_PIN, INPUT);
    ADCSRA = (ADCSRA & B11111000) | 4;
  }
  
  // magnitude-only goertzel algo operating on array, adapted from https://github.com/jacobrosenthal/Goertzel
  float goertzel(int samples[], int num_samples, float freq){
    float Q1 = 0;
    float Q2 = 0;
    float coeff = 2.0 * cos((2.0 * PI * freq) / SAMPLE_FREQ);
    for (int i = 0; i < num_samples; i++) {
      float Q0 = coeff * Q1 - Q2 + (float) (samples[i] - ADC_MIDPOINT);
      Q2 = Q1;
      Q1 = Q0;
    }
    return sqrt(Q1*Q1 + Q2*Q2 - coeff*Q1*Q2);
  }

  void loop() {
    int sampleData[NUM_SAMPLES];

    int before = millis();
    for(int i=0; i < NUM_SAMPLES; i++){
      sampleData[i] = analogRead(AUDIO_PIN);
    }
    int after = millis();

    //Serial.println(after - before);

    /*Serial.print(goertzel(sampleData,NUM_SAMPLES,5200));
    Serial.print(" ");
    Serial.print(goertzel(sampleData,NUM_SAMPLES,4600));
    Serial.print(" ");
    Serial.print(goertzel(sampleData,NUM_SAMPLES,4000));
    Serial.print(" ");
    Serial.print(goertzel(sampleData,NUM_SAMPLES,3400));
    Serial.print(" ");
    Serial.print(goertzel(sampleData,NUM_SAMPLES,2800));
    Serial.print(" ");
    Serial.print(goertzel(sampleData,NUM_SAMPLES,2200));
    Serial.print(" ");
    Serial.print(goertzel(sampleData,NUM_SAMPLES,1600));
    Serial.print(" ");
    Serial.print(goertzel(sampleData,NUM_SAMPLES,1000));
    Serial.print(" ");
    Serial.println(goertzel(sampleData,NUM_SAMPLES,400));*/

    if (goertzel(sampleData,NUM_SAMPLES,400) > 12000)
    {
      Serial.println(00);
    }
    else
    {
      int number = 0;
      
      if (goertzel(sampleData,NUM_SAMPLES,1000) > 200)
        number = number + 1;
      if (goertzel(sampleData,NUM_SAMPLES,1600) > 200)
        number = number + 2;
      if (goertzel(sampleData,NUM_SAMPLES,2200) > 200)
        number = number + 4;
      if (goertzel(sampleData,NUM_SAMPLES,2800) > 200)
        number = number + 8;
      if (goertzel(sampleData,NUM_SAMPLES,3400) > 200)
        number = number + 16;
      if (goertzel(sampleData,NUM_SAMPLES,4000) > 200)
        number = number + 32;
      if (goertzel(sampleData,NUM_SAMPLES,4600) > 200)
        number = number + 64;
      if (goertzel(sampleData,NUM_SAMPLES,5200) > 200)
        number = number + 128;

      if (number > 0)
        Serial.println(number);
    }
  }
