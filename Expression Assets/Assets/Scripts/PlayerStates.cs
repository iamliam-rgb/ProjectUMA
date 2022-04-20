using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    //This script is responsible for the equiping and unequipping of items.
    //It manages a list of all Equipable.cs in the scene.
    //If the player is close enough to one of the equipables and presses the interact button (E for now) it will swap items.

    //There are different categories of items to let this script dectivate the old item to replace it with the new one.
    //Example would be an equipable shoe, it will deactiavte all the possible shoe models and then reactivate just the desired shoe.

    //**************
    //This library of strings is important. They are required by the Equipable.cs.
    //LIST OF ALL CATERGORIES//

    //Hats
    const string hats = "hats";

    //Outfits
    const string outfits = "outfits";

    //Shoes
    const string shoes = "shoes";
    
    //list of lists of model part categories
    [Header("Categories of model parts")]

    [Tooltip("list of all possible shoes attached to the female and male gameobjects")]
    [SerializeField] public List<GameObject> listOfAllShoes = new List<GameObject>();

    [Tooltip("list of all possible outfits attached to the female and male gameobjects")]
    [SerializeField] public List<GameObject> listOfAllOutfits = new List<GameObject>();

    [Tooltip("list of all possible hats attached to the female and male gameobjects")]
    [SerializeField] public List<GameObject> listOfAllHats = new List<GameObject>();

    [Tooltip("list of all possible accessories attached to the female and male gameobjects")]
    [SerializeField] public List<GameObject> listOfAllAccessories = new List<GameObject>();

    [Tooltip("list of all possible right handheld items attached to the female and male gameobjects")]
    [SerializeField] public List<GameObject> listOfAllrightHandItems = new List<GameObject>();

    //**************

    [Header("Player Object")]
    [Tooltip("reference to the player transform for its position.. This is not the parent player object, but the child object that moves")]
    //reference to player model position
    public Transform playerTransform;


    //list of all equipable.cs
    [Header("List of all Equipable.cs")]
    //Managing the logic behind all of the Equipable.cs
    [Tooltip("Make sure all equipable.cs in a scene are added to this list")]
    [SerializeField] public List<Equipable> listOfAllEquipableScripts = new List<Equipable>();

    //DEMO HAIRSTYLES
    //list of all hairstyles (for demo purposes)
    [Header("List of all hairstyles")]
    //Managing the logic behind all of the hairstyles
    [Tooltip("This list will be cycled through with the H key")]
    [SerializeField] public List<GameObject> listOfAllFemaleHairStyles = new List<GameObject>();
    [SerializeField] public List<GameObject> listOfAllMaleHairStyles = new List<GameObject>();
    [SerializeField] private int currentHairIndex;
    //DEMO HAIRSTYLES

    //this next true or false considers the possible/available character genders
    //this will determine which initial character model to set active
    [SerializeField] public enum CurrentGender { male, female, policeCruiser};
    public CurrentGender currentRace;
    [Header("Race")]
    [SerializeField] public List<GameObject> listOfAllRacePrefabs; //list of all genders



    public bool test;
    //index 0 = female
    //index 1 = male
    private void Update()
    {
        ManageEquipablesList(); //monitor the equipable items in the scene

        MonitorDesiredCharacterGender(); //if ever this changes mid game, this function covers it

        ManageHairStyles();
    }

    /// <summary>
    /// Manages the logic behind cycling hairstyles
    /// </summary>
    private void ManageHairStyles()
    {
        //cycle through
        if (Input.GetKeyDown(KeyCode.H) && currentHairIndex < 7) //temp
        {
            //deactivate old hairstyles
            foreach(GameObject hairstyle in listOfAllFemaleHairStyles)
                hairstyle.SetActive(false);

            foreach (GameObject hairstyle in listOfAllMaleHairStyles)
                hairstyle.SetActive(false);

            currentHairIndex++; //increase hair index

            //set the hair active
            listOfAllFemaleHairStyles[currentHairIndex].SetActive(true);
            listOfAllMaleHairStyles[currentHairIndex].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.H) && currentHairIndex == 7)
        {
            //deactivate old hairstyles
            foreach (GameObject hairstyle in listOfAllFemaleHairStyles)
                hairstyle.SetActive(false);

            foreach (GameObject hairstyle in listOfAllMaleHairStyles)
                hairstyle.SetActive(false);

            currentHairIndex = 0; //reset hair index

            //set the hair active
            listOfAllFemaleHairStyles[currentHairIndex].SetActive(true);
            listOfAllMaleHairStyles[currentHairIndex].SetActive(true);
        }
    }

    //monitoring the desired character model
    /// <summary>
    /// this function swaps the character from lad to lady or vice versa
    /// </summary>
    private void MonitorDesiredCharacterGender()
    {
        //the following allows for realtime changing of character race by pressing the 1,2 or 3 key
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentRace = CurrentGender.female;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentRace = CurrentGender.male;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentRace = CurrentGender.policeCruiser;

        switch (currentRace)
        {
            //index of 0
            case CurrentGender.female:
                for(int i = 0; i < listOfAllRacePrefabs.Count; i++)
                {
                    if (i == 0)
                        listOfAllRacePrefabs[i].SetActive(true);
                    else
                        listOfAllRacePrefabs[i].SetActive(false);
                }
                break;

                //index of 1
            case CurrentGender.male:
                for (int i = 0; i < listOfAllRacePrefabs.Count; i++)
                {
                    if (i == 1)
                        listOfAllRacePrefabs[i].SetActive(true);
                    else
                        listOfAllRacePrefabs[i].SetActive(false);
                }
                break;

            case CurrentGender.policeCruiser:
                for (int i = 0; i < listOfAllRacePrefabs.Count; i++)
                {
                    if (i == 2)
                        listOfAllRacePrefabs[i].SetActive(true);
                    else
                        listOfAllRacePrefabs[i].SetActive(false);
                }
                break;
        }
    }
    
    /// <summary>
    /// This function checks the distance from the player to all equipables.
    /// This function calls the EquipItem() function from this script if the player is within range and presses E
    /// </summary>
    private void ManageEquipablesList()
    {
        for(int i = 0; i < listOfAllEquipableScripts.Count; i++)
        {
            //distance check
            Equipable subjectEquipableScript = listOfAllEquipableScripts[i];
            float distance = (playerTransform.position - subjectEquipableScript.transform.position).magnitude;

            //if within range and intereact button (E) is pressed
            if(distance < subjectEquipableScript.interactRange && Input.GetKeyDown(KeyCode.E))
            {
                EquipItem(subjectEquipableScript); //call the equip function
                test = true;
            }
        }
    }

    /// <summary>
    /// This script deactivates all the equipped items in the corresponding list type
    /// This script activates the desired object
    /// </summary>
    /// 
    private void EquipItem(Equipable subjectEquipableScript)
    {
        string categoryOfItem = subjectEquipableScript.categoryOfItem; //get and set the category of the desired item

        subjectEquipableScript.PlayInteractSound(); //give sound feedback when equipped

        //Deactivate all items in the category that is being swapped
        switch (categoryOfItem)
        {
            //shoes
            case "shoes":

                //deactivate all the shoes
                foreach (GameObject shoe in listOfAllShoes)
                    shoe.SetActive(false);

                //equip the right shoe
                for (int i = 0; i < listOfAllShoes.Count; i++)
                {
                    for (int x = 0; x < subjectEquipableScript.objectsToEquip.Count; x++)
                    {
                        var shoe = listOfAllShoes[i];

                        if (shoe == subjectEquipableScript.objectsToEquip[x])
                        {
                            shoe.SetActive(true);
                        }
                    }
                }

                break;

            //outfits
            case "outfits":

                //deactivate all the outfits
                foreach (GameObject outfit in listOfAllOutfits)
                    outfit.SetActive(false);

                //equip the right shoe
                for (int i = 0; i < listOfAllOutfits.Count; i++)
                {
                    for (int x = 0; x < subjectEquipableScript.objectsToEquip.Count; x++)
                    {
                        var outfit = listOfAllOutfits[i];

                        if (outfit == subjectEquipableScript.objectsToEquip[x])
                        {
                            outfit.SetActive(true);
                        }
                    }
                }

                break;

            //hats
            case "hats":

                //deactivate all the shoes
                foreach (GameObject hat in listOfAllHats)
                    hat.SetActive(false);

                //equip the right hat
                for (int i = 0; i < listOfAllHats.Count; i++)
                {
                    for (int x = 0; x < subjectEquipableScript.objectsToEquip.Count; x++)
                    {
                        var hat = listOfAllHats[i];

                        if (hat == subjectEquipableScript.objectsToEquip[x])
                        {
                            hat.SetActive(true);
                        }
                    }
                }

                break;

            //accessories
            case "accessories":

                //!!!!!!!!!!!!!!!!
                //accessories will not unequip all the other accessories

                foreach (GameObject accessory in listOfAllAccessories)
                    accessory.SetActive(false);

                //equip the right accessory
                for (int i = 0; i < listOfAllAccessories.Count; i++)
                {
                    for (int x = 0; x < subjectEquipableScript.objectsToEquip.Count; x++)
                    {
                        var accessory = listOfAllAccessories[i];

                        if (accessory == subjectEquipableScript.objectsToEquip[x])
                        {
                            accessory.SetActive(true);
                        }
                    }
                }

                break;

            //rightHandItems
            case "rightHandItems":

                //deactivate all the right handheld items
                foreach (GameObject rightHandItem in listOfAllrightHandItems)
                    rightHandItem.SetActive(false);

                //equip the right handheld item
                for (int i = 0; i < listOfAllrightHandItems.Count; i++)
                {
                    for (int x = 0; x < subjectEquipableScript.objectsToEquip.Count; x++)
                    {
                        var rightHandItem = listOfAllrightHandItems[i];

                        if (rightHandItem == subjectEquipableScript.objectsToEquip[x])
                        {
                            rightHandItem.SetActive(true);
                        }
                    }
                }

                break;
        }
    }
}
