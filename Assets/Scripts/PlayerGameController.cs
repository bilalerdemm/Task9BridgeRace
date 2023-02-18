using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerGameController : MonoBehaviour
{
    public GameObject spawnPoint;
    public Vector3 firstSpawnPointPos;
    private void Awake()
    {
        firstSpawnPointPos = spawnPoint.transform.localPosition;
    }
    private void Update()
    {
        if (transform.childCount < 4)
        {
            spawnPoint.transform.localPosition = firstSpawnPointPos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
            other.gameObject.transform.parent = transform;
            //other.gameObject.transform.DOMove(spawnPoint.transform.position,1f);
            other.gameObject.transform.DOLocalJump(spawnPoint.transform.localPosition, .5f, 1, 1f);
            other.gameObject.transform.localRotation = spawnPoint.transform.localRotation;
            spawnPoint.transform.position += new Vector3(0, .15f, 0);
        }
    }
}
