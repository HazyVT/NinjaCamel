using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponUIBehaviour : MonoBehaviour, IPointerDownHandler
{

    public GameObject gameHandler;
    public int number;
    private string type;

    // Start is called before the first frame update
    void Start()
    {
       


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
        print("Clicking");
        //Debug.Log("Clicked " + data.pointerCurrentRaycast.gameObject.name);
       //gameHandler.GetComponent<PauseHandler>().HighlightSelectedWeapon(number);
    }
}
