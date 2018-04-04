#pragma once
#include <TimeLib.h>
class RelayOutput
{
    uint8_t Pin;
    bool State;
    bool Manual;
    uint8_t TurnOnHour;
    uint8_t TurnOnMinute;
    uint8_t TurnOffHour;
    uint8_t TurnOffMinute;

public:
    RelayOutput(uint8_t pin);
    void ToggleState();
    void ToggleManualControl();
    bool CheckTime(tmElements_t time);
};

