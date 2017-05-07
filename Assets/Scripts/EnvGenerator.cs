using UnityEngine;
using System.Collections;

public class EnvGenerator : MonoBehaviour {

    //public GameObject[] forests;

    private int forestCount=2;

    public Forest forest1;
    public Forest forest2;
    public GameObject[] ways;
	// Use this for initialization
	void Start () 
    {
        //forest1 = GameController.Instance.forest1.GetComponent<Forest>();
        //forest2 = GameController.Instance.forest2.GetComponent<Forest>();
        forestCount -= 1;
	}

    Forest GenerateForest() {
        forestCount++;
        /*float z = 3000 * forestCount;
        int index = Random.Range(0, 3);//0 1 2 
        GameObject go = GameObject.Instantiate(forests[index], new Vector3(0, 0, z), Quaternion.identity) as GameObject;
        return go.GetComponent<Forest>();*/
        
        
        if (forestCount > 6)
        {
            return ways[6].GetComponent<Forest>();
        }
        else { return ways[forestCount].GetComponent<Forest>(); }
    }

    public void UpdateForest() {
        forest1 = forest2;
        forest2 = GenerateForest();
    }

	
}
