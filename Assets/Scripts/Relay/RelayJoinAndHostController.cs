using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class RelayJoinAndHostController : NetworkBehaviour
{
    
    
    
    public static RelayJoinAndHostController  Instance;
    public TMP_InputField codeInput;
    public static string relayCodeTMP;
    public TMP_InputField nameInput;
    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.ClearSessionToken();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Instance = this;

    }
    public override void OnNetworkSpawn()
    {
        Instance = this;
    }

    
    public async void CreateRelay(){
        if(!IsValidName()) return; 
        try{
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(12);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();
            relayCodeTMP = joinCode;    
            SceneLoader.LoadNetwork(SceneLoader.Scene.Lobby);
        }
        catch(RelayServiceException e){
            Debug.Log(e);
        }
        

    }
    public void JoinRelay(){
        if (!IsValidName()) return;
        JoinRelay(codeInput.text);
    }
    private async void JoinRelay(string joinCode){
        try{
            JoinAllocation joinedAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            RelayServerData relayServerData = new RelayServerData(joinedAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();
        }
        catch(RelayServiceException e){
            Debug.Log(e);
        }
    }
    
    private bool IsValidName(){
        bool result = false;
        string checkedString = nameInput.text;
        foreach(char c in checkedString){
            if(c == ' '){
                checkedString.Remove(c);
            }
        }
        result = !(checkedString == "") && (nameInput.text.Count() < 10);
        Debug.Log("IsValidName = " + result.ToString());
        return result;
    }
    public void Disconnect()
    {
        DisconnectServerRpc(NetworkManager.Singleton.LocalClientId);        
    }
    [ServerRpc(RequireOwnership = false)]
    public void DisconnectServerRpc(ulong id){
        NetworkManager.Singleton.DisconnectClient(id);
    }


    
}
