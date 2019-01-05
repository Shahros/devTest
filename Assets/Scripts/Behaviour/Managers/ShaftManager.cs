using System.Collections.Generic;
using UnityEngine;

public class ShaftManager : MonoBehaviour
{
    [SerializeField] private GameSettings settings;
    [SerializeField] private GameObject shaftPrefab;
	private FinanceManager financeManager;
	private Actor elevator;

    public int MaxShafts;

    public List<Shaft> Shafts;

	void Awake()
	{
		elevator = transform.Find ("Elevator").GetComponent<Actor>();
		financeManager = GetComponent<FinanceManager> ();
	}

    public void BuildNextShaft()
    {
        var position = Shafts[Shafts.Count - 1].NextShaftTransform.position;
        var newObject = Instantiate(shaftPrefab, position, Quaternion.identity);
		newObject.transform.parent = gameObject.transform;
        var shaft = newObject.GetComponent<Shaft>();
        Shafts.Add(shaft);
        shaft.ShaftManager = this;
        shaft.Initialize(elevator, financeManager, Shafts.Count);

        financeManager.UpdateMoney(-financeManager.NextShaftPrice);
        financeManager.NextShaftPrice *= settings.ShaftIncrement;
    }
}