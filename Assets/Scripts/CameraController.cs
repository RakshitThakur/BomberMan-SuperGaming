using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update

    public static Action<Transform> OnPlayerSpawned;
    private void Awake()
    {
        OnPlayerSpawned += AssignPlayer;
    }
    private void OnDestroy()
    {
        OnPlayerSpawned -= AssignPlayer;
    }
    void AssignPlayer(Transform playerTransform)
    {
        target = playerTransform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(target!= null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }
}
