using UnityEngine;

public class SoldierFactory : MonoBehaviour
{
	public enum SoldierType { Melee, Ranged }

	[Header("References")]
	[SerializeField] GameObject meleeSoldier;
	[SerializeField] GameObject rangedSoldier;
	
	public ISoldier CreateSoldier(SoldierType soldierType, Transform parent) 
	{
		GameObject soldier;
		switch(soldierType) 
		{
			case SoldierType.Melee:
				soldier = Instantiate(meleeSoldier);
				break;
			case SoldierType.Ranged:
				soldier = Instantiate(rangedSoldier);
				break;
			default:
				return null;
		}
		soldier.transform.SetParent(parent, false);
		
		return soldier.GetComponent<ISoldier>();
	}
}
