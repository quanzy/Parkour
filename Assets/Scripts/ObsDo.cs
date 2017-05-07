using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObsDo : MonoBehaviour {
    public GameObject[] Obs;
    public int difficulty=0;
    Dictionary<int, Obs1> Roomdic1 = new Dictionary<int, Obs1>();
    Dictionary<int, Obs1> Forestdic1 = new Dictionary<int, Obs1>();
    Dictionary<int, Obs1> Streetdic1 = new Dictionary<int, Obs1>();

    Dictionary<int, Obs1> Roomdic2 = new Dictionary<int, Obs1>();
    Dictionary<int, Obs1> Forestdic2 = new Dictionary<int, Obs1>();
    Dictionary<int, Obs1> Streetdic2 = new Dictionary<int, Obs1>();

    public class Obs1
    {
        public int ObsType;
        public Vector3 position;
        public Obs1(int a,Vector3 b)
        {
            ObsType = a;
            position = b;
        }
    }
    
	// Use this for initialization
	void Start () {
        if (difficulty == -1)
        {

        }
        if (difficulty == 0)
        {
            RoomObsPosition1();
            ForestObsPosition1();
            RoomObsCreate1();
            ForestObsCreate1();
            StreetObsPosition1();
            StreetObsCreate1();
        }

        if (difficulty == 1)
        {
            RoomObsPosition2();
            ForestObsPowition2();
            RoomObsCreate2();
            ForestObsCreate2();
            StreetObsPosition2();
            StreetObsCreate2();
        }
        
    }


    void RoomObsPosition1()
    {
        Obs1[] obs1 = new Obs1[11];
        obs1[0] = new Obs1(0, new Vector3(-26, 0, 175));
        obs1[1] = new Obs1(0, new Vector3(-16, 0, 470));
        obs1[2] = new Obs1(0, new Vector3(-16, 0, 816));
        obs1[3] = new Obs1(0, new Vector3(-16, 0, 562));
        obs1[4] = new Obs1(0, new Vector3(-6, 0, 562));


        obs1[6] = new Obs1(2, new Vector3(-16, 0, 233));
        obs1[7] = new Obs1(2, new Vector3(-6, 0, 233));
        obs1[5] = new Obs1(2, new Vector3(-26, 0, 661));



        obs1[8] = new Obs1(1, new Vector3(-26, 0, 350));
        obs1[9] = new Obs1(1, new Vector3(-4, 0, 403));
        obs1[10] = new Obs1(1, new Vector3(-4, 0, 836));
        for (int i = 0; i < 11; i++)
        {
            Roomdic1.Add(i, obs1[i]);
        }
    }
    void ForestObsPosition1()
    {
        Obs1[] obs1 = new Obs1[22];
        obs1[0] = new Obs1(3, new Vector3(-18, 0, 1158));
        obs1[1] = new Obs1(3, new Vector3(22, 0, 1741));
        obs1[2] = new Obs1(3, new Vector3(3, 0, 2700));

        obs1[3] = new Obs1(4, new Vector3(-16, 0, 911));
        obs1[4] = new Obs1(4, new Vector3(39, 0, 1800));
        obs1[5] = new Obs1(4, new Vector3(27, 0, 2500));


        obs1[6] = new Obs1(6, new Vector3(24, 0, 2572));
        obs1[7] = new Obs1(6, new Vector3(-27, 0, 1500));
        obs1[8] = new Obs1(6, new Vector3(-16, 0, 1036));
        obs1[9] = new Obs1(6, new Vector3(-20, 0, 1453));
        obs1[10] = new Obs1(6, new Vector3(81, 0, 2200));

        obs1[11] = new Obs1(7, new Vector3(-7, 0, 1085));
        obs1[12] = new Obs1(7, new Vector3(62, 0, 2400));
        obs1[13] = new Obs1(7, new Vector3(-7, 0, 2780));

        obs1[14] = new Obs1(9, new Vector3(-27, 0, 1300));
        obs1[15] = new Obs1(9, new Vector3(-10, 0, 1615));
        obs1[16] = new Obs1(9, new Vector3(79, 0, 1979));
        obs1[17] = new Obs1(9, new Vector3(-11, 0, 2800));


        obs1[18] = new Obs1(10, new Vector3(-28, 0, 1376));
        obs1[19] = new Obs1(10, new Vector3(85, 0, 2101));
        obs1[20] = new Obs1(10, new Vector3(68, 0, 2300));
        obs1[21] = new Obs1(10, new Vector3(-16, 0, 2900));

        for (int i = 0; i < 22; i++)
        {
            Forestdic1.Add(i, obs1[i]);
        }
    }
    void StreetObsPosition1()
    {
        Obs1[] obs1 = new Obs1[11];
        obs1[0] = new Obs1(16, new Vector3(-37.2f, 7.4f, 3515.2f));
        obs1[1] = new Obs1(16, new Vector3(-19.7f, 7.4f, 3631f));
        obs1[2] = new Obs1(16, new Vector3(-24.7f, 7.4f, 4139f));

        obs1[3] = new Obs1(17, new Vector3(0, 0, 0));

        obs1[4] = new Obs1(18, new Vector3(-34f, 12.9f, 3168.3f));
        obs1[5] = new Obs1(18, new Vector3(-40.2f, 12.5f, 3677.3f));

        obs1[6] = new Obs1(19, new Vector3(-30.8f, 2.9f, 3399.9f));
        obs1[7] = new Obs1(19, new Vector3(-32.4f, 2.9f, 3611f));
        obs1[8] = new Obs1(19, new Vector3(-34.9f, 2.9f, 3821f));
        obs1[9] = new Obs1(19, new Vector3(-17.16f, 2.9f, 3994f));
        obs1[10] = new Obs1(19, new Vector3(-38.62f, 2.9f, 4189.63f));

        for (int i = 0; i < 11; i++)
        {
            Streetdic1.Add(i, obs1[i]);
        }
    }

    void ForestObsCreate1()
    {
        for (int i = 0; i < 22; i++)
        {
            GameObject go = GameObject.Instantiate(Obs[Forestdic1[i].ObsType], Forestdic1[i].position, Quaternion.identity) as GameObject;
            go.transform.parent = this.transform;
        }
        GameObject di = GameObject.Instantiate(Obs[14], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        di.transform.parent = this.transform;
    }
    void RoomObsCreate1()
    {
        for (int i = 0; i < 11; i++)
        {
            GameObject go = GameObject.Instantiate(Obs[Roomdic1[i].ObsType], Roomdic1[i].position, Quaternion.identity) as GameObject;
            go.transform.parent = this.transform;
        }
        GameObject di = GameObject.Instantiate(Obs[13],new Vector3(0,0,0), Quaternion.identity) as GameObject;
        di.transform.parent = this.transform;
    }
    void StreetObsCreate1()
    {
        for (int i = 0; i < 11; i++)
        {
            GameObject go = GameObject.Instantiate(Obs[Streetdic1[i].ObsType], Streetdic1[i].position, Quaternion.identity) as GameObject;
            go.transform.parent = this.transform;
        }
        GameObject dia = GameObject.Instantiate(Obs[15], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        dia.transform.parent = this.transform;
    }

    void ForestObsPowition2()
    {
        Obs1[] obs1 = new Obs1[27];


        obs1[0] = new Obs1(4, new Vector3(-16, 0, 911));
        obs1[1] = new Obs1(5, new Vector3(-16, 0, 970));
        obs1[2] = new Obs1(6, new Vector3(-16, 0, 1036));
        obs1[3] = new Obs1(7, new Vector3(-7, 0, 1085));
        obs1[4] = new Obs1(3, new Vector3(-18, 0, 1158));
        obs1[5] = new Obs1(8, new Vector3(-23, 0, 1236));
        obs1[6] = new Obs1(9, new Vector3(-27, 0, 1300));
        obs1[7] = new Obs1(10, new Vector3(-28, 0, 1376));
        obs1[8] = new Obs1(11, new Vector3(-27, 0, 1452));
        obs1[9] = new Obs1(12, new Vector3(-13, 0, 1533));
        obs1[10] = new Obs1(5, new Vector3(6, 0, 1679));
        obs1[11] = new Obs1(4, new Vector3(25, 0, 1750));
        obs1[12] = new Obs1(5, new Vector3(36, 0, 1830));
        obs1[13] = new Obs1(7, new Vector3(75, 0, 1910));
        obs1[26] = new Obs1(12, new Vector3(73, 0, 1955));
        obs1[14] = new Obs1(9, new Vector3(80, 0, 2000));
        obs1[15] = new Obs1(3, new Vector3(85, 0, 2076));
        obs1[16] = new Obs1(7, new Vector3(84, 0, 2139));
        obs1[17] = new Obs1(9, new Vector3(80, 0, 2222));
        obs1[18] = new Obs1(5, new Vector3(67, 0, 2300));
        obs1[19] = new Obs1(6, new Vector3(38, 0, 2438));
        obs1[20] = new Obs1(3, new Vector3(34, 0, 2514));
        obs1[21] = new Obs1(11, new Vector3(16, 0, 2622));
        obs1[22] = new Obs1(10, new Vector3(3, 0, 2700));
        obs1[23] = new Obs1(7, new Vector3(-7, 0, 2780));
        obs1[24] = new Obs1(5, new Vector3(-14, 0, 2860));
        obs1[25] = new Obs1(10, new Vector3(-16, 0, 2927));
        
        
        for (int i = 0; i < 27; i++)
        {
            Forestdic2.Add(i, obs1[i]);
        }
    }
    void RoomObsPosition2()
    {
        Obs1[] obs1 = new Obs1[17];
        obs1[0] = new Obs1(0, new Vector3(-26, 0, 175));
        obs1[1] = new Obs1(0, new Vector3(-5, 0, 271));
        obs1[2] = new Obs1(0, new Vector3(-26, 0, 432));
        obs1[3] = new Obs1(0, new Vector3(-15, 0, 539));
        obs1[4] = new Obs1(0, new Vector3(-15, 0, 769));
        obs1[5] = new Obs1(0, new Vector3(-5, 0, 769));

        
        obs1[6] = new Obs1(2, new Vector3(-16, 0, 382));
        obs1[7] = new Obs1(2, new Vector3(-16, 0, 233));
        obs1[8] = new Obs1(2, new Vector3(-4, 0, 382));
        obs1[9] = new Obs1(2, new Vector3(-4, 0, 604));
        obs1[10] = new Obs1(2, new Vector3(-26, 0, 843));
        obs1[11] = new Obs1(2, new Vector3(-16, 0, 843));
        obs1[12] = new Obs1(2, new Vector3(-4, 0, 843));



        obs1[13] = new Obs1(1, new Vector3(-24.5f, 0, 309));
        obs1[14] = new Obs1(1, new Vector3(-5, 0, 483));
        obs1[15] = new Obs1(1, new Vector3(-24.5f, 0, 552));
        obs1[16] = new Obs1(1, new Vector3(-24.5f, 0, 680));
        for (int i = 0; i < 17; i++)
        {
            Roomdic2.Add(i, obs1[i]);
        }
    }
    void StreetObsPosition2()
    {
        Obs1[] obs1 = new Obs1[19];
        obs1[0] = new Obs1(16, new Vector3(-37.2f, 7.4f, 3515.2f));
        obs1[1] = new Obs1(16, new Vector3(-33.9f, 7.4f, 3136.3f));
        obs1[2] = new Obs1(16, new Vector3(-37.6f, 7.4f, 3577f));
        obs1[3] = new Obs1(16, new Vector3(-20.7f, 7.4f, 3713.6f));
        obs1[4] = new Obs1(16, new Vector3(-26.82f, 7.4f, 4334.8f));
        obs1[5] = new Obs1(16, new Vector3(-45.46f, 7.4f, 4334.9f));

        obs1[6] = new Obs1(17, new Vector3(0, 0, 0));

        obs1[7] = new Obs1(18, new Vector3(-34f, 12.9f, 3168.3f));
        obs1[8] = new Obs1(18, new Vector3(-40.2f, 12.5f, 3677.3f));

        obs1[9] = new Obs1(19, new Vector3(-30.8f, 2.9f, 3399.9f));
        obs1[10] = new Obs1(19, new Vector3(-26.3f, 2.9f, 3062.7f));
        obs1[11] = new Obs1(19, new Vector3(-8.5f, 2.9f, 3163f));
        obs1[12] = new Obs1(19, new Vector3(-8.5f, 2.9f, 3251f));
        obs1[13] = new Obs1(19, new Vector3(-13.3f, 2.9f, 3560.6f));
        obs1[14] = new Obs1(19, new Vector3(-33.1f, 2.9f, 3626.7f));
        obs1[15] = new Obs1(19, new Vector3(-35.3f, 2.9f, 3826.7f));
        obs1[16] = new Obs1(19, new Vector3(-18.4f, 2.9f, 3991.3f));
        obs1[17] = new Obs1(19, new Vector3(-38.5f, 2.9f, 4203.4f));
        obs1[18] = new Obs1(19, new Vector3(-19.4f, 2.9f, 4203.4f));

        for (int i = 0; i < 19; i++)
        {
            Streetdic2.Add(i, obs1[i]);
        }
    }

    void ForestObsCreate2()
    {
        for (int i = 0; i < 27; i++)
        {

            GameObject go = GameObject.Instantiate(Obs[Forestdic2[i].ObsType], Forestdic2[i].position, Quaternion.identity) as GameObject;
            go.transform.parent = this.transform;
        }
        GameObject to = GameObject.Instantiate(Obs[21], new Vector3(0,0,0), Quaternion.identity) as GameObject;
        to.transform.parent = this.transform;
    }
    void RoomObsCreate2()
    {
        for (int i = 0; i < 17; i++)
        {
            GameObject go = GameObject.Instantiate(Obs[Roomdic2[i].ObsType], Roomdic2[i].position, Quaternion.identity) as GameObject;
            go.transform.parent = this.transform;
        }
        GameObject di = GameObject.Instantiate(Obs[20], new Vector3(0,0,0), Quaternion.identity) as GameObject;
        di.transform.parent = this.transform;
    }
    void StreetObsCreate2()
    {
        for (int i = 0; i < 19; i++)
        {
            GameObject go = GameObject.Instantiate(Obs[Streetdic2[i].ObsType], Streetdic2[i].position, Quaternion.identity) as GameObject;
            go.transform.parent = this.transform;
        }
        GameObject dia = GameObject.Instantiate(Obs[15], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        dia.transform.parent = this.transform;
    }
}
