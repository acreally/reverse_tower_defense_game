using UnityEngine;
using System.Collections;

public class SmokeScreenBehaviour : MonoBehaviour
{
    /*
     * Wait for 3 seconds then destroy this game object.
     */
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
    /*
     * Set any soldier that enters the smoke screen to be invisible.
     */
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer ==
            LayerMask.NameToLayer("Soldiers"))
        {
            ChangeVisibility(collisionInfo, false);
        }
    }

    /*
     * Keep soldiers in the smoke screen invisible. A soldier may enter
     * another smoke screen before it exits one, which will make it
     * visible in the smoke screen.
     */
    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer ==
            LayerMask.NameToLayer("Soldiers"))
        {
            ChangeVisibility(collisionInfo, false);
        }
    }

    /*
     * Set the soldier to be visible again once it leaves the smoke
     * screen.
     */
    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer ==
            LayerMask.NameToLayer("Soldiers"))
        {
            ChangeVisibility(collisionInfo, true);
        }
    }

    /*
     * Set the soldier to be visible or not.
     */
    void ChangeVisibility(Collider collisionInfo, bool visibility)
    {
        GameCharacterModel soldierScript =
                collisionInfo.gameObject.GetComponent<
                GameCharacterModel>();
        soldierScript.Visible = visibility;
    }

    /*
     * Set any soldier still in the smoke screen when it disipates to
     * be visible again.
     */
    void OnDestroy()
    {
        Collider[] soldiers = Physics.OverlapSphere(transform.position,
            gameObject.transform.lossyScale.x,
            1 << LayerMask.NameToLayer("Soldiers"));
        foreach (Collider soldier in soldiers) 
        {
            ChangeVisibility(soldier, true);
        }
    }
}
