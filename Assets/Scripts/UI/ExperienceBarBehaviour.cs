using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBarBehaviour : MonoBehaviour
{
    public GameObject bar;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new(0, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        float size = ExperienceManager.experience / ExperienceManager.level;
        bar.transform.localScale = new(size / 100, bar.transform.localScale.y, bar.transform.localScale.z);
    }
}
