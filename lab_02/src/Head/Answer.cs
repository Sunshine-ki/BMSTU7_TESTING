using System;
using System.Collections.Generic;

using bl;
using db;

namespace Head
{
	public struct Answer
	{
		public Answer(int return_Value, string msg)
		{
			returnValue = return_Value;
			Msg = msg;
		}
		public Answer(int return_Value)
		{
			returnValue = return_Value;
			Msg = null;
		}

		public int returnValue { get; }
		public string Msg { get; }

		public override string ToString() => Msg;
	}
}
