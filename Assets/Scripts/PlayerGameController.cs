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
}
