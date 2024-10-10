using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melody", menuName = "Melodies")]
public class Melody : ScriptableObject
{
    public new string name;
    public string cardType = "Melody";
    public Sprite image;
    public AudioClip audio;
    public string specific;
}
