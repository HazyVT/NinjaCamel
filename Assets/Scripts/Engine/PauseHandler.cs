using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{

    public GameObject canvas;
    public GameObject screenDim;

    public Sprite shurikenImage;
    public Sprite chakramImage;
    public Sprite swordImage;
    public Sprite sandalImage;
    public Sprite thunderImage;

    public GameObject[] weaponImages;
    public GameObject weaponImageHolder;

    public GameObject descriptionHolder;
    public Text textDescription;
    public GameObject chooseButton;

    private static bool created;
    private string chosen = "shuriken";

    public GameObject healthDrop;
    private float healthTime = 0f;

    private List<string> weapons = new();
    public List<string> upgradeChoices = new();

    public GameObject player;

    void Start()
    {
        created = false;
        Globals.hasChakram = false;
        Globals.hasMelee = false;
        Globals.hasLightning = false;
        Globals.chakramLevel = 0;
        Globals.meleeLevel = 0;
        Globals.lightningLevel = 0;
        ExperienceManager.ResetExperienceManager();
        WaveManager.wave = 1;
        Globals.shurikenFireSpeed = 2;
        Globals.shurikenLevel = 1;
        Globals.lightningFireSpeed = 2;

        weapons.Add("shuriken");
        weapons.Add("chakram");
        weapons.Add("melee");
        weapons.Add("sandal");   
        weapons.Add("lightning");     
    }

    void Update()
    {
        if (ExperienceManager.isLeveling && !created)
        {
            screenDim.SetActive(true);

            List<string> upgrades = GetWeaponsToUpgrade();
            upgradeChoices = upgrades;

            for (int i = 0; i < upgrades.Count; i++)
            {
                string choice = upgrades[i];
                chosen = upgrades[0];
                Sprite image = null;

                switch (choice)
                {
                    case "shuriken":
                        image = shurikenImage;
                        break;
                    case "chakram":
                        image = chakramImage;
                        break;
                    case "melee":
                        image = swordImage;
                        break;
                    case "sandal":
                        image = sandalImage;
                        break;
                    case "lightning":
                        image = thunderImage;
                        break;
                }

                if (image != null)
                {
                    weaponImages[i].GetComponent<Image>().sprite = image;
                }

            }

            ShowLevelUpOptions();
            HighlightSelectedWeapon(0);
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
            case "sandal":
                OnSandalLevelUp();
                break;
            case "lightning":
                OnLightningLevelUp();
                break;
        }
    }

    public void OnShurikenLevelUp()
    {
        switch (Globals.shurikenLevel)
        {
            case 1:
                textDescription.text = "Fire 3 shurikens on every attack";
                break;
            case 2:
                textDescription.text = "Fire 5 shurikens on every attack";
                break;
            case 3:
                textDescription.text = "Fire attack twice";
                break;
        }

        chosen = "shuriken";
        //HighlightSelectedWeapon(0);
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
        //HighlightSelectedWeapon(1);
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
        //HighlightSelectedWeapon(2);
    }

    public void OnSandalLevelUp() 
    {
        textDescription.text = "Increase movement speed";
        chosen = "sandal";
    }

    public void OnLightningLevelUp()
    {
        switch (Globals.lightningLevel)
        {
            case 0:
                textDescription.text = "Gain a lightning attack that strikes from the sky";
                break;
            case 1:
                textDescription.text = "Make lightning attack strike faster";
                break;
            case 2:
                textDescription.text = "Make lightning attack strike twice";
                break;
        }
        chosen = "lightning";
    }

    public void HighlightSelectedWeapon(int index)
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
                if (Globals.shurikenLevel != Globals.shurikenMaxLevel) Globals.shurikenLevel++;
                break;
            case "chakram":
                if (Globals.chakramLevel != Globals.chakramMaxLevel) Globals.chakramLevel++;;
                break;
            case "melee":
                if (Globals.meleeLevel != Globals.meleeLMaxLevel) Globals.meleeLevel++;
                break;
            case "sandal":
                Globals.playerSpeed += 0.5f;
                break;
            case "lightning":
                if (Globals.lightningLevel !=  Globals.lightningMaxLevel) Globals.lightningLevel++;;
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
        switch (Globals.shurikenLevel)
        {
            case 1:
                break;
            case 2:
                break;
        }
        //Globals.shurikenDamage += 10;
        
    }

    private void ApplyLightningUpgrade()
    {
        switch (Globals.lightningLevel)
        {
            case 0:
                Globals.hasLightning = true;
                //print("Gained lightning");
                break;
            case 1:
                Globals.lightningFireSpeed -= 0.5f;
                break;
            case 2:
                player.GetComponent<LightningWeaponScript>().times = 2;
                break;
        }
        Globals.lightningDamage += 10;
        
    }

    private void ApplyChakramUpgrade()
    {
        Globals.hasChakram = true;
        Globals.chakramLevel = 1;
    }

    private void ApplyMeleeUpgrade()
    {
        Globals.meleeLevel++;
        Globals.hasMelee = true;
    }

    private List<string> GetWeaponsToUpgrade()
    {
        List<string> choices = new List<string>(weapons);
        List<string> made = new List<string>();

        for (int i = 0; i < 3; i++)
        {
            int choiceNo = Random.Range(0, choices.Count);
            made.Add(choices[choiceNo]);
            choices.RemoveAt(choiceNo);
        }

        return made;
    }
}
