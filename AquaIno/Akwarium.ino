/*
 Name:		Akwarium.ino
 Created:	3/28/2018 5:55:22 PM
 Author:	Hubert
*/

#include <DS1302RTC.h>
#include "RelayOutput.h"
#include "LedPwm.h"
using namespace std;

#define Heater A7;

RelayOutput** Relays = new RelayOutput*[8];

#define DS1302_GND_PIN 33
#define DS1302_VCC_PIN 35

DS1302RTC RTC(A4, A5, A6);
// the setup function runs once when you press reset or power the board
void setup()
{

    //digitalWrite(DS1302_GND_PIN, LOW);
    //pinMode(DS1302_GND_PIN, OUTPUT);

    //digitalWrite(DS1302_VCC_PIN, HIGH);
    //pinMode(DS1302_VCC_PIN, OUTPUT);

    Relays[0] = new RelayOutput(11);
    Relays[1] = new LedPwm(2, 5);
    Relays[2] = new LedPwm(3, 6);
    Relays[3] = new RelayOutput(12);
    Relays[4] = new RelayOutput(4);
    Relays[5] = new RelayOutput(7);
    Relays[6] = new RelayOutput(8);
    Relays[7] = new RelayOutput(9);

    Serial.begin(9600);
}

// the loop function runs over and over again until power down or reset
void loop()
{
    if (Serial.available())
    {
        String data = (String)Serial.readString();
        String action = data.substring(0, 4);
        String indexString = data.substring(4, 5);
        int index = indexString.toInt();
        if (action == "tran")
        {
            int h = data.substring(5).toInt();
            ((LedPwm*)Relays[index])->SetTransition(h);
        }
        else if (action == "data")
        {
            String sendData = "";
            for (size_t i = 0; i < 8; i++)
            {
                    sendData += Relays[i]->Manual;
                    sendData += Relays[i]->TurnOnHour;
                    sendData += Relays[i]->TurnOnMinute;
                    sendData += Relays[i]->TurnOffHour;
                    sendData += Relays[i]->TurnOffMinute;
                    if (i == 1 || i == 2)
                    {
                        sendData += ((LedPwm*)Relays[i])->transitionPeriod;
                    }
                    sendData += Relays[i]->State;
            }
            Serial.print(sendData);
        }
        else if (action == "tofm")
        {
            int h = data.substring(5).toInt();
            Relays[index]->SetOffMinute(h);
        }
        else if (action == "tofh")
        {
            int h = data.substring(5).toInt();
            Relays[index]->SetOffHour(h);
        }
        else if (action == "tonm")
        {
            int h = data.substring(5).toInt();
            Relays[index]->SetOnMinute(h);
        }
        else if (action == "tonh")
        {
            int h = data.substring(5).toInt();
            Relays[index]->SetOnHour(h);
        }
        else if (action == "ster")
        {
            Relays[index]->ToggleManualControl();
        }
        else if (action == "stan")
        {
            Relays[index]->ToggleState();
        }
    }
    //Serial.print(RTC.get());

    //tmElements_t tm;
    //if (!RTC.read(tm))
    //{
    //    for (size_t i = 0; i < 7; i++)
    //    {
    //        if (i != 1 && i != 2)
    //        {
    //            Relays[i]->CheckTime(tm);
    //        }
    //        else
    //        {
    //            ((LedPwm*)Relays[i])->CheckTime(tm);
    //        }
    //    }
    //}

    delay(100);
}
