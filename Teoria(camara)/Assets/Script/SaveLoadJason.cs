using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
struct PlayerData
{
    public Vector3 position;
    public int score;
    public List<string> hours;
}
public class SaveLoadJason : MonoBehaviour
{
    public string fileName = "playerposition.json";
    // Start is called before the first frame update
    private void Start()
    {
        fileName = Application.persistentDataPath + '\\' + fileName;
        Load();
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    // Update is called once per frame
    

    void Save()
    {
        StreamWriter streamwriter = new StreamWriter(fileName);

        PlayerData playerdata = new PlayerData(); // instancio objeto que vamos a guardar 
        playerdata.position = transform.position; // se rellena de info
        playerdata.score = GameManager.instance.GetPoints();
        List<string> hoursAux = GameManager.instance.GetHours();
        hoursAux.Add(DateTime.Now.ToString("HH:mm:ss"));
        playerdata.hours = hoursAux;

        string json = JsonUtility.ToJson(playerdata);   // pasar de un objeto serializable a un formato JSON con un formato string
        streamwriter.WriteLine(json);


        streamwriter.Close();
    }

    void Load()
    {
        if (File.Exists(fileName))
        {
            StreamReader streamReader = new StreamReader(fileName);
            try
            {                                                                                       // de formato JSON a objeto serializable
                PlayerData playerdata = JsonUtility.FromJson<PlayerData>(streamReader.ReadToEnd()); // el streamReader lee el json entero y lo pasa a objeto serializable
                transform.position = playerdata.position;
                GameManager.instance.SetPoints(playerdata.score);
                GameManager.instance.SetHours(playerdata.hours);
            }
            catch (System.Exception e)
            {
                // sale el topo
                Debug.Log(e.Message);
            }
            streamReader.Close();
        }
    }
}