#pragma once
#include "RelayOutput.h"
#include <TimeLib.h>
class LedPwm : public RelayOutput
{

public:
    uint8_t PwmPin;
    int PwmValue;
    int transitionPeriod;
    int stage;
    bool transitioning;
    long nextTransition;
    LedPwm(uint8_t pin, uint8_t pwm);
    void CheckTime(tmElements_t time);
    void SetTransition(int transition);
    long GetSeconds(tmElements_t time);
};

