#pragma once
#include "RelayOutput.h"
#include <TimeLib.h>
class LedPwm : public RelayOutput
{
    uint8_t PwmPin;
    int PwmValue;
    float transitionPeriod;
    bool transitioning;

public:
    LedPwm(uint8_t pin, uint8_t pwm);
    void CheckTime(tmElements_t time);
};

