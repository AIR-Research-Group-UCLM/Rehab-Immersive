Within src/, there are C# classes used in the VR Box And Block application. Some of them are specifically associated with this particular application, while others are more generic and can be adapted to different virtual reality rehabilitation applications. 

## Application classes
- BadWall.cs: detects if the passage from one compartment to another in the box has been done incorrectly in the difficult level, that is, by passing through the separator that is in the center of the wooden box.
- GoalBox: detects if the cube has come into contact with the target compartment.
- BlockUpdater: singleton used to update the number of blocks moved correctly, incorrectly, and the total number of blocks generated.
- GeneratorCubes: class responsible for generating three cubes in a randomly calculated position dynamically based on where the block output compartment is located. 
- OutsideBox.cs: class used to detect if the cube has fallen outside of the box.
- MainMenu.cs: class used to detect de interaction with the main menu of the game.
- StartGame.cs: used to start the game, it include methods for displaying the countdown and other information of the game.

## Framework classes
Regarding the classes that are part of the framework, the following are included:
### Block.cs
Class associated with a prefab of the same name. The prefab element can be located within the Unity project (in the Assets > Prefabs > Blocks category). This prefab is a cube with physical properties and "Hand Grab Interactable" components, so that hands can interact with it. It also recognizes all types of grip supported by Oculus. In addition to these properties, it also has visual properties (when the block is grabbed) and sound properties (pick up and release).
Block.cs randomly assigns one of the three colors used in the BBT test (red, blue, or yellow) to the block. It also obtains the "HandGrabInteractor" component, which allows it to interact with either (the right or left hand) and changes the grasp type when configured in "auto-grip" mode. In this case, the cube moves to the center of the hand when it is close to it.

### TrackingData.cs
Class that contains variables used for storing the kinematics data such as the frame number, time stamp, whether the hand is detected, the level of confidence, the position of the hand, the type of grasp being used (pinch, palm, or auto-grip), and the strength of the grasp for different fingers (thumb, index, middle, ring and pinky), hand velocity and wrist twist.

### TrackingDataWritter.cs
The TrackingDataWriter class is responsible for writing tracking data to a file. This data is converted to String and saved into a temporarily list before it is written to the file. The class implements the Singleton pattern, guaranteeing that only one instance of the class can exist at any given time. It includes a method called SetPathWritter, which sets the file path where the data will be written to. The SaveTrackingData method is used to store the tracking data to a list, which is then written to a file once the list reaches a certain threshold. This batch-writing method serves to prevent excessive file access and potential memory overflow. The class also includes a ResetAll method to reset its state and a WriteTrackingData method to write data to a CSV file. Additionally, it features a property called FilePath which allows for retrieval and modification of the file path where the data will be written to.

### Historical.cs
Historical is a class that is used to store information about a completed game session. The class contains variables that store data such as the user's ID, the name of the game, the date and time the game was played, the level the user was on, the amount of time the user spent playing the game, the number of blocks generated, the score the user achieved, the number of errors made, whether the right hand was used, whether the auto-grip was used, and the name of the tracking file. The class also contains a constructor that initializes default values for certain variables when an instance of the class is created. 

### HistoricalWritter.cs
HistoricalWritter is a class that is used to write historical data to a CSV file. 

### GameConfiguration.cs
GameConfiguration is a class that contains variables that store the configuration settings for a game. It includes variables for the user's hand preference, the timer for the game, the level of the game, whether or not the autogrip feature is enabled, the user's identification, the position of the box in the game, whether the level is easy (without walls) and the default values for all the variables when the class is instantiated. 

This class is used within a ScriptableObject created in Unity (DataSOGameConfiguration). In addition to the class it contains two other variables (block identifier that is grabbed and user path) that are relevant throughout the execution and that, thanks to this type of elements, remain in memory throughout all scenes.
software metadata

## Software metadata
In order to make the usage of the framework and its underlying data more understandable, we present below the main software metadata:
- Session metadata. After the patient finishes a virtual reality exercise, the system stores the corresponding working directory for the game and patient in a historical file that accumulates one row per session. This file contains information such as the user's ID, the name of the application, the level, the time it took to complete the exercise, whether automatic grip was used, the hand used to execute the exercise, and other data specific to the game. For example, when playing BBT, the system stores information about the generated blocks, blocks moved correctly, and blocks moved incorrectly. The counter for incorrectly moved blocks is only incremented when playing in Hard mode, where the player is not allowed to cross the separator wall between the compartments.
- Patient metadata. Currently, only a unique identifier is stored for each patient. This identifier is used to name the subdirectory where the configuration, history, and tracking files for a particular game will be stored.
- Tracking metadata. Every time the patient performs an exercise, a file is created containing relevant data extracted from the hand and wrist tracking. There is a set of generic metadata, including Frame, Time, HeadPosition, HandDetected, HighConfidence, HandPosition, HandVelocity and WristTwist. Additionally, there are specific metadata when the interaction involves gripping objects, such as IsPinchGrabbing, IsPalmGrabbing, IsAutogripGrabbing, StrengthThumbIndex, StrengthThumbMiddle, StrengthThumbRing, StrengthThumbPinky, StrengthPalmIndex, StrengthPalmMiddle, StrengthPalmRing and StrengthPalmPinky. These metadata are stored along with the session metadata and used for analysis of the patient's performance and progress.
- Configuration metadata. Configuration metadata provides information for setting up the virtual environment and the exercise session. It includes details such as whether the right hand is being used, the duration of the exercise session in seconds, the level of difficulty (ranging from 1 to n), whether the autogrip feature is enabled or not, the unique user ID, and the 3D spatial position of certain objects (such as the box in the BBT).
