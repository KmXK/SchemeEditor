using System;
using System.Collections.Generic;
using SchemeEditor.Schemes;

namespace SchemeEditor.CodeTranslate
{
    public class DelphiCodeParser
    {
        private string[] _code;
        private SchemeSettings _settings;

        private static string[] _reservedWords =
        {
            "begin", "end", "procedure", "function",
            "var", "uses"
        };
        
        public DelphiCodeParser(string[] code, SchemeSettings settings)
        {
            SetCode(code);
            _settings = settings;
        }

        public void SetCode(string[] code)
        {
            _code = code;
        }

        public ParseResult ParseCodeToScheme()
        {
            List<Scheme> schemes = new List<Scheme>();
            
            FormatCode();
            for (int i = 0; i < _code.Length; i++)
            {
                if (_code[i].StartsWith("function") ||
                    _code[i].StartsWith("procedure") || 
                    _code[i].StartsWith("begin"))
                {
                    int areaStart;

                    if (!_code[i].StartsWith("begin"))
                    {
                        if (!CheckAreaName(i, out areaStart, out string message))
                        {
                            return new ParseResult(false, message, null);
                        }

                        i++;
                    }
                    
                    areaStart = i;

                    
                    if (FindEndOfArea(areaStart, out int areaEnd))
                    {
                        var scheme = new Scheme(_settings);
                        WriteAreaToScheme(scheme, i + 1, areaEnd - 1);
                        schemes.Add(scheme);

                        i = areaEnd;

                    }
                    else
                    {
                        return new ParseResult(false,
                            $"Не найден конец области по индексу {areaStart + 1}",
                            null);
                    }
                }
            }

            return new ParseResult(true, "", schemes);
        }

        private void FormatCode()
        {
            for (int i = 0; i < _code.Length; i++)
            {
                _code[i] = _code[i].Trim();
                if (_code[i].Contains("//"))
                {
                    _code[i].Remove(_code[i].IndexOf("//", StringComparison.Ordinal));
                }
            }
        }

        private void WriteAreaToScheme(Scheme scheme, int start, int end)
        {
            start++;
            end--;
        }

        // areaStart - индекс begin области
        private bool FindEndOfArea(int areaStart, out int areaEnd)
        {
            areaEnd = areaStart;

            int line = areaStart;
            while (line <= _code.Length - 1)
            {
                if (_code[line].ToLower() == "end" ||
                    _code[line].ToLower() == "end;" ||
                    _code[line].ToLower() == "end.")
                {
                    areaEnd = line;
                    return true;
                }

                line++;
            }

            return false;
        }

        private bool CheckAreaName(int start, out int end, out string errorMessage)
        {
            errorMessage = "";
            end = start;
            
            var isEnded = false;
            var line = start;
            var bracketNesting = 0;
            while (_code.Length - 1 >= line)
            {
                for (int i = 0; i < _code[line].Length; i++)
                {
                    if (isEnded)
                    {
                        errorMessage = "После точки запятой ничего не должно быть.";
                        return false;
                    }
                    
                    if (_code[line][i] == '(')
                    {
                        bracketNesting++;
                    }
                    else if(_code[line][i] == ')')
                    {
                        bracketNesting--;
                    }
                    else if(_code[line][i] == ';')
                    {
                        isEnded = true;
                        end = line;
                    }
                }

                if (isEnded)
                {
                    if (bracketNesting != 0)
                    {
                        errorMessage = $"Лишняя(ие) скобки в оператора, начинающемся на строке {start}.";
                        return false;
                    }

                    if (end == _code.Length - 1 ||
                        !_code[end+1].StartsWith("begin"))
                    {
                        errorMessage = $"Ожидался begin на строке {end+1}.";
                    }
                    
                    end = line;
                    return true;
                }

                line++;
            }

            errorMessage = $"Не закрыта область кода в строке {start}.";
            return false;
        }
    }
}