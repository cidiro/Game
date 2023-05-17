using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceController : MonoBehaviour
{

    //They ID´s of the controllers, each will have a unique id so thats why we use them to identify them.
    //if the value in any them is -1 it will mean that theres not a controller id assigned for that player
    private int player1ControllerId=-1;
    private int player2ControllerId=-1;
    
    //At soon as the game starts we check if there are already controllers pro-plugged, in case that there are we will assign them to the player or players, depending
    //on if theres 1 or 2 controllers connected.
    private void Start() {
        DontDestroyOnLoad(this.gameObject);

        InputDevice[] devices =  InputSystem.devices.ToArray();//get all the devices connected at the moment
        foreach(InputDevice device in devices){//in each we will check if its a game pad
            if(device is Gamepad){//if it is, we eill check if any of the plyers doesnt have a controller already assigned, in case they not they will be assigned this one
                if(player1ControllerId == -1){
                    player1ControllerId = device.device.deviceId;
                    Debug.Log(device.name+" added for p1");
                }else if(player2ControllerId == -1){
                    player2ControllerId = device.device.deviceId;
                    Debug.Log(device.name+" added for p2");
                }else{
                    Debug.Log("Mandos de más");
                }
            }
        }
    }
    private void OnEnable() { //call OnDeviceUpdate everytime a controller is connected
        InputSystem.onDeviceChange += OnDeviceUpdate;
    }
    
    private void OnDisable() { //call OnDeviceUpdate everytime a controller is disabled/disconected
        InputSystem.onDeviceChange += OnDeviceUpdate;
    }

    //This method will be called everytime a decive is connected or disabled, in case its been connected we will see if theres any player available without a controller already asigned
    //in case there is we will asign it, in the other hand, if a controller is disabled we will check if it was assigned to a player, and in case it was we will un assign it and set the 
    //player controller id to -1 representing he doesnt have a controller assigned no more.
    private void OnDeviceUpdate(InputDevice device, InputDeviceChange change){ 
        if(change == InputDeviceChange.Added){
            Debug.Log("Mando de id " + device.name + " conectado");

            if(player1ControllerId == -1){
                Debug.Log("Config para p1");
                player1ControllerId = device.deviceId;
            }else if(player2ControllerId == -1){
                Debug.Log("Config para p2");
                player2ControllerId = device.deviceId;
            }else{
                Debug.Log("Numero de mandos excedido");
            }

        }else if(change == InputDeviceChange.Removed){
            Debug.Log("Mando de id " + device.name + " desconectado");

            if(player1ControllerId == device.deviceId){
                Debug.Log("Descon para p1");
                player1ControllerId = -1;
            }else if(player2ControllerId == device.deviceId){
                Debug.Log("Descon para p2");
                player2ControllerId = -1;
            }
        }
    }

    public int getPlayer1ControllerId(){
        return player1ControllerId;
    }

    public int getPlayer2ControllerId(){
        return player2ControllerId;
    }

    public int getPlayerControllerId(int playerId){
        if(playerId == 1){
            return player1ControllerId;
        }
        return player2ControllerId;
    }

    //Params-> playerId: the id of the player who´s controller device we want to obtain
    //method recieves the id of a player, we look at what controller id the player has assigned, 
    //and then search for the device an return it
    public InputDevice getDeviceOfPlayer(int playerId){
        int deviceId;
        if(playerId == 1){
            deviceId = player1ControllerId;
        }else{
            deviceId = player2ControllerId;
        }
        InputDevice[] devices =  InputSystem.devices.ToArray();
        foreach(InputDevice device in devices){
            if(device.deviceId == deviceId){
                return device;
            }
        }
        return null;
    }
}
