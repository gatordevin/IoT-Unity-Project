using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorManager : MonoBehaviour
{
    // Start is called before the first frame update
    private SocketTesting IoTmanager;
    public Material Red;
    public Material Green;
    private LightClass light = new LightClass();
    private class LightClass
    {
        public bool on;
        public int brightness;
    }
    void Start()
    {
        sendData(false, 0);
        IoTmanager = GameObject.Find("IoTDeviceProxy").GetComponent<SocketTesting>();
    }
    public void sendData(bool on, int brightness){
        light.on = on;
        light.brightness = brightness;
        string json = JsonUtility.ToJson(light);
        json = "{\"" + gameObject.name + "\": " + json + "}";
        GameObject.Find("IoTDeviceProxy").GetComponent<SocketTesting>().buffer.Add(json);
    }
    // Update is called once per frame
    void Update()
    {
        JSONObject json = new JSONObject(IoTmanager.jsonData);
        //Debug.Log(json[gameObject.name]["on"].ToString());
        try{
            if(json[gameObject.name]["on"].ToString() == "true"){
                gameObject.GetComponent<MeshRenderer> ().material = Green;
            }else{
                gameObject.GetComponent<MeshRenderer> ().material = Red;
            }
            //gameObject.transform.localScale= new Vector3(float.Parse(json[gameObject.name]["brightness"].ToString()),float.Parse(json[gameObject.name]["brightness"].ToString()),float.Parse(json[gameObject.name]["brightness"].ToString()));
        }catch{

        }
    }
}
