using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager
{
    public static int experience;
    public static int level = 1;
    public static bool isLeveling = false;
    public static int requiredExperience = 100;

    public static void ChangeExperience(int change)
    {
        experience += change;
    }

    public static void GainLevel()
    {
        level += 1;
        experience = 0;
        isLeveling = true;
        requiredExperience += 100;
    }

    public static void ResetExperienceManager() {
        experience = 0;
        level = 1;
        isLeveling = false;
    }

}
