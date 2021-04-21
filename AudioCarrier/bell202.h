#include <Arduino.h>
#include <util/atomic.h>

namespace bell202 {

  int readpin = A0;

  volatile uint16_t ringbuffer[8];
  volatile uint8_t ringbuffer_index=0;
  const static int8_t coeffloi[] = { 64,  45,   0, -45, -64, -45,   0,  45};
  const static int8_t coeffloq[] = {  0,  45,  64,  45,   0, -45, -64, -45};
  const static int8_t coeffhii[] = { 64,   8, -62, -24,  55,  39, -45, -51};
  const static int8_t coeffhiq[] = {  0,  63,  17, -59, -32,  51,  45, -39};

  volatile int32_t current_sign = 0;
  volatile int32_t accumulated_sign=0;
  volatile bool have_full_carrier=false;

  ISR(TIMER1_COMPA_vect) { // 9603.84Hz
    ATOMIC_BLOCK(ATOMIC_RESTORESTATE){
      ringbuffer[ringbuffer_index++]=analogRead(readpin);
      if(ringbuffer_index>=8){
        ringbuffer_index=0;
        have_full_carrier = true;
      }
    }
    int16_t outloi = 0, outloq = 0, outhii = 0, outhiq = 0;
    for (int i = 0; i < 8; i++) {
      int8_t sample = (ringbuffer[(i+ringbuffer_index)%8] - 512)>>2;
      outloi += sample * coeffloi[i];
      outloq += sample * coeffloq[i];
      outhii += sample * coeffhii[i];
      outhiq += sample * coeffhiq[i];
    }
    ATOMIC_BLOCK(ATOMIC_RESTORESTATE){
      current_sign = ((outhii >> 8) * (outhii >> 8) + (outhiq >> 8) * (outhiq >> 8)
                   - (outloi >> 8) * (outloi >> 8) - (outloq >> 8) * (outloq >> 8));
      accumulated_sign += current_sign / 8;
    }
  }

  int32_t get_sign(){
    int32_t ret;
    ATOMIC_BLOCK(ATOMIC_RESTORESTATE){
      if(!have_full_carrier)
        return 0;
      ret = accumulated_sign;
      have_full_carrier = false;
      accumulated_sign = 0;
    }
    if(ret==0)
      ret=-1;
    return ret;
  }

  void setup(int p) {
    noInterrupts();

    // setup readpin

    readpin = p;
    pinMode(p,INPUT);
    ADCSRA = (ADCSRA & B11111000) | 2;

    // setup timer

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

};
