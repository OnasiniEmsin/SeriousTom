using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int bonuses=30,index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            
            InventoryUI iui=GameObject.Find("Canvas/rukzak").GetComponent<InventoryUI>();
            
            iui.addBullet(index,bonuses);
            
            Destroy(gameObject);
        }
    }
}
