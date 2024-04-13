using System.Net;

namespace Gallery.Shared
{
    public class ResultOperation
    {
        internal bool _resultFlag = true;
        internal HttpStatusCode _resultCode;
        internal List<string> _errorMessages;
        internal List<string> _systemErrorMessages;
        internal List<string> _messages;

        public List<string> ErrorMessages
        {
            set
            {
                _errorMessages = value.ToList();
            }
            get
            {
                if (_errorMessages == null)
                {
                    _errorMessages = new List<string>();
                }
                return _errorMessages;
            }
        }

        public List<string> Messages
        {
            set
            {
                _messages = value;
            }
            get
            {
                if (_messages == null)
                {
                    _messages = new List<string>();
                }
                return _messages;
            }
        }

        public bool ResultFlag
        {
            set
            {
                if (ResultCode == HttpStatusCode.OK && !_errorMessages.Any())
                {
                    _resultFlag = true;
                }
                else
                {
                    _resultFlag = false;
                }
            }
            get
            {
                if (_errorMessages != null && _errorMessages.Any())
                {
                    ResultFlag = false;
                }
                return _resultFlag;
            }
        }

        public HttpStatusCode ResultCode
        {
            set
            {
                _resultCode = value;
                if (value == HttpStatusCode.OK && ErrorMessages == null)
                {
                    ErrorMessages = new List<string>();
                }
            }
            get { return _resultCode; }
        }

        public object? Result { set; get; }

    }

    public class ResultOperation<T> : ResultOperation
    {
        #region properties
        public new T? Result { set; get; }

        #endregion properties

        #region methods
        public ResultOperation()
        {
            ResultCode = default;
        }
        public ResultOperation(ResultOperation<T> crud)
        {
            Set(crud);
        }

        public ResultOperation<T> Set(ResultOperation<T> crud)
        {
            ResultFlag = crud.ResultFlag;
            ResultCode = crud.ResultCode;
            Result = crud.Result;
            ErrorMessages = crud.ErrorMessages;
            Messages = crud.Messages;
            return this;
        }

        public ResultOperation<T> SetResultObject(HttpStatusCode httpStatus, T result)
        {
            ResultCode = httpStatus;
            Result = result;
            return this;
        }
        public ResultOperation<T> SetData(bool resultFlag, HttpStatusCode httpStatus, IEnumerable<string> messages = null)
        {
            ResultFlag = resultFlag;
            ResultCode = httpStatus;
            if (messages != null)
            {
                ErrorMessages.AddRange(messages);
            }
            return this;
        }
        public ResultOperation<T> SetData(T resultObject)
        {
            Result = resultObject;
            ResultCode = HttpStatusCode.OK;
            ResultFlag = true;
            return this;
        }

        public ResultOperation<T> SetData(T resultObject, bool resultFlag)
        {
            Result = resultObject;
            ResultCode = HttpStatusCode.OK;
            ResultFlag = resultFlag;
            return this;
        }

        public ResultOperation<T> SetData(T resultObject, HttpStatusCode httpStatus, bool resultFlag)
        {
            Result = resultObject;
            ResultCode = httpStatus;
            ResultFlag = resultFlag;
            return this;
        }

        public ResultOperation<T> SetData(T resultObject, HttpStatusCode httpStatus, bool resultFlag, IEnumerable<string> errors)
        {
            ErrorMessages = errors.ToList();
            Result = resultObject;
            ResultCode = httpStatus;
            ResultFlag = resultFlag;
            return this;
        }

        public ResultOperation<T> SetSystemError(Exception ex)
        {
            ResultCode = HttpStatusCode.InternalServerError;
            ResultFlag = false;
            do
            {
                _errorMessages.Add(ex.Message);
                ex = ex.InnerException;
            } while (ex != null);
            return this;
        }

        public ResultOperation<T> SetError(string message, HttpStatusCode httpStatus)
        {
            ResultCode = httpStatus;
            ResultFlag = false;
            ErrorMessages = ErrorMessages == null ? new List<string>() : ErrorMessages;
            ErrorMessages.Add(message);
            return this;
        }
        public ResultOperation<T> SetMessage(string message)
        {
            this.Messages.Add(message);
            return this;
        }

        public ResultOperation<T> SetError(string errorMessage)
        {
            ResultCode = HttpStatusCode.BadRequest;
            ResultFlag = false;
            ErrorMessages.Add(errorMessage);
            return this;
        }

        public ResultOperation<T> SetError(IEnumerable<string> messages)
        {
            ResultCode = HttpStatusCode.BadRequest;
            ResultFlag = false;
            ErrorMessages.AddRange(messages);
            return this;
        }

        public ResultOperation<T> SetError(IEnumerable<string> messages, HttpStatusCode httpStatus)
        {
            ResultCode = httpStatus != 0 ? httpStatus : HttpStatusCode.BadRequest;
            ResultFlag = false;
            ErrorMessages.AddRange(messages);
            return this;
        }

        public ResultOperation<T> SetSuccessfulMessage(IEnumerable<string> messages, HttpStatusCode httpStatus)
        {
            ResultCode = httpStatus;
            ResultFlag = true;
            Messages.AddRange(messages);
            return this;
        }

        public void SetResultCode(HttpStatusCode statusCode) => ResultCode = statusCode;
        #endregion methods
    }

    public class ResultOperationList<T> : ResultOperation<T>
    {
        public int TotalCount { get; private set; }
        public IEnumerable<T> ResultsList { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }

        public new ResultOperation<T> SetData(IEnumerable<T> resultObject, List<string> errorMessages = null, int totalCount = 0, HttpStatusCode statusCode = 0)
        {
            if (errorMessages != null && errorMessages.Any())
                SetError(errorMessages, statusCode);
            else
            {
                ResultsList = resultObject;
                ResultCode = HttpStatusCode.OK;
                ResultFlag = true;
                TotalCount = totalCount == 0 ? ResultsList.Count() : totalCount;
            }
            return this;
        }
    }
}