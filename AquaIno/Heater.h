#pragma once
#include "RelayOutput.h"
class Heater : public RelayOutput
{

public:    
    uint8_t TurnOnTemperature;
    uint8_t TurnOffTemperature;
    Heater(uint8_t pin);
    bool CheckTemperature(float temperature);
    void SetOnTemperature(uint8_t temperature);
    void SetOffTemperature(uint8_t temperature);
};