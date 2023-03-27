using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public void SetUp(bool explode = true)
    {
        if(explode)
            StartCoroutine(Explode());
    }
    //private void OnEnable()
    //{
    //    StartCoroutine(Explode());
    //}
    IEnumerator Explode()
    {
        MenuManager.Instance?.StartBombTimer();
        yield return new WaitForSeconds(3f);
        RaycastHit hit1, hit2, hit3, hit4;
        Physics.Raycast(transform.position, Vector3.forward, out hit1, 3f);
        Physics.Raycast(transform.position, -Vector3.forward, out hit2, 3f);
        Physics.Raycast(transform.position, Vector3.right, out hit3, 3f);
        Physics.Raycast(transform.position, -Vector3.right, out hit4, 3f);

        //var colliders = Physics.OverlapSphere(transform.position, 3f);
        //foreach(var item in colliders)
        //{
        //    if(item.tag == "obstacle")
        //    {
        //        if(Mathf.Abs((item.transform.position - transform.position).x) >=1 && Mathf.Abs((item.transform.position - transform.position).z) >= 1)
        //        {
        //            gameObject.SetActive(false);
        //            yield break;
        //        }
        //        item.gameObject.SetActive(false);
        //        LevelGenerator.Instance.FreeASpot(item.transform.position);
        //    }
        //    if(item.tag == "enemy")
        //    {
        //        item.gameObject.SetActive(false);
        //    }
        //}
        if (hit1.collider != null)
        {
            if (hit1.collider.tag == "obstacle")
            {
                hit1.collider.gameObject.SetActive(false);
                Spawner.Instance.FreeASpot(hit1.collider.transform.position);
            }
            if (hit1.collider.tag == "enemy")
            {
                Spawner.Instance.EnemyDied = 1;
                hit1.collider.gameObject.SetActive(false);
            }
            if (hit1.collider.tag == "Player")
            {
                MenuManager.Instance.EnableGameEndMenuMenu(GameEndType.LOSE);
            }
        }
        if (hit2.collider != null)
        {

            if (hit2.collider.tag == "obstacle")
            {
                hit2.collider.gameObject.SetActive(false);
                Spawner.Instance.FreeASpot(hit2.collider.transform.position);
            }
            if (hit2.collider.tag == "enemy")
            {
                Spawner.Instance.EnemyDied = 1;
                hit2.collider.gameObject.SetActive(false);
            }
            if (hit2.collider.tag == "Player")
            {
                MenuManager.Instance.EnableGameEndMenuMenu(GameEndType.LOSE);
            }
        }
        if (hit3.collider != null)
        {

            if (hit3.collider.tag == "obstacle")
            {
                hit3.collider.gameObject.SetActive(false);
                Spawner.Instance.FreeASpot(hit3.collider.transform.position);
            }
            if (hit3.collider.tag == "enemy")
            {
                Spawner.Instance.EnemyDied = 1;
                hit3.collider.gameObject.SetActive(false);
            }
            if (hit3.collider.tag == "Player")
            {
                MenuManager.Instance.EnableGameEndMenuMenu(GameEndType.LOSE);
            }
        }
        if (hit4.collider != null)
        {

            if (hit4.collider.tag == "obstacle")
            {
                Debug.Log("Hit1" + "  -> " + hit4.collider);
                hit4.collider.gameObject.SetActive(false);
                Spawner.Instance.FreeASpot(hit4.collider.transform.position);
            }
            if (hit4.collider.tag == "enemy")
            {
                Spawner.Instance.EnemyDied = 1;
                hit4.collider.gameObject.SetActive(false);
            }
            if (hit4.collider.tag == "Player")
            {
                MenuManager.Instance.EnableGameEndMenuMenu(GameEndType.LOSE);
            }
        }
        gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, 3f);
    }
}

