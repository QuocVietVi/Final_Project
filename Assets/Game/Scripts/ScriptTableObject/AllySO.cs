using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ally")]
public class AllySO : ScriptableObject
{
    public List<AllyData> allies;
}
