#include "RelayOutput.h"
#include <TimeLib.h>
#include <Arduino.h>

RelayOutput::RelayOutput(uint8_t pin)
{
    Pin = pin;
    pinMode(pin, OUTPUT);
}

void RelayOutput::ToggleState()
{
    Serial.print(State);
    if (State) 
    {
        digitalWrite(Pin, HIGH);
        State = false;
    }
    else
    {
        digitalWrite(Pin, LOW);
        State = true;
    }
}

bool RelayOutput::CheckTime(tmElements_t time)
{
    if (!Manual)
    {
        if (time.Hour == TurnOffHour && time.Minute == TurnOffMinute)
        {
            digitalWrite(Pin, HIGH);
            State = false;
            return true;
        }
        else if (time.Hour == TurnOnHour && time.Minute == TurnOnMinute)
        {
            digitalWrite(Pin, LOW);
            State = true;
            return true;
        }
    }
    return false;
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


