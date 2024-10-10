using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FX", menuName = "FXs")]
public class FX : ScriptableObject
{
    public new string name;
    public string cardType = "FX";
    public Sprite image;
    public AudioClip audio;
    public string specific;
}
