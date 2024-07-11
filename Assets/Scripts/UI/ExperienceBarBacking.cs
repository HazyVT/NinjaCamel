using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarBacking : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ExperienceManager.isLeveling)
        {
            var color = image.color;
            color.a = 0;
            image.color = color;
        } else
        {
            var color = image.color;
            color.a = 255;
            image.color = color;
        }
    }
}
