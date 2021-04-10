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

    //Serial.print(goertzel(sampleData,NUM_SAMPLES,1000));
    //Serial.print(" ");
    //Serial.println(goertzel(sampleData,NUM_SAMPLES,400));

    if (goertzel(sampleData,NUM_SAMPLES,1000) > 12000)
      status = 2;
    else if (goertzel(sampleData,NUM_SAMPLES,400) > 12000)
      status = 1;
    else
      status = 0;

    if (status != prevStatus)
    {
      switch (status)
      {
        case 0:
          Serial.println(-1);
          break;
        case 1:
          Serial.println(0);
          break;
        case 2:
          Serial.println(1);
          break;
      }

      prevStatus = status;
    }
  }
