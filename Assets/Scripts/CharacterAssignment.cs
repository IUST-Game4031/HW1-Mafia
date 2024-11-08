using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CharacterAssignment : MonoBehaviour
{
    private Image characterImage;           // Character Image UI component
    private TMP_Text characterName;             // Character Name UI component
    private Button nextCharacterButton;     // Next Character Button

    // Sprites for each character role (assign these in the Inspector)
    public Sprite godFatherSprite;
    public Sprite simpleMafiaSprite;
    public Sprite professionalMafiaSprite;
    public Sprite detectiveSprite;
    public Sprite doctorSprite;
    public Sprite sniperSprite;
    public Sprite simpleCitizenOneSprite;
    public Sprite simpleCitizenTwoSprite;
    public Sprite simpleCitizenThreeSprite;
    public Sprite mayorSprite;
    public Sprite killerSprite;
    public Sprite feramasonSprite;
    public Sprite priestSprite;

    public Sprite defaultImage;             // Default image shown before revealing character
    public string defaultText;              // Default text shown before revealing character

    private List<Character> characters = new List<Character>(); // List to store roles and images
    private int currentPlayerIndex = 0;                         // Index to track current player
    private int citizenIndex = 1;                               // Index of current citizen -> to show their names in order

    public bool wait = false;

    public SceneLoader sceneLoader;

    void Start()
    {
        // Find UI elements by tags
        characterImage = GameObject.FindGameObjectWithTag("Character").GetComponent<Image>();
        characterName = GameObject.FindGameObjectWithTag("Title_Text").GetComponent<TMP_Text>();
        nextCharacterButton = GameObject.FindGameObjectWithTag("Alert_Text").GetComponent<Button>();
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();

        // Initialize characters with roles and images based on the player count
        if (PlayerPrefs.GetInt("PlayerCount") == 8)
        {
            characters = new List<Character>
            {
                new Character("God Father", godFatherSprite),
                new Character("Simple Mafia", simpleMafiaSprite),
                new Character("Masked Mafia", professionalMafiaSprite),
                new Character("Detective", detectiveSprite),
                new Character("Doctor", doctorSprite),
                new Character("Sniper", sniperSprite),
                new Character("Citizen", simpleCitizenOneSprite),
                new Character("Citizen", simpleCitizenTwoSprite)
            };
        }
        else if (PlayerPrefs.GetInt("PlayerCount") == 10)
        {
            characters = new List<Character>
            {
                new Character("God Father", godFatherSprite),
                new Character("Simple Mafia", simpleMafiaSprite),
                new Character("Masked Mafia", professionalMafiaSprite),
                new Character("Detective", detectiveSprite),
                new Character("Doctor", doctorSprite),
                new Character("Sniper", sniperSprite),
                new Character("Mayor", mayorSprite),
                new Character("Citizen", simpleCitizenOneSprite),
                new Character("Citizen", simpleCitizenTwoSprite),
                new Character("Citizen", simpleCitizenThreeSprite)
            };
        }
        else if (PlayerPrefs.GetInt("PlayerCount") == 12)
        {
            characters = new List<Character>
            {
                new Character("God Father", godFatherSprite),
                new Character("Simple Mafia", simpleMafiaSprite),
                new Character("Masked Mafia", professionalMafiaSprite),
                new Character("Detective", detectiveSprite),
                new Character("Mayor", mayorSprite),
                new Character("Doctor", doctorSprite),
                new Character("Sniper", sniperSprite),
                new Character("Citizen", simpleCitizenOneSprite),
                new Character("Citizen", simpleCitizenTwoSprite),
                new Character("Killer", killerSprite),
                new Character("Feramason", feramasonSprite),
                new Character("Priest", priestSprite)
            };
        }

        // Shuffle characters for randomness
        ShuffleCharacters();

        // Set up initial UI for the first player
        ShowDefaultImage();

        // Add listener to the button
        nextCharacterButton.onClick.AddListener(ShowNextCharacter);
    }

    void ShowDefaultImage()
    {
        characterImage.sprite = defaultImage;
        characterName.text = defaultText;
        nextCharacterButton.GetComponentInChildren<TMP_Text>().text = "Click To See Your Role!";
    }

    void ShowNextCharacter()
    {
        // Check if all characters are assigned
        if (currentPlayerIndex < characters.Count)
        {
            if (wait)
            {
                ShowDefaultImage();
                wait = false;
            } else
            {
                Character currentCharacter = characters[currentPlayerIndex];
                characterImage.sprite = currentCharacter.image;
                // Check for the index of citizen to show citizens in correct order
                if (currentCharacter.name == "Citizen")
                {
                    characterName.text = currentCharacter.name;
                    citizenIndex++;
                }
                else
                {
                    characterName.text = currentCharacter.name;
                }

                nextCharacterButton.GetComponentInChildren<TMP_Text>().text = "Click To See Next Role!";

                // Move to the next player for the following click
                currentPlayerIndex++;

                // Change the button text for the last player
                if (currentPlayerIndex == characters.Count)
                {
                    nextCharacterButton.GetComponentInChildren<TMP_Text>().text = "Click to return to Main Menu!";
                }

                wait = true;
            }
        }
        else
        {
            Debug.Log("All players have been assigned characters.");
            nextCharacterButton.interactable = false;  // Disable button after all characters are shown
            sceneLoader.BackToMenu(); // Back to main menu if all characters are shown
        }
    }

    void ShuffleCharacters()
    {
        for (int i = characters.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Character temp = characters[i];
            characters[i] = characters[randomIndex];
            characters[randomIndex] = temp;
        }
    }
}

// Helper class to store character information
[System.Serializable]
public class Character
{
    public string name;
    public Sprite image;

    public Character(string name, Sprite image)
    {
        this.name = name;
        this.image = image;
    }
}