# Immersive VR framework for upper limb rehabilitation

Framework for the development of virtual reality clinical applications as a complement to the rehabilitation of patients with spinal cord injuries. 
A preconfiguration allows customization for each patient's specific needs. 
The framework also stores kinematics data, providing clinical staff with a valuable tool to evaluate progress and patient exercise performance. This repository contains an example of the proposed immersive virtual reality framework for upper limb rehabilitation, including a VR Box & Block test apk that utilizes the framework.

## Prerequisites 📋

1. To build the Unity package containing the scenes included in the .apk, Unity 2021.3.12f, Oculus Integration Pack v46.0 are required.
2. To run the application, the Oculus Quest 2 Meta, a data cable (or air link) to pass the information to the HMD and Meta Quest Developer Hub (or similar) are needed.

### Instalation 🔧

The installation of the application into the Headset can be done in two ways:

1. Building and running the Unity project with the Oculus Quest 2 connected to the computer.
2. Directly passing the apk (referenced here) to the Oculus Quest 2. Different programs can be used, in our case we recommend using Meta Quest Developer Hub.

## Execution ⚙️

The application can be run directly from the glasses, or it can be run from Meta Quest Developer Hub (or another application that allows running adb commands). If you want to create a specific user to store data related to it (configuration, history, and cinematics), it is necessary to run it in the second way using a custom adb execution command: 

"adb -d shell am start -a android.intent.action.MAIN -c android.intent.category.LAUNCHER -S -f 0x10200000 -n com.RehabImmersive.boxAndBlock/com.unity3d.player.UnityPlayerActivity -e user userID".

- Package "com.RehabImmersive.boxAndBlock": is the package name given in Unity and will also be used to set the working directory. In the case of running VR_BoxAndBlock.apk, no changes are necessary. However, if a new apk is created from Unity using the "RehabImmersivePack" package, the developer must give this package name in the project configuration options.
- User "-e user userID": in this way, the application is given the identifier of the user that the user wants to create or whose configuration they want to load.

If no user identifier is specified, the default user will be created or loaded.

## Files generated during execution 

The files generated during execution are stored in the working directory. If the application is run directly on the headset, we can find it within the shared internal storage of the Oculus Quest 2, in the subdirectory: "\Android\data\com.RehabImmersive.boxAndBlock\BoxAndBlock".
Starting from this directory, the following file structure is generated:
UserID_1/
└── TrackingData/
    │   ├── OculusTracking_1
    │   └── ...
    └── config.json
    └── Historical.csv
UserID_2/
...
UserID_N/


