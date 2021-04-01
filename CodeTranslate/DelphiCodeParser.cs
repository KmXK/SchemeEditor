using System;
using System.Collections.Generic;
using System.Linq;
using SchemeEditor.Schemes;
using SchemeEditor.Schemes.Blocks;

namespace SchemeEditor.CodeTranslate
{
    public class DelphiCodeParser
    {
        private string[] _code;
        private SchemeSettings _startSettings;
        private SchemeSettings _currentSettings;

        private Stack<int> _cycleNesting;

        private static string[] _reservedWords =
        {
            "begin", "end", "procedure", "function",
            "var", "uses", "if", "while", "for",
            "while", "program", "const"
        };
        
        public DelphiCodeParser(string[] code, SchemeSettings settings)
        {
            SetCode(code);
            _startSettings = settings;
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
                if (_code[i].ToLower().StartsWith("function") ||
                    _code[i].ToLower().StartsWith("procedure") || 
                    _code[i].ToLower().StartsWith("begin"))
                {
                    int areaStart;
                    string[] name = new string[0];

                    if (!_code[i].ToLower().StartsWith("begin"))
                    {
                        if (!CheckAreaName(i, out areaStart, out string message, out name))
                        {
                            return new ParseResult(false, message, null);
                        }

                        i = areaStart + 1;
                    }
                    
                    areaStart = i;
                    
                    if (FindEndOfArea(areaStart, out int areaEnd))
                    {
                        var scheme = new Scheme(_startSettings);

                        if (name.Length > 0)
                        {
                            name.CopyTo(scheme.MainBlock.GetChild(0, 0).Text, 0);
                            scheme.MainBlock.GetChild(0, 0).Text[0] =
                                "Вход " + scheme.MainBlock.GetChild(0, 0).Text[0];
                            
                            name.CopyTo(scheme.MainBlock.GetChild(0, 1).Text, 0);
                            scheme.MainBlock.GetChild(0, 1).Text[0] =
                                "Выход " + scheme.MainBlock.GetChild(0, 1).Text[0];
                        }
                        

                        _currentSettings = scheme.Settings;
                        i = areaEnd;
                        _cycleNesting = new Stack<int>();
                        _cycleNesting.Push(0);

                        if (ReadOperatorChilds(scheme.MainBlock, 0,1,areaStart + 1, out areaEnd, out string errorMessage))
                        {
                            schemes.Add(scheme);
                        }
                        else
                        {
                            return new ParseResult(false,
                                errorMessage,
                                null);
                        }
                    }
                    else
                    {
                        return new ParseResult(false,
                            $"Ожидался end для begin в строке {areaStart}",
                            null);
                    }
                }
            }

            return new ParseResult(true, "", schemes);
        }

        private bool ReadOperator(Block block, int branchIndex, ref int childIndex, int start, out int end, out string errorCode)
        {
            end = start;
            if (_code[start].ToLower().StartsWith("for ") || _code[start].ToLower() == "for" ||
                _code[start].ToLower().StartsWith("while ") || _code[start].ToLower() == "while")
            {
                int j = start;
                int conditionEnd = -1;
                while (!((_code[j].ToLower().StartsWith("end") && _code[j].Length <= 4) ||
                         _code[j].ToLower() == "end"))
                {
                    foreach (var reserved in _reservedWords)
                    {
                        if ((j != start && _code[j].ToLower().StartsWith(reserved + " ")) ||
                            _code[j].ToLower().EndsWith(" " + reserved) ||
                            _code[j].ToLower().Contains($" {reserved} ") ||
                            _code[j].ToLower() == reserved)
                        {
                            errorCode = $"Обнаружено зарезервированное слово внутри условия цикла в строке {start}";
                            return false;
                        }
                    }
                    
                    if (_code[j].ToLower().EndsWith(" do") || _code[j].ToLower() == "do")
                    {
                        conditionEnd = j;
                        break;
                    }

                    j++;
                }

                if (conditionEnd == -1)
                {
                    errorCode = $"Не найден do для цикла в строке {start}.";
                    return false;
                }
                
                // Получение условия
                var text = new List<string>();
                for (int i = start; i <= conditionEnd; i++)
                {
                    var line = _code[i];
                    if (i == start)
                    {
                        if (_code[start].ToLower().StartsWith("for"))
                        {
                            line = line.Remove(0, 3).Trim();
                        }
                        else
                        {
                            line = line.Remove(0, 5).Trim();
                        }
                    }

                    if (i == conditionEnd)
                    {
                        line = line.Remove(line.Length - 2, 2).Trim();
                    }

                    if (line.Length != 0)
                        text.Add(line);
                }

                // Получение номера цикла
                
                int number = _cycleNesting.Peek() + 1;
                char c = (char)(_cycleNesting.Count - 1 + (int)'A');
                string cycleName = c + number.ToString();
                
                _cycleNesting.Push(_cycleNesting.Pop() + 1);
                
                text.Insert(0, cycleName);

                Block first = new Block(BlockType.StartLoop, text.ToArray(), new string[1])
                {
                    Width = _currentSettings.StandartWidth,
                    Height = _currentSettings.StandartHeight,
                    FontSize = _currentSettings.FontSize
                };
                Block second = new Block(BlockType.EndLoop, new [] {cycleName}, new string[0])
                {
                    Width = _currentSettings.StandartWidth,
                    Height = _currentSettings.StandartHeight,
                    FontSize = _currentSettings.FontSize
                };

                block.AddChild(first, branchIndex, childIndex++);
                block.AddChild(second, branchIndex, childIndex++);

                int bodyIndex = conditionEnd + 1;

                _cycleNesting.Push(0);
                
                if (_code[bodyIndex].ToLower() == "begin")
                {
                    if(!FindEndOfArea(bodyIndex, out int areaEnd))
                    {
                        errorCode = $"Не найден end для begin по строке {bodyIndex}";
                        return false;
                    }
                    
                    if (ReadOperatorChilds(first,0, 0, bodyIndex + 1, out end, out errorCode))
                    {
                        errorCode = "";
                        end++;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    int ci = 0;
                    if (ReadOperator(first, 0, ref ci,bodyIndex, out end, out errorCode))
                    {
                        errorCode = "";
                    }
                    else
                    {
                        return false;
                    }
                }

                _cycleNesting.Pop();
                return true;
            }
            else if (_code[start].ToLower() == "repeat")
            {
                // TODO: Проверить существование until
                bool untilExists = false;
                int j = start+1;
                int n = 0;
                while (j <= _code.Length - 1)
                {
                    if (_code[j].ToLower().StartsWith("until ") ||
                        _code[j].ToLower() == "until")
                    {
                        if (n == 0)
                        {
                            untilExists = true;
                            break;
                        }
                        
                        n--;
                    }

                    if (_code[j].ToLower() == "repeat")
                        n++;
                    
                    j++;
                }

                if (!untilExists)
                {
                    errorCode = $"Не найден until для repeat в строке {start}";
                    return false;
                }

                Block first = new Block(BlockType.StartLoop, new string[0], new string[1])
                {
                    Width = _currentSettings.StandartWidth,
                    Height = _currentSettings.StandartHeight,
                    FontSize = _currentSettings.FontSize
                };
                Block second = new Block(BlockType.EndLoop, new string[0], new string[0])
                {
                    Width = _currentSettings.StandartWidth,
                    Height = _currentSettings.StandartHeight,
                    FontSize = _currentSettings.FontSize
                };
                block.AddChild(first, branchIndex, childIndex++);
                block.AddChild(second, branchIndex, childIndex++);

                // Получение номера цикла
                int number = _cycleNesting.Peek() + 1;
                char c = (char)(_cycleNesting.Count - 1 + (int)'A');
                string cycleName = c + number.ToString();
                
                _cycleNesting.Push(_cycleNesting.Pop() + 1);

                if (ReadOperatorChilds(first, 0, 0, start + 1, out end, out errorCode))
                {
                    _cycleNesting.Push(0);
                    
                    end++;

                    var text = new List<string>();

                    int conditionEnd = end;
                    
                    while(!(_code[conditionEnd].EndsWith(";") ||
                            _code[conditionEnd+1].ToLower()=="else" ||
                            (_code[conditionEnd+1].ToLower().StartsWith("end") && _code[conditionEnd+1].Length<=4 )))
                    {
                        conditionEnd++;
                    }

                    for (int i = end; i <= conditionEnd; i++)
                    {
                        foreach (var reserved in _reservedWords)
                        {
                            if ((i != end && _code[i].ToLower().StartsWith(reserved + " ")) ||
                                _code[i].ToLower().EndsWith(" " + reserved) ||
                                _code[i].ToLower().Contains($" {reserved} ") ||
                                _code[i].ToLower() == reserved)
                            {
                                errorCode = $"Обнаружено зарезервированное слово внутри условия цикла в строке {end}";
                                return false;
                            }
                        }
                        
                        var line = _code[i];
                        if (i == end)
                        {
                            line = line.Remove(0, 5).Trim();
                        }

                        if (i == conditionEnd && line.Last() == ';')
                        {
                            line = line.Remove(line.Length - 1, 1).Trim();
                        }

                        if (line.Length != 0)
                            text.Add(line);
                    }
                    
                    text.Add(cycleName);
                    
                    second.Text = text.ToArray();
                    first.Text = new[] {cycleName};
                    

                    _cycleNesting.Pop();
                    
                    // получить инфу о until 
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (_code[start].ToLower().StartsWith("if ") ||
                     _code[start].ToLower() == "if")
            {
                int j = start;
                int conditionEnd = -1;
                while (!((_code[j].ToLower().StartsWith("end") && _code[j].Length <= 4) ||
                         _code[j].ToLower() == "end"))
                {
                    foreach (var reserved in _reservedWords)
                    {
                        if ((j != start && _code[j].ToLower().StartsWith(reserved + " ")) ||
                            _code[j].ToLower().EndsWith(" " + reserved) ||
                            _code[j].ToLower().Contains($" {reserved} ") ||
                            _code[j].ToLower() == reserved)
                        {
                            errorCode = $"Обнаружено зарезервированное слово внутри условия в строке {start}";
                            return false;
                        }
                    }
                    
                    if (_code[j].ToLower().EndsWith(" then") || _code[j].ToLower() == "then")
                    {
                        conditionEnd = j;
                        break;
                    }

                    j++;
                }

                if (conditionEnd == -1)
                {
                    errorCode = $"Не найден then для условия в строке {start}.";
                    return false;
                }
                
                // Получение условия
                var text = new List<string>();
                for (int i = start; i <= conditionEnd; i++)
                {
                    var line = _code[i];
                    if (i == start)
                    {
                        if (_code[start].StartsWith("if"))
                        {
                            line = line.Remove(0, 2).Trim();
                        }
                    }

                    if (i == conditionEnd)
                    {
                        line = line.Remove(line.Length - 4, 4).Trim();
                    }

                    if (line.Length != 0)
                        text.Add(line);
                }

                Block ifBlock = new Block(BlockType.Condition, text.ToArray(), new[] {"T", "F"})
                {
                    Width = _currentSettings.StandartWidth,
                    Height = _currentSettings.StandartHeight,
                    FontSize = _currentSettings.FontSize
                };
                block.AddChild(ifBlock, branchIndex, childIndex++);

                int bodyStart = conditionEnd + 1;

                if (_code[bodyStart].ToLower() == "begin")
                {
                    if(!FindEndOfArea(bodyStart, out int areaEnd))
                    {
                        errorCode = $"Не найден end для begin по строке {bodyStart}";
                        return false;
                    }
                    
                    if (ReadOperatorChilds(ifBlock,0, 0, bodyStart + 1, out end, out errorCode))
                    {
                        errorCode = "";
                        end++;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    int ci = 0;
                    if (ReadOperator(ifBlock, 0, ref ci,bodyStart, out end, out errorCode))
                    {
                        errorCode = "";
                    }
                    else
                    {
                        return false;
                    }
                }

                if (_code[end + 1].ToLower() == "else")
                {
                    bodyStart = end + 2;
                    if (_code[bodyStart].ToLower() == "begin")
                    {
                        // TODO: Проверить наличие end
                    
                        if (ReadOperatorChilds(ifBlock,1, 0, bodyStart + 1, out end, out errorCode))
                        {
                            errorCode = "";
                            end++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        int ci = 0;
                        if (ReadOperator(ifBlock, 1, ref ci, bodyStart, out end, out errorCode))
                        {
                            errorCode = "";
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;

            }
            else
            {
                while (end<=_code.Length-2 &&
                    !((_code[end + 1].ToLower().StartsWith("end") && _code[end + 1].Length <= 4) ||
                      _code[end + 1].ToLower() == "end" ||
                      _code[end + 1].ToLower() == "else" ||
                      _code[end].EndsWith(";"))
                )
                {
                    end++;
                }

                if (end == _code.Length - 1)
                {
                    errorCode = $"Ожидался оператор в строке {start}";
                    return false;
                }

                var text = new List<string>();
                for (int i = start; i <= end; i++)
                {
                    var line = _code[i];
                    if (i == end && line.Last()==';')
                    {
                        line = line.Remove(line.Length - 1, 1);
                    }

                    if (line.Length != 0)
                        text.Add(line);
                }

                Block defBlock = new Block(BlockType.Default, text.ToArray(), new string[0])
                {
                    Width = _currentSettings.StandartWidth,
                    Height = _currentSettings.StandartHeight,
                    FontSize = _currentSettings.FontSize
                };

                block.AddChild(defBlock, branchIndex, childIndex++);

                errorCode = "";
                return true;

            }
        }
        
        private bool ReadOperatorChilds(Block block, int branchIndex, int childIndex, int childStart, out int end, out string errorCode)
        {
            end = childStart - 1;
            while (!((_code[end + 1].ToLower().StartsWith("end") && _code[end + 1].Length <= 4) ||
                     _code[end + 1].ToLower() == "end" || 
                     _code[end+1].ToLower().StartsWith("until") || 
                     _code[end+1].ToLower() == "else"))
            {
                end++;
                if (!ReadOperator(block, branchIndex, ref childIndex, end, out end, out errorCode))
                {
                    return false;
                }
            }

            errorCode = "";
            return true;
        }

        // Удаление лишних пробелов по краям строк, однострочных комментариев
        private void FormatCode()
        {
            var list = new List<string>();
            for (int i = 0; i < _code.Length; i++)
            {
                _code[i] = _code[i].Trim();
                if (_code[i].Contains("//"))
                {
                    _code[i] = _code[i].Remove(_code[i].IndexOf("//", StringComparison.Ordinal)).Trim();
                }
                
                if(_code[i].Length != 0)
                    list.Add(_code[i]);
            }

            _code = list.ToArray();
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
        private bool CheckAreaName(int start, out int end, out string errorMessage, out string[] name)
        {
            errorMessage = "";
            name = new string[0];
            end = start;
            
            var isEnded = false;
            var line = start;
            var bracketNesting = 0;
            var firstBracket = true;
            var delete = false;
            while (_code.Length - 1 >= line)
            {
                if(line != start)
                {
                    foreach (var reservedWord in _reservedWords)
                    {
                        if (_code[line].StartsWith(reservedWord + " ") ||
                            _code[line] == reservedWord ||
                            _code[line].EndsWith(" " + reservedWord) ||
                            _code[line].Contains($" {reservedWord} "))
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
                        if (!firstBracket && bracketNesting == 0)
                        {
                            errorMessage = $"Обнаружена лишняя скобка в строке {line}";
                            return false;
                        }
                        
                        bracketNesting++;
                        firstBracket = false;
                    }
                    else if (_code[line][i] == ')')
                    {
                        if (bracketNesting == 0)
                        {
                            errorMessage = $"Обнаружена лишняя скобка в строке {line}";
                            return false;
                        }

                        bracketNesting--;
                        delete = false;
                    }
                    else if (_code[line][i] == ';')
                    {
                        isEnded = bracketNesting == 0;
                        end = line;
                        delete = false;
                    }
                    else if (_code[line][i] == ':')
                    {
                        if (_code[start].StartsWith("procedure"))
                        {
                            errorMessage = $"Обнаружен возвращаемый параметр в процедуре в строке {line}";
                            return false;
                        }

                        if (delete)
                        {
                            errorMessage = $"Обнаружен лишний символ двоеточия в строке {line}";
                            return false;
                        }

                        delete = true;
                    }

                    if (delete)
                    {
                        _code[line] = _code[line].Remove(i--, 1);
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
                        errorMessage = $"Ожидался begin у подпрограммы в строке {start}.";
                        return false;
                    }
                    
                    // Достать название схемы
                    var list = new List<string>();
                    var isFunction = false;
                    for (int j = start; j <= end; j++)
                    {
                        var nameLine = _code[j];
                        if (nameLine.ToLower() == "function" ||
                            nameLine.ToLower() == "procedure")
                        {
                            continue;
                        }
                        
                        if (j == start)
                        {
                            if (_code[j].ToLower().StartsWith("function"))
                            {
                                nameLine = nameLine.Remove(0, "function".Length).Trim();
                                isFunction = true;
                            }
                            else
                            {
                                nameLine = nameLine.Remove(0, "procedure".Length).Trim();
                                isFunction = false;
                            }
                        }

                        if (j == end)
                        {
                            if (nameLine.Last() == ';')
                            {
                                nameLine = nameLine.Remove(nameLine.Length - 1, 1);
                            }

                            if (isFunction)
                            {
                                nameLine = nameLine.Remove(nameLine.Length - 1, 1);
                                nameLine = nameLine.Insert(nameLine.Length, ", Res)");
                            }
                        }

                        nameLine = nameLine.Replace(";", ",");
                        
                        list.Add(nameLine);
                    }

                    name = list.ToArray();
                    
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