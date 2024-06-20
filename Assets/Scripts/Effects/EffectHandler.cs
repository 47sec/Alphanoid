using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    // Этот скрипт надо подвязать к объекту, на который будут дейстововать эффекты

    private List<CustomEffectScript> effects = new List<CustomEffectScript>();

    private List<float> effectTimers = new List<float>();

    private void Update()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            // Отсчитываем время у всех таймеров
            effectTimers[i] -= Time.deltaTime;
            if (effectTimers[i] <= 0)
            {
                effects[i].deactivate(transform);

                effects.RemoveAt(i);
                effectTimers.RemoveAt(i);
            }
        }
    }

    public void addEffect(CustomEffectScript script, float time)
    {
        // Если эффект уже действует, просто обновляем время
        int index = effects.FindIndex((x) => x.getId() == script.getId());

        if (index != -1)
        {
            effectTimers[index] = time;
            return;
        }

        effects.Add(script);
        effectTimers.Add(time);
        script.activate(transform);
    }
}
