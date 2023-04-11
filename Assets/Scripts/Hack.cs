using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Hack : MonoBehaviour
{
    private const string ADBPath = "Assets/Plugins/platform-tools/adb.exe";
    private const string PhoneFilePath = "/sdcard/Android/data/com.CasualGamer.Casualgamer/files/GameData.dat";
    private const string LocalFilePath = "Assets/Target/GameData.dat";

    private GameData _gameData;

    /// <summary>
    /// Bob aici se ia de pe device si se da load la clasa
    /// </summary>
    public void Load()
    {
        if (File.Exists(LocalFilePath))
        {
            File.Delete(LocalFilePath);
            Debug.LogWarning("Local Old File Deleted");
        }

        DownloadFileFromPhone();

        _gameData = LoadFromTarget();
    }

    /// <summary>
    /// Bob aici setezi values
    /// </summary>
    public void SetCoins(int coins = 4000)
    {
        if (_gameData == null)
        {
            Debug.LogWarning("Not Loaded");
            return;
        }

        _gameData.SetCoins(coins);
        
    }

    /// <summary>
    /// Bob tr save la sf
    /// </summary>
    public void Save()
    {
        SaveToTarget(_gameData);
        UploadFileToPhone();
    }

    public void Start()
    {
        // if (!File.Exists(LocalFilePath))
        //     DownloadFileFromPhone();
        // else Debug.LogWarning("exist");
        //
        // var gameData = LoadFromTarget();
        //
        // if (gameData != null)
        // {
        //     Debug.LogWarning(gameData.coins);
        //     gameData.coins = 4000;
        //     SaveToTarget(gameData);
        //
        //     UploadFileToPhone();
        // }
    }

    private static void DownloadFileFromPhone()
    {
        var process = new Process();
        process.StartInfo.FileName = ADBPath;
        process.StartInfo.Arguments = $"pull {PhoneFilePath} {LocalFilePath}";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();

        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Debug.Log(output);
    }

    private static void UploadFileToPhone()
    {
        var process = new Process();
        process.StartInfo.FileName = ADBPath;
        process.StartInfo.Arguments = $"push {LocalFilePath} {PhoneFilePath}";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();

        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Debug.Log(output);
    }

    public static void SaveToTarget(GameData gameData)
    {
        FileStream file = null;
        try
        {
            var bf = new BinaryFormatter();

            file = File.Open(LocalFilePath, FileMode.Open);
            bf.Serialize(file, gameData);
        }
        finally
        {
            file?.Close();
        }
    }

    [CanBeNull]
    private static GameData LoadFromTarget()
    {
        if (!File.Exists(LocalFilePath)) return null;

        FileStream file = null;

        try
        {
            var bf = new BinaryFormatter();
            file = File.Open(LocalFilePath, FileMode.Open);
            var gameData = (GameData) bf.Deserialize(file);

            return gameData;
        }
        finally
        {
            file?.Close();
        }
    }
}

// [Serializable]
// public class GameData
// {
//     private bool isGameStartedFirstTime;
//
//     private int highScore;
//
//     public int coins;
//
//     public int activeBackground;
//
//     public bool[] backgroundsUnlocked;
//
//     public int activeTrail;
//
//     public bool[] trailsUnlocked;
//
//     public float soundVolume;
// }

[Serializable]
public class GameData
{
    #region Variables
    
    private bool isGameStartedFirstTime;
    
    private int highScore, coins;
    
    public int activeBackground;
    
    public bool[] backgroundsUnlocked;
    
    public int activeTrail;
    
    public bool[] trailsUnlocked;
    
    public float soundVolume;

    # endregion Variables

    #region Getters and Setters

    public void SetIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool GetIsGameStartedFirstTime()
    {
        return this.isGameStartedFirstTime;
    }

    public void SetHighScore(int highScore)
    {
        this.highScore = highScore;
    }

    public int GetHighScore()
    {
        return this.highScore;
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;
    }

    public int GetCoins()
    {
        return this.coins;
    }

    public void SetActiveBackground(int background)
    {
        this.activeBackground = background;
    }

    public int GetActiveBackground()
    {
        return this.activeBackground;
    }

    public void SetActiveTrail(int trail)
    {
        this.activeTrail = trail;
    }

    public int GetActiveTrail()
    {
        return this.activeTrail;
    }

    public void SetUnlockedBackgrounds(bool[] backgrounds)
    {
        this.backgroundsUnlocked = backgrounds;
    }

    public bool[] GetUnlockedBackgrounds()
    {
        return this.backgroundsUnlocked;
    }

    public void SetUnlockedTrails(bool[] trails)
    {
        this.trailsUnlocked = trails;
    }

    public bool[] GetUnlockedTrails()
    {
        return this.trailsUnlocked;
    }

    public void SetSoundVolume(float volume)
    {
        this.soundVolume = volume;
    }

    public float GetSoundVolume()
    {
        return this.soundVolume;
    }

    #endregion Getters and Setters
}