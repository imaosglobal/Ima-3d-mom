using UnityEngine;

/// <summary>
/// MathHelper - עזרים מתמטיים למשחק
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// חישוב נזק עם וריאציה אקראית
    /// </summary>
    public static int CalculateDamage(int baseDamage, float variance = 0.1f)
    {
        float randomVariance = Random.Range(1f - variance, 1f + variance);
        return Mathf.Max(1, Mathf.RoundToInt(baseDamage * randomVariance));
    }

    /// <summary>
    /// חישוב הסתברות
    /// </summary>
    public static bool CheckProbability(float probability)
    {
        return Random.value <= probability;
    }

    /// <summary>
    /// חישוב מרחק בין שתי נקודות
    /// </summary>
    public static float Distance(Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from, to);
    }

    /// <summary>
    /// בדיקה אם במטווח
    /// </summary>
    public static bool IsInRange(Vector3 from, Vector3 to, float range)
    {
        return Distance(from, to) <= range;
    }

    /// <summary>
    /// Clamp ערך בין min ל-max
    /// </summary>
    public static int Clamp(int value, int min, int max)
    {
        return Mathf.Clamp(value, min, max);
    }

    /// <summary>
    /// חישוב XP לרמה הבאה
    /// </summary>
    public static int GetXPForLevel(int level)
    {
        return level * 100;
    }

    /// <summary>
    /// חישוב רמה מ-XP
    /// </summary>
    public static int GetLevelFromXP(int totalXP)
    {
        int level = 1;
        int requiredXP = 100;
        while (totalXP >= requiredXP)
        {
            totalXP -= requiredXP;
            level++;
            requiredXP = level * 100;
        }
        return level;
    }
}

/// <summary>
/// TimeHelper - עזרים לזמן במשחק
/// </summary>
public static class TimeHelper
{
    /// <summary>
    /// המרת שניות לתצוגה של דקות:שניות
    /// </summary>
    public static string FormatTime(float seconds)
    {
        int mins = (int)seconds / 60;
        int secs = (int)seconds % 60;
        return $"{mins:D2}:{secs:D2}";
    }

    /// <summary>
    /// בדיקה אם עברה זמן מסוים
    /// </summary>
    public static bool HasTimePassed(float lastTime, float interval)
    {
        return Time.time - lastTime >= interval;
    }

    /// <summary>
    /// עיכוב קוד (Coroutine)
    /// </summary>
    public static System.Collections.IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}

/// <summary>
/// StringHelper - עזרים לטקסט
/// </summary>
public static class StringHelper
{
    /// <summary>
    /// הפוך תו ראשון לגדול
    /// </summary>
    public static string FirstLetterToUpper(string text)
    {
        if (text.Length == 0) return text;
        return char.ToUpper(text[0]) + text.Substring(1);
    }

    /// <summary>
    /// פצל טקסט לשורות
    /// </summary>
    public static string[] SplitText(string text, int charsPerLine)
    {
        var lines = new System.Collections.Generic.List<string>();
        for (int i = 0; i < text.Length; i += charsPerLine)
        {
            lines.Add(text.Substring(i, System.Math.Min(charsPerLine, text.Length - i)));
        }
        return lines.ToArray();
    }
}
