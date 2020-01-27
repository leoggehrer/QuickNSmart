using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpCodeGenerator.ConApp.Extensions
{
    public static class CSharpFormatterExtensions
    {
        public static string[] FormatCSharpCode(this string[] lines)
        {
            return lines.FormatCSharpCode(0);
        }
        public static string[] FormatCSharpCode(this string[] lines, int indent)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            var text = lines.ToText();
            var result = new List<string>();

            if (text.HasFullCodeBlock())
            {
                text.FormatCSharpCodeBlock(indent, result);
            }
            else
            {
                result.AddRange(lines);
            }
            return result.ToArray();
        }

        #region Helpers
        private static bool HasFullCodeBlock(this string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            var codeBegin = 0;
            var codeEnd = 0;

            foreach (var chr in text)
            {
                if (chr == '{')
                    codeBegin++;
                else if (chr == '}')
                    codeEnd++;
            }
            return codeBegin > 0 && codeBegin == codeEnd;
        }
        private static string TrimCSharpLine(this string text)
        {
            if (string.IsNullOrEmpty(text) == false)
            {
                var trimmer = new Regex(@"\s\s+");

                text = text.Replace("\t", string.Empty);
                text = text.Replace(Environment.NewLine, string.Empty);

                text = text.Trim();
                text = trimmer.Replace(text, " ");
            }
            return text;
        }
        private static bool GetBlockPositions(string text, ref int start, ref int end)
        {
            var cornerBraket = 0;
            var blockBegin = 0;
            var blockEnd = 0;
            var quotationMarks = 0;

            start = end = -1;
            for (var idx = 0; idx >= 0 && idx < text.Length && (start == -1 || end == -1); idx++)
            {
                var chr = text[idx];

                if (chr == '"')
                {
                    quotationMarks++;
                }
                else if (quotationMarks % 2 == 0)
                {
                    if (chr == '[')
                        cornerBraket++;
                    else if (chr == ']')
                        cornerBraket++;
                    else if (chr == '{' && cornerBraket % 2 == 0)
                    {
                        blockBegin++;
                        if (blockBegin == 1)
                            start = idx;
                    }
                    else if (chr == '}' && cornerBraket % 2 == 0)
                    {
                        blockEnd++;
                        if (blockEnd == blockBegin)
                            end = idx;
                    }
                }
            }
            return blockBegin > 0 && blockEnd > 0 && blockBegin == blockEnd;
        }
        private static string[] SplitCSharpAssignments(this string line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            var startIdx = -1;
            var partialStartIdx = -1;
            var partialEndIdx = 0;
            var lines = new List<string>();

            while ((partialEndIdx = line.IndexOf(';', startIdx + 1)) >= 0)
            {
                if (IsAssignmentSemicolon(line, partialEndIdx))
                {
                    lines.Add(line.Partialstring(partialStartIdx + 1, partialEndIdx).TrimCSharpLine());
                    partialStartIdx = partialEndIdx;
                    startIdx = partialStartIdx;
                }
                else
                {
                    startIdx++;
                }
            }
            string endPartial = line.Partialstring(partialStartIdx + 1, line.Length - 1).TrimCSharpLine();

            if (endPartial.Length > 0)
            {
                lines.Add(endPartial);
            }
            return lines.ToArray();
        }

        private static bool IsAssignmentSemicolon(string text, int pos)
        {
            text.CheckArgument(nameof(text));

            return IsLiteralCharacter(text, pos) == false && IsStringCharacter(text, pos) == false;
        }
        private static bool IsLiteralCharacter(string text, int pos)
        {
            text.CheckArgument(nameof(text));

            return pos > 0 && pos + 1 < text.Length && text[pos - 1] == '\'' && text[pos + 1] == '\'';
        }
        private static bool IsStringCharacter(string text, int pos)
        {
            text.CheckArgument(nameof(text));

            var limiterCount = 0;

            for (var i = 0; i < pos && i < text.Length; i++)
            {
                if (text[i] == '\"')
                    limiterCount++;
            }
            return limiterCount % 2 > 0;
        }

        private static string[] SplitCSharpLine(this string line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            var lines = new List<string>();

            lines.AddRange(line.SplitCSharpAssignments());

            var result = new List<string>();

            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i].Length > 0)
                {
                    int idx;

                    if ((idx = lines[i].IndexOf("/*", StringComparison.Ordinal)) >= 0
                        &&
                        (idx = lines[i].IndexOf("*/", idx + 1, StringComparison.Ordinal)) >= 0)
                    {
                        result.Add(lines[i]);
                    }
                    else if ((idx = lines[i].IndexOf("/*", StringComparison.Ordinal)) >= 0)
                    {
                        if (idx > 1)
                        {
                            string partLine = lines[i].Partialstring(0, idx - 1).TrimCSharpLine();

                            if (partLine.Length > 0)
                                result.Add(partLine);
                        }

                        result.Add("/*");
                        if (idx + 2 < lines[i].Length - 1)
                        {
                            string partLine = lines[i].Partialstring(idx + 2, lines[i].Length - 1).TrimCSharpLine();

                            if (partLine.Length > 0)
                                result.Add(partLine);
                        }
                    }
                    else if ((idx = lines[i].IndexOf("*/", StringComparison.Ordinal)) >= 0)
                    {
                        if (idx > 1)
                        {
                            string partLine = lines[i].Partialstring(0, idx - 1).TrimCSharpLine();

                            if (partLine.Length > 0)
                                result.Add(partLine);
                        }

                        result.Add("*/");
                        if (idx + 2 < lines[i].Length - 1)
                        {
                            string partLine = lines[i].Partialstring(idx + 2, lines[i].Length - 1).TrimCSharpLine();

                            if (partLine.Length > 0)
                                result.Add(partLine);
                        }
                    }
                    else
                    {
                        result.AddRange(lines[i].SplitCSharpLine('[', ']'));
                    }
                }
            }
            return result.ToArray();
        }
        private static string[] SplitCSharpLine(this string line, char left, char right)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            var lastIdx = -1;
            var startIdx = -1;
            var lines = new List<string>();

            static int IndexOf(string text, int start, char chr)
            {
                int result = -1;
                int quotationMarks = 0;

                for (int i = start; i < text.Length && result == -1; i++)
                {
                    if (text[i] == '"')
                        quotationMarks++;
                    else if (quotationMarks % 2 == 0 && text[i] == chr)
                        result = i;
                }
                return result;
            }

            int endIdx;
            while ((startIdx = IndexOf(line, startIdx + 1, left)) >= 0
                   && (endIdx = IndexOf(line, startIdx + 1, right)) > startIdx
                   && endIdx - startIdx > 1)
            {
                lines.Add(line.Partialstring(startIdx, endIdx).TrimCSharpLine());
                lastIdx = startIdx = endIdx;
            }
            string endPartial = line.Partialstring(lastIdx + 1, line.Length - 1).TrimCSharpLine();

            if (endPartial.Length > 0)
            {
                lines.Add(endPartial);
            }
            return lines.ToArray();
        }
        private static void FormatCSharpCodeBlock(this string text, int indent, List<string> lines)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            var beginPos = 0;
            var endPos = 0;

            void AddCodeLines(string txt, int idt, List<string> list)
            {
                string[] items = txt.SplitCSharpLine();

                list.AddRange(items.Where(l => l.Length > 0)
                    .Select(l => l.SetIndent(idt))
                    .ToArray());
            }

            if (GetBlockPositions(text, ref beginPos, ref endPos))
            {
                AddCodeLines(text.Partialstring(0, beginPos - 1), indent, lines);
                lines.Add("{".SetIndent(indent));
                text.Partialstring(beginPos + 1, endPos - 1).FormatCSharpCodeBlock(indent + 1, lines);
                lines.Add("}".SetIndent(indent));
                text.Partialstring(endPos + 1, text.Length - 1).FormatCSharpCodeBlock(indent, lines);
            }
            else
            {
                AddCodeLines(text, indent, lines);
            }
        }
        #endregion
    }
}
