#include "Heater.h"
#include <Arduino.h>

bool Heater::CheckTemperature(uint8_t temperature) 
{
    if (temperature > TurnOffTemperature)
    {
        digitalWrite(Pin, LOW);
    }
    if (temperature < TurnOnTemperature)
    {
        digitalWrite(Pin, HIGH);
    }
    return true;
}

void Heater::SetOnTemperature(uint8_t temperature)
{
    TurnOnTemperature = temperature;
}

void Heater::SetOffTemperature(uint8_t temperature)
{
    TurnOffTemperature = temperature;
}

