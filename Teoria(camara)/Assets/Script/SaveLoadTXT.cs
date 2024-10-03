using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SaveLoadTXT : MonoBehaviour
{
    // Start is called before the first frame update




    private void Start()
    {
        if (File.Exists("save.txt"))//cargar
        {
            try
            {
                StreamReader sr = new StreamReader("save.txt");
                float x = float.Parse(sr.ReadLine());
                float y = float.Parse(sr.ReadLine());
                float z = float.Parse(sr.ReadLine());
                int coin = int.Parse(sr.ReadLine());
                
                List<string> hoursAux = new List<string>();//Solo si se guarda lo ultimo
                while (!sr.EndOfStream)
                {
                    hoursAux.Add(sr.ReadLine());
                }


                sr.Close();

                transform.position = new Vector3(x, y, z);
                GameManager.instance.SetPoints(coin);
                GameManager.instance.SetHours(hoursAux);

            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }


        }
        
    }

    private void OnApplicationQuit()
    {
        StreamWriter sw = new StreamWriter("save.txt");//guardar
        sw.WriteLine(transform.position.x);
        sw.WriteLine(transform.position.y);
        sw.WriteLine(transform.position.z);
        sw.WriteLine(GameManager.instance.GetPoints());
        List<string> list = GameManager.instance.GetHours();
        list.Add(DateTime.Now.ToString("HH:mm:ss"));
        foreach (string s in list)
        {
            sw.WriteLine(s);
        }
        
        sw.Close();//importante!!


        // Update is called once per frame


    }       
       
    
}
