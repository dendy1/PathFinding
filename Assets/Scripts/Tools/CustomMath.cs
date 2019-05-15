using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CustomMath
{
    public struct Range
    {
        public int min;
        public int max;
        public int range {get {return max-min + 1;}}
        public Range(int aMin, int aMax)
        {
            min = aMin; max = aMax;
        }
    }
    
    [Serializable]
    public struct Borders
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public Vector3[] GetPoints(Vector3 offset = new Vector3())
        {
            return new[]
            {
                new Vector3(-left, 0f, top) + offset,
                new Vector3(right, 0f, top) + offset,
                new Vector3(right, 0f, -bottom) + offset,
                new Vector3(-left, 0f, -bottom) + offset
            };
        }

        public void Normalize(Borders externalBorders)
        {
            left = Mathf.Clamp(left, -externalBorders.left, externalBorders.left);
            top = Mathf.Clamp(top, -externalBorders.top, externalBorders.top);
            right = Mathf.Clamp(right, -externalBorders.right, externalBorders.right);
            bottom = Mathf.Clamp(bottom, -externalBorders.bottom, externalBorders.bottom);
        }
    }
 
    public static int RandomValueFromRanges(params Range[] ranges)
    {
        if (ranges.Length == 0)
            return 0;
        int count = 0;
        foreach (Range r in ranges)
            count += r.range;
        int sel = UnityEngine.Random.Range(0, count);
        foreach (Range r in ranges)
        {
            if (sel < r.range)
            {
                return r.min + sel;
            }
            sel -= r.range;
        }
        throw new Exception("This should never happen");
    }
}
