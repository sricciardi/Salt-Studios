using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// AssetBundle cache checker & loader with caching
// worsk by loading .manifest file from server and parsing hash string from it


public class DownloadAssetBundleTest : MonoBehaviour
{
    public string assetBundleURLlocalhost = "http://localhost/ar-exp-saltbrochure-android";
    public string assetBundleURLsandy = "http://resources.sandyinc.com/st/testportal/ar-exp-saltbrochure-android";
    public string assetBundleURL = "http://s3.us-east-2.amazonaws.com/www.salt-generic-ar-app.com/ar-exp-saltbrochure-android";

    private string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

    public Text Statustext;
    //readonly bool isInstalled = false;
    bool isCheckingCache = false;
    bool isLoading = false;
    bool isCached = false;
    //bool isARInstalled = false;
    readonly GameObject AssetBundleObj;
    public Button LoadSceneButton;
    //private readonly string AWSABURL = "http://s3.us-east-2.amazonaws.com/www.salt-generic-ar-app.com/ar-exp-saltbrochure-android";
    //private readonly string AWSABmanifestURL = "http://s3.us-east-2.amazonaws.com/www.salt-generic-ar-app.com/ar-exp-saltbrochure-android.manifest";
    public string ABFileName;
    public string ABmanifestFileName;
    public string ABSceneName;
    private string myRandomHash;
    private int percent;
    private int cachePercent;

    //private UnityWebRequest uwrABManifest;
    private UnityWebRequest uwrAssetBundle;
    private UnityWebRequest uwrABManifest;

    private void Awake()
    {
        ClearCache();
    }
    void Start()
    {
        //StartCoroutine(IsARExperienceCached(assetBundleURLsandy));

        //StartCoroutine(DownloadAndCache(assetBundleURL));
        //Debug.Log("Running Start on Script");
        //RandomHash(32);
    }

    // Update is called once per frame
    void Update()
    {
        // progress

        if (isCheckingCache)
        {
            Debug.Log("I am Checking the Cache......");
            cachePercent = (int)(uwrABManifest.downloadProgress * 100);
            Statustext.color = Color.yellow;
            Statustext.text = "Checking Cache ..... " + cachePercent.ToString() + "%";
        } else if (isLoading)
        {
            Debug.Log("I am Loading the Asset Bundle......");
            percent = (int)(uwrAssetBundle.downloadProgress * 100);
            Statustext.color = Color.blue;
            Statustext.text = "Downloading ..... " + percent.ToString() + "%";
        } else if (!isCheckingCache && !isLoading)
        {
            if (isCached)
            {
                Debug.Log("I am Cached");
                Statustext.color = Color.green;
                Statustext.text = "Installed";
            }
            else if (!isCached)
            {
                Debug.Log("I am NOT Cached");
                Statustext.color = Color.red;
                Statustext.text = "Not Installed";
            }
        }
    }

    void ClearCache()
    {
        bool success = Caching.ClearCache();
        if (!success)
        {
            Debug.Log("Unable to clear cache!");

        }
        else
        {
            Debug.Log("Cache Cleared");
        }
    }

    public void DownloadABandCache()
    {
        StartCoroutine(DownloadAndCache(assetBundleURLsandy));
    }

    IEnumerator IsARExperienceCached(string bundleURL)
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
        {
            yield return null;
        }

        // if you want to always load from server, can clear cache first
        //        Caching.CleanCache();

        // get current bundle hash from server, random value added to avoid caching
        UnityWebRequest uwrABManifest = UnityWebRequest.Get(bundleURL + ".manifest?r=" + RandomHash(32));
        //UnityWebRequest uwr = UnityWebRequest.Get(bundleURL + ".manifest");
        Debug.Log("Loading manifest:" + bundleURL + ".manifest");
        isCheckingCache = true;
        // wait for load to finish
        yield return uwrABManifest.SendWebRequest();

        // if received error, exit
        if (uwrABManifest.isNetworkError)
        {
            Debug.LogError("www error: " + uwrABManifest.error);
            uwrABManifest.Dispose();
            uwrABManifest = null;
            yield break;
        }

        if (uwrABManifest.isDone)
        {
            isCheckingCache = false;
            Debug.Log("I finished downloading the manifest file, now I will check if its valid and look for a hash");
        }

        // create empty hash string
        Hash128 hashString = (default(Hash128));// new Hash128(0, 0, 0, 0);

        // check if received data contains 'ManifestFileVersion'
        Debug.Log("manifest file " + uwrABManifest.downloadHandler.text);
        if (uwrABManifest.downloadHandler.text.Contains("ManifestFileVersion"))
        {
            // extract hash string from the received data, TODO should add some error checking here
            var hashRow = uwrABManifest.downloadHandler.text.ToString().Split("\n".ToCharArray())[5];
            hashString = Hash128.Parse(hashRow.Split(':')[1].Trim());

            if (hashString.isValid == true)
            {
                // we can check if there is cached version or not
                if (Caching.IsVersionCached(bundleURL, hashString) == true)
                {
                    Debug.Log("Bundle with this hash is already cached!");
                    isCached = true;
                    yield break;
                    //isCached = true;
                }
                else
                {
                    Debug.Log("No cached version found for this hash..");
                    isCached = false;
                    yield break;
                    //isCached = false;
                }
            }
            else
            {
                // invalid loaded hash, just try loading latest bundle
                Debug.LogError("Invalid hash:" + hashString);
                yield break;
            }

        }
        else
        {
            Debug.LogError("Manifest doesn't contain string 'ManifestFileVersion': " + bundleURL + ".manifest");
            yield break;
        }
    }

    string RandomHash(int length)
    {
        myRandomHash = "";
        //int charAmount = UnityEngine.Random.Range(length, length);
        for (int i = 0; i < length; i++)
        {
            myRandomHash = myRandomHash + glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
            //Debug.Log("String " + i + " = " + myRandomHash);

        }
        Debug.Log("end of RandomHash method " + myRandomHash);
        return myRandomHash;
    }

    /// <summary>
    /// load assetbundle manifest, check hash, load actual bundle with hash parameter to use caching
    /// instantiate gameobject
    /// </summary>
    /// <param name="bundleURL">full url to assetbundle file</param>
    /// <param name="assetName">optional parameter to access specific asset from assetbundle</param>
    /// <returns></returns>
    /// IEnumerator DownloadAndCache(string bundleURL, string assetName = "")
    IEnumerator DownloadAndCache(string bundleURL)
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
        {
            yield return null;
        }

        // if you want to always load from server, can clear cache first
        //        Caching.CleanCache();

        // get current bundle hash from server, random value added to avoid caching
        UnityWebRequest uwrABManifest = UnityWebRequest.Get(bundleURL + ".manifest?r=" + RandomHash(32));
        //UnityWebRequest uwr = UnityWebRequest.Get(bundleURL + ".manifest");
        Debug.Log("Loading manifest:" + bundleURL + ".manifest");

        // wait for load to finish
        yield return uwrABManifest.SendWebRequest();

        // if received error, exit
        if (uwrABManifest.isNetworkError)
        {
            Debug.LogError("www error: " + uwrABManifest.error);
            uwrABManifest.Dispose();
            uwrABManifest = null;
            yield break;
        }

        // create empty hash string
        Hash128 hashString = (default(Hash128));// new Hash128(0, 0, 0, 0);

        // check if received data contains 'ManifestFileVersion'
        Debug.Log("manifest file " + uwrABManifest.downloadHandler.text);
        if (uwrABManifest.downloadHandler.text.Contains("ManifestFileVersion"))
        {
            // extract hash string from the received data, TODO should add some error checking here
            var hashRow = uwrABManifest.downloadHandler.text.ToString().Split("\n".ToCharArray())[5];
            hashString = Hash128.Parse(hashRow.Split(':')[1].Trim());

            if (hashString.isValid == true)
            {
                // we can check if there is cached version or not
                if (Caching.IsVersionCached(bundleURL, hashString) == true)
                {
                    Debug.Log("Bundle with this hash is already cached!");
                    isCached = true;
                }
                else
                {
                    Debug.Log("No cached version found for this hash..");
                    isCached = false;
                }
            }
            else
            {
                // invalid loaded hash, just try loading latest bundle
                Debug.LogError("Invalid hash:" + hashString);
                yield break;
            }

        }
        else
        {
            Debug.LogError("Manifest doesn't contain string 'ManifestFileVersion': " + bundleURL + ".manifest");
            yield break;
        }

        // now download the actual bundle, with hashString parameter it uses cached version if available
        uwrAssetBundle = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL + "?r=" + RandomHash(32), hashString, 0);
        Debug.Log("I am initiating a Unity Web Request for the Asset Bundle NOW.");
        // wait for load to finish
        isLoading = true;
        yield return uwrAssetBundle.SendWebRequest();
        Debug.Log("Unity has returned from the UWR.");
        if (uwrAssetBundle.error != null || uwrAssetBundle.isNetworkError)
        {
            Debug.LogError("www error: " + uwrAssetBundle.error);
            uwrAssetBundle.Dispose();
            uwrAssetBundle = null;
            yield break;
        }

        // get bundle from downloadhandler
        //AssetBundle bundle = ((DownloadHandlerAssetBundle)uwr.downloadHandler).assetBundle;
        Debug.Log("The UWR came back fine and now I am initiating the actual download of the Asset Bundle.");
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwrAssetBundle);

        //isLoading = true;

        if (uwrAssetBundle.isDone)
        {
            isLoading = false;
            //AssetBundleManifest manifest = bundle.LoadAsset<AssetBundleManifest>(bundle.name);
            Debug.Log("The Asset Bundle has been downloaded successfully.");
            Debug.Log("bundle name " + bundle.name);
            Debug.Log("is Streamed Asset Bundle " + bundle.isStreamedSceneAssetBundle);
        }


        //AssetBundle manifestBundle = AssetBundle.LoadFromFile(manifestBundlePath);
        //AssetBundleManifest manifest = bundle.LoadAssetAsync<AssetBundleManifest>(ABFileName);
        //Debug.Log("manifest " + manifest);

        string[] assetBundlePaths = bundle.GetAllScenePaths();

        if (assetBundlePaths != null)
        {

            //AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(sceneAssetBundle, levelName, isAdditive);
            //if (request == null)
            //    yield break;
            //yield return StartCoroutine(request);

            for (int i = 0; i < assetBundlePaths.Length; i++)
            {
                Debug.Log("string # " + i + " is " + assetBundlePaths[i]);
            }

            //// fix pink shaders, NOTE: not always needed..
            //foreach (Renderer r in go.GetComponentsInChildren<Renderer>(includeInactive: true))
            //{
            //    // FIXME: creates multiple materials, not good
            //    var material = Shader.Find(r.material.shader.name);
            //    r.material.shader = null;
            //    r.material.shader = material;
            //}

            SceneManager.LoadSceneAsync(bundle.GetAllScenePaths()[0]);
        }


        /*
        //GameObject bundlePrefab = null;

        // if no asset name is given, take the first/main asset
        if (assetName == "")
        {
            bundlePrefab = (GameObject)bundle.LoadAsset(bundle.GetAllAssetNames()[0]);
        }
        else
        { // use asset name to access inside bundle
            bundlePrefab = (GameObject)bundle.LoadAsset(assetName);
        }

        // if we got something out
        if (bundlePrefab != null)
        {

            // instantiate at 0,0,0 and without rotation
            Instantiate(bundlePrefab, Vector3.zero, Quaternion.identity);


            //// fix pink shaders, NOTE: not always needed..
            //foreach (Renderer r in go.GetComponentsInChildren<Renderer>(includeInactive: true))
            //{
            //    // FIXME: creates multiple materials, not good
            //    var material = Shader.Find(r.material.shader.name);
            //    r.material.shader = null;
            //    r.material.shader = material;
            //}
        }
        */

        uwrAssetBundle.Dispose();
        uwrAssetBundle = null;

        // try to cleanup memory
        Resources.UnloadUnusedAssets();
        bundle.Unload(false);
        bundle = null;
    }
}