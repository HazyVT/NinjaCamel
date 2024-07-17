using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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

    public GameObject player;

    private static bool created;
    private string chosen = "shuriken";

    private string[] weapons = { "shuriken", "chakram", "melee"};

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (ExperienceManager.isLeveling && !created)
        {

            screenDim.SetActive(true);

            weaponImages[0].GetComponent<Image>().sprite = shurikenImage;
            weaponImages[1].GetComponent<Image>().sprite = chakramImage;
            weaponImages[2].GetComponent<Image>().sprite = swordImage;

            OnShurikenLevelUp();
            descriptionHolder.SetActive(true);

            chooseButton.SetActive(true);
            
            weaponImageHolder.SetActive(true);

            created = true;

        }
    }

    public void OnShurikenLevelUp() 
    {
        switch (Globals.shurikenLevel) {
            case 1:
                textDescription.text = "Increase shuriken attack speed by 40%";
                break;
            case 2:
                textDescription.text = "Fire two shurikens in your facing direction";
                break;
        }

        chosen = "shuriken";
        weaponImages[0].transform.localScale = new(1.5f, 1.5f, 1f);
        weaponImages[1].transform.localScale = new(1,1,1);
        weaponImages[2].transform.localScale = new(1,1,1);

        /*
        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        Globals.shurikenLevel++;
        ExperienceManager.isLeveling = false;
        created = false;
        */

    }

    public void OnChakramLevelUp() 
    {

        switch (Globals.chakramLevel)
        {
            case 0:
                textDescription.text = "Gain a chakram that spins around you";
                chosen = "chakram";
                weaponImages[1].transform.localScale = new(1.5f, 1.5f, 1f);
                weaponImages[0].transform.localScale = new(1,1,1);
                weaponImages[2].transform.localScale = new(1,1,1);
                break;
        }

        /*
        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        Globals.chakramLevel++;
        ExperienceManager.isLeveling = false;
        created = false;
        */
    }

    public void OnMeleeLevelUp()
    {
        switch (Globals.meleeLevel)
        {
            case 0:
                textDescription.text = "Gain a melee attack that attacks in front of you";
                chosen = "melee";
                weaponImages[2].transform.localScale = new(1.5f, 1.5f, 1f);
                weaponImages[1].transform.localScale = new(1,1,1);
                weaponImages[0].transform.localScale = new(1,1,1);
                break;
        }

        /*
        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        Globals.meleeLevel++;
        ExperienceManager.isLeveling = false;
        created = false;
        */
    }

    public void ChosenButtonClick()
    {
        switch (chosen)
        {
            case "shuriken":
                switch (Globals.shurikenLevel) {
                    case 1:
                        Globals.shurikenFireSpeed -= 0.4f;
                        break;
                    case 2:
                        break;

                }
                Globals.shurikenLevel++;
                break;
            case "chakram":
                Globals.hasChakram = true;
                Globals.chakramLevel = 1;
                break;
            case "melee":
                Globals.hasMelee = true;
                Globals.meleeLevel = 1;
                break;
        }
        
        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        chooseButton.SetActive(false);
        descriptionHolder.SetActive(false);
        ExperienceManager.isLeveling = false;
        created = false;
    }
}
