using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static float Approach(float start, float stop, float step)
    {
        if (start < stop)
        {
            return Mathf.Min(start + step, stop);
        } else
        {
            return Mathf.Max(start - step, stop);
        }
    }
}
