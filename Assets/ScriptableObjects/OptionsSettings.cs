using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OptionsSettings", menuName = "GameOptions")]
public class OptionsSettings : ScriptableObject
{
    public int musicVolume;
    public int sfxVolume;
}