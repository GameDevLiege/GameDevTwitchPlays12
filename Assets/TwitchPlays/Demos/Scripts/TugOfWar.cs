using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TugOfWar : MonoBehaviour {

	public Slider slider;
	public int PointLimit;
	int currentPoints;

	public Text PlusVotes;
	public Text MinusVotes;

	int pVotes;
	int mVotes;

	public Text PlusWins;
	public Text MinusWins;

	int pWins;
	int mWins;

	public void AddPoint()
	{
		currentPoints++;
		pVotes++;
	}
	
	public void SubPoint()
	{
		currentPoints--;
		mVotes++;
	}

	void Update()
	{
		if(Mathf.Abs(currentPoints) >= PointLimit)
		{
			if(currentPoints < 0)
				mWins++;
			else
				pWins++;
			currentPoints = 0;
		}
		slider.value = 0.5f + ((float)currentPoints/(float)PointLimit)/2.0f;

		PlusVotes.text = "Plust Votes\n" + pVotes;
		MinusVotes.text = "Minus Votes\n" + mVotes;

		PlusWins.text = "Plust Wins\n" + pWins;
		MinusWins.text = "Minus Wins\n" + mWins;
	}

}
