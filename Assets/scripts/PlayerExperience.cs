using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 10;

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
        experience -= experienceToNextLevel;
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.5f);
        Debug.Log("Level up! New level: " + level + ", experience to next level: " + experienceToNextLevel); 
    }

}
