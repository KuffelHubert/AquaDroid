#include "LedPwm.h"
#include <TimeLib.h>
#include "Arduino.h"


int maxPwmValue = 180;
int minutesInHour = 60;

LedPwm::LedPwm(uint8_t pin, uint8_t pwmPin) : RelayOutput(pin)
{
    PwmPin = pwmPin;
    pinMode(PwmPin, OUTPUT);
    transitioning = false;
    PwmValue = 0;
    digitalWrite(Pin, HIGH);
    State = true;
}

void LedPwm::CheckTime(tmElements_t time)
{
    if (true)
    {
        if (!transitioning)
        {
            if (TurnOffHour > TurnOnHour)
            {
                if (time.Hour > TurnOnHour && time.Hour < TurnOffHour)
                {
                    Wlacz(time);
                }
                else
                {
                    Wylacz(time);
                }
            }
            else if (TurnOffHour < TurnOnHour)
            {
                if (time.Hour > TurnOnHour || time.Hour < TurnOnHour)
                {
                    Wlacz(time);
                }
                else
                {
                    Wylacz(time);
                }
            }
            else if(TurnOffHour == TurnOnHour)
            {
                if (TurnOffMinute > TurnOffMinute)
                {
                    if (time.Hour > TurnOffHour || time.Hour < TurnOnHour)
                    {
                        Wylacz(time);
                    }
                    else
                    {
                        if (time.Minute < TurnOnMinute || time.Minute > TurnOffMinute)
                        {
                            Wylacz(time);
                        }
                        else
                        {
                            Wlacz(time);
                        }
                    }
                }
            }
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
                else if (PwmValue >= maxPwmValue)
                {
                    PwmValue = 0;
                    digitalWrite(Pin, HIGH);
                    State = true;
                    transitioning = false;
                }
                analogWrite(PwmPin, PwmValue);
            }
        }
    }
}

void LedPwm::Wylacz(const tmElements_t &time)
{
    if (State == false)
    {
        transitioning = true;
        nextTransition = GetSeconds(time);
        State = true;
        PwmValue = 0;
    }
}

void LedPwm::Wlacz(const tmElements_t &time)
{
    if (State == true)
    {
        transitioning = true;
        nextTransition = GetSeconds(time);
        digitalWrite(Pin, LOW);
        State = false;
        PwmValue = 180;
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
    transitionPeriod = ((transition * minutesInHour) / maxPwmValue) + 1;
}