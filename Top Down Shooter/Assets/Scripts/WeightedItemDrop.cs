using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeightedItemDrop : MonoBehaviour
{
    [SerializeField, Tooltip("The offset amount that a dropped item should spawn from the gameobject")]
    private float itemDropOffset = 1f;

    [SerializeField, Tooltip("The percent chance that an item will drop"), Range(0f, 100f)]
    private float itemDropChance = 50f;

    [Serializable]
    public struct WeightedObject
    {
        [Tooltip("The object that will be dropped if selected")]
        public GameObject item;

        [Tooltip("The weighted chance for the value to be selected")]
        public float weight;
    }

    [SerializeField, Tooltip(
         "The object to spawn and its weight. Weight can be thought of as the number of times that object " +
         "appears in the random choice \n \n I.E. if there are objects named hearts, diamonds, clubs, and spades, " +
         "with weights of 5,4,3, and 2 respectively placed in a bag then there are 5 hearts, 4 diamonds, etc. " +
         "in that bag.")]
    private WeightedObject[] itemDrops;

    private float[] cdfArray;

    /// <summary>
    /// This loads the CDF array with weights from the weightedObject array.weight
    /// </summary>
    private void seedCDFArray()
    {
        cdfArray = new float[itemDrops.Length];
        float previousWeight = 0f;
        for (int i = 0; i < itemDrops.Length; i++)
        {
            cdfArray[i] = previousWeight + itemDrops[i].weight;
            previousWeight = cdfArray[i];
        }
    }

    /// <summary>
    /// Chooses a random item from the array based off of weights
    /// </summary>
    private int selectItem()
    {
        int selectedIndex = Array.BinarySearch(cdfArray, Random.Range(0f, cdfArray.Last()));
        if (selectedIndex < 0)
            selectedIndex = ~selectedIndex;
        return selectedIndex;
    }

    private void Start()
    {
        seedCDFArray();
    }

    /// <summary>
    /// Determines if an item should be dropped or not on enemy death
    /// </summary>
    public void DropItem()
    {
        if (!(Random.Range(0f, 100f) < itemDropChance)) return;
        Vector3 position = transform.position;
        Instantiate(itemDrops[selectItem()].item, new Vector3(position.x + itemDropOffset, position.y + itemDropOffset,
            position.z + itemDropOffset), Quaternion.identity);
    }
}