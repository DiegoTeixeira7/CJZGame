using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  public int totalCoins;
  public Text coinText;

  public Image healthBar;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void addCoins()
  {
    totalCoins++;
    coinText.text = totalCoins.ToString();
  }

  public void loserLife(float value)
  {
    healthBar.fillAmount = value / 10;
  }
}
