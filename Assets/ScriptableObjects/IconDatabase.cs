using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IconDatabase", menuName = "ScriptableObjects/IconDatabases", order = 1)]
public class IconDatabase : ScriptableObject
{
    public List<RequestIcon> icons;

    public RequestIcon FindIconByName(string itemName)
    {
        foreach (RequestIcon icon in icons)
        {
            if (icon.iconType == itemName)
            {
                return icon;
            }
        }
        return null;
    }
}

[System.Serializable]
public class RequestIcon
{
    public string iconType;
    public Sprite image;
}
