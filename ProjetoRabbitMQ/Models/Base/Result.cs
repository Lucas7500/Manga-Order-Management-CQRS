namespace ProjetoRabbitMQ.Models.Base
{
    public class Result<TValue>
    {
        public TValue Value { get; private set; }
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; private set; }
        public bool IsFailure => !IsSuccess;

        private Result(TValue value)
        {
            ArgumentNullException.ThrowIfNull(value);

            IsSuccess = true;
            Value = value;
            ErrorMessage = null;
        }

        private Result(string errorMessage)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);

            IsSuccess = false;
            Value = default!;
            ErrorMessage = errorMessage;
        }

        public static Result<TValue> Success(TValue value) => new(value);
        public static Result<TValue> Failure(string errorMessage, params object?[] args)
            => args.Length > 0
                ? new(string.Format(errorMessage, args))
                : new(errorMessage);

        public TValue GetValueOrThrow()
        {
            if (IsFailure)
                throw new InvalidOperationException(ErrorMessage);

            return Value!;
        }

        public TValue? GetValueOrDefault(TValue? defaultValue = default)
        {
            return IsSuccess ? Value : defaultValue;
        }

        public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<string, TResult> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            return IsSuccess
                ? onSuccess(Value!)
                : onFailure(ErrorMessage!);
        }
    }
}