using Unity.MLAgents;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Agent[] Agents;
    public GameObject Ball;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        Agents[0].gameObject.transform.localPosition = new Vector3(0.0f, 0.5f, -7.0f);
        Agents[1].gameObject.transform.localPosition = new Vector3(0.0f, 0.5f, 7.0f);

        var speed = 10.0f;
        Ball.transform.localPosition = new Vector3(0.0f, 0.25f, 0.0f);
        var radius = Random.Range(45f, 135f) * Mathf.PI / 180.0f;
        var force = new Vector3(Mathf.Cos(radius) * speed, 0.0f, Mathf.Sin(radius) * speed);
        if (Random.value < 0.5f) force.z = -force.z;
        var rigidbody = Ball.GetComponent<Rigidbody>();
        rigidbody.velocity = force;
    }

    public void EndEpisode(int agentId)
    {
        if (agentId == 0)
        {
            Agents[0].AddReward(1.0f);
            Agents[1].AddReward(-1.0f);
        }
        else
        {
            Agents[0].AddReward(-1.0f);
            Agents[1].AddReward(1.0f);
        }
        Agents[0].EndEpisode();
        Agents[1].EndEpisode();
        Reset();
    }
}