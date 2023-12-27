using System;
using System.Collections.Generic;
using System.Linq;

namespace Moka.Extension
{
    public static class StringExtension
    {
        public enum Include
        {
            None,
            Include
        }

        public enum Includes
        {
            None,
            Start,
            End,
            All
        }

        public static string Push(this string text, string value, Include include = Include.None)
        {
            if (string.IsNullOrEmpty(text)) { throw new Exception("전체 문자열이 null입니다"); }
            if (value == null) { throw new Exception("입력받은 문자열이 null입니다"); }
            int indexOf = text.IndexOf(value);
            if (indexOf < 0) { throw new Exception("시작 문자열을 찾을 수 없습니다"); }
            int startIndex = indexOf + value.Length;
            string substring = text.Substring(startIndex);
            switch (include)
            {
                case Include.Include:
                    return value + substring;
                default:
                    return substring;
            }
        }
        public static string Cut(this string text, string value, Include include = Include.None)
        {
            if (string.IsNullOrEmpty(text)) { throw new Exception("전체 문자열이 null입니다"); }
            if (value == null) { throw new Exception("입력받은 문자열이 null입니다"); }
            int indexOf = text.IndexOf(value);
            if (indexOf < 0) { throw new Exception("시작 문자열을 찾을 수 없습니다"); }
            string substring = text.Substring(0, indexOf);
            switch (include)
            {
                case Include.Include:
                    return substring + value;
                default:
                    return substring;
            }
        }
        public static string Substring(this string text, string startWord, string endWord, Includes includes = Includes.None)
        {
            string push = text.Push(startWord);
            string cut = push.Cut(endWord);
            switch (includes)
            {
                case Includes.Start:
                    return startWord + cut;
                case Includes.End:
                    return cut + endWord;
                case Includes.All:
                    return startWord + cut + endWord;
                default: return cut;
            }
        }
        public static List<string> Substrings(this string text, string startWord, string endWord, Includes includes = Includes.None)
        {
            if (string.IsNullOrEmpty(text)) { throw new Exception("전체 문자열이 null입니다"); }
            if (startWord == null) { throw new Exception("입력받은 시작 문자열이 null입니다"); }
            if (endWord == null) { throw new Exception("입력받은 종료 문자열이 null입니다"); }
            var list = new List<string>();
            while (true)
            {
                var start = text.IndexOf(startWord);
                if (start < 0) break;
                switch (includes)
                {
                    case Includes.End:
                    case Includes.None:
                        start += startWord.Length;
                        break;
                    default:
                        break;
                }

                text = text.Substring(start);
                var end = text.IndexOf(endWord);
                if (end < 0) break;
                switch (includes)
                {
                    case Includes.End:
                    case Includes.All:
                        end += endWord.Length;
                        break;
                    default:
                        break;
                }
                var subStringText = text.Substring(0, end);
                if (string.IsNullOrEmpty(subStringText))
                {
                    break;
                }
                else
                {
                    text = text.Substring(subStringText.Length);
                    list.Add(subStringText);
                }
            }
            return list;
        }

        public static List<string> Split(this string text, string value, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            return text.Split(new string[] { value }, stringSplitOptions).ToList();
        }
    }
}
