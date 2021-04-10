bool laemp = false;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(24000, SERIAL_8O1);
  pinMode(2, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available() > 0)
  {
    byte incomingByte = Serial.read();

    if (incomingByte = 100)
    {
      if (!laemp)
      {
        digitalWrite(2, HIGH);
        laemp = true;
      }
      else
      {
        digitalWrite(2, LOW);
        laemp = false;
      }
    }
    
    Serial.println ("Value: " + incomingByte);
  }
}
