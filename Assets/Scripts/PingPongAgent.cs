using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PingPongAgent : Agent
{
    public int AgentId;
    public GameObject Ball;
    private Rigidbody ballRigidbody;

    public override void Initialize()
    {
        ballRigidbody = Ball.GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var dir = (AgentId == 0) ? 1.0f : -1.0f;
        sensor.AddObservation(Ball.transform.localPosition.x * dir);
        sensor.AddObservation(Ball.transform.localPosition.z * dir);
        sensor.AddObservation(ballRigidbody.velocity.x * dir);
        sensor.AddObservation(ballRigidbody.velocity.z * dir);
        sensor.AddObservation(transform.localPosition.x * dir);
    }

    public void OnCollisionEnter(Collision other)
    {
        AddReward(0.1f);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        var dir = (AgentId == 0) ? 1.0f : -1.0f;
        var action = (int) vectorAction[0];
        var pos = transform.localPosition;
        if (action == 1)
        {
            pos.x -= 0.2f * dir;
        }

        if (action == 2)
        {
            pos.x += 0.2f * dir;
        }

        if (pos.x < -4.0f) pos.x = -4.0f;
        if (pos.x > 4.0f) pos.x = 4.0f;

        transform.localPosition = pos;
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) actionsOut[0] = 1;
        if (Input.GetKey(KeyCode.RightArrow)) actionsOut[0] = 2;
    }
}