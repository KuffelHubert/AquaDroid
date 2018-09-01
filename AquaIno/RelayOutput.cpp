#include "RelayOutput.h"
#include <TimeLib.h>
#include <Arduino.h>

RelayOutput::RelayOutput(uint8_t pin)
{
    Pin = pin;
    pinMode(pin, OUTPUT);
}

void RelayOutput::ToggleState(bool value)
{
    if (value) 
    {
        digitalWrite(Pin, LOW);
        State = false;
    }
    else
    {
        digitalWrite(Pin, HIGH);
        State = true;
    }
}

bool RelayOutput::CheckTime(tmElements_t time)
{
    if (true)
    {
        if (TurnOffHour > TurnOnHour)
        {
            if (time.Hour > TurnOnHour && time.Hour < TurnOffHour)
            {
                Wlacz();
            }
            else
            {
                Wylacz();
            }
        }
        else if (TurnOffHour < TurnOnHour)
        {
            if (time.Hour > TurnOnHour || time.Hour < TurnOnHour)
            {
                Wlacz();
            }
            else
            {
                Wylacz();
            }
        }
        else if (TurnOffHour == TurnOnHour)
        {
            if (TurnOffMinute > TurnOffMinute)
            {
                if (time.Hour > TurnOffHour || time.Hour < TurnOnHour)
                {
                    Wylacz();
                }
                else
                {
                    if (time.Minute < TurnOnMinute || time.Minute > TurnOffMinute)
                    {
                        Wylacz();
                    }
                    else
                    {
                        Wlacz();
                    }
                }
            }
        }
    }
    return false;
}

bool RelayOutput::Wlacz()
{
    digitalWrite(Pin, LOW);
    State = true;
    return true;
}

bool RelayOutput::Wylacz()
{
    digitalWrite(Pin, HIGH);
    State = false;
    return true;
}

void RelayOutput::SetOffHour(uint8_t hour)
{
    TurnOffHour = hour;
}

void RelayOutput::SetOffMinute(uint8_t minute)
{
    TurnOffMinute = minute;
}
void RelayOutput::SetOnHour(uint8_t hour)
{
    TurnOnHour = hour;
}
void RelayOutput::SetOnMinute(uint8_t minute)
{
    TurnOnMinute = minute;
}

void RelayOutput::ToggleManualControl()
{
    Manual = !Manual;
}


