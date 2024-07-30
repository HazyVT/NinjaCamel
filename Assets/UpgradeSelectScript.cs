using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeSelectScript : MonoBehaviour, IPointerDownHandler
{

    public GameObject gameHandler;
    public int number;
    private string type;

    // Start is called before the first frame update
    void Start()
    {
        Physics2DRaycaster physics2DRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physics2DRaycaster == null) 
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ExperienceManager.isLeveling)
        {
            type = gameHandler.GetComponent<PauseHandler>().upgradeChoices[number];
        }
    }

    public void OnPointerDown(PointerEventData data)
    {

        print("Clicking " + number.ToString());
        gameHandler.GetComponent<PauseHandler>().ChosenButtonClick(number);
    }
}
