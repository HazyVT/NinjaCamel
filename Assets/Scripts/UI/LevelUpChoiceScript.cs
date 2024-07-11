using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpChoiceScript : MonoBehaviour
{
    private float horizontalBounds = 200;
    private float verticalBounds = 400;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                Vector3[] corners = new Vector3[4];
                GetComponent<RectTransform>().GetWorldCorners(corners);
                Vector2 localPos = (touch.position - (Vector2)corners[0]) / GameObject.FindGameObjectWithTag("Canvas").transform.localScale.x;
                if (localPos.x >= 0 && localPos.x <= horizontalBounds && localPos.y >= 0 && localPos.y <= verticalBounds)
                {
                    Globals.shurikenFireSpeed -= 0.2f;
                    ExperienceManager.isLeveling = false;
                    //PauseHandler.created = false;
                }

                if (!ExperienceManager.isLeveling)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
