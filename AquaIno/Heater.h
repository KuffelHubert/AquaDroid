#pragma once
#include "RelayOutput.h"
class LedPwm : public RelayOutput
{

public:    
    uint8_t TurnOnTemperature;
    uint8_t TurnOffTemperature;
    bool CheckTemperature(uint8_t temperature);
    void SetOnTemperature(uint8_t temperature);
    void SetOffTemperature(uint8_t temperature);
};