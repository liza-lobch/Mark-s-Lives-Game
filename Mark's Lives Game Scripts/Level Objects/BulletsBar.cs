using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletsBar : MonoBehaviour
{
    private Text text;

    private Character character;
    private void Start()
    {
        character = FindObjectOfType<Character>();
        text = GetComponent<Text>();
        text.text = character.Bullets.ToString();
    }

    public void Refresh()
    {
        if (character.Bullets > 0)
        {
            text.text = character.Bullets.ToString();
        }
        else
        {
            text.text = "0";
        }
    }
}
