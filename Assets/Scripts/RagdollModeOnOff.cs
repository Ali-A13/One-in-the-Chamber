using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollModeOnOff : MonoBehaviour
{

    public CapsuleCollider characterMainCollider;
    public GameObject characterRig; // The parent object of the ragdoll
    public float forceMagnitude = 25f; // The force magnitude to apply to the ragdoll
    //public Animator characterAnimator; // The animator of the character
    Collider[] ragDollColliders; // The colliders of the ragdoll
    Rigidbody[] limbsRigidbodies; // The rigidbodies of the limbs

    // Start is called before the first frame update
    void Start()
    {
        GetRagdollBits();
        RagdollModeOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetRagdollBits()
    {

        ragDollColliders = characterRig.GetComponentsInChildren<Collider>();
        limbsRigidbodies = characterRig.GetComponentsInChildren<Rigidbody>();

    }

    public void RagdollModeOn()
    { 
        //characterAnimator.enabled = false; // Disable the animator

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true; // Enable the colliders
        }

        foreach (Rigidbody rb in limbsRigidbodies)
        {
            rb.isKinematic = false; // disable the rigidbodies kinematic
        }

        characterMainCollider.enabled = false; // disable the main collider
    }

    void RagdollModeOff()
    {
        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false; // Disable the colliders
        }

        foreach (Rigidbody rb in limbsRigidbodies)
        {
            rb.isKinematic = true; // Make the rigidbodies kinematic
        }

        //characterAnimator.enabled = true; // Enable the animator
        characterMainCollider.enabled = true; // Enable the main collider

    }

    public void ApplyForceToRagdoll(Vector3 forceDirection)
    {
        foreach (Rigidbody rb in limbsRigidbodies)
        {
            if (rb != null)
            {
                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }
}
