using System;
using System.Text;
using TMPro.EditorUtilities;
using UnityEngine;

public class Score : MonoBehaviour
{
    [field: SerializeField] public int Points { get; private set; }
    // Событие изменения счёта
    public Action<int> OnScoreChanged;
    // Событие изменения очков комбо
    public Action<int> OnStreakPointsChanged;
    
    // Как упаковать streak и надо ли?

    // Очки комбо
    [HideInInspector] public int StreakPoints { get; private set; }
    // Комбо
    [HideInInspector] public int Streak { get; private set; }

    [SerializeField] private float streakFirstCountdown;
    [SerializeField] private float streakCountdownDecreaseMultiplier;
    private float _currentCountdownDecreaseMultiplier;
    public CounterFloat _streakTimer { get; private set; }

    private void Awake()
    {
        Streak = StreakPoints = 0;

        // Настройка таймре
        _streakTimer = new CounterFloat(CounterResetAction.HighLimit, streakFirstCountdown, CounterResetAction.HighLimit, 0, CounterResetAction.HighLimit);

        // Установка множителя скорости таймера по умолчанию
        _currentCountdownDecreaseMultiplier = streakCountdownDecreaseMultiplier;
    }
    public void AddPoints(int amount)
    {
        OnStreakPointsChanged?.Invoke(amount);

        StreakPoints += amount;

        ++Streak;
        _streakTimer.Reset(CounterResetAction.HighLimit);
        _currentCountdownDecreaseMultiplier += 0.1f;
    }
    private void Update()
    {
        if (Streak > 0)
            StreakUpdate();
    }
    private void StreakUpdate()
    {
        // Если таймер дошёл до минимума
        // Таймер сам сбрасывается до стандартного значения
        if(_streakTimer.Add(-Time.deltaTime * _currentCountdownDecreaseMultiplier))
        {
            int _addedScore = StreakPoints * Streak;

            StringBuilder sb = new StringBuilder();
            sb.Append("[Streak End] streak: ");
            sb.Append(Streak).Append("; streak points: ").Append(StreakPoints);

            // Начислить очки комбо
            Points += _addedScore;

            sb.Append("; score: ").Append(Points);
            Debug.Log(sb);

            // Сбросить очки комбо
            StreakPoints = 0;

            // Сбросить множитель до стандартного значения
            _currentCountdownDecreaseMultiplier = streakCountdownDecreaseMultiplier;

            // Сбросить комбо до 0
            Streak = 0;


            OnStreakPointsChanged?.Invoke(0);
            OnScoreChanged?.Invoke(_addedScore);
        }
    }
}
