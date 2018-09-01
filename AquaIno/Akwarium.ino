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
#include "Heater.h"

using namespace std;

RelayOutput** Relays = new RelayOutput*[8];


const int chipSelect = A0;
const String fileName = "akw7.txt";

DS1302RTC RTC(A5,A4,A2);
OneWire oneWire(2);
DallasTemperature sensors(&oneWire);
// the setup function runs once when you press reset or power the board
void setup()
{

    Serial.begin(9600);
    sensors.begin();
    if (!SD.begin(chipSelect))
    {
        //while (1);
    }

    Relays[0] = new Heater(4);
    Relays[1] = new LedPwm(3, 5);
    Relays[2] = new LedPwm(A1, 6);
    Relays[3] = new RelayOutput(8);
    Relays[4] = new RelayOutput(9);
    Relays[5] = new RelayOutput(10);
    Relays[6] = new RelayOutput(7);
    Relays[7] = new RelayOutput(A3);


    if (!SD.exists(fileName))
    {
        SaveSettings();
    }
    else
    {
        ReadSettings();
    }
}

// the loop function runs over and over again until power down or reset
void loop()
{
    sensors.requestTemperatures(); 
    float temperatura = sensors.getTempCByIndex(0);

    RTC.get();

    tmElements_t tm;
    if (!RTC.read(tm))
    {
        for (size_t i = 0; i <= 7; i++)
        {
            if (i == 0)
            {
                ((Heater*)Relays[i])->CheckTemperature(temperatura);
            }
            else
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
    }

    if (Serial.available())
    {
        String data = (String)Serial.readString();
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
            Serial.println("tran" + (String)index + (String)((LedPwm*)Relays[index])->transitionPeriod);
        }
        else if (action == "data")
        {
            String dataActionString = "data";
            String time = (String)tm.Hour + ":" + (String)tm.Minute + ";";
            Serial.println(dataActionString + time + GatherData());
        }
        else if (action == "tofm")
        {
            int h = data.substring(5).toInt();
            Relays[index]->SetOffMinute(h);
            Serial.println("tofm" + (String)index + (String)Relays[index]->TurnOffMinute);
        }
        else if (action == "tofh")
        {
            int h = data.substring(5).toInt();
            Relays[index]->SetOffHour(h);
            Serial.println("tofh" + (String)index + (String)Relays[index]->TurnOffHour);
        }
        else if (action == "tonm")
        {
            int h = data.substring(5).toInt();
            Relays[index]->SetOnMinute(h);
            Serial.println("tonm" + (String)index + (String)Relays[index]->TurnOnMinute);
        }
        else if (action == "tonh")
        {
            int h = data.substring(5).toInt();
            Relays[index]->SetOnHour(h);
            Serial.println("tonh" + (String)index + (String)Relays[index]->TurnOnHour);
        }
        else if (action == "temo")
        {
            int h = data.substring(5).toInt();
            ((Heater*)Relays[index])->SetOnTemperature(h);
            Serial.println("temo" + (String)((Heater*)Relays[index])->TurnOnTemperature);
        }
        else if (action == "temf")
        {
            int h = data.substring(5).toInt();
            ((Heater*)Relays[index])->SetOffTemperature(h);
            Serial.println("temf" + (String)((Heater*)Relays[index])->TurnOffTemperature);
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

    delay(1000);
}

void SaveSettings()
{
    SD.remove(fileName);
    File myFile = SD.open(fileName, FILE_WRITE);
    String data = GatherData();
    myFile.println(data);
    myFile.flush();
    myFile.close();
}

void ReadSettings()
{
    File myFile = SD.open(fileName, FILE_READ);

    for (size_t i = 0; i <= 7; i++)
    {
        if (i == 0)
        {
            String readed = myFile.readStringUntil(';');
            ((Heater*)Relays[i])->TurnOnTemperature = readed.toInt();

            readed = myFile.readStringUntil(';');
            ((Heater*)Relays[i])->TurnOffTemperature = readed.toInt();
        }
        else
        {
            String readed = myFile.readStringUntil(';');
            Relays[i]->TurnOnHour = readed.toInt();

            readed = myFile.readStringUntil(';');
            Relays[i]->TurnOnMinute = readed.toInt();

            readed = myFile.readStringUntil(';');
            Relays[i]->TurnOffHour = readed.toInt();

            readed = myFile.readStringUntil(';');
            Relays[i]->TurnOffMinute = readed.toInt();
            if (i == 1 || i == 2)
            {
                String readed = myFile.readStringUntil(';');
                ((LedPwm*)Relays[i])->transitionPeriod = readed.toInt();
            }
        }
    }
    myFile.close();
}

String GatherData()
{
    String data = "";
    for (size_t i = 0; i <= 7; i++)
    {
        if (i == 0)
        {
            data += (String)((Heater*)Relays[i])->TurnOnTemperature + ";";
            data += (String)((Heater*)Relays[i])->TurnOffTemperature + ";";
        }
        else
        {
            data += (String)Relays[i]->TurnOnHour + ";";
            data += (String)Relays[i]->TurnOnMinute + ";";
            data += (String)Relays[i]->TurnOffHour + ";";
            data += (String)Relays[i]->TurnOffMinute + ";";
            if (i == 1 || i == 2)
            {
                data += (String)((LedPwm*)Relays[i])->transitionPeriod + ";";
            }
        }
    }
    return data;
}
