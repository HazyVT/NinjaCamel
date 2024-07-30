using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public GameObject enemyText;

  void Start() {
    enemyText.GetComponent<TextMeshProUGUI>().text = Globals.enemeisDefeated.ToString();
  }
  
}
