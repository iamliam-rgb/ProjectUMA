using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour
{
    //This script is responsible for equipping a defined object when the player interacts with it.
    //When the player is in range of the specified equipable and presses E, it will equip this item.

    //The PlayerStates.cs is responsible for checking and calling the functions attached to this script.
    //It does so by tracking all of the Equipable scripts in the scene.

    [HideInInspector]
    [SerializeField] public float currentDistanceFromPlayer; //distance to player

    [Tooltip("minimuim distance that the player needs to be to interact")]
    [SerializeField] public float interactRange; //mindistance player needs to be to interact

    [Tooltip("This object will be compared to a list of all the player model objects. Once it finds the matching item in the player prefab, it will set it active")]
    [SerializeField] public List<GameObject> objectsToEquip;

    [Tooltip("This string is reponsible for determining which list to search from. This allows this script to deactivate the other items in the list to replace it with this one")]
    [SerializeField] public string categoryOfItem;
    
}
