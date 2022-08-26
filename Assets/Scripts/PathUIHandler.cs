using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUIHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject pathModal;

    void Start()
    {
        
    }

    public void SelectNextEnemy()
    {
        pathModal.SetActive(true);
    }

}
