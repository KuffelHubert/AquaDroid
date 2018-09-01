#include "Heater.h"
#include <Arduino.h>

Heater::Heater(uint8_t pin) : RelayOutput(pin)
{ }

bool Heater::CheckTemperature(float temperature) 
{
    if (temperature > TurnOffTemperature)
    {
        digitalWrite(Pin, HIGH);
    }
    else if (temperature < TurnOnTemperature)
    {
        digitalWrite(Pin, LOW);
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

