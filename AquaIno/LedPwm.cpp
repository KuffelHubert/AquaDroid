#include "LedPwm.h"
#include <TimeLib.h>
#include "Arduino.h"

LedPwm::LedPwm(uint8_t pin, uint8_t pwmPin) : RelayOutput(pin)
{
    PwmPin = pwmPin;
}

void LedPwm::CheckTime(tmElements_t time)
{
    if (RelayOutput::CheckTime(time))
    {
        if (!transitioning)
        {
            transitioning = true;
        }
    }
    if (transitioning)
    {
        if (RelayOutput::State)
        {
            PwmValue++;
        }
        else
        {
            PwmValue--;
        }
        if (PwmValue <= 0 || PwmValue >= 255)
        {
            transitioning = false;
        }
        analogWrite(PwmPin, PwmValue);
    }
}

void LedPwm::SetTransition(float transition)
{
    transitionPeriod = transition;
}