﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private Transform leftHandTrans;
    public GameObject arrowPrefab;

    private Vector3 shootDir;
	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
	    leftHandTrans = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");

    }

    // Update is called once per frame
    void Update () {
	    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
	    {
	        if (Input.GetMouseButtonDown(0))
	        {
	            Ray ray=    Camera.main.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit;
	            bool isCollider =    Physics.Raycast(ray, out hit);
	            if (isCollider)
	            {
	                Vector3 tartgetPoint = hit.point;
	                tartgetPoint.y = transform.position.y;
	                shootDir = tartgetPoint - transform.position;
                    transform.rotation = Quaternion.LookRotation(shootDir);
                    anim.SetTrigger("Attack");
                    Invoke("Shoot",0.3f);
                  
	            }

	        }
	    }
	}

    private void Shoot()
    {
        GameObject.Instantiate(arrowPrefab, leftHandTrans.position, Quaternion.LookRotation(shootDir));

    }
}
