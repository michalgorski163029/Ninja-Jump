
using UnityEngine;

public class ShurikenSpawner : MonoBehaviour
{
    public Transform leftShuriken;
    public Transform rightShuriken;
    public Transform thirdShuriken;
    public Transform fourthShuriken;
    public Transform ninja;

    public float minY = -3.5f;
    public float maxY = 3.5f;

    public float safeDistance = 1.5f;
    public float minShurikenSpacing = 1.2f;

    private const int maxAttempts = 100;

    public void RandomizeShurikens()
    {
        // Losowanie pozycji wszystkich 4 shurikenów
        // z zachowaniem odstępu od ninja i siebie nawzajem

        float leftY = GetSafeY(-2.15f, float.NaN);
        float thirdY = GetSafeY(-2.15f, leftY);

        float rightY = GetSafeY(2.15f, float.NaN);
        float fourthY = GetSafeY(2.15f, rightY);

        SetPosition(leftShuriken, -2.15f, leftY);
        SetPosition(thirdShuriken, -2.15f, thirdY);

        SetPosition(rightShuriken, 2.15f, rightY);
        SetPosition(fourthShuriken, 2.15f, fourthY);
    }

    float GetSafeY(float x, float otherY)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            float y = Random.Range(minY, maxY);

            // Nie pozwalamy shurikenom nachodzić na siebie
            if (!float.IsNaN(otherY) &&
                Mathf.Abs(y - otherY) < minShurikenSpacing)
            {
                continue;
            }

            // Sprawdzamy odległość od ninja
            if (ninja != null)
            {
                Vector2 newPosition = new Vector2(x, y);
                Vector2 ninjaPosition = ninja.position;

                if (Vector2.Distance(
                    newPosition,
                    ninjaPosition
                ) < safeDistance)
                {
                    continue;
                }
            }

            return y;
        }

        // Jeśli nie uda się znaleźć bezpiecznego miejsca,
        // pozostawiamy poprzednią pozycję
        return float.NaN;
    }

    void SetPosition(Transform shuriken, float x, float y)
    {
        if (shuriken == null || float.IsNaN(y))
            return;

        shuriken.position = new Vector3(x, y, 0f);
    }
}
