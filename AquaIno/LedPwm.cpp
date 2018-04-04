#include "LedPwm.h"
#include <TimeLib.h>

LedPwm::LedPwm(uint8_t pin, uint8_t pwmPin) : RelayOutput(pin)
{
    PwmPin = pwmPin;
}

void LedPwm::CheckTime(tmElements_t time)
{
    if (RelayOutput::CheckTime(time))
    {

    }
}