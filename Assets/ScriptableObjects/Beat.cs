using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beat", menuName = "Beats")]
public class Beat : ScriptableObject
{
    public new string name;
    public string cardType = "Beat";
    public Sprite image;
    public AudioClip audio;
    public string specific;
}
