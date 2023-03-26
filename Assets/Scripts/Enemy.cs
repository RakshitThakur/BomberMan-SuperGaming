using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 targetDirection;
    Vector3 targetPosition;
    [SerializeField] private float moveSpeed;
    struct PositionData
    {
        public Vector3 targetPosition;
        public Vector3 targetDirection;
        public float weight;
    }
    private void Start()
    {
        Spawner.OnSpotFreed += InitEnemy;
        PositionData data = FindPossiblePositions();
        if (data.targetPosition == null)
        {
            return;
        }
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
        targetDirection = data.targetDirection;
        targetPosition = data.targetPosition;
       
    }
    void InitEnemy()
    {
        if (targetDirection != Vector3.zero) return;
        PositionData data = FindPossiblePositions();
        if (data.targetPosition == null)
        {
            return;
        }
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
        targetDirection = data.targetDirection;
        targetPosition = data.targetPosition;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            PositionData data =  FindPossiblePositions();
            if(data.targetPosition == null)
            {
                return;
            }
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            targetDirection = data.targetDirection;
            targetPosition = data.targetPosition;
        }
        else
        {
            transform.Translate(targetDirection * moveSpeed * Time.deltaTime);
        }
    }

    PositionData FindPossiblePositions()
    {
        List<PositionData> possiblePlaces = new List<PositionData>();
        if (Spawner.Instance.GetEmptySpots().ContainsKey(new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z) + 1)))
        {
            float weight = 5f;
            if(targetDirection == Vector3.forward || targetDirection == -Vector3.forward)
            {
                weight = 1f;
            }
            possiblePlaces.Add(new PositionData
            {
                targetPosition = Spawner.Instance.GetEmptySpots()[new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z) + 1)].pos,
                targetDirection = Vector3.forward,
                weight = weight

            });
        }
        if (Spawner.Instance.GetEmptySpots().ContainsKey(new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z) - 1)))
        {
            float weight = 5f;
            if (targetDirection == -Vector3.forward || targetDirection == Vector3.forward )
            {
                weight = 1f;
            }
            possiblePlaces.Add(new PositionData
            {
                targetPosition = Spawner.Instance.GetEmptySpots()[new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z) - 1)].pos,
                targetDirection = -Vector3.forward,
                weight = weight

            });
        }
        if (Spawner.Instance.GetEmptySpots().ContainsKey(new Vector3((Mathf.RoundToInt(transform.position.x) + 1), 0, Mathf.RoundToInt(transform.position.z))))
        {
            float weight = 5f;
            if (targetDirection == Vector3.right || targetDirection == -Vector3.right)
            {
                weight = 1f;
            }
            possiblePlaces.Add(new PositionData
            {
                targetPosition = Spawner.Instance.GetEmptySpots()[new Vector3(Mathf.RoundToInt(transform.position.x) + 1, 0, Mathf.RoundToInt(transform.position.z))].pos,
                targetDirection = Vector3.right,
                weight= weight

            });
        }
        if (Spawner.Instance.GetEmptySpots().ContainsKey(new Vector3(Mathf.RoundToInt(transform.position.x) - 1, 0, Mathf.RoundToInt(transform.position.z) )))
        {
            float weight = 5f;
            if (targetDirection == -Vector3.right || targetDirection == Vector3.right)
            {
                weight = 1f;
            }
            possiblePlaces.Add(new PositionData
            {
                targetPosition = Spawner.Instance.GetEmptySpots()[new Vector3(Mathf.RoundToInt(transform.position.x) - 1, 0, Mathf.RoundToInt(transform.position.z))].pos,
                targetDirection = -Vector3.right,
                weight = weight

            });
        }
        //float totalWeight = 0;
        //float weightSum = 0;

        //foreach(var item in possiblePlaces)
        //{
        //    totalWeight += item.weight;
        //}
        //float random = Random.Range(0, totalWeight);
        //for (int i = 0; i < possiblePlaces.Count; i++)
        //{
        //    weightSum += possiblePlaces[i].weight;
        //    if (random < weightSum)
        //    {
        //        return possiblePlaces[i];
        //    }
        //}
        if(possiblePlaces.Count <=  0 )
        {
            return new PositionData();
        }
        else
            return possiblePlaces[Random.Range(0, possiblePlaces.Count)];
    }
}
