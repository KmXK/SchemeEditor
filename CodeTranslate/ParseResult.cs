using System.Collections.Generic;
using AutoScheme.Schemes;

namespace AutoScheme.CodeTranslate
{
    public struct ParseResult
    {
        public ParseResult(bool isSuccess, string errorMessage, IEnumerable<GraphicScheme> schemes)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Schemes = schemes;
        }

        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public IEnumerable<GraphicScheme> Schemes { get; }
    }
}