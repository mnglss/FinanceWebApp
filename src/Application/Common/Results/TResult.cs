namespace Application.Common.Results
{
    public class Result<T> : Result
    {
        private readonly T _value;

        protected internal Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public T Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access Value on a failed result");

    }
}
