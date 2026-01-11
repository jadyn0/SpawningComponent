using UnityEngine;
using System.Collections;

public class FishAI : MonoBehaviour
{
    public float minSwimWait = 1;
    public float maxSwimWait = 5;
    public bool canSwim = true;

    public LayerMask layerMask;
    public float swimRayDistance = 3f;
    public Rigidbody rb;
    public float maxSwimSpeed = 5f;
    public float minSwimSpeed = 2f;
    public float maxTurnSpeed = 0.7f;
    public float minTurnSpeed = -0.7f;

    public float turnMove = 3f;
    public float turnTurn = 0.5f;
    public float forward = 0.722f;

    private Transform target;
    void Start()
    {
        StartCoroutine(Swim());
    }

    private IEnumerator Swim()
    {
        while (true)
        {
            float interval = Random.Range(minSwimWait, maxSwimWait);        //code taken from https://www.youtube.com/watch?v=pQajI2xHe5U
            yield return new WaitForSeconds(interval);                      // random delay between fish swimming

            RaycastHit hit;
            // Does the ray intersect ground
            if (Physics.SphereCast(transform.position, 0.5f, transform.TransformDirection(Vector3.forward), out hit, swimRayDistance, layerMask))
            {
                canSwim = false;
            }
            else
            {
                canSwim = true;
            }
            float swimOrTurn = Random.Range(0, 5);

            if (canSwim == false)                                       // if the fish can't swim forward, it turns
            {
                float turnSpeed = Random.Range(minTurnSpeed, maxTurnSpeed);
                rb.AddTorque(new Vector3(0, turnSpeed, 0), ForceMode.Impulse);
            }
            else
            {
                if (swimOrTurn >= 1.5f)                                    // random chance to swim forward or turn a random amount
                {
                    float swimSpeed = Random.Range(minSwimSpeed, maxSwimSpeed);
                    rb.AddRelativeForce(new Vector3(0, 0, swimSpeed), ForceMode.Impulse);
                    float turnWhilstMoving = Random.Range(-0.1f, 0.1f);
                    rb.AddTorque(new Vector3(0, turnWhilstMoving, 0), ForceMode.Impulse);
                }
                else
                {
                    float turnSpeed = Random.Range(minTurnSpeed, maxTurnSpeed);
                    rb.AddTorque(new Vector3(0, turnSpeed, 0), ForceMode.Impulse);
                }
            }

        }
    }
}