
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Season", menuName = "ScriptableObjects/Season", order = 1)]
public class Season : ScriptableObject
{
    public string seasonName;
    public List<Day> days;
}
