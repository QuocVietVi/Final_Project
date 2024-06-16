using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private GameObject InstructionPanel;
    [SerializeField] private Transform panelTransform;
    [SerializeField] private Button back, next, skip, play;

    private int index;

    private void Start()
    {
        index = 0;
        back.onClick.AddListener(PrevPanel);
        next.onClick.AddListener(NextPanel);
        skip.onClick.AddListener(() => { InstructionPanel.SetActive(false); }) ;
        play.onClick.AddListener(() => { InstructionPanel.SetActive(false); }) ;

    }

    private void NextPanel()
    {
        panelTransform.GetChild(index).gameObject.SetActive(false);
        index++;
        panelTransform.GetChild(index).gameObject.SetActive(true);
    }

    private void PrevPanel()
    {
        panelTransform.GetChild(index).gameObject.SetActive(false);
        index--;
        panelTransform.GetChild(index).gameObject.SetActive(true);
    }
}
