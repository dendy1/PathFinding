using System.Linq;
using UnityEngine;

public static class GizmosExtensions
{
    public static void DrawThickLine(Vector3 p1, Vector3 p2, float width)
    {
        int count = 1 + Mathf.CeilToInt(width); // how many lines are needed.
        if (count == 1)
        {
            Gizmos.DrawLine(p1, p2);
        }
        else
        {
            Camera c = Camera.current;
            if (c == null)
            {
                Debug.LogError("Camera.current is null");
                return;
            }
            var scp1 = c.WorldToScreenPoint(p1);
            var scp2 = c.WorldToScreenPoint(p2);
 
            Vector3 v1 = (scp2 - scp1).normalized; // line direction
            Vector3 n = Vector3.Cross(v1, Vector3.forward); // normal vector
 
            for (int i = 0; i < count; i++)
            {
                Vector3 o = 0.99f * n * width * ((float)i / (count - 1) - 0.5f);
                Vector3 origin = c.ScreenToWorldPoint(scp1 + o);
                Vector3 destiny = c.ScreenToWorldPoint(scp2 + o);
                Gizmos.DrawLine(origin, destiny);
            }
        }
    }

    public static void DrawBorders(Color lineColor, float lineWidth, Vector3[] borders)
    {
        Gizmos.color = lineColor;
        for (int i = 0; i < borders.Length - 1; i++)
        {
            DrawThickLine(borders[i], borders[i + 1], lineWidth);
        }
        DrawThickLine(borders.Last(), borders.First(), lineWidth);
    }
    
    public static void DrawBorders(Color lineColor, float lineWidth, params Vector3[][] borders)
    {
        foreach (var border in borders)
        {
            DrawBorders(lineColor, lineWidth, border);
        }
    }
}
