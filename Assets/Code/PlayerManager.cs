using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int money;
    public float hunger;
    public float thrist;
    public float coldness;


    public static PlayerManager instance;



    public Text moneyText;
    public Image hungerBar;
    public Image thristBar;
    public Image coldnessBar;

    private void Awake()
    {
        instance=this;
        UpdateUI();
        StartCoroutine("GraduallyDecreaseStats");
    }


    public void UpdateUI()
    {
        moneyText.text=money.ToString();
        hungerBar.fillAmount = (hunger/100f);
        thristBar.fillAmount = (thrist/100f);
        coldnessBar.fillAmount = (coldness/100f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "VehicleEnter")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                other.transform.GetComponent<Vehicle>().Enter();
            }
        }
    }


    IEnumerator GraduallyDecreaseStats()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            thrist-=0.05f;
            hunger-=0.0125f;
            coldness-=0.025f;
            UpdateUI();
            yield return null;
        }
    }
}
