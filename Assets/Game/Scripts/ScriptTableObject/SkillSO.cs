using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Skill")]
public class SkillSO : ScriptableObject
{
    public List<SkillData> skills;
}
