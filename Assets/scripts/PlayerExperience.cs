using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int level = 1;

    public int Level => level;

    [SerializeField] private int experience = 0;
    [SerializeField] private int experienceToNextLevel = 10;

    [SerializeField] private GameObject levelUpButtons;

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
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.5f);
        Debug.Log("Level up! New level: " + level + ", experience to next level: " + experienceToNextLevel);

        Time.timeScale = 0;
        levelUpButtons.SetActive(true);
        levelUpButtons.GetComponent<LevelUpUpgradesController>().SetUpUpgrades();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            LevelUp();
        }
    }
}
