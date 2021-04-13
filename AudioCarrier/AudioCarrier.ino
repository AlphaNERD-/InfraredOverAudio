#include "Arduino.h"
#include "goertzel_dtmf.h"

#define NUM_SAMPLES 100
#define ADC_MIDPOINT 512
#define AUDIO_PIN 23

#define SAMPLE_FREQ 64516

GoertzelDTMF decoder(ADC_MIDPOINT, SAMPLE_FREQ, 6000);

int status = 0;
int prevStatus = 0;

void setup() {
  Serial.begin(115200);
  pinMode(AUDIO_PIN, INPUT);
  ADCSRA = (ADCSRA & B11111000) | 4;
}

void loop() {
  int sampleData[NUM_SAMPLES];

  int before = millis();
  for (int i = 0; i < NUM_SAMPLES; i++) {
    sampleData[i] = analogRead(AUDIO_PIN);
  }
  int after = millis();

  uint8_t nibble = decoder.decode(sampleData, NUM_SAMPLES);
  if (nibble == 0xff)
    // stille
    Serial.println(-1);
  else
    // nibble == decoded 0..16
    Serial.println(nibble);
}
