using System.Diagnostics.CodeAnalysis;

namespace project_get_discount_back.Results
{

    [ExcludeFromCodeCoverage]
    public abstract class Result
    {
        internal static Result Fail(ResultError resultError)
        {
            return new Fail(resultError.Mensagem, resultError.Code);
        }

        internal static Result Success()
        {
            return new Success();
        }
    }

    public class Success : Result
    {
    }

    [ExcludeFromCodeCoverage]
    public class NoContent : Result
    {
    }

    [ExcludeFromCodeCoverage]
    public class Fail : Result
    {
        public string Message { get; }
        public string Code { get; }

        public Fail(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public Fail(ResultError resultError) : this(resultError.Mensagem, resultError.Code) { }

        public Fail<G> AsFail<G>()
        {
            return new Fail<G>(Message, Code);
        }
    }

    public abstract class Result<T>
    {
        internal static Result<T> Fail(ResultError resultError)
        {
            return new Fail<T>(resultError.Mensagem, resultError.Code);
        }

        internal static Result<T> Success(T value)
        {
            return new Success<T>(value);
        }
    }

    public class Success<T> : Result<T>
    {
        public T Value { get; }

        public Success(T value) => Value = value;
    }

    public class Fail<T> : Result<T>
    {
        public string Message { get; }
        public string Code { get; }

        public Fail(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public Fail(ResultError resultError) : this(resultError.Mensagem, resultError.Code) { }

        public Fail<G> AsFail<G>()
        {
            return new Fail<G>(Message, Code);
        }

        public Fail AsFail()
        {
            return new Fail(Message, Code);
        }
    }
}