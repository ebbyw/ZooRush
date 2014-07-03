﻿using UnityEngine;
using System.Collections;

public class HelpMenuSetPainKiller : HelpMenuSet
{
		public PainKiller painkiller;

		public override void activate ()
		{
				transform.localPosition = Vector3.zero;
		}
	
		public override void dismiss ()
		{
				transform.position = originalPosition;
		}

		public override void reset ()
		{
//				throw new System.NotImplementedException ();
		}
}