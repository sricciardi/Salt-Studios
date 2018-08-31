using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class AssetBundleSample : MonoBehaviour
{
    //public string loadUrl;
    public Text Statustext;
    bool isInstalled = false;
    bool isLoading = false;
    GameObject AssetBundleObj;
    public Button LoadSceneButton;
    private string AWSURL = "https://s3.us-east-2.amazonaws.com/www.salt-generic-ar-app.com/ar-exp-saltbrochure-android";
    public string ABFileName;
    public string ABSceneName;

    private string pathAB;
    private string filePath;

    // Use this for initialization
    void Start()
    {
        pathAB = Application.persistentDataPath + "/" + ABFileName;
        //pathAB = Path.Combine(Application.persistentDataPath, ABFileName);
        Debug.Log("pathAB " + pathAB);
        filePath = "file://" + Application.persistentDataPath + "/" + ABFileName;
        //filePath = "file://" + pathAB;
        //filePath = Path.Combine("file://", pathAB);
        Debug.Log("filePath " + filePath);
        //isLoading = false;
        ////LoadSceneButton.gameObject.SetActive(false);
        //if (File.Exists(filePath))
        if (File.Exists(pathAB))
        {
            isInstalled = true;
            Statustext.color = Color.green;
            Statustext.text = "Installed";
        }
        else
        {
            isInstalled = false;
            Statustext.color = Color.red;
            Statustext.text = "Not Installed";
        }
        //StartCoroutine(LoadSceneBundle());
    }

    // Update is called once per frame
    void Update()
    {
        // progress

        if (isInstalled)
        {
            Statustext.color = Color.green;
            Statustext.text = "Installed";

        } else if (!isInstalled && isLoading)
        {
            int percent = (int)(www.progress * 100);
            Statustext.color = Color.blue;
            Statustext.text = "Downloading ..... " + percent.ToString() + "%";
        } else if (!isInstalled)
        {
            Statustext.color = Color.red;
            Statustext.text = "Not Installed";
        }

        //if (isLoading)
        //{
        //    int percent = (int)(www.progress * 100);
        //    Statustext.color = Color.red;
        //    Statustext.text = percent.ToString() + "%";
        //}
    }

    private WWW www;

    //private IEnumerator load(string soundAsset)
    //{
    //    // wait for the caching system to be ready
    //    while (!Caching.ready)
    //        yield return null;

    //    // load AssetBundle file from Cache if it exists with the same 		version or download and store it in the cache
    //    www = WWW.LoadFromCacheOrDownload(loadUrl, 1);
    //    yield return www;
    //    Statustext.text = "Loaded";
    //    if (www.error != null)
    //        throw new Exception("WWW download had an error: " + www.error);

    //    AssetBundle assetBundle = www.assetBundle;
    //    //Instantiate (assetBundle.mainAsset); // Instantiate(assetBundle.Load("AssetName"));
    //    AssetBundleObj = Instantiate(assetBundle.mainAsset) as GameObject;
    //    AssetBundleObj.transform.Find(soundAsset).GetComponent<AudioSource>().Play();
    //    // Unload the AssetBundles compressed contents to conserve memory
    //    assetBundle.Unload(false);
    //}

    //public void LoadSoundFiles(string soundAsset)
    //{
    //    isLoading = true;
    //    // Clear Cache
    //    Caching.ClearCache();

    //    if (AssetBundleObj)
    //    {
    //        Destroy(AssetBundleObj);
    //    }
    //    StartCoroutine(load(soundAsset));

    //}

    public void CheckAssetBundle()
    {
        //string pathAB = Application.persistentDataPath + "/" + ABFileName;

        //string filePath = "file://" + pathAB;
        //if (File.Exists(filePath) && isInstalled)
        if (File.Exists(pathAB) && isInstalled)
        {
            //Statustext.color = Color.green;
            //Statustext.text = "Installed";
            isLoading = false;
            //StartCoroutine(LoadFromMemoryAsync(pathAB));
            //StartCoroutine(LoadFromMemoryAsync(filePath));
            StartCoroutine(LoadFromMemoryAsync(filePath));
        } else if (!File.Exists(pathAB) && !isInstalled)
        {
            isLoading = true;
            //Debug.Log(filePath);
            StartCoroutine(LoadSceneBundle());
            //StartCoroutine(LoadFromMemoryAsync(pathAB));
        }
    }

    //public void LoadFromLocal()
    //{
    //    string path = "";

    //    path = "file://" + Application.persistentDataPath + "/ar-exp-saltbrochure-android";
    //    //path = "file://" + "C:/Users/sricciardi/Desktop/Projects/AR Projects/Salt-Generic-AR-App/Assets/AssetBundles/Android" + "/ar-exp-saltbrochure-android";
    //    Debug.Log(path);

    //    StartCoroutine(LoadFromMemoryAsync(path));
    //}

    IEnumerator LoadSceneBundle()
    {
        Statustext.color = Color.blue;
        Statustext.text = "Downloading AR Experience ... ";
        //string SceneURL = "https://www.dropbox.com/s/fo4v777xvgrakrh/AssetScene.unity3d?dl=1";
        //string SceneURL = "https://drive.google.com/file/d/1pQfdeGILULu9SIxW7rHWN5ROKUlVpA5o/view?usp=sharing";
        //string SceneURL = "https://s3.us-east-2.amazonaws.com/www.salt-generic-ar-app.com/ar-exp-saltbrochure-android";
        
        www = new WWW(AWSURL);
        yield return www;

        Debug.Log("Loaded ");
        if (www.error != null)
            throw new Exception("WWW download had an error: " + www.error);

        //File.WriteAllBytes(Application.persistentDataPath + "/AssetScene.unity3d", www.bytes);
        //File.WriteAllBytes(Application.persistentDataPath + "/ar-exp-saltbrochure-android", www.bytes);
        File.WriteAllBytes(Application.persistentDataPath + "/" + ABFileName, www.bytes);
        yield return new WaitForEndOfFrame();

        //Statustext.text = "";
        //Statustext.color = Color.green;
        //Statustext.text = "Installed";
        if (File.Exists(filePath))
        {
            isInstalled = true;
            Statustext.color = Color.green;
            Statustext.text = "Installed";
        }
        //isInstalled = true;

        LoadSceneButton.gameObject.SetActive(true);
    }

    IEnumerator LoadFromMemoryAsync(string path)
    {
        //var LoadedAssetScene = AssetBundle.LoadFromFile(Application.persistentDataPath + "/AssetScene.unity3d");
        var LoadedAssetScene = AssetBundle.LoadFromFile(Application.persistentDataPath + "/ar-exp-saltbrochure-android");
        //var LoadedAssetScene = AssetBundle.LoadFromFile(Application.persistentDataPath + "/" + ABFileName);
        //var LoadedAssetScene = AssetBundle.LoadFromFile(path);
        if (LoadedAssetScene == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield return new WaitForEndOfFrame();
        }
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SaltBrochure");
        //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ABSceneName);
        LoadedAssetScene.Unload(false);
    }
}