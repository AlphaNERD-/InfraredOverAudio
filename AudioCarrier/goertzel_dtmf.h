class GoertzelDTMF{
public:
  GoertzelDTMF(int adc_midpoint, int sample_rate, float threshold):
    adc_midpoint(adc_midpoint), sample_rate(sample_rate), threshold(threshold)
  {
    for(int i=0; i<8;i++){
      coeffs[i] = 2.0 * cos((2.0 * PI * f_tone[i]) / sample_rate);
    }
  }

  // returns 0xff for silence or 0..0x0f for detected nibble
  uint8_t decode(int samples[], int num_samples){
    float max=threshold;
    uint8_t row=0xff;
    uint8_t col=0xff;
    for(int i=0;i<4;i++){
      float cur=squared_goertzel(samples,num_samples,coeffs[i]);
      if(cur>max){
        max=cur;
        row=i;
      }
    }
    max=threshold;
    for(int i=4;i<8;i++){
      float cur=squared_goertzel(samples,num_samples,coeffs[i]);
      if(cur>max){
        max=cur;
        col=i;
      }
    }
    if(row == 0xff || col == 0xff){
      return 0xff;
    }
    return row*4 + col;
  }

protected:
  float squared_goertzel(int samples[], int num_samples, float coeff){
    float Q1 = 0;
    float Q2 = 0;
    for (int i = 0; i < num_samples; i++) {
      float Q0 = coeff * Q1 - Q2 + (float) (samples[i] - adc_midpoint);
      Q2 = Q1;
      Q1 = Q0;
    }
    return Q1*Q1 + Q2*Q2 - coeff*Q1*Q2;
  }
private:
  const int f_tone[8] = { 697, 770, 852, 941, 1209, 1336, 1477, 1633 }; // DTMF
  int coeffs[8];
  int adc_midpoint;
  int sample_rate;
  float threshold;
};
