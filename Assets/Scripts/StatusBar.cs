using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public HeroKnight hero;
    public Image fillImage;

    private void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
    }


    void Update()
    {
        if (gameObject.name == "HealthBar")
            fillImage.fillAmount = hero.heroHealth / 100;
        else if (gameObject.name == "ManaBar")
            fillImage.fillAmount = hero.heroMana / 100;
        else if (gameObject.name == "PowerBar")
            fillImage.fillAmount = hero.powerTimer / hero.powerDuration;
    }





}