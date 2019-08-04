//Modified by 6D Games
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerZero : MonoBehaviour {

    [SerializeField]
    private TerrainController terrainController;

    [SerializeField]
    private PhotonView PV;

    [SerializeField]
    private float distance = 10;

    private void Start()
    {
        terrainController = GetComponent<TerrainController>();
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            if (Vector3.Distance(Vector3.zero, transform.position) > distance)
            {
                terrainController.Level.position -= transform.position;
                transform.position = Vector3.zero;//only necessary if player isn't a child of the level
            }
        } 
    }

}