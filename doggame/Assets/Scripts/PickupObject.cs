using UnityEngine;
using System.Collections;

public class PickupObject : Object {
	public override Object.type getType(){
		return Object.type.TYPE_PICKUP;
	}
}
