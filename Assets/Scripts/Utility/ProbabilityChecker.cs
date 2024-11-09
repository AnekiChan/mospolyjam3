using UnityEngine;

public class ProbabilityChecker
{
    public static bool CheckProbability(float probability)
    {
        // Генерируем случайное число от 0 до 1 и проверяем, меньше ли оно, чем вероятность
        return Random.value < probability;
    }
}
