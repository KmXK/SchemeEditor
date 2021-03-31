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
            "var", "uses"
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
                        var scheme = new Scheme(_startSettings);
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
                            $"Ожидался end для begin по индексу {areaStart}",
                            null);
                    }
                }
            }

            return new ParseResult(true, "", schemes);
        }

        private bool ReadOperator(Block main, int branchIndex, ref int childIndex, int start, out int end, out string errorCode)
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
                        if (_code[start].StartsWith("for"))
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

                main.AddChild(first, branchIndex, childIndex++);
                main.AddChild(second, branchIndex, childIndex++);

                int bodyIndex = conditionEnd + 1;

                _cycleNesting.Push(0);
                
                if (_code[bodyIndex].ToLower() == "begin")
                {
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
                main.AddChild(first, branchIndex, childIndex++);
                main.AddChild(second, branchIndex, childIndex++);
                
                
                
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
                        var line = _code[i];
                        if (i == end)
                        {
                            line = line.Remove(0, 5).Trim();
                        }

                        if (i == conditionEnd && line.Last() == ';')
                        {
                            line = line.Remove(line.Length - 1, 1).Trim();
                        }
                        
                        text.Add(line);
                    }
                    
                    
                    text.Insert(0, c + number.ToString());
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
                throw new NotImplementedException();
            }
            else
            {
                while (
                    !((_code[end + 1].ToLower().StartsWith("end") && _code[end + 1].Length <= 4) ||
                      _code[end + 1].ToLower() == "end" ||
                      _code[end + 1].ToLower() == "else" ||
                      _code[end].EndsWith(";"))
                )
                {
                    end++;
                }

                var text = new List<string>();
                for (int i = start; i <= end; i++)
                {
                    var line = _code[i];
                    if (i == end && line.Last()==';')
                    {
                        line = line.Remove(line.Length - 1, 1);
                    }
                    
                    text.Add(line);
                }

                Block block = new Block(BlockType.Default, text.ToArray(), new string[0])
                {
                    Width = _currentSettings.StandartWidth,
                    Height = _currentSettings.StandartHeight,
                    FontSize = _currentSettings.FontSize
                };

                main.AddChild(block, branchIndex, childIndex++);

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
                    _code[i].Remove(_code[i].IndexOf("//", StringComparison.Ordinal));
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