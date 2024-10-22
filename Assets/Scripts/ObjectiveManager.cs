using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }

    public GameObject monster;

    public Light objectiveOne;
    public Light objectiveTwo;
    public Light objectiveThree;

    public GameObject finalDoor;

    public bool objectiveOneCompleted = false;
    public bool objectiveTwoComplete = false;
    public bool objectiveThreeComplete = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        ObjectivesControl();

        if(objectiveOneCompleted || objectiveTwoComplete || objectiveThreeComplete)
        {
            if(monster != null) { monster.SetActive(true); }
            
        }

        if (objectiveOneCompleted && objectiveTwoComplete && objectiveThreeComplete)
        {
            finalDoor.GetComponent<InteractableObject>().doorLocked = false;
            finalDoor.GetComponent<InteractableObject>().doorOpenable = true;
        }
    }

    private void ObjectivesControl()
    {
        if (objectiveOneCompleted)
        {
            objectiveOne.color = Color.green;
        }

        if (objectiveTwoComplete)
        {
            objectiveTwo.color = Color.green;
        }

        if (objectiveThreeComplete)
        {
            objectiveThree.color = Color.green;
        }
    }
}
