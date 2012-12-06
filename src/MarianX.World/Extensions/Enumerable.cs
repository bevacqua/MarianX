using System;
using System.Collections.Generic;
using System.Linq;

namespace MarianX.World.Extensions
{
	public static class EnumerableExtensions
	{
		public static T Random<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.OrderBy(i => Guid.NewGuid()).First();
		}
	}
}