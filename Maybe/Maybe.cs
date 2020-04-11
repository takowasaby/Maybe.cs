using System;
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

            return source.SelectMany(maybe => maybe.AsEnumerable());
        }
    }

    public readonly struct Maybe<T>
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

        public Maybe<U> Map<U>(Func<T, U> mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            var newValue = mapper(this.value);

            return Create<U>(newValue);
        }

        public Maybe<U> FlatMap<U>(Func<T, Maybe<U>> mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            return mapper(this.value);
        }

        public void Process(Action<T> processer)
        {
            processer(this.value);
        }

        public bool IsPresent()
        {
            return this.hasValue;
        }

        public IEnumerable<T> AsEnumerable()
        {
            if (this.hasValue)
            {
                return new List<T>{ this.value };
            }
            else
            {
                return new List<T>();
            }
        }
    }
}