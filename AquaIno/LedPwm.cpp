#include "LedPwm.h"
#include <TimeLib.h>
#include "Arduino.h"

LedPwm::LedPwm(uint8_t pin, uint8_t pwmPin) : RelayOutput(pin)
{
    PwmPin = pwmPin;
    pinMode(PwmPin, OUTPUT);
    transitioning = false;
    PwmValue = 0;
}

void LedPwm::CheckTime(tmElements_t time)
{
    if (true)
    {
        if (!transitioning && time.Hour == TurnOffHour && time.Minute == TurnOffMinute)
        {
            transitioning = true;
            nextTransition = GetSeconds(time);
            State = true;
            PwmValue = 0;
        }
        else if (!transitioning && time.Hour == TurnOnHour && time.Minute == TurnOnMinute)
        {
            transitioning = true;
            nextTransition = GetSeconds(time);
            digitalWrite(Pin, LOW);
            State = false;
            PwmValue = 180;
        }
        if (transitioning)
        {
            long currentSeconds = GetSeconds(time);
            if (currentSeconds >= nextTransition)
            {
                nextTransition = currentSeconds + transitionPeriod;
                if (RelayOutput::State)
                {
                    PwmValue++;
                }
                else
                {
                    PwmValue--;
                }
                if (PwmValue <= 0)
                {
                    transitioning = false;
                }
                else if (PwmValue >= 180)
                {
                    digitalWrite(Pin, HIGH);
                    State = true;
                    transitioning = false;
                }
                Serial.println(PwmValue);
                analogWrite(PwmPin, PwmValue);
            }
        }
    }
}

long LedPwm::GetSeconds(tmElements_t time)
{
    long fromMonth = time.Month * 2629743;
    long fromDay = time.Day * 86400;
    long fromHour = time.Hour * 3600;
    long fromMinute = time.Minute * 60;
    return fromMonth + fromDay + fromHour + fromMinute + time.Second;
}

void LedPwm::SetTransition(int transition)
{
    transitionPeriod = ((transition * 60) / 180) + 1;
    Serial.println(transitionPeriod);
}