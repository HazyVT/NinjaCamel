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

    public GameObject player;

    private static bool created;

    private string[] weapons = { "shuriken", "chakram", "melee"};

    // Start is called before the first frame update
    void Start()
    {
        created = false;
        Globals.hasChakram = true;
        Globals.hasMelee = false;
        Globals.chakramLevel = 1;
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
            
            weaponImageHolder.SetActive(true);

            created = true;

        }
    }

    public void OnShurikenLevelUp() 
    {
        switch (Globals.shurikenLevel) {
            case 1:
            case 3:
            case 5:
                Globals.shurikenFireSpeed -= 0.3f;
                break;
            case 2:
                player.GetComponent<PlayerScript>().bulletPrefab.transform.localScale = new(1.7f, 1.7f);
                player.GetComponent<PlayerScript>().bulletPrefab.GetComponent<BulletBehaviour>().damage += 5;
                break;
            case 4:
                player.GetComponent<PlayerScript>().bulletPrefab.transform.localScale = new(2f, 2f);
                player.GetComponent<PlayerScript>().bulletPrefab.GetComponent<BulletBehaviour>().damage += 5;
                break;
            case 6:
                Globals.shurikenFireSpeed /= 2;
                player.GetComponent<PlayerScript>().bulletPrefab.transform.localScale = new(2.5f, 2.5f);
                player.GetComponent<PlayerScript>().bulletPrefab.GetComponent<BulletBehaviour>().damage += 10;
                break;
        }

        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        Globals.shurikenLevel++;
        ExperienceManager.isLeveling = false;
        created = false;

    }

    public void OnChakramLevelUp() 
    {

        switch (Globals.chakramLevel)
        {
            case 0:
                Globals.hasChakram = true;
                break;
            case 1:

                
                break;
        }

        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        Globals.chakramLevel++;
        ExperienceManager.isLeveling = false;
        created = false;
    }

    public void OnMeleeLevelUp()
    {
        switch (Globals.meleeLevel)
        {
            case 0:
                Globals.hasMelee = true;
                break;
        }

        screenDim.SetActive(false);
        weaponImageHolder.SetActive(false);
        Globals.meleeLevel++;
        ExperienceManager.isLeveling = false;
        created = false;
    }
}
