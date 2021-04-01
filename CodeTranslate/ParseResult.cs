using System.Collections.Generic;
using SchemeEditor.Schemes;

namespace SchemeEditor.CodeTranslate
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