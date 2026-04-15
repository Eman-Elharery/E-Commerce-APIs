namespace CompanySystem.BLL
{
        public class GeneralResult<T>
        {
            public bool Success { get; set; }

            public string? Message { get; set; }

            public IEnumerable<string>? Errors { get; set; }

            public T? Data { get; set; }


            public static GeneralResult<T> SuccessResult(T data)
            {
                return new GeneralResult<T>
                {
                    Success = true,
                    Data = data
                };
            }

            /*-------------------------------------*/

            public static GeneralResult<T> FailResult(string message)
            {
                return new GeneralResult<T>
                {
                    Success = false,
                    Message = message
                };
            }

            /*-------------------------------------*/

            public static GeneralResult<T> FailResult(IEnumerable<string> errors)
            {
                return new GeneralResult<T>
                {
                    Success = false,
                    Errors = errors
                };
            }


            public static GeneralResult<T> NotFound()
            {
                return new GeneralResult<T>
                {
                    Success = false,
                    Message = "Resource Not Found"
                };
            }
        }
    }

