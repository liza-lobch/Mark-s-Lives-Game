using System.Collections;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField]
    private Transform redLight, yellowLight, greenLight;
    [SerializeField]
    private float rate = 2.0F;
    bool isRed = true, wasRed = true, playerIsOnRoad, isHitted;

    private void Start()
    {
        InvokeRepeating("SwithLight", rate, rate);
        redLight.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (playerIsOnRoad && !isHitted && isRed)
        {
            isHitted = true;
            //анимация
            Character player = FindObjectOfType<Character>();
            player.ReceiveDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            playerIsOnRoad = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            playerIsOnRoad = false;
        }
    }

    private void SwithLight()
    {
        if (redLight.gameObject.activeSelf == true)
        {
            redLight.gameObject.SetActive(false);
            yellowLight.gameObject.SetActive(true);
            isRed = false;
        }
        else if (greenLight.gameObject.activeSelf == true)
        {
            greenLight.gameObject.SetActive(false);
            yellowLight.gameObject.SetActive(true);
        }
        else
        {
            yellowLight.gameObject.SetActive(false);
            if (wasRed)
            {
                wasRed = false;
                greenLight.gameObject.SetActive(true);
            }
            else
            {
                wasRed = true;
                redLight.gameObject.SetActive(true);
                isRed = true;
                isHitted = false;
            }
        }
    }
}
