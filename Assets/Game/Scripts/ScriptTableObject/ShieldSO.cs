using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Shield")]
public class ShieldSO : ScriptableObject
{
    public List<ShieldData> shields;
}
