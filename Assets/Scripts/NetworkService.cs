using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService : MonoBehaviour
{
    private const string GameServerAddress = "http://name-your-server.com:8080";

    private readonly string _ItemEventEndpoint = GameServerAddress + "/item";

    public void PostPickUpItem(Item item)
    {
        string jsonBody = $"{{ \"item_id\": {item.Id} }}";
        StartCoroutine(SendJsonCoroutine(_ItemEventEndpoint, "POST", jsonBody));
    }

    public void DeleteLayOutItem(Item item)
    {
        string jsonBody = $"{{ \"item_id\": {item.Id} }}";
        StartCoroutine(SendJsonCoroutine(_ItemEventEndpoint, "DELETE", jsonBody));
    }

    private IEnumerator SendJsonCoroutine(string endpoint, string method, string jsonBody)
    {
        using (var webRequest = new UnityWebRequest(endpoint, method))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
                throw new Exception($"Error connecting game server: {webRequest.error}! Try later!");
        }
    }
}
