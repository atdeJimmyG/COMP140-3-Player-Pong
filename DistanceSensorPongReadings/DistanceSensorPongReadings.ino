const int trigPinP1 = 10;
const int echoPinP1 = 11;

const int trigPinP2 = 6;
const int echoPinP2 = 5;

const int trigPinP3 = 9;
const int echoPinP3 = 3;

int incomingByte;

long duration;
long duration2;
long duration3;
int distance;
int distance2;
int distance3;

void setup() {
  // put your setup code here, to run once:
  pinMode(trigPinP1, OUTPUT);
  pinMode(echoPinP1, INPUT);
  pinMode(trigPinP2, OUTPUT);
  pinMode(echoPinP2, INPUT);
  pinMode(trigPinP3, OUTPUT);
  pinMode(echoPinP3, INPUT);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available() > 0) { 
    incomingByte = Serial.read();

    digitalWrite(trigPinP1, LOW);
    delayMicroseconds(2);

    digitalWrite(trigPinP1, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPinP1, LOW);

    duration = pulseIn(echoPinP1, HIGH);
    distance = duration * 0.034 / 2;

    digitalWrite(trigPinP2, LOW);
    delayMicroseconds(2);

    digitalWrite(trigPinP2, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPinP2, LOW);

    duration = pulseIn(echoPinP2, HIGH);
    distance2 = duration * 0.034 / 2;

    digitalWrite(trigPinP3, LOW);
    delayMicroseconds(2);

    digitalWrite(trigPinP3, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPinP3, LOW);

    duration = pulseIn(echoPinP3, HIGH);
    distance3 = duration * 0.034 / 2;

    if (incomingByte == 'I') {
      Serial.print(distance);
      Serial.print("-");
      Serial.print(distance2);
      Serial.print("-");
      Serial.println(distance3);
    }
  }
}
