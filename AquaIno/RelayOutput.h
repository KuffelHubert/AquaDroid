#pragma once
#include <TimeLib.h>
class RelayOutput
{

public:
    uint8_t Pin;
    bool State;
    bool Manual;
    uint8_t TurnOnHour;
    uint8_t TurnOnMinute;
    uint8_t TurnOffHour;
    uint8_t TurnOffMinute;
    RelayOutput(uint8_t pin);
    void ToggleState(bool value);
    void ToggleManualControl();
    void SetOffHour(uint8_t hour);
    void SetOffMinute(uint8_t hour);
    void SetOnHour(uint8_t hour);
    void SetOnMinute(uint8_t hour);
    bool CheckTime(tmElements_t time);
};

