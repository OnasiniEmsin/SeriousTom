using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class Buttons : MonoBehaviour
{
    public GameObject deleteButton;
    public int patrony=30,id=007;
    public TMP_Text textOfBullets;
    InventoryUI iUI;
    int _myNumber=0;
    // Start is called before the first frame update
    void Start()
    {
        load();
        textOfBullets.text=patrony.ToString();
    }
    public void selected(){
        deleteButton.active=true;
        iUI.selectVeapon(_myNumber);

    }
    public void unSelected(){
        deleteButton.active=false;
    }
    public int getNumber(){
        return _myNumber;
    }
    public void setNumber(int i){
        _myNumber=i;
    }
    public void setInventory(InventoryUI iui){
        iUI=iui;
    }
    public void deleteMe(){
        iUI.deleteTheButton(this);
        Destroy(gameObject);
    }
    public void minus1(){
        patrony--;
        textOfBullets.text=patrony.ToString();
        save();//каждая стрелба сохранить
        if(patrony<=0){
            deleteMe();
        }
    }
    // Сохр. и загр. данних
    void save(){
        savePath = Application.persistentDataPath+id + "save.json";
        Dannie d=new Dannie();
        d.patrony=patrony;
        string json = JsonUtility.ToJson(d, true);
        File.WriteAllText(savePath, json);
        Debug.Log(savePath+"ga yozilvotti "+id);
    }

    private static string savePath ;

    public  bool SaveFileExists()
    {
        return File.Exists(savePath);
    }
    void load(){
        savePath = Application.persistentDataPath+id + "save.json";
        if(SaveFileExists()){
            Debug.Log("Bor");
            string json = File.ReadAllText(savePath);
            Dannie d= JsonUtility.FromJson<Dannie>(json);
            patrony=d.patrony;
        }else{
            Debug.Log("yo'q");
            save();
        }
    }
}
