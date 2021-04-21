#include <Arduino.h>
#include "bell202.h"

void setup(){
  pinMode(LED_BUILTIN, OUTPUT);
  bell202::setup(A0);
}

void loop(){
  int32_t sign = bell202::get_sign();

  if(sign==0) // no carrier accumulated (yet)
    return;

  if(abs(sign)>1000){ // sign loud enough
    if(sign>0){ // mark = 2200Hz
      digitalWrite(LED_BUILTIN,HIGH);
    }else{      // space = 1200Hz
      digitalWrite(LED_BUILTIN,LOW);
    }
  }else{ // silence

  }
}
