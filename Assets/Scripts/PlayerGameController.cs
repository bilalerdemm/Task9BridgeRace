using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerGameController : MonoBehaviour
{
    public GameObject spawnPoint;
    public CinemachineVirtualCamera myCam;
    public Vector3 firstSpawnPointPos;
    public List<GameObject> collectedStairs;
    public Rigidbody rb;


    private void Awake()
    {
        firstSpawnPointPos = spawnPoint.transform.localPosition;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (transform.childCount < 4)
        {
            spawnPoint.transform.localPosition = firstSpawnPointPos;
        }
        //Kamera acisinin toplananlara gore degismesi 
        if (collectedStairs.Count > 10)
        {
            //camera.transform.DOLocalMoveY(25, 1);
            //camera.transform.DOMove(new Vector3( camera.transform.position.x, 9,camera.transform.position.z), 1f);
            //myCam.transform.DORotate(new Vector3(38, 0, 0), 1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
            collectedStairs.Add(other.gameObject);
            other.gameObject.transform.parent = transform;
            //other.gameObject.transform.DOMove(spawnPoint.transform.position,1f);
            other.gameObject.transform.DOLocalJump(spawnPoint.transform.localPosition, .5f, 1, 1f);
            other.gameObject.transform.localRotation = spawnPoint.transform.localRotation;
            spawnPoint.transform.position += new Vector3(0, .13f, 0);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Checker"))
        {
            Debug.Log("Checker Trigger");
            if (collectedStairs.Count > 0)
            {
                collision.gameObject.transform.position += new Vector3(0, 0, 1.95f);
            }
        }

        if (collision.gameObject.CompareTag("Stair"))
        {
            if (collectedStairs.Count > 0)
            {
                if (!collision.gameObject.CompareTag("StairBlue"))
                {
                    Destroy(collectedStairs[collectedStairs.Count - 1].gameObject);
                    collectedStairs.RemoveAt(collectedStairs.Count - 1);
                }
                collision.gameObject.transform.GetComponent<MeshRenderer>().enabled = true;
                collision.transform.tag = "StairBlue";


            }
        }
    }
}
