using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DownloadAssetBundle : MonoBehaviour
{
    //public string loadUrl;
    public Text Statustext;
    readonly bool isInstalled = false;
    bool isLoading = false;
    bool isCached = false;
    readonly GameObject AssetBundleObj;
    public Button LoadSceneButton;
    private readonly string AWSABURL = "https://s3.us-east-2.amazonaws.com/www.salt-generic-ar-app.com/ar-exp-saltbrochure-android";
    private readonly string AWSABmanifestURL = "https://s3.us-east-2.amazonaws.com/www.salt-generic-ar-app.com/ar-exp-saltbrochure-android.manifest";
    public string ABFileName;
    public string ABmanifestFileName;
    public string ABSceneName;

    bool AssetBundleContainsAssetIWantToLoad(AssetBundle bundle)
    {
        string[] assetBundlePaths = bundle.GetAllScenePaths();

        if (assetBundlePaths != null)
        {
            Debug.Log("I found a Scene inside of the Asset Bundle.");
            //AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(sceneAssetBundle, levelName, isAdditive);
            //if (request == null)
            //    yield break;
            //yield return StartCoroutine(request);

            for (int i = 0; i < assetBundlePaths.Length; i++)
            {
                Debug.Log("string # " + i + " is " + assetBundlePaths[i]);
                return (assetBundlePaths[0] != null);
            }
        }
        Debug.Log("There is NO Scene in the Asset Bundle!");
        return false;

        //return (bundle.LoadAssetAsync<Scene>(ABSceneName) != null);     //this could be any conditional
    }

    private readonly string pathAB;
    private readonly string filePath;
    private UnityWebRequest UWRbundleRequest;
    private UnityWebRequest UWRmanifest;
    private DownloadHandler DHmanifest;
    //private UnityWebRequest UWRbundle;

    // Use this for initialization
    void Start()
    {
        //CheckABCacheVersion();

        StartCoroutine(DownloadAndCacheAB(AWSABURL, AWSABmanifestURL));

        //pathAB = Application.persistentDataPath + "/" + ABFileName;
        ////pathAB = Path.Combine(Application.persistentDataPath, ABFileName);
        //Debug.Log("pathAB " + pathAB);
        //filePath = "file://" + Application.persistentDataPath + "/" + ABFileName;
        ////filePath = "file://" + pathAB;
        ////filePath = Path.Combine("file://", pathAB);
        //Debug.Log("filePath " + filePath);
        ////isLoading = false;
        //////LoadSceneButton.gameObject.SetActive(false);
        ////if (File.Exists(filePath))
        //if (File.Exists(pathAB))
        //{
        //    isInstalled = true;
        //    Statustext.color = Color.green;
        //    Statustext.text = "Installed";
        //}
        //else
        //{
        //    isInstalled = false;
        //    Statustext.color = Color.red;
        //    Statustext.text = "Not Installed";
        //}
        ////StartCoroutine(LoadSceneBundle());
    }

    // Update is called once per frame
    void Update()
    {
        // progress

        if (isCached)
        {
            Statustext.color = Color.green;
            Statustext.text = "Installed";

        }
        else if (!isCached && isLoading)
        {
            int percent = (int)(UWRbundleRequest.downloadProgress * 100);
            Statustext.color = Color.blue;
            Statustext.text = "Downloading ..... " + percent.ToString() + "%";
        }
        else if (!isCached)
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

    //private WWW www;

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

    public void CheckABCacheVersion()
    {
        AssetBundle manifestBundle = AssetBundle.LoadFromFile(AWSABURL);
        //AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>(ABFileName);
        Hash128 hash = manifest.GetAssetBundleHash(ABFileName);

        if (Caching.IsVersionCached(AWSABURL, hash))
        {
            isCached = true;
            Debug.Log("the file is cached");
        }
        Debug.Log("Cached: " + isCached.ToString());
    }


    public void CheckAssetBundle()
    {
        //string pathAB = Application.persistentDataPath + "/" + ABFileName;

        //string filePath = "file://" + pathAB;
        //if (File.Exists(filePath) && isInstalled)
        if (isCached)
        {
            //Statustext.color = Color.green;
            //Statustext.text = "Installed";
            isLoading = false;
            //StartCoroutine(LoadFromMemoryAsync(pathAB));
            //StartCoroutine(LoadFromMemoryAsync(filePath));

            //StartCoroutine(LoadFromMemoryAsync(filePath));
        }
        else if (!isCached)
        {
            isLoading = true;
            ////Debug.Log(filePath);
            //StartCoroutine(LoadSceneBundle());
            ////StartCoroutine(LoadFromMemoryAsync(pathAB));
            //StartCoroutine(DownloadAndCacheAssetBundle(AWSABURL, AWSABmanifestURL));
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


    IEnumerator DownloadAndCacheAB(string bundleURLPath, string manifestURLBundlePath)
    {
        //AssetBundle manifestBundle = AssetBundle.LoadFromFile(manifestBundlePath);
        //AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string editorManifestFilePath = Application.dataPath + "/AssetBundles/Android/" + ABFileName + ".manifest";
        string manifestFilePath = Path.Combine(Application.persistentDataPath, ABmanifestFileName);
        string bundleFilePath = Path.Combine(Application.persistentDataPath, ABFileName);
        Debug.Log("Unity Editor manifest path " + editorManifestFilePath);

        if (Application.isEditor)
        {
            DHmanifest = new DownloadHandlerFile(editorManifestFilePath);
            Debug.Log("I am running in the Unity Editor!");
        } else
        {
            DHmanifest = new DownloadHandlerFile(manifestFilePath);
        }

        //DownloadHandlerFile dh = new DownloadHandlerFile(filePath);
        //DownloadHandlerAssetBundle DHAB = new DownloadHandlerAssetBundle(bundleFilePath);

        //DownloadHandler DHmanifest = new DownloadHandlerFile(manifestFilePath);   // Correct!

        UWRmanifest = UnityWebRequest.Get(manifestURLBundlePath);
        //UWRbundle = UnityWebRequestAssetBundle.GetAssetBundle(bundleURLPath);
        //uwr.downloadHandler = dhAB;
        UWRmanifest.downloadHandler = DHmanifest;
        yield return UWRmanifest.SendWebRequest();

        //UWRbundle.downloadHandler = DHAB;
        //yield return UWRbundle.SendWebRequest();

        //uwr.downloadHandler = dh;
        //yield return uwr.SendWebRequest();

        if (UWRmanifest.isNetworkError || UWRmanifest.isHttpError)
        {
            Debug.Log("Network error trying to retrieve the manifest file!");
        } else
        {
            Debug.Log("There was no Network error, so try to load the Asset Bundle from the local file");
            AssetBundle manifestBundle = AssetBundle.LoadFromFile(manifestFilePath);
            AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            //Create new cache
            string today = DateTime.Today.ToLongDateString();
            Directory.CreateDirectory(today);
            Cache newCache = Caching.AddCache(today);

            //Set current cache for writing to the new cache if the cache is valid
            if (newCache.valid)
            {
                Caching.currentCacheForWriting = newCache;
            }

            //Download the bundle
            Hash128 hash = manifest.GetAssetBundleHash(ABFileName);
            UnityWebRequest UWRbundleRequest = UnityWebRequestAssetBundle.GetAssetBundle(bundleURLPath, hash, 0);
            yield return UWRbundleRequest.SendWebRequest();
            AssetBundle UWRbundle = DownloadHandlerAssetBundle.GetContent(UWRbundleRequest);

            //Get all the cached versions
            List<Hash128> listOfCachedVersions = new List<Hash128>();
            Caching.GetCachedVersions(UWRbundle.name, listOfCachedVersions);

            if (!AssetBundleContainsAssetIWantToLoad(UWRbundle))
            {
                Caching.currentCacheForWriting = Caching.GetCacheAt(Caching.cacheCount);
                Caching.RemoveCache(newCache);
                Debug.Log("There is no Scene in the downloaded Asset Bundle.");

                for (int i = listOfCachedVersions.Count - 1; i > 0; i--)
                {
                    //Load a different bundle from a different cache.
                    UWRbundleRequest = UnityWebRequestAssetBundle.GetAssetBundle(bundleURLPath, listOfCachedVersions[i], 0);
                    yield return UWRbundleRequest.SendWebRequest();
                    UWRbundle = DownloadHandlerAssetBundle.GetContent(UWRbundleRequest);

                    //Check and see if the newly loaded bundle from the cache meets your criteria.
                    if (AssetBundleContainsAssetIWantToLoad(UWRbundle))
                    {
                        //This is where I need to load the scene the correct way.
                        break;
                    }
                }
            } else
            {
                Debug.Log("I found the scene I am looking for in the downloaded version.");
            }

        }




        /*
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        } else
        {
            //Get Asset Bundle
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
            
            isLoading = true;

            if (uwr.isDone)
            {
                
                //AssetBundleManifest manifest = bundle.LoadAsset<AssetBundleManifest>(bundle.name);
                Debug.Log("bundle name " + bundle.name);
                Debug.Log("is Streamed Asset Bundle " + bundle.isStreamedSceneAssetBundle);
            }


            //AssetBundle manifestBundle = AssetBundle.LoadFromFile(manifestBundlePath);
            //AssetBundleManifest manifest = bundle.LoadAssetAsync<AssetBundleManifest>(ABFileName);
            //Debug.Log("manifest " + manifest);

            string[] assetBundlePaths = bundle.GetAllScenePaths();

            if (assetBundlePaths != null) {

                //AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(sceneAssetBundle, levelName, isAdditive);
                //if (request == null)
                //    yield break;
                //yield return StartCoroutine(request);

                for (int i = 0; i < assetBundlePaths.Length; i++)
                {
                    Debug.Log("string # " + i + " is " + assetBundlePaths[i]);
                }
            }
            */




            //string filePath = Path.Combine(Application.persistentDataPath, ABFileName);

            //DownloadHandlerFile dh = new DownloadHandlerFile(filePath);

            //UnityWebRequest requestFile = UnityWebRequest.Get(url);

            //requestFile.downloadHandler = dh;

            
            //yield return requestFile.SendWebRequest();

            //if (!requestFile.isHttpError && !requestFile.isNetworkError)
            //{
            //    Debug.Log("Download complete, attempting to load bundle.");

            //    AssetBundle bundleFile = AssetBundle.LoadFromFile(filePath);

            //    SceneManager.LoadScene(bundleFile.GetAllScenePaths()[0]);
            //}
            //else
            //{
            //    Debug.LogError(requestFile.responseCode + ": " + requestFile.error);
            //}





            // Load level from assetBundle.



            ////AssetBundleRequest asset = bundle.LoadAssetAsync<AssetBundleManifest>(ABFileName);
            //AssetBundleRequest asset = bundle.LoadAsset<AssetBundleManifest>(ABFileName);
            //yield return asset;

            ////Get the Asset Bundle Manifest
            //AssetBundleManifest loadedAssetMf = asset.asset as AssetBundleManifest;
            ////Get Hash128 from the AssetBundleManifest
            //Hash128 tempHash128 = loadedAssetMf.GetAssetBundleHash(ABFileName);

            ////Pass to the isVersionCached function
            //if (Caching.IsVersionCached(bundlePath, tempHash128))
            //{
            //    isCached = true;
            //    Debug.Log("Bundle is Cached");
            //} else
            //{
            //    isLoading = true;
            //    Debug.Log("Bundle is Downloading....");
            //}
            ////Caching.IsVersionCached(bundlePath, tempHash128);
        }
    }


    /*
    IEnumerator DownloadAndCacheAssetBundle(string uri, string manifestBundlePath)
    {
        ////Load the manifest
        //AssetBundle manifestBundle = AssetBundle.LoadFromFile(manifestBundlePath);
        //AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>(ABFileName);

        ////Create new cache
        //string today = DateTime.Today.ToLongDateString();
        //Directory.CreateDirectory(today);
        //Cache newCache = Caching.AddCache(today);

        ////Set current cache for writing to the new cache if the cache is valid
        //if (newCache.valid)
        //{
        //    Caching.currentCacheForWriting = newCache;
        //}
 
        //Download the bundle
        //Hash128 hash = manifest.GetAssetBundleHash(ABFileName);
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri, hash, 0);
        yield return request.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        AssetBundleManifest manifest = bundle.LoadAsset<AssetBundleManifest>(ABFileName);
        Hash128 hash = manifest.GetAssetBundleHash(ABFileName);

        //Get all the cached versions
        List<Hash128> listOfCachedVersions = new List<Hash128>();
        Caching.GetCachedVersions(bundle.name, listOfCachedVersions);

        if (!AssetBundleContainsAssetIWantToLoad(bundle))     //Or any conditions you want to check on your new asset bundle
        {
            //If our criteria wasn't met, we can remove the new cache and revert back to the most recent one
            Caching.currentCacheForWriting = Caching.GetCacheAt(Caching.cacheCount);
            Caching.RemoveCache(newCache);

            for (int i = listOfCachedVersions.Count - 1; i > 0; i--)
            {
                //Load a different bundle from a different cache
                request = UnityWebRequestAssetBundle.GetAssetBundle(uri, listOfCachedVersions[i], 0);
                yield return request.SendWebRequest();
                bundle = DownloadHandlerAssetBundle.GetContent(request);

                //Check and see if the newly loaded bundle from the cache meets your criteria
                if (AssetBundleContainsAssetIWantToLoad(bundle))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ABSceneName);
                    bundle.Unload(false);
                    break;
                }
            }
        }
        else
        {
            //This is if we only want to keep 5 local caches at any time
            if (Caching.cacheCount > 5)
                Caching.RemoveCache(Caching.GetCacheAt(1));     //Removes the oldest user created cache
        }
    }
    */


    //IEnumerator LoadSceneBundle()
    //{
    //    Statustext.color = Color.blue;
    //    Statustext.text = "Downloading AR Experience ... ";
    //    //string SceneURL = "https://www.dropbox.com/s/fo4v777xvgrakrh/AssetScene.unity3d?dl=1";
    //    //string SceneURL = "https://drive.google.com/file/d/1pQfdeGILULu9SIxW7rHWN5ROKUlVpA5o/view?usp=sharing";
    //    //string SceneURL = "https://s3.us-east-2.amazonaws.com/www.salt-generic-ar-app.com/ar-exp-saltbrochure-android";

    //    www = new WWW(AWSABURL);
    //    yield return www;

    //    Debug.Log("Loaded ");
    //    if (www.error != null)
    //        throw new Exception("WWW download had an error: " + www.error);

    //    //File.WriteAllBytes(Application.persistentDataPath + "/AssetScene.unity3d", www.bytes);
    //    //File.WriteAllBytes(Application.persistentDataPath + "/ar-exp-saltbrochure-android", www.bytes);
    //    File.WriteAllBytes(Application.persistentDataPath + "/" + ABFileName, www.bytes);
    //    yield return new WaitForEndOfFrame();

    //    //Statustext.text = "";
    //    //Statustext.color = Color.green;
    //    //Statustext.text = "Installed";
    //    if (File.Exists(filePath))
    //    {
    //        isInstalled = true;
    //        Statustext.color = Color.green;
    //        Statustext.text = "Installed";
    //    }
    //    //isInstalled = true;

    //    LoadSceneButton.gameObject.SetActive(true);
    //}

    /*
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
    */
