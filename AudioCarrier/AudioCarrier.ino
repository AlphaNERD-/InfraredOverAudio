#include <Arduino.h>
#include "bell202.h"

bool recordByte = false;
uint8_t currentByte = 0;
int currentByteIndex = 0;

void setup(){
  pinMode(LED_BUILTIN, OUTPUT);
  bell202::setup(A0);
  Serial.begin(115200);
}

void loop(){
  int32_t sign = bell202::get_sign();
  
  if(sign==0) // no carrier accumulated (yet)
    return;

  if(abs(sign)>1000){ // sign loud enough
    if(sign>0){ // mark = 2200Hz
      //digitalWrite(LED_BUILTIN,HIGH);
      if (recordByte)
      {
        if (currentByteIndex > 7)
        {
          recordByte = false;
          currentByteIndex = 0;
          
          Serial.println(currentByte);
        }
        
        currentByte = (currentByte << 1) | 1;
        currentByteIndex++;
      }
      
    }else{      // space = 1200Hz
      //digitalWrite(LED_BUILTIN,LOW);

      if (!recordByte)
      {
        recordByte = true;
      }
      else
      {
        currentByte = (currentByte << 1) | 0;
        currentByteIndex++;
      }
    }
  }else{ // silence

  }
}
