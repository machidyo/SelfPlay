using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    public GameManager GameManager;
    public int AgentId;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.EndEpisode(AgentId);
    }
}
