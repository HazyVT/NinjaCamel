using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
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
        print("Click");
        SceneManager.LoadScene("SampleScene");
    }
}
