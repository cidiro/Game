using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceController : MonoBehaviour
{

    //AQUI LOS ID
    private int player1ControllerId=-1;
    private int player2ControllerId=-1;
    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        InputDevice[] devices =  InputSystem.devices.ToArray();
        foreach(InputDevice device in devices){
            if(device is Gamepad){
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
    private void OnEnable() {
        InputSystem.onDeviceChange += OnDeviceUpdate;
    }
    
    private void OnDisable() {
        InputSystem.onDeviceChange += OnDeviceUpdate;
    }

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
                //Mostrar por pantalla que el juego solo soporta 3 mandos
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
