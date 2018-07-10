#pragma once
#include "RelayOutput.h"
#include <TimeLib.h>
class LedPwm : public RelayOutput
{

public:
    uint8_t PwmPin;
    int PwmValue;
    float transitionPeriod;
    int stage;
    bool transitioning;
    LedPwm(uint8_t pin, uint8_t pwm);
    void CheckTime(tmElements_t time);
    void SetTransition(float transition);
};

