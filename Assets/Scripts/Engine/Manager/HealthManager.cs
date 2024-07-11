using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager
{
    public static int health = 100;

    public static void changeHealth(int change)
    {
        health += change;
    }
}
