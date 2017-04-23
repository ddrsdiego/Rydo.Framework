using System;

namespace ConsoleApplication
{
    public class Helper
    {
        public partial struct Try<TFailure, TSuccess>
        {
            internal TFailure Failure { get; }
            internal TSuccess Success { get; }

            public bool IsFailure { get; }
            public bool IsSucess => !IsFailure;

            internal Try(TFailure failure)
            {
                IsFailure = true;
                Failure = failure;
                Success = default(TSuccess);
            }

            internal Try(TSuccess success)
            {
                IsFailure = false;
                Failure = default(TFailure);
                Success = success;
            }

            public static implicit operator Try<TFailure, TSuccess>(TFailure failure) => new Try<TFailure, TSuccess>(failure);

            public static implicit operator Try<TFailure, TSuccess>(TSuccess success) => new Try<TFailure, TSuccess>(success);

            public TResult Match<TResult>(Func<TFailure, TResult> failure, Func<TSuccess, TResult> success) => IsFailure ? failure(Failure) : success(Success);

            public Unit Match(Action<TFailure> failure, Action<TSuccess> success) => Match(Helpers.ToFunc(failure), Helpers.ToFunc(success));
        }

        public partial struct Unit
        {

        }

        public static partial class Helpers
        {
            private static readonly Unit unit = new Unit();
            public static Unit Unit() => unit;

            public static Func<T, Unit> ToFunc<T>(Action<T> action) => o =>
            {
                action(o);
                return Unit();
            };
        }

        public struct Untrusted<T>
        {
            private readonly T _value;

            private Untrusted(T value)
            {
                _value = value;
            }

            public static implicit operator Untrusted<T>(T value) => new Untrusted<T>(value);

            public Try<TFailure, TSuccess> Validate<TFailure, TSuccess>(Func<T, bool> validation,
                                                                        Func<T, TFailure> onFailure,
                                                                        Func<T, TSuccess> onSuccess) => validation(_value) ? onSuccess(_value) : (Try<TFailure, TSuccess>)onFailure(_value);
        }
    }
}
