using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed;
   
    private float moveHorizontal;
    private float moveVertical;

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        characterController.Move(moveHorizontal * Vector3.forward * moveSpeed * Time.deltaTime);
        characterController.Move(-moveVertical * Vector3.right * moveSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Spawner.Instance.SpawnBomb(transform.position);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "enemy")
        {
            Debug.Log("Death");
            MenuManager.Instance.EnableGameEndMenuMenu(GameEndType.LOSE);
        }
    }
}
