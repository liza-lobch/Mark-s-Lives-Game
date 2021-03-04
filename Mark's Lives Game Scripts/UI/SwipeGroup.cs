using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SwipeGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject scrollBar;
    private float scrollPos;
    private float[] pos;
    private float distance;

    private void Start()
    {
        pos = new float[transform.childCount];
        distance = 1.0f / (pos.Length - 1.0f);
        scrollPos = (PlayerPrefs.GetInt("CurrentLv") - 1) * distance;
        scrollBar.GetComponent<Scrollbar>().value = scrollPos;
    }

    private void Update()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.0f, 1.0f), 0.1f);
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(0.3f, 0.3f), 0.05f);
                    }
                }
            }
        }

    }
}
