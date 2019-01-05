using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MineManager : MonoBehaviour {

	List<GameObject> allMines=new List<GameObject>();
	List<GameObject> mineButtons = new List<GameObject> ();
	GameObject lastMine;
	[SerializeField] private GameObject mine;
	[SerializeField] private int numberOfMinesAllowed=10;
	[SerializeField] private GameObject mineBtn,btnParent;
	void Start()
	{
		createMine ();
	}
	public void createMine()
	{
		if (allMines.Count == numberOfMinesAllowed) {
			enableMineSelection (true);
			return;
		}
		var newMine =Instantiate (mine);
		allMines.Add (newMine);
		mineActivator (newMine);
	}

	public void enableMineSelection(bool status)
	{
		lastMine.SetActive(status);
	}

	public void selectMine(int mineNumber)
	{
		mineActivator (allMines [mineNumber]);
		allMines [mineNumber].SetActive (true);
		GameObject.Find ("Panel").SetActive (false);
	}
	void mineActivator(GameObject newMine)
	{
		if (lastMine != null)
		{
			lastMine.SetActive (false);
		}
		lastMine = newMine;
	}
	public void generateMineButtons()
	{
		foreach (var item in mineButtons) {
			Destroy (item);
		}
		mineButtons.Clear ();
		foreach(var m in allMines) 
		{
			var btn = Instantiate (mineBtn);
			int s = allMines.IndexOf(m);
			btn.GetComponentInChildren<Text> ().text = "Mine " + (s+1).ToString ();
			btn.GetComponent<Button> ().onClick.AddListener (()=>selectMine(s));
			btn.transform.parent = btnParent.transform;
			mineButtons.Add (btn);
		}
	}
}
