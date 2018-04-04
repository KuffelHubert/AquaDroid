/*
 Name:		Akwarium.ino
 Created:	3/28/2018 5:55:22 PM
 Author:	Hubert
*/

#include <DS1302RTC.h>
#include "RelayOutput.h"
#include "LedPwm.h"

#define Heater A7;
#define Led1 2;
#define Led2 3;
#define Out4 4;
#define Out5 7;
#define Out6 8;
#define Out7 9;
#define Out8 12;

#define Pwm1 5;
#define Pwm2 6;

RelayOutput** Relays = new RelayOutput*[4];


DS1302RTC RTC(A4, A5, A6);
// the setup function runs once when you press reset or power the board
void setup() {
    Relays[0] = new RelayOutput(A7);
    Relays[1] = new LedPwm(2, 5);
    Relays[2] = new LedPwm(3, 6);
    Relays[3] = new RelayOutput(8);

    Serial.begin(9600);
}

// the loop function runs over and over again until power down or reset
void loop() {
    if (Serial.available()) {
        char data = Serial.read();
        switch (data)
        {
        case 'a':
            Relays[1]->ToggleState();
            break;
        default:
            break;
        }
    }
}