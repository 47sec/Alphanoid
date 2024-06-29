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

    private float shadeDir = 1f;

    [SerializeField][Tooltip("Время до окончания эффекта для активации моргания (в секундах)")]
    private float shadeTimer = 2f;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        for (int i = 0; i < lastingEffects.Count; i++)
        {
            if (lastingEffects[i]())
            {
                lastingEffects.RemoveAt(i);
                i--;
            }
        }

        if (effects.FindIndex((x) => { return x.timer <= shadeTimer; }) != -1)
            setShade();
        else
            resetShade();

        for (int i = 0; i < effects.Count; i++)
        {
            // Отсчитываем время у всех таймеров
            effects[i].timer -= Time.deltaTime;
            if (effects[i].timer <= 0)
            {
                effects[i].deactivate(transform);
                effects.RemoveAt(i);
                i--;
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

    // Метод для моргания спрайта
    private void setShade()
    {
        sprite.color -= (Color.black * shadeDir) * 0.025f;
        if (sprite.color.a >= 0.99f || sprite.color.a <= 0.5f)
            shadeDir *= -1f;
    }

    private void resetShade()
    {
        sprite.color = sprite.color * new Color(1, 1, 1, 0) + Color.black;
        shadeDir = 1f;
    }
}
