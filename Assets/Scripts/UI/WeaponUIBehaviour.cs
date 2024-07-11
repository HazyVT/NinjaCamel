using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponUIBehaviour : MonoBehaviour, IPointerDownHandler
{

    public GameObject gameHandler;
    public string type;

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
        
    }

    public void OnPointerDown(PointerEventData data)
    {
        //Debug.Log("Clicked " + data.pointerCurrentRaycast.gameObject.name);
        switch (type) 
        {
            case "shuriken":
                gameHandler.GetComponent<PauseHandler>().OnShurikenLevelUp();
                break;
            case "chakram":
                gameHandler.GetComponent<PauseHandler>().OnChakramLevelUp();
                break;
            case "melee":
                gameHandler.GetComponent<PauseHandler>().OnMeleeLevelUp();
                break;
        }
    }
}
