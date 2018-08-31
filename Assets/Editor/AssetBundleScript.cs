using System.IO;
using UnityEditor;
public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles/Android")]
    static void BuildAndroidAssetBundles()
    {
        var assetBundleDirectory = "Assets/AssetBundles/Android";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        // BuildAssetBundleOptions.UncompressedAssetBundle is needed because Unity currently has a bug on Android as of 2018.1
        // where it will not display a video that is within an assetBundle that has compression. So compression must
        // be disabled on the assetBundle for it to work. Unity is currently trying to fix this bug, check later builds
        // to see if it has been corrected. (SR_07-13-2018)
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);
        //BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
    }
    [MenuItem("Assets/Build AssetBundles/IOS")]
    static void BuildIOSAssetBundles()
    {
        var assetBundleDirectory = "Assets/AssetBundles/IOS";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.iOS);
    }
}