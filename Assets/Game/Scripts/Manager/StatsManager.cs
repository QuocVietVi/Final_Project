using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Tower tower;
    public Player player;

    private void Start()
    {
    }

    private void Update()
    {
        player = CameraFollow.Instance.target;
        tower = LevelManager.Instance.map.tower;
        if (player != null)
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
       
        if (tower != null)
        {
            if (barImage.name == "AmountOfTowerHp")
            {
                barImage.fillAmount = tower.GetHpNormalized();
            }
        }

    }


}
