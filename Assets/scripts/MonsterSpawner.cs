using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] mutanty;
    public int numberOfEnemies=3;
    int minx=-8,maxx=8,miny=-4,maxy=4;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<numberOfEnemies;i++){
            Instantiate(mutanty[Random.Range(0,mutanty.Length)],new Vector2(Random.Range(minx,maxx),Random.Range(miny,maxy)),Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
