using UnityEngine;

public static class LevelManager
{
    private static int levelIndex;

    private static LevelList levelList;
    public static Level currentLevel;

    public static void SetLevelManager(LevelList list)
    {
        levelList = list;
        levelIndex = PlayerPrefs.GetInt("levelIndex", 0);

        LoadLevel();
    }

    public static void LoadLevel()
    {
        currentLevel = GameObject.Instantiate(levelList.Levels[levelIndex % levelList.Levels.Count]);
    }

    public static void NextLevel()
    {
        currentLevel.DestroyLevel();

        levelIndex++;
        PlayerPrefs.SetInt("levelIndex", levelIndex);

        LoadLevel();
    }

    public static void RestartLevel()
    {
        currentLevel.DestroyLevel();

        LoadLevel();
    }
}