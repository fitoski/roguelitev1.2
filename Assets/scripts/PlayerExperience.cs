using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int level = 1;

    public int Level => level;

    [SerializeField] private int experience = 0;
    [SerializeField] private int experienceToNextLevel = 10;
    private int experienceToNextLevelBase = 0;

    [SerializeField] private GameObject levelUpButtons;

    private void Awake()
    {
        experienceToNextLevelBase = experienceToNextLevel;
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        Debug.Log("Experience: " + experience + "/" + experienceToNextLevel); 
        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        experience = 0;
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevelBase * Mathf.Pow(1.5f, level - 1));
        Debug.Log("Level up! New level: " + level + ", experience to next level: " + experienceToNextLevel);

        Time.timeScale = 0;
        levelUpButtons.SetActive(true);
        levelUpButtons.GetComponent<LevelUpUpgradesController>().SetUpUpgrades();
    }

    public void LevelUpToThis(int newLevel)
    {
        level = newLevel;
        experience = 0;
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevelBase * Mathf.Pow(1.5f, level - 1));
        Debug.Log("Level up! New level: " + level + ", experience to next level: " + experienceToNextLevel);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            LevelUp();
        }
    }
}
