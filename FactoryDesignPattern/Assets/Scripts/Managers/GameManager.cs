using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	[Header("References")]
	[SerializeField] UIManager uIManager;
	[SerializeField] GameObject enemy;
	[SerializeField] Transform soldierParent;
	[SerializeField] SoldierFactory soldierFactory;

	ISoldier currentSoldier;
	GameObject currentEnemy;
	
	public ISoldier GetCurrentSoldier() { return currentSoldier; }
	
	void Start() => StartCoroutine(CreateNewEnemy());
	
	void Update() { if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); }
	
	public void CreateNewSoldier() 
	{
		currentSoldier?.Change();
		currentSoldier = soldierFactory.CreateSoldier(GetRandomSoldierType(), soldierParent);
	}

	SoldierFactory.SoldierType GetRandomSoldierType()
	{
		return (SoldierFactory.SoldierType)Random.Range(0, (int)Enum.GetValues(typeof(SoldierFactory.SoldierType)).Cast<SoldierFactory.SoldierType>().Max() + 1);
	}

	public IEnumerator Versus() 
	{
		yield return new WaitForSeconds(1.5f);
		Destroy(currentEnemy);
		currentSoldier.Dead();
		currentSoldier = null;
		StartCoroutine(CreateNewEnemy());
	}
	
	IEnumerator CreateNewEnemy() 
	{
		yield return new WaitForSeconds(0.5f);
		currentEnemy = Instantiate(enemy);
		currentEnemy.transform.SetParent(soldierParent, false);
		uIManager.SetCreateBtn(true);
	}
}
