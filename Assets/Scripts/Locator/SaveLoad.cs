using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Responsible for saving and loading objects securily
/// </summary>
public class SaveLoad
{
    static readonly string fileDirectory = Application.persistentDataPath + "/";
    static readonly string profileFileName = "PlayerProfile";
    static readonly string fileType = ".txt";

    public static bool PlayerProfileExist()
    {
        return File.Exists(fileDirectory + profileFileName + fileType);
    }

    public static void SavePlayerProfile(PlayerProfile profile)
    {
        PlayerProfileSerialized playerProfileSerialized = new PlayerProfileSerialized(profile);
        XmlSerializer xml = new XmlSerializer(typeof(PlayerProfileSerialized));
        TextWriter writer = new StreamWriter(fileDirectory + profileFileName + fileType);
        xml.Serialize(writer, playerProfileSerialized);
        writer.Close();
    }

    public static PlayerProfile LoadPlayerProfile()
    {
        XmlSerializer xml = new XmlSerializer(typeof(PlayerProfileSerialized));
        FileStream fs = new FileStream(fileDirectory + profileFileName + fileType, FileMode.Open);
        var serializedProfile = (PlayerProfileSerialized)xml.Deserialize(fs);
        fs.Close();
        return serializedProfile.Deserialize();
    }
}
