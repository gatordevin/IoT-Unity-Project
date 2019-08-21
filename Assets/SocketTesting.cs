
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class SocketTesting : MonoBehaviour
{
    #region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 
    private bool firstTime = true;	
    private bool dataRecv = false;
    private List<JSONObject> buffer;
    public string jsonData;
	public string deviceColor;
	public string ipAddress = "172.16.0.30";
	#endregion  	
	// Use this for initialization 	
	void Start () {
		ConnectToTcpServer(); 
            
	}  	
	// Update is called once per frame
	void Update () {
        if(dataRecv){
            if(firstTime){
				Debug.Log("first time");
				DeviceInfoClass deviceInfoClass = new DeviceInfoClass();
				deviceInfoClass.Color = deviceColor;
				//SendMessage(JsonUtility.ToJson(deviceInfoClass));
				if(jsonData == "waiting"){
					SendMessage("Start");
            		SendMessage(DevicesJson());
				}
            	firstTime = false;
			}
            SendMessage("Data");
            dataRecv = false;
        }
        if(buffer != null){

        }
         
		
	}  
	public class DeviceInfoClass
    {
        public String Color;
    }	
    public class DevicesClass
    {
        public List<string> UnityLight = new List<string>();
        public List<string> UnityButton = new List<string>();
    }
    private String DevicesJson(){
        GameObject[] lights = GameObject.FindGameObjectsWithTag("IOTLight");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("IOTButton");
        DevicesClass devices = new DevicesClass();
        foreach (GameObject light in lights)
        {
            devices.UnityLight.Add(light.name);
        }
        foreach (GameObject button in buttons)
        {
            devices.UnityButton.Add(button.name);
        }
        string json = JsonUtility.ToJson(devices);
        return json;
    }
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer () { 		
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		} 	
	}  	
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData() { 		
		try { 			
			socketConnection = new TcpClient(ipAddress, 2000);  			
			Byte[] bytes = new Byte[1024];             
			while (true) { 				
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) { 					
					int length; 					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 						
						var incommingData = new byte[length]; 						
						Array.Copy(bytes, 0, incommingData, 0, length); 						
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData); 						
						//Debug.Log("server message received as: " + serverMessage);
                        dataRecv = true;
                        jsonData = serverMessage; 					
					} 				
				} 			
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	private void SendMessage(string data) {         
		if (socketConnection == null) {             
			return;         
		}  		
		try { 			
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream(); 			
			if (stream.CanWrite) {                 
				string clientMessage = data; 				
				// Convert string message to byte array.                 
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage); 				
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);            
			}         
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	} 
    void OnApplicationQuit()
    {
        SendMessage("Stop");
    }
}
