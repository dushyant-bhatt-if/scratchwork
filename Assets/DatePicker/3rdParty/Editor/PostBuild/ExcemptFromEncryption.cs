
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
    using UnityEditor.iOS.Xcode;
#endif


public class MyBuildPostprocessor
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
#if UNITY_IOS
        string plistPath = pathToBuiltProject + "/Info.plist";

            PlistDocument plist = new PlistDocument(); // Read Info.plist file into memory
            plist.ReadFromString(File.ReadAllText(plistPath));

            PlistElementDict rootDict = plist.root;
            rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);

            File.WriteAllText(plistPath, plist.WriteToString()); // Override Info.plist
#endif
        
    }

   
}