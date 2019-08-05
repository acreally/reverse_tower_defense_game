using System.Collections;
using UnityEngine;
using System;

public class TowerController : MonoBehaviour 
{
    // The transform component of this game object
    Transform thisTransform;
    // The current target of this tower
    GameObject target;
    // The projectile that this tower shoots
    public GameObject projectile;
    // The base rate of fire for this tower
    public float baseRateOfFire;
    // The delay between shots
    float rateOfFire;
    // True if the rate of fire of this tower has been slowed
    bool slowed = false;
    // The firing range of this tower
    public int fireRange;

    /*
     * Property for fireRange field.
     */
    public int FireRange
    {
        get { return fireRange; }
        set { fireRange = value; }
    }

    /*
     * Property for baseRateOfFire field.
     */
    public float BaseRateOfFire
    {
        get { return baseRateOfFire; }
        set { baseRateOfFire = value; }
    }

    /*
     * Property for rateOfFire field.
     */
    public float RateOfFire
    {
        get { return rateOfFire; }
        set { rateOfFire = value; }
    }

    void Awake()
    {
        thisTransform = transform;
    }

	void Start() 
    {
        rateOfFire = baseRateOfFire;
        StartCoroutine(GetTarget());       
	}

    /*
     * Start the coroutine to slow the rate of fire of this tower.
     */
    public void Slow(float magnitude, float duration)
    {
        StartCoroutine(SlowCoroutine(magnitude, duration));
    }

    /*
     * Slow the rate of fire of this tower by magnitude percent for
     * duration seconds.
     */
    IEnumerator SlowCoroutine(float magnitude, float duration)
    {
        if (!slowed)
        {
            slowed = true;
            rateOfFire = rateOfFire * 1.33f;
            yield return new WaitForSeconds(duration);
            rateOfFire = baseRateOfFire;
            slowed = false;
        }        
    }

    /*
     * Instantiate a new projectile from a prefab.
     */
    IEnumerator CreateProjectile()
    {
        while (target && Vector3.Distance(thisTransform.position,
            target.transform.position) < fireRange && 
            target.layer == LayerMask.NameToLayer("Soldiers"))
        {
            GameObject newProjectile = (GameObject)
                Instantiate(projectile, thisTransform.position,
                Quaternion.identity);
            newProjectile.GetComponent<
                ProjectileController>().Target = target.transform;
            yield return new WaitForSeconds(rateOfFire);
        }
        yield return null;
    }

    /*
    * Set the soldier that is closest to the goal to be the target of this
    * tower.
    */
    IEnumerator GetTarget()
    {        
        Collider[] targets = Physics.OverlapSphere(thisTransform.position -
            Vector3.up * thisTransform.position.y,
            fireRange, 1 << LayerMask.NameToLayer("Soldiers"));
        MinHeap.Node[] nodes = new MinHeap.Node[targets.Length];
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++) 
            {
                float distance =
                    targets[i].GetComponent<
                    SoldierController>().CurrentWaypoint.GetComponent<
                    WaypointModel>().Distance +
                    Vector3.Distance(targets[i].transform.position,
                    targets[i].GetComponent<
                    SoldierController>().CurrentWaypoint.position);
                nodes[i] = new MinHeap.Node(distance, targets[i].gameObject);
            }
            MinHeap heap = new MinHeap(nodes);
            try
            {

                target = heap.ExtractMin().Soldier;
            }
            catch (HeapEmptyException)
            {
                StartCoroutine(GetTarget());
            }                            
            yield return StartCoroutine(CreateProjectile());                        
            StartCoroutine(GetTarget());
        }
        else
        {            
            yield return new WaitForFixedUpdate();
            StartCoroutine(GetTarget());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position - Vector3.up *
            gameObject.transform.position.y,
            fireRange);
    }
}
