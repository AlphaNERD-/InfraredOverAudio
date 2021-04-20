#include <Arduino.h>
#define NOP __asm__ __volatile__ ("nop\n\t")

void setupTimer1() { // call from setup()
  noInterrupts();
  TCCR1A = 0;
  TCCR1B = 0;
  TCNT1 = 0;
  // 9603.841536614646 Hz=(16000000/((1665+1)*1))
  OCR1A = 1665;
  // CTC
  TCCR1B |= (1 << WGM12);
  // Prescaler 1
  TCCR1B |= (1 << CS10);
  // Output Compare Match A Interrupt Enable
  TIMSK1 |= (1 << OCIE1A);
  interrupts();
}

ISR(TIMER1_COMPA_vect) {
  //NOP;
  //NOP;
  //NOP;
  //TCNT1=0;
  digitalWrite(3,HIGH);
  delayMicroseconds(10);
  digitalWrite(3,LOW);
}

void setup(){
  pinMode(3,OUTPUT);
  setupTimer1();
}

void loop(){
}
