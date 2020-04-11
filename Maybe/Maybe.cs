using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Opt
{
    public static class Maybe
    {
        public static Maybe<T> Create<T>(T value)
        {
            return Maybe<T>.Create(value);
        }

        public static Maybe<T> Nothing<T>()
        {
            return Maybe<T>.Nothing<T>();
        }

        public static Maybe<T> FromRef<T>(T value)
            where T : class
        {
            if (value is null)
            {
                return Nothing<T>();
            }
            else
            {
                return Create(value);
            }
        }

        public static IEnumerable<T> Flatten<T>(IEnumerable<Maybe<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SelectMany(maybe => maybe as IEnumerable<T>);
        }
    }

    public readonly struct Maybe<T> : IEnumerable<T>
    {
        private readonly T value;
        private readonly bool hasValue;

        private Maybe(T value, bool hasValue)
        {
            this.value = value;
            this.hasValue = hasValue;
        }

        public static implicit operator Maybe<T>(T value) => Create(value);
        public static Maybe<U> Create<U>(U value) => new Maybe<U>(value, true);
        public static Maybe<U> Nothing<U>() => new Maybe<U>(default(U), false);

        public bool IsPresent()
        {
            return this.hasValue;
        }



        public IEnumerator<T> GetEnumerator()
        {
            if (this.hasValue)
            {
                return new List<T>{ this.value }.GetEnumerator();
            }
            else
            {
                return new List<T>().GetEnumerator();
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}