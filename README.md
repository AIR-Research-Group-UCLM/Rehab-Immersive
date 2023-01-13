# Immersive VR framework for upper limb rehabilitation

Framework for the development of virtual reality clinical applications as a complement to the rehabilitation of patients with spinal cord injuries. 
A preconfiguration allows customization for each patient's specific needs. 
The framework also stores kinematics data, providing clinical staff with a valuable tool to evaluate progress and patient exercise performance. This repository contains an example of the proposed immersive virtual reality framework for upper limb rehabilitation, including a VR Box & Block test apk that utilizes the framework.


## Prerequisites 📋

1. To build the Unity package containing the scenes included in the .apk, Unity 2021.3.12f, Oculus Integration Pack v46.0 are required.
2. To run the application, the Oculus Quest 2 Meta, a data cable (or air link) to pass the information to the HMD and Meta Quest Developer Hub (or similar) are needed.

## Build apk

In the src\UnityPackage folder, there are two files with the .rar extension that contain the Unity package with the scenes used in the application. This package can serve as a starting point for new applications, as it contains the classes that make up the framework. To use it, it is necessary to first extract it. After the decompression, the RehabImmersivePack package can be imported into a Unity project. 
Steps to follow:
1. Create a new 3D core project with Unity v2021.3.12f1.
2. Install the Oculus Integration from the Asset Store (v46.0).
3. Import the package within the project. Go to Assets -> Import Package -> Custom Package.
4. Follow the configuration recommendations at https://developer.oculus.com/documentation/unity/unity-gs-overview/ and  https://developer.oculus.com/documentation/unity/unity-conf-settings/.
5. Select Android platform to Build de application.
6. Make sure to set up the hand tracking and select the high frequency. Go to OVRCameraRig -> OVRManager -> Quest Features -> set "Hand Tracking support" to "Hands Only" and select "Hand Tracking Frequency" to MAX.
7. Set into "Player Settings" the Company and Product name. 
8. Build de application.



### Installation 🔧

The installation of the application into the Headset can be done in two ways:

1. Building and running the Unity project with the Oculus Quest 2 connected to the computer.
2. The apk which can be transferred to Oculus Quest 2. Due to the size of the application exceeding the limit allowed by GitHub, it is divided into two .rar files. Once the contents are extracted, the VR_BoxAndBlock apk will be obtained, . Different programs can be used, in our case we recommend using Meta Quest Developer Hub.

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
├── TrackingData/

│	├── OculusTracking_1

│	└── OculusTracking_n

├── config.json

└── Historical.csv

## Repository

root/
├── apk/
│	├── VR_BoxAndBlock.part1
│	└── VR_BoxAndBlock.part2  
├── appDatappData/
│	└── com.RehabImmersive.boxAndBlock/
│		└── BoxAndBlock/
│				└── user13/
│					├── TrackingData
│					│	└──OculusTracking_20230113_103711
│					├── config.json
│					└──Historical.csv
└── src/
	├── UnityPackage/
	│	├──RehabImmersivePack.part1
	│	└──RehabImmersivePack.part2
	├── BadWall.cs
	├── Block.cs
	├── BlockCalibration.cs
	├── BlockUpdater.cs
	├── GameConfiguration.cs
	├── GeneratorCubes.cs
	├── GoalBox.cs
	├── GoodWall.cs
	├── Historical.cs
	├── HistoricalWritter.cs
	├── InitBox.cs
	├── LoadConfiguration.cs
	├── MainMenu.cs
	├── OutsideBox.cs 
	├── RehabConstants.cs
	├── StartGame.cs
	├── TrackingData.cs
	└── TrackingDataWritter.cs

## Contributors

- Vanesa Herrera (vanesa.herrera@uclm.es).
- David Vallejo (david.vallejo@uclm.es).
- José J. Castro-Schez (josejesus.castro@uclm.es).
- Dorothy N. Monekosso (dorothy.monekosso@durham.ac.uk).
- Ana de los Reyes (adlos@sescam.jccm.es).
- Carlos Glez-Morcillo (carlos.gonzalez@uclm.es).
- Javier Albusac (javieralonso.albusac@uclm.es).

## Copyright and license

Code released under the MIT License.
					  
				


