using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour
{
    private IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        
        texture.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
        texture.Apply();
        
        
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/screenshot.png",bytes);

        Destroy(texture);
    }

    public void TakeScreenshot()
    {
        StartCoroutine("Screenshot");
    }
}
