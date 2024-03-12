using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private Image barImage;
    public Player player;

    private void Update()
    {
        if (barImage.name == "AmountOfHp")
        {
            barImage.fillAmount = player.GetHpNormalized();
        }
        if (barImage.name == "AmountOfMana")
        {
            barImage.fillAmount = player.GetManaNormalized();
        }
        if (barImage.name == "AmountOfEnergy")
        {
            barImage.fillAmount = player.GetEnergyNormalized();
        }
    }


}
