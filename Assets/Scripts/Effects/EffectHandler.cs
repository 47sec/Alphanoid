using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    // ���� ������ ���� ��������� � �������, �� ������� ����� ������������ �������

    private List<CustomEffectScript> effects = new List<CustomEffectScript>();

    // ����� ��� ����������� ������� ������ ���������� true, ���� �� ����������
    // ��� ���� ���� � ���� �������� ��������, ������� ����� ���������� ��� ������ ������ Update()
    // ������ ������������� � WideEffect.cs
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
            // ����������� ����� � ���� ��������
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
        // ���� ������ ��� ���������, ������ ��������� �����
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
