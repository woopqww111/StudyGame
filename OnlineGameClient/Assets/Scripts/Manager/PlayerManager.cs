﻿using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class PlayerManager : BaseManager {

    public PlayerManager(GameFacade facade):base(facade)
    {
        
    }

    private RoleType currentRoleType;
    private GameObject currentRoleGameObject;
    private GameObject playerSyncRequest;
    private GameObject remoteRoleGameObject;
    public void SetCurrentRoleType(RoleType rt)
    {
        currentRoleType = rt;
    }
    private Transform rolePositions;
    private UserData userData;
    private Dictionary<RoleType,RoleData> roleDataDict = new Dictionary<RoleType, RoleData>();
    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }

    private void InitRoleDataDict()
    {
        roleDataDict.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Prefabs/Hunter_BLUE", "Prefabs/Arrow_BLUE", rolePositions.GetChild(0)));
        roleDataDict.Add(RoleType.Red, new RoleData(RoleType.Red, "Prefabs/Hunter_RED", "Prefabs/Arrow_RED", rolePositions.GetChild(1)));

    }

    public override void OnInit()
    {
        rolePositions = GameObject.Find("RolePositions").transform;
        InitRoleDataDict();
    }

    public void SpawnRoles()
    {

        foreach (RoleData rd in roleDataDict.Values)
        {
            GameObject go =  GameObject.Instantiate(rd.RolePrefab, rd.SpawnPosition, Quaternion.identity);

            if (rd.RoleType == currentRoleType)
            {
                currentRoleGameObject = go;
            }
            else
            {
                remoteRoleGameObject = go;
            }
        }
    }

    public GameObject GetCurrentRoleGameObject()
    {
        return currentRoleGameObject;
    }

    private RoleData GetRoleData(RoleType rt)
    {
        RoleData rd = null;
        roleDataDict.TryGetValue(rt,out rd);
        return rd;
    }
    public void AddControlScript()
    {
        currentRoleGameObject.AddComponent<PlayerMove>();
        PlayerAttack playerAttack =   currentRoleGameObject.AddComponent<PlayerAttack>();
        RoleType rt = currentRoleGameObject.GetComponent<PlayerInfo>().RoleType;
        RoleData rd = GetRoleData(rt);
        playerAttack.arrowPrefab = rd.ArrowPrefab;

    }

    public void CreateSyncRequest()
    {
        playerSyncRequest = new GameObject("PlayerSyncRequest");
        playerSyncRequest.AddComponent<MoveRequest>()
            .SetLocalPlayer(currentRoleGameObject.transform, currentRoleGameObject.GetComponent<PlayerMove>())
            .SetRemotePlayer(remoteRoleGameObject.transform);

    }
}
