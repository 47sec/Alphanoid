using System;
using System.Timers;
using Unity.VisualScripting;
public enum CounterResetAction
{
    Zero,
    LowLimit,
    HighLimit
}

[Serializable]
public class CounterFloat
{
    public float Count { get; private set; }

    public float high_limit;
    public CounterResetAction reset_on_high;
    
    public float high_reset_to;

    public float low_limit;
    public CounterResetAction reset_on_low;

    // Обычный счётчик положительных чисел
    public CounterFloat(CounterResetAction reset_initialize, float high_limit, CounterResetAction reset_on_high, float low_limit, CounterResetAction reset_on_low)
    {
        this.high_limit = high_limit;
        this.reset_on_high = reset_on_high;

        this.low_limit = low_limit;
        this.reset_on_low = reset_on_low;

        switch(reset_initialize)
        {
            case CounterResetAction.Zero:
                {
                    Count = 0;
                    break;
                }
            case CounterResetAction.HighLimit:
                {
                    Count = high_limit;
                    break;
                }
            case CounterResetAction.LowLimit:
                {
                    Count = low_limit;
                    break;
                }
        }
    }
    // Возвращает true если был выход за пределы
    public bool Add(float value)
    {
        Count += value;

        if (Count > high_limit)
        {
            Reset(reset_on_high);
            return true;
        }
        if (Count < low_limit)
        {
            Reset(reset_on_low);
            return true;
        }

        return false;
    }
    public void Reset(CounterResetAction action)
    {
        switch (action)
        {
            case CounterResetAction.Zero:
                {
                    Count = 0;
                    break;
                }
            case CounterResetAction.HighLimit:
                {
                    Count = high_limit;
                    break;
                }
            case CounterResetAction.LowLimit:
                {
                    Count = low_limit;
                    break;
                }
        }
    }
}