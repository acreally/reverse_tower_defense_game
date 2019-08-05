using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour
{
    // The transform of this game object
    Transform thisTransform;
    // The next waypoint that the soldier should travel to
    public Transform currentWaypoint;
    // The script containing the information about this soldier
    GameCharacterModel statsScript;

    /*
     * Property for currentWaypoint field.
     */
    public Transform CurrentWaypoint
    {
        get { return currentWaypoint; }
        set { currentWaypoint = value; }
    }

    void Awake()
    {
        thisTransform = transform;
    }

    void Start()
    {
        statsScript = GetComponent<GameCharacterModel>();
    }

    /*
     * Move this soldier towards its next waypoint.
     */
    void Update()
    {
        if (currentWaypoint)
        {
            Vector3 newPos = currentWaypoint.position;
            newPos.y = thisTransform.lossyScale.y + newPos.y;
            thisTransform.position = Vector3.MoveTowards(
                thisTransform.position, newPos,
                statsScript.CurrentSpeed * Time.deltaTime);
            thisTransform.LookAt(newPos);
        }
    }
}
