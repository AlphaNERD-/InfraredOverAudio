#include <Arduino.h>
#include "bell202.h"

bool recordByte = false;
uint8_t currentByte = 0;
uint8_t currentByteIndex = 0;

void setup(){
  pinMode(LED_BUILTIN, OUTPUT);
  bell202::setup(A0);

  pinMode(3,OUTPUT); // Clock (data stable when high)
  pinMode(4,OUTPUT); // Data
}

void loop(){
  digitalWrite(3,LOW);
  int32_t sign = bell202::get_sign();

  if(sign==0) // no carrier accumulated (yet)
    return;

  if(abs(sign)>100){ // sign loud enough
    digitalWrite(4,sign>0);
    digitalWrite(3,HIGH);

    if(sign < 0 && !recordByte){
      recordByte = true;
    }else{
      if (++currentByteIndex > 7)
      {
        recordByte = false;
        currentByteIndex = 0;
      }else{
        currentByte = (currentByte << 1) | (sign>0)&1;
      }
    }

  }else{ // silence
    recordByte = false;

  }
}
