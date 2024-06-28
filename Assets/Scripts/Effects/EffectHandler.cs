using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    // Этот скрипт надо подвязать к объекту, на который будут дейстововать эффекты

    private List<CustomEffectScript> effects = new List<CustomEffectScript>();

    // Лямда для длительного эффекта должна возвращать true, если он закончился
    // Сам этот лист в себе содержит действия, которые будут выполнятся при каждом вызове Update()
    // Пример использования в WideEffect.cs
    private List<Func<bool>> lastingEffects = new List<Func<bool>>();

    private void Update()
    {
        for (int i = 0; i < lastingEffects.Count; i++)
        {
            if (lastingEffects[i]())
                lastingEffects.RemoveAt(i);
        }

        for (int i = 0; i < effects.Count; i++)
        {
            // Отсчитываем время у всех таймеров
            effects[i].timer -= Time.deltaTime;
            if (effects[i].timer <= 0)
            {
                effects[i].deactivate(transform);

                effects.RemoveAt(i);
            }
        }
    }

    public void addEffect(CustomEffectScript script, float time)
    {
        // Если эффект уже действует, просто обновляем время
        int index = effects.FindIndex((x) => x.getId() == script.getId());

        if (index != -1)
        {
            effects[index].timer = time;
            return;
        }

        script.timer = time;
        effects.Add(script);
        script.activate(transform);
    }

    public void addLastingEffect(Func<bool> effect)
    {
        lastingEffects.Add(effect);
    }
}
