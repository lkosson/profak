﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	readonly struct Ref<T> where T : Rekord<T>
	{
		public readonly int Id { get; }

		public Type Typ => typeof(T);

		public Ref(int id) => Id = id;
		public override string ToString() => Typ.Name + "#" + Id;
		public override bool Equals(object otherObj) => otherObj is Ref<T> other && other.Id == Id && other.Typ == Typ;
		public override int GetHashCode() => Id;
		public static bool operator ==(Ref<T> ref1, Ref<T> ref2) => ref1.Id == ref2.Id;
		public static bool operator !=(Ref<T> ref1, Ref<T> ref2) => ref1.Id != ref2.Id;
		public static implicit operator int(Ref<T> r) => r.Id;
		public static implicit operator int?(Ref<T> r) => r.Id == 0 ? null : r.Id;
		public static implicit operator Ref<T>(int id) => new Ref<T>(id);
		public static implicit operator Ref<T>(int? id) => id.GetValueOrDefault();
		public static implicit operator Ref<T>(Rekord<T> rekord) => rekord?.Id;
		public bool IsNull => Id == 0;
		public bool IsNotNull => !IsNull;
	}
}
