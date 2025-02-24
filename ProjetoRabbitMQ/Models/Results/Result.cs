namespace ProjetoRabbitMQ.Models.Results
{
    public class Result<TValue>
    {
        public TValue Value { get; private set; }
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; private set; }

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
        public static Result<TValue> Failure(string errorMessage) => new(errorMessage);

        public TValue GetValueOrThrow()
        {
            if (!IsSuccess)
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