using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private AllyAndEnemy allyAndEnemy;
    [SerializeField] private Image image;

    private void Update()
    {
        image.fillAmount = allyAndEnemy.GetHpNormalized();
    }

}
