using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeImage : MonoBehaviour
{
    private Sprite OnImage;
    public Sprite OffImage;
    public Button button;
    private bool isOn = true; // to check whether the image has light or no 
    public GameObject AudioManager;

    void Start()
    {
        //// Store this sprite of the button "sound on" 
        OnImage = button.image.sprite;
    }

    public void ButtonClicked()
    {
        if (isOn) 
        {
            button.image.sprite = OffImage; //to change the button image to the "sound off" sprite
            isOn = false; // to turn off the sound


        }
        else
        {

            button.image.sprite = OnImage; // to change to button sprite to "On"
            isOn = true; // turn "On" the sound
            
        }
    }

}