using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject[] weaponNames;

    public GameObject[] weaponImages;
    public GameObject[] weaponDescriptions;
    public GameObject weaponImageHolder;

    public GameObject descriptionHolder;
    public Text textDescription;
    public GameObject chooseButton;

    private static bool created;
    public string chosen;

    private List<string> weapons = new();
    public List<string> upgradeChoices = new();

    public GameObject player;

    void Start()
    {
        created = false;
        Globals.hasChakram = true;
        Globals.hasMelee = false;
        Globals.hasLightning = false;
        Globals.chakramLevel = 1;
        Globals.meleeLevel = 0;
        Globals.lightningLevel = 0;
        ExperienceManager.ResetExperienceManager();
        WaveManager.wave = 1;
        Globals.shurikenFireSpeed = 1f;
        Globals.shurikenLevel = 1;
        Globals.lightningFireSpeed = 2;
        Globals.gameTime = 0;
        Globals.enemeisDefeated = 0;

        weapons.Add("shuriken");
        weapons.Add("chakram");
        //weapons.Add("melee");
        weapons.Add("sandal");   
        weapons.Add("lightning");     
    }

    void Update()
    {
        if (ExperienceManager.isLeveling && !created)
        {
            screenDim.SetActive(true);

            List<string> upgrades = GetWeaponsToUpgrade();
            chosen = upgrades[0];
            upgradeChoices = upgrades;

            for (int i = 0; i < upgrades.Count; i++)
            {
                string choice = upgrades[i];
                Sprite image = null;
                string description = "";

                switch (choice)
                {
                    case "shuriken":
                        image = shurikenImage;
                        description = OnShurikenLevelUp();
                        break;
                    case "chakram":
                        description = OnChakramLevelUp();
                        image = chakramImage;
                        break;
                    /*
                    case "melee":
                        image = swordImage;
                        break;
                    */
                    case "sandal":
                        image = sandalImage;
                        description = OnSandalLevelUp();
                        break;
                    case "lightning":
                        image = thunderImage;
                        description = OnLightningLevelUp();
                        break;
                }

                if (image != null)
                {
                    weaponImages[i].GetComponent<Image>().sprite = image;
                    weaponNames[i].GetComponent<TextMeshProUGUI>().text = choice;
                    weaponDescriptions[i].GetComponent<TextMeshProUGUI>().text = description;

                }

            }

            //ShowLevelUpOptions();
            //descriptionHolder.SetActive(true);
            //chooseButton.SetActive(true);
            weaponImageHolder.SetActive(true);
            created = true;
        }
    }

    public string OnShurikenLevelUp()
    {
        string txt;

        switch (Globals.shurikenLevel)
        {
            case 1:
                txt = "Fire three shurikens at once";
                chosen = "shuriken";
                break;
            case 2:
                txt = "Fire five shurikens at once";
                chosen = "shuriken";
                break;
            case 3:
                txt = "Fire five shurikens twice in rapid succession";
                chosen = "shuriken";
                break;
            default:
                txt = "Max level reached";
                chosen = "";
                break;
        }

        //chosen = "shuriken";
        return txt;

    }

    public string OnChakramLevelUp()
    {
        string txt;

        switch (Globals.chakramLevel)
        {
            case 0:
                txt = "Gain a chakram that spins around you";
                break;
            case 1:
                txt = "Gain a second chakram";
                break;
            case 2:
                txt  = "Make chakrams spin faster";
                break;
            default:
                txt = "Max level reached";
                break;
        }

        //chosen = "chakram";
        return txt;
        //HighlightSelectedWeapon(1);
    }

    /*
    public string OnMeleeLevelUp()
    {
        switch (Globals.meleeLevel)
        {
            case 0:
                textDescription.text = "Gain a melee attack that attacks in front of you";
                break;
            case 1:
                textDescription.text = "Attack forward then backward";
                break;
        }

        chosen = "melee";
        //HighlightSelectedWeapon(2);
    }
    */

    public string OnSandalLevelUp() 
    {
        //chosen = "sandal";
        return "Increase movement speed";
    }

    public string OnLightningLevelUp()
    {
        string txt;

        switch (Globals.lightningLevel)
        {
            case 0:
                txt = "Gain a lightning attack that strikes from the sky";
                break;
            case 1:
                txt = "Make lightning attack strike faster";
                break;
            case 2:
                txt = "Make lightning attack strike twice";
                break;
            default:
                txt = "Max level reached";
                break;
        }

        return txt;
    }

    public void ChosenButtonClick(int index)
    {
        string choose = upgradeChoices[index];
        print(choose);
        
        switch (choose)
        {
            case "shuriken":
                if (Globals.shurikenLevel != Globals.shurikenMaxLevel) Globals.shurikenLevel++;
                break;
            case "chakram":
                if (Globals.chakramLevel != Globals.chakramMaxLevel) ApplyChakramUpgrade();
                break;
            case "melee":
                if (Globals.meleeLevel != Globals.meleeLMaxLevel) ApplyMeleeUpgrade();
                break;
            case "sandal":
                Globals.playerSpeed += 0.5f;
                break;
            case "lightning":
                if (Globals.lightningLevel !=  Globals.lightningMaxLevel) ApplyLightningUpgrade();
                break;
            default:
                print("Cannot level");
                break;
        }

        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        //chooseButton.SetActive(false);
        //descriptionHolder.SetActive(false);
        ExperienceManager.isLeveling = false;
        created = false;
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
        //Globals.lightningDamage += 10;
        
    }

    private void ApplyChakramUpgrade()
    {
       switch (Globals.chakramLevel) {
        case 0:
            Globals.hasChakram = true;
            Globals.chakramLevel++;
            break;
        case 1:
            Globals.chakramLevel++;
            break;
        case 2:
            Globals.orbitSpeed = 100;
            Globals.chakramLevel++;
            break;
       }

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
