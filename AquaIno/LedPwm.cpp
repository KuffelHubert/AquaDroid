#include "LedPwm.h"
#include <TimeLib.h>
#include "Arduino.h"

LedPwm::LedPwm(uint8_t pin, uint8_t pwmPin) : RelayOutput(pin)
{
    PwmPin = pwmPin;
}

void LedPwm::CheckTime(tmElements_t time)
{
    if (!Manual)
    {
        if (time.Hour == TurnOffHour && time.Minute == TurnOffMinute)
        {
            transitioning = true;
        }
        else if (time.Hour == TurnOnHour && time.Minute == TurnOnMinute)
        {
            transitioning = true;
        }
        if (transitioning)
        {
            stage++;
            if (stage >= transitionPeriod)
            {
                stage = 0;
                if (RelayOutput::State)
                {
                    PwmValue--;
                }
                else
                {
                    PwmValue++;
                }
                if (PwmValue <= 0)
                {
                    digitalWrite(Pin, HIGH);
                    State = false;
                    transitioning = false;
                }
                else if (PwmValue >= 255)
                {
                    digitalWrite(Pin, LOW);
                    State = true;
                    transitioning = false;
                }
                analogWrite(PwmPin, PwmValue);
            }
        }
    }
}

void LedPwm::SetTransition(float transition)
{
    transitionPeriod = (transition * 600) / 255;
}