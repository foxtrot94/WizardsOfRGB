using UnityEngine;

public static class Tools
{
    public const float laneHeight = 1.5f;

    public static Rect RectOffset(Rect rect, int x, int y)
    {
        return new Rect(rect.x + x, rect.y + y, rect.width, rect.height);
    }

    public static Rect RectShrink(Rect rect, int x, int y)
    {
        return new Rect(rect.x + x / 2, rect.y + y / 2, rect.width - x, rect.height - y);
    }

    public static Vector3 GameToWorldPosition(float row, float x)
    {
        return new Vector3(1f * x, 3f * laneHeight - (row + 1) * laneHeight);
    }

    public static Vector3 GameToWorldPosition(float sourceRow, float targetRow, float progression)
    {
        if (progression < 0.25f)
        {
            return GameToWorldPosition(sourceRow, Mathf.Lerp(0, -1, progression / 0.25f));
        }
        else if (progression < 0.75f)
        {
            return GameToWorldPosition(Mathf.Lerp(sourceRow, targetRow, (progression - 0.25f) / 0.5f), -1);
        }
        else
        {
            return GameToWorldPosition(targetRow, Mathf.Lerp(-1, 0, (progression - 0.75f) / 0.25f));
        }
    }

    public static Rect MakeRect(float centerX, float centerY, float width, float height)
    {
        int w = (int)(Screen.width * width);
        int h = (int)(Screen.height * height);
        return new Rect((int)(centerX * Screen.width - w / 2), (int)(centerY * Screen.height - h / 2), w, h);
    }
}