#include <Arduino.h>
#include "bell202.h"

bool recordByte = false;
uint8_t currentByte = 0;
uint8_t currentByteIndex = 0;

void setup(){
  pinMode(LED_BUILTIN, OUTPUT);
  bell202::setup(A0);
}

void loop(){
  int32_t sign = bell202::get_sign();

  if(sign==0) // no carrier accumulated (yet)
    return;

  if(abs(sign)>1000){ // sign loud enough
    if(sign < 0 && !recordByte){
      recordByte = true;
    }else{
      if (++currentByteIndex > 7)
      {
        recordByte = false;
        currentByteIndex = 0;

        Serial.begin(115200);
        Serial.println(currentByte, HEX);
        Serial.end();
      }else{
        currentByte = (currentByte >> 1) | ((sign>0)<<7);
      }
    }
  }else{ // silence

  }
}
