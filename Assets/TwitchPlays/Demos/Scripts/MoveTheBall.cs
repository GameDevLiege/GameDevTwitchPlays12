using UnityEngine;
using System.Collections;

public class MoveTheBall : MonoBehaviour {

	public int Width;
	public int Height;

	public Transform Ball;
	public int bX;
	public int bY;


	public void MoveLeft(string s)
	{
		int d = 0;
		int.TryParse(s, out d);
		bX -= d;
	}

	public void MoveRight(string s)
	{
		int d = 0;
		int.TryParse(s, out d);
		bX += d;
	}

	public void MoveUp(string s)
	{
		int d = 0;
		int.TryParse(s, out d);
		bY += d;
	}

	public void MoveDown(string s)
	{
		int d = 0;
		int.TryParse(s, out d);
		bY -= d;
	}
	

	void LateUpdate () 
	{
		bX = Mathf.Max(Mathf.Min(Width,bX), 0);
		bY = Mathf.Max(Mathf.Min(Height,bY), 0);
		Ball.transform.position = new Vector3(bX-(Width/2), 1, bY-(Height/2));
	}
}
