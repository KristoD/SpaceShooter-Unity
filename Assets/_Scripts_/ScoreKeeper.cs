using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	private Text myText;
	
	void Start () {
		myText = GetComponent<Text>();
		ResetScore();
	}
	
	public void GetScore (int points) {
		score += points;
		myText.text = score.ToString();
	}
	
	public static void ResetScore () {
		score = 0;
	}
	
	
	
	
}
