using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorManager : MonoBehaviour
{
    // Start is called before the first frame update
    private SocketTesting IoTmanager;
    public Material Red;
    public Material Green;
    void Start()
    {
        IoTmanager = GameObject.Find("IoTDeviceProxy").GetComponent<SocketTesting>();
    }

    // Update is called once per frame
    void Update()
    {
        JSONObject json = new JSONObject(IoTmanager.jsonData);
        Debug.Log(json[gameObject.name]["on"].ToString());
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
