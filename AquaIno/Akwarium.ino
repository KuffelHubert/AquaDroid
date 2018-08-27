/*
 Name:		Akwarium.ino
 Created:	3/28/2018 5:55:22 PM
 Author:	Hubert
*/

#include <DS1302RTC.h>
#include <SD.h>
#include <OneWire.h>
#include <DallasTemperature.h>
#include "RelayOutput.h"
#include "LedPwm.h"

using namespace std;

#define Heater A7;

RelayOutput** Relays = new RelayOutput*[8];


const int chipSelect = A0;
const String fileName = "akw6.txt";

DS1302RTC RTC(A5,A4,A6);
OneWire oneWire(2); //Pod³¹czenie do A5
DallasTemperature sensors(&oneWire);
// the setup function runs once when you press reset or power the board
void setup()
{

    Serial.begin(9600);
    sensors.begin();
    if (!SD.begin(chipSelect))
    {
        Serial.println("initialization failed!");
        while (1);
    }
    Serial.println("initialization done.");

    Relays[0] = new RelayOutput(3);
    Relays[1] = new LedPwm(4, 5);
    Relays[2] = new LedPwm(7, 6);
    Relays[3] = new RelayOutput(8);
    Relays[4] = new RelayOutput(9);
    Relays[5] = new RelayOutput(2);
    Relays[6] = new RelayOutput(A1);
    Relays[7] = new RelayOutput(10);


    if (!SD.exists(fileName))
    {
        Serial.print("tworzenie pliku");
        SaveSettings();
    }
    else
    {
        Serial.println("plik juz istnieje");
        ReadSettings();
    }
}

void SaveSettings()
{
    SD.remove(fileName);
    File myFile = SD.open(fileName, FILE_WRITE);
    for (size_t i = 0; i <= 7; i++)
    {
        myFile.println((String)Relays[i]->TurnOnHour + ";");
        myFile.println((String)Relays[i]->TurnOnMinute + ";");
        myFile.println((String)Relays[i]->TurnOffHour + ";");
        myFile.println((String)Relays[i]->TurnOffMinute + ";");
        if (i == 1 || i == 2)
        {
            myFile.println((String)((LedPwm*)Relays[i])->transitionPeriod + ";");
        }
    }
    myFile.flush();
    myFile.close();
    Serial.print("zapisano ustawienia");
}

void ReadSettings()
{
    File myFile = SD.open(fileName, FILE_READ);

    for (size_t i = 0; i <= 7; i++)
    {
        Serial.println("relay: " + (String)i);
        String readed = myFile.readStringUntil(';');
        Serial.println(readed.toInt());
        Relays[i]->TurnOnHour = readed.toInt();
        readed = myFile.readStringUntil(';');
        Serial.println(readed.toInt());
        Relays[i]->TurnOnMinute = readed.toInt();
        readed = myFile.readStringUntil(';');
        Serial.println(readed.toInt());
        Relays[i]->TurnOffHour = readed.toInt();
        readed = myFile.readStringUntil(';');
        Serial.println(readed.toInt());
        Relays[i]->TurnOffMinute = readed.toInt();
        if (i == 1 || i == 2)
        {
            String readed = myFile.readStringUntil(';');
            Serial.println(readed.toInt());
            ((LedPwm*)Relays[i])->transitionPeriod = readed.toInt();
        }
    }
    myFile.close();
}

// the loop function runs over and over again until power down or reset
void loop()
{
    sensors.requestTemperatures(); //Pobranie temperatury czujnika
    Serial.print("Aktualna temperatura: ");
    Serial.println(sensors.getTempCByIndex(0));
    if (Serial.available())
    {
        String data = (String)Serial.readString();
        Serial.println(data);
        if (data.length() > 10)
        {
            return;
        }
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
        else if (action == "stan")
        {
            int value = data.substring(5).toInt();
            if (value == 0)
            {
                Relays[index]->ToggleState(false);
            }
            else
            {
                Relays[index]->ToggleState(true);
            }
        }
        SaveSettings();
    }
 //   RTC.get();

    tmElements_t tm;
    if (false)//!RTC.read(tm))
    {
        for (size_t i = 0; i <= 7; i++)
        {
            if (i != 1 && i != 2)
            {
                Relays[i]->CheckTime(tm);
            }
            else
            {
                ((LedPwm*)Relays[i])->CheckTime(tm);
            }
        }
    }

    delay(100);
}
