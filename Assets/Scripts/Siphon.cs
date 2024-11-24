using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siphon : MonoBehaviour
{
    [SerializeField] float timer = 5, pullForce = 1f, pushForce = 12f;

    private GameObject target;
    private Rigidbody targetRB;
    private bool inRange = false;
    private HingeJoint hinge;
    private JointMotor motor;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetRB = target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inRange)
        {
            PullPush();
        }
    }

    private void OnTriggerEnter(Collider other)    
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player in Range.");
            ChangeHingeMotorSpeed(-100f);
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") inRange = false;
        timer = 5; ChangeHingeMotorSpeed(-100);
    }
    void PullPush()
    {
        timer = Mathf.Clamp(timer - Time.fixedDeltaTime, 0, 8);
        Vector3 direction = (transform.parent.position - targetRB.position).normalized;

        if (timer > 0 && timer <= 5)
        {
            Debug.Log("Pulling...");
            //Start Motor for pulling
            var motorSpd = timer > 1 ? 16 : -200;
            ChangeHingeMotorSpeed(motorSpd);

            // Apply a force to the player's Rigidbody while ignoring mass
            targetRB.AddForce(direction * pullForce, ForceMode.Acceleration);
        }
        else if (timer == 0)
        {
            Debug.Log("Push!!!");
            targetRB.AddForce(-direction * pushForce, ForceMode.Impulse);

            //10 seconds interval before another pull
            timer = 8;
        }
    }
    void ChangeHingeMotorSpeed(float newVal)
    {
        hinge = GetComponentInChildren<HingeJoint>();
        motor = hinge.motor;
        motor.targetVelocity = newVal;

        // Assign the modified motor back to the hinge joint
        hinge.motor = motor;
    }
}
