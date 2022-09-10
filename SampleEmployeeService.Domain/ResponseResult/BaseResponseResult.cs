using System.Threading.Tasks;
using FluentValidation.Results;

namespace SampleEmployeeService.Domain.ResponseResult
{
    public class BaseResponseResult : IBaseResult
    {
        public ValidationResult ValidationResult { get; set; }
        public bool Failed => !Succeeded;
        public string Message { get; set; }
        public bool Succeeded { get; set; }

        public static IBaseResult Fail()
        {
            return new BaseResponseResult
            {
                Succeeded = false
            };
        }

        public static IBaseResult Fail(string message)
        {
            return new BaseResponseResult
            {
                Succeeded = false,
                Message = message
            };
        }

        public static Task<IBaseResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IBaseResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static IBaseResult Success()
        {
            return new BaseResponseResult
            {
                Succeeded = true
            };
        }

        public static IBaseResult Success(string message)
        {
            return new BaseResponseResult
            {
                Succeeded = true,
                Message = message
            };
        }

        public static Task<IBaseResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IBaseResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
    }

    public class BaseResponseResult<T> : BaseResponseResult, IBaseResult<T>, IBaseResult
    {
        public T Data { get; set; }

        public new static BaseResponseResult<T> Fail()
        {
            var result = new BaseResponseResult<T>
            {
                Succeeded = false
            };

            return result;
        }

        public new static BaseResponseResult<T> Fail(string message)
        {
            var result = new BaseResponseResult<T>
            {
                Succeeded = false,
                Message = message
            };

            return result;
        }

        public new static Task<BaseResponseResult<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public new static Task<BaseResponseResult<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public new static BaseResponseResult<T> Success()
        {
            var result = new BaseResponseResult<T> { Succeeded = true };
            return result;
        }

        public new static BaseResponseResult<T> Success(string message)
        {
            var result = new BaseResponseResult<T> { Succeeded = true, Message = message };
            return result;
        }

        public static BaseResponseResult<T> Success(T data)
        {
            var result = new BaseResponseResult<T> { Succeeded = true, Data = data };
            return result;
        }

        public static BaseResponseResult<T> Success(T data, string message)
        {
            var result = new BaseResponseResult<T>
            {
                Succeeded = true,
                Data = data,
                Message = message
            };

            return result;
        }

        public new static Task<BaseResponseResult<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public new static Task<BaseResponseResult<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<BaseResponseResult<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<BaseResponseResult<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }
    }
}