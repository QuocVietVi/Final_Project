using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Level")]
public class LevelSO : ScriptableObject
{
    public List<Level> levels;
}