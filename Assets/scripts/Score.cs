using TMPro;
using UnityEngine;
using System.IO;

public class Score : MonoBehaviour
{
    public TMP_Text text;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        load();
        text.text=score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setScore(int xp){
        score+=xp;
        save();
        text.text=score.ToString();
    }
    void save(){
        savePath = Application.persistentDataPath+"score" + "save.json";
        Dannie d=new Dannie();
        d.patrony=score;
        string json = JsonUtility.ToJson(d, true);
        File.WriteAllText(savePath, json);
    }

    private static string savePath ;

    public  bool SaveFileExists()
    {
        return File.Exists(savePath);
    }
    void load(){
        savePath = Application.persistentDataPath+"score" + "save.json";
        if(SaveFileExists()){
            
            string json = File.ReadAllText(savePath);
            Dannie d= JsonUtility.FromJson<Dannie>(json);
            score=d.patrony;
        }else{
            
            save();
        }
    }
}
