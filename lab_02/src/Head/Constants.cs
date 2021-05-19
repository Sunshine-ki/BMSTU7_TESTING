using System;
using System.Collections.Generic;

using bl;
using db;

namespace Head
{
	public class Constants
	{
		public const int OK = 0;
		public enum Errors { UserExists = 1, LoginUserExists, EmailUserExists, ShortLengthPassword, OnlyNumericPassword, AddNewUser };
	}
}
