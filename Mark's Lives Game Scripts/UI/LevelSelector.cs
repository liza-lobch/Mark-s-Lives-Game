using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private bool isUnlocked;
    [SerializeField]
    private int levelNumber;
    [SerializeField]
    private Image lockedImage;
    [SerializeField]
    private GameObject[] unlokedImages;
    private int lastComplLevel;
    private Color buttonColor;

    private void Start()
    {
        for (int i = 0; i < unlokedImages.Length; i++)
        {
            if (unlokedImages[i].gameObject.tag.Equals("PlayBtn"))
            {
                buttonColor = unlokedImages[i].GetComponent<Image>().color;
            }
        }
    }

    private void Update()
    {
        UpdateLevelImage();
        UpdateLevelStatus();
    }

    private void UpdateLevelStatus()
    {
        lastComplLevel = PlayerPrefs.GetInt("LastComplLv");
        if (levelNumber == 1 || levelNumber <= lastComplLevel + 1)
        {
            isUnlocked = true;
        }
    }
    private void UpdateLevelImage()
    {
        if (!isUnlocked)
        {
            lockedImage.gameObject.SetActive(true);
            for (int i = 0; i < unlokedImages.Length; i++)
            {
                unlokedImages[i].GetComponent<Image>().color = new Vector4(0.07f, 0.07f, 0.07f, 1);
            }
        }
        else
        {
            lockedImage.gameObject.SetActive(false);
            for (int i = 0; i < unlokedImages.Length; i++)
            {
                unlokedImages[i].GetComponent<Image>().color = new Vector4(1, 1, 1, 1);

                if (unlokedImages[i].gameObject.tag.Equals("PlayBtn"))
                {
                    unlokedImages[i].GetComponent<Image>().color = buttonColor;
                }

                if ((unlokedImages[i].gameObject.tag.Equals("PlayerImg")) && (levelNumber > lastComplLevel))
                {
                    unlokedImages[i].GetComponent<Image>().color = new Vector4(0.07f, 0.07f, 0.07f, 1);
                }
            }
        }
    }
}
