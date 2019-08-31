# This Repository
Is a Unity3D project that can be used as a template to create virtual IoT devices are visable and addable by a Mozilla IoT router when used in conjunction with this [repository](https://github.com/gatordevin/webthing-python-unity).
# Why?
  The purpose of this project was to enable virtual objects such as a virtual ligh switch to dynamically interact with real world smart device through rules created on the [Mozilla IoT router gateway](https://iot.mozilla.org/gateway/). 
  This has potential in AR and VR applications where the goal is to intertwine the real world and the virtual world. In this case it was used for a robotics competition to allow users wearing a [MagicLeap AR headset](https://www.magicleap.com/) to interact with real gameobjects and also to add visual effects to the headset when certain events happen in the [RDL](https://tech-garage.org/robot-drone-league/) game.
# Install
1. **Download** repository
```shell
git clone https://github.com/gatordevin/IoT-Unity-Project.git
```
2. **Download** and **Install** the Unity Hub
3. **Download** Unity Version *2019.2.0f1*
4. **Click** *Add* under the projects tab and navigate to where the IoT-Unity-Project directory was downloaded
5. **Double click** the project after it was added
6. **Download** and start python server, follow directions [here](https://github.com/gatordevin/webthing-python-unity)
7. In unity **select** the gameobject *IoTDevie Proxy* from the left side of the screen and **enter** the Ip address of the computer running the python server on the right side under *Socket Testing* and *Ip Address*
8. **Click** *run* and you should be able to see your devices in the Mozilla IoT gateway!
