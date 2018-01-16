using System;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Linq;

public class ImportNinja : ScriptableObject
{
    public DirectoryInfo projectDirectory;
    public string unityDirectory;
    public string importNinja_template;

    public Dictionary<string, string> vars = new Dictionary<string, string>();

    // STATIC //

    static private string _ninjaData = "_ninjaData";
    static private string _editorTemplate = "Editor.ninjatemplate.cs.txt";

    static public string ninjaData { get { return _ninjaData; } }
    static public string editorTemplate { get { return _editorTemplate; } }

    static private ImportNinja _data;
    static public ImportNinja data
    {
        set { _data = value; }
        get
        {
            if (_data == null)
				_data = Resources.Load<ImportNinja>(_ninjaData);

            return _data;
        }
    }
}

#if UNITY_EDITOR

[InitializeOnLoad]
public class ImportNinjaInitializer
{
    static ImportNinjaInitializer()
    {
        Debug.Log("Your Personal Ninja");

        if (ImportNinja.data != null)
            return;

        ImportNinja.data = ScriptableObject.CreateInstance<ImportNinja>();
        ImportNinja data = ImportNinja.data;

        data.projectDirectory    = Directory.GetParent(Application.dataPath);
        data.unityDirectory      = Directory.GetParent(EditorApplication.applicationPath).FullName;
        data.importNinja_template = data.unityDirectory + @"\Data\Resources\ScriptTemplates\ImportNinjaTemplate\";

        data.vars.Add("PROJECTNAME", data.projectDirectory.Name);

        string from = data.importNinja_template;
        string to = Directory.GetParent(Application.dataPath).FullName;

        foreach (EditablePathInfo node in DirectoryCopy(from, to, copySubDirs: true))
        {
            foreach (KeyValuePair<string, string> rr in data.vars)
                node.nodeName = node.nodeName.Replace("#" + rr.Key + "#", rr.Value);

            string nodeName = node.nodeName;

            if (nodeName == ImportNinja.ninjaData)
            {
                AssetDatabase.CreateAsset(data, node.relativePath + ".asset");

                node.dontSave = true;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    /// <summary>
    ///     Source: https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
    ///     Modified by JeromeJ.
    /// </summary>
    /// <param name="vars"></param>

    private static IEnumerable<EditablePathInfo> DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);

        if (!dir.Exists)
            // throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            yield break;

        DirectoryInfo[] dirs = dir.GetDirectories();
        
        // If the destination directory doesn't exist, create it.
        Directory.CreateDirectory(destDirName);

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            EditablePathInfo path = new EditablePathInfo(Path.Combine(destDirName, file.Name));

            yield return path; // By reference

            if (path.dontSave)
                continue;

            try
            {
                file.CopyTo(path.fullPath);
            }
            catch (IOException)
            {

            }
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                EditablePathInfo path = new EditablePathInfo(Path.Combine(destDirName, subdir.Name));

                yield return path; // By reference

                if (path.dontSave)
                    continue;

                foreach (EditablePathInfo node in DirectoryCopy(subdir.FullName, path.fullPath, copySubDirs))
                {
                    // Can be modified as well before execution resumes
                    yield return node;
                }
            }
        }
    }
}

public class EditablePathInfo
{
    public bool dontSave = false;
    public string fullPath;

    public string nodeName
    {
        get { return Path.GetFileName(fullPath); }
        set {
            fullPath = Path.Combine(Path.GetDirectoryName(fullPath), value);
        }
    }

    public string relativePath
    {
        get { return new Uri(Application.dataPath).MakeRelativeUri(new Uri(fullPath)).ToString(); }
    }

    public EditablePathInfo(string _fullPath)
    {
        fullPath = _fullPath;
    }
}

#endif