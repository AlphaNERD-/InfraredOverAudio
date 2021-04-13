#include "Arduino.h"

#define NUM_SAMPLES 100
#define ADC_MIDPOINT 512
#define AUDIO_PIN 23

#define SAMPLE_FREQ 35562

int status = 0;
int prevStatus = 0;

void setup() {
  Serial.begin(115200);
  pinMode(AUDIO_PIN, INPUT);
  ADCSRA = (ADCSRA & B11111000) | 5;
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
  return Q1*Q1 + Q2*Q2 - coeff*Q1*Q2;
}

void loop() {
  int sampleData[NUM_SAMPLES];

  //int before = micros();
  for (int i = 0; i < NUM_SAMPLES; i++) {
    sampleData[i] = analogRead(AUDIO_PIN);
  }
  //int after = micros();

  //Serial.println(after-before);
  
  Serial.print(goertzel(sampleData,NUM_SAMPLES,1633));
  Serial.print(" ");
  Serial.print(goertzel(sampleData,NUM_SAMPLES,1477));
  Serial.print(" ");
  Serial.print(goertzel(sampleData,NUM_SAMPLES,1336));
  Serial.print(" ");
  Serial.print(goertzel(sampleData,NUM_SAMPLES,1209));
  Serial.print(" ");
  Serial.print(goertzel(sampleData,NUM_SAMPLES,941));
  Serial.print(" ");
  Serial.print(goertzel(sampleData,NUM_SAMPLES,852));
  Serial.print(" ");
  Serial.print(goertzel(sampleData,NUM_SAMPLES,770));
  Serial.print(" ");
  Serial.println(goertzel(sampleData,NUM_SAMPLES,697));
}
