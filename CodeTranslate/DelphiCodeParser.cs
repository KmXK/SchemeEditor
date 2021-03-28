using System;
using System.Collections.Generic;
using SchemeEditor.Schemes;
using SchemeEditor.Schemes.Blocks;

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

                        i = areaStart + 1;
                    }
                    
                    areaStart = i;

                    
                    if (FindEndOfArea(areaStart, out int areaEnd))
                    {
                        var scheme = new Scheme(_settings);
                        
                        // areaStart указывает на begin схемы
                        // areaEnd - на end
                        
                        // Нужно пройтись по всем операторам и добавить их в scheme.MainBlock
                    }
                    else
                    {
                        return new ParseResult(false,
                            $"Ожидался end для begin по индексу {areaStart}",
                            null);
                    }
                }
            }

            return new ParseResult(true, "", schemes);
        }

        // Удаление лишних пробелов по краям строк, однострочных комментариев
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

        // Найти end для begin
        private bool FindEndOfArea(int areaStart, out int areaEnd)
        {
            areaEnd = areaStart;
            int nesting = 0;

            int line = areaStart;
            while (line <= _code.Length - 1)
            {
                if (_code[line].ToLower() == "end" ||
                    _code[line].ToLower() == "end;" ||
                    _code[line].ToLower() == "end.")
                {
                    nesting--;
                    
                    if(nesting == 0)
                    {
                        areaEnd = line;
                        return true;
                    }
                }
                else if (_code[line].ToLower() == "begin")
                {
                    nesting++;
                }

                line++;
            }

            return false;
        }

        // Проверка имени подпрограммы
        private bool CheckAreaName(int start, out int end, out string errorMessage)
        {
            errorMessage = "";
            end = start;
            
            var isEnded = false;
            var line = start;
            var bracketNesting = 0;
            while (_code.Length - 1 >= line)
            {
                if(line != start)
                {
                    foreach (var reservedWord in _reservedWords)
                    {
                        if (_code[line].StartsWith(reservedWord + " ") || _code[line] == reservedWord)
                        {
                            end = line;
                            errorMessage = $"Ожидалась точка с запятой в названии подпрограммы по индексу {start}";
                            return false;
                        }
                    }
                }

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
                    else if (_code[line][i] == ')')
                    {
                        bracketNesting--;
                    }
                    else if (_code[line][i] == ';')
                    {
                        isEnded = true;
                        end = line;
                    }
                }

                if (isEnded)
                {
                    if (bracketNesting != 0)
                    {
                        errorMessage = $"Лишняя(ие) скобки в названии подпрограммы, начинающейся на строке {start}.";
                        return false;
                    }

                    if (end == _code.Length - 1 ||
                        _code[end + 1] != "begin")
                    {
                        errorMessage = $"Ожидался begin на строке {end+1}.";
                        return false;
                    }
                    
                    end = line;
                    return true;
                }

                line++;
            }

            errorMessage = $"Не закрыта подпрограмма в строке {start}.";
            return false;
        }
    }
}