using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
	public string _levelName;
	private Button _levelButton;

	private void Start()
	{
        _levelButton = GetComponent<Button>();
        _levelButton.onClick.AddListener(OnLevelSelectButtonClick);
	}

    private void OnLevelSelectButtonClick()
    {
        SceneManager.LoadScene(_levelName);

    }
}
