using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Forest : MonoBehaviour {

    public float startLength = 100;
    public float minLength = 10;
    public float maxLength = 100;
    float mindis = 200;
    float maxdis = 500;


    public GameObject[] obs;
    
    private Transform player;
    public WayPoints waypoints;
    private int targetWayPointIndex = 0;

    
    Dictionary<Vector3, int> ObsPosition = new Dictionary<Vector3, int>();
    private BattleController battleController;

    void Start() {
        player = GameController.Instance.player.transform;
        waypoints = this.transform.Find("wayPoints").GetComponent<WayPoints>();
        battleController = GameController.battleController;
        targetWayPointIndex = waypoints.waypoints.Length - 2;       
    }

    
    public Vector3 GetNextWayPoint() {
        while (true) {

            if (waypoints.waypoints[targetWayPointIndex].position.z - player.position.z < 0.3f )
            {
                targetWayPointIndex--;
                if (targetWayPointIndex < 0) 
                {
                    targetWayPointIndex = 0;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnvGenerator>().UpdateForest();                 
                    if (PlayerBigCollider.ontop==true)
                    {
                        return waypoints.waypoints[0].position + new Vector3(0, 21, 0);
                    }
                    else
                    {
                        return waypoints.waypoints[0].position;
                    }
                }
            } 
            else {
                if (PlayerBigCollider.ontop == true)
                {
                    return waypoints.waypoints[targetWayPointIndex].position + new Vector3(0, 21, 0);
                }
                else
                {
                    return waypoints.waypoints[targetWayPointIndex].position;
                }
            }
        }
    }

    public void GetDictionary(Dictionary<Vector3,int> dic)
    {
        ObsPosition = dic;
    }

    Vector3 GetWayPoint(float z) {
        Transform[] wps = waypoints.waypoints;
        int index = GetIndex(z);

        return Vector3.Lerp(wps[index + 1].position, wps[index].position, (z + wps[wps.Length-1].position.z - wps[index + 1].position.z) / (wps[index].position.z - wps[index + 1].position.z));
    }

    int GetIndex(float z) {
        Transform[] wps = waypoints.waypoints;
        float startZ = wps[wps.Length - 1].position.z;
        int index = 0;
        for (int i = 0; i < wps.Length; i++) {
            if (wps[i].position.z - startZ >= z) {
                index = i;
            } else {
                break;
            }
        }
        return index;
    }

    /*void GenerateObstacles() {
       if (GameController.Instance.isMaster)
       {
           int difficulty = PlayerMove.difficulty;
           float startZ = transform.position.z - 3000;
           float endz = transform.position.z;
           float z = startZ + startLength;
           while ((z += Random.Range(mindis, maxdis)) <= endz)
           {

               Vector3 position = GetWayPoint(z);
               int obsindex = 0;
               if (difficulty == 0)
               {
                   obsindex = Random.Range(0, obs.Length - 4);
                   int obsindex2 = Random.Range(0, obs.Length - 4);
                   ObsPosition.Add(position, obsindex);
                   GameObject go = GameObject.Instantiate(obs[obsindex2], position, Quaternion.identity) as GameObject;
                   go.transform.parent = this.transform;
               }
               else if (difficulty == 1)
               {
                   mindis = 200;
                   maxdis = 400; ObsPosition.Add(position, obsindex);
                   obsindex = Random.Range(0, obs.Length - 4);
                   GameObject go = GameObject.Instantiate(obs[obsindex], position, Quaternion.identity) as GameObject;
                   go.transform.parent = this.transform;
                   if (obsindex == 0 || obsindex == 4 || obsindex == 5)
                   {
                       float nextZ = z + Random.Range(75, 200);
                       Vector3 position1 = GetWayPoint(nextZ);
                       for (; ; )
                       {
                           int obsindex1 = Random.Range(4, 6);
                           if (obsindex1 == 4 || obsindex1 == 5)
                           {
                               if (Random.Range(0, 5) == 1)
                               {
                                   position1.x -= 14;
                               }
                               else
                               {
                                   position1.x += 14;
                               }
                               ObsPosition.Add(position, obsindex);
                               GameObject to = GameObject.Instantiate(obs[obsindex1], position1, Quaternion.identity) as GameObject;
                               to.transform.parent = this.transform;
                               break;
                           }
                       }
                   }
               }
               else if (difficulty == 2)
               {
                   obsindex = Random.Range(0, obs.Length - 2);
                   mindis = 150;
                   maxdis = 400; ObsPosition.Add(position, obsindex);
                   GameObject go = GameObject.Instantiate(obs[obsindex], position, Quaternion.identity) as GameObject;
                   go.transform.parent = this.transform;
                   if (obsindex == 0 || obsindex == 4 || obsindex == 5)
                   {
                       float nextZ = z + Random.Range(75, 200);
                       Vector3 position1 = GetWayPoint(nextZ);
                       for (; ; )
                       {
                           int obsindex1 = Random.Range(4, 6);
                           if (obsindex1 == 4 || obsindex1 == 5)
                           {
                               if (Random.Range(0, 4) == 1)
                               {
                                   position1.x -= 14;
                               }
                               else
                               {
                                   position1.x += 14;
                               }
                               ObsPosition.Add(position, obsindex);
                               GameObject to = GameObject.Instantiate(obs[obsindex1], position1, Quaternion.identity) as GameObject;
                               to.transform.parent = this.transform;
                               break;
                           }
                       }
                   }
               }
               else if (difficulty == 3)
               {
                   obsindex = Random.Range(0, obs.Length);
                   Debug.Log(obsindex);
                   mindis = 150;
                   maxdis = 300;
                   ObsPosition.Add(position, obsindex);
                   GameObject go = GameObject.Instantiate(obs[obsindex], position, Quaternion.identity) as GameObject;
                   go.transform.parent = this.transform;

                   if (obsindex == 0 || obsindex == 4 || obsindex == 6)
                   {
                       float nextZ = z + Random.Range(75, 150);
                       Vector3 position1 = GetWayPoint(nextZ);
                       for (; ; )
                       {
                           int obsindex1 = Random.Range(4, 7);
                           if (obsindex1 == 4 || obsindex1 == 6)
                           {
                               if (Random.Range(0, 3) == 1)
                               {
                                   position1.x -= 14;
                               }
                               else
                               {
                                   position1.x += 14;
                               }
                               ObsPosition.Add(position1, obsindex1);
                               GameObject to = GameObject.Instantiate(obs[obsindex1], position1, Quaternion.identity) as GameObject;
                               to.transform.parent = this.transform;
                               break;
                           }
                       }
                   }
               }
               else if (difficulty == 4)
               {
                   obsindex = Random.Range(0, obs.Length);
                   mindis = 125;
                   maxdis = 250; ObsPosition.Add(position, obsindex);
                   GameObject go = GameObject.Instantiate(obs[obsindex], position, Quaternion.identity) as GameObject;
                   go.transform.parent = this.transform;
                   if (obsindex == 0 || obsindex == 4 || obsindex == 5)
                   {
                       float nextZ = z + Random.Range(62, 125);
                       Vector3 position1 = GetWayPoint(nextZ);
                       for (; ; )
                       {
                           int obsindex1 = Random.Range(4, 6);
                           if (obsindex1 == 4 || obsindex1 == 5)
                           {
                               if (Random.Range(0, 2) == 1)
                               {
                                   position1.x -= 14;
                               }
                               else
                               {
                                   position1.x += 14;
                               }
                               ObsPosition.Add(position, obsindex);
                               GameObject to = GameObject.Instantiate(obs[obsindex1], position1, Quaternion.identity) as GameObject;
                               to.transform.parent = this.transform;
                               break;
                           }
                       }
                   }
               }
               else if (difficulty == 5)
               {
                   obsindex = Random.Range(0, obs.Length);
                   mindis = 100;
                   maxdis = 200; ObsPosition.Add(position, obsindex);
                   GameObject go = GameObject.Instantiate(obs[obsindex], position, Quaternion.identity) as GameObject;
                   go.transform.parent = this.transform;
                   if (obsindex == 4 || obsindex == 5)
                   {
                       float nextZ = z + Random.Range(50, 100);
                       Vector3 position1 = GetWayPoint(nextZ);
                       for (; ; )
                       {
                           int obsindex1 = Random.Range(4, 6);
                           if (obsindex1 == 4 || obsindex1 == 5)
                           {
                               if (Random.Range(0, 2) == 1)
                               {
                                   position1.x -= 14;
                               }
                               else
                               {
                                   position1.x += 14;
                               }
                               ObsPosition.Add(position, obsindex);
                               GameObject to = GameObject.Instantiate(obs[obsindex1], position1, Quaternion.identity) as GameObject;
                               to.transform.parent = this.transform;
                               break;
                           }
                       }
                   }
               }

           }
           battleController.SyncObstacles(ObsPosition);
       }
       else
       {
           Vector3 position;
           int index;
           GameObject to;
           foreach (var vv in ObsPosition)
           {
               position = vv.Key;
               index = vv.Value;
               to = GameObject.Instantiate(obs[index], position, Quaternion.identity) as GameObject;
               to.transform.parent = this.transform;
           }  
       }
   }*/
}
