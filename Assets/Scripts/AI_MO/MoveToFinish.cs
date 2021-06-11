using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToFinish : Agent
{
    public Rigidbody2D agentRigidbody;
    [SerializeField] private Transform targetTransform;
    public override void Initialize()
    {
        agentRigidbody = GetComponent<Rigidbody2D>();
        switch (PlayerPrefs.GetInt("type"))
        {
            case 1:
                transform.position = new Vector2(0.5f, 0.5f);
                break;
            case 2:
                transform.position = new Vector2(0.75f, 0.4330127f);
                break;
            case 3:
                transform.position = new Vector2(0, 0);
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 10.0f;
            Debug.Log(Time.timeScale);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1.0f;
            Debug.Log(Time.timeScale);
        }
    }
    public override void OnEpisodeBegin()
    {
        
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float moveSpeed = 25f;
        agentRigidbody.velocity = moveSpeed * new Vector2(moveX, moveY);
        AddReward(-0.001f);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish") == true) {
            AddReward(1.0f);
            GameManager.GetInstance().CollisionCheck(collision, this.transform);
            EndEpisode();
        }      
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Wall") == true)
        {
            AddReward( +0.01f);
        }
    }
}
