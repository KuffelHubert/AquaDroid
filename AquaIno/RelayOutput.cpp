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
        if (time.Hour == TurnOnHour && time.Minute == TurnOnMinute)
        {
            digitalWrite(Pin, LOW);
            State = true;
            return true;
        }
    }
    return false;
}

void RelayOutput::ToggleManualControl()
{
    Manual = !Manual;
}


