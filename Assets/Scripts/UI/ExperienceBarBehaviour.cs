using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarBehaviour : MonoBehaviour
{
    //public GameObject bar;
    private float fillAmount;

    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = new(0, 0.3f);
        fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //float size = ExperienceManager.experience / ExperienceManager.level;
        //bar.transform.localScale = new(size / 100, bar.transform.localScale.y, bar.transform.localScale.z);
        fillAmount = ExperienceManager.experience / ExperienceManager.level;
        //print(fillAmount);
        //fillAmount += Time.deltaTime;
        GetComponent<Image>().fillAmount = fillAmount / 100;
    }
}
