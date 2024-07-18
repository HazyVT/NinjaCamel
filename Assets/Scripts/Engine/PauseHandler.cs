using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{

    public GameObject canvas;
    public GameObject screenDim;

    public Sprite shurikenImage;
    public Sprite chakramImage;
    public Sprite swordImage;

    public GameObject[] weaponImages;
    public GameObject weaponImageHolder;

    public GameObject descriptionHolder;
    public Text textDescription;
    public GameObject chooseButton;

    private static bool created;
    private string chosen = "shuriken";

    public GameObject healthDrop;
    private float healthTime = 0f;

    private string[] weapons = { "shuriken", "chakram", "melee"};

    void Start()
    {
        created = false;
        Globals.hasChakram = false;
        Globals.hasMelee = false;
        Globals.chakramLevel = 0;
        Globals.meleeLevel = 0;
        ExperienceManager.ResetExperienceManager();
        WaveManager.wave = 1;
        Globals.shurikenFireSpeed = 2;
        Globals.shurikenLevel = 1;
    }

    void Update()
    {
        if (ExperienceManager.isLeveling && !created)
        {
            screenDim.SetActive(true);

            weaponImages[0].GetComponent<Image>().sprite = shurikenImage;
            weaponImages[1].GetComponent<Image>().sprite = chakramImage;
            weaponImages[2].GetComponent<Image>().sprite = swordImage;

            ShowLevelUpOptions();
            descriptionHolder.SetActive(true);

            chooseButton.SetActive(true);

            weaponImageHolder.SetActive(true);

            created = true;
        }
    }

    private void ShowLevelUpOptions()
    {
        switch (chosen)
        {
            case "shuriken":
                OnShurikenLevelUp();
                break;
            case "chakram":
                OnChakramLevelUp();
                break;
            case "melee":
                OnMeleeLevelUp();
                break;
        }
    }

    public void OnShurikenLevelUp()
    {
        switch (Globals.shurikenLevel)
        {
            case 1:
                textDescription.text = "Increase shuriken attack speed by 40%";
                break;
            case 2:
                textDescription.text = "Fire three shurikens in your facing direction";
                break;
        }

        chosen = "shuriken";
        HighlightSelectedWeapon(0);
    }

    public void OnChakramLevelUp()
    {
        switch (Globals.chakramLevel)
        {
            case 0:
                textDescription.text = "Gain a chakram that spins around you";
                break;
        }

        chosen = "chakram";
        HighlightSelectedWeapon(1);
    }

    public void OnMeleeLevelUp()
    {
        switch (Globals.meleeLevel)
        {
            case 0:
                textDescription.text = "Gain a melee attack that attacks in front of you";
                break;
            case 1:
                textDescription.text = "Attack forward then backward";
                break;
            default:
                textDescription.text = "Max level reached";
                break;
        }

        chosen = "melee";
        HighlightSelectedWeapon(2);
    }

    private void HighlightSelectedWeapon(int index)
    {
        for (int i = 0; i < weaponImages.Length; i++)
        {
            weaponImages[i].transform.localScale = new Vector3(1, 1, 1);
        }
        weaponImages[index].transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    }

    public void ChosenButtonClick()
    {
        switch (chosen)
        {
            case "shuriken":
                ApplyShurikenUpgrade();
                break;
            case "chakram":
                ApplyChakramUpgrade();
                break;
            case "melee":
                ApplyMeleeUpgrade();
                break;
        }

        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        chooseButton.SetActive(false);
        descriptionHolder.SetActive(false);
        ExperienceManager.isLeveling = false;
        created = false;
    }

    private void ApplyShurikenUpgrade()
    {
        if (Globals.shurikenLevel < 2)
        {
            switch (Globals.shurikenLevel)
            {
                case 1:
                    Globals.shurikenFireSpeed -= 0.4f;
                    break;
                case 2:
                    break;
            }
            Globals.shurikenLevel++;
        }
    }

    private void ApplyChakramUpgrade()
    {
        if (Globals.chakramLevel < 1)
        {
            Globals.hasChakram = true;
            Globals.chakramLevel = 1;
        }
    }

    private void ApplyMeleeUpgrade()
    {
        if (Globals.meleeLevel < 2)
        {
            Globals.meleeLevel++;
            Globals.hasMelee = true;
        }
    }
}
