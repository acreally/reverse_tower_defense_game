using UnityEngine;
using System.Collections;

public class WaypointModel : MonoBehaviour {
    // The script that controls soldiers
    SoldierController soldierScript;
    // The next waypoint that soldiers should travel to
    public GameObject nextWaypoint;
    // The main camera for the sceen
    Camera mainCamera;
    // The distance from this waypoint to the goal
    public float distance;
    // The distance to the next waypoint
    public float distanceToNextWaypoint;
    // The transform of this waypoint
    Transform thisTransform;

    /*
     * Property for distance field.
     */
    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    /*
     * Property for distanceToNextWaypoint field.
     */
    public float DistanceToNextWaypoint
    {
        get { return distanceToNextWaypoint; }
        set { distanceToNextWaypoint = value; }
    }

    void Awake()
    {
        thisTransform = transform;
        if (nextWaypoint)
        {
            distanceToNextWaypoint = Vector3.Distance(
                thisTransform.position, nextWaypoint.transform.position);
        }
        else
        {
            distanceToNextWaypoint = 0.0f;
        }        
    }
	
	void Start() 
    {        
        mainCamera = Camera.main;
        GameObject waypoint = nextWaypoint;
        distance = distanceToNextWaypoint;
        while (waypoint)
        {
            WaypointModel waypointScript = waypoint.GetComponent<
                WaypointModel>();
            distance += waypointScript.DistanceToNextWaypoint;
            waypoint = waypointScript.nextWaypoint;
        }
	}

    void OnTriggerEnter(Collider collisionInfo) 
    {
        if (collisionInfo.gameObject.layer ==
            LayerMask.NameToLayer("Soldiers") ||
            collisionInfo.gameObject.layer ==
            LayerMask.NameToLayer("Invisible"))
        {
            soldierScript = collisionInfo.gameObject.GetComponent<
            SoldierController>();
            if (nextWaypoint)
            {
                soldierScript.CurrentWaypoint = nextWaypoint.transform;
            }
            else
            {
                Destroy(collisionInfo.gameObject);
                mainCamera.GetComponent<WaveInfo>().RaiseScore();
            }
        }
                
    }
}
